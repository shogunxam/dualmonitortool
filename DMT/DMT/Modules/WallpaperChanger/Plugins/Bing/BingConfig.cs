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

namespace DMT.Modules.WallpaperChanger.Plugins.Bing
{
	using DMT.Library.WallpaperPlugin;
	using System.Collections.Generic;
	/// <summary>
	/// Configuration required for each provider from the Bing plugin
	/// </summary>
	public class BingConfig
	{
		public BingConfig()
		{
		}

		public BingConfig(Dictionary<string, string> configDictionary)
		{
			Enabled = ProviderHelper.ConfigToBool(configDictionary, "enabled", true);
			Weight = ProviderHelper.ConfigToInt(configDictionary, "weight", 10);
			Description = ProviderHelper.ConfigToString(configDictionary, "description", "Images from www.bing.com");
			Market = ProviderHelper.ConfigToString(configDictionary, "market", @"en-WW");
		}

		/// <summary>
		/// Gets or sets the wight for this provider
		/// </summary>
		public bool Enabled { get; set; }
		public int Weight { get; set; }
		public string Description { get; set; }
		public string Market { get; set; }

		public Dictionary<string, string> ToDictionary()
		{
			Dictionary<string, string> configDictionary = new Dictionary<string, string>();
			configDictionary["enabled"] = Enabled.ToString();
			configDictionary["weight"] = Weight.ToString();
			configDictionary["description"] = Description;
			configDictionary["market"] = Market;
			return configDictionary;
		}
	}
}
