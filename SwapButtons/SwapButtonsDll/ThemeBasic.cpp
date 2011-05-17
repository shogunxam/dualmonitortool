#include "StdAfx.h"

#include <dwmapi.h>
#include <UxTheme.h>
#include <vsstyle.h>
#include <vssym32.h>

#include "ThemeBasic.h"
#include "LayoutManager.h"
#include "ButtonList.h"
#include "ImageStrip.h"

#include "ResourceDll.h"


const int CThemeBasic::LEFT_BORDER = 0;
const int CThemeBasic::RIGHT_BORDER = 0;
const int CThemeBasic::TOP_BORDER = 0;
const int CThemeBasic::BOTTOM_BORDER = 0;
const int CThemeBasic::BUTTON_SPACING = 2;


CThemeBasic::CThemeBasic(void)
	: m_hTheme(NULL),
	  m_hDCMem(NULL),
	  m_hbmBar(NULL),
	  m_hbmOld(NULL),
	  m_hbmBackground(NULL),
	  m_hDwmLib(NULL)
{
}

// virtual
CThemeBasic::~CThemeBasic(void)
{
	if (m_hbmSupersizeMask)
	{
		DeleteObject(m_hbmSupersizeMask);
	}
	if (m_hbmNextMask)
	{
		DeleteObject(m_hbmNextMask);
	}
	if (m_hbmPrevMask)
	{
		DeleteObject(m_hbmPrevMask);
	}

	if (m_hbmBackground)
	{
		DeleteObject(m_hbmBackground);
	}
	if (m_hDwmLib)
	{
		FreeLibrary(m_hDwmLib);
	}
	if (m_hTheme)
	{
		CloseThemeData(m_hTheme);
	}
}

// virtual
void CThemeBasic::LoadBitmaps(HMODULE hModule)
{
	m_hbmPrev = LoadBitmap(hModule, MAKEINTRESOURCE(IDB_PREV));
	m_hbmNext = LoadBitmap(hModule, MAKEINTRESOURCE(IDB_NEXT));
	m_hbmSupersize = LoadBitmap(hModule, MAKEINTRESOURCE(IDB_SUPERSIZE));

	// create the masks for these glyphs
	m_hbmPrevMask = CreateMask(m_hbmPrev);
	m_hbmNextMask = CreateMask(m_hbmNext);
	m_hbmSupersizeMask = CreateMask(m_hbmSupersize);
}

// virtual 
bool CThemeBasic::IsAvailable()
{
	if (!m_bCheckedAvailable)
	{
		CheckIfAvailable();
	}
	return m_bIsAvailable;
}

// private
void CThemeBasic::CheckIfAvailable()
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

bool CThemeBasic::IsInUse(HWND hWndFrame)
{
	// TODO: how do we determine this?
	return true;
}

