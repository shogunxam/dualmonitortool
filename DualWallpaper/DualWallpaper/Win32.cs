using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace DualWallpaper
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

		[DllImport("user32.dll")]
		public static extern int GetSystemMenu(int hwnd, int bRevert);

		[DllImport("user32.dll")]
		public static extern bool SetProcessDPIAware();

		[DllImport("user32.dll")]
		public static extern int SystemParametersInfo(uint uiAction, uint uiParam, string pvParam, uint fWinIni);

	}
}
