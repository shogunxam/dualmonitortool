#include "StdAfx.h"
#include "ThemeClassic.h"
#include "LayoutManager.h"

#include "ResourceDll.h"


const int CThemeClassic::LEFT_BORDER = 0;
const int CThemeClassic::RIGHT_BORDER = 0;
const int CThemeClassic::TOP_BORDER = 0;
const int CThemeClassic::BOTTOM_BORDER = 0;
const int CThemeClassic::BUTTON_SPACING = 0;


CThemeClassic::CThemeClassic(void)
{
}

// virtual
CThemeClassic::~CThemeClassic(void)
{
}

// virtual
void CThemeClassic::LoadBitmaps(HMODULE hModule)
{
	m_hbmPrev = LoadBitmap(hModule, MAKEINTRESOURCE(IDB_PREV_CLASSIC));
	m_hbmNext = LoadBitmap(hModule, MAKEINTRESOURCE(IDB_NEXT_CLASSIC));
	m_hbmSupersize = LoadBitmap(hModule, MAKEINTRESOURCE(IDB_SUPERSIZE_CLASSIC));
}

// virtual 
bool CThemeClassic::IsAvailable()
{
	// classic theme is always available
	return true;
}

// if DWM composition is enabled, then we assume it is in use by all windows
// virtual 
bool CThemeClassic::IsInUse(HWND hWndFrame)
{
	// always in use if asked
	return true;
}

// virtual 
bool CThemeClassic::ReInit(struct LayoutMetrics* pLayoutMetrics, HWND hWndFrame)
{
	// TODO: must find a better way of doing this
	int nStdButtonSize;

	//DWORD dwStyle = GetWindowLong(hWndFrame, GWL_STYLE);
	DWORD dwExStyle = GetWindowLong(hWndFrame, GWL_EXSTYLE);

	//int nButtonWidth = GetSystemMetrics(SM_CXSIZE);
	//int nButtonHeight = GetSystemMetrics(SM_CYSIZE);
	if (dwExStyle & WS_EX_TOOLWINDOW)
	{
		nStdButtonSize = GetSystemMetrics(SM_CYSMSIZE);
	}
	else
	{
		nStdButtonSize = GetSystemMetrics(SM_CYSIZE);
	}

	//TODO: this is a temprary fudge
	pLayoutMetrics->m_nButtonWidth = nStdButtonSize - 2;
	pLayoutMetrics->m_nButtonHeight = nStdButtonSize - 4;

	// TODO: hack while testing
	//pLayoutMetrics->m_nButtonWidth = nButtonWidth;
	//pLayoutMetrics->m_nButtonHeight = nButtonHeight;

	wchar_t szMsg[256];
	wsprintf(szMsg, L"nStdButtonSize: %d Width: %d Height: %d\n", nStdButtonSize, pLayoutMetrics->m_nButtonWidth, pLayoutMetrics->m_nButtonHeight);
	OutputDebugString(szMsg);


	pLayoutMetrics->m_nLeftBorder = LEFT_BORDER;
	pLayoutMetrics->m_nRightBorder = RIGHT_BORDER;
	pLayoutMetrics->m_nTopBorder = TOP_BORDER;
	pLayoutMetrics->m_nBottomBorder = BOTTOM_BORDER;
	pLayoutMetrics->m_nSpacing = BUTTON_SPACING;

	return true;
}

#define IS_TITLE_BUTTON_VISIBLE(dw) (dw & (STATE_SYSTEM_INVISIBLE | STATE_SYSTEM_OFFSCREEN | STATE_SYSTEM_UNAVAILABLE)) == 0

