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

namespace DMT.Modules.WallpaperChanger
{
	using System;
	using System.Collections.Generic;
	using System.IO;
	using System.Reflection;
	using System.Text;

	using DMT.Library.Settings;
	using DMT.Library.Wallpaper;
	using DMT.Library.WallpaperPlugin;

	/// <summary>
	/// Factory to create providers using the built-in providers
	/// plug-ins as used by DualWallpaperChanger to come later (maybe?)
	/// </summary>
	public class ProviderFactory : IProviderFactory
	{
		ISettingsService _settingsService;
		List<IDWC_Plugin> _plugins;

		/// <summary>
		/// Initialises a new instance of the <see cref="ProviderFactory" /> class.
		/// </summary>
		/// <param name="settingsService">Settings repository</param>
		public ProviderFactory(ISettingsService settingsService)
		{
			_settingsService = settingsService;
			CreatePlugins();
		}

		/// <summary>
		/// Returns a list of available plugins
		/// </summary>
		/// <returns>list of available plugins</returns>
		public List<IDWC_Plugin> GetAvailablePlugins()
		{
			return _plugins;
		}

		/// <summary>
		/// Creates a new provider given the plugin name and required configuration
		/// </summary>
		/// <param name="name">name of the plugin</param>
		/// <param name="dictionaryConfig">required configuration</param>
		/// <returns>new provider</returns>
		public IImageProvider CreateProvider(string name, Dictionary<string, string> dictionaryConfig)
		{
			IImageProvider provider = null;

			foreach (IDWC_Plugin p in _plugins)
			{
				if (p.Name == name)
				{
					provider = p.CreateProvider(dictionaryConfig);
				}
			}

			return provider;
		}

		void CreatePlugins()
		{
			_plugins = new List<IDWC_Plugin>();
			_plugins.Add(new DMT.Modules.WallpaperChanger.Plugins.LocalDisk.LocalDiskPlugin());
			_plugins.Add(new DMT.Modules.WallpaperChanger.Plugins.RandomShapes.RandomShapesPlugin());
			_plugins.Add(new DMT.Modules.WallpaperChanger.Plugins.Unsplash.WebScrapePlugin());
			_plugins.Add(new DMT.Modules.WallpaperChanger.Plugins.Flickr.FlickrPlugin(_settingsService));
			_plugins.Add(new DMT.Modules.WallpaperChanger.Plugins.Url.WebScrapePlugin());
		}
	}
}
