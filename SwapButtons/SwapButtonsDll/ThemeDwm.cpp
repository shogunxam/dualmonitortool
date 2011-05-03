#include "StdAfx.h"
#include <dwmapi.h>

#include "ThemeDwm.h"
#include "LayoutManager.h"

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


CThemeDwm::~CThemeDwm(void)
{
	if (m_hDwmLib)
	{
		FreeLibrary(m_hDwmLib);
	}
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
	pLayoutMetrics->m_nButtonWidth = nStdButtonSize + 4;
	pLayoutMetrics->m_nButtonHeight = nStdButtonSize - 3;

	// TODO: hack while testing
	pLayoutMetrics->m_nButtonWidth = 26;
	pLayoutMetrics->m_nButtonHeight = 16;

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

Graphics* m_pGraphics = NULL;
Bitmap* m_pBitmap = NULL;
DWORD* m_pdwBits = NULL;
int m_nWidth;
int m_nHeight;
RECT m_RectBar;

// virtual 
void CThemeDwm::PaintStart(HDC hDC, RECT rectBar)
{
#ifdef USE_GDI
#else
	m_RectBar = rectBar;
	//GdiplusStartup(&gdiplusToken, &gdiplusStartupInput, NULL);

	//m_pGraphics = new Graphics(hDC);
	//m_pBitmap = new Bitmap(rectBar.right - rectBar.left, rectBar.bottom - rectBar.top);

	m_nWidth = rectBar.right - rectBar.left;
	m_nHeight = rectBar.bottom - rectBar.top;
	m_pdwBits = new DWORD[m_nWidth * m_nHeight];

	//int nMid = m_nHeight / 2;
	//for (int y = 0; y < m_nHeight; y++)
	//{
	//	byte alpha = 160;
	//	BYTE greyLevel;
	//	if (y == 0)
	//	{
	//		greyLevel = 252;
	//	}
	//	else if (y > nMid)
	//	{
	//		greyLevel = 180 + (180 - 200) * (y - nMid) / nMid;
	//	}
	//	else
	//	{
	//		greyLevel = 234 + (211 - 234) * y / nMid;
	//	}

	//	//Color color(alpha, greyLevel, greyLevel, greyLevel);
	//	DWORD dw = (alpha << 24) | (greyLevel << 16) | (greyLevel << 8) | greyLevel;

	//	for (int x = 0; x < m_nWidth; x++)
	//	{
	//		m_pdwBits[y * m_nWidth + x] = dw;
	//	}
	//}
#endif

}

// virtual 
void CThemeDwm::PaintBarBackground(HDC hDC, RECT rectBar)
{
#ifdef USE_GDI
	//HBRUSH hBrush = CreateSolidBrush(m_BgrColour);
	HBRUSH hBrush = CreateSolidBrush(TRANSPARENT_COLOUR);
	//HBRUSH hBrush = CreateSolidBrush(RGB(0, 0, 0));
	//HBRUSH hBrush = CreateSolidBrush(RGB(255, 255, 255));
	FillRect(hDC, &rectBar, hBrush);
#else
//	Pen redPen(Color(255, 255, 0, 0), 2);
//	m_pGraphics->DrawLine(&redPen, 0, 0, rectBar.right - rectBar.left, rectBar.bottom - rectBar.top);
#endif
}

// virtual 
void CThemeDwm::PaintButtonFace(HDC hDC, RECT rectButton, HBITMAP hbmImage, HBITMAP hbmMask)
{
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
#else
	byte alpha = 160;
	BYTE greyLevel = 252;
	DWORD dwLightBorder =(alpha << 24) | (greyLevel << 16) | (greyLevel << 8) | greyLevel;

	int nMid = (rectButton.top + rectButton.bottom) / 2;
	for (int y = rectButton.top; y < rectButton.bottom; y++)
	{
		if (y == rectButton.top || y == rectButton.bottom - 1)
		{
			greyLevel = 252;
		}
		else if (y > nMid)
		{
			greyLevel = 180 + (180 - 200) * (y - nMid) / nMid;
		}
		else
		{
			greyLevel = 234 + (211 - 234) * y / nMid;
		}

		//Color color(alpha, greyLevel, greyLevel, greyLevel);
		//DWORD dw = (alpha << 24) | (greyLevel << 16) | (greyLevel << 8) | greyLevel;

		DWORD dw = Color::MakeARGB(alpha, greyLevel, greyLevel, greyLevel);

		m_pdwBits[y * m_nWidth + rectButton.left] = dwLightBorder;
		for (int x = rectButton.left + 1; x < rectButton.right - 1; x++)
		{
			m_pdwBits[y * m_nWidth + x] = dw;
		}
		m_pdwBits[y * m_nWidth + rectButton.right - 1] = dwLightBorder;
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
					DWORD dwRGB = pBits[y * bm.bmWidth + x];
					if (dwRGB != 0)
					{
						// black is transparent
						DWORD dwARGB = 0xFF000000 | dwRGB;	// TODO: ordering
						m_pdwBits[(y + yOffset) * m_nWidth + x + xOffset] = dwARGB;
					}
				}
			}
		}
		delete [] pBits;

		//SelectObject(hDC, hbmOld);
	}
