#region copyright
// This file is part of Dual Monitor Tools which is a set of tools to assist
// users with multiple monitor setups.
// Copyright (C) 2009-2015  Gerald Evans
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

namespace DMT.Modules.Snap
{
	using System;
	using System.Collections.Generic;
	using System.Drawing;
	using System.Text;

	/// <summary>
	/// A single snapshot of the screen
	/// </summary>
	public class Snap
	{
		/// <summary>
		/// Initialises a new instance of the <see cref="Snap" /> class.
		/// </summary>
		/// <param name="image">Bitmap containing the screen image</param>
		public Snap(Bitmap image)
		{
			Image = image;
		}

		/// <summary>
		/// Gets  the snapshot image as a Bitmap
		/// </summary>
		public Bitmap Image { get; private set; }
	}
}
