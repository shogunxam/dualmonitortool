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

#include <Uxtheme.h>

class LOCALMODE_API CImageStrip
{
public:
	CImageStrip();
	~CImageStrip(void);

	void SetImageStrip(HBITMAP hbmStrip, int nImages, const MARGINS& Margins, bool bVerticalLayout);
	void Draw(int nImage, HDC hDC, const RECT& DestRect);

private:
	HBITMAP m_hbmStrip;
	int m_nImages;
	MARGINS m_Margins;
	bool m_bVerticalLayout;

	// size of each individual image in the image strip
	int m_nSrcWidth;
	int m_nSrcHeight;
};

