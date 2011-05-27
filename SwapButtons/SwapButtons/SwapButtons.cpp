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

// SwapButtons.cpp : Defines the entry point for the application.
//

#include "stdafx.h"
#include "SwapButtons.h"
#include "../SwapButtonsDll/FloatBar.h"

#include "../SwapButtonsDll/SwapButtonsDll.h"

#pragma comment(linker,"\"/manifestdependency:type='win32' name='Microsoft.Windows.Common-Controls' version='6.0.0.0' processorArchitecture='*' publicKeyToken='6595b64144ccf1df' language='*'\"")


#define MAX_LOADSTRING 100

// Global Variables:
HINSTANCE ghInst;								// current instance
TCHAR gszTitle[MAX_LOADSTRING];					// The title bar text


// START LOCAL TESTING - only needed for local mode testing (not hooking into all applications) 
HMODULE ghModuleBitmaps = NULL;
CFloatBar* gpFloatBar = NULL;
// our window message to force the FloatBar to reload the configuration info - typically after the gui has changed it
UINT gwm_reinit = 0;
// our window message to force the FloatBar to unhook itself and close
UINT gwm_unload = 0;
// END LOCAL TESTING

// Forward declarations of functions included in this code module:
bool CheckIfAlreadyRunning(HANDLE* phMutex);
ATOM				MyRegisterClass(LPCTSTR pszWindowClass);
bool RegisterMessages();
int				InitInstance(int);
LRESULT CALLBACK	WndProc(HWND, UINT, WPARAM, LPARAM);
void				SetLocalMode(HWND hWnd);
INT_PTR CALLBACK	About(HWND, UINT, WPARAM, LPARAM);

int APIENTRY _tWinMain(HINSTANCE hInstance,
                     HINSTANCE hPrevInstance,
                     LPTSTR    lpCmdLine,
                     int       nCmdShow)
{
	UNREFERENCED_PARAMETER(hPrevInstance);
	UNREFERENCED_PARAMETER(lpCmdLine);

 	// TODO: Place code here.
	MSG msg;
	HACCEL hAccelTable;
	int err;

	// setup the global variables to save passing them around 
	ghInst = hInstance;
	LoadString(hInstance, IDS_APP_TITLE, gszTitle, MAX_LOADSTRING);

	// check we are not already running
	HANDLE hMutex;
	if (CheckIfAlreadyRunning(&hMutex))
	{
		return 1;
	}

	// Perform application initialization:
	err = InitInstance (nCmdShow);
	if (err)
	{
		return err;
	}

	hAccelTable = LoadAccelerators(hInstance, MAKEINTRESOURCE(IDC_SWAPBUTTONS));

	// Main message loop:
	while (GetMessage(&msg, NULL, 0, 0))
	{
		if (!TranslateAccelerator(msg.hwnd, hAccelTable, &msg))
		{
			TranslateMessage(&msg);
			DispatchMessage(&msg);
		}
	}

	// this will happen when we exit anyway, but it is nicer to be explicit
	CloseHandle(hMutex);

	return (int) msg.wParam;
}


bool CheckIfAlreadyRunning(HANDLE* phMutex)
{
#ifdef _WIN64
	TCHAR* pszMutexName = L"GNE_DMT_SwapButtons_64";
#else
	TCHAR* pszMutexName = _T("GNE_DMT_SwapButtons_32");
#endif
	*phMutex = CreateMutex(NULL, FALSE, pszMutexName);
	if (GetLastError() == ERROR_ALREADY_EXISTS || GetLastError() == ERROR_ACCESS_DENIED)
	{
		MessageBox(NULL, L"SwapButtons is already running", pszMutexName, MB_OK | MB_ICONWARNING);
		return true;
	}

	return false;
}

//
//   FUNCTION: InitInstance(HINSTANCE, int)
//
//   PURPOSE: Saves instance handle and creates main window
//
//   COMMENTS:
//
//        In this function, we save the instance handle in a global variable and
//        create and display the main program window.
//
int InitInstance(int nCmdShow)
{
	HWND hWnd;

	TCHAR szWindowClass[MAX_LOADSTRING];			// the main window class name
	LoadString(ghInst, IDC_SWAPBUTTONS, szWindowClass, MAX_LOADSTRING);

   	MyRegisterClass(szWindowClass);

	if (!RegisterMessages())
	{
		return 3;
	}

	DWORD dwExStyle = 0;
	// for local testing of tool windows
	//dwExStyle |= WS_EX_TOOLWINDOW;
	hWnd = CreateWindowEx(dwExStyle, szWindowClass, gszTitle, WS_OVERLAPPEDWINDOW,
      CW_USEDEFAULT, 0, CW_USEDEFAULT, 0, NULL, NULL, ghInst, NULL);

	if (!hWnd)
	{
		MessageBox(NULL, _T("Failed to create window"), gszTitle, MB_OK | MB_ICONERROR);
		return 4;
	}

	ShowWindow(hWnd, nCmdShow);
	UpdateWindow(hWnd);

	return 0;
}

