#include "StdAfx.h"
#include "ThemeClassic.h"
#include "LayoutManager.h"
#include "ButtonList.h"

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
	m_nButtonWidth = nStdButtonSize - 2;
	m_nButtonHeight = nStdButtonSize - 4;

	// TODO: hack while testing
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

	return true;
}

#define IS_TITLE_BUTTON_VISIBLE(dw) (dw & (STATE_SYSTEM_INVISIBLE | STATE_SYSTEM_OFFSCREEN | STATE_SYSTEM_UNAVAILABLE)) == 0

// virtual 
//SIZE CThemeClassic::CalcBarOffsets(HWND hWndFrame)
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
//	// Classic theme is supported on XP, where there is no WM_GETTITLRBARINFOEX
//
//	//adjust for the border
//	if (dwStyle & WS_THICKFRAME)
//	{
//		nRightPos -= GetSystemMetrics(SM_CXSIZEFRAME);
//		nTopPos += GetSystemMetrics(SM_CYSIZEFRAME);
//	}
//	else
//	{
//		nRightPos -= GetSystemMetrics(SM_CXFIXEDFRAME);
//		nTopPos += GetSystemMetrics(SM_CYFIXEDFRAME);
//	}
//
//	// TODO
//	// hack: move 2 pixels down to center 
//	nTopPos += 2;
//
//	// adjust for any titlebar 
//	TITLEBARINFO titleBarInfo;
//	titleBarInfo.cbSize = sizeof(titleBarInfo);
//	GetTitleBarInfo(hWndFrame, &titleBarInfo);
//	// TODO: are there defines for the child indexes?
//	//if (dwStyle & WS_SYSMENU)
//	if (IS_TITLE_BUTTON_VISIBLE(titleBarInfo.rgstate[5]))
//	{
//		nRightPos -= (nStdButtonSize + nStdSpacing);
//	}
//
//	// Alt: could use titleBarInfo for these as well
//	if (dwStyle & (WS_MINIMIZEBOX | WS_MAXIMIZEBOX))
//	{
//		nRightPos -= (nStdButtonSize + nStdSpacing) * 2;
//	}
//	else if (dwExStyle & WS_EX_CONTEXTHELP)
//	{
//		nRightPos -= (nStdButtonSize + nStdSpacing);
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
void CThemeClassic::PrepareFloatBar(HWND hWndFloatBar)
{
}

//static DWORD* m_pdwBits = NULL;
//static int m_nWidth;
//static int m_nHeight;
//static RECT m_RectBar;

// virtual 
void CThemeClassic::PaintBar(HWND hWndFloatBar, HWND hWndFrame, HDC hDC, const CButtonList& buttonList, RECT rectBar, bool bActive)
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


#define TO_ARGB(rgb) (((rgb & 0xFF) << 16) | ((rgb & 0xFF0000) >> 16) | (rgb & 0xFF00) | 0xFF000000)

//#define FLIP_Y(y) (m_nHeight - (y))

//#define BM_INDEX(x, y) ((m_nHeight - (y) - 1) * m_nWidth + (x))

// virtual 
void CThemeClassic::PaintButtonFace(HDC hDC, RECT rectButton, EFloatButton button)
{
	HBITMAP hbmImage = GetImage(button);

	DWORD dwLight = GetSysColor(COLOR_3DHILIGHT);
	dwLight = TO_ARGB(dwLight);
	//DWORD dwFace = GetSysColor(COLOR_3DFACE);
	DWORD dwFace = GetSysColor(COLOR_3DFACE);
	dwFace = TO_ARGB(dwFace);
	DWORD dwShadow = GetSysColor(COLOR_3DSHADOW);
	dwShadow = TO_ARGB(dwShadow);
	DWORD dwDkShadow = GetSysColor(COLOR_3DDKSHADOW);
	dwDkShadow = TO_ARGB(dwDkShadow);
	DWORD dwBtnText = GetSysColor(COLOR_BTNTEXT);
	dwBtnText = TO_ARGB(dwBtnText);

	// get co-ords of TLHC and BRHC - these are all inclusive values
	// i.e. (right, bottom) is really part of the button
	int left = rectButton.left;
	int top = rectButton.top;
	int right = rectButton.right - 1;
	int bottom = rectButton.bottom - 1;

	// add highlight to top and left
	m_BitmapBuffer.DrawHLine(left, right - 1, top, dwLight);
	m_BitmapBuffer.DrawVLine(left, top, bottom -1, dwLight);

	// fill the face
	m_BitmapBuffer.Fill(left + 1, top + 1, right - 2, bottom - 2, dwFace);

	// add the shadows
	m_BitmapBuffer.DrawHLine(left + 1, right - 1, bottom - 1, dwShadow);
	m_BitmapBuffer.DrawVLine(right - 1, top + 1, bottom -1, dwShadow);

	// add the dark shadows
	m_BitmapBuffer.DrawHLine(left, right, bottom, dwDkShadow);
	m_BitmapBuffer.DrawVLine(right, top, bottom, dwDkShadow);

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
					// green is transparent
					if (dwRGB != 0x00FF00)
					{
						//DWORD dwARGB = 0xFF000000 | dwRGB;	// TODO: ordering
						//m_BitmapBuffer.Set(x + xOffset, y + yOffset, dwRGB);
						m_BitmapBuffer.Set(x + xOffset, y + yOffset, dwBtnText);
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
void CThemeClassic::PaintEnd(HDC hDC, RECT rectBar)
{
//	if (m_pdwBits)
	{
		// create a bitmap 

		int nWidth = m_BitmapBuffer.GetWidth();
		int nHeight = m_BitmapBuffer.GetHeight();

		BITMAPINFO bmi;
		memset(&bmi, 0, sizeof(bmi));
		bmi.bmiHeader.biSize = sizeof(bmi.bmiHeader);
		bmi.bmiHeader.biWidth = nWidth;
		bmi.bmiHeader.biHeight = -nHeight;
		bmi.bmiHeader.biPlanes = 1;
		bmi.bmiHeader.biBitCount = 32;
		bmi.bmiHeader.biCompression = BI_RGB;
		bmi.bmiHeader.biSizeImage = 0;

		//BITMAP bm;
		//memeset(&bm, 0, sizeof(bm));

		//HBITMAP hbmBar = CreateBitmap (m_nWidth, m_nHeight, 1, 32, m_pdwBits);
		HBITMAP hbmBar = CreateCompatibleBitmap (hDC, nWidth, nHeight);
		SetDIBits(hDC, hbmBar, 0, nHeight, m_BitmapBuffer.GetBits(), &bmi, DIB_RGB_COLORS);

		HDC hDCMem = CreateCompatibleDC(hDC);

		HBITMAP hbmOld = (HBITMAP)SelectObject(hDCMem, hbmBar);

		BitBlt(hDC, 0, 0, nWidth, nHeight, hDCMem, 0, 0, SRCCOPY);
		SelectObject(hDCMem, hbmOld);
		DeleteDC(hDCMem);
	}

//	delete [] m_pdwBits;
//	m_pdwBits = NULL;
}
