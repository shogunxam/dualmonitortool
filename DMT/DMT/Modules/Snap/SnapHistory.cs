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
	using System.Collections;
	using System.Collections.Generic;
	using System.Drawing;
	using System.Text;

	/// <summary>
	/// Remembers the last n snaps.
	/// The maximum number of snaps remembered are specified in the constructor
	/// and can be changed later if required.
	/// If more than this number of snaps are added, then the oldest snap is forgotten.
	/// </summary>
	public class SnapHistory : IEnumerable<Snap>
	{
		int _maxSnaps;

		// list of snaps with the latest at the end
		List<Snap> _snaps = new List<Snap>();

		/// <summary>
		/// Initialises a new instance of the <see cref="SnapHistory" /> class.
		/// </summary>
		/// <param name="maxSnaps">Maximum number of snaps to remember</param>
		public SnapHistory(int maxSnaps)
		{
			_maxSnaps = maxSnaps;
		}

		/// <summary>
		/// Gets or sets the maximum number of snaps that are remembered.
		/// </summary>
		public int MaxSnaps
		{
			get 
			{ 
				return _maxSnaps; 
			}

			set 
			{
				if (value >= 0)
				{
					_maxSnaps = value;

					// remove excess snaps if applicable
					if (_snaps.Count > _maxSnaps)
					{
						_snaps.RemoveRange(0, _snaps.Count - _maxSnaps);
					}
				}
			}
		}

		/// <summary>
		/// Gets the count of Snaps currently in the history.
		/// </summary>
		public int Count
		{
			get { return _snaps.Count; }
		}

		/// <summary>
		/// Adds the Snap as the most recent snap to the history
		/// </summary>
		/// <param name="snap">New Snap to remember.</param>
		public void Add(Snap snap)
		{
			_snaps.Add(snap);
			if (_snaps.Count > _maxSnaps)
			{
				_snaps.RemoveAt(0);
			}
		}

		/// <summary>
		/// Deletes all snaps in the history
		/// </summary>
		public void DeleteAll()
		{
			_snaps.Clear();
		}

		/// <summary>
		/// Deletes at most a single snap using the same image
		/// </summary>
		/// <param name="image">The image we are trying to delete</param>
		/// <returns>true if a snap was deleted</returns>
		public bool Delete(Image image)
		{
			foreach (Snap snap in _snaps)
			{
				if (snap.Image == image)
				{
					_snaps.Remove(snap);
					return true;
				}
			}

			return false;
		}

		/// <summary>
		/// Returns the last snap available
		/// </summary>
		/// <returns>The last snap, or null if no snaps</returns>
		public Snap LastSnap()
		{
			if (_snaps.Count > 0)
			{
				return _snaps[_snaps.Count - 1];
			}
			else
			{
				return null;
			}
		}

		/// <summary>
		/// Iterator starts with the oldest snap first.
		/// </summary>
		/// <returns>Enumerator for the snaps</returns>
		public IEnumerator<Snap> GetEnumerator()
		{
			return _snaps.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}
