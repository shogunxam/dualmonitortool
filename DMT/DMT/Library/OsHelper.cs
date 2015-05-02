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

namespace DMT.Library
{
	/// <summary>
	/// Utility class to handle O/S specific issues
	/// </summary>
	class OsHelper
	{
		/// <summary>
		/// Determine if we are running Windows 7 or later
		/// </summary>
		/// <returns>true if this is Windows 7 or later</returns>
		public static bool IsWin7OrLater()
		{
			bool isWin7orLater = false;

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
					if (osInfo.Version.Minor >= 1)
					{
						isWin7orLater = true;
					}
					// TODO: what about future versions of Windows?
				}
				else if (osInfo.Version.Major > 6)
				{
					isWin7orLater = true;
				}

			}

			return isWin7orLater;
		}
	}
}
