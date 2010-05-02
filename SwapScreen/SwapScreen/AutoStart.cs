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
