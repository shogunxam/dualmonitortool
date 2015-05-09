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

using DMT.Library.WallpaperPlugin;
using System;
using System.Collections.Generic;
//using System.ComponentModel.Composition;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMT.Modules.WallpaperChanger.Plugins.LocalDisk
{

	//[Export(typeof(IDWC_Plugin))]
	//[ExportMetadata("Name", LocalDiskPlugin._pluginName)]
	public class LocalDiskPlugin : IDWC_Plugin
	{
		const string _pluginName = "Local disk";
		public const string PluginVersion = "0.0";

		public static string PluginName { get { return _pluginName; } }
		public static Image PluginImage { get { return Properties.Resources.LocalDiskPlugin; } }

		public string Name { get { return PluginName; } }
		public Image Image { get { return PluginImage; } }

		public IImageProvider CreateProvider(Dictionary<string, string> config)
		{
			return new LocalDiskProvider(config);
		}
	}
}
