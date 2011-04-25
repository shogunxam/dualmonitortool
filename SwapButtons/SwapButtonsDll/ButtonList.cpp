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
	: m_dwButtons(dwButtons),
	  m_hbmPrev(NULL),
	  m_hbmPrevMask(NULL),
	  m_hbmNext(NULL),
	  m_hbmNextMask(NULL),
	  m_hbmSupersize(NULL),
	  m_hbmSupersizeMask(NULL)
{
}

CButtonList::~CButtonList(void)
{
	if (m_hbmSupersizeMask)
	{
		DeleteObject(m_hbmSupersizeMask);
	}
	if (m_hbmSupersize)
	{
		DeleteObject(m_hbmSupersize);
	}
	if (m_hbmNextMask)
	{
		DeleteObject(m_hbmNextMask);
	}
	if (m_hbmNext)
	{
		DeleteObject(m_hbmNext);
	}
	if (m_hbmPrevMask)
	{
		DeleteObject(m_hbmPrevMask);
	}
	if (m_hbmPrev)
	{
		DeleteObject(m_hbmPrev);
	}
}

void CButtonList::LoadBitmaps(HMODULE hModule)
{
	m_hbmPrev = LoadBitmap(hModule, MAKEINTRESOURCE(IDB_PREV));
	m_hbmPrevMask = CreateMask(m_hbmPrev);
	m_hbmNext = LoadBitmap(hModule, MAKEINTRESOURCE(IDB_NEXT));
	m_hbmNextMask = CreateMask(m_hbmNext);
	m_hbmSupersize = LoadBitmap(hModule, MAKEINTRESOURCE(IDB_SUPERSIZE));
	m_hbmSupersizeMask = CreateMask(m_hbmSupersize);
}

int CButtonList::Count() const
{
	return 3;
}

SIZE CButtonList::GetSize(int index)
{
	SIZE size;
	size.cx = 20;
	size.cy = 20;
	return size;
}

HBITMAP CButtonList::CreateMask(HBITMAP hbmImage)
{
	// see http://www.winprog.org/tutorial/transparency.html

	HBITMAP hbmMask;
	HDC hdcMemImage;
	HDC hdcMemMask;
	BITMAP bm;

	GetObject(hbmImage, sizeof(BITMAP), &bm);

	// mask is same size as image but only 1 bit/pixel
	hbmMask = CreateBitmap(bm.bmWidth, bm.bmHeight, 1, 1, NULL);

	hdcMemImage = CreateCompatibleDC(0);
	hdcMemMask = CreateCompatibleDC(0);

	SelectObject(hdcMemImage, hbmImage);
	SelectObject(hdcMemMask, hbmMask);

	// create the mask
	SetBkColor(hdcMemImage, RGB(0, 0, 0));
	BitBlt(hdcMemMask, 0, 0, bm.bmWidth, bm.bmHeight, hdcMemImage, 0, 0, SRCCOPY);

	DeleteDC(hdcMemMask);
	DeleteDC(hdcMemImage);

	return hbmMask;
}

void CButtonList::Paint(int index, HDC hDC, const RECT& rect) const
{
	//HBRUSH hBrush = CreateSolidBrush(RGB(255, 0, 0));
	//FillRect(hDC, &rect, hBrush);

	//if (index == 0)
	//{
	//	TextOut(hDC, rect.left, rect.top, L"N", 1);
	//}
	//else
	//{
	//	TextOut(hDC, rect.left, rect.top, L"S", 1);
	//}
	if (index == 0)
	{
		PaintBitmap(m_hbmPrev, m_hbmPrevMask, hDC, rect);
	}
	else if (index == 1)
	{
		PaintBitmap(m_hbmNext, m_hbmNextMask, hDC, rect);
	}
	else
	{
		PaintBitmap(m_hbmSupersize, m_hbmSupersizeMask, hDC, rect);
	}
}

void CButtonList::PaintBitmap(HBITMAP hbmImage, HBITMAP hbmMask, HDC hDC, const RECT& rect) const
{
	HDC hDCMem = CreateCompatibleDC(hDC);
	HBITMAP hbmOld = (HBITMAP)SelectObject(hDCMem, hbmMask);

	BITMAP bm;
	GetObject(hbmImage, sizeof(bm), &bm);

	int x = (rect.right + rect.left - bm.bmWidth) / 2;
	int y = (rect.top + rect.bottom - bm.bmHeight) / 2;

	BitBlt(hDC, x, y, bm.bmWidth, bm.bmHeight, hDCMem, 0, 0, SRCAND);
	SelectObject(hDCMem, hbmImage);
	BitBlt(hDC, x, y, bm.bmWidth, bm.bmHeight, hDCMem, 0, 0, SRCPAINT);

	SelectObject(hDCMem, hbmOld);
	DeleteDC(hDCMem);
}


void CButtonList::Click(int index, HWND hWndFrame)
{
	if (index == 0)
	{
		CWinHelper::MoveWindowToNext(hWndFrame);
	}
	else if (index == 1)
	{
		CWinHelper::MoveWindowToNext(hWndFrame);
	}
	else
	{
		CWinHelper::SupersizeWindow(hWndFrame);
	}
}

//CButton* CButtonList::At(int index)
//{
//	CButton* pRet = NULL;
//
//	return pRet;
//}
