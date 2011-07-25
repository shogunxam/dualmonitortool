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

#include <dwmapi.h>
#include <UxTheme.h>
#include <vsstyle.h>
#include <vssym32.h>

#include "ThemeBasic.h"
#include "LayoutManager.h"
#include "ButtonList.h"
#include "ImageStrip.h"

#include "ResourceDll.h"


const int CThemeBasic::LEFT_BORDER = 0;
const int CThemeBasic::RIGHT_BORDER = 0;
const int CThemeBasic::TOP_BORDER = 0;
const int CThemeBasic::BOTTOM_BORDER = 0;
const int CThemeBasic::BUTTON_SPACING = 2;


CThemeBasic::CThemeBasic(void)
	: m_hTheme(NULL),
	  m_hDCMem(NULL),
	  m_hbmBar(NULL),
	  m_hbmOld(NULL),
	  m_hbmBackground(NULL),
	  m_bCheckedAvailable(false),
	  m_bIsAvailable(false),
	  m_hDwmLib(NULL),
	  m_hUxThemeLib(NULL),
	  m_pfnGetThemeBitmap(NULL),
	  m_hbmPrevMask(NULL),
	  m_hbmNextMask(NULL),
	  m_hbmSupersizeMask(NULL)
{
}

// virtual
CThemeBasic::~CThemeBasic(void)
{
	if (m_hbmSupersizeMask)
	{
		DeleteObject(m_hbmSupersizeMask);
	}
	if (m_hbmNextMask)
	{
		DeleteObject(m_hbmNextMask);
	}
	if (m_hbmPrevMask)
	{
		DeleteObject(m_hbmPrevMask);
	}

	if (m_hbmBackground)
	{
		DeleteObject(m_hbmBackground);
	}
	if (m_hUxThemeLib)
	{
		FreeLibrary(m_hUxThemeLib);
	}
	if (m_hDwmLib)
	{
		FreeLibrary(m_hDwmLib);
	}
	if (m_hTheme)
	{
		CloseThemeData(m_hTheme);
	}
}

// virtual
void CThemeBasic::LoadBitmaps(HMODULE hModule)
{
	// the glyphs to display on the buttons
	m_hbmPrev = LoadBitmap(hModule, MAKEINTRESOURCE(IDB_PREV));
	m_hbmNext = LoadBitmap(hModule, MAKEINTRESOURCE(IDB_NEXT));
	m_hbmSupersize = LoadBitmap(hModule, MAKEINTRESOURCE(IDB_SUPERSIZE));

	// create the masks for these glyphs
	m_hbmPrevMask = CreateMask(m_hbmPrev);
	m_hbmNextMask = CreateMask(m_hbmNext);
	m_hbmSupersizeMask = CreateMask(m_hbmSupersize);
}

// virtual 
bool CThemeBasic::IsAvailable()
{
	if (!m_bCheckedAvailable)
	{
		CheckIfAvailable();
	}
	return m_bIsAvailable;
}

// private
void CThemeBasic::CheckIfAvailable()
{
	m_hUxThemeLib = LoadLibrary(L"uxtheme.dll");
	if (m_hUxThemeLib)
	{
		m_bIsAvailable = true;
		// GetThemeBitmap() is only available on Vista or later
		m_pfnGetThemeBitmap = (PGetThemeBitmap)GetProcAddress(m_hUxThemeLib, "GetThemeBitmap");
	}
	m_bCheckedAvailable = true;
}

bool CThemeBasic::IsInUse(HWND hWndFrame)
{
	// TODO: how do we determine this?
	return true;
}

