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
#include "SwapButtonsHook.h"
//#include "FloatInfo.h"
#include "FloatBar.h"


// data specific to this instance of the dll
HMODULE ghModule = 0;

#define MAX_WINDOWS_PER_PROC	16
HWND ghWndFrames[MAX_WINDOWS_PER_PROC];
int gnFrameCount = 0;


// data shared by all instances of the dll
#pragma data_seg("shared")
#pragma comment(linker, "/section:shared,rws")
HHOOK ghHook = 0;
ATOM gAtomFloatInfo = 0;
#pragma data_seg()

// forward declarations
__declspec(dllexport) LRESULT CALLBACK HookProc(int nCode, WPARAM wParam, LPARAM lParam);

void CheckIsSubClassed(HWND hWnd);
static void EndSubClass(HWND hWnd, CFloatBar* pFloatBar);
static void RemoveHWnd(HWND hWnd);
static int FindHWnd(HWND hWnd);
static WNDPROC SubClassWindow(HWND hWnd, WNDPROC WndProc);
static LRESULT CallWndProc(WNDPROC WndProc, HWND hWnd, UINT nMsg, WPARAM wParam, LPARAM lParam);

static LRESULT CALLBACK MyWndProc(HWND hWnd, UINT nMsg, WPARAM wParam, LPARAM lParam);


// called by gui
DWORD HookStart()
{
	//ghShellHook = SetWindowsHookEx(WH_SHELL, ShellProc, ghModule, 0);
	ghHook = SetWindowsHookEx(WH_CBT, HookProc, ghModule, 0);
	
	if (ghHook == NULL)
	{
		OutputDebugString(L"Hook failed\n");
		return GetLastError();
	}
	else
	{
		OutputDebugString(L"Hooked\n");
		return 0;
	}
}


// called by gui
DWORD HookEnd()
{
	DWORD err = 0;

	if (ghHook)
	{
		// TODO: this will unload the DLL from all processes,
		// so better make sure wehaven't got any windows sub-classed
		if (!UnhookWindowsHookEx(ghHook))
		{
			OutputDebugString(L"UnHook failed\n");
			err = GetLastError();
		}
		// assume it has been unhooked
		ghHook = 0;
	}
	OutputDebugString(L"UnHooked\n");

	return err;
}


DWORD HookProcessAttach(HMODULE hModule)
{
	wchar_t szMsg[1025];
	wchar_t szDllFnm[MAX_PATH];

	ghModule = hModule;
	gAtomFloatInfo = GlobalAddAtom(L"GNE_SwapButtons");

	wsprintf(szMsg, L"gAtomFloatInfo=0x%x\n", gAtomFloatInfo);
	OutputDebugString(szMsg);

	// explicitly load the dll in case the gui exits (or calls UnhookWindowsHookEx())
	GetModuleFileName(hModule, szDllFnm, MAX_PATH);
	LoadLibrary(szDllFnm);

	CFloatBar::ProcInit(ghModule);

	return 0;
}

DWORD HookProcessDetach()
{
	DWORD err = 0;

#ifdef RE_ENABLE_THIS_AT_SOME_POINT

	GlobalDeleteAtom(gAtomFloatInfo);
	//gAtomFloatInfo = 0; - in shared section!
#endif

	return err;

}


__declspec(dllexport) LRESULT CALLBACK HookProc(int nCode, WPARAM wParam, LPARAM lParam)
{
	//wchar_t szMsg[1025];
	//wsprintf(szMsg, L"nCode=%d\n", nCode);
	//OutputDebugString(szMsg);
	//if (nCode == HSHELL_WINDOWCREATED)

	switch (nCode)
	{
	case HCBT_CREATEWND:
		break;
	case HCBT_ACTIVATE:
		OutputDebugString(L"HCBT_ACTIVATE\n");
		CheckIsSubClassed((HWND)wParam);
		break;
	case HCBT_DESTROYWND:
		// no need to handle as we will get WM_NCDESTROY in our sub-classed windows
		break;
	default:
		break;
	}

	return CallNextHookEx(ghHook, nCode, wParam, lParam);
}

// static
void CheckIsSubClassed(HWND hWndFrame)
{
	int nFrame;

	// we are only interested in frame windows
	LONG lStyle = GetWindowLong(hWndFrame, GWL_STYLE);

	// must have a caption bar
	if (!(lStyle & WS_CAPTION))
	{
		OutputDebugString(L"non-Caption window\n");
		return;
	}
	OutputDebugString(L"Caption window found\n");

	// EnterCriticalSection

	// check we haven't already subclassed this window
	nFrame = FindHWnd(hWndFrame);

	// nFrame will be negative if hWnd not found
	if (nFrame < 0 && gnFrameCount < MAX_WINDOWS_PER_PROC)
	{
		OutputDebugString(L"adding hWnd\n");
		// not subclassed this window and we have room to add it
#ifdef OLD_CODE
		// TODO: can the app change the default heap while running? - if so, we need to remember the heap
		struct FloatInfo* pFloatInfo = (struct FloatInfo*)HeapAlloc(GetProcessHeap(), 0, sizeof(struct FloatInfo));
		if (pFloatInfo)
		{
			OutputDebugString(L"allocated FloatInfo\n");
			// add the window
			ghWndFrames[gnFrameCount++] = hWndFrame;

			// initialise the FloatInfo
			memset(pFloatInfo, 0, sizeof(struct FloatInfo));
			pFloatInfo->hWndFrame = hWndFrame;

			// save FloatInfo with the (frame) hWnd
			SetProp(hWndFrame, (LPCWSTR)gAtomFloatInfo, (HANDLE)pFloatInfo);

			// subclass only when everything else is in place
			pFloatInfo->OldWndProc = SubClassWindow(hWndFrame, MyWndProc);

		}
#endif
		// TODO: use CFloatBar::CreateFloatBar() to handle exceptions
		DWORD dwButtons = 0x00000CBA;
		CFloatBar* pFloatBar = new CFloatBar(ghModule, hWndFrame, dwButtons);
		if (pFloatBar)
		{
			OutputDebugString(L"allocated FloatBar\n");
			// add the window
			ghWndFrames[gnFrameCount++] = hWndFrame;

			// save FloatBar with hWndFrame
			SetProp(hWndFrame, (LPCWSTR)gAtomFloatInfo, (HANDLE)pFloatBar);

			// subclass only when everything else is in place
			pFloatBar->SetOldWndProc(SubClassWindow(hWndFrame, MyWndProc));
		}

	}

	// LeaveCriticalSection
}

