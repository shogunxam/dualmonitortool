#region copyright
// This file is part of Dual Monitor Tools which is a set of tools to assist
// users with multiple monitor setups.
// Copyright (C) 2015  Gerald Evans
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

namespace DMT.Library.Utils
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;

	/// <summary>
	/// Helper class to generate random numbers
	/// </summary>
	public static class RNG
	{
		static Random _random = new Random();

		static RNG()
		{
			_random = new Random();
		}

		/// <summary>
		/// Gets a random number between 0 and given value
		/// </summary>
		/// <param name="maxValue">max value</param>
		/// <returns>Random number</returns>
		public static int Next(int maxValue)
		{
			return Next(0, maxValue);
		}

		/// <summary>
		/// Gets a random number between given values
		/// </summary>
		/// <param name="minValue">Min value</param>
		/// <param name="maxValue">Max value</param>
		/// <returns>Random number</returns>
		public static int Next(int minValue, int maxValue)
		{
			int r = _random.Next(minValue, maxValue);

#if DEBUG
			DMT.Library.Logging.Logger logger = new Logging.Logger();
			logger.LogInfo("RNG", "[{0}, {1}) -> {2}", minValue, maxValue, r);
#endif
			return r;
		}
	}
}
