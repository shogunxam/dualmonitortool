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

//class CButton;

#define FLOAT_BUTTON_NONE			0x00
#define FLOAT_BUTTON_PREV			0x01
#define FLOAT_BUTTON_NEXT			0x02
#define FLOAT_BUTTON_SUPERSIZE	0x05

class LOCALMODE_API CButtonList
{
public:
	CButtonList(DWORD dwButtons);
	~CButtonList(void);

	void LoadBitmaps(HMODULE hModule);

	int Count() const;

	SIZE GetSize(int index);
	void Paint(int index, HDC hDC, const RECT& rect) const;
	void Click(int index, HWND hWndFrame);

//	CButton* At(int index);

private:
	HBITMAP CreateMask(HBITMAP hbmImage);
	void PaintBitmap(HBITMAP hbmImage, HBITMAP hbmMask, HDC hDC, const RECT& rect) const;

private:
	DWORD m_dwButtons;

	HBITMAP m_hbmPrev;
	HBITMAP m_hbmPrevMask;
	HBITMAP m_hbmNext;
	HBITMAP m_hbmNextMask;
	HBITMAP m_hbmSupersize;
	HBITMAP m_hbmSupersizeMask;
};

