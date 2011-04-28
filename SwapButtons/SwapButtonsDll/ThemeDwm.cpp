#include "StdAfx.h"
#include <dwmapi.h>

#include "ThemeDwm.h"
#include "LayoutManager.h"

//#define TRANSPARENT_COLOUR	(RGB(0, 255, 0))
//#define TRANSPARENT_COLOUR	(RGB(1, 1, 1))
#define TRANSPARENT_COLOUR	(RGB(0, 0, 0))



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

	pLayoutMetrics->m_nLeftBorder = 1;
	pLayoutMetrics->m_nRightBorder = 1;
	pLayoutMetrics->m_nTopBorder = 0;
	pLayoutMetrics->m_nBottomBorder = 1;
	pLayoutMetrics->m_nSpacing = 1;

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
//	DWORD dwExStyle = GetWindowLong(hWndFloatBar, GWL_EXSTYLE);
//	dwExStyle |= WS_EX_LAYERED;
//	SetWindowLong(hWndFloatBar, GWL_EXSTYLE, dwExStyle);

	// set the colour we are going to use as transparent
	SetLayeredWindowAttributes(hWndFloatBar, TRANSPARENT_COLOUR, 0, LWA_COLORKEY);


	DWM_BLURBEHIND blurBehind = { 0 };
    
    blurBehind.dwFlags = DWM_BB_ENABLE | DWM_BB_TRANSITIONONMAXIMIZED;
    blurBehind.fEnable = TRUE;
    blurBehind.fTransitionOnMaximized = FALSE;

        blurBehind.dwFlags |= DWM_BB_BLURREGION;
        blurBehind.hRgnBlur = 0;
 
   DwmEnableBlurBehindWindow(hWndFloatBar, &blurBehind);
}

// virtual 
void CThemeDwm::PaintBarBackground(HDC hDC, RECT rectBar)
{
	//HBRUSH hBrush = CreateSolidBrush(m_BgrColour);
	//HBRUSH hBrush = CreateSolidBrush(TRANSPARENT_COLOUR);
	HBRUSH hBrush = CreateSolidBrush(RGB(0, 0, 0));
	FillRect(hDC, &rectBar, hBrush);
}

// virtual 
void CThemeDwm::PaintButtonFace(HDC hDC, RECT rectButton, HBITMAP hbmImage, HBITMAP hbmMask)
{
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
}

// virtual 
void CThemeDwm::PaintButtonSpacing(HDC hDC, RECT rectButton)
{
	HPEN hPen = CreatePen(PS_SOLID, 1, RGB(128, 128, 128));
	HPEN hOldPen = (HPEN)SelectObject(hDC, hPen);

	MoveToEx(hDC, rectButton.right, rectButton.top, NULL);
	LineTo(hDC, rectButton.right, rectButton.bottom);

	// restore DC to original state 
	SelectObject(hDC, hOldPen);
}

// virtual 
void CThemeDwm::PaintBarBorder(HDC hDC, RECT rectBar)
{
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
}
