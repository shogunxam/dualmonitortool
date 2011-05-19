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
#include <Windowsx.h>
#include "FloatBar.h"
#include "FloatInfo.h"

#include "WinHelper.h"

static LRESULT CALLBACK FloatBarWndProc(HWND hWnd, UINT message, WPARAM wParam, LPARAM lParam);


#define WM_ERASEBACKGROUND 0x0014

WCHAR szFloatBarClassName[] = L"GNE_FloatBar";



// public static
void CFloatBar::ProcInit(HINSTANCE hInstance)
{
	// initialisation for FloatBar for current process
	RegisterFloatBarClass(hInstance);
}

// private static
ATOM CFloatBar::RegisterFloatBarClass(HINSTANCE hInstance)
{
	WNDCLASSEX wcex;

	wcex.cbSize = sizeof(WNDCLASSEX);

	wcex.style			= CS_HREDRAW | CS_VREDRAW;
	wcex.lpfnWndProc	= FloatBarWndProc;
	wcex.cbClsExtra		= 0;
	wcex.cbWndExtra		= sizeof(LPVOID);
	wcex.hInstance		= hInstance;
	wcex.hIcon			= NULL;
	wcex.hCursor		= LoadCursor(NULL, IDC_ARROW);
	//wcex.hbrBackground	= (HBRUSH)(COLOR_WINDOW+1);
	wcex.hbrBackground	= NULL;
	wcex.lpszMenuName	= NULL;
	wcex.lpszClassName	= szFloatBarClassName;
	wcex.hIconSm		= NULL;

	return RegisterClassEx(&wcex);
}

// static 
//CFloatBar* CFloatBar::CreateStdFloatBar(HWND hWndFrame, HINSTANCE hInstance)
//{
//	// TODO: move code from SwapButtonsHook into here to do this
//}

CFloatBar::CFloatBar(HMODULE hModule, HWND hWndFrame, DWORD dwButtons)
	: m_hWndFrame(hWndFrame),
	  m_hWndFloatBar(0),
	  m_OldWndProc(NULL),
	  m_bActive(false),
	  m_ButtonList(dwButtons)
{
//	m_pTheme = new CTheme();
//	m_pTheme->GrabThemeData(hWndFrame);
	m_LayoutManager.ReInit(hWndFrame);
	//m_ButtonList.LoadBitmaps(hModule);
	m_LayoutManager.LoadBitmaps(hModule);
}

CFloatBar::~CFloatBar()
{
//	delete m_pTheme;
}

void CFloatBar::SetOldWndProc(WNDPROC WndProc)
{
	m_OldWndProc = WndProc;
}

WNDPROC CFloatBar::GetOldWndProc() const
{
	return m_OldWndProc;
}

void CFloatBar::SetHWndFloatBar(HWND hWndFloatBar)
{
	m_hWndFloatBar = hWndFloatBar;
}

HWND CFloatBar::GetHWndFloatBar() const
{
	return m_hWndFloatBar;
}



void CFloatBar::UpdateBarWindow(HWND hWndFrame, HINSTANCE hInstance)
{
	wchar_t szMsg[256];
	wsprintf(szMsg, L"FloatBarUpdate hWnd: 0x%x hWndFloatBar: 0x%x\n", hWndFrame, m_hWndFloatBar);
	OutputDebugString(szMsg);

	if (!m_hWndFloatBar)
	{
		CreateBarWindow(hWndFrame, hInstance);
	}

	AdjustBarToParent();
}

void CFloatBar::ShowActiveState(bool bActive)
{
	if (bActive != m_bActive)
	{
		m_bActive = bActive;
		if (m_hWndFloatBar)
		{
			InvalidateRect(m_hWndFloatBar, NULL, FALSE);
		}
	}
}


void CFloatBar::CreateBarWindow(HWND hWndFrame, HINSTANCE hInstance)
{
	wchar_t szMsg[256];
	wsprintf(szMsg, L"Creating float bar for 0x%x\n", hWndFrame);
	OutputDebugString(szMsg);
	// creates a FloatBar for a particular window
	m_hWndFloatBar = CreateWindow(szFloatBarClassName, NULL, WS_CHILD | WS_CLIPSIBLINGS,
	    100, 100, 200, 200, GetDesktopWindow(), NULL, hInstance, this);

	if (m_hWndFloatBar)
	{
		wsprintf(szMsg, L"Created float bar  0x%x\n", m_hWndFloatBar);
		OutputDebugString(szMsg);

		// stop FloatBar from appearing in the task bar
		LONG ExtendedStyle = GetWindowLong(m_hWndFloatBar, GWL_EXSTYLE );
		SetWindowLong(m_hWndFloatBar, GWL_EXSTYLE, (ExtendedStyle & ~WS_EX_APPWINDOW) | WS_EX_TOOLWINDOW );

		AddStdBarButtons();

		AdjustBarToParent();

		ShowWindow(m_hWndFloatBar, SW_SHOW);

		// initialise the active flag
		m_bActive = (GetActiveWindow() == m_hWndFrame);

		UpdateWindow(m_hWndFloatBar);
	}
}

void CFloatBar::AddStdBarButtons()
{
}

