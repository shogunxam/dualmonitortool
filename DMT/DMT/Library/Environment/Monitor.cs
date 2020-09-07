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
		public enum EMonitorOrder { DotNet = 1, DisplayName = 2, LeftRight = 3 };

		Screen _screen;

		static EMonitorOrder _monitorOrder = EMonitorOrder.DotNet;	// TODO
		public static EMonitorOrder MonitorOrder
		{
			get
			{
				return _monitorOrder;
			}
			set
			{
				_monitorOrder = value;
				ReBuild();
			}
		}

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
					ReBuild();
				}

				return _allMonitors;
			}
		}

		public static void ReBuild()
		{
			switch (_monitorOrder)
			{

				case EMonitorOrder.DotNet: // treat as default case
					BuildDotNetOrder();
					break;

				case EMonitorOrder.LeftRight:  // treat as default case
				default:
					BuildLeftRightOrder();
					break;
			}
		}

		static void BuildDotNetOrder()
		{
			Screen[] allScreens = Screen.AllScreens;

			// set up Monitor[] -> Screen[] map
			// direct mapping in this case
			int[] toScreenIdx = new int[allScreens.Length];
			for (int n = 0; n < allScreens.Length; n++)
			{
				toScreenIdx[n] = n;
			}

			BuildNewOrder(toScreenIdx, allScreens);
		}

		static void BuildLeftRightOrder()
		{
			Screen[] allScreens = Screen.AllScreens;

			// set up Monitor[] -> Screen[] map
			// start with direct mapping
			int[] toScreenIdx = new int[allScreens.Length];
			for (int n = 0; n < allScreens.Length; n++)
			{
				toScreenIdx[n] = n;
			}

			// now sort into left-right, top-down order
			Array.Sort(toScreenIdx, new LeftRightComparer(allScreens));

			BuildNewOrder(toScreenIdx, allScreens);
		}

		static void BuildNewOrder(int[] toScreenIdx, Screen[] allScreens)
		{

			// and add the monitors in this order
			Monitors allMonitors = new Monitors(allScreens.Length);
			for (int n = 0; n < allScreens.Length; n++)
			{
				Screen screen = allScreens[toScreenIdx[n]];
				allMonitors.Add(new Monitor(screen));
			}

			// TODO: don't think we need to do this as an atomic operation?
			_toScreenIdx = toScreenIdx;
			_allMonitors = allMonitors;
		}

		public class LeftRightComparer : System.Collections.IComparer
		{
			Screen[] _allScreens;
			public LeftRightComparer(Screen[] allScreens)
			{
				_allScreens = allScreens;
			}

			public int Compare(Object a, Object b)
			{
				Screen screenA = ObjectToScreen(a);
				Screen screenB = ObjectToScreen(b);

				if (screenA.Bounds.Left < screenB.Bounds.Left)
				{
					return -1;
				}
				else if (screenA.Bounds.Left > screenB.Bounds.Left)
				{
					return 1;
				}
				else
				{
					// use top down if lefts are the same
					if (screenA.Bounds.Top < screenB.Bounds.Top)
					{
						return -1;
					}
					else if (screenA.Bounds.Top > screenB.Bounds.Top)
					{
						return 1;
					}
				}

				// shouldn't get here
				return 0;
			}

			Screen ObjectToScreen(Object o)
			{
				int idx = (int)o;
				if (idx < 0 || idx >= _allScreens.Length)
				{
					throw new ApplicationException(string.Format("LeftRightComparer.ObjectToScreen({0}) - invalid", idx));
				}

				return _allScreens[idx];
			}
		}
	}
}
