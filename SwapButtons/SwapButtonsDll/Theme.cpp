// This file is part of Dual Monitor Tools which is a set of tools to assist
// users with multiple monitor setups.
// Copyright (C) 2011  Gerald Evans
// 
// Dual Monitor Tools is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.

#include "StdAfx.h"

#include <UxTheme.h>
#include <vsstyle.h>
#include <vssym32.h>

#include <dwmapi.h>

#include "Theme.h"
#include "ButtonList.h"

#pragma comment(linker,"\"/manifestdependency:type='win32' name='Microsoft.Windows.Common-Controls' version='6.0.0.0' processorArchitecture='*' publicKeyToken='6595b64144ccf1df' language='*'\"")

#define IS_TITLE_BUTTON_VISIBLE(dw) (dw & (STATE_SYSTEM_INVISIBLE | STATE_SYSTEM_OFFSCREEN | STATE_SYSTEM_UNAVAILABLE)) == 0

#define TRANSPARENT_COLOUR	(RGB(0, 255, 0))

CTheme::CTheme(void)
	: m_hbmBackground(NULL)
{
}

// virtual
CTheme::~CTheme(void)
{
	if (m_hbmBackground)
	{
		DeleteObject(m_hbmBackground);
	}
}

void CTheme::GrabThemeData(HWND hWndFrame)
{
	m_hWndFrame = hWndFrame;

	HTHEME hTheme = OpenThemeData(hWndFrame, L"WINDOW");
	if (hTheme)
	{
		if (m_hbmBackground)
		{
			DeleteObject(m_hbmBackground);
			m_hbmBackground = NULL;
		}

		HRESULT hr = GetThemeBitmap(hTheme, WP_CAPTION, CS_ACTIVE, TMT_DIBDATA, GBF_COPY, &m_hbmBackground);
		//HRESULT hr = GetThemeBitmap(hTheme, WP_MAXBUTTON, MAXBS_NORMAL, TMT_DIBDATA, GBF_COPY, &m_hbmBackground);
		//HRESULT hr = GetThemeBitmap(hTheme, WP_VERTSCROLL, VSS_NORMAL, TMT_GLYPHDIBDATA, GBF_COPY, &m_hbmBackground);
		if (hr == S_OK)
		{
			OutputDebugString(L"Got background\n");
			if (m_hbmBackground)
			{
				BITMAP bm;
				GetObject(m_hbmBackground, sizeof(bm), &bm);
				m_nButtonWidth = bm.bmWidth;
				m_nButtonHeight = bm.bmHeight;
			}
		}

		int nWidth = GetThemeSysSize(hTheme, SM_CXSIZE);
		int nHeight = GetThemeSysSize(hTheme, SM_CYSIZE);

		wchar_t szThemeName[256];
		hr = GetCurrentThemeName(szThemeName, 256, 0, 0, 0, 0);

		wchar_t szFilename[256];
		hr = GetThemeFilename(hTheme, WP_MAXBUTTON, MAXBS_NORMAL, TMT_IMAGEFILE, szFilename, 256);

		COLORREF clrBg;
		hr = GetThemeColor(hTheme, WP_CAPTION, CS_ACTIVE, TMT_COLOR, &clrBg);
		hr = GetThemeColor(hTheme, WP_CAPTION, CS_ACTIVE, TMT_BACKGROUND, &clrBg);
		hr = GetThemeColor(hTheme, WP_CAPTION, CS_ACTIVE, TMT_FILLCOLOR, &clrBg);
		hr = GetThemeColor(hTheme, WP_CAPTION, CS_ACTIVE, TMT_ACTIVECAPTION, &clrBg);
		hr = GetThemeColor(hTheme, WP_CAPTION, CS_ACTIVE, TMT_BTNFACE, &clrBg);
		hr = GetThemeColor(hTheme, WP_CAPTION, CS_ACTIVE, TMT_WINDOW, &clrBg);

		GrabThemeBackgroundColour(hTheme);

		CloseThemeData(hTheme);
	}
}

