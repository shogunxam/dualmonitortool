#region copyright
// This file is part of Dual Monitor Tools which is a set of tools to assist
// users with multiple monitor setups.
// Copyright (C) 2010-2015  Gerald Evans
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

namespace DMT.Library.Transform
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

		private double zoom;

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
			this.zoom = 1.0;
		}

		/// <summary>
		/// Specifies an extra displacement to apply to the co-ords in the second space
		/// </summary>
		/// <param name="displacement">Extra amount to displace the second space co-ords by</param>
		public void Displace(int displacement)
		{
			//offset += displacement;
			d1 += displacement;
			d2 += displacement;
		}

		public void Zoom(int center, double factor)
		{
			// update the total zoom, but limit to 10X in or out
			double newZoom = zoom * factor;
			if (newZoom < 0.1)
			{
				newZoom = 0.1;
			}
			else if (newZoom > 10.0)
			{
				newZoom = 10.0;
			}

			if (newZoom != zoom)
			{
				double realFactor = newZoom / zoom;

				int range = (d2 - d1) / 2;
				int newRange = Convert.ToInt32(Math.Round(range * realFactor));
				if (newRange > 0)
				{
					int delta = newRange - range;

					// need to offset image so that we zoom around center
					int offset = (center - (d1 + d2) / 2) * delta / range;
					d1 -= offset;
					d2 -= offset;

					// now zoom in/out
					d1 -= delta;
					d2 += delta;
				}
				zoom = newZoom;
			}

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
