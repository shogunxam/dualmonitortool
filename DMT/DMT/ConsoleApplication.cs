#region copyright
// This file is part of Dual Monitor Tools which is a set of tools to assist
// users with multiple monitor setups.
// Copyright (C) 2015 Gerald Evans
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
	using System.Linq;
	using System.Text;

	using DMT.Library.Command;
	using DMT.Library.PInvoke;

	/// <summary>
	/// Runs DMT as a console application.
	/// </summary>
	class ConsoleApplication
	{
		ProgramOptions _programOptions;

		/// <summary>
		/// Initialises a new instance of the <see cref="ConsoleApplication" /> class.
		/// </summary>
		/// <param name="programOptions">Options to use (after being parsed from command line)</param>
		public ConsoleApplication(ProgramOptions programOptions)
		{
			_programOptions = programOptions;
		}

		/// <summary>
		/// Allows the DMT console application to do it's processing
		/// </summary>
		public void Run()
		{
			if (_programOptions.Errors.Count > 0)
			{
				foreach (string error in _programOptions.Errors)
				{
					Console.WriteLine(error);
				}

				ShowUsage();
			}
			else if (_programOptions.ShowUsage)
			{
				ShowUsage();
			}
			else if (_programOptions.ShowVersion)
			{
				ShowVersion();
			}
			else if (_programOptions.CloseGui)
			{
				IntPtr hWnd = NativeMethods.FindWindow(null, "DMT_GUI_WINDOW");

				if (hWnd != IntPtr.Zero)
				{
					NativeMethods.SendMessage(hWnd, NativeMethods.WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
				}
			}
			else
			{
				CommandMessaging commandMessaging = new CommandMessaging();
				foreach (string command in _programOptions.Commands)
				{
					string magicCommand = MakeMagicCommand(command);
					CommandMessaging.EMsgResult msgResult = commandMessaging.SendCommandMessage(magicCommand);

					switch (msgResult)
					{
						case CommandMessaging.EMsgResult.OK:
							// nothing to report
							break;

						case CommandMessaging.EMsgResult.DmtNotFound:
							Console.WriteLine("Couldn't find a running version of DMT");
							break;

						case CommandMessaging.EMsgResult.CmdUnknown:
							Console.WriteLine("The command \"" + command + "\" is unknown");
							break;

						default:
							Console.WriteLine("Unknown error from command: " + ((int)msgResult).ToString());
							break;
					}
				}
			}
		}

		// make sure that the command starts with "DMT:"
		string MakeMagicCommand(string command)
		{
			string magicCommandPrefix = MagicCommand.MagicCommandPrefix;

			if (command.StartsWith(magicCommandPrefix, StringComparison.OrdinalIgnoreCase))
			{
				return command;
			}
			else
			{
				return magicCommandPrefix + command;
			}
		}

		void ShowUsage()
		{
			Console.WriteLine("");
			Console.WriteLine("Usage: DMT [options] [commands]");
			Console.WriteLine(" Without any options or commands, runs Dual Monitor Tools as a GUI application");
			Console.WriteLine(" in the notification area");
			Console.WriteLine(" If commands are specified, DMT must already be running as a GUI application");
			Console.WriteLine();
			Console.WriteLine("Options:");
			Console.WriteLine("  -?   show usage");
			Console.WriteLine("  -v   show version");
			Console.WriteLine("  -x   close down DMT running in the notification area");
			Console.WriteLine();
			Console.WriteLine("Commands:");
			Console.WriteLine("  General:Options             Show Options");
			Console.WriteLine("  \"General:ChangePrimary 2\"   Make screen 2 the primary screen");
			Console.WriteLine("  Cursor:FreeCursor           Cursor free to move between screens");
			Console.WriteLine("  Cursor:StickyCursor         Cursor movement between screens is sticky");
			Console.WriteLine("  + many more");
			Console.WriteLine("");
		}

		void ShowVersion()
		{
			System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
			System.Diagnostics.FileVersionInfo fileVersionInfo = System.Diagnostics.FileVersionInfo.GetVersionInfo(assembly.Location);
			string version = fileVersionInfo.ProductVersion;
			Console.WriteLine(string.Format("DMT {0}", version));
		}
	}
}
