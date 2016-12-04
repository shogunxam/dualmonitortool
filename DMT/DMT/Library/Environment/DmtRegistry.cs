using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DMT.Library.Environment
{
	public static class DmtRegistry
	{
		static string _lastDmtWallpaperFilename = null;

		const string DmtKey = @"SOFTWARE\Dual Monitor Tools\DMT";
		const string DmtWallpaperPathName = "DmtWallpaperPath";

		public static void SetDmtWallpaperFilename(string dmtWallpaperFilename)
		{
			if (dmtWallpaperFilename != _lastDmtWallpaperFilename)
			{
				RegistryKey key = Registry.CurrentUser.CreateSubKey(DmtKey);
				key.SetValue(DmtWallpaperPathName, dmtWallpaperFilename);
				key.Close();

				_lastDmtWallpaperFilename = dmtWallpaperFilename;
			}
		}
	}
}
