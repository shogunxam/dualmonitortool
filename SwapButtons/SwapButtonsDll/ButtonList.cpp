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
#include "ButtonList.h"
#include "WinHelper.h"

#include "ResourceDll.h"

//extern HMODULE ghModule;

CButtonList::CButtonList(DWORD dwButtons)
	: m_dwButtons(dwButtons)
{
	Init(dwButtons);
}

CButtonList::~CButtonList(void)
{
}

void CButtonList::Init(DWORD dwButtons)
{
	m_nButtons = 0;
	int nMask = 0x0000000F;
	for (int i = 0; i < MAX_BUTTONS; i++)
	{
		EFloatButton button = (EFloatButton)(dwButtons & nMask);
		if (button != FB_NONE)
		{
			if (m_nButtons < MAX_BUTTONS)
			{
				m_Buttons[m_nButtons] = button;
				m_nButtons++;
			}
		}
		dwButtons >>= 4;
	}
}

int CButtonList::Count() const
{
	// hardcoded for now
	//return 3;
	return m_nButtons;
}

EFloatButton CButtonList::IndexToButton(int index) const
{
	// hardcoded for now
	//if (index == 0)
	//{
	//	return FB_PREV;
	//}
	//else if (index == 1)
	//{
	//	return FB_NEXT;
	//}
	//else if (index == 2)
	//{
	//	return FB_SUPERSIZE;
	//}

	if (index >= 0 && index < m_nButtons)
	{
		return m_Buttons[index];
	}
	return FB_NONE;
}


void CButtonList::Click(int index, HWND hWndFrame)
{
	EFloatButton button = IndexToButton(index);
	switch (button)
	{
	case FB_SCREEN_1:
	case FB_SCREEN_2:
	case FB_SCREEN_3:
	case FB_SCREEN_4:
	case FB_SCREEN_5:
	case FB_SCREEN_6:
		// TODO:
		break;
	case FB_PREV:
		// TODO:
		CWinHelper::MoveWindowToNext(hWndFrame);
		break;
	case FB_NEXT:
		CWinHelper::MoveWindowToNext(hWndFrame);
		break;
	case FB_SUPERSIZE:
		CWinHelper::SupersizeWindow(hWndFrame);
		break;

	default:
		// invalid button?
		break;
	}
}
