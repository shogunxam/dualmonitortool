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
using System.Windows.Forms;

namespace DmtWallpaper
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main(string[] args)
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			ProgramOptions options = new ProgramOptions(args);

			switch (options.RunMode)
			{
				case ProgramOptions.Mode.FullScreen:
				Application.Run(new DmtWallpaperForm(IntPtr.Zero));
				break;

				case ProgramOptions.Mode.Preview:
				Application.Run(new DmtWallpaperForm(options.HWnd));
				break;

				default:
				case ProgramOptions.Mode.Configuration:
				Application.Run(new SettingsForm());
				break;
			}
		}
	}
}
