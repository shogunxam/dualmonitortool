using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DMT.Modules.General
{
	public static class NativeDisplayHelper
	{
		public static string OutputTechnologyToString(NativeDisplayMethods.DISPLAYCONFIG_VIDEO_OUTPUT_TECHNOLOGY outputTechnology)
		{
			switch (outputTechnology)
			{
				case NativeDisplayMethods.DISPLAYCONFIG_VIDEO_OUTPUT_TECHNOLOGY.DISPLAYCONFIG_OUTPUT_TECHNOLOGY_HD15:
					return "VGA";

				case NativeDisplayMethods.DISPLAYCONFIG_VIDEO_OUTPUT_TECHNOLOGY.DISPLAYCONFIG_OUTPUT_TECHNOLOGY_SVIDEO:
					return "SVIDEO";

				case NativeDisplayMethods.DISPLAYCONFIG_VIDEO_OUTPUT_TECHNOLOGY.DISPLAYCONFIG_OUTPUT_TECHNOLOGY_COMPOSITE_VIDEO:
					return "Composite video";

				case NativeDisplayMethods.DISPLAYCONFIG_VIDEO_OUTPUT_TECHNOLOGY.DISPLAYCONFIG_OUTPUT_TECHNOLOGY_COMPONENT_VIDEO:
					return "Component video";

				case NativeDisplayMethods.DISPLAYCONFIG_VIDEO_OUTPUT_TECHNOLOGY.DISPLAYCONFIG_OUTPUT_TECHNOLOGY_DVI:
					return "DVI";

				case NativeDisplayMethods.DISPLAYCONFIG_VIDEO_OUTPUT_TECHNOLOGY.DISPLAYCONFIG_OUTPUT_TECHNOLOGY_HDMI:
					return "HDMI";

				case NativeDisplayMethods.DISPLAYCONFIG_VIDEO_OUTPUT_TECHNOLOGY.DISPLAYCONFIG_OUTPUT_TECHNOLOGY_LVDS:
					return "LVDS";

				case NativeDisplayMethods.DISPLAYCONFIG_VIDEO_OUTPUT_TECHNOLOGY.DISPLAYCONFIG_OUTPUT_TECHNOLOGY_D_JPN:
					return "D JPN";

				case NativeDisplayMethods.DISPLAYCONFIG_VIDEO_OUTPUT_TECHNOLOGY.DISPLAYCONFIG_OUTPUT_TECHNOLOGY_SDI:
					return "SDI";

				case NativeDisplayMethods.DISPLAYCONFIG_VIDEO_OUTPUT_TECHNOLOGY.DISPLAYCONFIG_OUTPUT_TECHNOLOGY_DISPLAYPORT_EXTERNAL:
				case NativeDisplayMethods.DISPLAYCONFIG_VIDEO_OUTPUT_TECHNOLOGY.DISPLAYCONFIG_OUTPUT_TECHNOLOGY_DISPLAYPORT_EMBEDDED:
					return "DP";

				case NativeDisplayMethods.DISPLAYCONFIG_VIDEO_OUTPUT_TECHNOLOGY.DISPLAYCONFIG_OUTPUT_TECHNOLOGY_UDI_EXTERNAL:
				case NativeDisplayMethods.DISPLAYCONFIG_VIDEO_OUTPUT_TECHNOLOGY.DISPLAYCONFIG_OUTPUT_TECHNOLOGY_UDI_EMBEDDED:
					return "UDI";

				case NativeDisplayMethods.DISPLAYCONFIG_VIDEO_OUTPUT_TECHNOLOGY.DISPLAYCONFIG_OUTPUT_TECHNOLOGY_SDTVDONGLE:
					return "SDTV";

				case NativeDisplayMethods.DISPLAYCONFIG_VIDEO_OUTPUT_TECHNOLOGY.DISPLAYCONFIG_OUTPUT_TECHNOLOGY_MIRACAST:
					return "Miracast";

				default:
					return "Other/Unknown";
			}
		}

		public static int RotationToDegrees(NativeDisplayMethods.DISPLAYCONFIG_ROTATION rotation)
		{
			switch (rotation)
			{
				case NativeDisplayMethods.DISPLAYCONFIG_ROTATION.DISPLAYCONFIG_ROTATION_ROTATE90:
					return 90;

				case NativeDisplayMethods.DISPLAYCONFIG_ROTATION.DISPLAYCONFIG_ROTATION_ROTATE180:
					return 180;

				case NativeDisplayMethods.DISPLAYCONFIG_ROTATION.DISPLAYCONFIG_ROTATION_ROTATE270:
					return 270;

				// treat anything else including identity as 0
				case NativeDisplayMethods.DISPLAYCONFIG_ROTATION.DISPLAYCONFIG_ROTATION_IDENTITY:
				default:
					return 0;
			}
		}

		public static int PixelFormatToBits(NativeDisplayMethods.DISPLAYCONFIG_PIXELFORMAT pixelFormat)
		{
			switch (pixelFormat)
			{
				case NativeDisplayMethods.DISPLAYCONFIG_PIXELFORMAT.DISPLAYCONFIG_PIXELFORMAT_8BPP:
					return 8;

				case NativeDisplayMethods.DISPLAYCONFIG_PIXELFORMAT.DISPLAYCONFIG_PIXELFORMAT_16BPP:
					return 16;

				case NativeDisplayMethods.DISPLAYCONFIG_PIXELFORMAT.DISPLAYCONFIG_PIXELFORMAT_24BPP:
					return 24;

				case NativeDisplayMethods.DISPLAYCONFIG_PIXELFORMAT.DISPLAYCONFIG_PIXELFORMAT_32BPP:
					return 32;

				default:
					// it is up to the calling code to treat this as unknown
					return 0;
			}
		}
	}
}
