using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DMT.Library.Command
{
	/// <summary>
	/// A command that is executed via Magic Words
	/// </summary>
	public class MagicCommand
	{
		const string _magicCommandPrefix = "DMT:";

		public static bool SplitMagicCommand(string command, out string moduleName, out string actionName)
		{
			if (command.StartsWith(_magicCommandPrefix))
			{
				string moduleAction = command.Substring(_magicCommandPrefix.Length);

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

		public static string JoinMagicCommand(string moduleName, string actionName)
		{
			return _magicCommandPrefix + moduleName + ":" + actionName;
		}
	}
}
