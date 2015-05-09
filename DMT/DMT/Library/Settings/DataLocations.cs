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

namespace DMT.Library.Settings
{
	class DataLocations
	{
		public string SettingsFilename { get; protected set; }
		public string MagicWordsFilename { get; set; }
		public string WallpaperProvidersFilename { get; set; }

		#region Singleton support
		// the single instance of the controller object
		static readonly DataLocations instance = new DataLocations();

		// Explicit static constructor to tell C# compiler
		// not to mark type as beforefieldinit
		static DataLocations()
		{
		}

		DataLocations()
		{
			LoadDataLocations();
		}

		public static DataLocations Instance
		{
			get
			{
				return instance;
			}
		}
		#endregion

		void LoadDataLocations()
		{
			// TODO - want to load dataDir from xml file

			string dataDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

			SettingsFilename = Path.Combine(dataDir, "DmtSettings.xml");
			MagicWordsFilename = Path.Combine(dataDir, "DmtMagicWords.xml");
			WallpaperProvidersFilename = Path.Combine(dataDir, "DmtWallpaperProviders.xml");
		}
	}
}
