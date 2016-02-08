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

namespace DMT.Modules
{
	using System;
	using System.Collections.Generic;
	using System.Drawing;
	using System.Linq;
	using System.Text;

	/// <summary>
	/// Interface for a command runner
	/// </summary>
	public interface ICommandRunner
	{
		/// <summary>
		/// Runs the internal command
		/// </summary>
		/// <param name="command">The command name</param>
		/// <param name="parameters">Any parameters for the command</param>
		/// <returns>True if found and ran</returns>
		bool RunInternalCommand(string command, string parameters);

		/// <summary>
		/// Checks that the command name is a valid internal command
		/// </summary>
		/// <param name="command">The command name</param>
		/// <returns>True if valid</returns>
		bool IsInternalCommand(string command);

		/// <summary>
		/// Gets an icon corresponding to the internal command
		/// </summary>
		/// <param name="command">The command name</param>
		/// <returns>Icon for command, or null if not found</returns>
		Icon GetInternalCommandIcon(string command);

		/// <summary>
		/// Gets a list of available module names that have commands
		/// </summary>
		/// <returns>List of available modules</returns>
		IEnumerable<string> GetModuleNames();

		/// <summary>
		/// Gets a list of actions available for given module
		/// </summary>
		/// <param name="moduleName">The module name</param>
		/// <returns>List of available actions</returns>
		IEnumerable<string> GetModuleCommandNames(string moduleName);

		/// <summary>
		/// Gets a description for the module 
		/// </summary>
		/// <param name="moduleName">The module name</param>
		/// <param name="action">The action name</param>
		/// <returns>A description of the command</returns>
		string GetModuleActionDescription(string moduleName, string action);
	}
}