void CTheme::GrabThemeBackgroundColour(HTHEME hTheme)
{
	HRESULT hr;
	DWORD color = 0;
	BOOL opaque = FALSE;

	hr = DwmGetColorizationColor(&color, &opaque);
	if (hr == S_OK)
	{
		m_clrBackground = ARGBToColorref(color);
	}
	else
	{

		// desperation: settle on a light grey
		m_clrBackground = RGB(224, 224, 224);
	}
}

// static
COLORREF CTheme::ARGBToColorref(DWORD ARGB)
{
	DWORD red =   (ARGB & 0x00FF0000) >> 16;
	DWORD green = (ARGB & 0x0000ff00) >> 8;
	DWORD blue =  (ARGB & 0x000000FF);
	DWORD alpha = (ARGB & 0xFF000000) >> 24;

	// assume the colour underneath is going to be white
	DWORD blend = 0xFF;	// for red, green and blue
	// and adust the colours according to the alpha

#define BLEND_COLOUR(in, alpha, blend) ((in * alpha + blend * (0xFF - alpha)) / 0xFF)
	red = BLEND_COLOUR(red, alpha, blend);
	green = BLEND_COLOUR(green, alpha, blend);
	blue = BLEND_COLOUR(blue, alpha, blend);
#undef BLEND_COLOUR

	return RGB(red, green, blue);
}

// Called once only per FloatBar (ideally during WM_CREATE of the FloatBar)
void CTheme::Init(HWND hWndFloatBar)
{
	// set the layered style
	DWORD dwExStyle = GetWindowLong(hWndFloatBar, GWL_EXSTYLE);
	dwExStyle |= WS_EX_LAYERED;
	SetWindowLong(hWndFloatBar, GWL_EXSTYLE, dwExStyle);

	// set the colour we are going to use as transparent
	SetLayeredWindowAttributes(hWndFloatBar, TRANSPARENT_COLOUR, 0, LWA_COLORKEY);
}

//void CTheme::Activate(HWND hWndFloatBar)
//{
//	MARGINS margins = { -1, -1, -1, -1 };
//	HRESULT hr = DwmExtendFrameIntoClientArea(hWndFloatBar, &margins);
//}

