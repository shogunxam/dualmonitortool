#region copyright
// This file is part of Dual Monitor Tools which is a set of tools to assist
// users with multiple monitor setups.
// Copyright (C) 2016  Gerald Evans
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

using DMT.Library.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DMT.Modules.WallpaperChanger.Plugins.LocalDisk
{
	/// <summary>
	/// Allows the integers between min and max (both inclusive) to be cycled through in a random order
	/// </summary>
	public class NumberCycler
	{
		int[] _cycleList = null;
		int _cycleListLength = 0;

		public NumberCycler(int min, int max)
		{
			_cycleListLength = max - min + 1;
			_cycleList = new int[_cycleListLength];

			for (int n = 0; n < _cycleListLength; n++)
			{
				_cycleList[n] = n + min;
			}
		}

		public int Count
		{
			get
			{
				return _cycleListLength;
			}
		}

		public int NextRandom()
		{
			int ret = int.MinValue;

			if (_cycleListLength > 0)
			{
				int lastIndex = _cycleListLength - 1;
				int rndIndex = RNG.Next(0, _cycleListLength); // lastIndex);  // (inclusive, exclusive)

				// take item at this index
				ret = _cycleList[rndIndex];

				// put the last entry into the slot where we have just taken an entry from
				// doesn't matter if the taken index is the the last index
				_cycleList[rndIndex] = _cycleList[lastIndex];

				// can now just delete last entry
				_cycleListLength--;
			}

			return ret;
		}
	}
}