#endif
}

// called to add spacing after the button
// virtual 
void CThemeDwm::PaintButtonSpacing(HDC hDC, RECT rectButton)
{
#ifdef USE_GDI
	HPEN hPen = CreatePen(PS_SOLID, 1, RGB(128, 128, 128));
	HPEN hOldPen = (HPEN)SelectObject(hDC, hPen);

	MoveToEx(hDC, rectButton.right, rectButton.top, NULL);
	LineTo(hDC, rectButton.right, rectButton.bottom);

	// restore DC to original state 
	SelectObject(hDC, hOldPen);

#else
	//Graphics	graphics(hDC);
	//Pen pen(Color(255, 128, 128, 128));
	//graphics.DrawLine(&pen, rectButton.right, rectButton.top, rectButton.right, rectButton.bottom);

	BYTE alpha = 160;
	BYTE greyLevel = 0;
	DWORD dw = (alpha << 24) | (greyLevel << 16) | (greyLevel << 8) | greyLevel;
	for (int y = rectButton.top; y < rectButton.bottom; y++)
	{
		m_pdwBits[y * m_nWidth + rectButton.right] = dw;
	}
#endif
}

// virtual 
void CThemeDwm::PaintBarBorder(HDC hDC, RECT rectBar)
{
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
#else
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
	for (int y = rectBar.top; y < rectBar.bottom - radius; y++)
	{
		// left border
		m_pdwBits[y * m_nWidth + rectBar.left] = dwLight;
		m_pdwBits[y * m_nWidth + rectBar.left + 1] = dwDark;

		// right border
		m_pdwBits[y * m_nWidth + rectBar.right - 1] = dwLight;
		m_pdwBits[y * m_nWidth + rectBar.right - 2] = dwDark;
	}

	for (int x = rectBar.left + radius; x < rectBar.right - radius; x++)
	{
		// bottom border
		m_pdwBits[(rectBar.bottom - 1) * m_nWidth + x] = dwLight;
		m_pdwBits[(rectBar.bottom - 2) * m_nWidth + x] = dwDark;
	}

	// add curves
	m_pdwBits[(rectBar.bottom - radius)* m_nWidth + 0] = dwTransparent;
	m_pdwBits[(rectBar.bottom - radius)* m_nWidth + 1] = dwLight;
	m_pdwBits[(rectBar.bottom - radius)* m_nWidth + 2] = dwDark;
	m_pdwBits[(rectBar.bottom - radius)* m_nWidth + 3] = dwLight;

	m_pdwBits[(rectBar.bottom - radius + 1)* m_nWidth + 0] = dwTransparent;
	m_pdwBits[(rectBar.bottom - radius + 1)* m_nWidth + 1] = dwLight;
	m_pdwBits[(rectBar.bottom - radius + 1)* m_nWidth + 2] = dwDark;
	m_pdwBits[(rectBar.bottom - radius + 1)* m_nWidth + 3] = dwDark;
	m_pdwBits[(rectBar.bottom - radius + 1)* m_nWidth + 4] = dwLight;

	m_pdwBits[(rectBar.bottom - radius + 2)* m_nWidth + 0] = dwTransparent;
	m_pdwBits[(rectBar.bottom - radius + 2)* m_nWidth + 1] = dwTransparent;
	m_pdwBits[(rectBar.bottom - radius + 2)* m_nWidth + 2] = dwLight;
	m_pdwBits[(rectBar.bottom - radius + 2)* m_nWidth + 3] = dwLight;

	m_pdwBits[(rectBar.bottom - radius + 3)* m_nWidth + 0] = dwTransparent;
	m_pdwBits[(rectBar.bottom - radius + 3)* m_nWidth + 1] = dwTransparent;
	m_pdwBits[(rectBar.bottom - radius + 3)* m_nWidth + 2] = dwTransparent;
	m_pdwBits[(rectBar.bottom - radius + 3)* m_nWidth + 3] = dwTransparent;


	m_pdwBits[(rectBar.bottom - radius)* m_nWidth + m_nWidth - 1] = dwTransparent;
	m_pdwBits[(rectBar.bottom - radius)* m_nWidth + m_nWidth - 2] = dwLight;
	m_pdwBits[(rectBar.bottom - radius)* m_nWidth + m_nWidth - 3] = dwDark;
	m_pdwBits[(rectBar.bottom - radius)* m_nWidth + m_nWidth - 4] = dwLight;

	m_pdwBits[(rectBar.bottom - radius + 1)* m_nWidth + m_nWidth - 1] = dwTransparent;
	m_pdwBits[(rectBar.bottom - radius + 1)* m_nWidth + m_nWidth - 2] = dwLight;
	m_pdwBits[(rectBar.bottom - radius + 1)* m_nWidth + m_nWidth - 3] = dwDark;
	m_pdwBits[(rectBar.bottom - radius + 1)* m_nWidth + m_nWidth - 4] = dwDark;
	m_pdwBits[(rectBar.bottom - radius + 1)* m_nWidth + m_nWidth - 5] = dwLight;

	m_pdwBits[(rectBar.bottom - radius + 2)* m_nWidth + m_nWidth - 1] = dwTransparent;
	m_pdwBits[(rectBar.bottom - radius + 2)* m_nWidth + m_nWidth - 2] = dwTransparent;
	m_pdwBits[(rectBar.bottom - radius + 2)* m_nWidth + m_nWidth - 3] = dwLight;
	m_pdwBits[(rectBar.bottom - radius + 2)* m_nWidth + m_nWidth - 4] = dwLight;

	m_pdwBits[(rectBar.bottom - radius + 3)* m_nWidth + m_nWidth - 1] = dwTransparent;
	m_pdwBits[(rectBar.bottom - radius + 3)* m_nWidth + m_nWidth - 2] = dwTransparent;
	m_pdwBits[(rectBar.bottom - radius + 3)* m_nWidth + m_nWidth - 3] = dwTransparent;
	m_pdwBits[(rectBar.bottom - radius + 3)* m_nWidth + m_nWidth - 4] = dwTransparent;

#endif
}