// calculates:
//	int m_nButtonHeight;
//  int m_nButtonWidth;
//  int m_nRightOffset;
//	int m_nTopOffset;
//
// This needs to be called whenever the size of the frame window changes,
// the theme changes or the buttons change
void CTheme::CalcPositioning(HWND hWndFrame)
{
	RECT rectFrame;	// in screen co-ords
	GetWindowRect(hWndFrame, &rectFrame);

	DWORD dwStyle = GetWindowLong(hWndFrame, GWL_STYLE);
	DWORD dwExStyle = GetWindowLong(hWndFrame, GWL_EXSTYLE);

	int nTopPos = rectFrame.top;
	int nRightPos = rectFrame.right;	// right position in screen co-ords
	int nButtonSize = 0;	// used for both width and height

	int nStdButtonSize;
	if (dwExStyle & WS_EX_TOOLWINDOW)
	{
		nStdButtonSize = GetSystemMetrics(SM_CYSMSIZE);
	}
	else
	{
		//int nWidth = GetSystemMetrics(SM_CXSIZE);
		//int nHeight = GetSystemMetrics(SM_CYSIZE);

		nStdButtonSize = GetSystemMetrics(SM_CYSIZE);
	}
	int nStdSpacing = 2; // x pixels between std buttons

	if (IsPreVista())
	{
		// pre-Vista i.e. no WM_GETTITLEBARINFOEX

		// adjust for the border
		if (dwStyle & WS_THICKFRAME)
		{
			nRightPos -= GetSystemMetrics(SM_CXSIZEFRAME);
			nTopPos += GetSystemMetrics(SM_CYSIZEFRAME);
		}
		else
		{
			nRightPos -= GetSystemMetrics(SM_CXFIXEDFRAME);
			nTopPos += GetSystemMetrics(SM_CYFIXEDFRAME);
		}

		// adjust for any titlebar 
		TITLEBARINFO titleBarInfo;
		titleBarInfo.cbSize = sizeof(titleBarInfo);
		GetTitleBarInfo(hWndFrame, &titleBarInfo);
		// TODO: are there defines for the child indexes?
		//if (dwStyle & WS_SYSMENU)
		if (IS_TITLE_BUTTON_VISIBLE(titleBarInfo.rgstate[5]))
		{
			nRightPos -= (nStdButtonSize + nStdSpacing);
		}

		// Alt: could use titleBarInfo for these as well
		if (dwStyle & (WS_MINIMIZEBOX | WS_MAXIMIZEBOX))
		{
			nRightPos -= (nStdButtonSize + nStdSpacing) * 2;
		}
		else if (dwExStyle & WS_EX_CONTEXTHELP)
		{
			nRightPos -= (nStdButtonSize + nStdSpacing);
		}
	}
	else
	{
		// Vista & later only
		TITLEBARINFOEX titleBarInfoEx;
		titleBarInfoEx.cbSize = sizeof(titleBarInfoEx);
		SendMessage(hWndFrame, WM_GETTITLEBARINFOEX, 0, (LPARAM)&titleBarInfoEx);

		// iterate over all of the children on the titlebar
		// finding the one with the minimum x co-ord
		// TODO: are there defines for the child indexes?
		for (int nTitleBarChild = 2; nTitleBarChild <= 5; nTitleBarChild++)
		{
			if ((titleBarInfoEx.rgstate[nTitleBarChild] & (STATE_SYSTEM_INVISIBLE | STATE_SYSTEM_OFFSCREEN | STATE_SYSTEM_UNAVAILABLE)) == 0)
			{
				// button should be visible
				nTopPos = titleBarInfoEx.rgrect[nTitleBarChild].top;
				nButtonSize = titleBarInfoEx.rgrect[nTitleBarChild].bottom - titleBarInfoEx.rgrect[nTitleBarChild].top;
				if (titleBarInfoEx.rgrect[nTitleBarChild].left < nRightPos)
				{
					nRightPos = titleBarInfoEx.rgrect[nTitleBarChild].left;
					// should be able to break here as indexes are in button order (left to right)
				}
			}
		}
	}

	if (nButtonSize == 0)
	{
		nButtonSize = nStdButtonSize;
	}

	// adjust nRightPos so there is a button width between our buttons and std buttons 
	nRightPos -= nButtonSize;

	m_nRightOffset = rectFrame.right - nRightPos;
	m_nTopOffset = nTopPos - rectFrame.top;
	m_nButtonWidth = nButtonSize;
	m_nButtonHeight = nButtonSize;

	// TODO: this is a temporary fudge
	m_nButtonWidth += 4;
	m_nButtonHeight -= 2;


	//if (m_hbmBackground)
	//{
	//	BITMAP bm;
	//	GetObject(m_hbmBackground, sizeof(bm), &bm);
	//	m_nButtonWidth = bm.bmWidth;
	//	m_nButtonHeight = bm.bmHeight;
	//}


}

RECT CTheme::GetBarRect(HWND hWndFrame, const CButtonList& buttonList)
{
	// CalcPositioning() must have been called previously

	RECT rectFloatBar;

	RECT rectFrame;
	// get frame rect in screen co-ords
	GetWindowRect(hWndFrame, &rectFrame);

	SIZE size = GetBarSize(buttonList);

	rectFloatBar.right = rectFrame.right - m_nRightOffset;
	rectFloatBar.left = rectFloatBar.right - size.cx;
	rectFloatBar.top = rectFrame.top + m_nTopOffset;
	rectFloatBar.bottom = rectFloatBar.top + size.cy;

	return rectFloatBar;
}

SIZE CTheme::GetBarSize(const CButtonList& buttonList)
{
	SIZE size;

	// CalcPositioning() must have been called previously

	// add single pixel border all around except at top
	size.cx = 1 + 1; 
	size.cy = 1;
	int index;
	int count = buttonList.Count();
	for (index = 0; index < count; index++)
	{
		if (index != 0)
		{
			// add in seperator
			size.cx += 2;
		}
		size.cx += m_nButtonWidth;
	}
	size.cy += m_nButtonHeight;

	return size;
}

