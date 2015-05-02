using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMT.Modules
{
	class ModuleOptionNode
	{
		public string Name { get; protected set; }

		public ModuleOptionNode(string name)
		{
			Name = name;
		}
	}
}
