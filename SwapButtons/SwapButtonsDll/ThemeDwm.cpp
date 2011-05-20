#include "StdAfx.h"
#include <dwmapi.h>

#include "ThemeDwm.h"
#include "LayoutManager.h"
#include "ButtonList.h"

#include "ResourceDll.h"

#include <objidl.h>
#include <gdiplus.h>
using namespace Gdiplus;
#pragma comment (lib,"Gdiplus.lib")


//#define TRANSPARENT_COLOUR	(RGB(0, 255, 0))
#define TRANSPARENT_COLOUR	(RGB(1, 1, 1))
//#define TRANSPARENT_COLOUR	(RGB(0, 0, 0))


const int CThemeDwm::LEFT_BORDER = 2;
const int CThemeDwm::RIGHT_BORDER = 2;
const int CThemeDwm::TOP_BORDER = 0;
const int CThemeDwm::BOTTOM_BORDER = 2;
const int CThemeDwm::BUTTON_SPACING = 1;


   GdiplusStartupInput gdiplusStartupInput;
   ULONG_PTR           gdiplusToken;

CThemeDwm::CThemeDwm(void)
	: m_bCheckedAvailable(false),
	  m_bIsAvailable(false),
	  m_hDwmLib(NULL),
	  m_BgrColour(RGB(224, 224, 224))
{
}

// virtual
CThemeDwm::~CThemeDwm(void)
{
	if (m_hDwmLib)
	{
		FreeLibrary(m_hDwmLib);
	}
}


// virtual
void CThemeDwm::LoadBitmaps(HMODULE hModule)
{
	m_hbmPrev = LoadBitmap(hModule, MAKEINTRESOURCE(IDB_PREV));
	m_hbmNext = LoadBitmap(hModule, MAKEINTRESOURCE(IDB_NEXT));
	m_hbmSupersize = LoadBitmap(hModule, MAKEINTRESOURCE(IDB_SUPERSIZE));
}


// virtual 
bool CThemeDwm::IsAvailable()
{
	if (!m_bCheckedAvailable)
	{
		CheckIfAvailable();
	}
	return m_bIsAvailable;
}

// private
void CThemeDwm::CheckIfAvailable()
{
	m_hDwmLib = LoadLibrary(L"dwmapi.dll");
	if (m_hDwmLib)
	{
		if (GetProcAddress(m_hDwmLib, "DwmIsCompositionEnabled")
		 && GetProcAddress(m_hDwmLib, "DwmGetColorizationColor"))
		{
			m_bIsAvailable = true;
		}
	}
	m_bCheckedAvailable = true;
}

// if DWM composition is enabled, then we assume it is in use by all windows
// virtual 
bool CThemeDwm::IsInUse(HWND hWndFrame)
{
	bool bInUse = false;

	if (m_bIsAvailable)
	{
		BOOL fEnabled = FALSE;
		HRESULT hr = DwmIsCompositionEnabled(&fEnabled);
		if (SUCCEEDED(hr) && fEnabled)
		{
			bInUse = true;
		}
	}

	return bInUse;
}

// virtual 
bool CThemeDwm::ReInit(struct LayoutMetrics* pLayoutMetrics, HWND hWndFrame)
{
	// TODO: must find a better way of doing this
	int nStdButtonSize;

	//DWORD dwStyle = GetWindowLong(hWndFrame, GWL_STYLE);
	DWORD dwExStyle = GetWindowLong(hWndFrame, GWL_EXSTYLE);

	if (dwExStyle & WS_EX_TOOLWINDOW)
	{
		nStdButtonSize = GetSystemMetrics(SM_CYSMSIZE);
	}
	else
	{
		nStdButtonSize = GetSystemMetrics(SM_CYSIZE);
	}

	//TODO: this is a temprary fudge
	m_nButtonWidth = nStdButtonSize + 4;
	m_nButtonHeight = nStdButtonSize - 3;

	// TODO: hack while testing
	m_nButtonWidth = 26;
	m_nButtonHeight = 16;

	// TODO: do we need these in the metrics?
	pLayoutMetrics->m_nButtonWidth = m_nButtonWidth;
	pLayoutMetrics->m_nButtonHeight = m_nButtonHeight;

	wchar_t szMsg[256];
	wsprintf(szMsg, L"nStdButtonSize: %d Width: %d Height: %d\n", nStdButtonSize, pLayoutMetrics->m_nButtonWidth, pLayoutMetrics->m_nButtonHeight);
	OutputDebugString(szMsg);


	pLayoutMetrics->m_nLeftBorder = LEFT_BORDER;
	pLayoutMetrics->m_nRightBorder = RIGHT_BORDER;
	pLayoutMetrics->m_nTopBorder = TOP_BORDER;
	pLayoutMetrics->m_nBottomBorder = BOTTOM_BORDER;
	pLayoutMetrics->m_nSpacing = BUTTON_SPACING;

	SaveBgrColour();

	return true;
}

