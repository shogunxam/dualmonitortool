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

using DMT.Library.Transform;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace DMT.Library.Wallpaper
{
	public interface IWallpaperCompositor
	{
		List<ScreenMapping> AllScreens { get; }
		Rectangle DesktopRect { get; }
		Color DesktopRectBackColor { get; set; }
		void AddImage(Image image, List<int> screenIndexes, StretchType.Fit fit);
		void MoveScreens(List<int> screenIndexes, int deltaX, int deltaY);
		void ZoomScreens(List<int> screenIndexes, double factor);

		Image CreateWallpaperImage();
	}
}
