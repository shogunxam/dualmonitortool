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
	using System.Drawing;
	using System.Drawing.Drawing2D;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;

	using DMT.Library.Utils;
	using DMT.Library.WallpaperPlugin;
	
	/// <summary>
	/// An instance of a provider from the Random Shapes plugin
	/// </summary>
	class RandomShapesProvider : IImageProvider
	{
		RandomShapesConfig _config;

		/// <summary>
		/// Initialises a new instance of the <see cref="RandomShapesProvider" /> class.
		/// </summary>
		/// <param name="config">Configuration for unsplash scraper</param>
		public RandomShapesProvider(Dictionary<string, string> config)
		{
			_config = new RandomShapesConfig(config);
		}

		/// <summary>
		/// Gets the provider name - same for all instances of this class
		/// </summary>
		public string ProviderName
		{
			get
			{
				return RandomShapesPlugin.PluginName;
			}
		}

		/// <summary>
		/// Gets the provider image - same for all instances of this class
		/// </summary>
		public Image ProviderImage
		{
			get
			{
				return RandomShapesPlugin.PluginImage;
			}
		}

		/// <summary>
		/// Gets the provider version - same for all instances of this class
		/// </summary>
		public string Version
		{
			get
			{
				return RandomShapesPlugin.PluginVersion;
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
			RandomShapesForm dlg = new RandomShapesForm(_config);
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
		/// <returns>Randomly generated image</returns>
		public ProviderImage GetRandomImage(Size optimumSize, int screenIndex)
		{
			ProviderImage providerImage = new ProviderImage(new Bitmap(optimumSize.Width, optimumSize.Height));
			providerImage.Provider = ProviderName;

			using (Graphics g = Graphics.FromImage(providerImage.Image))
			{
				if (_config.RandomBackground)
				{
					// first add the background
					Color color1 = GetRandomColor(false);
					Color color2 = GetRandomColor(false);
					Rectangle rect = new Rectangle(new Point(0, 0), optimumSize);
					using (Brush brush = GetRandomBrush(rect, false))	// don't use alpha on background
					{
						g.FillRectangle(brush, rect);
					}
				}

				for (int i = 0; i < _config.ShapeCount; i++)
				{
					Rectangle rect = GetRandomRectangle(optimumSize, i, _config.ShapeCount);
					using (Brush brush = GetRandomBrush(rect, _config.UseAlpha))
					{
						AddShape(g, rect, brush);
					}
				}
			}

			return providerImage;
		}

		void AddShape(Graphics g, Rectangle rect, Brush brush)
		{
			// if further shapes are added, will need something like HashSet<EShape> to do this
			bool drawRectangle = _config.UseRectangles;
			bool drawEllipse = _config.UseEllipses;
			if (drawRectangle && drawEllipse)
			{
				// need to choose one or the other
				if (RNG.Next(0, 2) == 0)
				{
					drawRectangle = false;
				}
				else
				{
					drawEllipse = false;
				}
			}

			if (drawRectangle)
			{
				g.FillRectangle(brush, rect);
			}
			else if (drawEllipse)
			{
				g.FillEllipse(brush, rect);
			}
		}

		Brush GetRandomBrush(Rectangle rect, bool useAlpha)
		{
			Brush brush;
			if (_config.UseGradients)
			{
				Color color1 = GetRandomColor(useAlpha);
				Color color2 = GetRandomColor(useAlpha);
				brush = new LinearGradientBrush(rect.Location, new Point(rect.Right, rect.Bottom), color1, color2);
			}
			else
			{
				Color color = GetRandomColor(_config.UseAlpha);
				brush = new SolidBrush(color);
			}

			return brush;
		}

		Color GetRandomColor(bool useAlpha)
		{
			int alpha = useAlpha ? RNG.Next(256) : 255;
			int red = RNG.Next(256);
			int green = RNG.Next(256);
			int blue = RNG.Next(256);
			Color color = Color.FromArgb(alpha, red, green, blue);
			return color;
		}

		Rectangle GetRandomRectangle(Size maximumSize, int curNum, int totalNum)
		{
			int width;
			int height;

			// want to make rectangle smaller as we go along
			int multiplier = totalNum - curNum - 1;
			int maxWidth = maximumSize.Width / 10 + maximumSize.Width * 9 * multiplier / (totalNum * 10);
			int maxHeight = maximumSize.Height / 10 + maximumSize.Height * 9 * multiplier / (totalNum * 10);

			width = RNG.Next(maximumSize.Width / 10, maxWidth);
			height = RNG.Next(maximumSize.Height / 10, maxHeight);

			int x = RNG.Next(-width, maximumSize.Width);
			int y = RNG.Next(-height, maximumSize.Height);

			return new Rectangle(x, y, width, height);
		}
	}
}