// private
void CThemeDwm::SaveBgrColour()
{
	if (m_bIsAvailable)
	{
		DWORD color = 0;
		BOOL opaque = FALSE;

		HRESULT hr = DwmGetColorizationColor(&color, &opaque);
		if (SUCCEEDED(hr))
		{
			m_BgrColour = ARGBToColorref(color);
		}
	}
}

// virtual
//SIZE CThemeDwm::CalcBarOffsets(HWND hWndFrame)
//{
//	int nExistingButtonsWidth = 0;
//	DWORD dwStyle = GetWindowLong(hWndFrame, GWL_STYLE);
//	DWORD dwExStyle = GetWindowLong(hWndFrame, GWL_EXSTYLE);
//	RECT rectFrame;
//	GetWindowRect(hWndFrame, &rectFrame);
//	int nRightPos = rectFrame.right;
//	int nTopPos = rectFrame.top;
//
//	int nStdButtonSize = 0;
//	if (dwExStyle & WS_EX_TOOLWINDOW)
//	{
//		nStdButtonSize = GetSystemMetrics(SM_CYSMSIZE);
//	}
//	else
//	{
//		nStdButtonSize = GetSystemMetrics(SM_CYSIZE);
//	}
//	int nStdSpacing = 2; // x pixels between std buttons
//
//	// NOTE: DWMWA_CAPTION_BUTTON_BOUNDS may also be of use?
//
//	// Vista & later only
//	TITLEBARINFOEX titleBarInfoEx;
//	titleBarInfoEx.cbSize = sizeof(titleBarInfoEx);
//	SendMessage(hWndFrame, WM_GETTITLEBARINFOEX, 0, (LPARAM)&titleBarInfoEx);
//
//	// iterate over all of the children on the titlebar
//	// finding the one with the minimum x co-ord
//	// TODO: are there defines for the child indexes?
//	for (int nTitleBarChild = 2; nTitleBarChild <= 5; nTitleBarChild++)
//	{
//		if ((titleBarInfoEx.rgstate[nTitleBarChild] & (STATE_SYSTEM_INVISIBLE | STATE_SYSTEM_OFFSCREEN | STATE_SYSTEM_UNAVAILABLE)) == 0)
//		{
//			// button should be visible
//			nTopPos = titleBarInfoEx.rgrect[nTitleBarChild].top;
//			if (titleBarInfoEx.rgrect[nTitleBarChild].left < nRightPos)
//			{
//				nRightPos = titleBarInfoEx.rgrect[nTitleBarChild].left;
//				// should be able to break here as indexes are in button order (left to right)
//			}
//		}
//	}
//
//	// TODO: temp hack - this needs to go into theme
//	//nTopPos += 1;
//	nRightPos -= nStdButtonSize / 2; // to allow spacing between the button sets
//
//	SIZE offsets;
//	offsets.cx = rectFrame.right - nRightPos;
//	offsets.cy = nTopPos - rectFrame.top;
//	return offsets;
//}

// virtual 
void CThemeDwm::PrepareFloatBar(HWND hWndFloatBar)
{
	// set the layered style
	DWORD dwExStyle = GetWindowLong(hWndFloatBar, GWL_EXSTYLE);
	dwExStyle |= WS_EX_LAYERED;
//	dwExStyle |= WS_EX_TRANSPARENT;
//	dwExStyle |= WS_EX_TRANSPARENT | WS_EX_LAYERED;
	SetWindowLong(hWndFloatBar, GWL_EXSTYLE, dwExStyle);

	// set the colour we are going to use as transparent
	SetLayeredWindowAttributes(hWndFloatBar, TRANSPARENT_COLOUR, 0, LWA_COLORKEY);
//	SetLayeredWindowAttributes(hWndFloatBar, 0, 128, LWA_ALPHA);


	//DWM_BLURBEHIND blurBehind = { 0 };
 //   
 //   blurBehind.dwFlags = DWM_BB_ENABLE | DWM_BB_TRANSITIONONMAXIMIZED;
 //   blurBehind.fEnable = TRUE;
 //   blurBehind.fTransitionOnMaximized = FALSE;

 //       blurBehind.dwFlags |= DWM_BB_BLURREGION;
 //       blurBehind.hRgnBlur = 0;
 //
 //  DwmEnableBlurBehindWindow(hWndFloatBar, &blurBehind);


	MARGINS margins = {-1, -1, -1, -1};
   // Extend the frame across the entire window.
//   HRESULT hr = DwmExtendFrameIntoClientArea(hWndFloatBar,&margins);
   
}