// virtual 
bool CThemeBasic::ReInit(struct LayoutMetrics* pLayoutMetrics, HWND hWndFrame)
{
	if (m_hTheme)
	{
		CloseThemeData(m_hTheme);
		m_hTheme = NULL;
	}
	m_hTheme = OpenThemeData(hWndFrame, L"WINDOW");
	if (m_hTheme)
	{
		m_nButtonWidth = 0;
		m_nButtonHeight = 0;
		// remove any existing button background
		if (m_hbmBackground)
		{
			DeleteObject(m_hbmBackground);
			m_hbmBackground = NULL;
		}

		HRESULT hr;

		MARGINS margins;
		hr = GetThemeMargins(m_hTheme, NULL, WP_MINBUTTON, 0, TMT_SIZINGMARGINS, NULL, &margins);

		int nLayout;
		hr = GetThemeEnumValue(m_hTheme, WP_MINBUTTON, 0, TMT_IMAGELAYOUT, &nLayout);

		RECT rectButton;
		hr = GetThemeRect(m_hTheme, WP_MINBUTTON, 0, TMT_DEFAULTPANESIZE, &rectButton);
		int nW;
		int nH;
		hr = GetThemeInt(m_hTheme, WP_MINBUTTON, 0, TMT_WIDTH, &nW);
		hr = GetThemeInt(m_hTheme, WP_MAXBUTTON, 0, TMT_WIDTH, &nW);
		hr = GetThemeInt(m_hTheme, WP_CLOSEBUTTON, 0, TMT_WIDTH, &nW);
		hr = GetThemeMetric(m_hTheme, NULL, WP_MINBUTTON, 0, TMT_HEIGHT, &nH);

		SIZE sz;
		hr = GetThemePartSize(m_hTheme, NULL, WP_MINBUTTON, 0, NULL, TS_TRUE, &sz);
		hr = GetThemePartSize(m_hTheme, NULL, WP_MAXBUTTON, 0, NULL, TS_TRUE, &sz);
		hr = GetThemePartSize(m_hTheme, NULL, WP_CLOSEBUTTON, 0, NULL, TS_TRUE, &sz);

		//HRESULT hr = GetThemeBitmap(hTheme, WP_CAPTION, CS_ACTIVE, TMT_DIBDATA, GBF_COPY, &m_hbmBackground);
		//HRESULT hr = GetThemeBitmap(m_hTheme, WP_CLOSEBUTTON, CBS_NORMAL, TMT_DIBDATA, GBF_COPY, &m_hbmBackground);
		//HRESULT hr = GetThemeBitmap(m_hTheme, WP_CAPTION, 0, TMT_DIBDATA, GBF_COPY, &m_hbmBackground);
		//HRESULT hr = GetThemeBitmap(m_hTheme, WP_MINBUTTON, MINBS_NORMAL, TMT_DIBDATA, GBF_COPY, &m_hbmBackground);
		//HRESULT hr = GetThemeBitmap(m_hTheme, WP_MINBUTTON, 0, TMT_DIBDATA, GBF_COPY, &m_hbmBackground);

		SaveBackgroundBitmap();

		SaveButtonSize(hWndFrame);

		m_ImageStrip.SetImageStrip(m_hbmBackground, 8, margins, nLayout == IL_VERTICAL);

		pLayoutMetrics->m_nButtonWidth = m_nButtonWidth;
		pLayoutMetrics->m_nButtonHeight = m_nButtonHeight;

		wchar_t szMsg[256];
		wsprintf(szMsg, L"Width: %d Height: %d\n", pLayoutMetrics->m_nButtonWidth, pLayoutMetrics->m_nButtonHeight);
		OutputDebugString(szMsg);


		pLayoutMetrics->m_nLeftBorder = LEFT_BORDER;
		pLayoutMetrics->m_nRightBorder = RIGHT_BORDER;
		pLayoutMetrics->m_nTopBorder = TOP_BORDER;
		pLayoutMetrics->m_nBottomBorder = BOTTOM_BORDER;
		pLayoutMetrics->m_nSpacing = BUTTON_SPACING;

		SaveBgrColour();

		return true;
	}

	return false;
}

// private
void CThemeBasic::SaveButtonSize(HWND hWndFrame)
{
	DWORD dwExStyle = GetWindowLong(hWndFrame, GWL_EXSTYLE);
	int nxSize;
	int nySize;

	//int nButtonWidth = GetSystemMetrics(SM_CXSIZE);
	//int nButtonHeight = GetSystemMetrics(SM_CYSIZE);
	if (dwExStyle & WS_EX_TOOLWINDOW)
	{
		nxSize = GetSystemMetrics(SM_CXSMSIZE);
		nySize = GetSystemMetrics(SM_CYSMSIZE);
	}
	else
	{
		nxSize = GetSystemMetrics(SM_CXSIZE);
		nySize = GetSystemMetrics(SM_CYSIZE);
	}

	//TODO: this is a temprary fudge
	m_nButtonWidth = nxSize -4; // was - 2;
	m_nButtonHeight = nySize - 4;

	// HACK: for Vista
	//m_nButtonWidth = 28;
	//m_nButtonHeight = 15;
}

