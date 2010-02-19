#region copyright
// This file is part of Dual Monitor Tools which is a set of tools to assist
// users with multiple monitor setups.
// Copyright (C) 2009  Gerald Evans
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
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace DualSnap
{
	/// <summary>
	/// Remembers the last n snaps.
	/// The maximum number of snaps remembered are specified in the constructor
	/// and can be changed later if required.
	/// If more than this number of snaps are added, then the oldest snap is forgotten.
	/// </summary>
	public class SnapHistory : IEnumerable<Snap>
	{
		private int maxSnaps;
		/// <summary>
		/// Gets or sets the maximum number of snaps that are remembered.
		/// </summary>
		public int MaxSnaps
		{
			get { return maxSnaps; }
			set 
			{
				if (value >= 0)
				{
					maxSnaps = value;
					// remove excess snaps if applicable
					if (snaps.Count > maxSnaps)
					{
						snaps.RemoveRange(0, snaps.Count - maxSnaps);
					}
				}
			}
		}

		// the latest snap is always added at the end
		private List<Snap> snaps = new List<Snap>();

		/// <summary>
		/// Constructs the SnapHistory
		/// </summary>
		/// <param name="maxSnaps">Maximum number of snaps to remember</param>
		public SnapHistory(int maxSnaps)
		{
			this.maxSnaps = maxSnaps;
		}

		/// <summary>
		/// Adds the Snap as the most recent snap to the history
		/// </summary>
		/// <param name="snap">New Snap to remember.</param>
		public void Add(Snap snap)
		{
			snaps.Add(snap);
			if (snaps.Count > maxSnaps)
			{
				snaps.RemoveAt(0);
			}
		}

		/// <summary>
		/// Iterator starts with the oldest snap first.
		/// </summary>
		/// <returns></returns>
		public IEnumerator<Snap> GetEnumerator()
		{
			return snaps.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		/// <summary>
		/// Count of Snaps currently in the history.
		/// </summary>
		public int Count
		{
		    get { return snaps.Count; }
		}

		//public Snap this[int index]
		//{
		//    get { return snaps[index]; }
		//}
	}
}
