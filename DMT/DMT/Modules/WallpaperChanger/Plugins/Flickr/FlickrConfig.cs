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

namespace DMT.Modules.WallpaperChanger.Plugins.Flickr
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;

	using DMT.Library.WallpaperPlugin;

	/// <summary>
	/// Configuration for Flickr provider
	/// </summary>
	public class FlickrConfig
	{
		/// <summary>
		/// Initialises a new instance of the <see cref="FlickrConfig" /> class.
		/// </summary>
		public FlickrConfig()
		{
		}

		/// <summary>
		/// Initialises a new instance of the <see cref="FlickrConfig" /> class.
		/// </summary>
		/// <param name="configDictionary">Configuration as a dictionary</param>
		public FlickrConfig(Dictionary<string, string> configDictionary)
		{
			Weight = ProviderHelper.ConfigToInt(configDictionary, "weight", 10);
			Description = ProviderHelper.ConfigToString(configDictionary, "description", "Images from flickr");
			Tags = ProviderHelper.ConfigToString(configDictionary, "tags", string.Empty);
			TagModeAll = ProviderHelper.ConfigToBool(configDictionary, "tagModeAll", false);
			Text = ProviderHelper.ConfigToString(configDictionary, "text", string.Empty);
			UserId = ProviderHelper.ConfigToString(configDictionary, "userId", string.Empty);
			GroupId = ProviderHelper.ConfigToString(configDictionary, "groupId", string.Empty);
			RandomPage = ProviderHelper.ConfigToBool(configDictionary, "randomPage", false);
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
		/// Gets or sets the Flickr tags to search
		/// </summary>
		public string Tags { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether all Flickr tags need to be found for a match
		/// </summary>
		public bool TagModeAll { get; set; }

		/// <summary>
		/// Gets or sets the Flickr text to search
		/// </summary>
		public string Text { get; set; }

		/// <summary>
		/// Gets or sets the Flickr user id to search
		/// </summary>
		public string UserId { get; set; }

		/// <summary>
		/// Gets or sets the Flickr group id to search
		/// </summary>
		public string GroupId { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether to use a random page rather than the first page
		/// </summary>
		public bool RandomPage { get; set; }

		/// <summary>
		/// Gets the configuration as a dictionary ready for saving to disk
		/// </summary>
		/// <returns>Dictionary representation of configuration</returns>
		public Dictionary<string, string> ToDictionary()
		{
			Dictionary<string, string> configDictionary = new Dictionary<string, string>();
			configDictionary["weight"] = Weight.ToString();
			configDictionary["description"] = Description;
			configDictionary["tags"] = Tags;
			configDictionary["tagModeAll"] = TagModeAll.ToString();
			configDictionary["text"] = Text;
			configDictionary["userId"] = UserId;
			configDictionary["groupId"] = GroupId;
			configDictionary["randomPage"] = RandomPage.ToString();
			return configDictionary;
		}
	}
}
