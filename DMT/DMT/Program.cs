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

using DMT.Library.PInvoke;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DMT
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main(string[] args)
		{
			ProgramOptions programOptions = new ProgramOptions(args);

			if (programOptions.CmdMode)
			{
				ConsoleMain(programOptions);
			}
			else
			{
				GuiMain(programOptions);
			}
		}


		static void ConsoleMain(ProgramOptions programOptions)
		{
			Win32.AttachConsole(-1);
			ConsoleApplication consoleApplication = new ConsoleApplication(programOptions);
			consoleApplication.Run();
			Win32.FreeConsole();
		}

		static void GuiMain(ProgramOptions programOptions)
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			AppForm appForm = new AppForm();
			Application.Run();
		}


	}
}
