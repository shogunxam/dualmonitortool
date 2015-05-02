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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DMT.Modules
{
	class ModuleRepository : IModuleService
	{
		List<Module> _modules = new List<Module>();

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

		public List<ModuleOptionNode> GetOptionNodes(Form form)
		{
			List<ModuleOptionNode> optionNodes = new List<ModuleOptionNode>();
			foreach (Module module in _modules)
			{
				optionNodes.Add(module.GetOptionNodes(/*form*/));
			}

			return optionNodes;
		}

		public void TerminateAllModules()
		{
			foreach (Module module in _modules)
			{
				module.Terminate();
			}
		}
	}
}
