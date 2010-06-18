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
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace DisMon
{
	/// <summary>
	/// Singleton that allows disabling and re-eanbling of minitors
	/// 
	/// No support for Windows 7 yet.
	/// </summary>
	sealed class DisMon
	{
		static readonly DisMon instance = new DisMon();

		List<MonitorMode> allMonitors = new List<MonitorMode>();

		// Explicit static constructor to tell C# compiler
		// not to mark type as beforefieldinit
		static DisMon()
		{
		}

		DisMon()
		{
			// initialise our own list of monitors
			EnumMonitors();
		}

		public static DisMon Instance
		{
			get
			{
				return instance;
			}
		}

		private void EnumMonitors()
		{
			// build list of devices
			Screen[] allScreens = Screen.AllScreens;
			for (int screenIndex = 0; screenIndex < allScreens.Length; screenIndex++)
			{
				allMonitors.Add(new MonitorMode(allScreens[screenIndex].DeviceName));
			}
		}

		public int Count
		{
			get
			{
				return allMonitors.Count;
			}
		}

		public void MarkAsPrimary(int newPrimaryIndex)
		{
			if (newPrimaryIndex < 0 || newPrimaryIndex >= allMonitors.Count)
			{
				throw new ApplicationException(string.Format("newPrimaryIndex: {0} out of range", newPrimaryIndex));
			}

			if (allMonitors[newPrimaryIndex].Disabled)
			{
				MarkAsEnabled(newPrimaryIndex);
				// TODO chack if we really need to apply changes now
				ApplyChanges();
			}

			Point pt = allMonitors[newPrimaryIndex].NewPosition;
			foreach (MonitorMode monitorMode in allMonitors)
			{
				monitorMode.Offset(-pt.X, -pt.Y);
			}
		}

		/// <summary>
		/// Disables all secondary monitors
		/// </summary>
		/// <returns>true if one or more monitors were disabled</returns>
		public void MarkAllSecondaryAsDisabled()
		{
			for (int monitorIndex = 0; monitorIndex < allMonitors.Count; monitorIndex++)
			{
				if (!allMonitors[monitorIndex].Primary)
				{
					MarkAsDisabled(monitorIndex);
				}
			}
		}

		public bool IsDisabled(int monitorIndex)
		{
			if (monitorIndex < 0 || monitorIndex >= allMonitors.Count)
			{
				throw new ApplicationException(string.Format("monitorIndex: {0} out of range", monitorIndex));
			}

			return allMonitors[monitorIndex].Disabled;
		}

		public void MarkAsDisabled(int monitorIndex)
		{
			if (monitorIndex < 0 || monitorIndex >= allMonitors.Count)
			{
				throw new ApplicationException(string.Format("monitorIndex: {0} out of range", monitorIndex));
			}

			allMonitors[monitorIndex].MarkAsDisabled();
		}

		public void MarkAsEnabled(int enableIndex)
		{
			if (enableIndex < 0 || enableIndex >= allMonitors.Count)
			{
				throw new ApplicationException(string.Format("monitorIndex: {0} out of range", enableIndex));
			}

			// must reposition all monitors to as re-enabling a monitor
			// can cause positions to change
			for (int monitorIndex = 0; monitorIndex < allMonitors.Count; monitorIndex++)
			{
				if (monitorIndex == enableIndex)
				{
					allMonitors[enableIndex].MarkAsEnabled();
				}
				else
				{
					if (!allMonitors[enableIndex].Disabled)
					{
						allMonitors[enableIndex].RePosition();
					}
				}
			}
		}

		public bool ApplyChanges()
		{
			bool bRet = false;

			foreach (MonitorMode monitorMode in allMonitors)
			{
				if (monitorMode.ApplyChanges())
				{
					bRet = true;
				}
			}

			Win32.ChangeDisplaySettings(IntPtr.Zero, 0);

			return bRet;
		}

		/// <summary>
		/// re-enable all devices that have been disabled by MarkAllSecondaryAsDisabled()
		/// </summary>
		public void Restore()
		{
			foreach (MonitorMode monitorMode in allMonitors)
			{
				monitorMode.Restore();
			}

			Win32.ChangeDisplaySettings(IntPtr.Zero, 0);
		}

		///// <summary>
		///// Indicates if any monitors have been disabled
		///// </summary>
		//public bool MonitorsDisabled
		//{
		//    get { return disabledDevices.Capacity > 0; }
		//}
	}
}
