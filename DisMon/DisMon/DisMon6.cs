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
using System.Text;
using System.Windows.Forms;

namespace DisMon
{
	/// <summary>
	/// Implementation of IDisMon for Windows Vista and earlier O/Ss
	/// </summary>
	class DisMon6 : IDisMon
	{
		List<MonitorMode> allMonitors;

		/// <summary>
		/// ctor
		/// </summary>
		public DisMon6()
		{
			// initialise our own list of monitors
			EnumMonitors();
		}

		public void Reset()
		{
			// restore any changed monitors
			Restore();

			// rebuild list of monitors
			// (should be identical each time we call, but jic)
			EnumMonitors();
		}

		// initialises the list of monitors
		private void EnumMonitors()
		{
			// build list of devices
			allMonitors = new List<MonitorMode>();
			Screen[] allScreens = Screen.AllScreens;
			for (int screenIndex = 0; screenIndex < allScreens.Length; screenIndex++)
			{
				allMonitors.Add(new MonitorMode(allScreens[screenIndex].DeviceName));
			}
		}

		/// <summary>
		/// The number of monitors we know about.
		/// </summary>
		public int Count()
		{
			return allMonitors.Count;
		}

		///// <summary>
		///// Revert monitors back to starting condition,
		///// but don't apply the changes
		///// </summary>
		//public void Revert()
		//{
		//}

		/// <summary>
		/// Mark the specified monitor as being the primary monitor.
		/// 
		/// WARNING: if this monitor is currently disabled, then it will be
		/// re-enabled with immediate effect.
		/// </summary>
		/// <param name="newPrimaryIndex">Zero based index of monitor.</param>
		public void MarkAsPrimary(int newPrimaryIndex)
		{
			if (newPrimaryIndex < 0 || newPrimaryIndex >= allMonitors.Count)
			{
				throw new ApplicationException(string.Format("newPrimaryIndex: {0} out of range", newPrimaryIndex));
			}

//            if (allMonitors[newPrimaryIndex].Disabled)
//            {
//                MarkAsEnabled(newPrimaryIndex);
//                // TODO check if we really need to apply changes now
////				ApplyChanges();
//            }

			Point pt = allMonitors[newPrimaryIndex].NewPosition;
			for (int monitorIndex = 0; monitorIndex < allMonitors.Count; monitorIndex++)
			{
				if (monitorIndex == newPrimaryIndex)
				{
					allMonitors[monitorIndex].MarkAsPrimary();
				}
				else
				{
					allMonitors[monitorIndex].MarkAsSecondary(-pt.X, -pt.Y);
				}
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

		/// <summary>
		/// Indicates if the monitor will be disabled after any pending changes have been made.
		/// </summary>
		/// <param name="monitorIndex">Zero based index of monitor.</param>
		/// <returns>true if the monitor is (or will be) disabled.</returns>
		public bool IsDisabled(int monitorIndex)
		{
			if (monitorIndex < 0 || monitorIndex >= allMonitors.Count)
			{
				throw new ApplicationException(string.Format("monitorIndex: {0} out of range", monitorIndex));
			}

			return allMonitors[monitorIndex].Disabled;
		}

		/// <summary>
		/// Mark the specified monitor as disabled.
		/// </summary>
		/// <param name="monitorIndex">Zero based index of monitor.</param>
		public void MarkAsDisabled(int monitorIndex)
		{
			if (monitorIndex < 0 || monitorIndex >= allMonitors.Count)
			{
				throw new ApplicationException(string.Format("monitorIndex: {0} out of range", monitorIndex));
			}

			allMonitors[monitorIndex].MarkAsDisabled();
		}

		///// <summary>
		///// Mark the specified monitor as enabled.
		///// </summary>
		///// <param name="enableIndex">Zero based index of monitor.</param>
		//public void MarkAsEnabled(int enableIndex)
		//{
		//    if (enableIndex < 0 || enableIndex >= allMonitors.Count)
		//    {
		//        throw new ApplicationException(string.Format("monitorIndex: {0} out of range", enableIndex));
		//    }

		//    // must reposition all monitors as re-enabling a monitor
		//    // can cause positions to change
		//    for (int monitorIndex = 0; monitorIndex < allMonitors.Count; monitorIndex++)
		//    {
		//        if (monitorIndex == enableIndex)
		//        {
		//            allMonitors[enableIndex].MarkAsEnabled();
		//        }
		//        else
		//        {
		//            if (!allMonitors[enableIndex].Disabled)
		//            {
		//                allMonitors[enableIndex].RePosition();
		//            }
		//        }
		//    }
		//}

		/// <summary>
		/// Updates all of the monitors with any pending changes.
		/// </summary>
		/// <returns>true if any change has been made to any monitor.</returns>
		public bool ApplyChanges()
		{
			bool changesMade = false;

			foreach (MonitorMode monitorMode in allMonitors)
			{
				if (monitorMode.ApplyChanges())
				{
					changesMade = true;
				}
			}

			if (changesMade)
			{
				Win32.ChangeDisplaySettings(IntPtr.Zero, 0);
			}

			return changesMade;
		}

		/// <summary>
		/// Restore all monitors to the state that they were in at construction.
		/// </summary>
		public void Restore()
		{
			foreach (MonitorMode monitorMode in allMonitors)
			{
				monitorMode.Restore();
			}

			Win32.ChangeDisplaySettings(IntPtr.Zero, 0);
		}
	}
}