// private
void CThemeBasic::SaveBackgroundBitmap()
{
	HRESULT hr;
	HBITMAP hbm;

	if (m_pfnGetThemeBitmap)
	{
		hr = m_pfnGetThemeBitmap(m_hTheme, WP_MINBUTTON, 0, TMT_DIBDATA, GBF_COPY, &hbm);
		if (SUCCEEDED(hr))
		{
			m_hbmBackground = hbm;
		}
	}
	else
	{
		wchar_t szThemeFnm[MAX_PATH];
		wchar_t szBitmapName[MAX_PATH];	// not really a path

		// get the path to the theme
		hr = GetCurrentThemeName(szThemeFnm, MAX_PATH, NULL, 0, NULL, 0);
		if (SUCCEEDED(hr))
		{
			hr = GetThemeFilename(m_hTheme, WP_MINBUTTON, 0, TMT_IMAGEFILE, szBitmapName, MAX_PATH);
			if (SUCCEEDED(hr))
			{
				// convert the bitmap name from a path style name to a resource style name
				ConvertPathToResourceName(szBitmapName);

				HMODULE hThemeModule = LoadLibraryEx(szThemeFnm, NULL, LOAD_LIBRARY_AS_DATAFILE);
				if (hThemeModule)
				{
					m_hbmBackground = (HBITMAP)LoadImage(hThemeModule, szBitmapName, IMAGE_BITMAP, 0, 0, LR_CREATEDIBSECTION);
					FreeLibrary(hThemeModule);
				}
			}
		}
	}
}

// private
void CThemeBasic::ConvertPathToResourceName(wchar_t* pszName)
{
	for (wchar_t* psz = pszName; *psz; psz++)
	{
		if (isalnum(*psz))
		{
			*psz = toupper(*psz);
		}
		else
		{
			*psz = '_';
		}
	}
}

// private
void CThemeBasic::SaveBgrColour()
{
	if (m_bIsAvailable)
	{
		//DWORD color = 0;
		//BOOL opaque = FALSE;

		//HRESULT hr = DwmGetColorizationColor(&color, &opaque);
		//if (SUCCEEDED(hr))
		//{
		//	m_BgrColour = ARGBToColorref(color);
		//}

		COLORREF color;
		HRESULT hr;
		hr = GetThemeColor(m_hTheme, WP_CAPTION, CS_ACTIVE, TMT_ACTIVECAPTION, &color);
		if (SUCCEEDED(hr))
		{
			m_BgrColour = color;
		}
		hr = GetThemeColor(m_hTheme, WP_CAPTION, CS_ACTIVE, TMT_BACKGROUND, &color);
		if (SUCCEEDED(hr))
		{
			m_BgrColour = color;
		}
		hr = GetThemeColor(m_hTheme, WP_CAPTION, CS_ACTIVE, TMT_FILLCOLOR, &color);
		if (SUCCEEDED(hr))
		{
			m_BgrColour = color;
		}
		hr = GetThemeColor(m_hTheme, WP_CAPTION, CS_ACTIVE, TMT_FILLCOLORHINT, &color);
		if (SUCCEEDED(hr))
		{
			m_BgrColour = color;
		}
		hr = GetThemeColor(m_hTheme, WP_CAPTION, CS_ACTIVE, TMT_TRANSPARENTCOLOR, &color);
		if (SUCCEEDED(hr))
		{
			m_BgrColour = color;
		}

	}
}

#define IS_TITLE_BUTTON_VISIBLE(dw) (dw & (STATE_SYSTEM_INVISIBLE | STATE_SYSTEM_OFFSCREEN | STATE_SYSTEM_UNAVAILABLE)) == 0

