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
#include "WinHelper.h"


BOOL CALLBACK EnumMonitorCallback(HMONITOR hMonitor, HDC hDCMonitor, LPRECT lprcMonitor, LPARAM dwData);


CScreenMap CWinHelper::m_ScreenMap;

CWinHelper::CWinHelper(void)
{
}


CWinHelper::~CWinHelper(void)
{
}


// static
void CWinHelper::SupersizeWindow(HWND hWnd)
{
	static HWND lastSupersizeHwnd = 0;
	static RECT lastSupersizeRect;

	LONG lStyle = GetWindowLong(hWnd, GWL_STYLE);
	if (lStyle & WS_THICKFRAME)
	{
		RECT VirtualWorkingRect = GetVitrualWorkingRect();
		WINDOWPLACEMENT WindowPlacement;
		GetWindowPlacement(hWnd, &WindowPlacement);
		RECT curRect = WindowPlacement.rcNormalPosition;

		// TODO: ability to un-supersize
		//if (hWnd == lastSupersizeHwnd && curRect == lastSupersizeRect)
		if (hWnd == lastSupersizeHwnd && memcmp(&curRect, &VirtualWorkingRect, sizeof(RECT)) == 0)
		{
			// the window has already been supersized
			// so we need to return it to its previous (restored) size
			WindowPlacement.rcNormalPosition = lastSupersizeRect;
		}
		else
		{
			// supersize
			WindowPlacement.rcNormalPosition = VirtualWorkingRect;

			// and remember it, so we can undo it
			lastSupersizeHwnd = hWnd;
			lastSupersizeRect = curRect;
		}

		WindowPlacement.showCmd = SW_SHOWNORMAL;
		SetWindowPlacement(hWnd, &WindowPlacement);
	}
}

// static
void CWinHelper::MoveWindowToNext(HWND hWnd)
{
	WINDOWPLACEMENT windowPlacement;
	GetWindowPlacement(hWnd, &windowPlacement);
	RECT curRect = windowPlacement.rcNormalPosition;
	RECT newRect = m_ScreenMap.TransformToOtherScreen(curRect, 1);
	UINT oldShowCmd = windowPlacement.showCmd;

	if (oldShowCmd == SW_SHOWMINIMIZED || oldShowCmd == SW_SHOWMAXIMIZED)
	{
		// first restore it
		windowPlacement.showCmd = SW_RESTORE;
		SetWindowPlacement(hWnd, &windowPlacement);

		// now move it to the new screen
		windowPlacement.showCmd = SW_SHOW;
		windowPlacement.rcNormalPosition = newRect;
		SetWindowPlacement(hWnd, &windowPlacement);

		// now min or maximise it
		windowPlacement.showCmd = oldShowCmd;
		SetWindowPlacement(hWnd, &windowPlacement);
	}
	else
	{
		windowPlacement.rcNormalPosition = newRect;
		SetWindowPlacement(hWnd, &windowPlacement);
	}


}

// static
RECT CWinHelper::GetVitrualWorkingRect()
{
	RECT rect;

	// TODO
	rect.left = 0;
	rect.top = 0;
	rect.right = 2560;
	rect.bottom = 1024;

	return rect;
}

// static
RECT CWinHelper::TransformToOtherScreen(const RECT& srcRect, int nDelta)
{
	RECT newRect = srcRect;

	return newRect;
}
