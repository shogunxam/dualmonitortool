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

#include "ScreenMap.h"

class CWinHelper
{
public:
	CWinHelper(void);
	~CWinHelper(void);

	static void SupersizeWindow(HWND hWnd);
	static void MoveWindowToNext(HWND hWnd);


private:
	static RECT GetVitrualWorkingRect();


	static RECT TransformToOtherScreen(const RECT& srcRect, int nDelta);

private:
	static 
		CScreenMap m_ScreenMap;
};

