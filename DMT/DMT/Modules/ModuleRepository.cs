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
		//const string _internalCommandPrefix = "DMT:";

		//#region Singleton support
		//// the single instance of this object
		//static readonly ModuleRepository instance = new ModuleRepository();

		//// Explicit static constructor to tell C# compiler
		//// not to mark type as beforefieldinit
		//static ModuleRepository()
		//{
		//}

		//ModuleRepository()
		//{
		//	InitModuleList();
		//}

		//public static ModuleRepository Instance
		//{
		//	get
		//	{
		//		return instance;
		//	}
		//}
		//#endregion

		public ModuleRepository()
		{
			//InitModuleList();
		}

		//void InitModuleList()
		//{
		//	_modules = new List<Module>();

		//	_modules.Add(new GeneralModule());
		//	_modules.Add(new SwapScreen.SwapScreenModule());
		//	_modules.Add(new Cursor.CursorModule());
		//}

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
		public void RunInternalCommand(string command, string parameters)
		{
			string moduleName;
			string commandName;
			if (MagicCommand.SplitMagicCommand(command, out moduleName, out commandName))
			{
				Module module = FindModule(moduleName);
				if (module != null)
				{
					module.RunCommand(commandName, parameters);
				}
			}
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

		//bool SplitInternalCommand(string command, out string moduleName, out string commandName)
		//{
		//	if (command.StartsWith(_internalCommandPrefix))
		//	{
		//		string moduleAction = command.Substring(_internalCommandPrefix.Length);

		//		// split what's left into the module name and the action to be performed
		//		int idx = moduleAction.IndexOf(':');
		//		if (idx > 0)
		//		{
		//			moduleName = moduleAction.Substring(0, idx);
		//			commandName = moduleAction.Substring(idx + 1);

		//			return true;
		//		}
		//	}

		//	moduleName = null;
		//	commandName = null;
		//	return false;
		//}

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
