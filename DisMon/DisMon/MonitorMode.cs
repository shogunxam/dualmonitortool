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
	/// Represents a single monitor with the ability to be enabled/disabled
	/// and for it's virtual position within the virtual desktop to be changed
	/// which also can indicate if it is the primary monitor or not.
	/// </summary>
	class MonitorMode
	{
		private string deviceName;
		// This is the initial mode of the monitor, 
		// so it can be restored later on
		private Win32.DEVMODE oldDeviceMode = new Win32.DEVMODE();
		// This is the new mode that will be used when changes are applied
		private Win32.DEVMODE newDeviceMode = new Win32.DEVMODE();
		// TLHC position within the virtual desktop if all monitors
		// were enabled.
		private Point virtualPosition;
		private bool changesPending;
		// extra flags for dwFlags param of ChangeDisplaySettingsEx()
		private uint extraFlags;
		private bool disabled;

		/// <summary>
		/// Constructs the MonitorMode.
		/// </summary>
		/// <param name="deviceName">Device name for monitor.</param>
		public MonitorMode(string deviceName)
		{
			this.deviceName = deviceName;

			oldDeviceMode.dmSize = (ushort)Marshal.SizeOf(oldDeviceMode);

			Win32.EnumDisplaySettings(deviceName, Win32.ENUM_REGISTRY_SETTINGS, ref oldDeviceMode);
			Win32.EnumDisplaySettings(deviceName, Win32.ENUM_CURRENT_SETTINGS, ref oldDeviceMode);

			virtualPosition = new Point(oldDeviceMode.dmPositionX, oldDeviceMode.dmPositionY);

			newDeviceMode = oldDeviceMode;
			changesPending = false;
			disabled = false;	// assume always initially enabled
		}

		/// <summary>
		/// Read only value indicating if this will be the primary monitor
		/// after any pending changes have been made.
		/// </summary>
		public bool Primary
		{
			get 
			{
				//return (newDeviceMode.dmPositionX == 0 && newDeviceMode.dmPositionY == 0);
				return (virtualPosition.X == 0 && virtualPosition.Y == 0); 
			}
		}

		/// <summary>
		/// Read only value indicating the virtual position of the monitor (if all monitors
		/// were enabled) after any pending changes have been made.
		/// </summary>
		public Point NewPosition
		{
			get
			{
				return virtualPosition;
			}
		}

		/// <summary>
		/// Read only value indicating if this monitor will be disabled
		/// after any pending changes have been made.
		/// </summary>
		public bool Disabled
		{
			get
			{
				return disabled;
			}
		}

		/// <summary>
		/// Marks the monitor ready to be disabled if it is not already disabled.
		/// Call ApplyChanges() to implement the change.
		/// </summary>
		public void MarkAsDisabled()
		{
			if (!disabled)
			{
				// clear the fields
				newDeviceMode = new Win32.DEVMODE();
				newDeviceMode.dmSize = (ushort)Marshal.SizeOf(newDeviceMode);
				newDeviceMode.dmFields = Win32.DM_POSITION | Win32.DM_PELSWIDTH | Win32.DM_PELSHEIGHT
					| Win32.DM_BITSPERPEL | Win32.DM_DISPLAYFREQUENCY | Win32.DM_DISPLAYFLAGS;
				changesPending = true;
				disabled = true;
			}
		}

		/// <summary>
		/// Marks the monitor ready to be enabled if it is not already enabled.
		/// Call ApplyChanges() to implement the change.
		/// </summary>
		public void MarkAsEnabled()
		{
			if (disabled)
			{
				// copy the fields back from the saved structure
				newDeviceMode = oldDeviceMode;
				// what about virtualPosition?
				newDeviceMode.dmPositionX = virtualPosition.X;
				newDeviceMode.dmPositionY = virtualPosition.Y;
				changesPending = true;
				disabled = false;
			}
		}

		/// <summary>
		/// Marks this monitor as being the primary monitor
		/// </summary>
		public void MarkAsPrimary()
		{
			extraFlags |= Win32.CDS_SET_PRIMARY;
			virtualPosition.X  = 0;
			virtualPosition.Y  = 0;
			RePosition();
		}

		/// <summary>
		/// Marks this monitor as being a secondary monitor.
		/// This will be called on all monitors apart from the monitor
		/// that is becoming the primary monitor when the primary monitor is changed.
		/// 
		/// This also involves adjusting its position by the amount specified to
		/// handle it's position relative to the new primary monitor.
		/// </summary>
		/// <param name="deltaX">Amount to increase the X virtual position by.</param>
		/// <param name="deltaY">Amount to increase the Y virtual position by.</param>
		public void MarkAsSecondary(int deltaX, int deltaY)
		{
			extraFlags &= ~Win32.CDS_SET_PRIMARY;	// make sure this is not set
			virtualPosition.X += deltaX;
			virtualPosition.Y += deltaY;
			RePosition();
		}

		/// <summary>
		/// Refresh the virtual position of the monitor from the stored virtual virtual position.
		/// </summary>
		public void RePosition()
		{
			if (!disabled)
			{
				if (newDeviceMode.dmPositionX != virtualPosition.X || newDeviceMode.dmPositionY != virtualPosition.Y)
				{
					newDeviceMode.dmPositionX = virtualPosition.X;
					newDeviceMode.dmPositionY = virtualPosition.Y;
					changesPending = true;
				}
			}
		}

		/// <summary>
		/// Applies all outstanding changes to the monitor.
		/// </summary>
		/// <returns>true if the changes have been successully made.</returns>
		public bool ApplyChanges()
		{
			bool ret = false;
			if (changesPending)
			{
				int change;
				uint dwFlags = Win32.CDS_UPDATEREGISTRY | Win32.CDS_NORESET | extraFlags;
				change = Win32.ChangeDisplaySettingsEx(deviceName, ref newDeviceMode, IntPtr.Zero, dwFlags, IntPtr.Zero);
				if (change == Win32.DISP_CHANGE_SUCCESSFUL)
				{
					changesPending = false;
					extraFlags = 0;
					ret = true;
				}
				else
				{
					string errorMsg = string.Format("Change failed with: {0}", change);
					MessageBox.Show(errorMsg, Program.MyTitle);
				}
			}

			return ret;
		}

		/// <summary>
		/// Restores the monitor to the state it was in when constructed.
		/// </summary>
		public void Restore()
		{
			uint dwFlags = Win32.CDS_UPDATEREGISTRY | Win32.CDS_NORESET;
			int change = Win32.ChangeDisplaySettingsEx(deviceName, ref oldDeviceMode, IntPtr.Zero, dwFlags, IntPtr.Zero);
			if (change == Win32.DISP_CHANGE_SUCCESSFUL)
			{
				// indicate back to the original device mode
				newDeviceMode = oldDeviceMode;
			}
		}
	}
}
