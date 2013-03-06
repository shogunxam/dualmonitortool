#region copyright
// This file is part of Dual Monitor Tools which is a set of tools to assist
// users with multiple monitor setups.
// Copyright (C) 2009  Gerald Evans
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
using System.Drawing;
using System.Windows.Forms;
using SwapScreen.Properties;

namespace SwapScreen
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			CheckIfNeedSettingsUpgrade();

			OptionsForm form = new OptionsForm();

			// Experimental code start
			// Note: this setting is normally false in the configuration file
			// and needs to be manually edited to force it to true
			if (Properties.Settings.Default.ReduceMemSizeOnStartup)
			{
				ReduceMemSize();
			}
			// Experimental code end

			Application.Run();
		}

		// If this is a new version of the program,
		// import settings from previous version
		private static void CheckIfNeedSettingsUpgrade()
		{
			if (Properties.Settings.Default.NeedSettingsUpgrade)
			{
				Properties.Settings.Default.Upgrade();
				Properties.Settings.Default.NeedSettingsUpgrade = false;
				// save the settings now, as settings are normally only saved
				// when user explicitly changes a setting and if the user doesn't
				// change any settings, we would end up upgrading each time the
				// program is started.
				//Properties.Settings.Default.Save();
				Controller.Instance.SaveSettings();
			}
		}

		// This will not get called in a default release.
		//
		// We force the garbage collector to run and then force all the apps pages
		// to be released.
		// This will then cause page faults as code/data is required, but
		// any code/data that was only required for startup shouldn't get paged back in
		// unless it resides in the same page as non-startup code/data.
		// This is to get an idea of the real working set size when the program is running/idling.
		//
		// Although this will make the application look smaller and hence more efficient
		// it will actually do the reverse due to the extra page faults caused.
		//
		// Also, if the system was to run low on memory the O/S would page our least active pages out
		// anyway.
		private static void ReduceMemSize()
		{
			GC.Collect();
			GC.WaitForPendingFinalizers();
			Win32.SetProcessWorkingSetSize(System.Diagnostics.Process.GetCurrentProcess().Handle, 0xFFFFFFFF, 0xFFFFFFFF);
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
	}
}