// virtual 
void CThemeDwm::PaintEnd(HDC hDC)
{
#ifdef USE_GDI
#else
	GdiplusStartup(&gdiplusToken, &gdiplusStartupInput, NULL);

//	m_pGraphics = new Graphics(hDC);

	if (m_pdwBits)
	{
		//BITMAP gdiBitmap;
		//gdiBitmap.bmType = 0;
		//gdiBitmap.bmWidth = m_nWidth;
		//gdiBitmap.bmHeight = m_nHeight;
		//gdiBitmap.bmWidthBytes = m_nWidth * sizeof(DWORD);
		//gdiBitmap.bmPlanes = 1;
		//gdiBitmap.bmBitsPixel = 32;
		//gdiBitmap.bmBits = m_pdwBits;

		//HBITMAP hBitmap = CreateBitmap(m_nWidth, m_nHeight, 1, 32, m_pdwBits);
		//if (hBitmap)
		//{
		//	Bitmap bitmap(hBitmap, NULL);

		//	Graphics graphics(hDC);
		//	graphics.DrawImage(&bitmap, 0, 0);


		//	DeleteObject(hBitmap);
		//}


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
				graphics.FillRectangle(&bgrBrush, 0, 0, m_nWidth, m_nHeight);
			}
		}



		Bitmap bitmap(m_nWidth, m_nHeight, 4 * m_nWidth, PixelFormat32bppARGB, (BYTE*)m_pdwBits);
		graphics.DrawImage(&bitmap, 0, 0);

//	Pen redPen(Color(255, 255, 0, 0), 2);
//	graphics.DrawLine(&redPen, 0, 0, m_nWidth / 2, 0);



	}

	delete [] m_pdwBits;
	m_pdwBits = NULL;

	//delete m_pBitmap;
	//m_pBitmap = NULL;

//	delete m_pGraphics;
//	m_pGraphics = NULL;

	GdiplusShutdown(gdiplusToken);
#endif
}
