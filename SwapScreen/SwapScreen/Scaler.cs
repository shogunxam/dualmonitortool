#region copyright
// This file is part of Dual Monitor Tools which is a set of tools to assist
// users with multiple monitor setups.
// Copyright (C) 2010  Gerald Evans
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
using System.Text;

namespace SwapScreen
{
	/// <summary>
	/// Class to assist in scaling values between 2 co-ordinate spaces.
	/// This works in a single dimension, so you will need 2 instances for x and y.
	/// 
	/// Class copied from Dual Wallpaper and simplified.
	/// </summary>
	class Scaler
	{
		// 2 co-ords from first space
		private int s1;
		private int s2;
		// corresponding 2 co-ords from second space
		private int d1;
		private int d2;

		/// <summary>
		/// Ctor takes 2 1D co-rdinates in one space
		/// that map to the given co-ordinates in the other space
		/// </summary>
		/// <param name="s1">First co-ord in first space</param>
		/// <param name="s2">Second co-ord in first space</param>
		/// <param name="d1">What s1 maps to in the second space</param>
		/// <param name="d2">What s2 maps to in the second space</param>
		public Scaler(int s1, int s2, int d1, int d2)
		{
			this.s1 = s1;
			this.s2 = s2;
			this.d1 = d1;
			this.d2 = d2;
		}

		/// <summary>
		/// Given a co-ord in first space, returns corresponding co-ord in second space.
		/// </summary>
		/// <param name="s3">co-ord in first space</param>
		/// <returns>corresponding co-ord in second space</returns>
		public int DestFromSrc(int s3)
		{
			int srcDelta = s2 - s1;

			// + destDelta / 2 to minimise rounding errors
			int d3 = d1 + ((s3 - s1) * (d2 - d1) + srcDelta / 2) / srcDelta;

			return d3;
		}

		/// <summary>
		/// Given a co-ord in second space, returns corresponding co-ord in first space.
		/// </summary>
		/// <param name="d3">co-ord in second space</param>
		/// <returns>corresponding co-ord in first space</returns>
		public int SrcFromDest(int d3)
		{
			int destDelta = d2 - d1;

			// + destDelta / 2 to minimise rounding errors
			int s3 = s1 + ((d3 - d1) * (s2 - s1) + destDelta / 2) / destDelta;

			return s3;
		}
	}
}
