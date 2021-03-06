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

class CButtonList;
enum EFloatButton;


class LOCALMODE_API CTheme
{
public:
	CTheme(void);
	virtual ~CTheme(void);

	virtual void LoadBitmaps(HMODULE hModule) = 0;
	virtual HBITMAP GetImage(EFloatButton button);

	virtual bool IsAvailable() = 0;
	virtual bool IsInUse(HWND hWndFrame) = 0;

	virtual bool ReInit(struct LayoutMetrics* pLayoutMetrics, HWND hWndFrame) = 0;
	virtual SIZE CalcBarOffsets(HWND hWndFrame);
	virtual void PrepareFloatBar(HWND hWndFloatBar);
	virtual void PaintBar(HWND hWndFloatBar, HWND hWndFrame, HDC hDC, const CButtonList& buttonList, RECT rectBar, bool bActive, int nHoverIndex) = 0;

	//virtual void PaintStart(HDC hDC, RECT rectBar);
	//virtual void PaintBarBackground(HDC hDC, RECT rectBar);
	//virtual void PaintButtonFace(HDC hDC, RECT rectButton, int index) = 0;
	//virtual void PaintButtonSpacing(HDC hDC, RECT rectButton);
	//virtual void PaintBarBorder(HDC hDC, RECT rectBar);
	//virtual void PaintEnd(HDC hDC, RECT rectBar);

protected:
	static COLORREF ARGBToColorref(DWORD ARGB);
	static DWORD ARGBBlend(DWORD ARGB);
	static HBITMAP CreateMask(HBITMAP hbmImage);
	static POINT GetStdButtonsTLHC(HWND hWndFrame);
	static bool IsPreVista();

private:
	static POINT GetStdButtonsTLHCPreVista(HWND hWndFrame);
	static POINT GetStdButtonsTLHCVista(HWND hWndFrame);

protected:
	HBITMAP m_hbmPrev;
	HBITMAP m_hbmNext;
	HBITMAP m_hbmSupersize;

};