// virtual 
bool CThemeBasic::ReInit(struct LayoutMetrics* pLayoutMetrics, HWND hWndFrame)
{
	if (m_hTheme)
	{
		CloseThemeData(m_hTheme);
		m_hTheme = NULL;
	}
	m_hTheme = OpenThemeData(hWndFrame, L"WINDOW");
	if (m_hTheme)
	{
		m_nButtonWidth = 0;
		m_nButtonHeight = 0;
		// remove any existing button background
		if (m_hbmBackground)
		{
			DeleteObject(m_hbmBackground);
			m_hbmBackground = NULL;
		}

		HRESULT hr;

		MARGINS margins;
		hr = GetThemeMargins(m_hTheme, NULL, WP_MINBUTTON, 0, TMT_SIZINGMARGINS, NULL, &margins);

		int nLayout;
		hr = GetThemeEnumValue(m_hTheme, WP_MINBUTTON, 0, TMT_IMAGELAYOUT, &nLayout);

		//RECT rectButton;
		//hr = GetThemeRect(m_hTheme, WP_MINBUTTON, 0, TMT_DEFAULTPANESIZE, &rectButton);
		int nW;
		int nH;
		hr = GetThemeInt(m_hTheme, WP_MINBUTTON, 0, TMT_WIDTH, &nW);
		hr = GetThemeMetric(m_hTheme, NULL, WP_MINBUTTON, 0, TMT_HEIGHT, &nH);

		//SIZE sz;
		//hr = GetThemePartSize(m_hTheme, NULL, WP_CAPTION, 0, NULL, TMT_CAPTIONBAR, &sz);

		//HRESULT hr = GetThemeBitmap(hTheme, WP_CAPTION, CS_ACTIVE, TMT_DIBDATA, GBF_COPY, &m_hbmBackground);
		//HRESULT hr = GetThemeBitmap(m_hTheme, WP_CLOSEBUTTON, CBS_NORMAL, TMT_DIBDATA, GBF_COPY, &m_hbmBackground);
		//HRESULT hr = GetThemeBitmap(m_hTheme, WP_CAPTION, 0, TMT_DIBDATA, GBF_COPY, &m_hbmBackground);
		//HRESULT hr = GetThemeBitmap(m_hTheme, WP_MINBUTTON, MINBS_NORMAL, TMT_DIBDATA, GBF_COPY, &m_hbmBackground);
		//HRESULT hr = GetThemeBitmap(m_hTheme, WP_MINBUTTON, 0, TMT_DIBDATA, GBF_COPY, &m_hbmBackground);
		hr = GetThemeBitmap(m_hTheme, WP_MINBUTTON, 0, TMT_DIBDATA, GBF_COPY, &m_hbmBackground);
		if (SUCCEEDED(hr))
		{
			OutputDebugString(L"Got background\n");
			if (m_hbmBackground)
			{
				BITMAP bm;
				GetObject(m_hbmBackground, sizeof(bm), &bm);
				m_nButtonWidth = bm.bmWidth;
				m_nButtonHeight = bm.bmHeight;
			}
		}
		//CloseThemeData(hTheme);

		// TODO:
		m_nButtonWidth = 24;
		m_nButtonHeight = 16;

		m_ImageStrip.SetImageStrip(m_hbmBackground, 8, margins, nLayout == IL_VERTICAL);

		pLayoutMetrics->m_nButtonWidth = m_nButtonWidth;
		pLayoutMetrics->m_nButtonHeight = m_nButtonHeight;

		wchar_t szMsg[256];
		wsprintf(szMsg, L"Width: %d Height: %d\n", pLayoutMetrics->m_nButtonWidth, pLayoutMetrics->m_nButtonHeight);
		OutputDebugString(szMsg);


		pLayoutMetrics->m_nLeftBorder = LEFT_BORDER;
		pLayoutMetrics->m_nRightBorder = RIGHT_BORDER;
		pLayoutMetrics->m_nTopBorder = TOP_BORDER;
		pLayoutMetrics->m_nBottomBorder = BOTTOM_BORDER;
		pLayoutMetrics->m_nSpacing = BUTTON_SPACING;

		SaveBgrColour();

		return true;
	}

	return false;
}

// private
void CThemeBasic::SaveBgrColour()
{
	if (m_bIsAvailable)
	{
		//DWORD color = 0;
		//BOOL opaque = FALSE;

		//HRESULT hr = DwmGetColorizationColor(&color, &opaque);
		//if (SUCCEEDED(hr))
		//{
		//	m_BgrColour = ARGBToColorref(color);
		//}

		COLORREF color;
		HRESULT hr = GetThemeColor(m_hTheme, WP_CAPTION, CS_ACTIVE, TMT_ACTIVECAPTION, &color);
		if (SUCCEEDED(hr))
		{
			m_BgrColour = color;
		}

	}
}

#define IS_TITLE_BUTTON_VISIBLE(dw) (dw & (STATE_SYSTEM_INVISIBLE | STATE_SYSTEM_OFFSCREEN | STATE_SYSTEM_UNAVAILABLE)) == 0