// virtual 
void CThemeDwm::PaintBar(HWND hWndFloatBar, HWND hWndFrame, HDC hDC, const CButtonList& buttonList, RECT rectBar, bool bActive)
{
	m_BitmapBuffer.Init(rectBar.right - rectBar.left, rectBar.bottom - rectBar.top);

	int x = rectBar.left + LEFT_BORDER;
	int y = rectBar.top + TOP_BORDER;
	int index;
	int count = buttonList.Count();
	RECT rectButton;

	for (index = 0; index < count; index++)
	{
		rectButton.left = x;
		rectButton.right = rectButton.left + m_nButtonWidth;
		rectButton.top = y;
		rectButton.bottom = rectButton.top + m_nButtonHeight;

		// convert index to button ID
		EFloatButton button = buttonList.IndexToButton(index);
		PaintButtonFace(hDC, rectButton, button);

		if (index  < count - 1)
		{
			// paint spacing between buttons
			PaintButtonSpacing(hDC, rectButton);

			x += BUTTON_SPACING;
		}
		x += m_nButtonWidth;
	}

	PaintBarBorder(hDC, rectBar);

	PaintEnd(hDC, rectBar);
}

//// virtual 
//void CThemeDwm::PaintStart(HDC hDC, RECT rectBar)
//{
//	m_BitmapBuffer.Init(rectBar.right - rectBar.left, rectBar.bottom - rectBar.top);
//}

// virtual 
void CThemeDwm::PaintBarBackground(HDC hDC, RECT rectBar)
{
}

