#region copyright
// This file is part of Dual Monitor Tools which is a set of tools to assist
// users with multiple monitor setups.
// Copyright (C) 2016 Gerald Evans
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
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DmtWallpaper
{
	public partial class DmtWallpaperForm : Form
	{
		const int AllowedMosueMovement = 5;

		IntPtr _hWnd;
		bool FullScreenMode { get { return _hWnd == IntPtr.Zero; } }
		Point _lastMouseLocation;
		string _wallpaperFilename;
		FileSystemWatcher _fileWatcher;

		public DmtWallpaperForm(IntPtr hWnd)
		{
			_hWnd = hWnd;

			InitializeComponent();

			InitFileWatcher();

			if (FullScreenMode)
			{
				InitFullScreenMode();
			}
			else
			{
				InitPreviewMode();
			}
		}

		protected override void WndProc(ref Message m)
		{
			if (m.Msg == NativeMethods.WM_COPYDATA)
			{
				NativeMethods.COPYDATASTRUCT cds = (NativeMethods.COPYDATASTRUCT)m.GetLParam(typeof(NativeMethods.COPYDATASTRUCT));

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
			else
			{
				base.WndProc(ref m);
			}
		}

		void InitFileWatcher()
		{
			// get location where DMT saves its wallpaper
			GetWallpaperFilename();
			// _wallpaperFilename should be filled in
			System.Console.WriteLine(_wallpaperFilename);

			// now setup a watcher to spot changes to this
			_fileWatcher = new FileSystemWatcher();
			_fileWatcher.Path = Path.GetDirectoryName(_wallpaperFilename);
			_fileWatcher.Filter = Path.GetFileName(_wallpaperFilename);
			_fileWatcher.NotifyFilter = NotifyFilters.LastWrite;
			_fileWatcher.Created += _fileWatcher_Changed;
			_fileWatcher.Changed += _fileWatcher_Changed;
			_fileWatcher.EnableRaisingEvents = true;
		}

		void _fileWatcher_Changed(object sender, FileSystemEventArgs e)
		{
			ShowWallpaper();
		}

		void InitFullScreenMode()
		{
			// get bounding rectangle
			Rectangle rect = ScreenHelper.GetVitrualDesktopRect();


			// TEMP CODE to simplify development
			//rect = Screen.AllScreens[1].Bounds;


			Bounds = rect;
		}

		void InitPreviewMode()
		{
			NativeMethods.SetParent(Handle, _hWnd);
			int windowStyle = NativeMethods.GetWindowLong(Handle, NativeMethods.GWL_STYLE);
			windowStyle |= NativeMethods.WS_CHILD;
			NativeMethods.SetWindowLong(Handle, NativeMethods.GWL_STYLE, windowStyle);

			Rectangle previewRect;
			NativeMethods.GetClientRect(_hWnd, out previewRect);
			Location = new Point();
			Size = previewRect.Size;
		}

		MemoryStream _curImageMemoryStream;
		void ShowWallpaper()
		{
			MemoryStream ms = LoadWallpaperWithRetry();
			if (ms == null)
			{
				// couldn't get wallpaper
				return;
			}

			MemoryStream oldImageMemoryStream = _curImageMemoryStream;
			_curImageMemoryStream = ms;
	
			Image image = new Bitmap(ms);

			pictureBox.Image = image;
			if (oldImageMemoryStream != null)
			{
				oldImageMemoryStream.Dispose();
			}
		}

		MemoryStream LoadWallpaperWithRetry()
		{
			for (int attempt = 1; attempt <= 3; attempt++)
			{
				MemoryStream ms = null;
				try
				{
					ms = LoadWallpaper();
					return ms;
				}
				catch (FileNotFoundException)
				{
					// Wallpaper Changer not run yet?
					// or path is wrong!
					// No point in retrying
					return null;
				}
				catch (Exception)
				{
					// TODO: need to be in background thread
					Thread.Sleep(500 * attempt * attempt);
				}
			}

			return null;
		}

		MemoryStream LoadWallpaper()
		{
			MemoryStream ms = new MemoryStream();
			using (FileStream fs = File.OpenRead(_wallpaperFilename))
			{
				fs.CopyTo(ms);
			}

			return ms;
		}

		private void DmtWallpaperForm_Load(object sender, EventArgs e)
		{
			Cursor.Hide();
			TopMost = true;

			ShowWallpaper();
		}


		private void DmtWallpaperForm_KeyDown(object sender, KeyEventArgs e)
		{
			//System.Console.WriteLine("DmtWallpaperForm_KeyDown");
			UserActivity();
		}

		private void pictureBox_MouseMove(object sender, MouseEventArgs e)
		{
			if (!_lastMouseLocation.IsEmpty)
			{
				if (Math.Abs(e.X - _lastMouseLocation.X) + Math.Abs(e.Y - _lastMouseLocation.Y) > AllowedMosueMovement)
				{
					UserActivity();
				}
			}

			_lastMouseLocation = e.Location;
		}

		private void pictureBox_MouseClick(object sender, MouseEventArgs e)
		{
			UserActivity();
		}

		void UserActivity()
		{
			if (FullScreenMode)
			{
				Application.Exit();
			}
		}

		void GetWallpaperFilename()
		{
			// query the running DMT to get the location of the wallpaper
			IntPtr hWndDmt = CommandMessaging.FindDmtHWnd();
			if (hWndDmt != null)
			{
				CommandMessaging.SendString(Handle, hWndDmt, "WallpaperFilename", CommandMessaging.DmtQueryMessage);
			}
		}
	}
}
