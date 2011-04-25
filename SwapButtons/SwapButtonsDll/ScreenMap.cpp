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
#include "ScreenMap.h"

CScreenMap::CScreenMap()
{
	BuildMap();
}


CScreenMap::~CScreenMap()
{
}

BOOL CALLBACK MonitorEnumProc(
  HMONITOR hMonitor,  // handle to display monitor
  HDC hdcMonitor,     // handle to monitor DC
  LPRECT lprcMonitor, // monitor intersection rectangle
  LPARAM dwData       // data
)
{
	void* pData = (void*)dwData;
	CScreenMap* pThis = reinterpret_cast<CScreenMap*>(dwData);
	if (pThis)
	{
		pThis->AddMonitor(hMonitor, lprcMonitor);
	}
	else
	{
		//TRACE("MonitorEnumProc data is not a CScreenMap\n");
	}

	return TRUE;
}

void CScreenMap::BuildMap()
{
	EnumDisplayMonitors(NULL, NULL, ::MonitorEnumProc, reinterpret_cast<LPARAM>(this));
}

void CScreenMap::AddMonitor(HMONITOR hMonitor, LPRECT lprcMonitor)
{
	//TRACE("MONITOR: 0x%0x %d,%d %d,%d\n", hMonitor,
	//	lprcMonitor->left, lprcMonitor->top, lprcMonitor->right, lprcMonitor->bottom);
	CScreen NewScreen(hMonitor, *lprcMonitor);
	m_Screens.push_back(NewScreen);
}

RECT CScreenMap::TransformToOtherScreen(const RECT& srcRect, int deltaScreenIndex)
{
	RECT otherRect = srcRect;

	// determine which screen this rectangle is (mainly) on
	HMONITOR hMonitor = MonitorFromRect(&srcRect, MONITOR_DEFAULTTONEAREST);

	int curScreenIndex = FindHMonitor(hMonitor);
	int otherScreenIndex = DeltaScreenIndex(curScreenIndex, deltaScreenIndex);

	if (otherScreenIndex != curScreenIndex)
	{
		// keep TLHC in next screen same as current screen (relative to the working araea)
		CScreen curScreen = m_Screens[curScreenIndex];
		CScreen otherScreen = m_Screens[otherScreenIndex];
		RECT curScreenRect = curScreen.GetRect();
		RECT otherScreenRect = otherScreen.GetRect();
		int xOffset = otherScreenRect.left - curScreenRect.left;
		int yOffset = otherScreenRect.top - curScreenRect.top;
		otherRect.left += xOffset;
		otherRect.right += xOffset;
		otherRect.top += yOffset;
		otherRect.bottom += yOffset;
	}

	return otherRect;
}

int CScreenMap::DeltaScreenIndex(int screenIndex, int deltaScreenIndex)
{
	int newScreenIndex = (screenIndex + deltaScreenIndex) % m_Screens.size();
	if (newScreenIndex < 0)
	{
		newScreenIndex += m_Screens.size();
	}

	return newScreenIndex;
}

int CScreenMap::FindHMonitor(HMONITOR hMonitor)
{
	int nScreenIndex = -1;
	for (int nIndex = 0; nIndex < (int)m_Screens.size(); nIndex++)
	{
		if (m_Screens[nIndex].GetHMonitor() == hMonitor)
		{
			nScreenIndex = nIndex;
			break;
		}
	}

	return nScreenIndex;
}
