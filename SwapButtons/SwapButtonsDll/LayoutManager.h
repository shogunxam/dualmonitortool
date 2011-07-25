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

#pragma once

#include "SwapButtonsDll.h"
#include "ThemeClassic.h"
#include "ThemeBasic.h"
#include "ThemeDwm.h"

class CButtonList;

struct LayoutMetrics
{
	int m_nButtonWidth;
	int m_nButtonHeight;
	int m_nLeftBorder;
	int m_nRightBorder;
	int m_nTopBorder;
	int m_nBottomBorder;
	int m_nSpacing;
};

class LOCALMODE_API CLayoutManager
{
public:
	CLayoutManager(void);
	~CLayoutManager(void);


	void ReInit(HWND hWndFrame);
	void LoadBitmaps(HMODULE hModule);

	void PrepareFloatBar(HWND hWndFloatBar);

	RECT GetBarRect(HWND hWndFrame, const CButtonList& buttonList);
	SIZE GetBarSize(const CButtonList& buttonList);

	void PaintBar(HWND hWndFloatBar, HWND hWndFrame, const CButtonList& buttonList, bool bActive, int nHoverIndex);
	//int GetButtonWidth();
	int HitToIndex(int x, int y, const CButtonList& buttonList);


private:
	SIZE CalcBarOffsets(HWND hWndFrame);
	//static bool IsPreVista();

private:
	CThemeClassic m_ThemeClassic;
	CThemeBasic m_ThemeBasic;
	CThemeDwm m_ThemeDwm;
	CTheme* m_pCurTheme;
	struct LayoutMetrics m_LayoutMetrics;
};

