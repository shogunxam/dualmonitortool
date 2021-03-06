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
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Reflection;
using System.Text;

namespace DualMonitorTools.DualWallpaperChanger
{
	/// <summary>
	/// Factory to create providers using the available plugins
	/// </summary>
	class ProviderFactory : IProviderFactory
	{
		CompositionContainer _container;

		[ImportMany(typeof(IDWC_Plugin))]
		public IEnumerable<Lazy<IDWC_Plugin, IImageProviderData>> Plugins { get; set; }

		public ProviderFactory()
		{
			GetPlugins();
		}

		void GetPlugins()
		{
			string pluginDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
			DirectoryCatalog directoryCatalog = new DirectoryCatalog(pluginDirectory, "DWC_*.dll");
			// CompositionOptions needs .NET 4.5, but this would exclude XP users
			//_container = new CompositionContainer(directoryCatalog, CompositionOptions.DisableSilentRejection);	
			_container = new CompositionContainer(directoryCatalog);

			try
			{
				_container.ComposeParts(this);
			}
			catch (CompositionException compositionException)
			{
				Console.WriteLine(compositionException.ToString());
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.ToString());
			}

		}

		/// <summary>
		/// Returns a list of available plugins
		/// </summary>
		/// <returns>list of available plugins</returns>
		public List<IDWC_Plugin> GetAvailablePlugins()
		{
			List<IDWC_Plugin> providerList = new List<IDWC_Plugin>();

			foreach (Lazy<IDWC_Plugin, IImageProviderData> p in Plugins)
			{
				providerList.Add(p.Value);
			}

			return providerList;
		}

		/// <summary>
		/// Creates a new provider given the plugin name and required configiration
		/// </summary>
		/// <param name="name">name of the plugin</param>
		/// <param name="dictionaryConfig">required configiration</param>
		/// <returns>new provider</returns>
		public IImageProvider CreateProvider(string name, Dictionary<string, string> dictionaryConfig)
		{
			IImageProvider provider = null;

			foreach (Lazy<IDWC_Plugin, IImageProviderData> p in Plugins)
			{
				if (p.Metadata.Name == name)
				{
					provider = p.Value.CreateProvider(dictionaryConfig);
				}
			}

			return provider;
		}
	}
}
