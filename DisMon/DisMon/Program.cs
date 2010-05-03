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
using System.Diagnostics;
using System.Windows.Forms;
using DisMon.Properties;

namespace DisMon
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main(string[] args)
		{
			// TODO: any harm in calling these even if we don't display a gui?
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			Options options = new Options(args);

			string cmdName = options.CmdName;
			if (cmdName != null && cmdName.Length > 0)
			{
				// we are just going to run another command
				RunCmd(options);
			}
			else
			{
				// no command - so just launch the gui
				Application.Run(new DisMonForm());

				// not sure if this is really wanted, but...
				// make sure any disabled monitors are re-enabled
				DisMon.Instance.ReEnable();
			}
		}

		static void RunCmd(Options options)
		{
			// disable all secondary monitors
			DisMon.Instance.DisableAllSecondary();

			ProcessStartInfo pInfo = new ProcessStartInfo();
			pInfo.FileName = options.CmdName;
			if (options.CmdArgs != null)
			{
				pInfo.Arguments = options.CmdArgs;
			}
			try
			{
				Process p = Process.Start(pInfo);
				if (p != null)
				{
					// following returns immediatedly for non-gui apps
					p.WaitForInputIdle();
					// wait for process to exist
					p.WaitForExit();
				}
				else
				{
					MessageBox.Show("No new process started", MyTitle);
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, MyTitle);
			}

			// re-enable all the monitors we disabled
			DisMon.Instance.ReEnable();
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