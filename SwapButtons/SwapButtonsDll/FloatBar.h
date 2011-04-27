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
#include "ButtonList.h"

struct FloatInfo;

void FloatBarUpdate(HWND hWndFrame, HINSTANCE hInstance, struct FloatInfo* pFloatInfo);


class CTheme;

class LOCALMODE_API CFloatBar
{
private:

public:
//	static CFloatBar* CreateStdFloatBar(HWND hWndFrame, HINSTANCE hInstance);

	static void ProcInit(HINSTANCE hInstance);

private:
	static ATOM RegisterFloatBarClass(HINSTANCE hInstance);

public:
	CFloatBar(HMODULE hModule, HWND hWndFrame, DWORD dwButtons);
	virtual ~CFloatBar();

	void SetOldWndProc(WNDPROC WndProc);
	WNDPROC GetOldWndProc() const;
	void SetHWndFloatBar(HWND hWndFloatBar);
	HWND GetHWndFloatBar() const;


	void UpdateBarWindow(HWND hWndFrame, HINSTANCE hInstance);
	void CreateBarWindow(HWND hWndFrame, HINSTANCE hInstance);
	void AddStdBarButtons();
	void AdjustBarToParent();
	SIZE GetBarSize() ;//const;

	void OnCreate(HWND hWnd);
	//void OnActivate();
	void OnPaint();
	void CalcPositioning();

	void OnLButtonDown(WPARAM wParam, int x, int y);

	static CFloatBar* FloatBarInstance(HWND hWndFloatBar);
	//static bool IsPreVista();


//	void Invalidate();

//	void AddButton(CFloatButton* pFloatButton);
//	void DeleteButtons();
//	SIZE GetSize();

//	void CFloatBar::AdjustToParent();
//	CFloatButton* HitTest(int x, int y);



//	void WmPaint(HWND hWnd);
//	void WmLButtonDown(WPARAM wParam, int xPos, int yPos);



private:
//	ATOM MyRegisterClass(HINSTANCE hInstance);


private:
	HWND m_hWndFrame;
	HWND m_hWndFloatBar;
	WNDPROC m_OldWndProc;

	CButtonList m_ButtonList;

	//int m_nButtonHeight;	// height of an individual button
	//int m_nButtonWidth;		// width of an individual button (excluding any padding)
	//int m_nRightOffset;		// offset between right of FloatBar and right of frame
	//int m_nTopOffset;		// offset between top of FloatBar and top of frame

	CTheme* m_pTheme;
};

