#include "StdAfx.h"
#include "Theme.h"


CTheme::CTheme(void)
:     m_hbmPrev(NULL),
	  m_hbmNext(NULL),
	  m_hbmSupersize(NULL)
{
}


CTheme::~CTheme(void)
{
	if (m_hbmSupersize)
	{
		DeleteObject(m_hbmSupersize);
	}
	if (m_hbmNext)
	{
		DeleteObject(m_hbmNext);
	}
	if (m_hbmPrev)
	{
		DeleteObject(m_hbmPrev);
	}
}

// virtual (but probably will not need to be)
HBITMAP CTheme::GetImage(int index)
{
	switch (index)
	{
	case 0:
		return m_hbmPrev;
	case 1:
		return m_hbmNext;
	case 2:
		return m_hbmSupersize;
	default:
		return NULL;
	}
}

// virtual 
void CTheme::PrepareFloatBar(HWND hWndFloatBar)
{
}

//// virtual 
//void CTheme::PaintStart(HDC hDC, RECT rectBar)
//{
//	// base implementation does nothing
//}
//
//// virtual 
//void CTheme::PaintBarBackground(HDC hDC, RECT rectBar)
//{
//	// base implementation does nothing
//}
//
//// virtual 
//void CTheme::PaintButtonSpacing(HDC hDC, RECT rectButton)
//{
//	// base implementation does nothing
//}
//
//// virtual 
//void CTheme::PaintBarBorder(HDC hDC, RECT rectBar)
//{
//	// base implementation does nothing
//}
//
//// virtual 
//void CTheme::PaintEnd(HDC hDC, RECT rectBar)
//{
//	// base implementation does nothing
//}

///////////////////////// static helper functions /////////////////////////

// protected static
COLORREF CTheme::ARGBToColorref(DWORD ARGB)
{
	DWORD red =   (ARGB & 0x00FF0000) >> 16;
	DWORD green = (ARGB & 0x0000ff00) >> 8;
	DWORD blue =  (ARGB & 0x000000FF);
	DWORD alpha = (ARGB & 0xFF000000) >> 24;

	// assume the colour underneath is going to be white
	DWORD blend = 0xFF;	// for red, green and blue
	// and adust the colours according to the alpha

#define BLEND_COLOUR(in, alpha, blend) ((in * alpha + blend * (0xFF - alpha)) / 0xFF)
	red = BLEND_COLOUR(red, alpha, blend);
	green = BLEND_COLOUR(green, alpha, blend);
	blue = BLEND_COLOUR(blue, alpha, blend);
#undef BLEND_COLOUR

	return RGB(red, green, blue);
}

// protected static
DWORD CTheme::ARGBBlend(DWORD ARGB)
{
	DWORD red =   (ARGB & 0x00FF0000) >> 16;
	DWORD green = (ARGB & 0x0000ff00) >> 8;
	DWORD blue =  (ARGB & 0x000000FF);
	DWORD alpha = (ARGB & 0xFF000000) >> 24;

	// assume the colour underneath is going to be white
	DWORD blend = 0xFF;	// for red, green and blue
	// and adust the colours according to the alpha

#define BLEND_COLOUR(in, alpha, blend) ((in * alpha + blend * (0xFF - alpha)) / 0xFF)
	red = BLEND_COLOUR(red, alpha, blend);
	green = BLEND_COLOUR(green, alpha, blend);
	blue = BLEND_COLOUR(blue, alpha, blend);
#undef BLEND_COLOUR

	return 0xFF000000 | (red << 16) | (green << 8) | blue;
}


// protected static
HBITMAP CTheme::CreateMask(HBITMAP hbmImage)
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


