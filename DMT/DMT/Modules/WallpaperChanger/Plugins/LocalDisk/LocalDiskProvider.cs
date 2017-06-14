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

namespace DMT.Modules.WallpaperChanger.Plugins.LocalDisk
{
	using System;
	using System.Collections.Generic;
	using System.Diagnostics;
	using System.Drawing;
	using System.IO;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;

	using DMT.Library.Utils;
	using DMT.Library.WallpaperPlugin;

	/// <summary>
	/// An instance of a provider from the Local Disk plugin
	/// </summary>
	public class LocalDiskProvider : IImageProvider
	{
		LocalDiskConfig _config;
		CandidateFilenames _monitor1Cache = new CandidateFilenames();
		CandidateFilenames _monitor2Cache = new CandidateFilenames();
		CandidateFilenames _monitor3Cache = new CandidateFilenames();
		CandidateFilenames _monitor4Cache = new CandidateFilenames();
		CandidateFilenames _portraitCache = new CandidateFilenames();
		CandidateFilenames _defaultCache = new CandidateFilenames();

		/// <summary>
		/// Initialises a new instance of the <see cref="LocalDiskProvider" /> class.
		/// </summary>
		/// <param name="config">Configuration for the provider</param>
		public LocalDiskProvider(Dictionary<string, string> config)
		{
			_config = new LocalDiskConfig(config);
		}

		/// <summary>
		/// Gets the provider name - same for all instances of this class
		/// </summary>
		public string ProviderName
		{
			get
			{
				return LocalDiskPlugin.PluginName;
			}
		}

		/// <summary>
		/// Gets the provider image - same for all instances of this class
		/// </summary>
		public Image ProviderImage
		{
			get
			{
				return LocalDiskPlugin.PluginImage;
			}
		}

		/// <summary>
		/// Gets the provider version - same for all instances of this class
		/// </summary>
		public string Version
		{
			get
			{
				return LocalDiskPlugin.PluginVersion;
			}
		}

		/// <summary>
		/// Gets the description for this instance of the provider
		/// </summary>
		public string Description
		{
			get
			{
				return _config.Description;
			}
		}

		/// <summary>
		/// Gets the enabled state for this instance of the provider
		/// </summary>
		public bool Enabled
		{
			get
			{
				return _config.Enabled;
			}
			set
			{
				_config.Enabled = value;
			}
		}

		/// <summary>
		/// Gets the weight for this instance of the provider
		/// </summary>
		public int Weight
		{
			get
			{
				return _config.Weight;
			}
		}

		/// <summary>
		/// Gets the configuration 
		/// </summary>
		public Dictionary<string, string> Config
		{
			get
			{
				return _config.ToDictionary();
			}
		}

		/// <summary>
		/// Allows the user to update the configuration 
		/// </summary>
		/// <returns>New configuration, or null if no changes</returns>
		public Dictionary<string, string> ShowUserOptions()
		{
			LocalDiskForm dlg = new LocalDiskForm(_config);
			if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				_config = dlg.GetConfig();
				return _config.ToDictionary();
			}

			// return null to indicate options have not been updated
			return null;
		}

		/// <summary>
		/// Returns a random image
		/// </summary>
		/// <param name="optimumSize">Optimum image size</param>
		/// <param name="screenIndex">Screen index image is for</param>
		/// <returns>A random image, or null if one can't be returned</returns>
		public ProviderImage GetRandomImage(Size optimumSize, int screenIndex)
		{
			ProviderImage providerImage = null;

			// make sure caches reflect current settings
			_monitor1Cache.SetDirectory(_config.Monitor1Directory, _config.Recursive, _config.Cycle);
			_monitor2Cache.SetDirectory(_config.Monitor2Directory, _config.Recursive, _config.Cycle);
			_monitor3Cache.SetDirectory(_config.Monitor3Directory, _config.Recursive, _config.Cycle);
			_monitor4Cache.SetDirectory(_config.Monitor4Directory, _config.Recursive, _config.Cycle);
			_portraitCache.SetDirectory(_config.PortraitDirectory, _config.Recursive, _config.Cycle);
			_defaultCache.SetDirectory(_config.DefaultDirectory, _config.Recursive, _config.Cycle);

			if (_config.Rescan)
			{
				// this is needed in case user has set the rescan option since the last scan
				ClearAllCaches();
			}

			string filename = GetRandomImageFilename(optimumSize, screenIndex);
			if (filename != null)
			{
				try
				{
					providerImage = new ProviderImage(Image.FromFile(filename));
					providerImage.Provider = ProviderName;
					providerImage.Source = filename;
					providerImage.SourceUrl = filename;
				}
				catch (Exception)
				{
					// silently ignore exceptions
				}
			}

			if (_config.Rescan)
			{
				// we will rescan the folders every time we need a new image
				// so allow any current lists to be garbage collected
				ClearAllCaches();
			}

			return providerImage;
		}

		void ClearAllCaches()
		{
			_monitor1Cache.ClearCache();
			_monitor2Cache.ClearCache();
			_monitor3Cache.ClearCache();
			_monitor4Cache.ClearCache();
			_portraitCache.ClearCache();
			_defaultCache.ClearCache();
		}

		string GetRandomImageFilename(Size optimumSize, int screenIndex)
		{
			string ret = null;

			// check for image for a particular monitor
			// Screen Index is zero based, 
			// and will be -1 if want an image to cover multiple monitors 
			//
			// NOTE: Will also be -1 if only a single monitor,
			// but the'Multi Monitor' option is one that could imply that
			// the image is needed for multiple monitors
			if (screenIndex >= 0)
			{
				switch (screenIndex)
				{
					case 0:
						ret = _monitor1Cache.GetRandomImage();
						break;
					case 1:
						ret = _monitor2Cache.GetRandomImage();
						break;
					case 2:
						ret = _monitor3Cache.GetRandomImage();
						break;
					case 3:
						ret = _monitor4Cache.GetRandomImage();
						break;
					default:
						// leave ret as null
						break;
				}
			}

			// check for portrait
			if (ret == null)
			{
				if (optimumSize.Height > optimumSize.Width)
				{
					// ideally want a portrait image
					ret = _portraitCache.GetRandomImage();
				}
			}

			// landscape / default
			if (ret == null)
			{
				ret = _defaultCache.GetRandomImage();
			}

			return ret;
		}
	}
}
