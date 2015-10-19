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

using DMT.Library.Binding;
using DMT.Library.Logging;
using DMT.Library.Utils;
using DMT.Library.Wallpaper;
using DMT.Library.WallpaperPlugin;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Text;

namespace DMT.Modules.WallpaperChanger
{
	/// <summary>
	/// Repository used to get new images.
	/// This is based around the list of providers that the user has selected/configured.
	/// </summary>
	class ImageRepository : IImageRepository
	{
		SortableBindingList<IImageProvider> _providers = new SortableBindingList<IImageProvider>();
		IProviderPersistence _providerPersistence;
		ILogger _logger;

		//static Random _random = new Random();


		public ImageRepository(IProviderPersistence providerPersistence, ILogger logger)
		{
			_providerPersistence = providerPersistence;
			_logger = logger;

			LoadProviders();
			//_providers.ListChanged += new ListChangedEventHandler(Providers_ListChanged);
		}

		//private void Providers_ListChanged(object sender, ListChangedEventArgs e)
		//{
		//}

		/// <summary>
		/// Savs the current list of providers together with their configuration
		/// </summary>
		/// <returns></returns>
		public bool Save()
		{
			_providerPersistence.Save(_providers);
			return true;
		}

		void LoadProviders()
		{
			Collection<IImageProvider> providers = _providerPersistence.Load();
			foreach (IImageProvider provider in providers)
			{
				_providers.Add(provider);
			}
		}

		/// <summary>
		/// Get the list of providers for use in a DataGrid
		/// </summary>
		public BindingList<IImageProvider> DataSource
		{
			get { return _providers; }
		}

		/// <summary>
		/// Add a new provider
		/// </summary>
		/// <param name="provider">Provider to add</param>
		public void Add(IImageProvider provider)
		{
			_providers.Insert(provider);
		}

		/// <summary>
		/// Gets a random image from the repository
		/// </summary>
		/// <param name="optimumSize">Optimum size for the image. - may be ignored by the provider</param>
		/// <returns></returns>
		public ProviderImage GetRandomImage(Size optimumSize)
		{
			// first choose a provider
			int totalWeight = 0;
			foreach (IImageProvider provider in _providers)
			{
				int weight = provider.Weight;
				// ignore negative weights
				if (weight > 0)
				{
					totalWeight += weight;
				}
			}

			if (totalWeight > 0)
			{
				//Random random = new Random();
				//int index = _random.Next(totalWeight);
				int index = RNG.Next(totalWeight);

				foreach (IImageProvider provider in _providers)
				{
					int weight = provider.Weight;
					// ignore negative weights
					if (weight > 0)
					{
						index -= weight;
						if (index < 0)
						{
							// use this provider
							//return provider.GetRandomImage(optimumSize);
							return GetRandomImageFromProvider(optimumSize, provider);
						}
					}
				}
			}

			// no image found
			return null;
		}

		ProviderImage GetRandomImageFromProvider(Size optimumSize, IImageProvider provider)
		{
			if (provider == null)
			{
				// shouldn't happen
				_logger.LogError("ImageRepository", "null provider found");
				return null;
			}

			try
			{
				return provider.GetRandomImage(optimumSize);
			}
			catch (Exception ex)
			{
				string providerName = provider.ProviderName ?? "Unknown";
				_logger.LogException(providerName, ex);
				return null;
			}
		}
	}
}
