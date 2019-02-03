#region copyright
// This file is part of Dual Monitor Tools which is a set of tools to assist
// users with multiple monitor setups.
// Copyright (C) 2018  Gerald Evans
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

using DMT.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DMT.Library.GuiUtils
{
	// MsgDlg.Error
	// MsgDlg.UserError
	// MsgDlg.SystemError

	static class MsgDlg
	{
		/// <summary>
		/// Error that hasn't been classified as a user or system error (yet)
		/// </summary>
		/// <param name="msg"></param>
		public static void Error(string msg)
		{
			MessageBox.Show(msg, CommonStrings.MyTitle);
		}

		/// <summary>
		/// Show error that resulted from a user action.
		/// These must always be displayed to the user
		/// </summary>
		/// <param name="msg"></param>
		public static void UserError(string msg)
		{
			MessageBox.Show(msg, CommonStrings.MyTitle);
		}

		/// <summary>
		/// This is a system error, which in most cases the user has no control over.
		/// We may wish to hide these so as not to annoy the user.
		/// </summary>
		/// <param name="msg"></param>
		public static void SystemError(string msg)
		{
			MessageBox.Show(msg, CommonStrings.MyTitle);
		}
	}
}