// virtual 
SIZE CThemeBasic::CalcBarOffsets(HWND hWndFrame)
{
	int nExistingButtonsWidth = 0;
	DWORD dwStyle = GetWindowLong(hWndFrame, GWL_STYLE);
	DWORD dwExStyle = GetWindowLong(hWndFrame, GWL_EXSTYLE);
	RECT rectFrame;
	GetWindowRect(hWndFrame, &rectFrame);
	int nRightPos = rectFrame.right;
	int nTopPos = rectFrame.top;

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

	// NOTE: DWMWA_CAPTION_BUTTON_BOUNDS may also be of use?

//	if (IsPreVista())
	{
	}
//	else
	{
		// Vista & later only
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
				nTopPos = titleBarInfoEx.rgrect[nTitleBarChild].top;
				if (titleBarInfoEx.rgrect[nTitleBarChild].left < nRightPos)
				{
					nRightPos = titleBarInfoEx.rgrect[nTitleBarChild].left;
					// should be able to break here as indexes are in button order (left to right)
				}
			}
		}
	}

	// TODO: temp hack - this needs to go into theme
	//nTopPos += 1;
	nRightPos -= nStdButtonSize / 2; // to allow spacing between the button sets

	SIZE offsets;
	offsets.cx = rectFrame.right - nRightPos;
	offsets.cy = nTopPos - rectFrame.top;
	return offsets;
}


// virtual 
void CThemeBasic::PrepareFloatBar(HWND hWndFloatBar)
{
}

//static DWORD* m_pdwBits = NULL;
//static int m_nWidth;
//static int m_nHeight;
//static RECT m_RectBar;

