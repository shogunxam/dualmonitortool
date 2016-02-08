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
	using System.Linq;
	using System.Text;

	using DMT.Library.HotKeys;

	/// <summary>
	/// This describes an action that one of the modules can perform 
	/// when either activated by a hotkey or a magic word
	/// </summary>
	class Command
	{
		/// <summary>
		/// Initialises a new instance of the <see cref="Command" /> class.
		/// </summary>
		/// <param name="name">Name of the command</param>
		/// <param name="description">Description of the command</param>
		/// <param name="win7Key">Windows 7 key</param>
		/// <param name="handler">Parameter less handler</param>
		/// <param name="hotKey">True if can be used from a hot key</param>
		/// <param name="magicWord">True if can be used from a magic word</param>
		public Command(string name, string description, string win7Key, HotKey.HotKeyHandler handler, bool hotKey = true, bool magicWord = true)
		{
			Name = name;
			Description = description;
			Handler = handler;
			HandlerWithParameters = null;
			RegisterHotKey = hotKey;
			RegisterMagicWord = magicWord;
		}

		/// <summary>
		/// Initialises a new instance of the <see cref="Command" /> class.
		/// </summary>
		/// <param name="name">Name of the command</param>
		/// <param name="description">Description of the command</param>
		/// <param name="win7Key">Windows 7 key</param>
		/// <param name="handler">Parameter less handler</param>
		/// <param name="handlerWithParams">Handler that takes parameters</param>
		/// <param name="hotKey">True if can be used from a hot key</param>
		/// <param name="magicWord">True if can be used from a magic word</param>
		public Command(string name, string description, string win7Key, HotKey.HotKeyHandler handler, CommandHandlerWithParameters handlerWithParams, bool hotKey = true, bool magicWord = true)
		{
			Name = name;
			Description = description;
			Handler = handler;
			HandlerWithParameters = handlerWithParams;
			RegisterHotKey = hotKey;
			RegisterMagicWord = magicWord;
		}

		/// <summary>
		/// Delegate for a command handler with parameters
		/// </summary>
		/// <param name="parameters">Parameters for command</param>
		public delegate void CommandHandlerWithParameters(string parameters);

		/// <summary>
		/// Gets or sets the command name
		/// </summary>
		public string Name { get; protected set; }

		/// <summary>
		/// Gets or sets the command description
		/// </summary>
		public string Description { get; set; }	

		/// <summary>
		/// Gets or sets the windows 7 key
		/// </summary>
		public string Win7Key { get; protected set; }

		/// <summary>
		/// Gets or sets the command handler
		/// </summary>
		public HotKey.HotKeyHandler Handler { get; set; }

		/// <summary>
		/// Gets or sets a command handler that takes parameters
		/// </summary>
		public CommandHandlerWithParameters HandlerWithParameters { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this command can be used from a hot key
		/// </summary>
		public bool RegisterHotKey { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this command can be used from a magic word
		/// </summary>
		public bool RegisterMagicWord { get; set; }
	}
}
