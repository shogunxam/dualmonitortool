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

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace DMT.Library.Environment
{
	/// <summary>
	/// Provides access to the environment
	/// </summary>
	public class LocalEnvironment : ILocalEnvironment
	{
		public Monitors Monitors
		{
			get
			{
				// this can change with time, so do not cache
				Monitors monitors = new Monitors();
				foreach (Screen screen in Screen.AllScreens)
				{
					Monitor monitor = new Monitor();
					monitor.Bounds = screen.Bounds;
					monitor.Primary = screen.Primary;
					monitors.Add(monitor);
				}
				return monitors;
			}
		}

		public bool IsWin8OrLater()
		{
			return OsHelper.IsWin8OrLater();
		}

	}
}
