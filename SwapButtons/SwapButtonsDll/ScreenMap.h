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

#include "Screen.h"

class CScreenMap
{
public:
	CScreenMap();
	virtual ~CScreenMap();

	void AddMonitor(HMONITOR hMonitor, LPRECT lprcMonitor);

	RECT TransformToOtherScreen(const RECT& srcRect, int nDelta);


protected:
	void BuildMap();
	const CScreen& GetNextScreen(HMONITOR hMonitor);
	int DeltaScreenIndex(int screenIndex, int deltaScreenIndex);

	int FindHMonitor(HMONITOR hMonitor);
//	void TracePlacementInfo(CWnd* pWnd, const WINDOWPLACEMENT* pPlacement);

private:
	std::vector<CScreen> m_Screens;

};

