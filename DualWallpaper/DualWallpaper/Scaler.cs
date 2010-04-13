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

namespace DualWallpaper
{
	class Scaler
	{
		private int s1;
		private int s2;
		private int d1;
		private int d2;
		// positive offset image shifted to the srcRight (or down)
		// so need to add offset when calculating dest
		// and subtract when calculating src
		private int offset;

		public Scaler(int s1, int s2, int d1, int d2)
		{
			this.s1 = s1;
			this.s2 = s2;
			this.d1 = d1;
			this.d2 = d2;
			this.offset = 0;
		}

		public void Displace(int displacement)
		{
			offset += displacement;
		}

		public int DestFromSrc(int s3)
		{
			int srcDelta = s2 - s1;

			// + destDelta / 2 to minimise rounding errors
			int d3 = d1 + ((s3 - s1) * (d2 - d1) + srcDelta / 2) / srcDelta;
			d3 += offset;

			return d3;
		}

		public int SrcFromDest(int d3)
		{
			int destDelta = d2 - d1;

			d3 -= offset;

			// + destDelta / 2 to minimise rounding errors
			int s3 = s1 + ((d3 - d1) * (s2 - s1) + destDelta / 2) / destDelta;

			return s3;
		}

	}
}
