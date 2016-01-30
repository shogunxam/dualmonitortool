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
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMT.Modules.WallpaperChanger.Plugins.RandomShapes
{
	/// <summary>
	/// Configuration required for each provider from the RandomShapes plugin
	/// </summary>
	public class RandomShapesConfig
	{
		public RandomShapesConfig()
		{
		}

		public RandomShapesConfig(Dictionary<string, string> configDictionary)
		{
			Weight = ProviderHelper.ConfigToInt(configDictionary, "weight", 10);
			Description = ProviderHelper.ConfigToString(configDictionary, "description", "Random shapes");
			ShapeCount = ProviderHelper.ConfigToInt(configDictionary, "shapeCount", 20);
			RandomBackground = ProviderHelper.ConfigToBool(configDictionary, "randomBackground", true);
			UseRectangles = ProviderHelper.ConfigToBool(configDictionary, "useRectangles", true);
			UseEllipses = ProviderHelper.ConfigToBool(configDictionary, "useEllipses", true);
			UseGradients = ProviderHelper.ConfigToBool(configDictionary, "useGradients", true);
			UseAlpha = ProviderHelper.ConfigToBool(configDictionary, "useAlpha", true);
		}

		public int Weight { get; set; }
		public string Description { get; set; }
		public int ShapeCount { get; set; }
		public bool RandomBackground { get; set; }
		public bool UseRectangles { get; set; }
		public bool UseEllipses { get; set; }
		public bool UseGradients { get; set; }
		public bool UseAlpha { get; set; }

		public Dictionary<string, string> ToDictionary()
		{
			Dictionary<string, string> configDictionary = new Dictionary<string, string>();
			configDictionary["weight"] = Weight.ToString();
			configDictionary["description"] = Description;
			configDictionary["shapeCount"] = ShapeCount.ToString();
			configDictionary["randomBackground"] = RandomBackground.ToString();
			configDictionary["useRectangles"] = UseRectangles.ToString();
			configDictionary["useEllipses"] = UseEllipses.ToString();
			configDictionary["useGradients"] = UseGradients.ToString();
			configDictionary["useAlpha"] = UseAlpha.ToString();
			return configDictionary;
		}
	}
}