// virtual 
void CThemeDwm::PaintButtonFace(HDC hDC, RECT rectButton, EFloatButton button)
{
	HBITMAP hbmImage = GetImage(button);

	byte alpha = 160;
	BYTE greyLevel = 252;
	DWORD dwLight = (alpha << 24) | (greyLevel << 16) | (greyLevel << 8) | greyLevel;

	// get co-ords of TLHC and BRHC - these are all inclusive values
	// i.e. (right, bottom) is really part of the button
	int left = rectButton.left;
	int top = rectButton.top;
	int right = rectButton.right - 1;
	int bottom = rectButton.bottom - 1;

	int nMid = (rectButton.top + rectButton.bottom) / 2;

	// add light border around the button
	greyLevel = 252;
	m_BitmapBuffer.DrawHLine(left, right, top, dwLight);
	m_BitmapBuffer.DrawHLine(left, right, bottom, dwLight);
	m_BitmapBuffer.DrawVLine(left, top, bottom, dwLight);
	m_BitmapBuffer.DrawVLine(right, top, bottom, dwLight);


	for (int y = top + 1; y <= bottom - 1; y++)
	{
		if (y > nMid)
		{
			greyLevel = 180 + (180 - 200) * (y - nMid) / nMid;
		}
		else
		{
			greyLevel = 234 + (211 - 234) * y / nMid;
		}

		DWORD dw = Color::MakeARGB(alpha, greyLevel, greyLevel, greyLevel);

		m_BitmapBuffer.DrawHLine(left + 1, right - 1, y, dw);
	}

	// now add the glyph
	//HBITMAP hbmOld = (HBITMAP)SelectObject(hDC, hbmImage);
	BITMAP bm;
	GetObject(hbmImage, sizeof(BITMAP), &bm);
	BITMAPINFO bi;
	memset(&bi, 0, sizeof(bi));
	bi.bmiHeader.biSize = sizeof(bi.bmiHeader);
	int ret = GetDIBits(hDC, hbmImage, 0, bm.bmHeight, NULL, &bi, DIB_RGB_COLORS);
	if (ret != 0)
	{
		DWORD* pBits = new DWORD[bm.bmWidth * bm.bmHeight];
		bi.bmiHeader.biSize = sizeof(bi.bmiHeader);
		bi.bmiHeader.biBitCount = 32;
		bi.bmiHeader.biCompression  = BI_RGB;
		int ret = GetDIBits(hDC, hbmImage, 0, bm.bmHeight, pBits, &bi, DIB_RGB_COLORS);
		if (ret != 0)
		{
			// add glyph to our bitmap
			int xOffset = rectButton.left + (rectButton.right - rectButton.left - bm.bmWidth) / 2;
			int yOffset = rectButton.top + (rectButton.bottom - rectButton.top - bm.bmHeight) / 2;
			for (int y = 0; y < bm.bmHeight; y++)
			{
				for (int x = 0; x < bm.bmWidth; x++)
				{
					// remember a bitmap is upside down (with y=0 at the bottom)
					DWORD dwRGB = pBits[(bm.bmHeight - y - 1) * bm.bmWidth + x];
					if (dwRGB != 0)
					{
						// black is transparent
						DWORD dwARGB = 0xFF000000 | dwRGB;	// TODO: ordering
						//m_pdwBits[(y + yOffset) * m_nWidth + x + xOffset] = dwARGB;
						m_BitmapBuffer.Set(x + xOffset, y + yOffset, dwARGB);

					}
				}
			}
		}
		delete [] pBits;

		//SelectObject(hDC, hbmOld);
	}

#ifdef USE_GDI
	HDC hDCMem = CreateCompatibleDC(hDC);
	HBITMAP hbmOld = (HBITMAP)SelectObject(hDCMem, hbmMask);

	// get the size of the glyph
	BITMAP bm;
	GetObject(hbmImage, sizeof(bm), &bm);

	// and center it
	int x = (rectButton.right + rectButton.left - bm.bmWidth) / 2;
	int y = (rectButton.top + rectButton.bottom - bm.bmHeight) / 2;

	BitBlt(hDC, x, y, bm.bmWidth, bm.bmHeight, hDCMem, 0, 0, SRCAND);
	SelectObject(hDCMem, hbmImage);
	BitBlt(hDC, x, y, bm.bmWidth, bm.bmHeight, hDCMem, 0, 0, SRCPAINT);

	SelectObject(hDCMem, hbmOld);
	DeleteDC(hDCMem);
#elif USE_GDIPLUS
#endif
}

// called to add spacing after the button
// virtual 
void CThemeDwm::PaintButtonSpacing(HDC hDC, RECT rectButton)
{
	BYTE alpha = 160;
	BYTE greyLevel = 0;
	DWORD dwRGB = (alpha << 24) | (greyLevel << 16) | (greyLevel << 8) | greyLevel;

	m_BitmapBuffer.DrawVLine(rectButton.right, rectButton.top, rectButton.bottom - 1, dwRGB);
}

