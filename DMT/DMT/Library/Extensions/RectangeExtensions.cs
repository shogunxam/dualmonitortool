#region copyright
// This file is part of Dual Monitor Tools which is a set of tools to assist
// users with multiple monitor setups.
// Copyright (C) 2017  Gerald Evans
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

namespace DMT.Library.Extensions
{
	// Extends the Rectangle class to make it easier to find half screens and quadrants
	// TODO: handle odd widths/heights so don't lose a pixel at end
	public static class RectangeExtensions
	{
		public static Rectangle LeftHalf(this Rectangle rectangle)
		{
			return new Rectangle(rectangle.Left, rectangle.Top, rectangle.Width / 2, rectangle.Height);
		}

		public static Rectangle RightHalf(this Rectangle rectangle)
		{
			return new Rectangle(rectangle.Left + rectangle.Width / 2, rectangle.Top, rectangle.Width / 2, rectangle.Height);
		}

		public static Rectangle TopHalf(this Rectangle rectangle)
		{
			return new Rectangle(rectangle.Left, rectangle.Top, rectangle.Width, rectangle.Height / 2);
		}

		public static Rectangle BottomHalf(this Rectangle rectangle)
		{
			return new Rectangle(rectangle.Left, rectangle.Top + rectangle.Height / 2, rectangle.Width, rectangle.Height / 2);
		}

		public static Rectangle TopLeftQuadrant(this Rectangle rectangle)
		{
			return new Rectangle(rectangle.Left, rectangle.Top, rectangle.Width / 2, rectangle.Height / 2);
		}

		public static Rectangle TopRightQuadrant(this Rectangle rectangle)
		{
			return new Rectangle(rectangle.Left + rectangle.Width / 2, rectangle.Top, rectangle.Width / 2, rectangle.Height / 2);
		}

		public static Rectangle BottomLeftQuadrant(this Rectangle rectangle)
		{
			return new Rectangle(rectangle.Left, rectangle.Top + rectangle.Height / 2, rectangle.Width / 2, rectangle.Height / 2);
		}

		public static Rectangle BottomRightQuadrant(this Rectangle rectangle)
		{
			return new Rectangle(rectangle.Left + rectangle.Width / 2, rectangle.Top + rectangle.Height / 2, rectangle.Width / 2, rectangle.Height / 2);
		}
	}
}
