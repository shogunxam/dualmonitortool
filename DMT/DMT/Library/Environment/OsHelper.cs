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

namespace DMT.Library.Environment
{
	/// <summary>
	/// Utility class to handle O/S specific issues
	/// </summary>
	class OsHelper
	{
		//  5.0 => 2000
		//  5.1 => XP
		//  5.2 => 2003
		//  6.0 => Vista / 2008
		//  6.1 => Win 7 / 2008 R2 / 2011
		//  6.2 => Win 8 / 2012
		//  6.3 => Win 8.1
		//  6.4 => Win 10 Technical Preview build 9841
		// 10.0 => Win 10
		const int _vistaMajor = 6;
		const int _vistaMinor = 0;

		const int _win7Major = 6;
		const int _win7Minor = 1;

		const int _win8Major = 6;
		const int _win8Minor = 2;

		/// <summary>
		/// Determine if we are running Windows Vista (or later)
		/// </summary>
		/// <returns>true if this is Windows Vista or later</returns>
		public static bool IsVistaOrLater()
		{
			return IsLaterThan(_vistaMajor, _vistaMinor);
		}

		/// <summary>
		/// Determine if we are running Windows 7 or later
		/// </summary>
		/// <returns>true if this is Windows 7 or later</returns>
		public static bool IsWin7OrLater()
		{
			return IsLaterThan(_win7Major, _win7Minor);
		}

		/// <summary>
		/// Determine if we are running Windows 8 (or later)
		/// </summary>
		/// <returns>true if this is Windows 8 or later</returns>
		public static bool IsWin8OrLater()
		{
			return IsLaterThan(_win8Major, _win8Minor);
		}

		static bool IsLaterThan(int major, int minor)
		{
			bool isLater = false;

			System.OperatingSystem osInfo = System.Environment.OSVersion;

			if (osInfo.Platform == PlatformID.Win32NT)
			{
				if (osInfo.Version.Major == major)
				{
					if (osInfo.Version.Minor >= minor)
					{
						isLater = true;
					}
				}
				else if (osInfo.Version.Major > major)
				{
					isLater = true;
				}
			}

			return isLater;

		}
	}
}
