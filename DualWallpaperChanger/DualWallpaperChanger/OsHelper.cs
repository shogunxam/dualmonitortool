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

namespace DualMonitorTools.DualWallpaperChanger
{
	/// <summary>
	/// Utility class to handle O/S specific issues
	/// </summary>
	class OsHelper
	{
		/// <summary>
		/// Determine if we are running Windows Vista (or later)
		/// </summary>
		/// <returns>true if this is Windows Vista or later</returns>
		public static bool IsVistaOrLater()
		{
			bool isVistaOrLater = false;

			System.OperatingSystem osInfo = System.Environment.OSVersion;

			if (osInfo.Platform == PlatformID.Win32NT)
			{
				// 5.0 => 2000
				// 5.1 => XP
				// 5.2 => 2003
				// 6.0 => Vista / 2008
				// 6.1 => Win 7 / 2008 R2 / 2011
				// 6.2 => Win 8 / 2012
				if (osInfo.Version.Major >= 6)
				{
					isVistaOrLater = true;
				}
			}

			return isVistaOrLater;
		}

		/// <summary>
		/// Determine if we are running Windows 8 (or later)
		/// </summary>
		/// <returns>true if this is Windows 8 or later</returns>
		public static bool IsWin8OrLater()
		{
			bool isWin8OrLater = false;

			System.OperatingSystem osInfo = System.Environment.OSVersion;

			if (osInfo.Platform == PlatformID.Win32NT)
			{
				// 5.0 => 2000
				// 5.1 => XP
				// 5.2 => 2003
				// 6.0 => Vista / 2008
				// 6.1 => Win 7 / 2008 R2 / 2011
				// 6.2 => Win 8 / 2012
				if (osInfo.Version.Major == 6)
				{
					if (osInfo.Version.Minor >= 2)
					{
						isWin8OrLater = true;
					}
				}
				else if (osInfo.Version.Major > 6)
				{
					isWin8OrLater = true;
				}
			}

			return isWin8OrLater;
		}
	}
}
