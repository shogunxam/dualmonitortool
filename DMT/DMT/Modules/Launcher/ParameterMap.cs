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

using System;
using System.Collections.Generic;
using System.Text;

namespace DMT.Modules.Launcher
{
	/// <summary>
	/// Map of (parameter name, parameter value) pairs for dynamic input parameters
	/// </summary>
	public class ParameterMap
	{
		Dictionary<string, string> _map = new Dictionary<string, string>();

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
			if (_map.ContainsKey(name))
			{
				return _map[name];
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
			_map[name] = value;
		}
	}
}
