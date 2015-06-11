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
using System.IO;
//using System.Linq;
using System.Reflection;
using System.Text;
//using System.Threading.Tasks;
using System.Xml;

namespace DMT.Library.Settings
{
	class FileLocations
	{
		public string DataDirectory { get; protected set; }
		public string SettingsFilename { get; protected set; }
		public string MagicWordsFilename { get; protected set; }
		public string WallpaperProvidersFilename { get; protected set; }
		public string WallpaperFilename { get; protected set; }
		public string LogFilename { get; protected set; }

		string _homeDirectory;

		#region Singleton support
		// the single instance of the controller object
		static readonly FileLocations instance = new FileLocations();

		// Explicit static constructor to tell C# compiler
		// not to mark type as beforefieldinit
		static FileLocations()
		{
		}

		FileLocations()
		{
			LoadFileLocations();
		}

		public static FileLocations Instance
		{
			get
			{
				return instance;
			}
		}
		#endregion

		public string Filename(string filename)
		{
			return Path.Combine(DataDirectory, filename);
		}

		void LoadFileLocations()
		{
			_homeDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
			// by default, we use the home directory as the data directory to simplify portable usage
			DataDirectory = _homeDirectory;

			// The Locations file ALWAYS lives in same directory as executable (if it is used)
			string fileLocationsFilename = Path.Combine(_homeDirectory, "DmtFileLocations.xml");
			//bool haveLocationsFile = File.Exists(fileLocationsFilename);
			List<Tuple<string, string>> LocationRemaps = null;
			if (File.Exists(fileLocationsFilename))
			{
				// check too see if we are told to pick up anything from anywhere else
				LocationRemaps = GetLocationRemaps(fileLocationsFilename);
				// check if data directory was specified in the file
				Tuple<string, string> directoryRemap = LocationRemaps.Find(m => m.Item1 == "datadirectory");
				if (directoryRemap != null)
				{
					DataDirectory = directoryRemap.Item2;
				}
			}

			// set default locations of files - which are in same directory as executable
			SettingsFilename = Path.Combine(DataDirectory, "DmtSettings.xml");
			MagicWordsFilename = Path.Combine(DataDirectory, "DmtMagicWords.xml");
			WallpaperProvidersFilename = Path.Combine(DataDirectory, "DmtWallpaperProviders.xml");
			WallpaperFilename = Path.Combine(DataDirectory, "DmtWallpaper.bmp");

			// default is to to have no logfile, unless explicitly set
			LogFilename = null;

			if (LocationRemaps != null)
			{
				// allow individual locations to be remapped
				foreach (Tuple<string, string> remap in LocationRemaps)
				{
					SetFileLocation(remap.Item1, remap.Item2);
				}
			}
		}

		List<Tuple<string, string>> GetLocationRemaps(string fileLocationsFilename)
		{
			List<Tuple<string, string>> LocationRemaps = new List<Tuple<string, string>>();

			try
			{
				using (Stream stream = File.Open(fileLocationsFilename, FileMode.Open, FileAccess.Read))
				{
					XmlTextReader reader = new XmlTextReader(stream);
					while (reader.Read())
					{
						if (reader.NodeType == XmlNodeType.Element)
						{
							bool isEmptyElement = reader.IsEmptyElement;
							if (reader.LocalName == "filelocation")
							{
								string name = reader.GetAttribute("name");
								string value = reader.GetAttribute("value");
								// expand any environment variables like %APPDATA% 
								value = System.Environment.ExpandEnvironmentVariables(value);
								LocationRemaps.Add(new Tuple<string, string>(name, value));
							}
						}
					}
				}
			}
			catch (Exception)
			{
			}

			return LocationRemaps;
		}

		void SetFileLocation(string name, string value)
		{
			switch (name.ToLower())
			{
				case "settings":
					SettingsFilename = Path.Combine(DataDirectory, value);
					break;

				case "magicwords":
					MagicWordsFilename = Path.Combine(DataDirectory, value);
					break;

				case "wallpaperproviders":
					WallpaperProvidersFilename = Path.Combine(DataDirectory, value);
					break;

				case "wallpaper":
					WallpaperFilename = Path.Combine(DataDirectory, value);
					break;

				case "log":
					LogFilename = Path.Combine(DataDirectory, value);
					break;

				default:
					break;
			}
		}
	}
}