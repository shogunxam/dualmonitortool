using System;
using System.Collections.Generic;
using System.Text;

namespace DualLauncher
{
	public class ParameterMap
	{
		private Dictionary<string, string> map = new Dictionary<string, string>();

		public ParameterMap()
		{
		}

		public string GetValue(string name)
		{
			if (map.ContainsKey(name))
			{
				return map[name];
			}

			return null;
		}

		public void SetValue(string name, string value)
		{
			map[name] = value;
		}
	}
}
