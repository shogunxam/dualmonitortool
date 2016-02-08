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
	using System.Collections.ObjectModel;
	using System.ComponentModel;
	using System.Drawing;
	using System.IO;
	using System.Text;

	using DMT.Library.Binding;
	using DMT.Library.Logging;
	using DMT.Library.Utils;
	using DMT.Library.Wallpaper;
	using DMT.Library.WallpaperPlugin;

	/// <summary>
	/// Repository used to get new images.
	/// This is based around the list of providers that the user has selected/configured.
	/// </summary>
	class ImageRepository : IImageRepository
	{
		SortableBindingList<IImageProvider> _providers = new SortableBindingList<IImageProvider>();
		IProviderPersistence _providerPersistence;
		ILogger _logger;

		/// <summary>
		/// Initialises a new instance of the <see cref="ImageRepository" /> class.
		/// </summary>
		/// <param name="providerPersistence">Provides persistence</param>
		/// <param name="logger">Application logger</param>
		public ImageRepository(IProviderPersistence providerPersistence, ILogger logger)
		{
			_providerPersistence = providerPersistence;
			_logger = logger;

			LoadProviders();
		}

		/// <summary>
		/// Get the list of providers for use in a DataGrid
		/// </summary>
		public BindingList<IImageProvider> DataSource
		{
			get { return _providers; }
		}

		/// <summary>
		/// Saves the current list of providers together with their configuration
		/// </summary>
		/// <returns>True if saved successfully</returns>
		public bool Save()
		{
			_providerPersistence.Save(_providers);
			return true;
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
		/// <param name="optimumSize">Optimum size for the image. May be ignored by the provider</param>
		/// <param name="screenIndex">Index of screen image is for</param>
		/// <returns>Random image, or null if unable to return image</returns>
		public ProviderImage GetRandomImage(Size optimumSize, int screenIndex)
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
							return GetRandomImageFromProvider(optimumSize, screenIndex, provider);
						}
					}
				}
			}

			// no image found
			return null;
		}

		void LoadProviders()
		{
			Collection<IImageProvider> providers = _providerPersistence.Load();
			foreach (IImageProvider provider in providers)
			{
				_providers.Add(provider);
			}
		}

		ProviderImage GetRandomImageFromProvider(Size optimumSize, int screenIndex, IImageProvider provider)
		{
			if (provider == null)
			{
				// shouldn't happen
				_logger.LogError("ImageRepository", "null provider found");
				return null;
			}

			try
			{
				return provider.GetRandomImage(optimumSize, screenIndex);
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
