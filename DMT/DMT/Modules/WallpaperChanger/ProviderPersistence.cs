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

using DMT.Library.Environment;
using DMT.Library.Settings;
using DMT.Library.Wallpaper;
using DMT.Library.WallpaperPlugin;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;

namespace DMT.Modules.WallpaperChanger
{
	/// <summary>
	/// Allows the users chosen and configured providers to be saved and retrieved from disk
	/// </summary>
	class ProviderPersistence : IProviderPersistence
	{
		IProviderFactory _providerFactory;
		ILocalEnvironment _localEnvironment;

		//static string _persistenceFilename = "DualWallpaperChanger.xml";

		public ProviderPersistence(IProviderFactory providerFactory, ILocalEnvironment localEnvironment)
		{
			_providerFactory = providerFactory;
			_localEnvironment = localEnvironment;
		}

		/// <summary>
		/// Load the users providers from disk
		/// </summary>
		/// <returns></returns>
		public Collection<IImageProvider> Load()
		{
			Collection<IImageProvider> providers = null;

			string filename = GetFullFilename();
			if (File.Exists(filename))
			{
				using (Stream stream = File.Open(filename, FileMode.Open))
				{
					ProviderReader reader = new ProviderReader(_providerFactory);
					providers = reader.Read(stream);
				}
			}
			else
			{
				// no file, so start off with an empty list
				providers = new Collection<IImageProvider>();
			}

			return providers;
		}

		/// <summary>
		/// Saves the users providers to disk
		/// </summary>
		/// <param name="providers"></param>
		public void Save(Collection<IImageProvider> providers)
		{
			string filename = GetFullFilename();
			using (Stream stream = File.Open(filename, FileMode.Create))
			{
				ProviderWriter writer = new ProviderWriter();
				writer.Write(providers, stream);
			}
		}

		string GetFullFilename()
		{
			return DataLocations.Instance.WallpaperProvidersFilename;
		}
	}
}
