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

namespace DMT.Library.Transform
{
	using System;
	using System.Collections.Generic;
	using System.Drawing;
	using System.Linq;
	using System.Text;

	/// <summary>
	/// Utility class to help with scaling co-ordinates
	/// </summary>
	public static class ScaleHelper
	{
		/// <summary>
		/// Scales the source size so that it will be as large as possible
		/// and still fit within the target size
		/// while maintaining aspect ratio
		/// </summary>
		/// <param name="src">Source size</param>
		/// <param name="target">Target size</param>
		/// <returns>Scaled size</returns>
		public static Size UnderScale(Size src, Size target)
		{
			Size dest = target;

			// if one of the sizes is zero, then something has gone wrong?
			if (src.Width > 0 && src.Height > 0)
			{
				// The bigger a factor is, the more that dimension needs to be increased 
				// to hit its target dimension
				int widthFactor = target.Width * src.Height;
				int heightFactor = target.Height * src.Width;

				// need the minimum factor to keep everything within the target size
				if (widthFactor < heightFactor)
				{
					// width factor is minimum, so width will be full width, just scale the height
					dest.Height = (src.Height * dest.Width) / src.Width;
				}
				else 
				{
					// height factor is minimum, so height will be full height, just scale the width
					dest.Width = (src.Width * dest.Height) / src.Height;
				}
			}

			return dest;
		}
	}
}
