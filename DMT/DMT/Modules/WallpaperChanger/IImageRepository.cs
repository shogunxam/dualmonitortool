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

namespace DMT.Modules.WallpaperChanger
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel;
	using System.Drawing;
	using System.Text;

	using DMT.Library.Wallpaper;
	using DMT.Library.WallpaperPlugin;

	/// <summary>
	/// Interface for an image repository
	/// </summary>
	public interface IImageRepository
	{
		/// <summary>
		/// Gets the list of providers for use in a DataGrid
		/// </summary>
		BindingList<IImageProvider> DataSource { get; }

		/// <summary>
		/// Saves the current list of providers together with their configuration
		/// </summary>
		/// <returns>True if saved successfully</returns>
		bool Save();

		/// <summary>
		/// Gets a random image from the repository
		/// </summary>
		/// <param name="optimumSize">Optimum size for the image. May be ignored by the provider</param>
		/// <param name="screenIndex">Screen index image is for</param>
		/// <returns>Random image, or null if unable to return image</returns>
		ProviderImage GetRandomImage(Size optimumSize, int screenIndex);
	}
}
