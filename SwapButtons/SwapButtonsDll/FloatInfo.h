#pragma once

struct FloatInfo
{
	WNDPROC OldWndProc;

	HWND hWndFrame;
	HWND hWndFloatBar;

	WORD ButtonMask;
	WORD CurButtons;
};


#define FBB_PREV		0x01
#define FBB_NEXT		0x02
#define FBB_SUPERSIZE	0x04