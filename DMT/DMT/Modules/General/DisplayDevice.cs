using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace DMT.Modules.General
{
	public class DisplayDevice
	{
		public NativeDisplayMethods.LUID AdapterId { get; private set; }
		public uint OutletId { get; private set; }	// connector/outlet on above adapter

		public bool IsActive { get; set; }
		public bool IsPrimary { get; set; }

		//public string AdapterName { get; set; }
		public string SourceName { get; set; }
		public string FriendlyName { get; set; }

		//public int OutputTechnology { get; set; }
		//public int Rotation { get; set; }
		public string OutputTechnology { get; set; }
		public int RotationDegrees { get; set; }

		public Rectangle Bounds { get; set; }

		public int BitsPerPixel { get; set; }


		public int DeviceIndex { get; set; }
		public int ActiveDeviceIndex { get; set; }



		public DisplayDevice(NativeDisplayMethods.LUID adapterId, uint outletId)
		{
			AdapterId = adapterId;
			OutletId = outletId;

		}
	}
}
