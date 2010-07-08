using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace DisMon
{
	class DisMon7 : IDisMon
	{

		// consts/structures extracted from WinGDI.h
		private const int QDC_ALL_PATHS = 1;

		// flags for SetDisplayConfig(,,,,Flags)
		private const int SDC_USE_SUPPLIED_DISPLAY_CONFIG = 0x00000020;
		private const int SDC_APPLY = 0x00000080;
		private const int SDC_ALLOW_CHANGES = 0x00000400;

		// flags for DISPLAYCONFIG_PATH_INFO.flags
		private const uint DISPLAYCONFIG_PATH_ACTIVE = 0x00000001;

		//flags for DISPLAYCONFIG_PATH_TARGET_INFO.modeInfoIdx
		private const uint DISPLAYCONFIG_PATH_MODE_IDX_INVALID = 0xffffffff;

		// flags for DISPLAYCONFIG_MODE_INFO.infoType
		private const int DISPLAYCONFIG_MODE_INFO_TYPE_SOURCE = 1;

		// 20 bytes
		[StructLayout(LayoutKind.Sequential,Pack = 1)]
		private struct  DISPLAYCONFIG_PATH_SOURCE_INFO 
		{
			public long   adapterId;
			public uint id;
			public uint modeInfoIdx;
			public uint statusFlags;
		};

		// 8 bytes
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		private struct DISPLAYCONFIG_RATIONAL
		{
			public uint Numerator;
			public uint Denominator;
		};

		// 48 bytes
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		private struct DISPLAYCONFIG_PATH_TARGET_INFO 
		{
			public long                                  adapterId;
			public uint id;
			public uint modeInfoIdx;
			public int outputTechnology;
			public int rotation;
			public int scaling;
			public DISPLAYCONFIG_RATIONAL                refreshRate;
			public int scanLineOrdering;
			public int targetAvailable;
			public uint statusFlags;
		};

		// 72 bytes
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		private struct DISPLAYCONFIG_PATH_INFO
		{
			public DISPLAYCONFIG_PATH_SOURCE_INFO sourceInfo;
			public DISPLAYCONFIG_PATH_TARGET_INFO targetInfo;
			public uint flags;
		};

		// 8 bytes
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		private struct DISPLAYCONFIG_2DREGION 
		{
			public uint cx;
			public uint cy;
		};

		// 48 bytes
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		private struct DISPLAYCONFIG_TARGET_MODE
		{
			public ulong                          pixelRate;
			public DISPLAYCONFIG_RATIONAL          hSyncFreq;
			public DISPLAYCONFIG_RATIONAL          vSyncFreq;
			public DISPLAYCONFIG_2DREGION          activeSize;
			public DISPLAYCONFIG_2DREGION          totalSize;
			public uint                          videoStandard;
			public uint scanLineOrdering;

		};

		// 8 bytes
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		private struct POINTL
		{
			public int x;
			public int y;
		};

		// 20 bytes
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		private struct DISPLAYCONFIG_SOURCE_MODE 
		{
			public uint                    width;
			public uint                    height;
			public int pixelFormat;
			public POINTL                    position;
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
		private struct DISPLAYCONFIG_MODE_INFO 
		{
			public int infoType;
			public uint                       id;
			public long                         adapterId;
			//union 
			//{
			//    DISPLAYCONFIG_TARGET_MODE targetMode;
			//    DISPLAYCONFIG_SOURCE_MODE sourceMode;
			//}
			public DISPLAYCONFIG_SOURCE_MODE sourceMode;
		} ;


		[DllImport("user32.dll")]
		private static extern int GetDisplayConfigBufferSizes(uint flags, out uint numPathArrayElements, out uint numModeArrayElements);

		[DllImport("user32.dll")]
		private static extern int QueryDisplayConfig(uint Flags,
			//ref uint pNumPathArrayElements, IntPtr pPathInfoArray,
			ref uint pNumPathArrayElements, [In, Out] DISPLAYCONFIG_PATH_INFO[] pPathInfoArray,
			//ref uint pNumModeInfoArrayElements, IntPtr pModeInfoArray,
			ref uint pNumModeInfoArrayElements, [In, Out] DISPLAYCONFIG_MODE_INFO[] pModeInfoArray,
			IntPtr pCurrentTopologyId);

		[DllImport("user32.dll")]
		private static extern int SetDisplayConfig(
			uint numPathArrayElements, DISPLAYCONFIG_PATH_INFO[] pPathInfoArray,
			uint numModeInfoArrayElements, DISPLAYCONFIG_MODE_INFO[] pModeInfoArray,
			uint Flags);

		//uint savedPathArraySize;
		//uint savedModeArraySize;
		//IntPtr pSavedPathInfo;
		//IntPtr pSavedModeInfo;
		//byte[] savedPathInfos;
		//byte[] savedModeInfos;

		private DISPLAYCONFIG_PATH_INFO[] pathInfos;
		private DISPLAYCONFIG_MODE_INFO[] modeInfos;
		private DISPLAYCONFIG_PATH_INFO[] savedPathInfos;
		private DISPLAYCONFIG_MODE_INFO[] savedModeInfos;


		/// <summary>
		/// ctor
		/// </summary>
		public DisMon7()
		{
			// initialise our own list of monitors
			EnumMonitors();
		}

		// initialises the list of monitors
		private void EnumMonitors()
		{
			//// build list of devices
			//Screen[] allScreens = Screen.AllScreens;
			//for (int screenIndex = 0; screenIndex < allScreens.Length; screenIndex++)
			//{
			//    allMonitors.Add(new MonitorMode(allScreens[screenIndex].DeviceName));
			//}


			uint pathArraySize;
			uint modeArraySize;
			//IntPtr pPathInfo;
			//IntPtr pModeInfo;


			int err = GetDisplayConfigBufferSizes(QDC_ALL_PATHS, out pathArraySize, out modeArraySize);
			if (err != 0)
			{
				throw new ApplicationException(string.Format("GetDisplayConfigBufferSizes() error: {0}", err));
			}

			//pPathInfo = Marshal.AllocHGlobal((int)pathArraySize * Marshal.SizeOf(typeof(DISPLAYCONFIG_PATH_INFO)));
			//pModeInfo = Marshal.AllocHGlobal((int)modeArraySize * Marshal.SizeOf(typeof(DISPLAYCONFIG_MODE_INFO)));

			DISPLAYCONFIG_PATH_INFO[] tempPathInfo = new DISPLAYCONFIG_PATH_INFO[pathArraySize];
			DISPLAYCONFIG_MODE_INFO[] tempModeInfo = new DISPLAYCONFIG_MODE_INFO[modeArraySize];

			IntPtr pCurrentTopologyId = IntPtr.Zero;
			err = QueryDisplayConfig(QDC_ALL_PATHS,
				//ref pathArraySize, pPathInfo,
				ref pathArraySize, tempPathInfo,
				//ref modeArraySize, pModeInfo,
				ref modeArraySize, tempModeInfo,
				pCurrentTopologyId);
			if (err != 0)
			{
				//Marshal.FreeHGlobal(pModeInfo);
				//Marshal.FreeHGlobal(pPathInfo);
				throw new ApplicationException(string.Format("QueryDisplayConfig() error: {0}", err));
			}

			// make copy so we can restore at end
			//savedPathArraySize = pathArraySize;
			//savedModeArraySize = modeArraySize;
			////pSavedPathInfo = Marshal.AllocHGlobal((int)savedPathArraySize * Marshal.SizeOf(typeof(DISPLAYCONFIG_PATH_INFO)));
			////pSavedModeInfo = Marshal.AllocHGlobal((int)savedModeArraySize * Marshal.SizeOf(typeof(DISPLAYCONFIG_MODE_INFO)));
			//savedPathInfos = new byte[savedPathArraySize];
			//savedModeInfos = new byte[savedModeArraySize];
			//Marshal.Copy(pPathInfo, savedPathInfos, 0, (int)savedPathArraySize);
			//Marshal.Copy(pModeInfo, savedModeInfos, 0, (int)savedModeArraySize);

			// take 2 copies of the structures
			// 1 so that we can restore everything at the end
			// 2 as a working copy (with the correct length as pathArraySize and modeArraySize
			// may have been reduced by QueryDisplayConfig())
			pathInfos = new DISPLAYCONFIG_PATH_INFO[pathArraySize];
			modeInfos = new DISPLAYCONFIG_MODE_INFO[modeArraySize];
			savedPathInfos = new DISPLAYCONFIG_PATH_INFO[pathArraySize];
			savedModeInfos = new DISPLAYCONFIG_MODE_INFO[modeArraySize];
			for (int i = 0; i < pathArraySize; i++)
			{
				pathInfos[i] = tempPathInfo[i];
				savedPathInfos[i] = tempPathInfo[i];
			}
			for (int i = 0; i < modeArraySize; i++)
			{
				modeInfos[i] = tempModeInfo[i];
				savedModeInfos[i] = tempModeInfo[i];
			}

			Dump();
		}

		/// <summary>
		/// The number of monitors we know about.
		/// </summary>
		public int Count()
		{
			//return allMonitors.Count;
			return 0;
		}

		/// <summary>
		/// Revert monitors back to starting condition,
		/// but don't apply the changes
		/// </summary>
		public void Revert()
		{
			// restore the working copy of the path/modes back to
			// their original values
			for (int i = 0; i < savedPathInfos.Length; i++)
			{
				pathInfos[i] = savedPathInfos[i];
			}
			for (int i = 0; i < savedModeInfos.Length; i++)
			{
				modeInfos[i] = savedModeInfos[i];
			}
		}

		/// <summary>
		/// Mark the specified monitor as being the primary monitor.
		/// </summary>
		/// <param name="newPrimaryIndex">Zero based index of monitor.</param>
		public void MarkAsPrimary(int newPrimaryIndex)
		{
			int newPrimaryModeIndex = ScreenIndexToSourceModeIndex(newPrimaryIndex);
			if (newPrimaryModeIndex < 0)
			{
				throw new ApplicationException(string.Format("MarkAsPrimary: can't find newPrimaryIndex {0}", newPrimaryIndex));
			}

			// get current position of new screen to be primary
			POINTL position = modeInfos[newPrimaryModeIndex].sourceMode.position;

			// and subtract it's position from all screens
			for (int modeIndex = 0; modeIndex < modeInfos.Length; modeIndex++)
			{
				if (modeInfos[modeIndex].infoType == DISPLAYCONFIG_MODE_INFO_TYPE_SOURCE)
				{
					modeInfos[modeIndex].sourceMode.position.x -= position.x;
					modeInfos[modeIndex].sourceMode.position.y -= position.y;
				}
			}
		}

		/// <summary>
		/// Disables all secondary monitors
		/// </summary>
		/// <returns>true if one or more monitors were disabled</returns>
		public void MarkAllSecondaryAsDisabled()
		{
			for (int pathIdx = 0; pathIdx < pathInfos.Length; pathIdx++)
			{
				if (IsSecondaryModeIndex(pathInfos[pathIdx].sourceInfo.modeInfoIdx))
				{
					if ((pathInfos[pathIdx].flags & DISPLAYCONFIG_PATH_ACTIVE) != 0)
					{
						pathInfos[pathIdx].flags &= ~DISPLAYCONFIG_PATH_ACTIVE;
						pathInfos[pathIdx].targetInfo.modeInfoIdx = DISPLAYCONFIG_PATH_MODE_IDX_INVALID;
					}
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
			//if (monitorIndex < 0 || monitorIndex >= allMonitors.Count)
			//{
			//    throw new ApplicationException(string.Format("monitorIndex: {0} out of range", monitorIndex));
			//}

			//return allMonitors[monitorIndex].Disabled;
			return false;
		}

		/// <summary>
		/// Mark the specified monitor as disabled.
		/// </summary>
		/// <param name="monitorIndex">Zero based index of monitor.</param>
		public void MarkAsDisabled(int monitorIndex)
		{
			int disabledModeIndex = ScreenIndexToSourceModeIndex(monitorIndex);
			if (disabledModeIndex < 0)
			{
				throw new ApplicationException(string.Format("MarkAsDisabled: can't find monitorIndex {0}", monitorIndex));
			}

			for (int pathIdx = 0; pathIdx < pathInfos.Length; pathIdx++)
			{
				if (pathInfos[pathIdx].sourceInfo.modeInfoIdx == disabledModeIndex)
				{
					if ((pathInfos[pathIdx].flags & DISPLAYCONFIG_PATH_ACTIVE) != 0)
					{
						pathInfos[pathIdx].flags &= ~DISPLAYCONFIG_PATH_ACTIVE;
						pathInfos[pathIdx].targetInfo.modeInfoIdx = DISPLAYCONFIG_PATH_MODE_IDX_INVALID;
					}
				}
			}
		}

		/// <summary>
		/// Mark the specified monitor as enabled.
		/// </summary>
		/// <param name="enableIndex">Zero based index of monitor.</param>
		public void MarkAsEnabled(int enableIndex)
		{
			// not needed as we use Revert() to restore all screens to their starting state
		}

		/// <summary>
		/// Updates all of the monitors with any pending changes.
		/// </summary>
		/// <returns>true if any change has been made to any monitor.</returns>
		public bool ApplyChanges()
		{
			bool changesMade = false;

			int err = SetDisplayConfig(
				(uint)pathInfos.Length, pathInfos,
				(uint)modeInfos.Length, modeInfos,
				SDC_APPLY | SDC_ALLOW_CHANGES | SDC_USE_SUPPLIED_DISPLAY_CONFIG);
			if (err == 0)
			{
				changesMade = true;
			}
			else
			{
				throw new ApplicationException(string.Format("SetDisplayConfig() error: {0}", err));
			}

			return changesMade;
		}

		/// <summary>
		/// Restore all monitors to the state that they were in at construction.
		/// </summary>
		public void Restore()
		{
			int err;

			err = SetDisplayConfig(
				(uint)savedPathInfos.Length, savedPathInfos,
				(uint)savedModeInfos.Length, savedModeInfos,
				SDC_APPLY | SDC_ALLOW_CHANGES | SDC_USE_SUPPLIED_DISPLAY_CONFIG);
			if (err != 0)
			{
				throw new ApplicationException(string.Format("SetDisplayConfig() error: {0}", err));
			}
		}

		#region Helper functions

		private bool IsSecondaryModeIndex(uint modeIdx)
		{
			bool ret = false;

			if (modeIdx != DISPLAYCONFIG_PATH_MODE_IDX_INVALID)
			{
				if (modeIdx >= 0 && modeIdx < modeInfos.Length)
				{
					if (modeInfos[modeIdx].infoType == DISPLAYCONFIG_MODE_INFO_TYPE_SOURCE)
					{
						if (modeInfos[modeIdx].sourceMode.position.x != 0 || modeInfos[modeIdx].sourceMode.position.y != 0)
						{
							ret = true;
						}
					}
				}
			}
			return ret;
		}

		// Documentation is unclear on how to associate a mode structure
		// with it's  screen number (as used in 'Change resolution' dialog).
		// So we assume here that the source mode structures are in the same 
		// order as the screen numbers, but I have not seen anything to confirm
		// that this is always the case.
		private int ScreenIndexToSourceModeIndex(int screenIndex)
		{
			int sourceModeIndex = -1;
			int curScreenIndex = 0;

			for (int modeIndex = 0; modeIndex < modeInfos.Length; modeIndex++)
			{
				if (modeInfos[modeIndex].infoType == DISPLAYCONFIG_MODE_INFO_TYPE_SOURCE)
				{
					if (curScreenIndex == screenIndex)
					{
						sourceModeIndex = modeIndex;
						break;
					}
					curScreenIndex++;
				}
			}

			return sourceModeIndex;
		}

		#endregion

		#region Debug code

		private void Dump()
		{
			string line;
			for (int pathIdx = 0; pathIdx < pathInfos.Length; pathIdx++)
			{
				line = string.Format("PATH {0} flags:{1}", pathIdx, pathInfos[pathIdx].flags);
				Console.WriteLine(line);
				line = string.Format("source: {0}\t{1}\t{2}\t{3}",
					pathInfos[pathIdx].sourceInfo.adapterId,
					pathInfos[pathIdx].sourceInfo.id,
					pathInfos[pathIdx].sourceInfo.modeInfoIdx,
					pathInfos[pathIdx].sourceInfo.statusFlags);
				Console.WriteLine(line);
				line = string.Format("target: {0}\t{1}\t{2}\t{3}\t{4}\t{5}",
					pathInfos[pathIdx].targetInfo.adapterId,
					pathInfos[pathIdx].targetInfo.id,
					pathInfos[pathIdx].targetInfo.modeInfoIdx,
					pathInfos[pathIdx].targetInfo.outputTechnology,
					pathInfos[pathIdx].targetInfo.targetAvailable,
					pathInfos[pathIdx].targetInfo.statusFlags);
				Console.WriteLine(line);
				Console.WriteLine("");
			}

			for (int modeIdx = 0; modeIdx < modeInfos.Length; modeIdx++)
			{
				line = string.Format("MODE {0} infoType:{1}", modeIdx, modeInfos[modeIdx].infoType);
				Console.WriteLine(line);
				line = string.Format("id:{0} adapterId:{1}", modeInfos[modeIdx].id, modeInfos[modeIdx].adapterId);
				Console.WriteLine(line);
				if (modeInfos[modeIdx].infoType == DISPLAYCONFIG_MODE_INFO_TYPE_SOURCE)
				{
					line = string.Format("source: ({0},{1})\t{2}\t({3},{4})",
						modeInfos[modeIdx].sourceMode.width,
						modeInfos[modeIdx].sourceMode.height,
						modeInfos[modeIdx].sourceMode.pixelFormat,
						modeInfos[modeIdx].sourceMode.position.x,
						modeInfos[modeIdx].sourceMode.position.y);
					Console.WriteLine(line);
				}
				else
				{
					// activeSize & totalSize
					line = string.Format("target: ({0},{1})\t({2},{3}",
						modeInfos[modeIdx].sourceMode.padding2,
						modeInfos[modeIdx].sourceMode.padding3,
						modeInfos[modeIdx].sourceMode.padding4,
						modeInfos[modeIdx].sourceMode.padding5);
					Console.WriteLine(line);
				}
			}

		}

		#endregion
	}
}