void CTheme::PaintBar(HWND hWndFloatBar, const CButtonList& buttonList)
{
	if (hWndFloatBar)
	{
		PAINTSTRUCT ps;

		HDC hDC = BeginPaint(hWndFloatBar, &ps);
		RECT rectBar;
		RECT rectButton;
		GetClientRect(hWndFloatBar, &rectBar);

		// Transparency is not what we need as clicks go to whats below us
		//HBRUSH hBrush = CreateSolidBrush(TRANSPARENT_COLOUR);
		//HBRUSH hBrush = CreateSolidBrush(RGB(224, 224, 224));
		HBRUSH hBrush = CreateSolidBrush(m_clrBackground);
		FillRect(hDC, &rectBar, hBrush);

		// the pen to draw border around buttons
		HPEN hPen = CreatePen(PS_SOLID, 1, RGB(128, 128, 128));
		HPEN hOldPen = (HPEN)SelectObject(hDC, hPen);


		int x = rectBar.left + 1;
		int y = rectBar.top;
		int index;
		int count = buttonList.Count();
		for (index = 0; index < count; index++)
		{
			if (index != 0)
			{
				// add in seperator
				MoveToEx(hDC, x, rectBar.top, NULL);
				LineTo(hDC, x, rectBar.bottom - 1);

				x += 2;
			}


			//SIZE buttonSize = m_ButtonList.GetSize(index);
			rectButton.left = x;
			rectButton.top = y;
			rectButton.right = x + m_nButtonWidth; //buttonSize.cx;
			rectButton.bottom = y + m_nButtonHeight;//buttonSize.cy;
			x += m_nButtonWidth;//buttonSize.cx;

			//if (m_hbmBackground)
			//{
			//	HBITMAP hbmOld = (HBITMAP)SelectObject(hDC, m_hbmBackground);

			//	BitBlt(hDC, x, y, m_nButtonWidth, m_nButtonHeight, hDC, 0, 0, SRCCOPY);

			//	SelectObject(hDC, hbmOld);
			//}

	//HTHEME hTheme = OpenThemeData(m_hWndFrame, L"WINDOW");
	//if (hTheme)
	//{

	//	HRESULT hr = DrawThemeBackground(hTheme, hDC, WP_MAXBUTTON, MAXBS_NORMAL, &rectButton, NULL);
	//	if (hr == S_OK)
	//	{
	//		OutputDebugString(L"background painted\n");
	//	}

	//	CloseThemeData(hTheme);
	//}


			buttonList.Paint(index, hDC, rectButton);
		}

		// PaintBorder
		int nArcRadius = 4;

		MoveToEx(hDC, rectBar.left, rectBar.top, NULL);
		LineTo(hDC, rectBar.left, rectBar.bottom - 1 - nArcRadius);
		// TODO: need AA
		Arc(hDC, rectBar.left, rectBar.bottom - nArcRadius - nArcRadius,
			rectBar.left + nArcRadius * 2, rectBar.bottom,
			// direction is counter clockwide by default
			rectBar.left, rectBar.bottom - nArcRadius,
			rectBar.left + nArcRadius, rectBar.bottom);

			//	Ellipse(hDC, rectBar.left, rectBar.bottom - nArcRadius - nArcRadius,
			//rectBar.left + nArcRadius * 2, rectBar.bottom);

		MoveToEx(hDC, rectBar.left + nArcRadius - 1, rectBar.bottom - 1, NULL);
		LineTo(hDC, rectBar.right - 1 - nArcRadius, rectBar.bottom - 1);

		Arc(hDC, rectBar.right - 2 * nArcRadius, rectBar.bottom - nArcRadius - nArcRadius,
			rectBar.right, rectBar.bottom,
			// direction is counter clockwide by default
			rectBar.right - nArcRadius, rectBar.bottom,
			rectBar.right, rectBar.bottom - nArcRadius);

			//Ellipse(hDC, rectBar.right - 2 * nArcRadius, rectBar.bottom - nArcRadius - nArcRadius,
			//rectBar.right, rectBar.bottom);

		MoveToEx(hDC, rectBar.right - 1, rectBar.top, NULL);
		LineTo(hDC, rectBar.right - 1, rectBar.bottom - nArcRadius);



		// restore DC to original state 
		SelectObject(hDC, hOldPen);

		EndPaint(hWndFloatBar, &ps);
	}
}

int CTheme::GetButtonWidth()
{
	return m_nButtonWidth;
}

// static
bool CTheme::IsPreVista()
{
	return false;
	//return true;
}
