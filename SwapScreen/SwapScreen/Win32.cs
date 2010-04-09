#region copyright
// This file is part of Dual Monitor Tools which is a set of tools to assist
// users with multiple monitor setups.
// Copyright (C) 2009  Gerald Evans
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
#endregion

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace SwapScreen
{
	/// <summary>
	/// Definitions extracted from winuser.h
	/// </summary>
	static class Win32
	{
		// Flags for ShowWindow() and WINDOWPLACEMENT.showCmd
		public const int SW_HIDE = 0;
		public const int SW_SHOWNORMAL = 1;
		public const int SW_SHOWMINIMIZED = 2;
		public const int SW_SHOWMAXIMIZED = 3;
		public const int SW_SHOWNOACTIVATE = 4;
		public const int SW_SHOW = 5;
		public const int SW_MINIMIZE = 6;
		public const int SW_SHOWMINNOACTIVE = 7;
		public const int SW_SHOWNA = 8;
		public const int SW_RESTORE = 9;
		public const int SW_SHOWDEFAULT = 10;
		public const int SW_FORCEMINIMIZE = 11;

		// Flags for Add/Check/EnableMenuItem() 
		public const int MF_STRING = 0x00000000;
		public const int MF_SEPARATOR = 0x00000800;

		// Indexes for GetWindowLong()
		public const int GWL_STYLE = -16;
		public const int GWL_EXSTYLE = -20;

		// Flags for GetWindowLong( , GWL_STYLE)
		public const int WS_MAXIMIZEBOX = 0x00010000;
		public const int WS_THICKFRAME = 0x00040000;
		public const int WS_MAXIMIZE = 0x01000000;

		// Flags for GetWindowLong( , GWL_EXSTYLE)
		public const int WS_EX_TOPMOST = 0x00000008;
		public const int WS_EX_TRANSPARENT = 0x00000020;
		public const int WS_EX_TOOLWINDOW = 0x00000080;
		public const int WS_EX_APPWINDOW = 0x00040000;

		// WINDOWPLACEMENT.Flags
		public const int WPF_SETMINPOSITION = 0x0001;
		public const int WPF_RESTORETOMAXIMIZED = 0x0002;

		// Modifier keys
		public const int MOD_ALT = 0x0001;
		public const int MOD_CONTROL = 0x0002;
		public const int MOD_SHIFT = 0x0004;
		public const int MOD_WIN = 0x0008;

		// Windows messages
		public const int WM_SYSCOMMAND = 0x0112;
		public const int WM_HOTKEY = 0x0312;

		public struct POINT
		{
			public int x;
			public int y;
		}

		public struct RECT
		{
			public int left;
			public int top;
			public int right;
			public int bottom;

			public RECT(int left, int top, int right, int bottom)
			{
				this.left = left;
				this.top = top;
				this.right = right;
				this.bottom = bottom;
			}
		}

		public struct WINDOWPLACEMENT
		{
			public uint length;
			public uint flags;
			public uint showCmd;
			public POINT ptMinPosition;
			public POINT ptMaxPosition;
			public RECT rcNormalPosition;
		}

		// deleagte used by EnumWindows()
		public delegate bool EnumWindowsProc(IntPtr Wnd, uint lParam);

		[DllImport("user32.dll")]
		public static extern int AppendMenu(int hMenu, int uFlags, int uIDNewItem, string lpNewItem);

		[DllImport("user32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool EnumWindows(EnumWindowsProc lpEnumFunc, uint lParam);

		[DllImport("user32.dll")]
		public static extern IntPtr GetDesktopWindow();

		[DllImport("user32.dll")]
		public static extern IntPtr GetForegroundWindow();

		[DllImport("user32.dll")]
		public static extern IntPtr GetShellWindow();

		[DllImport("user32.dll")]
		public static extern int GetSystemMenu(int hwnd, int bRevert);

		[DllImport("user32.dll")]
		public static extern int GetWindowLong(IntPtr hWnd, int nIndex);

		[DllImport("user32.dll")]
		public static extern bool GetWindowPlacement(IntPtr hWnd, ref WINDOWPLACEMENT lpwndpl);

		[DllImport("user32.dll")]
		public static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

		[DllImport("user32.dll")]
		public static extern int GetWindowTextLength(IntPtr hWnd);

		[DllImport("user32.dll")]
		public static extern bool IsWindowVisible(IntPtr hWnd);

		[DllImport("user32.dll")]
		public static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);

		[DllImport("user32.dll")]
		public static extern bool SetWindowPlacement(IntPtr hWnd, ref WINDOWPLACEMENT lpwndpl);

		[DllImport("user32.dll")]
		public static extern bool UnregisterHotKey(IntPtr hWnd, int id);
	}
}
