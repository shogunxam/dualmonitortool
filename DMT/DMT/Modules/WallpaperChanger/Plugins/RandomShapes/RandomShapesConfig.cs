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

namespace DMT.Modules.WallpaperChanger.Plugins.RandomShapes
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;

	using DMT.Library.WallpaperPlugin;

	/// <summary>
	/// Configuration required for each provider from the RandomShapes plugin
	/// </summary>
	public class RandomShapesConfig
	{
		/// <summary>
		/// Initialises a new instance of the <see cref="RandomShapesConfig" /> class.
		/// </summary>
		public RandomShapesConfig()
		{
		}

		/// <summary>
		/// Initialises a new instance of the <see cref="RandomShapesConfig" /> class.
		/// </summary>
		/// <param name="configDictionary">Configuration as a dictionary</param>
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

		/// <summary>
		/// Gets or sets the wight for this provider
		/// </summary>
		public int Weight { get; set; }

		/// <summary>
		/// Gets or sets the description for this provider
		/// </summary>
		public string Description { get; set; }

		/// <summary>
		/// Gets or sets the number of shapes to draw
		/// </summary>
		public int ShapeCount { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether the background should be a random colour
		/// </summary>
		public bool RandomBackground { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether to draw rectangles
		/// </summary>
		public bool UseRectangles { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether to draw ellipses
		/// </summary>
		public bool UseEllipses { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether to use colour gradients
		/// </summary>
		public bool UseGradients { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether the shapes should be opaque
		/// </summary>
		public bool UseAlpha { get; set; }

		/// <summary>
		/// Gets the configuration as a dictionary ready for saving to disk
		/// </summary>
		/// <returns>Dictionary representation of configuration</returns>
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
