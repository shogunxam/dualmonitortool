#region copyright
// This file is part of SwapScreen
// Copyright (C) 2009 Gerald Evans
// 
// File Locater is free software: you can redistribute it and/or modify
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
using System.Windows.Forms;

namespace SwapScreen
{
	/// <summary>
	/// Class to help in the process of adding menu items to the system menu.
	/// This just contains the bare minimum needed by Swap Screen.
	/// </summary>
	static class SystemMenuHelper
	{
		/// <summary>
		/// Adds a menu separator bar after as the last item on the system menu.
		/// </summary>
		/// <param name="form">Form containing the system menu.</param>
		public static void AppendSeparator(Form form)
		{
			int hMenu = Win32.GetSystemMenu(form.Handle.ToInt32(), 0);
			Win32.AppendMenu(hMenu, Win32.MF_SEPARATOR, 0, null);
		}

		/// <summary>
		/// Adds a new menu item as the last item on the system menu.
		/// </summary>
		/// <param name="form">Form containing the system menu.</param>
		/// <param name="idNewItem">ID of the new menu item.</param>
		/// <param name="newItem">Text for the new menu item.</param>
		public static void Append(Form form, int idNewItem, string newItem)
		{
			int hMenu = Win32.GetSystemMenu(form.Handle.ToInt32(), 0);
			Win32.AppendMenu(hMenu, Win32.MF_STRING, idNewItem, newItem);
		}
	}
}
