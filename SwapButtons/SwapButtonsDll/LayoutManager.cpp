#include "StdAfx.h"
#include "LayoutManager.h"
#include "ButtonList.h"

#define IS_TITLE_BUTTON_VISIBLE(dw) (dw & (STATE_SYSTEM_INVISIBLE | STATE_SYSTEM_OFFSCREEN | STATE_SYSTEM_UNAVAILABLE)) == 0

CLayoutManager::CLayoutManager(void)
	: m_pCurTheme(NULL)
{
}


CLayoutManager::~CLayoutManager(void)
{
}

void CLayoutManager::ReInit(HWND hWndFrame)
{
	if (m_ThemeDwm.IsAvailable() && m_ThemeDwm.IsInUse(hWndFrame))
	{
		// use the DWM
		m_pCurTheme = &m_ThemeDwm;
		m_pCurTheme->ReInit(&m_LayoutMetrics, hWndFrame);
	}
	else
	{
		// use classic theme
		OutputDebugString(L"No theme available\n");
	}
}

void CLayoutManager::PrepareFloatBar(HWND hWndFloatBar)
{
	if (m_pCurTheme)
	{
		m_pCurTheme->PrepareFloatBar(hWndFloatBar);
	}
}

RECT CLayoutManager::GetBarRect(HWND hWndFrame, const CButtonList& buttonList)
{
	RECT rectFloatBar;
	RECT rectFrame;

	// get frame rect in screen co-ords
	GetWindowRect(hWndFrame, &rectFrame);

	// get TRHC to place the bar at
	SIZE offsets = CalcBarOffsets(hWndFrame);

	SIZE size = GetBarSize(buttonList);

	rectFloatBar.right = rectFrame.right - offsets.cx;
	rectFloatBar.left = rectFloatBar.right - size.cx;
	rectFloatBar.top = rectFrame.top + offsets.cy;
	rectFloatBar.bottom = rectFloatBar.top + size.cy;

	return rectFloatBar;
}

// private
SIZE CLayoutManager::CalcBarOffsets(HWND hWndFrame)
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

	if (IsPreVista())
	{
		// pre-Vista so no WM_GETTITLRBARINFOEX
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
	}
	else
	{
		// NOTE: DWMWA_CAPTION_BUTTON_BOUNDS may also be of use?

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

		// TODO: temp hack - this needs to go into theme
		//nTopPos += 1;
		nRightPos -= nStdButtonSize / 2; // to allow spacing between the button sets
	}

	SIZE offsets;
	offsets.cx = rectFrame.right - nRightPos;
	offsets.cy = nTopPos - rectFrame.top;
	return offsets;
}

// private static
bool CLayoutManager::IsPreVista()
{
	return false;
}

SIZE CLayoutManager::GetBarSize(const CButtonList& buttonList)
{
	SIZE size = {0, 0};
	int nButtons = buttonList.Count();
	
	if (nButtons)
	{
		// first add the borders
		size.cx = m_LayoutMetrics.m_nLeftBorder + m_LayoutMetrics.m_nRightBorder;
		size.cy = m_LayoutMetrics.m_nTopBorder + m_LayoutMetrics.m_nBottomBorder;

		// always one button high
		size.cy += m_LayoutMetrics.m_nButtonHeight;

		// add the button widths
		size.cx += m_LayoutMetrics.m_nButtonWidth * nButtons;
		// add the space between buttons
		size.cx += m_LayoutMetrics.m_nSpacing * (nButtons - 1);
	}

	return size;
}

void CLayoutManager::PaintBar(HWND hWndFloatBar, const CButtonList& buttonList)
{
	if (m_pCurTheme)
	{
		PAINTSTRUCT ps;

		HDC hDC = BeginPaint(hWndFloatBar, &ps);
		RECT rectBar;
		RECT rectButton;
		GetClientRect(hWndFloatBar, &rectBar);

		m_pCurTheme->PaintStart(hDC, rectBar);

		m_pCurTheme->PaintBarBackground(hDC, rectBar);

		// the pen to draw border around buttons
		HPEN hPen = CreatePen(PS_SOLID, 1, RGB(128, 128, 128));
		HPEN hOldPen = (HPEN)SelectObject(hDC, hPen);


		int x = rectBar.left + m_LayoutMetrics.m_nLeftBorder;
		int y = rectBar.top + m_LayoutMetrics.m_nTopBorder;
		int index;
		int count = buttonList.Count();
		for (index = 0; index < count; index++)
		{

			rectButton.left = x;
			rectButton.right = rectButton.left + m_LayoutMetrics.m_nButtonWidth;
			rectButton.top = y;
			rectButton.bottom = rectButton.top + m_LayoutMetrics.m_nButtonHeight;

			HBITMAP hbmImage;
			HBITMAP hbmMask;
			if (buttonList.GetGlyph(index, &hbmImage, &hbmMask))
			{
				m_pCurTheme->PaintButtonFace(hDC, rectButton, hbmImage, hbmMask);
			}

			if (index  < count - 1)
			{
				// paint spacing between buttons
				m_pCurTheme->PaintButtonSpacing(hDC, rectButton);

				x += m_LayoutMetrics.m_nSpacing;
			}
			x += m_LayoutMetrics.m_nButtonWidth;
		}

		m_pCurTheme->PaintBarBorder(hDC, rectBar);

		m_pCurTheme->PaintEnd(hDC);

		EndPaint(hWndFloatBar, &ps);
	}
}

int CLayoutManager::HitToIndex(int x, int y, const CButtonList& buttonList)
{

	int right = 1;
	int index;
	int count = buttonList.Count();
	for (index = 0; index < count; index++)
	{
		if (index != 0)
		{
			// add in seperator
			right += m_LayoutMetrics.m_nSpacing;
		}
		right += m_LayoutMetrics.m_nButtonWidth;

		if (x <= right + 1)
		{
			return index;
		}
	}

	return -1;
}
