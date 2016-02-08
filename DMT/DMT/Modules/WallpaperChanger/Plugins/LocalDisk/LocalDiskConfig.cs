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

namespace DMT.Modules.WallpaperChanger.Plugins.LocalDisk
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;

	using DMT.Library.WallpaperPlugin;

	/// <summary>
	/// Configuration required for each provider from the LocalDisk plugin
	/// </summary>
	public class LocalDiskConfig
	{
		/// <summary>
		/// Initialises a new instance of the <see cref="LocalDiskConfig" /> class.
		/// </summary>
		public LocalDiskConfig()
		{
		}

		/// <summary>
		/// Initialises a new instance of the <see cref="LocalDiskConfig" /> class.
		/// </summary>
		/// <param name="configDictionary">Configuration as a dictionary</param>
		public LocalDiskConfig(Dictionary<string, string> configDictionary)
		{
			Weight = ProviderHelper.ConfigToInt(configDictionary, "weight", 10);
			Description = ProviderHelper.ConfigToString(configDictionary, "description", "Windows Wallpaper from local disk");
			Monitor1Directory = ProviderHelper.ConfigToString(configDictionary, "monitor1Directory", string.Empty);
			Monitor2Directory = ProviderHelper.ConfigToString(configDictionary, "monitor2Directory", string.Empty);
			Monitor3Directory = ProviderHelper.ConfigToString(configDictionary, "monitor3Directory", string.Empty);
			Monitor4Directory = ProviderHelper.ConfigToString(configDictionary, "monitor4Directory", string.Empty);
			PortraitDirectory = ProviderHelper.ConfigToString(configDictionary, "portraitDirectory", string.Empty);
			DefaultDirectory = ProviderHelper.ConfigToString(configDictionary, "directory", @"C:\Windows\Web\Wallpaper");
			Recursive = ProviderHelper.ConfigToBool(configDictionary, "recursive", true);
			Rescan = ProviderHelper.ConfigToBool(configDictionary, "rescan", false);
		}

		/// <summary>
		/// Gets or sets the wight for this provider
		/// </summary>
		public int Weight { get; set; }

		/// <summary>
		/// Gets or sets the description for this provider
		/// </summary>
		public string Description { get; set; }

		/// <summary>
		/// Gets or sets the directory to use for images for monitor 1
		/// </summary>
		public string Monitor1Directory { get; set; }

		/// <summary>
		/// Gets or sets the directory to use for images for monitor 2
		/// </summary>
		public string Monitor2Directory { get; set; }

		/// <summary>
		/// Gets or sets the directory to use for images for monitor 3
		/// </summary>
		public string Monitor3Directory { get; set; }

		/// <summary>
		/// Gets or sets the directory to use for images for monitor 4
		/// </summary>
		public string Monitor4Directory { get; set; }

		/// <summary>
		/// Gets or sets the directory to use for images for monitors in portrait mode
		/// without an explicit directory  
		/// </summary>
		public string PortraitDirectory { get; set; }

		/// <summary>
		/// Gets or sets the directory to use for any monitors
		/// without a explicit directory
		/// </summary>
		public string DefaultDirectory { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether to scan recursively down through directories
		/// </summary>
		public bool Recursive { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether to re-scan disk contents for every image request
		/// </summary>
		public bool Rescan { get; set; }

		/// <summary>
		/// Gets the configuration as a dictionary ready for saving to disk
		/// </summary>
		/// <returns>Dictionary representation of configuration</returns>
		public Dictionary<string, string> ToDictionary()
		{
			Dictionary<string, string> configDictionary = new Dictionary<string, string>();
			configDictionary["weight"] = Weight.ToString();
			configDictionary["description"] = Description;
			configDictionary["monitor1Directory"] = Monitor1Directory;
			configDictionary["monitor2Directory"] = Monitor2Directory;
			configDictionary["monitor3Directory"] = Monitor3Directory;
			configDictionary["monitor4Directory"] = Monitor4Directory;
			configDictionary["portraitDirectory"] = PortraitDirectory;
			configDictionary["directory"] = DefaultDirectory;
			configDictionary["recursive"] = Recursive.ToString();
			configDictionary["rescan"] = Rescan.ToString();
			return configDictionary;
		}
	}
}
