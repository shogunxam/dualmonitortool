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

namespace DMT.Library.Command
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;

	/// <summary>
	/// A command that is executed via Magic Words
	/// </summary>
	class MagicCommand
	{
		const string TheMagicCommandPrefix = "DMT:";

		/// <summary>
		/// Gets the magic command prefix
		/// </summary>
		public static string MagicCommandPrefix 
		{ 
			get 
			{ 
				return TheMagicCommandPrefix; 
			} 
		}

		/// <summary>
		/// Splits a magic command into the module name and action to be performed in that module
		/// </summary>
		/// <param name="command">The command</param>
		/// <param name="moduleName">The module name</param>
		/// <param name="actionName">The action name</param>
		/// <returns>true if in correct format</returns>
		public static bool SplitMagicCommand(string command, out string moduleName, out string actionName)
		{
			if (command.StartsWith(TheMagicCommandPrefix))
			{
				string moduleAction = command.Substring(TheMagicCommandPrefix.Length);

				// split what's left into the module name and the action to be performed
				int idx = moduleAction.IndexOf(':');
				if (idx > 0)
				{
					moduleName = moduleAction.Substring(0, idx);
					actionName = moduleAction.Substring(idx + 1);

					return true;
				}
			}

			moduleName = null;
			actionName = null;
			return false;
		}

		/// <summary>
		/// Creates a command name from module and action names
		/// </summary>
		/// <param name="moduleName">Module name</param>
		/// <param name="actionName">Action name</param>
		/// <returns>Full command name</returns>
		public static string JoinMagicCommand(string moduleName, string actionName)
		{
			return TheMagicCommandPrefix + moduleName + ":" + actionName;
		}
	}
}
