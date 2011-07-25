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
#include "Theme.h"
#include "BitmapBuffer.h"

struct LayoutMetrics;
enum EFloatButton;

class LOCALMODE_API CThemeClassic : public CTheme
{
public:
	CThemeClassic(void);
	virtual ~CThemeClassic(void);

	virtual void LoadBitmaps(HMODULE hModule);
	virtual bool IsAvailable();
	virtual bool IsInUse(HWND hWndFrame);

	virtual bool ReInit(struct LayoutMetrics* pLayoutMetrics, HWND hWndFrame);
	//virtual SIZE CalcBarOffsets(HWND hWndFrame);
	virtual void PrepareFloatBar(HWND hWndFloatBar);
	virtual void PaintBar(HWND hWndFloatBar, HWND hWndFrame, HDC hDC, const CButtonList& buttonList, RECT rectBar, bool bActive, int nHoverIndex);

	//virtual void PaintStart(HDC hDC, RECT rectBar);
	//virtual void PaintBarBackground(HDC hDC, RECT rectBar);
	//virtual void PaintButtonFace(HDC hDC, RECT rectButton, int index);
	//virtual void PaintButtonSpacing(HDC hDC, RECT rectButton);
	//virtual void PaintBarBorder(HDC hDC, RECT rectBar);
	//virtual void PaintEnd(HDC hDC, RECT rectBar);


private:
	void PaintBarBackground(HDC hDC, RECT rectBar);
	void PaintButtonFace(HDC hDC, RECT rectButton, EFloatButton button);
	void PaintButtonSpacing(HDC hDC, RECT rectButton);
	void PaintBarBorder(HDC hDC, RECT rectBar);
	void PaintEnd(HDC hDC, RECT rectBar);

private:
	CBitmapBuffer m_BitmapBuffer;

	int m_nButtonWidth;
	int m_nButtonHeight;


private:
	static const int LEFT_BORDER;
	static const int RIGHT_BORDER;
	static const int TOP_BORDER;
	static const int BOTTOM_BORDER;
	static const int BUTTON_SPACING;

};
