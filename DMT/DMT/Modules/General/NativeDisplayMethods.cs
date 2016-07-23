using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace DMT.Modules.General
{
	/// <summary>
	/// Native methods used by DisplayDevices
	/// 
	/// TODO: When complete, this needs moving/merging into PInvoke/NativeMethods
	/// </summary>
	public static class NativeDisplayMethods
	{
		// consts/structures extracted from WinGDI.h
		public const int QDC_ALL_PATHS = 1;

		// flags for SetDisplayConfig(,,,,Flags)
		public const int SDC_USE_SUPPLIED_DISPLAY_CONFIG = 0x00000020;
		public const int SDC_APPLY = 0x00000080;
		public const int SDC_ALLOW_CHANGES = 0x00000400;

		// flags for DISPLAYCONFIG_PATH_INFO.flags
		public const uint DISPLAYCONFIG_PATH_ACTIVE = 0x00000001;

		//flags for DISPLAYCONFIG_PATH_TARGET_INFO.modeInfoIdx
		public const uint DISPLAYCONFIG_PATH_MODE_IDX_INVALID = 0xffffffff;

		// flags for DISPLAYCONFIG_MODE_INFO.infoType
		public const int DISPLAYCONFIG_MODE_INFO_TYPE_SOURCE = 1;


		public const int ERROR_INSUFFICIENT_BUFFER = 122;


		public enum DISPLAYCONFIG_DEVICE_INFO_TYPE : uint
		{
			DISPLAYCONFIG_DEVICE_INFO_GET_SOURCE_NAME = 1,
			DISPLAYCONFIG_DEVICE_INFO_GET_TARGET_NAME = 2,
			DISPLAYCONFIG_DEVICE_INFO_GET_TARGET_PREFERRED_MODE = 3,
			DISPLAYCONFIG_DEVICE_INFO_GET_ADAPTER_NAME = 4,
			DISPLAYCONFIG_DEVICE_INFO_SET_TARGET_PERSISTENCE = 5,
			DISPLAYCONFIG_DEVICE_INFO_GET_TARGET_BASE_TYPE = 6,
			DISPLAYCONFIG_DEVICE_INFO_FORCE_UINT32 = 0xFFFFFFFF
		}

		public enum DISPLAYCONFIG_VIDEO_OUTPUT_TECHNOLOGY : uint
		{
			DISPLAYCONFIG_OUTPUT_TECHNOLOGY_OTHER = 0xFFFFFFFF,
			DISPLAYCONFIG_OUTPUT_TECHNOLOGY_HD15 = 0,
			DISPLAYCONFIG_OUTPUT_TECHNOLOGY_SVIDEO = 1,
			DISPLAYCONFIG_OUTPUT_TECHNOLOGY_COMPOSITE_VIDEO = 2,
			DISPLAYCONFIG_OUTPUT_TECHNOLOGY_COMPONENT_VIDEO = 3,
			DISPLAYCONFIG_OUTPUT_TECHNOLOGY_DVI = 4,
			DISPLAYCONFIG_OUTPUT_TECHNOLOGY_HDMI = 5,
			DISPLAYCONFIG_OUTPUT_TECHNOLOGY_LVDS = 6,
			DISPLAYCONFIG_OUTPUT_TECHNOLOGY_D_JPN = 8,
			DISPLAYCONFIG_OUTPUT_TECHNOLOGY_SDI = 9,
			DISPLAYCONFIG_OUTPUT_TECHNOLOGY_DISPLAYPORT_EXTERNAL = 10,
			DISPLAYCONFIG_OUTPUT_TECHNOLOGY_DISPLAYPORT_EMBEDDED = 11,
			DISPLAYCONFIG_OUTPUT_TECHNOLOGY_UDI_EXTERNAL = 12,
			DISPLAYCONFIG_OUTPUT_TECHNOLOGY_UDI_EMBEDDED = 13,
			DISPLAYCONFIG_OUTPUT_TECHNOLOGY_SDTVDONGLE = 14,
			DISPLAYCONFIG_OUTPUT_TECHNOLOGY_MIRACAST = 15,
			DISPLAYCONFIG_OUTPUT_TECHNOLOGY_INTERNAL = 0x80000000,
			DISPLAYCONFIG_OUTPUT_TECHNOLOGY_FORCE_UINT32 = 0xFFFFFFFF
		}

		public enum DISPLAYCONFIG_PIXELFORMAT : uint
		{
			DISPLAYCONFIG_PIXELFORMAT_8BPP = 1,
			DISPLAYCONFIG_PIXELFORMAT_16BPP = 2,
			DISPLAYCONFIG_PIXELFORMAT_24BPP = 3,
			DISPLAYCONFIG_PIXELFORMAT_32BPP = 4,
			DISPLAYCONFIG_PIXELFORMAT_NONGDI = 5,
			DISPLAYCONFIG_PIXELFORMAT_FORCE_UINT32 = 0xffffffff
		}

		public enum DISPLAYCONFIG_ROTATION : uint
		{
			DISPLAYCONFIG_ROTATION_IDENTITY = 1,
			DISPLAYCONFIG_ROTATION_ROTATE90 = 2,
			DISPLAYCONFIG_ROTATION_ROTATE180 = 3,
			DISPLAYCONFIG_ROTATION_ROTATE270 = 4,
			DISPLAYCONFIG_ROTATION_FORCE_UINT32 = 0xFFFFFFFF
		}


		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct LUID
		{
			public uint LowPart;
			public int HighPart;
		};


		// 20 bytes
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct DISPLAYCONFIG_PATH_SOURCE_INFO
		{
			public LUID adapterId;
			public uint id;
			public uint modeInfoIdx;
			public uint statusFlags;
		};

		// 8 bytes
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct DISPLAYCONFIG_RATIONAL
		{
			public uint Numerator;
			public uint Denominator;
		};

		// 48 bytes
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct DISPLAYCONFIG_PATH_TARGET_INFO
		{
			public LUID adapterId;
			public uint id;
			public uint modeInfoIdx;
			public DISPLAYCONFIG_VIDEO_OUTPUT_TECHNOLOGY outputTechnology;
			public DISPLAYCONFIG_ROTATION rotation;
			public int scaling;
			public DISPLAYCONFIG_RATIONAL refreshRate;
			public int scanLineOrdering;
			public int targetAvailable;
			public uint statusFlags;
		};

		// 72 bytes
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct DISPLAYCONFIG_PATH_INFO
		{
			public DISPLAYCONFIG_PATH_SOURCE_INFO sourceInfo;
			public DISPLAYCONFIG_PATH_TARGET_INFO targetInfo;
			public uint flags;
		};

		// 8 bytes
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct DISPLAYCONFIG_2DREGION
		{
			public uint cx;
			public uint cy;
		};

		// 48 bytes
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct DISPLAYCONFIG_TARGET_MODE
		{
			public ulong pixelRate;
			public DISPLAYCONFIG_RATIONAL hSyncFreq;
			public DISPLAYCONFIG_RATIONAL vSyncFreq;
			public DISPLAYCONFIG_2DREGION activeSize;
			public DISPLAYCONFIG_2DREGION totalSize;
			public uint videoStandard;
			public uint scanLineOrdering;

		};

		// 8 bytes
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct POINTL
		{
			public int x;
			public int y;
		};

		// 20 bytes
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct DISPLAYCONFIG_SOURCE_MODE
		{
			public uint width;
			public uint height;
			public DISPLAYCONFIG_PIXELFORMAT pixelFormat;
			public POINTL position;
			// padding to make same length as DISPLAYCONFIG_TARGET_MODE
			public uint padding1;
			public uint padding2;
			public uint padding3;
			public uint padding4;
			public uint padding5;
			public uint padding6;
			public uint padding7;

		};

		// 64 bytes
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct DISPLAYCONFIG_MODE_INFO
		{
			public int infoType;
			public uint id;
			public LUID adapterId;
			//union 
			//{
			//    DISPLAYCONFIG_TARGET_MODE targetMode;
			//    DISPLAYCONFIG_SOURCE_MODE sourceMode;
			//}
			public DISPLAYCONFIG_SOURCE_MODE sourceMode;
		};

		[StructLayout(LayoutKind.Sequential)]
		public struct DISPLAYCONFIG_DEVICE_INFO_HEADER
		{
			public DISPLAYCONFIG_DEVICE_INFO_TYPE type;
			public uint size;
			public LUID adapterId;
			public uint id;
		}


		[StructLayout(LayoutKind.Sequential)]
		public struct DISPLAYCONFIG_TARGET_DEVICE_NAME_FLAGS
		{
			public uint value;
		}


		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		public struct DISPLAYCONFIG_SOURCE_DEVICE_NAME
		{
			public DISPLAYCONFIG_DEVICE_INFO_HEADER header;
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
			public string viewGdiDeviceName;
		}

		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		public struct DISPLAYCONFIG_TARGET_DEVICE_NAME
		{
			public DISPLAYCONFIG_DEVICE_INFO_HEADER header;
			public DISPLAYCONFIG_TARGET_DEVICE_NAME_FLAGS flags;
			public DISPLAYCONFIG_VIDEO_OUTPUT_TECHNOLOGY outputTechnology;
			public ushort edidManufactureId;
			public ushort edidProductCodeId;
			public uint connectorInstance;
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
			public string monitorFriendlyDeviceName;
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
			public string monitorDevicePath;
		}

		//[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		//public struct DISPLAYCONFIG_ADAPTER_NAME
		//{
		//	public DISPLAYCONFIG_DEVICE_INFO_HEADER header;
		//	[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
		//	public string adapterDevicePath;
		//}


		[DllImport("user32.dll", EntryPoint = "DisplayConfigGetDeviceInfo")]
		public static extern int DisplayConfigGetDeviceInfo_Target(
			ref DISPLAYCONFIG_TARGET_DEVICE_NAME deviceName);

		[DllImport("user32.dll", EntryPoint="DisplayConfigGetDeviceInfo")]
		public static extern int DisplayConfigGetDeviceInfo_Source(
			ref DISPLAYCONFIG_SOURCE_DEVICE_NAME deviceName);

		//[DllImport("user32.dll", EntryPoint = "DisplayConfigGetDeviceInfo")]
		//public static extern int DisplayConfigGetDeviceInfo_Adapter(
		//	ref DISPLAYCONFIG_ADAPTER_NAME deviceName);

		[DllImport("user32.dll")]
		public static extern int GetDisplayConfigBufferSizes(uint flags, out uint numPathArrayElements, out uint numModeArrayElements);

		[DllImport("user32.dll")]
		public static extern int QueryDisplayConfig(uint Flags,
			//ref uint pNumPathArrayElements, IntPtr pPathInfoArray,
			ref uint pNumPathArrayElements, [In, Out] DISPLAYCONFIG_PATH_INFO[] pPathInfoArray,
			//ref uint pNumModeInfoArrayElements, IntPtr pModeInfoArray,
			ref uint pNumModeInfoArrayElements, [In, Out] DISPLAYCONFIG_MODE_INFO[] pModeInfoArray,
			IntPtr pCurrentTopologyId);

		[DllImport("user32.dll")]
		public static extern int SetDisplayConfig(
			uint numPathArrayElements, DISPLAYCONFIG_PATH_INFO[] pPathInfoArray,
			uint numModeInfoArrayElements, DISPLAYCONFIG_MODE_INFO[] pModeInfoArray,
			uint Flags);
	}
}
