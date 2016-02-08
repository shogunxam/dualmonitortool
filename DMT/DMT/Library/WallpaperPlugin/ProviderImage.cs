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

namespace DMT.Library.WallpaperPlugin
{
	using System;
	using System.Collections.Generic;
	using System.Drawing;
	using System.Linq;
	using System.Text;

	/// <summary>
	/// An image returned by a provider
	/// </summary>
	public class ProviderImage : IDisposable
	{
		/// <summary>
		/// Initialises a new instance of the <see cref="ProviderImage" /> class.
		/// </summary>
		/// <param name="image">The image</param>
		public ProviderImage(Image image)
		{
			Image = image;
		}

		~ProviderImage()
		{
			Dispose(false);
		}

		/// <summary>
		/// Gets or sets the image
		/// </summary>
		public Image Image { get; protected set; }

		/// <summary>
		/// Gets or sets the name of the provider
		/// </summary>
		public string Provider { get; set; }

		/// <summary>
		/// Gets or sets a URL to the provider
		/// </summary>
		public string ProviderUrl { get; set; }

		/// <summary>
		/// Gets or sets a description of the image source
		/// </summary>
		public string Source { get; set; }

		/// <summary>
		/// Gets or sets a URL to the image source
		/// </summary>
		public string SourceUrl { get; set; }

		/// <summary>
		/// Gets or sets the name of the photographer
		/// </summary>
		public string Photographer { get; set; }

		/// <summary>
		/// Gets or sets a URL to the photographer
		/// </summary>
		public string PhotographerUrl { get; set; }

		/// <summary>
		/// Gets or sets more information about the image
		/// </summary>
		public string MoreInfo { get; set; }

		/// <summary>
		/// Gets or sets a URL containing more information about the image
		/// </summary>
		public string MoreInfoUrl { get; set; }

		/// <summary>
		/// Dispose of any resources held
		/// </summary>
		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (Image != null)
				{
					Image.Dispose();
				}
			}
		}
	}
}
