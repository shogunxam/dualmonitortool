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
#include "LayoutManager.h"
#include "ButtonList.h"

#define IS_TITLE_BUTTON_VISIBLE(dw) (dw & (STATE_SYSTEM_INVISIBLE | STATE_SYSTEM_OFFSCREEN | STATE_SYSTEM_UNAVAILABLE)) == 0

CLayoutManager::CLayoutManager(void)
	: m_pCurTheme(NULL)
{

}


CLayoutManager::~CLayoutManager(void)
{
}

void CLayoutManager::ReInit(HWND hWndFrame)
{
	if (m_ThemeDwm.IsAvailable() && m_ThemeDwm.IsInUse(hWndFrame))
	{
		// use the DWM
		m_pCurTheme = &m_ThemeDwm;
		m_pCurTheme->ReInit(&m_LayoutMetrics, hWndFrame);
	}
	else if (m_ThemeBasic.IsAvailable() && m_ThemeBasic.IsInUse(hWndFrame))
	{
		// use the basic theme
		m_pCurTheme = &m_ThemeBasic;
		m_pCurTheme->ReInit(&m_LayoutMetrics, hWndFrame);
	}
	else
	{
		// use classic theme
		m_pCurTheme = &m_ThemeClassic;
		m_pCurTheme->ReInit(&m_LayoutMetrics, hWndFrame);
	}
}

void CLayoutManager::LoadBitmaps(HMODULE hModule)
{
	m_ThemeClassic.LoadBitmaps(hModule);
	m_ThemeBasic.LoadBitmaps(hModule);
	m_ThemeDwm.LoadBitmaps(hModule);
}

void CLayoutManager::PrepareFloatBar(HWND hWndFloatBar)
{
	if (m_pCurTheme)
	{
		m_pCurTheme->PrepareFloatBar(hWndFloatBar);
	}
}

RECT CLayoutManager::GetBarRect(HWND hWndFrame, const CButtonList& buttonList)
{
	RECT rectFloatBar;
	RECT rectFrame;

	// get frame rect in screen co-ords
	GetWindowRect(hWndFrame, &rectFrame);

	// get TRHC to place the bar at
	SIZE offsets = CalcBarOffsets(hWndFrame);

	SIZE size = GetBarSize(buttonList);

	rectFloatBar.right = rectFrame.right - offsets.cx;
	rectFloatBar.left = rectFloatBar.right - size.cx;
	rectFloatBar.top = rectFrame.top + offsets.cy;
	rectFloatBar.bottom = rectFloatBar.top + size.cy;

	return rectFloatBar;
}

// private
SIZE CLayoutManager::CalcBarOffsets(HWND hWndFrame)
{
	if (m_pCurTheme)
	{
		return m_pCurTheme->CalcBarOffsets(hWndFrame);
	}

	// TODO
	SIZE offsets;
	offsets.cx = 0;
	offsets.cy = 0;
	return offsets;
}

//// private static
//bool CLayoutManager::IsPreVista()
//{
//	bool ret = true;
//	//return true;	// for testing
//
//	OSVERSIONINFO VerInfo;
//
//	memset(&VerInfo, 0, sizeof(VerInfo));
//	VerInfo.dwOSVersionInfoSize = sizeof(VerInfo);
//	GetVersionEx((OSVERSIONINFO*)&VerInfo);
//
//	if (VerInfo.dwPlatformId == 1)
//	{
//		// 9X and earlier
//	}
//	else if (VerInfo.dwPlatformId == 2)
//	{
//		// NT and later
//		if (VerInfo.dwMajorVersion >= 6)	// Vista or later
//		{
//			ret = false;
//		}
//	}
//	else if (VerInfo.dwPlatformId == 3)
//	{
//		// Windows CE
//	}
//
//	return ret;
//}

SIZE CLayoutManager::GetBarSize(const CButtonList& buttonList)
{
	SIZE size = {0, 0};
	int nButtons = buttonList.Count();
	
	if (nButtons)
	{
		// first add the borders
		size.cx = m_LayoutMetrics.m_nLeftBorder + m_LayoutMetrics.m_nRightBorder;
		size.cy = m_LayoutMetrics.m_nTopBorder + m_LayoutMetrics.m_nBottomBorder;

		// always one button high
		size.cy += m_LayoutMetrics.m_nButtonHeight;

		// add the button widths
		size.cx += m_LayoutMetrics.m_nButtonWidth * nButtons;
		// add the space between buttons
		size.cx += m_LayoutMetrics.m_nSpacing * (nButtons - 1);
	}

	return size;
}

void CLayoutManager::PaintBar(HWND hWndFloatBar, HWND hWndFrame, const CButtonList& buttonList, bool bActive, int nHoverIndex)
{
	if (m_pCurTheme)
	{
		PAINTSTRUCT ps;

		HDC hDC = BeginPaint(hWndFloatBar, &ps);
		RECT rectBar;
		//RECT rectButton;
		GetClientRect(hWndFloatBar, &rectBar);

		m_pCurTheme->PaintBar(hWndFloatBar, hWndFrame, hDC, buttonList, rectBar, bActive, nHoverIndex);

		//m_pCurTheme->PaintStart(hDC, rectBar);

		//m_pCurTheme->PaintBarBackground(hDC, rectBar);

		//// the pen to draw border around buttons
		//HPEN hPen = CreatePen(PS_SOLID, 1, RGB(128, 128, 128));
		//HPEN hOldPen = (HPEN)SelectObject(hDC, hPen);


		//int x = rectBar.left + m_LayoutMetrics.m_nLeftBorder;
		//int y = rectBar.top + m_LayoutMetrics.m_nTopBorder;
		//int index;
		//int count = buttonList.Count();
		//for (index = 0; index < count; index++)
		//{

		//	rectButton.left = x;
		//	rectButton.right = rectButton.left + m_LayoutMetrics.m_nButtonWidth;
		//	rectButton.top = y;
		//	rectButton.bottom = rectButton.top + m_LayoutMetrics.m_nButtonHeight;

		//	//HBITMAP hbmImage;
		//	//HBITMAP hbmMask;
		//	//if (buttonList.GetGlyph(index, &hbmImage, &hbmMask))
		//	//{
		//	//	m_pCurTheme->PaintButtonFace(hDC, rectButton, hbmImage, hbmMask);
		//	//}

		//	// TODO: convert index to button ID
		//	m_pCurTheme->PaintButtonFace(hDC, rectButton, index);

		//	if (index  < count - 1)
		//	{
		//		// paint spacing between buttons
		//		m_pCurTheme->PaintButtonSpacing(hDC, rectButton);

		//		x += m_LayoutMetrics.m_nSpacing;
		//	}
		//	x += m_LayoutMetrics.m_nButtonWidth;
		//}

		//m_pCurTheme->PaintBarBorder(hDC, rectBar);

		//m_pCurTheme->PaintEnd(hDC, rectBar);

		EndPaint(hWndFloatBar, &ps);
	}
}

int CLayoutManager::HitToIndex(int x, int y, const CButtonList& buttonList)
{

	int right = 1;
	int index;
	int count = buttonList.Count();
	for (index = 0; index < count; index++)
	{
		if (index != 0)
		{
			// add in seperator
			right += m_LayoutMetrics.m_nSpacing;
		}
		right += m_LayoutMetrics.m_nButtonWidth;

		if (x <= right + 1)
		{
			return index;
		}
	}

	return -1;
}
