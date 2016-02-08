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

namespace DMT.Library.Transform
{
	using System;
	using System.Collections.Generic;
	using System.Text;

	/// <summary>
	/// Class to assist in scaling values between 2 co-ordinate spaces.
	/// This works in a single dimension, so you will need 2 instances for x and y.
	/// <para />
	/// Class copied from Dual Wallpaper and simplified.
	/// </summary>
	class Scaler
	{
		// 2 co-ordinates from first space
		int _src1;
		int _src2;

		// corresponding 2 co-ordinates from second space
		int _dest1;
		int _dest2;

		double _zoom;

		/// <summary>
		/// Initialises a new instance of the <see cref="Scaler" /> class.
		/// Takes 2 1D co-ordinates in one space
		/// that map to the given co-ordinates in the other space
		/// </summary>
		/// <param name="src1">First co-ordinate in first space</param>
		/// <param name="src2">Second co-ordinate in first space</param>
		/// <param name="dest1">What s1 maps to in the second space</param>
		/// <param name="dest2">What s2 maps to in the second space</param>
		public Scaler(int src1, int src2, int dest1, int dest2)
		{
			_src1 = src1;
			_src2 = src2;
			_dest1 = dest1;
			_dest2 = dest2;
			_zoom = 1.0;
		}

		/// <summary>
		/// Specifies an extra displacement to apply to the co-ordinates in the second space
		/// </summary>
		/// <param name="displacement">Extra amount to displace the second space co-ordinates by</param>
		public void Displace(int displacement)
		{
			_dest1 += displacement;
			_dest2 += displacement;
		}

		/// <summary>
		/// Specifies a zoom factor
		/// </summary>
		/// <param name="center">The origin that doesn't move during a zoom</param>
		/// <param name="factor">The zoom factor</param>
		public void Zoom(int center, double factor)
		{
			// update the total zoom, but limit to 10X in or out
			double newZoom = _zoom * factor;
			if (newZoom < 0.1)
			{
				newZoom = 0.1;
			}
			else if (newZoom > 10.0)
			{
				newZoom = 10.0;
			}

			if (newZoom != _zoom)
			{
				double realFactor = newZoom / _zoom;

				int range = (_dest2 - _dest1) / 2;
				int newRange = Convert.ToInt32(Math.Round(range * realFactor));
				if (newRange > 0)
				{
					int delta = newRange - range;

					// need to offset image so that we zoom around center
					int offset = (center - (_dest1 + _dest2) / 2) * delta / range;
					_dest1 -= offset;
					_dest2 -= offset;

					// now zoom in/out
					_dest1 -= delta;
					_dest2 += delta;
				}

				_zoom = newZoom;
			}
		}

		/// <summary>
		/// Given a co-ordinate in first space, returns corresponding co-ordinate in second space.
		/// </summary>
		/// <param name="s3">co-ordinate in first space</param>
		/// <returns>corresponding co-ordinate in second space</returns>
		public int DestFromSrc(int s3)
		{
			int srcDelta = _src2 - _src1;

			// + destDelta / 2 to minimise rounding errors
			int d3 = _dest1 + ((s3 - _src1) * (_dest2 - _dest1) + srcDelta / 2) / srcDelta;

			return d3;
		}

		/// <summary>
		/// Given a co-ordinate in second space, returns corresponding co-ordinate in first space.
		/// </summary>
		/// <param name="d3">co-ordinate in second space</param>
		/// <returns>corresponding co-ordinate in first space</returns>
		public int SrcFromDest(int d3)
		{
			int destDelta = _dest2 - _dest1;

			// + destDelta / 2 to minimise rounding errors
			int s3 = _src1 + ((d3 - _dest1) * (_src2 - _src1) + destDelta / 2) / destDelta;

			return s3;
		}
	}
}
