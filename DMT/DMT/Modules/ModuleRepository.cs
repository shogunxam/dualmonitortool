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
	using System.Threading.Tasks;
	using System.Windows.Forms;

	using DMT.Library.Command;

	/// <summary>
	/// Repository to hold all available modules
	/// </summary>
	class ModuleRepository : IModuleService, ICommandRunner
	{
		List<Module> _modules = new List<Module>();

		/// <summary>
		/// Initialises a new instance of the <see cref="ModuleRepository" /> class.
		/// </summary>
		public ModuleRepository()
		{
		}

		/// <summary>
		/// Adds the module to the repository
		/// </summary>
		/// <param name="module">Module to add</param>
		public void AddModule(Module module)
		{
			_modules.Add(module);
		}

		/// <summary>
		/// Gets an enumerator to enumerate over all modules
		/// </summary>
		/// <returns>Module enumerator</returns>
		public IEnumerable<Module> GetAllModules()
		{
			return _modules;
		}

		/// <summary>
		/// Gets all of the option nodes for all of the modules
		/// </summary>
		/// <returns>All option nodes</returns>
		public IEnumerable<ModuleOptionNode> GetOptionNodes()
		{
			List<ModuleOptionNode> optionNodes = new List<ModuleOptionNode>();
			foreach (Module module in _modules)
			{
				optionNodes.Add(module.GetOptionNodes());
			}

			return optionNodes;
		}

		/// <summary>
		/// Start all modules in the repository
		/// </summary>
		public void StartAllModules()
		{
			foreach (Module module in _modules)
			{
				module.Start();
			}
		}

		/// <summary>
		/// Second stage start up.
		/// This is to let the modules know that the other modules have been started
		/// so they can make use of them if required.
		/// </summary>
		public void StartUpComplete()
		{
			foreach (Module module in _modules)
			{
				module.StartUpComplete();
			}
		}

		/// <summary>
		/// Informs modules that the display resolution has changed
		/// </summary>
		public void DisplayResolutionChanged()
		{
			foreach (Module module in _modules)
			{
				module.DisplayResolutionChanged();
			}
		}

		/// <summary>
		/// Let all modules flush any data out to disk
		/// </summary>
		public void FlushAllModules()
		{
			foreach (Module module in _modules)
			{
				module.Flush();
			}
		}

		/// <summary>
		/// Terminates all modules
		/// </summary>
		public void TerminateAllModules()
		{
			foreach (Module module in _modules)
			{
				module.Terminate();
			}
		}

		#region ICommandRunner support
		/// <summary>
		/// Runs the internal command
		/// </summary>
		/// <param name="command">The command name</param>
		/// <param name="parameters">Any parameters for the command</param>
		/// <returns>True if found and ran</returns>
		public bool RunInternalCommand(string command, string parameters)
		{
			string moduleName;
			string commandName;
			if (MagicCommand.SplitMagicCommand(command, out moduleName, out commandName))
			{
				Module module = FindModule(moduleName);
				if (module != null)
				{
					return module.RunCommand(commandName, parameters);
				}
			}

			return false;
		}

		/// <summary>
		/// Checks that the command name is a valid internal command
		/// </summary>
		/// <param name="command">The command name</param>
		/// <returns>True if valid</returns>
		public bool IsInternalCommand(string command)
		{
			string moduleName;
			string commandName;
			return MagicCommand.SplitMagicCommand(command, out moduleName, out commandName);
		}

		/// <summary>
		/// Gets an icon corresponding to the internal command
		/// </summary>
		/// <param name="command">The command name</param>
		/// <returns>Icon for command, or null if not found</returns>
		public Icon GetInternalCommandIcon(string command)
		{
			string moduleName;
			string commandName;
			if (MagicCommand.SplitMagicCommand(command, out moduleName, out commandName))
			{
				Module module = FindModule(moduleName);
				if (module != null)
				{
					return module.GetModuleIcon();
				}
			}

			return null;
		}

		/// <summary>
		/// Gets a list of available module names that have commands
		/// </summary>
		/// <returns>List of available modules</returns>
		public IEnumerable<string> GetModuleNames()
		{
			List<string> moduleNames = new List<string>();
			foreach (Module module in _modules)
			{
				// we could exclude modules that don't have any internal commands
				moduleNames.Add(module.ModuleName);
			}

			return moduleNames;
		}

		/// <summary>
		/// Gets a list of actions available for given module
		/// </summary>
		/// <param name="moduleName">The module name</param>
		/// <returns>List of available actions</returns>
		public IEnumerable<string> GetModuleCommandNames(string moduleName)
		{
			Module module = FindModule(moduleName);
			if (module != null)
			{
				return module.GetActions();
			}

			return null;
		}

		/// <summary>
		/// Gets a description for the module 
		/// </summary>
		/// <param name="moduleName">The module name</param>
		/// <param name="actionName">The action name</param>
		/// <returns>A description of the command</returns>
		public string GetModuleActionDescription(string moduleName, string actionName)
		{
			Module module = FindModule(moduleName);
			if (module != null)
			{
				Command command = module.FindCommand(actionName);
				if (command != null)
				{
					return command.Description;
				}
			}

			return null;
		}

		Module FindModule(string moduleName)
		{
			foreach (Module module in _modules)
			{
				if (string.Compare(module.ModuleName, moduleName, true) == 0)
				{
					return module;
				}
			}

			return null;
		}
		#endregion ICommandRunner support
	}
}
