#region copyright
// This file is part of Dual Monitor Tools which is a set of tools to assist
// users with multiple monitor setups.
// Copyright (C) 2016-2020  Gerald Evans
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

namespace DMT.Modules.General
{
	public class DisplayDevice
	{
		// links to where this information came from
		public int PathIndex { get; private set; }
		public IntPtr MonitorHandle { get; set; }
		//public IntPtr PhysicalMonitorHandle { get; set; }


		public NativeDisplayMethods.LUID AdapterId { get; private set; }
		public uint OutletId { get; private set; }	// connector/outlet on above adapter

		public bool IsActive { get; set; }
		public bool IsPrimary { get; set; }

		//public string AdapterName { get; set; }
		public string SourceName { get; set; }
		public string FriendlyName { get; set; }
		public string DeviceName { get; set; }	// from virtual monitor
		public string Description { get; set; }	// from physical monitor

		//public int OutputTechnology { get; set; }
		//public int Rotation { get; set; }
		public string OutputTechnology { get; set; }
		public int RotationDegrees { get; set; }

		public Rectangle Bounds { get; set; }
		public Rectangle WorkingArea { get; set; }

		public int BitsPerPixel { get; set; }

		public uint MinBrightness { get; set; }
		public uint MaxBrightness { get; set; }
		public uint CurBrightness { get; set; }

		public int DeviceIndex { get; set; }
		public int ActiveDeviceIndex { get; set; }



		public DisplayDevice(int pathIndex, NativeDisplayMethods.LUID adapterId, uint outletId)
		{
			PathIndex = pathIndex;
			AdapterId = adapterId;
			OutletId = outletId;

		}
	}
}
