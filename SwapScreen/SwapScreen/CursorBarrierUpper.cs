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
	/// Represents an upper barrier for cursor movement.
	/// This is for 1D only, so 2 of these classes will be needed
	/// to constrain cursor movement.
	/// </summary>
	class CursorBarrierUpper
	{
		// could have base class for these and ctor, but not really worth it
		private bool active;
		private int limit;
		private int minForce;
		private int totalForce;

		/// <summary>
		/// Constructs the upper barrier.
		/// </summary>
		/// <param name="active">Indicates if the barrier is active.  If false, the other parameters are ignored.</param>
		/// <param name="limit">This is the upper limit which we try to keep the cursor above. 
		/// Note this value is inclusive so the cursor is allowed to reach this value, but no higher.</param>
		/// <param name="minForce">This is the amount of force required to break through the barrier.
		/// If this is Int32.MaxValue then the barrier is solid and no amount of movement can break through it.
		/// Otherwise it represents the number of extra screen pixels the cursor has to move before
		/// we allow the cursor to break through the barrier.</param>
		public CursorBarrierUpper(bool active, int limit, int minForce)
		{
			this.active = active;
			this.limit = limit;
			this.minForce = minForce;
			this.totalForce = 0;
		}

		/// <summary>
		/// Checks if the cursor has broken through the barrier.
		/// </summary>
		/// <param name="newValue">On entry this is the position that Windows wants to put the cursor.
		/// On exit we adjust this value if needed to limit the position by the barrier, or
		/// if it has broken through the barrier, we restrict the value to take into account
		/// the effort required to break through the barrier.</param>
		/// <returns>true if the cursor has broken through the barrier.</returns>
		public bool BrokenThrough(ref int newValue)
		{
			bool brokenThrough = false;
			if (active)
			{
				if (newValue > limit)
				{
					if (minForce == Int32.MaxValue)
					{
						// not allowed to break through barrier
						newValue = limit;
					}
					else
					{
						totalForce += newValue - limit;
						if (totalForce > minForce)
						{
							// cursor has broken through barrier
							newValue = limit + totalForce - minForce;
							brokenThrough = true;
						}
						else
						{
							newValue = limit;
						}
					}
				}
				else
				{
					totalForce = 0;
				}
			}

			return brokenThrough;
		}
	}
}
