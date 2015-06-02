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
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMT.Library.WallpaperPlugin
{
	/// <summary>
	/// Utility class to help plugins
	/// </summary>
	static public class ProviderHelper
	{
		public static string ConfigToString(Dictionary<string, string> config, string key, string defaultValue = "")
		{
			string value;

			if (config.TryGetValue(key, out value))
			{
				return value;
			}

			return defaultValue;
		}

		public static bool ConfigToBool(Dictionary<string, string> config, string key, bool defaultValue = false)
		{
			string value;

			if (config.TryGetValue(key, out value))
			{
				bool isTrue;
				if (bool.TryParse(value, out isTrue))
				{
					return isTrue;
				}
			}

			return defaultValue;
		}

		public static int ConfigToInt(Dictionary<string, string> config, string key, int defaultValue = 0)
		{
			string value;

			if (config.TryGetValue(key, out value))
			{
				int number;
				if (int.TryParse(value, out number))
				{
					return number;
				}
			}

			return defaultValue;
		}
	}
}
