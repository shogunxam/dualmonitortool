#region copyright
// This file is part of Dual Monitor Tools which is a set of tools to assist
// users with multiple monitor setups.
// Copyright (C) 2015-2016  Gerald Evans
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

namespace DMT.Modules.WallpaperChanger.Plugins.Url
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;

	using DMT.Library.WallpaperPlugin;

	/// <summary>
	/// Configuration required for each provider from the Unsplash plugin
	/// </summary>
	public class WebScrapeConfig
	{
		/// <summary>
		/// Initialises a new instance of the <see cref="WebScrapeConfig" /> class.
		/// </summary>
		public WebScrapeConfig()
		{
		}

		/// <summary>
		/// Initialises a new instance of the <see cref="WebScrapeConfig" /> class.
		/// </summary>
		/// <param name="configDictionary">Configuration dictionary</param>
		public WebScrapeConfig(Dictionary<string, string> configDictionary)
		{
			Weight = ProviderHelper.ConfigToInt(configDictionary, "weight", 10);
			Description = ProviderHelper.ConfigToString(configDictionary, "description", "Images from a Url");
			Url = ProviderHelper.ConfigToString(configDictionary, "url", "");
			AllowEscapes = ProviderHelper.ConfigToBool(configDictionary, "allowEscapes", true);
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
		/// Gets or sets the url to get the image from
		/// </summary>
		public string Url { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether escapes will be processed in above URL
		/// </summary>
		public bool AllowEscapes { get; set; }

		/// <summary>
		/// Gets the configuration as a dictionary ready for saving to disk
		/// </summary>
		/// <returns>Dictionary representation of configuration</returns>
		public Dictionary<string, string> ToDictionary()
		{
			Dictionary<string, string> configDictionary = new Dictionary<string, string>();
			configDictionary["weight"] = Weight.ToString();
			configDictionary["description"] = Description;
			configDictionary["url"] = Url;
			configDictionary["allowEscapes"] = AllowEscapes.ToString();
			return configDictionary;
		}
	}
}
