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
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace DMT.Library.Settings
{
	class FileLocations
	{
		public string SettingsFilename { get; protected set; }
		public string MagicWordsFilename { get; protected set; }
		public string WallpaperProvidersFilename { get; protected set; }
		public string WallpaperFilename { get; protected set; }

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

		void LoadFileLocations()
		{
			string homeDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

			// set default locations of files - which are in same directory as executable
			SettingsFilename = Path.Combine(homeDir, "DmtSettings.xml");
			MagicWordsFilename = Path.Combine(homeDir, "DmtMagicWords.xml");
			WallpaperProvidersFilename = Path.Combine(homeDir, "DmtWallpaperProviders.xml");
			WallpaperFilename = Path.Combine(homeDir, "DmtWallpaper.bmp");

			// allow these values to be overridden by paths in DmtFileLocations.xml
			string fileLocationsFilename = Path.Combine(homeDir, "DmtFileLocations.xml");
			if (File.Exists(fileLocationsFilename))
			{
				LoadLocationsFromFile(fileLocationsFilename);
			}
		}

		void LoadLocationsFromFile(string fileLocationsFilename)
		{
			try
			{
				using (Stream stream = File.Open(fileLocationsFilename, FileMode.Open))
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
								SetFileLocation(name, value);
							}
						}
					}
				}
			}
			catch (Exception)
			{
			}
		}

		void SetFileLocation(string name, string value)
		{
			switch (name.ToLower())
			{
				case "settings":
					SettingsFilename = value;
					break;

				case "magicwords":
					MagicWordsFilename = value;
					break;

				case "wallpaperproviders":
					WallpaperProvidersFilename = value;
					break;

				case "wallpaper":
					WallpaperFilename = value;
					break;

				default:
					break;
			}
		}
	}
}
