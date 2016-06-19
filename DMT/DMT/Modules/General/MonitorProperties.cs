using DMT.Library.Environment;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace DMT.Modules.General
{
	public class MonitorProperties
	{
		[Flags]
		public enum EMonitorType
		{
			None = 0,
			Virtual = 1,
			Physical = 2
		}

		public int Number { get; set; }

		public EMonitorType MonitorType { get; set; }

		public uint Handle { get; set; }

		public uint NumPhysicalMonitors { get; set; }

		/// <summary>
		/// Gets or sets the bounds for the monitor
		/// </summary>
		public Rectangle Bounds { get; set; }

		/// <summary>
		/// Gets or sets the working area for the monitor
		/// </summary>
		public Rectangle WorkingArea { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this is the primary monitor
		/// </summary>
		public bool Primary { get; set; }

		public string DeviceName { get; set; }
		public int BitsPerPixel { get; set; }

		public uint MinBrightness { get; set; }
		public uint MaxBrightness { get; set; }
		public uint CurBrightness { get; set; }

		public MonitorProperties()
		{
		}

		public MonitorProperties(Monitor monitor)
		{
			// just copy for now
			Bounds = monitor.Bounds;
			WorkingArea = monitor.WorkingArea;
			Primary = monitor.Primary;
		}
	}
}
