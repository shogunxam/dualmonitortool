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

// The idea of this class was to build the bar buttons in here with alpha channel values
// but not for the alpha values to have an impact while being drawn into here,
// but only when they get drawn into the window's DC.
//
// but seeing as I can't get transparency working anyway, It may be simpler (and faster) 
// to create a DC and draw directly into that, but we'll see.
//
class LOCALMODE_API CBitmapBuffer
{
public:
	CBitmapBuffer();
	CBitmapBuffer(int nWidth, int nHeight);
	~CBitmapBuffer(void);

	void Init(int nWidth, int nHeight);

	int GetWidth() const;
	int GetHeight() const;
	const DWORD* GetBits() const;

	void Fill(int x1, int y1, int x2, int y2, DWORD dwColour);
	void DrawHLine(int x1, int x2, int y, DWORD dwColour);
	void DrawVLine(int x, int y1, int y2, DWORD dwColour);
	void Set(int x, int y, DWORD dwColour);
	void CenterTransparentBitmap(HBITMAP hbm, int left, int top, int right, int bottom, DWORD dwTransparent);


private:
	void InlineSet(int x, int y, DWORD dwColour);
	bool IsValidX(int x);
	bool IsValidY(int y);

private:
	int m_nWidth;
	int m_nHeight;
	DWORD* m_pdwBits;
};