// virtual 
void CThemeDwm::PaintBarBorder(HDC hDC, RECT rectBar)
{
	BYTE alpha = 160;
	BYTE greyLevel = 0;
	DWORD dwDark = (alpha << 24) | (greyLevel << 16) | (greyLevel << 8) | greyLevel;
	greyLevel = 252;
	DWORD dwLight = (alpha << 24) | (greyLevel << 16) | (greyLevel << 8) | greyLevel;
	alpha = 160;
	greyLevel = 255;
	//DWORD dwTransparent = (alpha << 24) | (greyLevel << 16) | (greyLevel << 8) | greyLevel;
	DWORD dwTransparent = dwLight;

	int radius = 4;

	// left border
	m_BitmapBuffer.DrawVLine(rectBar.left, rectBar.top, rectBar.bottom - radius, dwLight);
	m_BitmapBuffer.DrawVLine(rectBar.left + 1, rectBar.top, rectBar.bottom - radius, dwDark);

	// right border
	m_BitmapBuffer.DrawVLine(rectBar.right - 2, rectBar.top, rectBar.bottom - radius, dwDark);
	m_BitmapBuffer.DrawVLine(rectBar.right - 1, rectBar.top, rectBar.bottom - radius, dwLight);


	// bottom border
	m_BitmapBuffer.DrawHLine(rectBar.left + radius, rectBar.right - 1 - radius, rectBar.bottom - 2, dwDark);
	m_BitmapBuffer.DrawHLine(rectBar.left + radius, rectBar.right - 1 - radius, rectBar.bottom - 1, dwLight);

	// add curves
	m_BitmapBuffer.Set(0, rectBar.bottom - radius, dwTransparent);
	m_BitmapBuffer.Set(1, rectBar.bottom - radius, dwLight);
	m_BitmapBuffer.Set(2, rectBar.bottom - radius, dwDark);
	m_BitmapBuffer.Set(3, rectBar.bottom - radius, dwLight);

	m_BitmapBuffer.Set(0, rectBar.bottom - radius + 1, dwTransparent);
	m_BitmapBuffer.Set(1, rectBar.bottom - radius + 1, dwLight);
	m_BitmapBuffer.Set(2, rectBar.bottom - radius + 1, dwDark);
	m_BitmapBuffer.Set(3, rectBar.bottom - radius + 1, dwDark);
	m_BitmapBuffer.Set(4, rectBar.bottom - radius + 1, dwLight);

	m_BitmapBuffer.Set(0, rectBar.bottom - radius + 2, dwTransparent);
	m_BitmapBuffer.Set(1, rectBar.bottom - radius + 2, dwTransparent);
	m_BitmapBuffer.Set(2, rectBar.bottom - radius + 2, dwLight);
	m_BitmapBuffer.Set(3, rectBar.bottom - radius + 2, dwLight);

	m_BitmapBuffer.Set(0, rectBar.bottom - radius + 3, dwTransparent);
	m_BitmapBuffer.Set(1, rectBar.bottom - radius + 3, dwTransparent);
	m_BitmapBuffer.Set(2, rectBar.bottom - radius + 3, dwTransparent);
	m_BitmapBuffer.Set(3, rectBar.bottom - radius + 3, dwTransparent);


	m_BitmapBuffer.Set(rectBar.right - 1, rectBar.bottom - radius, dwTransparent);
	m_BitmapBuffer.Set(rectBar.right - 2, rectBar.bottom - radius, dwLight);
	m_BitmapBuffer.Set(rectBar.right - 3, rectBar.bottom - radius, dwDark);
	m_BitmapBuffer.Set(rectBar.right - 4, rectBar.bottom - radius, dwLight);

	m_BitmapBuffer.Set(rectBar.right - 1, rectBar.bottom - radius + 1, dwTransparent);
	m_BitmapBuffer.Set(rectBar.right - 2, rectBar.bottom - radius + 1, dwLight);
	m_BitmapBuffer.Set(rectBar.right - 3, rectBar.bottom - radius + 1, dwDark);
	m_BitmapBuffer.Set(rectBar.right - 4, rectBar.bottom - radius + 1, dwDark);
	m_BitmapBuffer.Set(rectBar.right - 5, rectBar.bottom - radius + 1, dwLight);

	m_BitmapBuffer.Set(rectBar.right - 1, rectBar.bottom - radius + 2, dwTransparent);
	m_BitmapBuffer.Set(rectBar.right - 2, rectBar.bottom - radius + 2, dwTransparent);
	m_BitmapBuffer.Set(rectBar.right - 3, rectBar.bottom - radius + 2, dwLight);
	m_BitmapBuffer.Set(rectBar.right - 4, rectBar.bottom - radius + 2, dwLight);

	m_BitmapBuffer.Set(rectBar.right - 1, rectBar.bottom - radius + 3, dwTransparent);
	m_BitmapBuffer.Set(rectBar.right - 2, rectBar.bottom - radius + 3, dwTransparent);
	m_BitmapBuffer.Set(rectBar.right - 3, rectBar.bottom - radius + 3, dwTransparent);
	m_BitmapBuffer.Set(rectBar.right - 4, rectBar.bottom - radius + 3, dwTransparent);

#ifdef USE_GDI
		// the pen to draw border around buttons
		HPEN hPen = CreatePen(PS_SOLID, 1, RGB(128, 128, 128));
		//HPEN hPen = CreatePen(PS_SOLID, 1, RGB(255, 0, 0));
		//HPEN hPen = CreatePen(PS_SOLID, 1, RGB(1, 0, 0));
		//HPEN hPen = CreatePen(PS_SOLID, 1, RGB(0, 0, 0));
		HPEN hOldPen = (HPEN)SelectObject(hDC, hPen);

		int nArcRadius = 4;

		MoveToEx(hDC, rectBar.left, rectBar.top, NULL);
		LineTo(hDC, rectBar.left, rectBar.bottom - 1 - nArcRadius);
		// TODO: need AA
		Arc(hDC, rectBar.left, rectBar.bottom - nArcRadius - nArcRadius,
			rectBar.left + nArcRadius * 2, rectBar.bottom,
			// direction is counter clockwide by default
			rectBar.left, rectBar.bottom - nArcRadius,
			rectBar.left + nArcRadius, rectBar.bottom);

			//	Ellipse(hDC, rectBar.left, rectBar.bottom - nArcRadius - nArcRadius,
			//rectBar.left + nArcRadius * 2, rectBar.bottom);

		MoveToEx(hDC, rectBar.left + nArcRadius - 1, rectBar.bottom - 1, NULL);
		LineTo(hDC, rectBar.right - 1 - nArcRadius, rectBar.bottom - 1);

		Arc(hDC, rectBar.right - 2 * nArcRadius, rectBar.bottom - nArcRadius - nArcRadius,
			rectBar.right, rectBar.bottom,
			// direction is counter clockwide by default
			rectBar.right - nArcRadius, rectBar.bottom,
			rectBar.right, rectBar.bottom - nArcRadius);

			//Ellipse(hDC, rectBar.right - 2 * nArcRadius, rectBar.bottom - nArcRadius - nArcRadius,
			//rectBar.right, rectBar.bottom);

		MoveToEx(hDC, rectBar.right - 1, rectBar.top, NULL);
		LineTo(hDC, rectBar.right - 1, rectBar.bottom - nArcRadius);

		// restore DC to original state 
		SelectObject(hDC, hOldPen);
#elif USE_GDIPLUS
#endif
}

