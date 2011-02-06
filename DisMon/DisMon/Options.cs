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
	/// <summary>
	/// The command line options.
	/// </summary>
	class Options
	{
		// list of options for the individual monitors 
		private Dictionary<int, string> monOps = new Dictionary<int, string>();

		// the external command and its parameters that we need to run
		private List<string> processArgs = new List<string>();

		/// <summary>
		/// The command that needs to be run
		/// </summary>
		public string CmdName
		{
			get { return GetCmdName(); }
		}

		/// <summary>
		/// The parameters for the command
		/// </summary>
		public string CmdArgs
		{
			get { return GetCmdArgs(); }
		}

		// Debug flag - not currently used
		private bool debug;
		public bool Debug
		{
			get { return debug; }
		}

		// restore monitors when existing flag (true by default)
		private bool restore = true;
		public bool Restore
		{
			get { return restore; }
		}

		// sets monitors and exits immediatedly
		private bool exitImmediatedly = false;
		public bool ExitImmediatedly
		{
			get { return exitImmediatedly; }
		}
	
		/// <summary>
		/// Ctor that takes the list of command line arguments passed into us
		/// </summary>
		/// <param name="args">Command line arguments</param>
		public Options(string[] args)
		{
			int argIndex = 0;

			while (argIndex < args.Length)
			{
				string arg = args[argIndex];
				if (arg.Length > 1 && arg[0] == '-')
				{
					// first pick up the monitor number (1 based)
					int monIndex = 0;
					int strIndex = 0;
					for (strIndex = 1; strIndex < arg.Length; strIndex++)
					{
						if (Char.IsDigit(arg[strIndex]))
						{
							monIndex = monIndex * 10 + arg[strIndex] - '0';
						}
						else
						{
							break;
						}
					}

					if (monIndex > 0)
					{
						// pick up the rest of the string
						string ops = arg.Substring(strIndex);
						// make monitor index zero based
						monOps.Add(monIndex - 1, ops);
					}
					else if (arg.Substring(1) == "d")
					{
						debug = true;
					}
					else if (arg.Substring(1) == "n")
					{
						restore = false;
					}
					else if (arg.Substring(1) == "x")
					{
						exitImmediatedly = true;
						// this also forces the restore flag off
						restore = false;
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

		/// <summary>
		/// Gets the string representing the monitor operations for
		/// the specified monitor.
		/// If there are none, an empty string is returned.
		/// </summary>
		/// <param name="monitorIndex">Zero based monitor index</param>
		/// <returns>Monitor operations for the monitor</returns>
		public string GetMonOps(int monitorIndex)
		{
			string ret;

			if (!monOps.TryGetValue(monitorIndex, out ret))
			{
				ret = "";
			}

			return ret;
		}

		/// <summary>
		/// Indicates if there are any monitor specific operations
		/// </summary>
		/// <returns>true, if there are any monitor specific operations</returns>
		public bool HasMonOps()
		{
			return (monOps.Count > 0);
		}
	
		private string GetCmdName()
		{
			string cmdName = null;
			if (processArgs.Count > 0)
			{
				cmdName = processArgs[0];
			}

			return cmdName;
		}

		private string GetCmdArgs()
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