// virtual 
//SIZE CThemeBasic::CalcBarOffsets(HWND hWndFrame)
//{
//	int nExistingButtonsWidth = 0;
//	DWORD dwStyle = GetWindowLong(hWndFrame, GWL_STYLE);
//	DWORD dwExStyle = GetWindowLong(hWndFrame, GWL_EXSTYLE);
//	RECT rectFrame;
//	GetWindowRect(hWndFrame, &rectFrame);
//	int nRightPos = rectFrame.right;
//	int nTopPos = rectFrame.top;
//
//	int nStdButtonSize = 0;
//	if (dwExStyle & WS_EX_TOOLWINDOW)
//	{
//		nStdButtonSize = GetSystemMetrics(SM_CYSMSIZE);
//	}
//	else
//	{
//		nStdButtonSize = GetSystemMetrics(SM_CYSIZE);
//	}
//	int nStdSpacing = 2; // x pixels between std buttons
//
//	// NOTE: DWMWA_CAPTION_BUTTON_BOUNDS may also be of use?
//
//	if (IsPreVista())
//	{
//	}
//	else
//	{
//		// Vista & later only
//		TITLEBARINFOEX titleBarInfoEx;
//		titleBarInfoEx.cbSize = sizeof(titleBarInfoEx);
//		SendMessage(hWndFrame, WM_GETTITLEBARINFOEX, 0, (LPARAM)&titleBarInfoEx);
//
//		// iterate over all of the children on the titlebar
//		// finding the one with the minimum x co-ord
//		// TODO: are there defines for the child indexes?
//		for (int nTitleBarChild = 2; nTitleBarChild <= 5; nTitleBarChild++)
//		{
//			if ((titleBarInfoEx.rgstate[nTitleBarChild] & (STATE_SYSTEM_INVISIBLE | STATE_SYSTEM_OFFSCREEN | STATE_SYSTEM_UNAVAILABLE)) == 0)
//			{
//				// button should be visible
//				nTopPos = titleBarInfoEx.rgrect[nTitleBarChild].top;
//				if (titleBarInfoEx.rgrect[nTitleBarChild].left < nRightPos)
//				{
//					nRightPos = titleBarInfoEx.rgrect[nTitleBarChild].left;
//					// should be able to break here as indexes are in button order (left to right)
//				}
//			}
//		}
//	}
//
//	nRightPos -= nStdButtonSize / 2; // to allow spacing between the button sets
//
//	SIZE offsets;
//	offsets.cx = rectFrame.right - nRightPos;
//	offsets.cy = nTopPos - rectFrame.top;
//	return offsets;
//}


// virtual 
void CThemeBasic::PrepareFloatBar(HWND hWndFloatBar)
{
}

//static DWORD* m_pdwBits = NULL;
//static int m_nWidth;
//static int m_nHeight;
//static RECT m_RectBar;

// virtual 
void CThemeBasic::PaintBar(HWND hWndFloatBar, HWND hWndFrame, HDC hDC, const CButtonList& buttonList, RECT rectBar, bool bActive, int nHoverIndex)
{
	PaintStart(hDC, rectBar);


	//// test start
	//// not all clients process WM_PRINTCLIENT, so this si not a solution
	//POINT pt;
	//pt.x = 0;
	//pt.y = 0;
	//MapWindowPoints(hWndFloatBar, hWndFrame, &pt, 1);
	//RECT rc;
	//GetClipBox(m_hDCMem, &rc);
	////SetViewportOrgEx(m_hDCMem, -pt.x - rc.left, -pt.y - rc.top, &pt);
	//SendMessage(hWndFrame, WM_PRINTCLIENT, WPARAM(m_hDCMem), PRF_NONCLIENT);
	////SetViewportOrgEx(m_hDCMem, pt.x, pt.y, NULL);
	//// test end

		//HRESULT hr;
		//SIZE sz;
		//hr = GetThemePartSize(m_hTheme, m_hDCMem, WP_MINBUTTON, 1, &rectBar, TS_DRAW, &sz);

	int x = rectBar.left + LEFT_BORDER;
	int y = rectBar.top + TOP_BORDER;
	int index;
	int count = buttonList.Count();
	RECT rectButton;
	bool bHover;

	for (index = 0; index < count; index++)
	{
		rectButton.left = x;
		rectButton.right = rectButton.left + m_nButtonWidth;
		rectButton.top = y;
		rectButton.bottom = rectButton.top + m_nButtonHeight;

		// TODO: convert index to button ID
		bHover = (nHoverIndex == index);
		PaintButtonFace(hWndFloatBar, hDC, buttonList, rectButton, index, bActive, bHover);

		if (index  < count - 1)
		{
			// paint spacing between buttons
			PaintButtonSpacing(hDC, rectButton);

			x += BUTTON_SPACING;
		}
		x += m_nButtonWidth;
	}

	PaintBarBorder(hDC, rectBar);

	PaintEnd(hDC, rectBar);
}

