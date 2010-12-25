using System;
using System.Collections.Generic;
using System.Text;

namespace DualLauncher
{
	/// <summary>
	/// Map of (parameter name, parameter value) pairs for dynamic input parameters
	/// </summary>
	public class ParameterMap
	{
		private Dictionary<string, string> map = new Dictionary<string, string>();

		public ParameterMap()
		{
		}

		/// <summary>
		/// Gets the value for a particular parameter name.
		/// Returns null if parameter name not found.
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		public string GetValue(string name)
		{
			if (map.ContainsKey(name))
			{
				return map[name];
			}

			return null;
		}

		/// <summary>
		/// Saves the parameter value for the parameter name.
		/// </summary>
		/// <param name="name"></param>
		/// <param name="value"></param>
		public void SetValue(string name, string value)
		{
			map[name] = value;
		}
	}
}
