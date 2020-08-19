#region copyright
// This file is part of Dual Monitor Tools which is a set of tools to assist
// users with multiple monitor setups.
// Copyright (C) 2015-2020  Gerald Evans
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
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DMT.Library.Environment
{
	/// <summary>
	/// Replacement of System.Windows.Forms.Screen
	/// Mainly so we can control our own ordering of the screens
	/// </summary>
	public class Monitor
	{
		public enum EOrderType { DotNet = 1, DisplayName = 2, LeftRight = 3 };

		Screen _screen;

		/// <summary>
		/// Gets or sets the bounds for the monitor
		/// </summary>
		public Rectangle Bounds
		{
			get
			{
				return _screen.Bounds;
			}
		}

		/// <summary>
		/// Gets or sets the working area for the monitor
		/// </summary>
		public Rectangle WorkingArea 
		{
			get
			{
				return _screen.WorkingArea;
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether this is the primary monitor
		/// </summary>
		public bool Primary
		{
			get
			{
				return _screen.Primary;
			}
		}

		/// <summary>
		/// To simplify replacement of Screen
		/// May remove this in time?
		/// </summary>
		public Screen Screen
		{
			get
			{
				return _screen;
			}
		}


		public Monitor(Screen screen)
		{
			_screen = screen;
		}


		static Monitors _allMonitors = null;
		static int[] _toScreenIdx;

		public static Monitors AllMonitors
		{
			get
			{
				if (_allMonitors == null)
				{
					// TODO: want to throw?
					ReBuild(EOrderType.DotNet);
				}

				return _allMonitors;
			}
		}

		public static void ReBuild(EOrderType orderType)
		{
			switch (orderType)
			{
				case EOrderType.DotNet: // treat as default case
				default:
					BuildDotNetOrder();
					break;
			}
		}

		static void BuildDotNetOrder()
		{
			Screen[] allScreens = Screen.AllScreens;

			// set up Monitor[] -> Screen[] map
			// direct mapping in this case
			_toScreenIdx = new int[allScreens.Length];
			for (int n = 0; n < allScreens.Length; n++)
			{
				_toScreenIdx[n] = n;
			}

			_allMonitors = new Monitors(allScreens.Length);
			for (int n = 0; n < allScreens.Length; n++)
			{
				Screen screen = allScreens[n];
				_allMonitors.Add(new Monitor(screen));
			}
		}
	}
}
