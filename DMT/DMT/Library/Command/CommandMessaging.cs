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

namespace DMT.Library.Command
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Runtime.InteropServices;
	using System.Text;
	using System.Windows.Forms;

	using DMT.Library.PInvoke;

	/// <summary>
	/// Allows commands to be sent to the running (GUI) version of DMT
	/// </summary>
	class CommandMessaging
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


		/// <summary>
		/// Result of command
		/// </summary>
		public enum EMsgResult
		{
			/// <summary>
			/// Command found and run
			/// </summary>
			OK,

			/// <summary>
			/// Cold not find running instance of DMT
			/// </summary>
			DmtNotFound,

			/// <summary>
			/// Command not recognised
			/// </summary>
			CmdUnknown
		}

		/// <summary>
		/// Sends a command message to the GUI instance of DMT
		/// </summary>
		/// <param name="commandName">Command to send</param>
		/// <returns>OK if command found and run</returns>
		//public EMsgResult SendCommandMessage(string commandName)
		//{
		//	IntPtr hWnd = FindDmtHWnd();

		//	if (hWnd == IntPtr.Zero)
		//	{
		//		return EMsgResult.DmtNotFound;
		//	}

		//	IntPtr wParam = IntPtr.Zero;

		//	// Using Ansi, we can calculate the length from the source
		//	IntPtr lpData = Marshal.StringToHGlobalAnsi(commandName);
		//	int cbData = commandName.Length + 1;

		//	NativeMethods.COPYDATASTRUCT cds;
		//	cds.dwData = (IntPtr)DmtCommandMessage;
		//	cds.cbData = cbData;
		//	cds.lpData = lpData;

		//	IntPtr lParam = Marshal.AllocHGlobal(Marshal.SizeOf(cds));
		//	Marshal.StructureToPtr(cds, lParam, false);

		//	IntPtr result = NativeMethods.SendMessage(hWnd, NativeMethods.WM_COPYDATA, wParam, lParam);

		//	Marshal.FreeHGlobal(lParam);
		//	Marshal.FreeHGlobal(lpData);

		//	if (result == IntPtr.Zero)
		//	{
		//		return EMsgResult.OK;
		//	}
		//	else
		//	{
		//		// assume error is because command was unknown
		//		return EMsgResult.CmdUnknown;
		//	}
		//}
		public EMsgResult SendCommandMessage(string commandName)
		{
			IntPtr hWnd = FindDmtHWnd();

			if (hWnd == IntPtr.Zero)
			{
				return EMsgResult.DmtNotFound;
			}

			IntPtr result = SendString(hWnd, commandName, DmtCommandMessage);

			if (result == IntPtr.Zero)
			{
				return EMsgResult.OK;
			}
			else
			{
				// assume error is because command was unknown
				return EMsgResult.CmdUnknown;
			}
		}

		public static IntPtr SendString(IntPtr hWnd, string s, int messageType)
		{
			IntPtr wParam = IntPtr.Zero;

			// Note: using Unicode now rather tha ANSI as used in versions prior to v2.5
			IntPtr lpData = Marshal.StringToHGlobalUni(s);
			int cbData = (s.Length + 1) * 2;

			NativeMethods.COPYDATASTRUCT cds;
			cds.dwData = (IntPtr)messageType;
			cds.cbData = cbData;
			cds.lpData = lpData;

			IntPtr lParam = Marshal.AllocHGlobal(Marshal.SizeOf(cds));
			Marshal.StructureToPtr(cds, lParam, false);

			IntPtr result = NativeMethods.SendMessage(hWnd, NativeMethods.WM_COPYDATA, wParam, lParam);

			Marshal.FreeHGlobal(lParam);
			Marshal.FreeHGlobal(lpData);

			return result;
		}

		IntPtr FindDmtHWnd()
		{
			return NativeMethods.FindWindow(null, "DMT_GUI_WINDOW");
		}
	}
}
