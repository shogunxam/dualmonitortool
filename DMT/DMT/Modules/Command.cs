using DMT.Library.HotKeys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DMT.Modules
{
	/// <summary>
	/// This describes an action that one of the modules can perform 
	/// when either activated by a hotkey or a magic word
	/// </summary>
	class Command
	{
		public string Name { get; protected set; }
		public string Description { get; set; }	// can be changed dynamically for UDAs
		public string Win7Key { get; protected set; }
		public HotKey.HotKeyHandler Handler { get; set; }	// needs to be set by UDA
		public bool RegisterHotKey { get; set; }
		public bool RegisterMagicWord { get; set; }

		public Command(string name, string description, string win7Key, HotKey.HotKeyHandler handler, bool hotKey = true, bool magicWord = true)
		{
			Name = name;
			Description = description;
			Handler = handler;
			RegisterHotKey = hotKey;
			RegisterMagicWord = magicWord;
		}
	}
}
