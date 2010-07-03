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
using System.Windows.Forms;
using DualSnap.Properties;

namespace DualSnap
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

			SnapForm form = new SnapForm();
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
				Properties.Settings.Default.Save();
			}
		}

		public static void VisitDualSnapWebsite()
		{
			try
			{
				System.Diagnostics.Process.Start("http://dualmonitortool.sourceforge.net/dualsnap.html");
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, Program.MyTitle);
			}
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