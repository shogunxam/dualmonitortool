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

using DMT.Library.WallpaperPlugin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DMT.Modules.WallpaperChanger.Plugins.Flickr
{
	public class FlickrConfig
	{
		public FlickrConfig()
		{
		}

		public FlickrConfig(Dictionary<string, string> configDictionary)
		{
			Weight = ProviderHelper.ConfigToInt(configDictionary, "weight", 10);
			Description = ProviderHelper.ConfigToString(configDictionary, "description", "Images from flickr");
			Tags = ProviderHelper.ConfigToString(configDictionary, "tags", "");
			TagModeAll = ProviderHelper.ConfigToBool(configDictionary, "tagModeAll", false);
			Text = ProviderHelper.ConfigToString(configDictionary, "text", "");
			UserId = ProviderHelper.ConfigToString(configDictionary, "userId", "");
			GroupId = ProviderHelper.ConfigToString(configDictionary, "groupId", "");
			RandomPage = ProviderHelper.ConfigToBool(configDictionary, "randomPage", false);
		}

		public int Weight { get; set; }
		public string Description { get; set; }
		public string Tags { get; set; }
		public bool TagModeAll { get; set; }
		public string Text { get; set; }
		public string UserId { get; set; }
		public string GroupId { get; set; }
		public bool RandomPage { get; set; }

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
