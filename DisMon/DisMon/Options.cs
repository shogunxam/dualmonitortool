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
using System.Text;
using System.Threading;

namespace DisMon
{
	class Options
	{
		private List<string> processArgs = new List<string>();

		public string CmdName
		{
			get { return GetCmdName(); }
		}

		public string CmdArgs
		{
			get { return GetCmdArgs(); }
		}

		private bool debug;
		public bool Debug
		{
			get { return debug; }
		}
	
	
		public Options(string[] args)
		{
			int argIndex = 0;

			while (argIndex < args.Length)
			{
				string arg = args[argIndex];
				if (arg.Length > 0 && arg[0] == '-')
				{
					// process option
					if (arg.Substring(1) == "d")
					{
						debug = true;
					}
					argIndex++;
				}
				else
				{
					// end of options
					break;
				}
			}

			// anything remaining is considered to be the command we are going to run
			while (argIndex < args.Length)
			{
				processArgs.Add(args[argIndex]);
				argIndex++;
			}

		}

		string GetCmdName()
		{
			string cmdName = null;
			if (processArgs.Count > 0)
			{
				cmdName = processArgs[0];
			}

			return cmdName;
		}

		string GetCmdArgs()
		{
			string cmdArgs = null;

			if (processArgs.Count > 1)
			{
				cmdArgs = "";
				for (int i = 1; i < processArgs.Count; i++)
				{
					string arg = processArgs[i];
					if (cmdArgs.Length > 0)
					{
						cmdArgs += " ";
						if (arg.IndexOf(' ') > 0)
						{
							// argument contains embedded spaces
							cmdArgs += "\"";
							cmdArgs += arg;
							cmdArgs += "\"";
						}
						else
						{
							cmdArgs += arg;
						}
					}
				}
			}

			return cmdArgs;
		}
	}
}
