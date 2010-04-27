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

		List<MonitorMode> disabledDevices = new List<MonitorMode>();

		// Explicit static constructor to tell C# compiler
		// not to mark type as beforefieldinit
		static DisMon()
		{
		}

		DisMon()
		{
		}

		public static DisMon Instance
		{
			get
			{
				return instance;
			}
		}

		public bool DisableAllSecondary()
		{
			bool ret = true;

			Screen[] allScreens = Screen.AllScreens;

			for (int screenIndex = 0; screenIndex < allScreens.Length; screenIndex++)
			{
				if (!allScreens[screenIndex].Primary)
				{
					Disable(allScreens[screenIndex]);
				}
			}

			return ret;
		}

		bool Disable(Screen screen)
		{
			bool ret = false;

			Win32.DEVMODE defaultMode = new Win32.DEVMODE();
			defaultMode.dmSize = (ushort)Marshal.SizeOf(defaultMode);

			Win32.EnumDisplaySettings(screen.DeviceName, Win32.ENUM_REGISTRY_SETTINGS, ref defaultMode);
			Win32.EnumDisplaySettings(screen.DeviceName, Win32.ENUM_CURRENT_SETTINGS, ref defaultMode);

			Win32.DEVMODE dm = new Win32.DEVMODE();
			dm.dmSize = (ushort)Marshal.SizeOf(dm);
			dm.dmFields = Win32.DM_POSITION | Win32.DM_PELSWIDTH | Win32.DM_PELSHEIGHT
				| Win32.DM_BITSPERPEL | Win32.DM_DISPLAYFREQUENCY | Win32.DM_DISPLAYFLAGS;
			int change;
			change = Win32.ChangeDisplaySettingsEx(screen.DeviceName, ref dm, IntPtr.Zero, Win32.CDS_UPDATEREGISTRY, IntPtr.Zero);
			if (change == Win32.DISP_CHANGE_SUCCESSFUL)
			{
				change = Win32.ChangeDisplaySettingsEx(screen.DeviceName, ref dm, IntPtr.Zero, Win32.CDS_UPDATEREGISTRY, IntPtr.Zero);

				MonitorMode disabledMonitor = new MonitorMode(screen.DeviceName, defaultMode);
				disabledDevices.Add(disabledMonitor);
				ret = true;
			}

			return ret;
		}

		/// <summary>
		/// re-enable all devices that have been disabled by DisableAllSecondary()
		/// </summary>
		public void ReEnable()
		{
			//foreach (Win32.DEVMODE dm in disabledDevices)
			//for (int i = 0; i < disabledDevices.Count; i++)
			foreach (MonitorMode monitor in disabledDevices)
			{
				Win32.DEVMODE dm = monitor.DeviceMode;
				int change = Win32.ChangeDisplaySettingsEx(monitor.DeviceName, ref dm, IntPtr.Zero, Win32.CDS_UPDATEREGISTRY, IntPtr.Zero);
			}

			Win32.ChangeDisplaySettings(IntPtr.Zero, 0);

			disabledDevices = new List<MonitorMode>();
		}

		/// <summary>
		/// Indicates if any monitors have been disabled
		/// </summary>
		public bool MonitorsDisabled
		{
			get { return disabledDevices.Capacity > 0; }
		}
	}
}
