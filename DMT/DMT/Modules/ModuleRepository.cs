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

using DMT.Library.Command;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DMT.Modules
{
	class ModuleRepository : IModuleService, ICommandRunner
	{
		List<Module> _modules = new List<Module>();

		public ModuleRepository()
		{
		}

		public void AddModule(Module module)
		{
			_modules.Add(module);
		}

		public IEnumerable<Module> GetAllModules()
		{
			return _modules;
		}

		public IEnumerable<ModuleOptionNode> GetOptionNodes(Form form)
		{
			List<ModuleOptionNode> optionNodes = new List<ModuleOptionNode>();
			foreach (Module module in _modules)
			{
				optionNodes.Add(module.GetOptionNodes(/*form*/));
			}

			return optionNodes;
		}

		public void StartAllModules()
		{
			foreach (Module module in _modules)
			{
				module.Start();
			}
		}

		public void StartUpComplete()
		{
			foreach (Module module in _modules)
			{
				module.StartUpComplete();
			}
		}

		public void DisplayResolutionChanged()
		{
			foreach (Module module in _modules)
			{
				module.DisplayResolutionChanged();
			}
		}

		public void FlushAllModules()
		{
			foreach (Module module in _modules)
			{
				module.Flush();
			}
		}

		public void TerminateAllModules()
		{
			foreach (Module module in _modules)
			{
				module.Terminate();
			}
		}

		#region ICommandRunner support
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

		public bool IsInternalCommand(string command)
		{
			string moduleName;
			string commandName;
			return MagicCommand.SplitMagicCommand(command, out moduleName, out commandName);
		}

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

		public IEnumerable<string> GetModuleCommandNames(string moduleName)
		{
			Module module = FindModule(moduleName);
			if (module != null)
			{
				return module.GetActions();
			}

			return null;
		}

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

		#endregion ICommandRunner support

	}
}
