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
using DMT.Library.PInvoke;

namespace DualWallpaper
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main(string[] args)
		{
			// This is now done by the manifest
			//// SetProcessDPIAware is not available on XP
			//if (OsHelper.IsVistaOrLater())
			//{
			//	Win32.SetProcessDPIAware();
			//}

			Options options = new Options(args);

			if (options.CmdMode)
			{
				ConsoleMain(options);
			}
			else
			{
				GuiMain(options);
			}
		}

		static void ConsoleMain(Options options)
		{
			NativeMethods.AttachConsole(-1);
			ConsoleApplication consoleApplication = new ConsoleApplication(options);
			consoleApplication.Run();
			NativeMethods.FreeConsole();
		}

		static void GuiMain(Options options)
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new DualWallpaper());
		}


		/// <summary>
		/// Returns the name that we are known as.
		/// This is used for display to the user in message boxes and the about box 
		/// and the caption of the main window.
		/// This is not expected to change even if the program gets translated?
		/// </summary>
		public static string MyTitle
		{
			get
			{
				return Resources.MyTitle;
			}
		}
	}
}