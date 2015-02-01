#region copyright
// This file is part of Dual Monitor Tools which is a set of tools to assist
// users with multiple monitor setups.
// Copyright (C) 2015  Gerald Evans
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

namespace DualMonitorTools.DualWallpaperChanger
{
	/// <summary>
	/// Definitions extracted from winuser.h
	/// </summary>
	static class Win32
	{
		// Flags for Add/Check/EnableMenuItem() 
		public const int MF_STRING = 0x00000000;
		public const int MF_SEPARATOR = 0x00000800;

		// flags for SystemParametersInfo(uiAction)
		public static uint SPI_SETDESKWALLPAPER = 20;

		// flags for SystemParametersInfo(fWinIni)
		public static uint SPIF_UPDATEINIFILE = 0x0001;
		public static uint SPIF_SENDWININICHANGE = 0x0002;

		// Windows messages
		public const int WM_SYSCOMMAND = 0x0112;


		[DllImport("user32.dll")]
		public static extern int AppendMenu(int hMenu, int uFlags, int uIDNewItem, string lpNewItem);

		[DllImport("kernel32.dll")]
		public static extern bool AttachConsole(int pid);

		[DllImport("user32.dll")]
		public static extern int GetSystemMenu(int hwnd, int bRevert);

		[DllImport("kernel32")]
		public static extern bool FreeConsole();

		[DllImport("user32.dll")]
		public static extern bool SetProcessDPIAware();

		[DllImport("user32.dll")]
		public static extern int SystemParametersInfo(uint uiAction, uint uiParam, string pvParam, uint fWinIni);

	}
}