// virtual 
void CThemeBasic::PaintBar(HWND hWndFloatBar, HDC hDC, const CButtonList& buttonList, RECT rectBar)
{
	PaintStart(hDC, rectBar);

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

		// TODO: convert index to button ID
		PaintButtonFace(hWndFloatBar, hDC, rectButton, index);

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

// virtual 
void CThemeBasic::PaintStart(HDC hDC, RECT rectBar)
{
	if (m_hDCMem)
	{
		// shouldn't get here
	}

	int nWidth = rectBar.right - rectBar.left;
	int nHeight = rectBar.bottom - rectBar.top;

	BITMAPINFO bmi;
	memset(&bmi, 0, sizeof(bmi));
	bmi.bmiHeader.biSize = sizeof(bmi.bmiHeader);
	bmi.bmiHeader.biWidth = nWidth;
	bmi.bmiHeader.biHeight = -nHeight;
	bmi.bmiHeader.biPlanes = 1;
	bmi.bmiHeader.biBitCount = 32;
	bmi.bmiHeader.biCompression = BI_RGB;
	bmi.bmiHeader.biSizeImage = 0;

	m_hbmBar = CreateCompatibleBitmap (hDC, nWidth, nHeight);

	m_hDCMem = CreateCompatibleDC(hDC);
	m_hbmOld = (HBITMAP)SelectObject(m_hDCMem, m_hbmBar);


	// fill background with defult colour
	HBRUSH hBrush = CreateSolidBrush(m_BgrColour); 
	FillRect(hDC, &rectBar, hBrush);
	DeleteObject(hBrush);

}

// virtual 
void CThemeBasic::PaintBarBackground(HDC hDC, RECT rectBar)
{
	HBRUSH hBrush = CreateSolidBrush(m_BgrColour);
	FillRect(m_hDCMem, &rectBar, hBrush);
}

#define TO_ARGB(rgb) (((rgb & 0xFF) << 16) | ((rgb & 0xFF0000) >> 16) | (rgb & 0xFF00) | 0xFF000000)

//#define FLIP_Y(y) (m_nHeight - (y))

//#define BM_INDEX(x, y) ((m_nHeight - (y) - 1) * m_nWidth + (x))

// virtual 
void CThemeBasic::PaintButtonFace(HWND hWndFloatBar, HDC hDC, RECT rectButton, int index)
{
	int nButtonWidth = rectButton.right - rectButton.left;
	int nButtonHeight = rectButton.bottom - rectButton.top;

	//HDC hdDCButton = CreateCompatibleDC(m_hDCMem);
//	HDC hdDCButton = CreateCompatibleDC(hDC);
//	HBITMAP hbmOld = (HBITMAP)SelectObject(hdDCButton, m_hbmBackground);
//	BITMAP bm;
//	GetObject(m_hbmBackground, sizeof(BITMAP), &bm);

	//BitBlt(m_hDCMem, rectButton.left, rectButton.top, nButtonWidth, nButtonHeight, hdDCButton, 0, 0, SRCCOPY);
//	StretchBlt(m_hDCMem, rectButton.left, rectButton.top, nButtonWidth, nButtonHeight, hdDCButton, 0, 0, bm.bmWidth, bm.bmHeight, SRCCOPY);

	//if (m_hTheme)
	//{
		HRESULT hr;

		if (IsThemeBackgroundPartiallyTransparent(m_hTheme, WP_MINBUTTON, 0))
		{
			DrawThemeParentBackground(hWndFloatBar, m_hDCMem, &rectButton);
		}

	//	//hr = DrawThemeBackground(m_hTheme, m_hDCMem, WP_CAPTION, 0, &rectButton, NULL);
	//	//hr = DrawThemeBackground(m_hTheme, m_hDCMem, WP_CLOSEBUTTON, CBS_NORMAL, &rectButton, NULL);
	//	//hr = DrawThemeBackground(m_hTheme, m_hDCMem, WP_MINBUTTON, MINBS_NORMAL, &rectButton, NULL);
	//	hr = DrawThemeBackground(m_hTheme, m_hDCMem, WP_MINBUTTON, 0, &rectButton, NULL);

	//}

	//SelectObject(hdDCButton, hbmOld);
	//DeleteDC(hdDCButton);







	m_ImageStrip.Draw(0, m_hDCMem, rectButton);



	// now add the glyph
	//HBITMAP hbmImage = GetImage(index);

	//BITMAP bm;
	//GetObject(hbmImage, sizeof(BITMAP), &bm);
	//int nGlyphWidth = bm.bmWidth;
	//int nGlyphHeight = bm.bmHeight;
	//int xOffset = (nButtonWidth - bm.bmWidth) / 2;
	//int yOffset = (nButtonHeight - bm.bmHeight) / 2;

	//HDC hdDCButton = CreateCompatibleDC(m_hDCMem);
	//HBITMAP hbmOld = (HBITMAP)SelectObject(hdDCButton, hbmImage);
	//BitBlt(m_hDCMem, rectButton.left + xOffset, rectButton.top + yOffset, nGlyphWidth, nGlyphHeight, hdDCButton, 0, 0, SRCCOPY);
	//SelectObject(hdDCButton, hbmOld);
	//DeleteDC(hdDCButton);


	HBITMAP hbmImage;
	HBITMAP hbmMask;

	if (GetImage(index, &hbmImage, &hbmMask))
	{
		HDC hDCMem = CreateCompatibleDC(m_hDCMem);
		HBITMAP hbmOld = (HBITMAP)SelectObject(hDCMem, hbmMask);

		BITMAP bm;
		GetObject(hbmImage, sizeof(bm), &bm);

		int x = (rectButton.right + rectButton.left - bm.bmWidth) / 2;
		int y = (rectButton.top + rectButton.bottom - bm.bmHeight) / 2;

		BitBlt(m_hDCMem, x, y, bm.bmWidth, bm.bmHeight, hDCMem, 0, 0, SRCAND);
		SelectObject(hDCMem, hbmImage);
		BitBlt(m_hDCMem, x, y, bm.bmWidth, bm.bmHeight, hDCMem, 0, 0, SRCPAINT);

		SelectObject(hDCMem, hbmOld);
		DeleteDC(hDCMem);
	}


}

// called to add spacing after the button
// virtual 
void CThemeBasic::PaintButtonSpacing(HDC hDC, RECT rectButton)
{
	// no spacing between buttons
}

// virtual 
void CThemeBasic::PaintBarBorder(HDC hDC, RECT rectBar)
{
	// no border
}

// virtual 
void CThemeBasic::PaintEnd(HDC hDC, RECT rectBar)
{
	int nWidth = rectBar.right - rectBar.left;
	int nHeight = rectBar.bottom - rectBar.top;

	BitBlt(hDC, 0, 0, nWidth, nHeight, m_hDCMem, 0, 0, SRCCOPY);

	// clean up
	SelectObject(m_hDCMem, m_hbmOld);
	DeleteDC(m_hDCMem);
}

bool CThemeBasic::GetImage(int index, HBITMAP* pImage, HBITMAP* pMask)
{
	switch (index)
	{
	case 0:
		*pImage = m_hbmPrev;
		*pMask = m_hbmPrevMask;
		break;
	case 1:
		*pImage = m_hbmNext;
		*pMask = m_hbmNextMask;
		break;
	case 2:
		*pImage = m_hbmSupersize;
		*pMask = m_hbmSupersizeMask;
		break;
	default:
		return false;
	}

	return true;
}
