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

namespace DMT.Modules.WallpaperChanger.Plugins.Unsplash
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
		/// Types of image selection available with Unsplash
		/// Note: Don't change existing numeric values, as these are saved in the config file
		/// </summary>
		public enum UnsplashType { Random = 0, Featured = 1, Category = 2, User = 3, LikedByUser = 4 };

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
			Enabled = ProviderHelper.ConfigToBool(configDictionary, "enabled", true);
			Weight = ProviderHelper.ConfigToInt(configDictionary, "weight", 10);
			Description = ProviderHelper.ConfigToString(configDictionary, "description", "Images from www.unsplash.com");
			FirstPageOnly = ProviderHelper.ConfigToBool(configDictionary, "firstPageOnly", false);
			Type = (UnsplashType)ProviderHelper.ConfigToInt(configDictionary, "type", (int)UnsplashType.Random);
			Category = ProviderHelper.ConfigToString(configDictionary, "category", "");
			User = ProviderHelper.ConfigToString(configDictionary, "user", "");
			LikedByUser = ProviderHelper.ConfigToString(configDictionary, "likedByUser", "");
			Filter = ProviderHelper.ConfigToString(configDictionary, "filter", "");
		}

		/// <summary>
		/// Gets or sets the wight for this provider
		/// </summary>
		public bool Enabled { get; set; }

		/// <summary>
		/// Gets or sets the wight for this provider
		/// </summary>
		public int Weight { get; set; }

		/// <summary>
		/// Gets or sets the description for this provider
		/// </summary>
		public string Description { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether only the first page should be looked at
		/// 
		/// Note: this is no longer used
		/// </summary>
		public bool FirstPageOnly { get; set; }

		/// <summary>
		/// Specifies how we are going tp restrict the image
		/// </summary>
		public UnsplashType Type { get; set; }

		/// <summary>
		/// Unsplash category to get images from
		/// (Only used when UnsplashType = Category)
		/// </summary>
		public string Category { get; set; }

		/// <summary>
		/// Unsplash user to get images from
		/// (Only when UnsplashType = User)
		/// </summary>
		public string User { get; set; }

		/// <summary>
		/// Unsplash user to get images from
		/// (Only when UnsplashType = UserLike)
		/// </summary>
		public string LikedByUser { get; set; }

		/// <summary>
		/// Search term(s) to further limit images returned
		/// </summary>
		public string Filter { get; set; }

		/// <summary>
		/// Gets the configuration as a dictionary ready for saving to disk
		/// </summary>
		/// <returns>Dictionary representation of configuration</returns>
		public Dictionary<string, string> ToDictionary()
		{
			Dictionary<string, string> configDictionary = new Dictionary<string, string>();
			configDictionary["enabled"] = Enabled.ToString();
			configDictionary["weight"] = Weight.ToString();
			configDictionary["description"] = Description;
			configDictionary["firstPageOnly"] = FirstPageOnly.ToString();
			configDictionary["type"] = ((int)Type).ToString();
			configDictionary["category"] = Category;
			configDictionary["user"] = User;
			configDictionary["likedByUser"] = LikedByUser;
			configDictionary["filter"] = Filter;
			return configDictionary;
		}
	}
}
