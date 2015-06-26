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

		static Random _random = new Random();

		List<string> _candidateFilenames = null;
		string _lastDirectory = null;
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

		public ProviderImage GetRandomImage(Size optimumSize)
		{
			ProviderImage providerImage = null;

			// if user has changed configuration, since we last built _candidateFilenames
 			// we need to rebuild if
			if (_config.Directory != _lastDirectory || _config.Recursive != _lastRecusrive)
			{
				_candidateFilenames = null;
			}
			if (_config.Rescan)
			{
				// this is needed in case user has set this option since the last scan
				_candidateFilenames = null;
			}

			if (_candidateFilenames == null)
			{
				_candidateFilenames = GetCandidateFilenames(_config.Directory, _config.Recursive);
				_lastDirectory = _config.Directory;
				_lastRecusrive = _config.Recursive;
			}

			if (_candidateFilenames.Count > 0)
			{
				// choose random file
				int max = _candidateFilenames.Count;
				int index = _random.Next(max);
				Trace.WriteLine(string.Format("Rnd: {0} Max: {1}", index, max));
				string filename = _candidateFilenames[index];
				try
				{
					providerImage = new ProviderImage(Image.FromFile(filename));
					providerImage.Provider = ProviderName;
					providerImage.Source = filename;
				}
				catch (Exception)
				{
					// silently ignore exceptions
				}
			}

			if (_config.Rescan)
			{
				// we will rescan the folders every time we need a new image
				// so allow the current list to be garbage collected
				_candidateFilenames = null;
			}

			return providerImage;
		}

		List<string> GetCandidateFilenames(string baseDirectory, bool recursive)
		{
			List<string> candidateFilenames = new List<string>();

			AddCandidateFilenames(baseDirectory, recursive, candidateFilenames);

			return candidateFilenames;
		}

		void AddCandidateFilenames(string baseDirectory, bool recursive, List<string> candidateFilenames)
		{
			try
			{
				DirectoryInfo dir = new DirectoryInfo(baseDirectory);

				FileSystemInfo[] infos = dir.GetFileSystemInfos();
				foreach (FileSystemInfo info in infos)
				{
					if (info is FileInfo)
					{
						if (IsImageFile(info))
						{
							candidateFilenames.Add(info.FullName);
						}
					}
					else if (info is DirectoryInfo)
					{
						if (recursive)
						{
							AddCandidateFilenames(info.FullName, recursive, candidateFilenames);
						}
					}
				}
			}
			catch (Exception)
			{
				// ignore any i/o errors
			}

		}

		bool IsImageFile(FileSystemInfo info)
		{
			string extension = info.Extension.ToLower();
			switch (extension)
			{
				case ".jpg": // drop into ".jpeg"
				case ".jpeg":
					return true;
				case ".png":
					return true;
				case ".bmp":
					return true;
				case ".gif":
					return true;
			}

			return false;
		}

	}
}