// virtual 
void CThemeDwm::PaintEnd(HDC hDC, RECT rectBar)
{
#ifdef USE_GDI
#else
	GdiplusStartup(&gdiplusToken, &gdiplusStartupInput, NULL);

	const DWORD* pdwBits = m_BitmapBuffer.GetBits();
	int nWidth = m_BitmapBuffer.GetWidth();
	int nHeight = m_BitmapBuffer.GetHeight();
	if (pdwBits)
	{
#ifdef ORIGINAL
		Gdiplus::Graphics graphics(hDC);

		
		// can't seem to get transparency working correctly, so just pre-fill
		// our background with the theme colour for now
		if (m_bIsAvailable)
		{
			DWORD color = 0;
			BOOL opaque = FALSE;

			HRESULT hr = DwmGetColorizationColor(&color, &opaque);
			if (SUCCEEDED(hr))
			{
				SolidBrush bgrBrush(CTheme::ARGBBlend(color));
				graphics.FillRectangle(&bgrBrush, 0, 0, nWidth, nHeight);
			}
		}

		Bitmap bitmap(nWidth, nHeight, 4 * nWidth, PixelFormat32bppARGB, (BYTE*)pdwBits);
		graphics.DrawImage(&bitmap, 0, 0);
#else
		// avoid flickering by building the image in a buffer
		Bitmap DisplayBuffer(nWidth, nHeight, PixelFormat32bppARGB);
		Graphics* pBufferGraphics = Graphics::FromImage(&DisplayBuffer);

		// can't seem to get transparency working correctly, so just pre-fill
		// our buffer with the theme colour for now
		// we really need to fill it with what ever is below our window
		if (m_bIsAvailable)
		{
			DWORD color = 0;
			BOOL opaque = FALSE;

			HRESULT hr = DwmGetColorizationColor(&color, &opaque);
			if (SUCCEEDED(hr))
			{
				SolidBrush bgrBrush(CTheme::ARGBBlend(color));
				pBufferGraphics->FillRectangle(&bgrBrush, 0, 0, nWidth, nHeight);
			}
		}

		Bitmap bitmap(nWidth, nHeight, 4 * nWidth, PixelFormat32bppARGB, (BYTE*)pdwBits);
		pBufferGraphics->DrawImage(&bitmap, 0, 0);

		Gdiplus::Graphics graphics(hDC);
		graphics.DrawImage(&DisplayBuffer, 0, 0);

		delete pBufferGraphics;

#endif
	}

	GdiplusShutdown(gdiplusToken);

#endif
}
