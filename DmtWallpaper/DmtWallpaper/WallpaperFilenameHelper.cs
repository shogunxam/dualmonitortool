using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace DmtWallpaper
{
	static class WallpaperFilenameHelper
	{
		const string DmtKey = @"SOFTWARE\Dual Monitor Tools\DMT";
		const string DmtWallpaperPathName = "DmtWallpaperPath";

		static string _wallpaperFilename = null;

		public static string GetWallpaperFilename(IntPtr hWnd)
		{
			if (_wallpaperFilename == null)
			{
				FindWallpaperFilename();
			}

			return _wallpaperFilename;
		}

#if QUERY_DMT_USING_COPYDATA
		// NOTE: COPYDATA will not work as the screensaver runs in a different desktop to DMT
		// and can't send messages to its window

		public static void HandleCopyData(NativeMethods.COPYDATASTRUCT cds)
		{
			if (cds.dwData == (IntPtr)CommandMessaging.DmtQueryReplyMessage)
			{
				string fullReply = Marshal.PtrToStringUni(cds.lpData);
				int index = fullReply.IndexOf(':');
				if (index >= 0)
				{
					string query = fullReply.Substring(0, index);
					string reply = fullReply.Substring(index + 1);
					if (query == "WallpaperFilename")
					{
						_wallpaperFilename = reply;
					}
				}
			}
		}

		static void FindWallpaperFilename(IntPtr hWnd)
		{
			// query the running DMT to get the location of the wallpaper
			IntPtr hWndDmt = CommandMessaging.FindDmtHWnd();
			if (hWndDmt != null)
			{
				// The following should result in HandleCopyData() being called by hWnd
				CommandMessaging.SendString(hWnd, hWndDmt, "WallpaperFilename", CommandMessaging.DmtQueryMessage);
			}
		}
#endif

		static void FindWallpaperFilename()
		{
			RegistryKey key = Registry.CurrentUser.OpenSubKey(DmtKey);
			if (key != null)
			{
				object keyValue = key.GetValue(DmtWallpaperPathName);
				if (keyValue != null)
				{
					_wallpaperFilename = keyValue as string;
				}

				// release any resources
				key.Close();
			}
		}

	}
}