// TOFO: should be in CFloatBar
static LRESULT CALLBACK MyWndProc(HWND hWnd, UINT nMsg, WPARAM wParam, LPARAM lParam)
{
	wchar_t szMsg[256];
	CFloatBar* pFloatBar = (CFloatBar*)GetProp(hWnd, (LPCWSTR)gAtomFloatInfo);
	// if pFloatBar is null - problems!!
	if (pFloatBar)
	{
		// get OldWndProc now as we will free pFloatInfo in WM_DESTROY
		WNDPROC OldWndProc = pFloatBar->GetOldWndProc();

		switch (nMsg)
		{
		case WM_SHOWWINDOW:
			pFloatBar->UpdateBarWindow(hWnd, ghModule);
			break;

		case WM_ACTIVATE:
			pFloatBar->ShowActiveState(LOWORD(wParam) ? true : false);
			break;

		case WM_WINDOWPOSCHANGED:
			OutputDebugString(L"WM_WINDOWPOSCHANGED\n");
			pFloatBar->UpdateBarWindow(hWnd, ghModule);
			break;

		case WM_DESTROY:
			// makes sense to end the subclassing here than in WM_NCDESTROY
			// to stop the FloatBars from lingering
			EndSubClass(hWnd, pFloatBar);
			break;

		//case WM_NCDESTROY:
		//	// this should be the last message for this window
		//	EndSubClass(hWnd, pFloatInfo);
		//	break;

		default:
			break;
		}

		return CallWndProc(OldWndProc, hWnd, nMsg, wParam, lParam);
	}
	else
	{
		// if we've lost the FloatInfo, we don't know what the old WndProc was, so can't call it!
		wsprintf(szMsg, L"MyWndProc: hWnd: 0x%x, nMsg: %d\n", hWnd, nMsg);
		OutputDebugString(szMsg);
		return 0;
	}
}

static void EndSubClass(HWND hWnd, CFloatBar* pFloatBar)
{
	// restore the original wndproc
	WNDPROC OldWndProc = pFloatBar->GetOldWndProc();
	if (OldWndProc)
	{
		SubClassWindow(hWnd, OldWndProc);
	}

	// delete the FloatBar
	HWND hWndFloatBar = pFloatBar->GetHWndFloatBar();
	if (hWndFloatBar)
	{
		DestroyWindow(hWndFloatBar);
		pFloatBar->SetHWndFloatBar(0);
	}

	// release the memory used for FloatInfo
	delete pFloatBar;

	// and remove this hWnd from the map for this process
	RemoveHWnd(hWnd);
}

static void RemoveHWnd(HWND hWnd)
{
	// EnterCriticalSection

	int nFrame = FindHWnd(hWnd);
	if (nFrame >= 0)
	{
		ghWndFrames[nFrame] = ghWndFrames[--gnFrameCount];
	}

	// LeaveCriticalSection

}

// the caller is responsible for making sure this code is not re-entrable
static int FindHWnd(HWND hWnd)
{
	int nFrame;

	for (nFrame = 0; nFrame < gnFrameCount; nFrame++)
	{
		if (ghWndFrames[nFrame] == hWnd)
		{
			// found it
			return nFrame;
		}
	}

	// not found
	return -1;
}

static WNDPROC SubClassWindow(HWND hWnd, WNDPROC WndProc)
{
	if (IsWindowUnicode(hWnd))
	{
		return (WNDPROC)SetWindowLongPtrW(hWnd, GWLP_WNDPROC, (LONG_PTR)WndProc);
	}
	else
	{
		return (WNDPROC)SetWindowLongPtrA(hWnd, GWLP_WNDPROC, (LONG_PTR)WndProc);
	}
}

static LRESULT CallWndProc(WNDPROC WndProc, HWND hWnd, UINT nMsg, WPARAM wParam, LPARAM lParam)
{
	if (IsWindowUnicode(hWnd))
	{
		return CallWindowProcW(WndProc, hWnd, nMsg, wParam, lParam);
	}
	else
	{
		return CallWindowProcA(WndProc, hWnd, nMsg, wParam, lParam);
	}
}
