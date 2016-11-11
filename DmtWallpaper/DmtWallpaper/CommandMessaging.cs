#region copyright
// This file is part of Dual Monitor Tools which is a set of tools to assist
// users with multiple monitor setups.
// Copyright (C) 2015-2016 Gerald Evans
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
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DmtWallpaper
{
	/// <summary>
	/// based on DMT/Library/Command/CommandMessaging.cs
	/// </summary>
	static class CommandMessaging
	{
		/// <summary>
		/// Message sent from command line DMT to DMT window
		/// </summary>
		public const int DmtCommandMessage = 0x7405;

		/// <summary>
		/// Message sent from DmtWallpaper.scr to DMT window
		/// </summary>
		public const int DmtQueryMessage = 0x7406;

		/// <summary>
		/// Message sent from DMT window to DmtWallpaper.scr
		/// </summary>
		public const int DmtQueryReplyMessage = 0x7407;


		public static IntPtr SendString(IntPtr hWndFrom, IntPtr hWndTo, string s, int messageType)
		{
			IntPtr wParam = hWndFrom;

			// Send string as Unicode, as pathnames could contain non-ANSI characters
			IntPtr lpData = Marshal.StringToHGlobalUni(s);
			int cbData = (s.Length + 1) * 2;

			NativeMethods.COPYDATASTRUCT cds;
			cds.dwData = (IntPtr)messageType;
			cds.cbData = cbData;
			cds.lpData = lpData;

			IntPtr lParam = Marshal.AllocHGlobal(Marshal.SizeOf(cds));
			Marshal.StructureToPtr(cds, lParam, false);

			IntPtr result = NativeMethods.SendMessage(hWndTo, NativeMethods.WM_COPYDATA, wParam, lParam);

			Marshal.FreeHGlobal(lParam);
			Marshal.FreeHGlobal(lpData);

			return result;
		}

		public static IntPtr FindDmtHWnd()
		{
			return NativeMethods.FindWindow(null, "DMT_GUI_WINDOW");
		}
	}
}
