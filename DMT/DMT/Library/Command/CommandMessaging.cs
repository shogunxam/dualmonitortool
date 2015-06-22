#region copyright
// This file is part of Dual Monitor Tools which is a set of tools to assist
// users with multiple monitor setups.
// Copyright (C) 2015 Gerald Evans
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

using DMT.Library.PInvoke;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace DMT.Library.Command
{
	/// <summary>
	/// Allows commands to be sent to the running (GUI) version of DMT
	/// </summary>
	class CommandMessaging
	{
		public enum EMsgResult { OK, DmtNotFound, CmdUnknown };

		public const int DmtCommandMessage = 0x7405;

		//const string DmtCommandMessage = "DMT_COMMAND_MESSAGE";

		//uint _commandMessage = 0;

		//public uint GetCommandMessage()
		//{
		//	if (_commandMessage == 0)
		//	{
		//		_commandMessage = Win32.RegisterWindowMessage(DmtCommandMessage);
		//	}

		//	return _commandMessage;
		//}

		public EMsgResult SendCommandMessage(string commandName)
		{
			IntPtr hWnd = FindDmtHWnd();

			if (hWnd == IntPtr.Zero)
			{
				return EMsgResult.DmtNotFound;
			}

			IntPtr wParam = IntPtr.Zero;

			//// can't see how to get the lengh of the data?
			//// Marshal.SizeOf(commandName) shouldn't work as it wouldn't know what encoding will be used
			//IntPtr lpData = Marshal.StringToHGlobalUni(commandName);
			//int cbData = ?

			//// so do it the long way - doesn't work either
			//byte[] commandAsBytes = Encoding.UTF8.GetBytes(commandName);
			//IntPtr lpData = Marshal.AllocHGlobal(Marshal.SizeOf(commandAsBytes));
			//Marshal.StructureToPtr(commandAsBytes, lpData, false);
			//int cbData = Marshal.SizeOf(commandAsBytes);

			// Using Ansi, we can calculate the length from the source
			IntPtr lpData = Marshal.StringToHGlobalAnsi(commandName);
			int cbData = commandName.Length + 1;


			Win32.COPYDATASTRUCT cds;
			cds.dwData = (IntPtr)DmtCommandMessage;
			cds.cbData = cbData;
			cds.lpData = lpData;

			IntPtr lParam = Marshal.AllocHGlobal(Marshal.SizeOf(cds));
			Marshal.StructureToPtr(cds, lParam, false);

			IntPtr result = Win32.SendMessage(hWnd, Win32.WM_COPYDATA, wParam, lParam);

			Marshal.FreeHGlobal(lParam);
			Marshal.FreeHGlobal(lpData);

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

		IntPtr FindDmtHWnd()
		{
			return Win32.FindWindow(null, "DMT_GUI_WINDOW");
		}

	}
}
