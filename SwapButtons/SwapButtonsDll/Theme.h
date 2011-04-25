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

class CButtonList;

class CTheme
{
public:
	CTheme();
	//CTheme(HWND hWndFrame, DWORD dwButtons);
	virtual ~CTheme(void);

	void CTheme::Init(HWND hWndFloatBar);
	void CalcPositioning(HWND hWndFrame);
	RECT GetBarRect(HWND hWndFrame, const CButtonList& buttonList);
	SIZE GetBarSize(const CButtonList& buttonList);

	void PaintBar(HWND hWndFloatBar, const CButtonList& buttonList);
	//int GetButonSpacing();
	int GetButtonWidth();
	//DWORD HitTest(int x, int y);

protected:
	static bool IsPreVista();

protected:
	int m_nButtonHeight;
	int m_nButtonWidth;
	int m_nRightOffset;
	int m_nTopOffset;

};

