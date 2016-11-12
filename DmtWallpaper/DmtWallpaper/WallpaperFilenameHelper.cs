using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DmtWallpaper
{
	static class WallpaperFilenameHelper
	{
		static string _wallpaperFilename = null;

		public static string GetWallpaperFilename(IntPtr hWnd)
		{
			if (_wallpaperFilename == null)
			{
				FindWallpaperFilename(hWnd);
			}

			return _wallpaperFilename;
		}

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
	}
}