//
//  FUNCTION: MyRegisterClass()
//
//  PURPOSE: Registers the window class.
//
ATOM MyRegisterClass(LPCTSTR pszWindowClass)
{

	WNDCLASSEX wcex;
	wcex.cbSize = sizeof(WNDCLASSEX);

	wcex.style			= CS_HREDRAW | CS_VREDRAW;
	wcex.lpfnWndProc	= WndProc;
	wcex.cbClsExtra		= 0;
	wcex.cbWndExtra		= 0;
	wcex.hInstance		= ghInst;
	wcex.hIcon			= LoadIcon(ghInst, MAKEINTRESOURCE(IDI_SWAPBUTTONS));
	wcex.hCursor		= LoadCursor(NULL, IDC_ARROW);
	wcex.hbrBackground	= (HBRUSH)(COLOR_WINDOW+1);
	wcex.lpszMenuName	= MAKEINTRESOURCE(IDC_SWAPBUTTONS);
	wcex.lpszClassName	= pszWindowClass;
	wcex.hIconSm		= LoadIcon(wcex.hInstance, MAKEINTRESOURCE(IDI_SMALL));

	return RegisterClassEx(&wcex);
}

bool RegisterMessages()
{
	gwm_reinit = RegisterWindowMessage(_T("GNE_DMT_REINIT"));
	gwm_unload = RegisterWindowMessage(_T("GNE_DMT_UNLOAD"));

	if (!gwm_reinit)
	{
		MessageBox(NULL, _T("Failed to register reinit message"), gszTitle, MB_OK | MB_ICONERROR);
		return false;
	}
	else if (!gwm_unload)
	{
		MessageBox(NULL, _T("Failed to register reinit message"), gszTitle, MB_OK | MB_ICONERROR);
		return false;
	}

	return true;
}

//
//  FUNCTION: WndProc(HWND, UINT, WPARAM, LPARAM)
//
//  PURPOSE:  Processes messages for the main window.
//
//  WM_COMMAND	- process the application menu
//  WM_PAINT	- Paint the main window
//  WM_DESTROY	- post a quit message and return
//
//
LRESULT CALLBACK WndProc(HWND hWnd, UINT message, WPARAM wParam, LPARAM lParam)
{
	int wmId, wmEvent;
	PAINTSTRUCT ps;
	HDC hdc;
	LRESULT lResult;

	switch (message)
	{
	case WM_CREATE:
		break;
	case WM_COMMAND:
		wmId    = LOWORD(wParam);
		wmEvent = HIWORD(wParam);
		// Parse the menu selections:
		switch (wmId)
		{
		case IDM_LOCAL:
			SetLocalMode(hWnd);
			break;
		case IDM_HOOK:
			Hook();
			break;
		case IDM_UNHOOK:
			UnHook();
			break;
		case IDM_REINIT:
			if (gwm_reinit)
			{
				DWORD dwRecipients = BSM_APPLICATIONS;
				// TODO: not sure about BSF_POSTMESSAGE
				BroadcastSystemMessage(BSF_POSTMESSAGE, &dwRecipients, gwm_reinit, 0, 0);
			}
			break;
		case IDM_ABOUT:
			DialogBox(ghInst, MAKEINTRESOURCE(IDD_ABOUTBOX), hWnd, About);
			break;
		case IDM_EXIT:
			DestroyWindow(hWnd);
			break;
		default:
			return DefWindowProc(hWnd, message, wParam, lParam);
		}
		break;
	case WM_PAINT:
		hdc = BeginPaint(hWnd, &ps);
		// TODO: Add any drawing code here...
		EndPaint(hWnd, &ps);
		break;

	//case WM_MOVE:
	//	lResult = DefWindowProc(hWnd, message, wParam, lParam);
	//	pFloatBar->AdjustToParent();
	//	return lResult;
	//	//break;
	//case WM_SIZE:
	//	lResult = DefWindowProc(hWnd, message, wParam, lParam);
	//	pFloatBar->AdjustToParent();
	//	return lResult;

	case WM_ACTIVATE:
		if (gpFloatBar)
		{
			gpFloatBar->ShowActiveState(LOWORD(wParam) ? true : false);
		}
		return 0;

	case WM_WINDOWPOSCHANGED:
		lResult = DefWindowProc(hWnd, message, wParam, lParam);
		if (gpFloatBar)
		{
			//gpFloatBar->AdjustBarToParent();
			gpFloatBar->UpdateBarWindow(hWnd, ghInst);
		}
		return lResult;

	case WM_DESTROY:
		PostQuitMessage(0);
		break;
	default:
		if (message == gwm_reinit)
		{
			OutputDebugString(L"gwm_reinit\n");
			// it may make more sense to catch this message directly in the FloatBar?
			if (gpFloatBar)
			{
				gpFloatBar->ReInit();
			}
		}
		else if (message == gwm_unload)
		{
		}
		return DefWindowProc(hWnd, message, wParam, lParam);
	}
	return 0;
}

// to simplify testing only
void SetLocalMode(HWND hWnd)
{
	if (!gpFloatBar)
	{
		ghModuleBitmaps = LoadLibrary(L"SwapButtonsDll.dll");
		CFloatBar::ProcInit(ghInst);
		//DWORD dwButtons = 0x00000CBA;
		gpFloatBar = new CFloatBar(ghModuleBitmaps, hWnd);
	}
}

// Message handler for about box.
INT_PTR CALLBACK About(HWND hDlg, UINT message, WPARAM wParam, LPARAM lParam)
{
	UNREFERENCED_PARAMETER(lParam);
	switch (message)
	{
	case WM_INITDIALOG:
		return (INT_PTR)TRUE;

	case WM_COMMAND:
		if (LOWORD(wParam) == IDOK || LOWORD(wParam) == IDCANCEL)
		{
			EndDialog(hDlg, LOWORD(wParam));
			return (INT_PTR)TRUE;
		}
		break;
	}
	return (INT_PTR)FALSE;
}
