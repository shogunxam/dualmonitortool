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
//using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//using DualMonitorTools.DualWallpaperChanger;
using DMT.Library.WallpaperPlugin;
using DMT.Library.Utils;

namespace DMT.Modules.WallpaperChanger.Plugins.LocalDisk
{
	/// <summary>
	/// An instance of a provider from the Local Disk plugin
	/// </summary>
	public class LocalDiskProvider : IImageProvider
	{

		LocalDiskConfig _config;

		// these relate to the provider type
		public string ProviderName { get { return LocalDiskPlugin.PluginName; } }
		public Image ProviderImage { get { return LocalDiskPlugin.PluginImage; } }
		public string Version { get { return LocalDiskPlugin.PluginVersion; } }

		// these relate to the provider instance
		public string Description { get { return _config.Description; } }
		public int Weight { get { return _config.Weight; } }

		public Dictionary<string, string> Config { get { return _config.ToDictionary(); } }

		//static Random _random = new Random();

		//List<string> _candidateLandscapeFilenames = null;
		//List<string> _candidatePortraitFilenames = null;
		//string _lastLandscapeDirectory = null;
		//string _lastPortraitDirectory = null;

		CandidateFilenames Monitor1Cache = new CandidateFilenames();
		CandidateFilenames Monitor2Cache = new CandidateFilenames();
		CandidateFilenames Monitor3Cache = new CandidateFilenames();
		CandidateFilenames Monitor4Cache = new CandidateFilenames();
		CandidateFilenames PortraitCache = new CandidateFilenames();
		CandidateFilenames DefaultCache = new CandidateFilenames();

		bool _lastRecusrive = false;


		public LocalDiskProvider(Dictionary<string, string> config)
		{
			_config = new LocalDiskConfig(config);
		}

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

		public ProviderImage GetRandomImage(Size optimumSize, int screenIndex)
		{
			ProviderImage providerImage = null;

			// if user has changed configuration, since we last built _candidateFilenames
 			// we need to rebuild if
			//if (_config.DefaultDirectory != _lastLandscapeDirectory 
			//	|| _config.Recursive != _lastRecusrive)
			//{
			//	_candidateLandscapeFilenames = null;
			//}
			//if (_config.PortraitDirectory != _lastPortraitDirectory
			//	|| _config.Recursive != _lastRecusrive)
			//{
			//	_candidatePortraitFilenames = null;
			//}

			Monitor1Cache.SetDirectory(_config.Monitor1Directory, _config.Recursive);
			Monitor2Cache.SetDirectory(_config.Monitor2Directory, _config.Recursive);
			Monitor3Cache.SetDirectory(_config.Monitor3Directory, _config.Recursive);
			Monitor4Cache.SetDirectory(_config.Monitor4Directory, _config.Recursive);
			PortraitCache.SetDirectory(_config.PortraitDirectory, _config.Recursive);
			DefaultCache.SetDirectory(_config.DefaultDirectory, _config.Recursive);

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
			Monitor1Cache.ClearCache();
			Monitor2Cache.ClearCache();
			Monitor3Cache.ClearCache();
			Monitor4Cache.ClearCache();
			PortraitCache.ClearCache();
			DefaultCache.ClearCache();
		}

		string GetRandomImageFilename(Size optimumSize, int screenIndex)
		{
			string ret = null;

			// check for image for a particular monitor
			// Screen Index is zero based, 
			// and will be -1 if want an image to cover multiple monitors 
			if (screenIndex >= 0)
			{
				switch (screenIndex)
				{
					case 0:
						ret = Monitor1Cache.GetRandomImage();
						break;
					case 1:
						ret = Monitor2Cache.GetRandomImage();
						break;
					case 2:
						ret = Monitor3Cache.GetRandomImage();
						break;
					case 3:
						ret = Monitor4Cache.GetRandomImage();
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
					ret = PortraitCache.GetRandomImage();
				}
			}

			// landscape / default
			if (ret == null)
			{
				ret = DefaultCache.GetRandomImage();
			}

			return ret;
		}

		//List<string> GetCandidateFilenames(string baseDirectory, bool recursive)
		//{
		//	List<string> candidateFilenames = new List<string>();

		//	if (!string.IsNullOrEmpty(baseDirectory))
		//	{
		//		AddCandidateFilenames(baseDirectory, recursive, candidateFilenames);
		//	}

		//	return candidateFilenames;
		//}

		//void AddCandidateFilenames(string baseDirectory, bool recursive, List<string> candidateFilenames)
		//{
		//	try
		//	{
		//		DirectoryInfo dir = new DirectoryInfo(baseDirectory);

		//		FileSystemInfo[] infos = dir.GetFileSystemInfos();
		//		foreach (FileSystemInfo info in infos)
		//		{
		//			if (info is FileInfo)
		//			{
		//				if (IsImageFile(info))
		//				{
		//					candidateFilenames.Add(info.FullName);
		//				}
		//			}
		//			else if (info is DirectoryInfo)
		//			{
		//				if (recursive)
		//				{
		//					AddCandidateFilenames(info.FullName, recursive, candidateFilenames);
		//				}
		//			}
		//		}
		//	}
		//	catch (Exception)
		//	{
		//		// ignore any i/o errors
		//	}

		//}

		//bool IsImageFile(FileSystemInfo info)
		//{
		//	string extension = info.Extension.ToLower();
		//	switch (extension)
		//	{
		//		case ".jpg": // drop into ".jpeg"
		//		case ".jpeg":
		//			return true;
		//		case ".png":
		//			return true;
		//		case ".bmp":
		//			return true;
		//		case ".gif":
		//			return true;
		//	}

		//	return false;
		//}

	}
}
