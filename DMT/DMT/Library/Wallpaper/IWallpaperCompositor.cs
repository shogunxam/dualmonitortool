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

namespace DMT.Library.Wallpaper
{
	using System;
	using System.Collections.Generic;
	using System.Drawing;
	using System.Text;

	using DMT.Library.Transform;

	/// <summary>
	/// Interface for a wallpaper compositor 
	/// which can create a single wallpaper image 
	/// when instructed what to display on individual screens
	/// </summary>
	public interface IWallpaperCompositor
	{
		/// <summary>
		/// Gets the screen mapping for each screen
		/// </summary>
		List<ScreenMapping> AllScreens { get; }

		/// <summary>
		/// Gets the rectangle that covers all screens
		/// </summary>
		Rectangle DesktopRect { get; }

		/// <summary>
		/// Gets or sets the background colour 
		/// (that isn't covered with an image)
		/// </summary>
		Color DesktopRectBackColor { get; set; }

		/// <summary>
		/// Adds an image that covers one or more monitors
		/// </summary>
		/// <param name="image">The image to add</param>
		/// <param name="screenIndexes">The monitors that this image should cover</param>
		/// <param name="fit">How to fit the image to the monitor(s)</param>
		void AddImage(Image image, List<int> screenIndexes, StretchType.Fit fit);

		/// <summary>
		/// Moves the existing image by the specified displacement
		/// </summary>
		/// <param name="screenIndexes">Monitors to have their image adjusted</param>
		/// <param name="deltaX">Delta x value</param>
		/// <param name="deltaY">Delta y value</param>
		void MoveScreens(List<int> screenIndexes, int deltaX, int deltaY);

		/// <summary>
		/// Zooms the existing image by the specified magnification factor
		/// </summary>
		/// <param name="screenIndexes">Monitors to have their image adjusted</param>
		/// <param name="factor">Zoom factor</param>
		void ZoomScreens(List<int> screenIndexes, double factor);

		/// <summary>
		/// Gets the composite image covering all monitors 
		/// </summary>
		/// <returns>Composite image</returns>
		Image CreateWallpaperImage();
	}
}
