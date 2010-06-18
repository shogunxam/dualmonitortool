#region copyright
// This file is part of Dual Monitor Tools which is a set of tools to assist
// users with multiple monitor setups.
// Copyright (C) 2010  Gerald Evans
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

namespace DisMon
{
	/// <summary>
	/// Definitions extracted from winuser.h + wingdi.h
	/// </summary>
	class Win32
	{
		// Flags for Add/Check/EnableMenuItem() 
		public const int MF_STRING = 0x00000000;
		public const int MF_SEPARATOR = 0x00000800;

		// Flags for ChangeDisplaySettings(/Ex).dwFlags
		public const uint CDS_UPDATEREGISTRY = 0x00000001;
		public const uint CDS_SET_PRIMARY    = 0x00000010;
		public const uint CDS_NORESET        = 0x10000000;

		// return values for ChangeDisplaySettings(/Ex)
		public const int DISP_CHANGE_SUCCESSFUL = 0;

		// Flags for DEVMODE.dmFields
		public const uint DM_POSITION         = 0x00000020;
		public const uint DM_BITSPERPEL       = 0x00040000;
		public const uint DM_PELSWIDTH        = 0x00080000;
		public const uint DM_PELSHEIGHT       = 0x00100000;
		public const uint DM_DISPLAYFLAGS     = 0x00200000;
		public const uint DM_DISPLAYFREQUENCY = 0x00400000;

		// EnumDisplaySettings.iModeNum
		public const int ENUM_CURRENT_SETTINGS = -1;
		public const int ENUM_REGISTRY_SETTINGS = -2;


		// Windows messages
		public const int WM_SYSCOMMAND = 0x0112;

		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
		public struct DEVMODE
		{
			const int CCHDEVICENAME = 32;	// size of a device name string
			const int CCHFORMNAME = 32;		// size of a form name string

			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = CCHDEVICENAME)]
			public string dmDeviceName;

			public ushort dmSpecVersion;
			public ushort dmDriverVersion;
			public ushort dmSize;
			public ushort dmDriverExtra;
			public uint dmFields;

			// union - ignore printer stuff
			public int dmPositionX;	// POINTL.x
			public int dmPositionY;	// POINTL.y
			public int dmDisplayOrientation;
			public int dmDisplayFixedOutput;

			public short dmColor;
			public short dmDuplex;
			public short dmYResolution;
			public short dmTTOption;
			public short dmCollate;

			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = CCHFORMNAME)]
			public string dmFormName;

			public ushort dmLogPixels;
			public uint dmBitsPerPel;
			public uint dmPelsWidth;
			public uint dmPelsHeight;
			public uint dmDisplayFlags;
			public uint dmDisplayFrequency;
			public uint dmICMMethod;
			public uint dmICMIntent;
			public uint dmMediaType;
			public uint dmDitherType;
			public uint dmReserved1;
			public uint dmReserved2;
			public uint dmPanningWidth;
			public uint dmPanningHeight;
		}


		[DllImport("user32.dll")]
		public static extern int AppendMenu(int hMenu, int uFlags, int uIDNewItem, string lpNewItem);

		[DllImport("user32.dll")]
		//public static extern DISP_CHANGE ChangeDisplaySettings(ref DEVMODE lpDevMode, uint dwflags);
		public static extern int ChangeDisplaySettings(IntPtr lpDevMode, uint dwflags);

		[DllImport("user32.dll")]
		public static extern int ChangeDisplaySettingsEx(string lpszDeviceName, ref DEVMODE lpDevMode, IntPtr hwnd, uint dwflags, IntPtr lParam);

		[DllImport("user32.dll")]
		//public static extern bool EnumDisplaySettings(string lpszDeviceName, uint iModeNum, ref DEVMODE lpDevMode);
		// iModeNum declared as unsigned in header but declared signed here to simplify passing the settings
		public static extern bool EnumDisplaySettings(string lpszDeviceName, int iModeNum, ref DEVMODE lpDevMode);

		[DllImport("user32.dll")]
		public static extern int GetSystemMenu(int hwnd, int bRevert);
	}
}
