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

using DMT.Library.HotKeys;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DMT.Modules
{
	abstract class Module
	{
		protected IHotKeyService _hotKeyService;

		public bool Enabled { get; set; }
		public string ModuleName { get; protected set; }
		Icon _icon;
		protected List<Command> _commands = new List<Command>();
		List<HotKeyController> _hotKeyControllers = new List<HotKeyController>();


		public Module(IHotKeyService hotKeyService)
		{
			_hotKeyService = hotKeyService;
			Enabled = true;
			_icon = null;
		}

		public abstract ModuleOptionNode GetOptionNodes(/*Form form*/ );

		//public abstract void RunCommand(string commandName, string parameters);

		/// <summary>
		/// Starts the module up
		/// </summary>
		public virtual void Start()
		{
		}

		/// <summary>
		/// Indicates all modules have started, and allows
		/// individual modules to perform any post startup processing
		/// that depends on other modules
		/// </summary>
		public virtual void StartUpComplete()
		{
		}

		/// <summary>
		/// Gives the module a chance to flush any data out to disk
		/// Will be called if the app is closing or system about to shutdown
		/// </summary>
		public virtual void Flush()
		{
		}

		/// <summary>
		/// Terminates the module
		/// </summary>
		public virtual void Terminate()
		{
		}

		public Icon GetModuleIcon()
		{
			// these are loaded on demand to minimise startup time
			if (_icon == null)
			{
				_icon = (Icon)Properties.Resources.ResourceManager.GetObject(ModuleName);
			}

			return _icon;
		}

		//protected void RegisterHotKeys()
		//{
		//	foreach (Command command in _commands)
		//	{
		//		if (command.RegisterHotKey)
		//		{
		//			string settingName = command.Name + "HotKey";
		//			HotKeyController hotKeyController = _hotKeyService.CreateHotKeyController(ModuleName, settingName, command.Description, command.Win7Key, command.Handler);
		//			_hotKeyControllers.Add(hotKeyController);
		//		}
		//	}
		//}



		protected HotKeyController AddCommand(string name, string description, string win7Key, HotKey.HotKeyHandler handler, bool hotKey = true, bool magicWord = true)
		{
			Command command = new Command(name, description, win7Key, handler, hotKey, magicWord);
			return AddCommand(command);
		}

		protected HotKeyController AddCommand(Command command)
		{
			if (command.RegisterMagicWord)
			{
				_commands.Add(command);
			}
			if (command.RegisterHotKey)
			{
				string settingName = command.Name + "HotKey";
				return _hotKeyService.CreateHotKeyController(ModuleName, settingName, command.Description, command.Win7Key, command.Handler);
			}
			else
			{
				return null;
			}
		}
		protected HotKeyController CreateHotKeyController(Command command)
		{
			string settingName = command.Name + "HotKey";
			return _hotKeyService.CreateHotKeyController(ModuleName, settingName, command.Description, command.Win7Key, command.Handler);
		}

		public bool RunCommand(string commandName, string parameters)
		{
			// 'parameters' not currently used, but may have a use for them in the future

			bool commandRan = false;

			foreach (Command command in _commands)
			{
				if (command.RegisterMagicWord)
				{
					if (string.Compare(commandName, command.Name, true) == 0)
					{
						command.Handler();
						commandRan = true;
					}
				}
			}

			return commandRan;
		}

		public IEnumerable<string> GetActions()
		{
			List<string> actionNames = new List<string>();
			foreach (Command command in _commands)
			{
				if (command.RegisterMagicWord)
				{
					actionNames.Add(command.Name);
				}
			}

			return actionNames;
		}

		public Command FindCommand(string actionName)
		{
			foreach (Command command in _commands)
			{
				if (string.Compare(command.Name, actionName, true) == 0)
				{
					return command;
				}
			}

			return null;
		}

	}
}
