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
using System.Text;
using System.Windows.Forms;

using Microsoft.Win32;

namespace SwapScreen
{
	/// <summary>
	/// Utility class to assist starting the current app when the user logs in.
	/// </summary>
	class AutoStart
	{
		private const string runKey = @"Software\Microsoft\Windows\CurrentVersion\Run";

		/// <summary>
		/// Checks if the application is set to autostart using one of the Run registry keys
		/// </summary>
		/// <param name="keyName">Name used to identify this application</param>
		/// <returns>true if application autostarts using a registry run key</returns>
		public static bool IsAutoStart(string keyName)
		{
			bool ret = false;

			RegistryKey key = Registry.CurrentUser.OpenSubKey(runKey);
			if (key != null)
			{
				object keyValue = key.GetValue(keyName);
				if (keyValue != null)
				{
					ret = true;
				}
			}

			return ret;
		}

		/// <summary>
		/// Sets the application to autostart using a registry run key
		/// </summary>
		/// <param name="keyName">Name used to identify this application</param>
		public static void SetAutoStart(string keyName)
		{
			RegistryKey key = Registry.CurrentUser.CreateSubKey(runKey);
			key.SetValue(keyName, Application.ExecutablePath);
		}

		/// <summary>
		/// Stops the application from autostarting using a registry run key
		/// </summary>
		/// <param name="keyName">Name used to identify this application</param>
		public static void UnsetAutoStart(string keyName)
		{
			RegistryKey key = Registry.CurrentUser.CreateSubKey(runKey);
			key.DeleteValue(keyName);
		}
	}
}
