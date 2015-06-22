using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace DMT.Modules
{
	public interface ICommandRunner
	{
		bool RunInternalCommand(string command, string parameters);
		bool IsInternalCommand(string command);
		Icon GetInternalCommandIcon(string command);
		IEnumerable<string> GetModuleNames();
		IEnumerable<string> GetModuleCommandNames(string moduleName);
		string GetModuleActionDescription(string moduleName, string action);
	}
}
