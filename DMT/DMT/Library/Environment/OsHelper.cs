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

namespace DMT.Library.Environment
{
	using System;
	using System.Collections.Generic;
	using System.Text;

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
		const int VistaMajor = 6;
		const int VistaMinor = 0;

		const int Win7Major = 6;
		const int Win7Minor = 1;

		const int Win8Major = 6;
		const int Win8Minor = 2;

		const int Win10Major = 10;
		const int Win10Minor = 0;

		/// <summary>
		/// Determine if we are running Windows Vista (or later)
		/// </summary>
		/// <returns>true if this is Windows Vista or later</returns>
		public static bool IsVistaOrLater()
		{
			return IsLaterThan(VistaMajor, VistaMinor);
		}

		/// <summary>
		/// Determine if we are running Windows 7 or later
		/// </summary>
		/// <returns>true if this is Windows 7 or later</returns>
		public static bool IsWin7OrLater()
		{
			return IsLaterThan(Win7Major, Win7Minor);
		}

		/// <summary>
		/// Determine if we are running Windows 8 (or later)
		/// </summary>
		/// <returns>true if this is Windows 8 or later</returns>
		public static bool IsWin8OrLater()
		{
			return IsLaterThan(Win8Major, Win8Minor);
		}

		/// <summary>
		/// Determine if we are running Windows 10 (or later)
		/// </summary>
		/// <returns>true if this is Windows 10 or later</returns>
		public static bool IsWin10OrLater()
		{
			return IsLaterThan(Win10Major, Win10Minor);
		}

		static bool IsLaterThan(int major, int minor)
		{
			bool isLater = false;

			System.OperatingSystem operatingSystemInfo = System.Environment.OSVersion;

			if (operatingSystemInfo.Platform == PlatformID.Win32NT)
			{
				if (operatingSystemInfo.Version.Major == major)
				{
					if (operatingSystemInfo.Version.Minor >= minor)
					{
						isLater = true;
					}
				}
				else if (operatingSystemInfo.Version.Major > major)
				{
					isLater = true;
				}
			}

			return isLater;
		}
	}
}
