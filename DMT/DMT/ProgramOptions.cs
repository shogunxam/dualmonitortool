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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DMT
{
	class ProgramOptions
	{

		/// <summary>
		/// Indicates if running in command line mode
		/// </summary>
		public bool CmdMode { get; protected set; }

		/// <summary>
		/// Show usage and exit
		/// </summary>
		public bool ShowUsage { get; protected set; }

		/// <summary>
		/// Show version and exit
		/// </summary>
		public bool ShowVersion { get; protected set; }

		/// <summary>
		/// Set if command should be run by local instance of DMT
		/// - NOT IMPLEMENTED YET
		/// </summary>
		public bool RunCommandLocally { get; set; }

		/// <summary>
		/// Command to run
		/// </summary>
		public List<string> Commands { get; set; }

		/// <summary>
		/// Option specification errors
		/// </summary>
		public List<string> Errors { get; protected set; }

				/// <summary>
		/// Ctor that takes the list of command line arguments passed into us
		/// </summary>
		/// <param name="args">Command line arguments</param>
		public ProgramOptions(string[] args)
		{
			int argIndex = 0;

			Commands = new List<string>();
			Errors = new List<string>();

			while (argIndex < args.Length)
			{
				// at the moment any option/argument will force it into command line mode,
				// but at some time in the future, we may want to allow options for gui mode?
				CmdMode = true;

				string arg = args[argIndex];
				if (arg.Length > 1 && arg[0] == '-')
				{
					for (int i = 1; i < arg.Length; i++)
					{
						if (arg[i] == '?')
						{
							ShowUsage = true;
						}
						else if (arg[i] == 'v')
						{
							ShowVersion = true;
						}
						else
						{
							Errors.Add(string.Format("Unrecognised option: \"{0}\"", arg[i]));
						}
					}
				}
				else
				{
					Commands.Add(arg);
				}

				argIndex++;
			}

			// perform validation
			if (CmdMode && !ShowUsage && !ShowVersion)
			{
			}
		}

	}
}
