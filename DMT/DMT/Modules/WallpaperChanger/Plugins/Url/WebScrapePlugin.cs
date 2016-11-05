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
	using System.Drawing;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;

	using DMT.Library.WallpaperPlugin;

	/// <summary>
	/// Plugin for Unsplash
	/// </summary>
	public class WebScrapePlugin : IDWC_Plugin
	{
		public const string PluginVersion = "0.0";
		const string MyPluginName = "URL";

		/// <summary>
		/// Gets the plugin name
		/// </summary>
		public static string PluginName 
		{ 
			get 
			{ 
				return MyPluginName; 
			} 
		}

		/// <summary>
		/// Gets the plugin image
		/// </summary>
 		public static Image PluginImage 
		{ 
			get 
			{ 
				return Properties.Resources.UnsplashPlugin; 
			} 
		}

		/// <summary>
		/// Gets the plugin name
		/// </summary>
		public string Name 
		{ 
			get 
			{ 
				return PluginName; 
			} 
		}

		/// <summary>
		/// Gets the plugin image
		/// </summary>
		public Image Image 
		{ 
			get 
			{ 
				return PluginImage; 
			} 
		}

		/// <summary>
		/// Creates a provider from the plugin
		/// </summary>
		/// <param name="config">Configuration to use for the provider</param>
		/// <returns>The image provider</returns>
		public IImageProvider CreateProvider(Dictionary<string, string> config)
		{
			return new WebScrapeProvider(config);
		}
	}
}