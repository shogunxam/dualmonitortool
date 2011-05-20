#include "StdAfx.h"
#include "Theme.h"
#include "ButtonList.h"


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
HBITMAP CTheme::GetImage(EFloatButton button)
{
	switch (button)
	{
	case FB_PREV:
		return m_hbmPrev;
	case FB_NEXT:
		return m_hbmNext;
	case FB_SUPERSIZE:
		return m_hbmSupersize;
	default:
		return NULL;
	}
}

// virtual 
void CTheme::PrepareFloatBar(HWND hWndFloatBar)
{
}

/*
 * Calculates the offset in pixels from the frames TRHC and the bars TRHC
 * both values will be positive (indicating moving left and down)
 */
// virtual 
SIZE CTheme::CalcBarOffsets(HWND hWndFrame)
{
	RECT rectFrame;
	GetWindowRect(hWndFrame, &rectFrame);

	POINT ptTLHC = GetStdButtonsTLHC(hWndFrame);

	int nSpacing = 2;	// spacing between FloatBar and the stabdard buttons
	SIZE offsets;
	offsets.cx = rectFrame.right - ptTLHC.x + nSpacing;
	offsets.cy = ptTLHC.y - rectFrame.top;

	return offsets;
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

// protected static
POINT CTheme::GetStdButtonsTLHC(HWND hWndFrame)
{
	if (IsPreVista())
	{
		return GetStdButtonsTLHCPreVista(hWndFrame);
	}
	else
	{
		return GetStdButtonsTLHCVista(hWndFrame);
	}
}

// private static
POINT CTheme::GetStdButtonsTLHCPreVista(HWND hWndFrame)
{
	POINT ptTLHC;
	int nExistingButtonsWidth = 0;
	DWORD dwStyle = GetWindowLong(hWndFrame, GWL_STYLE);
	DWORD dwExStyle = GetWindowLong(hWndFrame, GWL_EXSTYLE);
	RECT rectFrame;
	GetWindowRect(hWndFrame, &rectFrame);
	ptTLHC.x = rectFrame.right;
	ptTLHC.y = rectFrame.top;

	int nStdButtonSize = 0;
	if (dwExStyle & WS_EX_TOOLWINDOW)
	{
		nStdButtonSize = GetSystemMetrics(SM_CYSMSIZE);
	}
	else
	{
		nStdButtonSize = GetSystemMetrics(SM_CYSIZE);
	}
	int nStdSpacing = 2; // x pixels between std buttons

	// no support for WM_GETTITLRBARINFOEX pre Vista
	// so do it the hard way

	//adjust for the border
	if (dwStyle & WS_THICKFRAME)
	{
		ptTLHC.x -= GetSystemMetrics(SM_CXSIZEFRAME);
		ptTLHC.y += GetSystemMetrics(SM_CYSIZEFRAME);
	}
	else
	{
		ptTLHC.x -= GetSystemMetrics(SM_CXFIXEDFRAME);
		ptTLHC.y += GetSystemMetrics(SM_CYFIXEDFRAME);
	}

	// TODO
	// hack: move 2 pixels down to center 
	ptTLHC.y += 2;

	// adjust for any titlebar 
	TITLEBARINFO titleBarInfo;
	titleBarInfo.cbSize = sizeof(titleBarInfo);
	GetTitleBarInfo(hWndFrame, &titleBarInfo);

	// TODO: are there defines for the child indexes?
#define IS_TITLE_BUTTON_VISIBLE(dw) (dw & (STATE_SYSTEM_INVISIBLE | STATE_SYSTEM_OFFSCREEN | STATE_SYSTEM_UNAVAILABLE)) == 0
	if (IS_TITLE_BUTTON_VISIBLE(titleBarInfo.rgstate[5]))
	{
		ptTLHC.x -= (nStdButtonSize + nStdSpacing);
	}
#undef IS_TITLE_BUTTON_VISIBLE

	// Alt: could use titleBarInfo for these as well
	if (dwStyle & (WS_MINIMIZEBOX | WS_MAXIMIZEBOX))
	{
		ptTLHC.x -= (nStdButtonSize + nStdSpacing) * 2;
	}
	else if (dwExStyle & WS_EX_CONTEXTHELP)
	{
		ptTLHC.x -= (nStdButtonSize + nStdSpacing);
	}

	return ptTLHC;
}

// private static
POINT CTheme::GetStdButtonsTLHCVista(HWND hWndFrame)
{
	POINT ptTLHC;
	//int nExistingButtonsWidth = 0;
	//DWORD dwStyle = GetWindowLong(hWndFrame, GWL_STYLE);
	//DWORD dwExStyle = GetWindowLong(hWndFrame, GWL_EXSTYLE);
	RECT rectFrame;
	GetWindowRect(hWndFrame, &rectFrame);
	ptTLHC.x = rectFrame.right;
	ptTLHC.y = rectFrame.top;

	// NOTE: DWMWA_CAPTION_BUTTON_BOUNDS may also be of use?

	TITLEBARINFOEX titleBarInfoEx;
	titleBarInfoEx.cbSize = sizeof(titleBarInfoEx);
	SendMessage(hWndFrame, WM_GETTITLEBARINFOEX, 0, (LPARAM)&titleBarInfoEx);

	// iterate over all of the children on the titlebar
	// finding the one with the minimum x co-ord
	// TODO: are there defines for the child indexes?
	for (int nTitleBarChild = 2; nTitleBarChild <= 5; nTitleBarChild++)
	{
		if ((titleBarInfoEx.rgstate[nTitleBarChild] & (STATE_SYSTEM_INVISIBLE | STATE_SYSTEM_OFFSCREEN | STATE_SYSTEM_UNAVAILABLE)) == 0)
		{
			// button should be visible
			ptTLHC.y = titleBarInfoEx.rgrect[nTitleBarChild].top;
			if (titleBarInfoEx.rgrect[nTitleBarChild].left < ptTLHC.x)
			{
				ptTLHC.x = titleBarInfoEx.rgrect[nTitleBarChild].left;
				// should be able to break here as indexes are in button order (left to right)
			}
		}
	}

	return ptTLHC;
}


// private static
bool CTheme::IsPreVista()
{
	return false;
}
