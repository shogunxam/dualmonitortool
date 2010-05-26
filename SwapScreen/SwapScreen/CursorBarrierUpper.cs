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
	class CursorBarrierUpper
	{
		// could have base class for these and ctor, but not really worth it
		private bool active;
		private int limit;
		private int minForce;
		private int totalForce;

		public CursorBarrierUpper(bool active, int limit, int minForce)
		{
			this.active = active;
			this.limit = limit;
			this.minForce = minForce;
			this.totalForce = 0;
		}

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
