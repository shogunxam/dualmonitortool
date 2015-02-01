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

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Forms;

namespace DualMonitorTools.DualWallpaperChanger
{
	/// <summary>
	/// Controller used by ChangerForm
	/// </summary>
	public class Controller
	{
		ILocalEnvironment _localEnvironment;
		IProviderPersistence _providerPersistence;
		IProviderFactory _providerFactory;
		IWallpaperCompositorFactory _wallpaperCompositorFactory;
		IImageRepository _imageRepository;
		Desktop _desktop;

		public Controller(ILogger logger)
		{
			_localEnvironment = new LocalEnvironment();
			_providerFactory = new ProviderFactory();
			_providerPersistence = new ProviderPersistence(_providerFactory, _localEnvironment);
			_wallpaperCompositorFactory = new WallpaperCompositorFactory();
			_imageRepository = new ImageRepository(_providerPersistence, logger);
			_desktop = new Desktop(_localEnvironment, _imageRepository, _wallpaperCompositorFactory);
		}

		/// <summary>
		/// Returns a list of all available plugins
		/// </summary>
		/// <returns>All available plugins</returns>
		public List<IDWC_Plugin> GetPluginsDataSource()
		{
			return _providerFactory.GetAvailablePlugins();
		}

		/// <summary>
		/// Returns a list of all providers the user has currently configured
		/// ready for display in a DataGrid
		/// </summary>
		/// <returns>Users configured providers</returns>
		public BindingList<IImageProvider> GetProvidersDataSource()
		{
			return _imageRepository.DataSource;
		}

		/// <summary>
		/// Generates a new wallpaper for the desktop
		/// </summary>
		public void UpdateWallpaper()
		{
			_desktop.UpdateWallpaper();
		}

		/// <summary>
		/// Allows the user to add a new provider.
		/// This will cause the providers config dialog to be displayed
		/// </summary>
		/// <param name="providerName">Name of provider</param>
		/// <returns>true iff provider added</returns>
		public bool AddProvider(string providerName)
		{
			Dictionary<string, string> configDictionary = new Dictionary<string, string>();
			IImageProvider provider = _providerFactory.CreateProvider(providerName, configDictionary);
			if (provider != null)
			{
				configDictionary = provider.ShowUserOptions();
				if (configDictionary != null)
				{
					_imageRepository.DataSource.Add(provider);
					_imageRepository.Save();
					return true;
				}
			}
			return false;
		}

		/// <summary>
		/// Allows the user to edit a provider they have previously added
		/// This will cause the providers config dialog to be displayed
		/// </summary>
		/// <param name="rowIndex">Index of provider returned by GetProvidersDataSource()</param>
		/// <returns>true iff provider config changes saved</returns>
		public bool EditProvider(int rowIndex)
		{
			IImageProvider provider = _imageRepository.DataSource[rowIndex];
			Dictionary<string, string> configDictionary = provider.ShowUserOptions();
			if (configDictionary != null)
			{
				_imageRepository.Save();
				return true;
			}
			return false;
		}

		/// <summary>
		/// Deletes all indicated providers
		/// </summary>
		/// <param name="rowIndexes">List of indexes returned by GetProvidersDataSource() to delete</param>
		public void DeleteProviders(List<int> rowIndexes)
		{
			// TODO - check
			List<IImageProvider> providers = new List<IImageProvider>();
			foreach (int rowIndex in rowIndexes)
			{
				providers.Add(_imageRepository.DataSource[rowIndex]);
			}
			foreach (IImageProvider provider in providers)
			{
				_imageRepository.DataSource.Remove(provider);
			}
			_imageRepository.Save();
		}
	}
}
