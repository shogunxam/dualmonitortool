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
using System.IO;
using System.Windows.Forms;
using DualWallpaper.Properties;

namespace DualWallpaper
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			Win32.SetProcessDPIAware();

			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new DualWallpaper());
		}

		/// <summary>
		/// Returns the name that we are known as.
		/// This is used for display to the user in message boxes and the about box 
		/// and the cpation of the main window.
		/// This is not expected to change even if the program gets translated?
		/// </summary>
		public static string MyTitle
		{
			get
			{
				return Resources.MyTitle;
			}
		}

		private static string myAppDataDir;

		/// <summary>
		/// Directory to save the wallpaper too
		/// </summary>
		public static string MyAppDataDir
		{
			get
			{
				if (myAppDataDir == null)
				{
					// data dir needs to be on a per user per machine basis 
					// (not roaming as different machines may have different screen layouts etc.)
					//string appDataDir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
					string appDataDir = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

					// Add the company name to the path
					appDataDir = Path.Combine(appDataDir, "GNE");

					// Add the program name to the path
					myAppDataDir = Path.Combine(appDataDir, "DualWallpaper");
					// make sure the directory exists
					Directory.CreateDirectory(myAppDataDir);
				}
				return myAppDataDir;
			}
		}

	}
}