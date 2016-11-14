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
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace DmtWallpaper
{
	public partial class DmtWallpaperForm : Form
	{
		const int AllowedMosueMovement = 5;

		// To try and achieve a smoth fade between old and new image, we use
		// a blend value that ranges between 0.0 which means just show the old image
		// and 1.0 which means just show the new image
		//
		// There is no point in reducing the delay between steps to a very small value
		// as the paint itself will take a considerable amout of time 
		// say typically around 100ms on slowish hardware.
		//
		// Keeping the transition time short seems to give the best results.
		//const float BlendStep = 0.251f;
		//const int DelayBetweenBlendSteps = 125;
		const float BlendStep = 0.201f;
		const int DelayBetweenBlendSteps = 50;

		IntPtr _hWnd;
		bool FullScreenMode { get { return _hWnd == IntPtr.Zero; } }
		Point _lastMouseLocation;
		string _wallpaperFilename;
		FileSystemWatcher _fileWatcher;

		//bool _gettingWallpaper = false;

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

#if QUERY_DMT_USING_COPYDATA
		protected override void WndProc(ref Message m)
		{
			if (m.Msg == NativeMethods.WM_COPYDATA)
			{
				NativeMethods.COPYDATASTRUCT cds = (NativeMethods.COPYDATASTRUCT)m.GetLParam(typeof(NativeMethods.COPYDATASTRUCT));
				WallpaperFilenameHelper.HandleCopyData(cds);
			}
			else
			{
				base.WndProc(ref m);
			}
		}
#endif

		void InitFileWatcher()
		{
			// get location where DMT saves its wallpaper
			//GetWallpaperFilename();
			_wallpaperFilename = WallpaperFilenameHelper.GetWallpaperFilename(Handle);
			// _wallpaperFilename should be filled in
			System.Console.WriteLine(_wallpaperFilename);

			// now setup a watcher to spot changes to this
			_fileWatcher = new FileSystemWatcher();
			_fileWatcher.Path = Path.GetDirectoryName(_wallpaperFilename);
			_fileWatcher.Filter = Path.GetFileName(_wallpaperFilename);
			_fileWatcher.NotifyFilter = NotifyFilters.LastWrite;
			_fileWatcher.SynchronizingObject = this;	// want callbacks on UI thread
			//_fileWatcher.Created += _fileWatcher_Created;
			_fileWatcher.Changed += _fileWatcher_Changed;
			_fileWatcher.EnableRaisingEvents = true;
		}

		//void _fileWatcher_Created(object sender, FileSystemEventArgs e)
		//{
		//	System.Diagnostics.Debug.WriteLine("{0} _fileWatcher_Created _gettingWallpaper: {1}", DateTime.Now, _gettingWallpaper);
		//	if (_gettingWallpaper)
		//	{
		//		// ignore events if still in process of getting wallpaper from last event
		//	}
		//	else
		//	{
		//		_gettingWallpaper = true;
		//		ShowWallpaper();
		//		_gettingWallpaper = false;
		//	}
		//}

		DateTime lastChangedEventTime; 
		void _fileWatcher_Changed(object sender, FileSystemEventArgs e)
		{
			if (DateTime.Now > lastChangedEventTime.AddSeconds(5))
			{
				//System.Diagnostics.Debug.WriteLine("{0} _fileWatcher_Changed _gettingWallpaper: {1}", DateTime.Now, _gettingWallpaper);
				//if (_gettingWallpaper)
				//{
				//	// ignore events if still in process of getting wallpaper from last event
				//}
				//else
				//{
				//	_gettingWallpaper = true;
					ShowWallpaper();
					lastChangedEventTime = DateTime.Now;
				//	_gettingWallpaper = false;
				//}
			}
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

		//MemoryStream _curImageMemoryStream;
		//void ShowWallpaper()
		//{
		//	MemoryStream ms = LoadWallpaperWithRetry();
		//	if (ms == null)
		//	{
		//		// couldn't get wallpaper
		//		return;
		//	}

		//	MemoryStream oldImageMemoryStream = _curImageMemoryStream;
		//	_curImageMemoryStream = ms;
	
		//	Image image = new Bitmap(ms);

		//	pictureBox.Image = image;
		//	if (oldImageMemoryStream != null)
		//	{
		//		oldImageMemoryStream.Dispose();
		//	}
		//}

		MemoryStream _oldImageMemoryStream = null;
		MemoryStream _newImageMemoryStream = null;
		float _blend;
		void ShowWallpaper()
		{
			MemoryStream ms = LoadWallpaperWithRetry();
			if (ms == null)
			{
				// couldn't get wallpaper
				return;
			}

			MemoryStream previousImageMemoryStream = _oldImageMemoryStream;
			_oldImageMemoryStream = _newImageMemoryStream;
			_newImageMemoryStream = ms;

			Image oldImage = null;
			if (_oldImageMemoryStream != null)
			{
				oldImage = new Bitmap(_oldImageMemoryStream);
			}

			Image newImage = new Bitmap(_newImageMemoryStream);

			_blend = BlendStep;
			imageBlender.SetImages(oldImage, newImage, _blend);

			if (previousImageMemoryStream != null)
			{
				previousImageMemoryStream.Dispose();
			}

			blendTimer.Interval = DelayBetweenBlendSteps;
			//System.Diagnostics.Debug.WriteLine("{0} TICK start: {1}", DateTime.Now, _blend);
			blendTimer.Start();
		}

		void blendTimer_Tick(object sender, EventArgs e)
		{
			//System.Diagnostics.Debug.WriteLine("{0} TICK _blend: {1}", DateTime.Now, _blend);
			_blend += BlendStep;
			if (_blend >= 1.0f)
			{
				_blend = 1.0f;
				//System.Diagnostics.Debug.WriteLine("{0} TICK stop: {1}", DateTime.Now, _blend);
				blendTimer.Stop();
			}
			imageBlender.Blend = _blend;
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
				// .NET 4.0 can use:
				//fs.CopyTo(ms);
				// but for earlier:
				byte[] buffer = new byte[16 * 1024];
				int bytesRead;

				while ((bytesRead = fs.Read(buffer, 0, buffer.Length)) > 0)
				{
					ms.Write(buffer, 0, bytesRead);
				}
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

		private void imageBlender_KeyDown(object sender, KeyEventArgs e)
		{
			UserActivity();
		}

		private void imageBlender_MouseClick(object sender, MouseEventArgs e)
		{
			UserActivity();
		}

		private void imageBlender_MouseMove(object sender, MouseEventArgs e)
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

		void UserActivity()
		{
			if (FullScreenMode)
			{
				Application.Exit();
			}
		}

		//void GetWallpaperFilename()
		//{
		//	// query the running DMT to get the location of the wallpaper
		//	IntPtr hWndDmt = CommandMessaging.FindDmtHWnd();
		//	if (hWndDmt != null)
		//	{
		//		CommandMessaging.SendString(Handle, hWndDmt, "WallpaperFilename", CommandMessaging.DmtQueryMessage);
		//	}
		//}
	}
}
