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
#include "ImageStrip.h"

CImageStrip::CImageStrip()
	: m_hbmStrip(NULL),
	  m_nImages(0),
	  m_bVerticalLayout(false),
	  m_nSrcWidth(0),
	  m_nSrcHeight(0)
{
}


CImageStrip::~CImageStrip(void)
{
	if (m_hbmStrip)
	{
		DeleteObject(m_hbmStrip);
	}
}

void CImageStrip::SetImageStrip(HBITMAP hbmStrip, int nImages, const MARGINS& Margins, bool bVerticalLayout)
{
	if (m_hbmStrip)
	{
		DeleteObject(m_hbmStrip);
		m_hbmStrip = NULL;
	}

	m_hbmStrip = hbmStrip;
	m_nImages = nImages;
	m_Margins = Margins;
	m_bVerticalLayout = bVerticalLayout;

	BITMAP bm;
	GetObject(hbmStrip, sizeof(BITMAP), &bm);

	if (bVerticalLayout)
	{
		m_nSrcWidth = bm.bmWidth;
		m_nSrcHeight = bm.bmHeight / nImages;

		// assert (bm.bmHeight % nImages) == 0
	}
	else
	{
		m_nSrcWidth = bm.bmWidth / nImages;
		// assert (bm.bmWidth % nImages) == 0
		m_nSrcHeight = bm.bmHeight;
	}
}

void CImageStrip::Draw(int nImage, HDC hDC, const RECT& DestRect)
{


	if (nImage >= 0 && nImage < m_nImages)
	{
		HDC hDcMem = CreateCompatibleDC(hDC);
		HBITMAP hbmOld = (HBITMAP)SelectObject(hDcMem, m_hbmStrip);



		int xSrc = 0;
		int ySrc = 0;
		if (m_bVerticalLayout)
		{
			ySrc = nImage * m_nSrcHeight;
		}
		else
		{
			xSrc = nImage * m_nSrcWidth;
		}

		// destination size
		int nDestWidth = DestRect.right - DestRect.left;
		int nDestHeight = DestRect.bottom - DestRect.top;

		// total size of horizontal and vertical margins
		int nXMargins = m_Margins.cxLeftWidth + m_Margins.cxRightWidth;
		int nYMargins = m_Margins.cyTopHeight + m_Margins.cyBottomHeight;
		// split the src image into a 3*3 grid and stretch/blt each cell to fit destination 

		//StretchBlt(hDC, DestRect.left, DestRect.top, nDestWidth, nDestHeight, hDcMem, 0, 0, m_nSrcWidth, m_nSrcHeight, SRCCOPY);

		// Top left corner
		BitBlt(hDC, DestRect.left, DestRect.top,
			m_Margins.cxLeftWidth, m_Margins.cyTopHeight,
			hDcMem, xSrc, ySrc,
			SRCCOPY);

		// Top middle - stretch x
		StretchBlt(hDC, DestRect.left + m_Margins.cxLeftWidth, DestRect.top,
			nDestWidth - nXMargins, m_Margins.cyTopHeight,
			hDcMem, xSrc + m_Margins.cxLeftWidth, ySrc,
			m_nSrcWidth - nXMargins, m_Margins.cyTopHeight,
			SRCCOPY);

		// Top right corner
		BitBlt(hDC, DestRect.right - m_Margins.cxRightWidth, DestRect.top,
			m_Margins.cxRightWidth, m_Margins.cyTopHeight,
			hDcMem, xSrc + m_nSrcWidth - m_Margins.cxRightWidth, ySrc,
			SRCCOPY);


		// Middle left - stretch y
		StretchBlt(hDC, DestRect.left, DestRect.top + m_Margins.cyTopHeight,
			m_Margins.cxLeftWidth, nDestHeight - nYMargins,
			hDcMem, xSrc, ySrc + m_Margins.cyTopHeight,
			m_Margins.cxLeftWidth, m_nSrcHeight - nYMargins,
			SRCCOPY);

		// Middle cell - stretch x & y
		StretchBlt(hDC, DestRect.left + m_Margins.cxLeftWidth, DestRect.top + m_Margins.cyTopHeight,
			nDestWidth - nXMargins,nDestHeight - nYMargins,
			hDcMem, xSrc + m_Margins.cxLeftWidth, ySrc + m_Margins.cyTopHeight,
			m_nSrcWidth - nXMargins, m_nSrcHeight - nYMargins,
			SRCCOPY);

		// Middle right - stretch y
		StretchBlt(hDC, DestRect.right - m_Margins.cxRightWidth, DestRect.top + m_Margins.cyTopHeight,
			m_Margins.cxRightWidth, nDestHeight - nYMargins,
			hDcMem, xSrc + m_nSrcWidth - m_Margins.cxRightWidth, ySrc + m_Margins.cyTopHeight,
			m_Margins.cxRightWidth, m_nSrcHeight - nYMargins,
			SRCCOPY);


		// bottom left
		BitBlt(hDC, DestRect.left, DestRect.bottom - m_Margins.cyBottomHeight,
			m_Margins.cxLeftWidth, m_Margins.cyBottomHeight,
			hDcMem, xSrc, ySrc + m_nSrcHeight - m_Margins.cyBottomHeight,
			SRCCOPY);

		// bottom middle - stretch x
		StretchBlt(hDC, DestRect.left + m_Margins.cxLeftWidth, DestRect.bottom - m_Margins.cyBottomHeight,
			nDestWidth - nXMargins, m_Margins.cyBottomHeight,
			hDcMem, xSrc + m_Margins.cxLeftWidth, ySrc + m_nSrcHeight - m_Margins.cyBottomHeight,
			m_nSrcWidth - nXMargins, m_Margins.cyBottomHeight,
			SRCCOPY);

		// bottom right
		BitBlt(hDC, DestRect.right - m_Margins.cxRightWidth, DestRect.bottom - m_Margins.cyBottomHeight,
			m_Margins.cxRightWidth, m_Margins.cyBottomHeight,
			hDcMem, xSrc + m_nSrcWidth - m_Margins.cxRightWidth, ySrc + m_nSrcHeight - m_Margins.cyBottomHeight,
			SRCCOPY);

		SelectObject(hDcMem, hbmOld);
		DeleteDC(hDcMem);
	}
}
