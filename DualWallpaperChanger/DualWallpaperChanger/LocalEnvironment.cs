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

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace DualMonitorTools.DualWallpaperChanger
{
	/// <summary>
	/// Provides access to the environment
	/// </summary>
	public class LocalEnvironment : ILocalEnvironment
	{
		public Monitors Monitors
		{
			get
			{
				// this can change with time, so do not cache
				Monitors monitors = new Monitors();
				foreach (Screen screen in Screen.AllScreens)
				{
					Monitor monitor = new Monitor();
					monitor.Bounds = screen.Bounds;
					monitor.Primary = screen.Primary;
					monitors.Add(monitor);
				}
				return monitors;
			}
		}

		string _appDataDir;

		/// <summary>
		/// Directory to save the wallpaper too
		/// </summary>
		public string AppDataDir
		{
			get
			{
				if (_appDataDir == null)
				{
					// data dir needs to be on a per user per machine basis 
					// (not roaming as different machines may have different screen layouts etc.)
					string appDataDir = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

					// Add the company name to the path
					appDataDir = Path.Combine(appDataDir, "GNE");

					// Add the program name to the path
					_appDataDir = Path.Combine(appDataDir, "DualWallpaperChanger");
					// make sure the directory exists
					Directory.CreateDirectory(_appDataDir);
				}
				return _appDataDir;
			}
		}

		public bool IsWin8OrLater()
		{
			return OsHelper.IsWin8OrLater();
		}

	}
}
