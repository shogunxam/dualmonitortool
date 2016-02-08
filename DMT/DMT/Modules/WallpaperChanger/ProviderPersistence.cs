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
	using System.IO;
	using System.Text;

	using DMT.Library.Environment;
	using DMT.Library.Settings;
	using DMT.Library.Utils;
	using DMT.Library.Wallpaper;
	using DMT.Library.WallpaperPlugin;

	/// <summary>
	/// Allows the users chosen and configured providers to be saved and retrieved from disk
	/// </summary>
	class ProviderPersistence : IProviderPersistence
	{
		IProviderFactory _providerFactory;
		ILocalEnvironment _localEnvironment;

		/// <summary>
		/// Initialises a new instance of the <see cref="ProviderPersistence" /> class.
		/// </summary>
		/// <param name="providerFactory">Factory to create providers</param>
		/// <param name="localEnvironment">Local environment</param>
		public ProviderPersistence(IProviderFactory providerFactory, ILocalEnvironment localEnvironment)
		{
			_providerFactory = providerFactory;
			_localEnvironment = localEnvironment;
		}

		/// <summary>
		/// Load the users providers from disk
		/// </summary>
		/// <returns>The image providers</returns>
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
		/// <param name="providers">The providers to save</param>
		public void Save(Collection<IImageProvider> providers)
		{
			string filename = GetFullFilename();

			SafeFileWriter newFile = new SafeFileWriter(filename);
			using (Stream stream = newFile.OpenForWriting())
			{
				ProviderWriter writer = new ProviderWriter();
				writer.Write(providers, stream);
			}

			newFile.CompleteWrite();
		}

		string GetFullFilename()
		{
			return FileLocations.Instance.WallpaperProvidersFilename;
		}
	}
}
