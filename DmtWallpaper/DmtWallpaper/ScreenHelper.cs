#region copyright
// This file is part of Dual Monitor Tools which is a set of tools to assist
// users with multiple monitor setups.
// Copyright (C) 2009-2016  Gerald Evans
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
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DmtWallpaper
{
	/// <summary>
	/// Utility class to help in handling multiple screens.
	/// 
	/// Copied from DMT/Library/GuiUtils/ScreenHelper.cs
	/// and stripped down
	/// </summary>
	static class ScreenHelper
	{
		/// <summary>
		/// Get the bounding rectangle that covers all areas of all screens
		/// </summary>
		/// <returns>Virtual desktop rectangle</returns>
		public static Rectangle GetVitrualDesktopRect() // From Library/GuiUtils/ScreenHelper.cs
		{
			Rectangle boundingRect = new Rectangle();
			for (int screenIndex = 0; screenIndex < Screen.AllScreens.Length; screenIndex++)
			{
				if (screenIndex == 0)
				{
					boundingRect = Screen.AllScreens[screenIndex].Bounds;
				}
				else
				{
					boundingRect = Rectangle.Union(boundingRect, Screen.AllScreens[screenIndex].Bounds);
				}
			}

			return boundingRect;
		}
	}
}