// virtual 
void CThemeBasic::PaintStart(HDC hDC, RECT rectBar)
{
	if (m_hDCMem)
	{
		// shouldn't get here
	}

	int nWidth = rectBar.right - rectBar.left;
	int nHeight = rectBar.bottom - rectBar.top;

	BITMAPINFO bmi;
	memset(&bmi, 0, sizeof(bmi));
	bmi.bmiHeader.biSize = sizeof(bmi.bmiHeader);
	bmi.bmiHeader.biWidth = nWidth;
	bmi.bmiHeader.biHeight = -nHeight;
	bmi.bmiHeader.biPlanes = 1;
	bmi.bmiHeader.biBitCount = 32;
	bmi.bmiHeader.biCompression = BI_RGB;
	bmi.bmiHeader.biSizeImage = 0;

	m_hbmBar = CreateCompatibleBitmap (hDC, nWidth, nHeight);

	m_hDCMem = CreateCompatibleDC(hDC);
	m_hbmOld = (HBITMAP)SelectObject(m_hDCMem, m_hbmBar);


	// fill background with defult colour
	HBRUSH hBrush = CreateSolidBrush(m_BgrColour); 
//	HBRUSH hBrush = CreateSolidBrush(RGB(255,0,0)); 
	FillRect(m_hDCMem, &rectBar, hBrush);
	DeleteObject(hBrush);



}

// virtual 
void CThemeBasic::PaintBarBackground(HDC hDC, RECT rectBar)
{
	HBRUSH hBrush = CreateSolidBrush(m_BgrColour);
	FillRect(m_hDCMem, &rectBar, hBrush);
}

#define TO_ARGB(rgb) (((rgb & 0xFF) << 16) | ((rgb & 0xFF0000) >> 16) | (rgb & 0xFF00) | 0xFF000000)

//#define FLIP_Y(y) (m_nHeight - (y))

//#define BM_INDEX(x, y) ((m_nHeight - (y) - 1) * m_nWidth + (x))

