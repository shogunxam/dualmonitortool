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
#include "Screen.h"


CScreen::CScreen(void)
	: m_hMonitor(0)
{
}

CScreen::CScreen(const CScreen& other)
: m_hMonitor(other.m_hMonitor),
  m_Rect(other.m_Rect)
{
}

CScreen::CScreen(HMONITOR hMonitor, const RECT& Rect)
: m_hMonitor(hMonitor),
  m_Rect(Rect)
{
}

CScreen::~CScreen(void)
{
}

const CScreen& CScreen::operator=(const CScreen& Other)
{
	if (this != &Other)
	{
		m_hMonitor = Other.m_hMonitor;
		m_Rect = Other.m_Rect;
	}

	return *this;
}

HMONITOR CScreen::GetHMonitor() const
{
	return m_hMonitor;
}

const RECT& CScreen::GetRect() const
{
	return m_Rect;
}
