using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMT.Modules
{
	class ModuleOptionNode
	{
		public string Name { get; protected set; }

		public Image Image { get; set; }

		public ModuleOptionNode(string name, Image image = null)
		{
			Name = name;
			Image = image;
		}
	}
}
