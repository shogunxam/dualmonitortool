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

namespace DisMon
{
	class MonitorMode
	{
		private string deviceName;
		private Win32.DEVMODE oldDeviceMode = new Win32.DEVMODE();
		private Win32.DEVMODE newDeviceMode = new Win32.DEVMODE();
		private Point position;
		private bool changesPending;
		private bool disabled;

		public MonitorMode(string deviceName)
		{
			this.deviceName = deviceName;

			oldDeviceMode.dmSize = (ushort)Marshal.SizeOf(oldDeviceMode);

			Win32.EnumDisplaySettings(deviceName, Win32.ENUM_REGISTRY_SETTINGS, ref oldDeviceMode);
			Win32.EnumDisplaySettings(deviceName, Win32.ENUM_CURRENT_SETTINGS, ref oldDeviceMode);

			position = new Point(oldDeviceMode.dmPositionX, oldDeviceMode.dmPositionY);

			newDeviceMode = oldDeviceMode;
			changesPending = false;
			disabled = false;	// assume always initially enabled
		}

		public bool Primary
		{
			get 
			{
				return (newDeviceMode.dmPositionX == 0 && newDeviceMode.dmPositionY == 0); 
			}
		}

		public void Disable()
		{
			// clear the fields
			newDeviceMode = new Win32.DEVMODE();
			newDeviceMode.dmSize = (ushort)Marshal.SizeOf(newDeviceMode);
			newDeviceMode.dmFields = Win32.DM_POSITION | Win32.DM_PELSWIDTH | Win32.DM_PELSHEIGHT
				| Win32.DM_BITSPERPEL | Win32.DM_DISPLAYFREQUENCY | Win32.DM_DISPLAYFLAGS;
			changesPending = true;
			disabled = true;
		}

		public bool Disabled
		{
			get
			{
				return disabled;
			}
		}

		public void Enable()
		{
			// copy the fields back from the saved structure
			newDeviceMode = oldDeviceMode;
			// what about position?
			newDeviceMode.dmPositionX = position.X;
			newDeviceMode.dmPositionY = position.Y;
			changesPending = true;
			disabled = false;
		}

		public void RePosition()
		{
			if (!disabled)
			{
				if (newDeviceMode.dmPositionX != position.X || newDeviceMode.dmPositionY != position.Y)
				{
					newDeviceMode.dmPositionX = position.X;
					newDeviceMode.dmPositionY = position.Y;
					changesPending = true;
				}
			}
		}

		public Point NewPosition
		{
			get
			{
				return new Point(newDeviceMode.dmPositionX, newDeviceMode.dmPositionY);
			}
		}

		public void Offset(int deltaX, int deltaY)
		{
			position.X += deltaX;
			position.Y += deltaY;

			RePosition();
		}

		//public bool Changed
		//{
		//    get
		//    {
		//        return (changesPending);
		//    }
		//}

		public bool ApplyChanges()
		{
			bool ret = false;
			if (changesPending)
			{
				int change;
				change = Win32.ChangeDisplaySettingsEx(deviceName, ref newDeviceMode, IntPtr.Zero, Win32.CDS_UPDATEREGISTRY, IntPtr.Zero);
				if (change == Win32.DISP_CHANGE_SUCCESSFUL)
				{
//					change = Win32.ChangeDisplaySettingsEx(deviceName, ref newDeviceMode, IntPtr.Zero, Win32.CDS_UPDATEREGISTRY, IntPtr.Zero);
					changesPending = false;
					ret = true;
				}
			}

			return ret;
		}

		public void Restore()
		{
			int change = Win32.ChangeDisplaySettingsEx(deviceName, ref oldDeviceMode, IntPtr.Zero, Win32.CDS_UPDATEREGISTRY, IntPtr.Zero);
			if (change == Win32.DISP_CHANGE_SUCCESSFUL)
			{
				// indicate back to the original device mode
				newDeviceMode = oldDeviceMode;
			}
		}
	}
}
