﻿#region copyright
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

namespace DualMonitorTools.DualWallpaperChanger.LocalDisk
{
	/// <summary>
	/// Configuration required for each provider from the LocalDisk plugin
	/// </summary>
	public class LocalDiskConfig
	{
		public LocalDiskConfig()
		{
		}

		public LocalDiskConfig(Dictionary<string, string> configDictionary)
		{
			Weight = ProviderHelper.ConfigToInt(configDictionary, "weight", 10);
			Description = ProviderHelper.ConfigToString(configDictionary, "description", "Windows Wallpaper from local disk");
			Directory = ProviderHelper.ConfigToString(configDictionary, "directory", @"C:\Windows\Web\Wallpaper");
			Recursive = ProviderHelper.ConfigToBool(configDictionary, "recursive", true);
			Rescan = ProviderHelper.ConfigToBool(configDictionary, "rescan", false);
		}

		public int Weight { get; set; }
		public string Description { get; set; }
		public string Directory { get; set; }
		public bool Recursive { get; set; }
		public bool Rescan { get; set; }

		public Dictionary<string, string> ToDictionary()
		{
			Dictionary<string, string> configDictionary = new Dictionary<string, string>();
			configDictionary["weight"] = Weight.ToString();
			configDictionary["description"] = Description;
			configDictionary["directory"] = Directory;
			configDictionary["recursive"] = Recursive.ToString();
			configDictionary["rescan"] = Rescan.ToString();
			return configDictionary;
		}
	}
}
