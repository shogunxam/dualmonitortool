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

namespace DMT.Library.Environment
{
	using System;
	using System.Collections.Generic;
	using System.Drawing;
	using System.Text;

	/// <summary>
	/// Represents all monitors
	/// </summary>
	public class Monitors : List<Monitor>
	{
		/// <summary>
		/// Gets the bounds rectangle for the union of all monitors
		/// </summary>
		public Rectangle Bounds
		{
			get
			{
				Rectangle boundingRect = Rectangle.Empty;

				for (int monitorIndex = 0; monitorIndex < this.Count; monitorIndex++)
				{
					if (monitorIndex == 0)
					{
						boundingRect = this[monitorIndex].Bounds;
					}
					else
					{
						boundingRect = Rectangle.Union(boundingRect, this[monitorIndex].Bounds);
					}
				}

				return boundingRect;
			}
		}

		/// <summary>
		/// Gets the working area rectangle for the union of all monitors
		/// </summary>
		public Rectangle WorkingArea
		{
			get
			{
				Rectangle boundingRect = Rectangle.Empty;

				for (int monitorIndex = 0; monitorIndex < this.Count; monitorIndex++)
				{
					if (monitorIndex == 0)
					{
						boundingRect = this[monitorIndex].WorkingArea;
					}
					else
					{
						boundingRect = Rectangle.Union(boundingRect, this[monitorIndex].WorkingArea);
					}
				}

				return boundingRect;
			}
		}
	}
}