// virtual 
SIZE CThemeClassic::CalcBarOffsets(HWND hWndFrame)
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

	// Classic theme is supported on XP, where there is no WM_GETTITLRBARINFOEX

	//adjust for the border
	if (dwStyle & WS_THICKFRAME)
	{
		nRightPos -= GetSystemMetrics(SM_CXSIZEFRAME);
		nTopPos += GetSystemMetrics(SM_CYSIZEFRAME);
	}
	else
	{
		nRightPos -= GetSystemMetrics(SM_CXFIXEDFRAME);
		nTopPos += GetSystemMetrics(SM_CYFIXEDFRAME);
	}

	// TODO
	// hack: move 2 pixels down to center 
	nTopPos += 2;

	// adjust for any titlebar 
	TITLEBARINFO titleBarInfo;
	titleBarInfo.cbSize = sizeof(titleBarInfo);
	GetTitleBarInfo(hWndFrame, &titleBarInfo);
	// TODO: are there defines for the child indexes?
	//if (dwStyle & WS_SYSMENU)
	if (IS_TITLE_BUTTON_VISIBLE(titleBarInfo.rgstate[5]))
	{
		nRightPos -= (nStdButtonSize + nStdSpacing);
	}

	// Alt: could use titleBarInfo for these as well
	if (dwStyle & (WS_MINIMIZEBOX | WS_MAXIMIZEBOX))
	{
		nRightPos -= (nStdButtonSize + nStdSpacing) * 2;
	}
	else if (dwExStyle & WS_EX_CONTEXTHELP)
	{
		nRightPos -= (nStdButtonSize + nStdSpacing);
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
void CThemeClassic::PrepareFloatBar(HWND hWndFloatBar)
{
}

static DWORD* m_pdwBits = NULL;
static int m_nWidth;
static int m_nHeight;
static RECT m_RectBar;

// virtual 
void CThemeClassic::PaintStart(HDC hDC, RECT rectBar)
{
	m_RectBar = rectBar;

	m_nWidth = rectBar.right - rectBar.left;
	m_nHeight = rectBar.bottom - rectBar.top;
	m_pdwBits = new DWORD[m_nWidth * m_nHeight];
}

// virtual 
void CThemeClassic::PaintBarBackground(HDC hDC, RECT rectBar)
{
}

#define TO_ARGB(rgb) (((rgb & 0xFF) << 16) | ((rgb & 0xFF0000) >> 16) | (rgb & 0xFF00) | 0xFF000000)

#define FLIP_Y(y) (m_nHeight - (y))

#define BM_INDEX(x, y) ((m_nHeight - (y) - 1) * m_nWidth + (x))

// virtual 
void CThemeClassic::PaintButtonFace(HDC hDC, RECT rectButton, int index)
{
	HBITMAP hbmImage = GetImage(index);
	int x;
	int y;

	DWORD dwLight = GetSysColor(COLOR_3DHILIGHT);
	dwLight = TO_ARGB(dwLight);
	//DWORD dwFace = GetSysColor(COLOR_3DFACE);
	DWORD dwFace = GetSysColor(COLOR_3DFACE);
	dwFace = TO_ARGB(dwFace);
	DWORD dwShadow = GetSysColor(COLOR_3DSHADOW);
	dwShadow = TO_ARGB(dwShadow);
	DWORD dwDkShadow = GetSysColor(COLOR_3DDKSHADOW);
	dwDkShadow = TO_ARGB(dwDkShadow);

	for (x = rectButton.left; x < rectButton.right - 1; x++)
	{
		m_pdwBits[BM_INDEX(x, 0)] = dwLight;
	}
	m_pdwBits[BM_INDEX(rectButton.right - 1, 0)] = dwDkShadow;
	for (y = rectButton.top + 1; y < rectButton.bottom - 2; y++)
	{
		m_pdwBits[BM_INDEX(rectButton.left, y)] = dwLight;
		for (x = rectButton.left + 1; x < rectButton.right - 2; x++)
		{
			m_pdwBits[BM_INDEX(x, y)] = dwFace;
		}
		m_pdwBits[BM_INDEX(rectButton.right - 2, y)] = dwShadow;
		m_pdwBits[BM_INDEX(rectButton.right - 1, y)] = dwDkShadow;
	}

	m_pdwBits[BM_INDEX(rectButton.left, m_nHeight - 2)] = dwLight;
	m_pdwBits[BM_INDEX(rectButton.left, m_nHeight - 1)] = dwDkShadow;
	for (x = rectButton.left + 1; x < rectButton.right - 1; x++)
	{
		m_pdwBits[BM_INDEX(x, m_nHeight - 2)] = dwShadow;
		m_pdwBits[BM_INDEX(x, m_nHeight - 1)] = dwDkShadow;
	}
	m_pdwBits[BM_INDEX(rectButton.right - 1, m_nHeight - 2)] = dwDkShadow;
	m_pdwBits[BM_INDEX(rectButton.right - 1, m_nHeight - 1)] = dwDkShadow;

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
			// +1 to move image up as 3D outline takes 1 line at top and 2 at bottom 
			// and remember this y increases as you move up
			int yOffset = rectButton.top + (rectButton.bottom - rectButton.top - bm.bmHeight + 1) / 2;
			for (int y = 0; y < bm.bmHeight; y++)
			{
				for (int x = 0; x < bm.bmWidth; x++)
				{
					DWORD dwRGB = pBits[y * bm.bmWidth + x];
					// green is transparent
					if (dwRGB != 0x00FF00)
					{
						//DWORD dwARGB = 0xFF000000 | dwRGB;	// TODO: ordering
						m_pdwBits[(y + yOffset) * m_nWidth + x + xOffset] = dwRGB;
					}
				}
			}
		}
		delete [] pBits;

		//SelectObject(hDC, hbmOld);
	}
}

// called to add spacing after the button
// virtual 
void CThemeClassic::PaintButtonSpacing(HDC hDC, RECT rectButton)
{
	// no spacing between buttons
}

// virtual 
void CThemeClassic::PaintBarBorder(HDC hDC, RECT rectBar)
{
	// no border
}

// virtual 
void CThemeClassic::PaintEnd(HDC hDC)
{
	if (m_pdwBits)
	{
		// create a bitmap 

		BITMAPINFO bmi;
		memset(&bmi, 0, sizeof(bmi));
		bmi.bmiHeader.biSize = sizeof(bmi.bmiHeader);
		bmi.bmiHeader.biWidth = m_nWidth;
		bmi.bmiHeader.biHeight = m_nHeight;
		bmi.bmiHeader.biPlanes = 1;
		bmi.bmiHeader.biBitCount = 32;
		bmi.bmiHeader.biCompression = BI_RGB;
		bmi.bmiHeader.biSizeImage = 0;

		//BITMAP bm;
		//memeset(&bm, 0, sizeof(bm));

		//HBITMAP hbmBar = CreateBitmap (m_nWidth, m_nHeight, 1, 32, m_pdwBits);
		HBITMAP hbmBar = CreateCompatibleBitmap (hDC, m_nWidth, m_nHeight);
		SetDIBits(hDC, hbmBar, 0, m_nHeight, m_pdwBits, &bmi, DIB_RGB_COLORS);

		HDC hDCMem = CreateCompatibleDC(hDC);

		HBITMAP hbmOld = (HBITMAP)SelectObject(hDCMem, hbmBar);

		BitBlt(hDC, 0, 0, m_nWidth, m_nHeight, hDCMem, 0, 0, SRCCOPY);
		SelectObject(hDCMem, hbmOld);
		DeleteDC(hDCMem);
	}

	delete [] m_pdwBits;
	m_pdwBits = NULL;
}