// virtual 
void CThemeBasic::PaintButtonFace(HWND hWndFloatBar, HDC hDC, const CButtonList& buttonList, RECT rectButton, int index, bool bActive, bool bHover)
{
	int nButtonWidth = rectButton.right - rectButton.left;
	int nButtonHeight = rectButton.bottom - rectButton.top;

	//HDC hdDCButton = CreateCompatibleDC(m_hDCMem);
//	HDC hdDCButton = CreateCompatibleDC(hDC);
//	HBITMAP hbmOld = (HBITMAP)SelectObject(hdDCButton, m_hbmBackground);
//	BITMAP bm;
//	GetObject(m_hbmBackground, sizeof(BITMAP), &bm);

	//BitBlt(m_hDCMem, rectButton.left, rectButton.top, nButtonWidth, nButtonHeight, hdDCButton, 0, 0, SRCCOPY);
//	StretchBlt(m_hDCMem, rectButton.left, rectButton.top, nButtonWidth, nButtonHeight, hdDCButton, 0, 0, bm.bmWidth, bm.bmHeight, SRCCOPY);

	//if (m_hTheme)
	//{
		//HRESULT hr;

		//if (IsThemeBackgroundPartiallyTransparent(m_hTheme, WP_MINBUTTON, 0))
		//{
		//	DrawThemeParentBackground(hWndFloatBar, m_hDCMem, &rectButton);
		//}

	//	//hr = DrawThemeBackground(m_hTheme, m_hDCMem, WP_CAPTION, 0, &rectButton, NULL);
	//	//hr = DrawThemeBackground(m_hTheme, m_hDCMem, WP_CLOSEBUTTON, CBS_NORMAL, &rectButton, NULL);
	//	//hr = DrawThemeBackground(m_hTheme, m_hDCMem, WP_MINBUTTON, MINBS_NORMAL, &rectButton, NULL);
	//	hr = DrawThemeBackground(m_hTheme, m_hDCMem, WP_MINBUTTON, 0, &rectButton, NULL);

	//}

	//SelectObject(hdDCButton, hbmOld);
	//DeleteDC(hdDCButton);






	int nImageIndex = bActive ? 0 : 4;
	if (bHover)
	{
		nImageIndex += 1;
	}
	m_ImageStrip.Draw(nImageIndex, m_hDCMem, rectButton);



	// now add the glyph
	//HBITMAP hbmImage = GetImage(index);

	//BITMAP bm;
	//GetObject(hbmImage, sizeof(BITMAP), &bm);
	//int nGlyphWidth = bm.bmWidth;
	//int nGlyphHeight = bm.bmHeight;
	//int xOffset = (nButtonWidth - bm.bmWidth) / 2;
	//int yOffset = (nButtonHeight - bm.bmHeight) / 2;

	//HDC hdDCButton = CreateCompatibleDC(m_hDCMem);
	//HBITMAP hbmOld = (HBITMAP)SelectObject(hdDCButton, hbmImage);
	//BitBlt(m_hDCMem, rectButton.left + xOffset, rectButton.top + yOffset, nGlyphWidth, nGlyphHeight, hdDCButton, 0, 0, SRCCOPY);
	//SelectObject(hdDCButton, hbmOld);
	//DeleteDC(hdDCButton);


	HBITMAP hbmImage;
	HBITMAP hbmMask;

	EFloatButton button = buttonList.IndexToButton(index);
	if (GetImage(button, &hbmImage, &hbmMask))
	{
		HDC hDCMem = CreateCompatibleDC(m_hDCMem);
		HBITMAP hbmOld = (HBITMAP)SelectObject(hDCMem, hbmMask);

		BITMAP bm;
		GetObject(hbmImage, sizeof(bm), &bm);

#ifdef OLD_CODE
		int x = (rectButton.right + rectButton.left - bm.bmWidth) / 2;
		int y = (rectButton.top + rectButton.bottom - bm.bmHeight) / 2;

		BitBlt(m_hDCMem, x, y, bm.bmWidth, bm.bmHeight, hDCMem, 0, 0, SRCAND);
		SelectObject(hDCMem, hbmImage);
		BitBlt(m_hDCMem, x, y, bm.bmWidth, bm.bmHeight, hDCMem, 0, 0, SRCPAINT);
#else
		// we use the height of the button to scale the glyph
		int nTopMargin = 3;
		int nBottomMargin = 3;
		int nTargetHeight = rectButton.bottom - rectButton.top - nTopMargin - nBottomMargin; 
		int nTargetWidth = nTargetHeight * bm.bmWidth / bm.bmHeight;

		// as scaled images don't look too good, we only scale if there is a significant difference in size
		if (bm.bmHeight >= nTargetHeight - 6 && bm.bmHeight <= nTargetHeight + 2)
		{
			int x = (rectButton.right + rectButton.left - bm.bmWidth) / 2;
			int y = (rectButton.top + rectButton.bottom - bm.bmHeight) / 2;

			BitBlt(m_hDCMem, x, y, bm.bmWidth, bm.bmHeight, hDCMem, 0, 0, SRCAND);
			SelectObject(hDCMem, hbmImage);
			BitBlt(m_hDCMem, x, y, bm.bmWidth, bm.bmHeight, hDCMem, 0, 0, SRCPAINT);
		}
		else
		{
			int x = (rectButton.right + rectButton.left - nTargetWidth) / 2;
			int y = (rectButton.top + rectButton.bottom - nTargetHeight) / 2; // should be rectButton.top + nTopMargin

			StretchBlt(m_hDCMem, x, y, 	nTargetWidth, nTargetHeight,
				hDCMem, 0, 0, bm.bmWidth, bm.bmHeight,
				SRCAND);
			SelectObject(hDCMem, hbmImage);
			StretchBlt(m_hDCMem, x, y, 	nTargetWidth, nTargetHeight,
				hDCMem, 0, 0, bm.bmWidth, bm.bmHeight,
				SRCPAINT);
			}
#endif

		SelectObject(hDCMem, hbmOld);
		DeleteDC(hDCMem);
	}


}

// called to add spacing after the button
// virtual 
void CThemeBasic::PaintButtonSpacing(HDC hDC, RECT rectButton)
{
	// no spacing between buttons
}

// virtual 
void CThemeBasic::PaintBarBorder(HDC hDC, RECT rectBar)
{
	// no border
}

// virtual 
void CThemeBasic::PaintEnd(HDC hDC, RECT rectBar)
{
	int nWidth = rectBar.right - rectBar.left;
	int nHeight = rectBar.bottom - rectBar.top;

	BitBlt(hDC, 0, 0, nWidth, nHeight, m_hDCMem, 0, 0, SRCCOPY);

	// clean up
	SelectObject(m_hDCMem, m_hbmOld);
	DeleteDC(m_hDCMem);
}

bool CThemeBasic::GetImage(EFloatButton button, HBITMAP* pImage, HBITMAP* pMask)
{
	switch (button)
	{
	case FB_PREV:
		*pImage = m_hbmPrev;
		*pMask = m_hbmPrevMask;
		break;
	case FB_NEXT:
		*pImage = m_hbmNext;
		*pMask = m_hbmNextMask;
		break;
	case FB_SUPERSIZE:
		*pImage = m_hbmSupersize;
		*pMask = m_hbmSupersizeMask;
		break;
	default:
		return false;
	}

	return true;
}
