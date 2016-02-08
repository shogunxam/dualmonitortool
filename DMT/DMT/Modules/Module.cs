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

	using DMT.Library.HotKeys;
		
	/// <summary>
	/// base class for all modules
	/// </summary>
	abstract class Module
	{
		/// <summary>
		/// Hot key service
		/// </summary>
		protected IHotKeyService _hotKeyService;

		/// <summary>
		/// Commands handled by this module
		/// </summary>
		protected List<Command> _commands = new List<Command>();

		Icon _icon;
		List<HotKeyController> _hotKeyControllers = new List<HotKeyController>();

		/// <summary>
		/// Initialises a new instance of the <see cref="Module" /> class.
		/// </summary>
		/// <param name="hotKeyService">Service for registering hot keys</param>
		public Module(IHotKeyService hotKeyService)
		{
			_hotKeyService = hotKeyService;
			Enabled = true;
			_icon = null;
		}

		/// <summary>
		/// Gets or sets a value indicating whether the module is enabled
		/// </summary>
		public bool Enabled { get; set; }

		/// <summary>
		/// Gets or sets the module name
		/// </summary>
		public string ModuleName { get; protected set; }

		/// <summary>
		/// Gets the option nodes for this module
		/// </summary>
		/// <returns>The root node</returns>
		public abstract ModuleOptionNode GetOptionNodes();

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
		/// Called when the display resolution changes
		/// </summary>
		public virtual void DisplayResolutionChanged()
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

		/// <summary>
		/// Gets the icon for the module
		/// </summary>
		/// <returns></returns>
		public Icon GetModuleIcon()
		{
			// these are loaded on demand to minimise startup time
			if (_icon == null)
			{
				_icon = (Icon)Properties.Resources.ResourceManager.GetObject(ModuleName);
			}

			return _icon;
		}
		/// <summary>
		/// Runs the given command with parameters
		/// </summary>
		/// <param name="commandName"></param>
		/// <param name="parameters"></param>
		/// <returns></returns>
		public bool RunCommand(string commandName, string parameters)
		{
			bool commandRan = false;

			foreach (Command command in _commands)
			{
				if (command.RegisterMagicWord)
				{
					if (string.Compare(commandName, command.Name, true) == 0)
					{
						if (string.IsNullOrWhiteSpace(parameters))
						{
							if (command.Handler != null)
							{
								command.Handler();
								commandRan = true;
							}
						}
						else
						{
							if (command.HandlerWithParameters != null)
							{
								command.HandlerWithParameters(parameters);
								commandRan = true;
							}
						}
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

		/// <summary>
		/// Adds the command for this module
		/// </summary>
		/// <param name="name">Name of command</param>
		/// <param name="description">Description of command</param>
		/// <param name="win7Key">Windows 7 key</param>
		/// <param name="handler">Handler to perform action</param>
		/// <param name="hotKey">True if hot key is supported for command</param>
		/// <param name="magicWord">True if magic word is supported for command</param>
		/// <returns>Controller for the hot key</returns>
		protected HotKeyController AddCommand(string name, string description, string win7Key, HotKey.HotKeyHandler handler, bool hotKey = true, bool magicWord = true)
		{
			Command command = new Command(name, description, win7Key, handler, hotKey, magicWord);
			return AddCommand(command);
		}

		/// <summary>
		/// Adds the command for this module
		/// </summary>
		/// <param name="name">Name of command</param>
		/// <param name="description">Description of command</param>
		/// <param name="win7Key">Windows 7 key</param>
		/// <param name="handler">Handler to perform action</param>
		/// <param name="handlerWithParameters">Handler that takes parameters to perform action</param>
		/// <param name="hotKey">True if hot key is supported for command</param>
		/// <param name="magicWord">True if magic word is supported for command</param>
		/// <returns>Controller for the hot key</returns>
		protected HotKeyController AddCommand(string name, string description, string win7Key,
			HotKey.HotKeyHandler handler, Command.CommandHandlerWithParameters handlerWithParameters,
			bool hotKey = true, bool magicWord = true)
		{
			Command command = new Command(name, description, win7Key, handler, handlerWithParameters, hotKey, magicWord);
			return AddCommand(command);
		}

		/// <summary>
		/// Adds the command for this module
		/// </summary>
		/// <param name="command">Command to add</param>
		/// <returns>Controller for the hot key</returns>
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

		/// <summary>
		/// Creates the hot key controller
		/// </summary>
		/// <param name="command">Command we are creating the controller for</param>
		/// <returns>Hot key controller</returns>
		protected HotKeyController CreateHotKeyController(Command command)
		{
			string settingName = command.Name + "HotKey";
			return _hotKeyService.CreateHotKeyController(ModuleName, settingName, command.Description, command.Win7Key, command.Handler);
		}
	}
}
