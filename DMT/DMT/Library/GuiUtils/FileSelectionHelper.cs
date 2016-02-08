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

namespace DMT.Library.GuiUtils
{
	using System;
	using System.Collections.Generic;
	using System.IO;
	using System.Linq;
	using System.Text;
	using System.Windows.Forms;

	/// <summary>
	/// Utility class to help with file selection
	/// </summary>
	static class FileSelectionHelper
	{
		/// <summary>
		/// Sets the initial filename displayed in the open file dialog
		/// </summary>
		/// <param name="dlg">The open file dialog</param>
		/// <param name="fileName">Initial filename to display</param>
		public static void SetInitialFileNameInDialog(OpenFileDialog dlg, string fileName)
		{
			dlg.FileName = fileName;
			if (fileName.Length > 0)
			{
				try
				{
					string dir = Path.GetDirectoryName(fileName);
					dlg.InitialDirectory = dir;
					dlg.FileName = Path.GetFileName(fileName);
				}
				catch (Exception)
				{
				}
			}
		}
	}
}
