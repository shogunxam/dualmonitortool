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

namespace DMT
{
	using System;
	using System.Collections.Generic;
	using System.IO;
	using System.Linq;
	using System.Threading.Tasks;
	using System.Windows.Forms;

	using DMT.Library.Command;
	using DMT.Library.Logging;
	using DMT.Library.PInvoke;

	/// <summary>
	/// Top level program
	/// </summary>
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		/// <param name="args">Command line arguments</param>
		[STAThread]
		static void Main(string[] args)
		{
#if DEBUG
			//try
			//{
			//	FileInfo file = new FileInfo(@"C:\Temp\dmt.log");
			//	StreamWriter writer = file.AppendText();
			//	writer.WriteLine(string.Format("{0}: {1}", DateTime.Now.ToString("HH:mm:ss"), "DMT loaded"));
			//	writer.Close();
			//}
			//catch (Exception ex)
			//{
			//	MessageBox.Show(ex.Message);
			//}
#endif

			ProgramOptions programOptions = new ProgramOptions(args);

			if (programOptions.CmdMode)
			{
				ConsoleMain(programOptions);
			}
			else
			{
				// Make sure the GUI isn't already running.
				// If it is, we activate it and exit silently.
				if (ActivateExistingGui())
				{
					// Yes it is already running, so lets not start up a second instance
					return;
				}

				// doesn't look like we are currently running
				GuiMain(programOptions);
			}
		}

		static bool ActivateExistingGui()
		{
			CommandMessaging commandMessaging = new CommandMessaging();
			string magicCommand = "DMT:General:Options";
			CommandMessaging.EMsgResult msgResult = commandMessaging.SendCommandMessage(magicCommand);
			return msgResult == CommandMessaging.EMsgResult.OK;
		}

		static void ConsoleMain(ProgramOptions programOptions)
		{
			NativeMethods.AttachConsole(-1);
			ConsoleApplication consoleApplication = new ConsoleApplication(programOptions);
			consoleApplication.Run();
			NativeMethods.FreeConsole();
		}

		static void GuiMain(ProgramOptions programOptions)
		{
			Application.ThreadException += Application_ThreadException;
			AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			AppForm appForm = new AppForm();
			Application.Run();
		}

		static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
		{
			MessageBox.Show((e.ExceptionObject as Exception).Message, "DMT - Unhandled Non-UI Thread Exception");
		}

		static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
		{
			MessageBox.Show(e.Exception.Message, "DMT - Unhandled UI Thread Exception");
		}
	}
}
