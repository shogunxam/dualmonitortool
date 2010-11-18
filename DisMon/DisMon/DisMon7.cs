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
using System.Runtime.InteropServices;
using System.Text;

namespace DisMon
{
	/// <summary>
	/// Implemetation of IDisMon for Windows 7
	/// 
	/// BUG: There is a bug in here, where disabling the primary monitor
	/// causes problems.
	/// Eg. (P=Primary, S=Secondary, X=Disabled)
	/// In a 2 monitor system starting with the primary on the left i.e. PS
	/// Change to XP - ok (but this step seems to be the key to the problem)
	/// Now change to PX - ok
	/// Change back to PS - stays with PX
	/// Even exiting so that the initial paths and modes are restored leaves it at PX.
	/// Only solution is to into the graphics card control center and extend primary
	/// onto secondary.
	/// </summary>
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

		private DISPLAYCONFIG_PATH_INFO[] originalPathInfos;
		private DISPLAYCONFIG_MODE_INFO[] originalModeInfos;
		private DISPLAYCONFIG_PATH_INFO[] pathInfos;
		private DISPLAYCONFIG_MODE_INFO[] modeInfos;


		/// <summary>
		/// ctor
		/// </summary>
		public DisMon7()
		{
			// initialise our own list of monitors
			EnumMonitors();
		}

		/// <summary>
		/// Resets the monitors back to their original state
		/// and resets our working copy of the structures.
		/// </summary>
		public void Reset()
		{
			// restore any changed monitors
			//Restore();

			// rebuild list of monitors
			// (should be identical each time we call, but jic)
			//EnumMonitors();
			CopyOriginalState();
		}

		// initialises the list of monitors
		private void EnumMonitors()
		{
			uint pathArraySize;
			uint modeArraySize;

			int err = GetDisplayConfigBufferSizes(QDC_ALL_PATHS, out pathArraySize, out modeArraySize);
			if (err != 0)
			{
				throw new ApplicationException(string.Format("GetDisplayConfigBufferSizes() error: {0}", err));
			}

			DISPLAYCONFIG_PATH_INFO[] tempPathInfo = new DISPLAYCONFIG_PATH_INFO[pathArraySize];
			DISPLAYCONFIG_MODE_INFO[] tempModeInfo = new DISPLAYCONFIG_MODE_INFO[modeArraySize];

			IntPtr pCurrentTopologyId = IntPtr.Zero;
			err = QueryDisplayConfig(QDC_ALL_PATHS,
				ref pathArraySize, tempPathInfo,
				ref modeArraySize, tempModeInfo,
				pCurrentTopologyId);
			if (err != 0)
			{
				throw new ApplicationException(string.Format("QueryDisplayConfig() error: {0}", err));
			}

			// save these structures so we can restore original state when required
			// (remember QueryDisplayConfig() may have decreased pathArraySize and modeArraySize
			//  so new array lengths may be smaller than the tempPathInfo and tempModeInfo arrays)
			originalPathInfos = new DISPLAYCONFIG_PATH_INFO[pathArraySize];
			originalModeInfos = new DISPLAYCONFIG_MODE_INFO[modeArraySize];
			for (int i = 0; i < pathArraySize; i++)
			{
				originalPathInfos[i] = tempPathInfo[i];
			}
			for (int i = 0; i < modeArraySize; i++)
			{
				originalModeInfos[i] = tempModeInfo[i];
			}

			// copy the state to our working copy
			CopyOriginalState();

			Dump();
		}

		private void CopyOriginalState()
		{
			pathInfos = new DISPLAYCONFIG_PATH_INFO[originalPathInfos.Length];
			modeInfos = new DISPLAYCONFIG_MODE_INFO[originalModeInfos.Length];
			for (int i = 0; i < originalPathInfos.Length; i++)
			{
				pathInfos[i] = originalPathInfos[i];
			}
			for (int i = 0; i < originalModeInfos.Length; i++)
			{
				modeInfos[i] = originalModeInfos[i];
			}
		}

		/// <summary>
		/// The number of monitors we know about.
		/// </summary>
		public int Count()
		{
			int count = 0;
			for (int modeIndex = 0; modeIndex < modeInfos.Length; modeIndex++)
			{
				if (modeInfos[modeIndex].infoType == DISPLAYCONFIG_MODE_INFO_TYPE_SOURCE)
				{
					count++;
				}
			}

			return count;
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
				(uint)originalPathInfos.Length, originalPathInfos,
				(uint)originalModeInfos.Length, originalModeInfos,
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
#if DEBUG
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
					line = string.Format("target: ({0},{1})\t({2},{3})",
						modeInfos[modeIdx].sourceMode.padding2,
						modeInfos[modeIdx].sourceMode.padding3,
						modeInfos[modeIdx].sourceMode.padding4,
						modeInfos[modeIdx].sourceMode.padding5);
					Console.WriteLine(line);
				}
				Console.WriteLine("");
			}
#endif

		}

		#endregion
	}
}
