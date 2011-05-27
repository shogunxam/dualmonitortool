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

//#define FLOAT_BUTTON_NONE			0x00
//#define FLOAT_BUTTON_1				0x01
//#define FLOAT_BUTTON_2				0x02
//#define FLOAT_BUTTON_3				0x03
//#define FLOAT_BUTTON_4				0x04
//#define FLOAT_BUTTON_5				0x05
//#define FLOAT_BUTTON_6				0x06
//#define FLOAT_BUTTON_PREV			0x0A
//#define FLOAT_BUTTON_NEXT			0x0B
//#define FLOAT_BUTTON_SUPERSIZE		0x0C

enum EFloatButton
{
	FB_NONE     = 0x0,
	FB_SCREEN_1 = 0x1,
	FB_SCREEN_2 = 0x2,
	FB_SCREEN_3 = 0x3,
	FB_SCREEN_4 = 0x4,
	FB_SCREEN_5 = 0x5,
	FB_SCREEN_6 = 0x6,
	FB_PREV     = 0xA,
	FB_NEXT     = 0xB,
	FB_SUPERSIZE = 0xC,
};

// max number of buttons we can display
#define MAX_BUTTONS	8	

class LOCALMODE_API CButtonList
{
public:
	CButtonList(DWORD dwButtons);
	~CButtonList(void);

	void ChangeButtons(DWORD dwButtons);


	int Count() const;
	EFloatButton IndexToButton(int index) const;


	//SIZE GetSize(int index);
	//bool GetGlyph(int index, HBITMAP* phbmImage, HBITMAP* phbmMask) const;
	//void Paint(int index, HDC hDC, const RECT& rect) const;
	void Click(int index, HWND hWndFrame);

//	CButton* At(int index);

private:
	void Init();

	//HBITMAP CreateMask(HBITMAP hbmImage);
	//void PaintBitmap(HBITMAP hbmImage, HBITMAP hbmMask, HDC hDC, const RECT& rect) const;

private:
	DWORD m_dwButtons;

	EFloatButton m_Buttons[MAX_BUTTONS];
	int m_nButtons;	// count of buttons in above

	//HBITMAP m_hbmPrev;
	//HBITMAP m_hbmPrevMask;
	//HBITMAP m_hbmNext;
	//HBITMAP m_hbmNextMask;
	//HBITMAP m_hbmSupersize;
	//HBITMAP m_hbmSupersizeMask;
};