void CFloatBar::AdjustBarToParent()
{
	wchar_t szMsg[256];

//	RECT rectFrame;
//	// get parent rect in screen co-ords
//	GetWindowRect(m_hWndFrame, &rectFrame);

#ifdef IS_CHILD_OF_CLIENT
	// adjust parents position to be relative to itself
	rectFrame.right -= rectFrame.left;
	rectFrame.left = 0;
	rectFrame.bottom -= rectFrame.top;
	rectFrame.top = 0;
#endif

//RectParent.top -= 30;

	// TODO: don't need to call this everytime
	//m_pTheme->CalcPositioning(m_hWndFrame);

	//SIZE size = m_pTheme->GetBarSize(m_ButtonList);
	//RECT rectFloatBar;
	//rectFloatBar.right = rectFrame.right - m_nRightOffset;
	//rectFloatBar.left = rectFloatBar.right - size.cx;
	//rectFloatBar.top = rectFrame.top + m_nTopOffset;
	//rectFloatBar.bottom = rectFloatBar.top + size.cy;
	//RECT rectFloatBar = m_pTheme->GetBarRect(m_hWndFrame, m_ButtonList);
	RECT rectFloatBar = m_LayoutManager.GetBarRect(m_hWndFrame, m_ButtonList);


	wsprintf(szMsg, L"Move-> (%d, %d), (%d, %d)\n", rectFloatBar.left, rectFloatBar.top, rectFloatBar.right, rectFloatBar.bottom);
	OutputDebugString(szMsg);

	//MoveWindow(m_hWnd, Rect.left, Rect.top, Rect.right -  Rect.left, Rect.bottom - Rect.top, FALSE);
	UINT uFlags;
	if (IsWindowVisible(m_hWndFrame))
	{
		uFlags = SWP_SHOWWINDOW;
	}
	else
	{
		uFlags = SWP_HIDEWINDOW;
	}
	BOOL ret = SetWindowPos(m_hWndFloatBar, m_hWndFrame, rectFloatBar.left, rectFloatBar.top, rectFloatBar.right - rectFloatBar.left, rectFloatBar.bottom - rectFloatBar.top, uFlags);
	//SetWindowPos(m_hWnd, HWND_TOP, Rect.left, Rect.top, Rect.right - Rect.left, Rect.bottom - Rect.top, SWP_SHOWWINDOW);
	//printf("%d\r\n", ret);

	SetActiveWindow(m_hWndFloatBar);
}



static LRESULT CALLBACK FloatBarWndProc(HWND hWnd, UINT message, WPARAM wParam, LPARAM lParam)
{
	CFloatBar* pFloatBar;
	CREATESTRUCT* pCreateStruct;
	int xPos;
	int yPos;

	switch (message)
	{
	case WM_NCCREATE:
		pCreateStruct = (CREATESTRUCT*)lParam;
		SetWindowLongPtr(hWnd, GWLP_USERDATA, (LONG_PTR)pCreateStruct->lpCreateParams);

		// We cannot call FloatBarInstance() as CreateWindow() hasn't returned yet
		pFloatBar = (CFloatBar*)pCreateStruct->lpCreateParams;
		if (pFloatBar)
		{
			pFloatBar->OnCreate(hWnd);
		}

		return DefWindowProc(hWnd, message, wParam, lParam);
	//case WM_ACTIVATE:
	//	pFloatBar = CFloatBar::FloatBarInstance(hWnd);
	//	if (pFloatBar)
	//	{
	//		pFloatBar->OnActivate();
	//	}
	//	break;
	case WM_MOVE:
		// TODO: only if using transparent mode
		InvalidateRect(hWnd, NULL, TRUE);
		break;
	case WM_ERASEBACKGROUND:
		return 1;
	case WM_PAINT:
		pFloatBar = CFloatBar::FloatBarInstance(hWnd);
		if (pFloatBar)
		{
			pFloatBar->OnPaint();
		}
		break;
	case WM_LBUTTONDOWN:
		pFloatBar = CFloatBar::FloatBarInstance(hWnd);
		if (pFloatBar)
		{
			xPos = GET_X_LPARAM(lParam);
			yPos = GET_Y_LPARAM(lParam);
			pFloatBar->OnLButtonDown(wParam, xPos, yPos);
		}
		break;
	case WM_DWMCOMPOSITIONCHANGED:
	case WM_DWMCOLORIZATIONCOLORCHANGED:
		pFloatBar = CFloatBar::FloatBarInstance(hWnd);
		if (pFloatBar)
		{
			pFloatBar->OnThemeChange();
		}
		break;
	case WM_DESTROY:
		break;
	default:
		return DefWindowProc(hWnd, message, wParam, lParam);
	}
	return 0;
}

void CFloatBar::OnCreate(HWND hWnd)
{
//	m_pTheme->Init(hWnd);
	m_LayoutManager.PrepareFloatBar(hWnd);
}

//void CFloatBar::OnActivate()
//{
//	m_pTheme->Activate(m_hWndFloatBar);
//}

void CFloatBar::OnThemeChange()
{
	m_LayoutManager.ReInit(m_hWndFrame);
	InvalidateRect(m_hWndFloatBar, NULL, FALSE);
}

void CFloatBar::OnPaint()
{
//	m_pTheme->PaintBar(m_hWndFloatBar, m_ButtonList);
	m_LayoutManager.PaintBar(m_hWndFloatBar, m_hWndFrame, m_ButtonList, m_bActive);
}

void CFloatBar::OnLButtonDown(WPARAM wParam, int x, int y)
{
	if (m_hWndFrame)
	{
		// can only move the window if it is enabled
		// (don't want to move a window if it is showing a modal dialog)
		if (IsWindowEnabled(m_hWndFrame))
		{
			SetActiveWindow(m_hWndFrame);
			//CWinHelper::MoveWindowToNext(m_hWndFrame);

			int index = m_LayoutManager.HitToIndex(x, y, m_ButtonList);
			if (index >= 0)
			{
				m_ButtonList.Click(index, m_hWndFrame);
			}
		}
	}
}

// static
CFloatBar* CFloatBar::FloatBarInstance(HWND hWndFloatBar)
{
	CFloatBar* pFloatBar = (CFloatBar*) GetWindowLongPtr(hWndFloatBar, GWLP_USERDATA);
	return pFloatBar;
}

