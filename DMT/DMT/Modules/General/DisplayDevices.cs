using DMT.Library.PInvoke;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace DMT.Modules.General
{
	public class DisplayDevices
	{
		const int MaxQueryAttempts = 3;

		List<DisplayDevice> _displayDevices;


		NativeDisplayMethods.DISPLAYCONFIG_PATH_INFO[] _originalPathInfos;
		NativeDisplayMethods.DISPLAYCONFIG_MODE_INFO[] _originalModeInfos;
		NativeDisplayMethods.DISPLAYCONFIG_PATH_INFO[] _pathInfos;
		NativeDisplayMethods.DISPLAYCONFIG_MODE_INFO[] _modeInfos;


		public DisplayDevices()
		{
			EnumMonitors();
		}


		public int Count()
		{
			//return CountMonitors(_originalModeInfos);
			return _displayDevices.Count;
		}

		// is this needed?
		public int ActiveCount()
		{
			//return CountMonitors(_modeInfos);
			return _displayDevices.Count(d => d.IsActive);
		}

		public IList<DisplayDevice> Items 
		{
			get
			{
				return GetAllDevices();
			}
		}




		// initialises the list of monitors
		bool EnumMonitors()
		{
			uint pathArraySize = 0;
			uint modeArraySize = 0;
			NativeDisplayMethods.DISPLAYCONFIG_PATH_INFO[] tempPathInfo = null;
			NativeDisplayMethods.DISPLAYCONFIG_MODE_INFO[] tempModeInfo = null;

			for (int attempt = 1; ; attempt++)
			{

				int err = NativeDisplayMethods.GetDisplayConfigBufferSizes(NativeDisplayMethods.QDC_ALL_PATHS, out pathArraySize, out modeArraySize);
				if (err != 0)
				{
					throw new ApplicationException(string.Format("GetDisplayConfigBufferSizes() error: {0}", err));
				}

				tempPathInfo = new NativeDisplayMethods.DISPLAYCONFIG_PATH_INFO[pathArraySize];
				tempModeInfo = new NativeDisplayMethods.DISPLAYCONFIG_MODE_INFO[modeArraySize];

				IntPtr pCurrentTopologyId = IntPtr.Zero;
				err = NativeDisplayMethods.QueryDisplayConfig(NativeDisplayMethods.QDC_ALL_PATHS,
					ref pathArraySize, tempPathInfo,
					ref modeArraySize, tempModeInfo,
					pCurrentTopologyId);

				if (err == 0)
				{
					break;
				}
				else if (err == NativeDisplayMethods.ERROR_INSUFFICIENT_BUFFER && attempt < MaxQueryAttempts)
				{
					// need to try again
				}
				else
				{
					throw new ApplicationException(string.Format("QueryDisplayConfig() error: {0}", err));
					// return false;
				}
			}

			// save these structures so we can restore original state when required
			// (remember QueryDisplayConfig() may have decreased pathArraySize and modeArraySize
			//  so new array lengths may be smaller than the tempPathInfo and tempModeInfo arrays)
			_originalPathInfos = new NativeDisplayMethods.DISPLAYCONFIG_PATH_INFO[pathArraySize];
			_originalModeInfos = new NativeDisplayMethods.DISPLAYCONFIG_MODE_INFO[modeArraySize];
			for (int i = 0; i < pathArraySize; i++)
			{
				_originalPathInfos[i] = tempPathInfo[i];
			}
			for (int i = 0; i < modeArraySize; i++)
			{
				_originalModeInfos[i] = tempModeInfo[i];
			}

			// copy the state to our working copy
			CopyOriginalState();

			Dump();

			return true;
		}

		List<DisplayDevice> GetAllDevices()
		{
			// rebuild list of devices we know about
			_displayDevices = new List<DisplayDevice>();

			//for (int modeIndex = 0; modeIndex < _originalModeInfos.Length; modeIndex++)
			//{
			//	if (_originalModeInfos[modeIndex].infoType == NativeDisplayMethods.DISPLAYCONFIG_MODE_INFO_TYPE_SOURCE)
			//	{
			//		DisplayDevice device = new DisplayDevice();
			//		device.DeviceIndex = nextDeviceIndex++;
			//		devices.Add(device);
			//	}
			//}

			for (int pathIndex = 0; pathIndex < _pathInfos.Length; pathIndex++)
			{
				NativeDisplayMethods.DISPLAYCONFIG_PATH_INFO pathInfo = _pathInfos[pathIndex];

				NativeDisplayMethods.LUID adapterId = pathInfo.sourceInfo.adapterId;
				uint outletId = pathInfo.sourceInfo.id;
				uint targetId = pathInfo.targetInfo.id;
				DisplayDevice displayDevice = FindDevice(adapterId, outletId);
				if (displayDevice == null)
				{
					displayDevice = new DisplayDevice(adapterId, outletId);
					_displayDevices.Add(displayDevice);
				}

				if ((pathInfo.flags & NativeDisplayMethods.DISPLAYCONFIG_PATH_ACTIVE) != 0)
				{
					if (displayDevice.IsActive)
					{
						System.Diagnostics.Debug.WriteLine("Same device {0} active in 2 paths", pathIndex);
					}

					displayDevice.IsActive = true;
					//displayDevice.OutputTechnology = pathInfo.targetInfo.outputTechnology;
					//displayDevice.Rotation = pathInfo.targetInfo.rotation;
					displayDevice.OutputTechnology = NativeDisplayHelper.OutputTechnologyToString(pathInfo.targetInfo.outputTechnology);
					displayDevice.RotationDegrees = NativeDisplayHelper.RotationToDegrees(pathInfo.targetInfo.rotation);

					NativeDisplayMethods.DISPLAYCONFIG_SOURCE_MODE sourceMode = GetSourceMode(pathInfo.sourceInfo.modeInfoIdx);
					displayDevice.Bounds = new System.Drawing.Rectangle(sourceMode.position.x, sourceMode.position.y, (int)sourceMode.width, (int)sourceMode.height);

					if (sourceMode.position.x == 0 && sourceMode.position.y == 0)
					{
						displayDevice.IsPrimary = true;
					}

					//displayDevice.AdapterName = AdapterName(adapterId, outletId);
					displayDevice.SourceName = SourceName(adapterId, outletId);
					displayDevice.FriendlyName = MonitorFriendlyName(adapterId, targetId);

					displayDevice.BitsPerPixel = NativeDisplayHelper.PixelFormatToBits(sourceMode.pixelFormat);
				}
				else
				{
					if (string.IsNullOrEmpty(displayDevice.SourceName) || displayDevice.SourceName == "Unknown")
					{
						displayDevice.SourceName = SourceName(adapterId, outletId);
					}

					if (string.IsNullOrEmpty(displayDevice.FriendlyName) || displayDevice.FriendlyName == "Unknown")
					{
						displayDevice.FriendlyName = MonitorFriendlyName(adapterId, targetId);
					}
				}
			}



			return _displayDevices;
		}

		// see http://stackoverflow.com/questions/26404982/how-get-monitors-friendly-name-with-winapi
		public static string MonitorFriendlyName(NativeDisplayMethods.LUID adapterId, uint targetId)
		{
			NativeDisplayMethods.DISPLAYCONFIG_TARGET_DEVICE_NAME deviceName = new NativeDisplayMethods.DISPLAYCONFIG_TARGET_DEVICE_NAME();
			deviceName.header.size = (uint)Marshal.SizeOf(typeof(NativeDisplayMethods.DISPLAYCONFIG_TARGET_DEVICE_NAME));
			deviceName.header.adapterId = adapterId;
			deviceName.header.id = targetId;
			deviceName.header.type = NativeDisplayMethods. DISPLAYCONFIG_DEVICE_INFO_TYPE.DISPLAYCONFIG_DEVICE_INFO_GET_TARGET_NAME;
			//deviceName.header.type = NativeDisplayMethods.DISPLAYCONFIG_DEVICE_INFO_TYPE.DISPLAYCONFIG_DEVICE_INFO_GET_ADAPTER_NAME;
			int error = NativeDisplayMethods.DisplayConfigGetDeviceInfo_Target(ref deviceName);
			if (error != NativeMethods.ERROR_SUCCESS)
			{
				return "Unknown";
			}

			return deviceName.monitorFriendlyDeviceName;
		}

		public static string SourceName(NativeDisplayMethods.LUID adapterId, uint outletId)
		{
			NativeDisplayMethods.DISPLAYCONFIG_SOURCE_DEVICE_NAME deviceName = new NativeDisplayMethods.DISPLAYCONFIG_SOURCE_DEVICE_NAME();
			deviceName.header.size = (uint)Marshal.SizeOf(typeof(NativeDisplayMethods.DISPLAYCONFIG_SOURCE_DEVICE_NAME));
			deviceName.header.adapterId = adapterId;
			deviceName.header.id = outletId;
			deviceName.header.type = NativeDisplayMethods.DISPLAYCONFIG_DEVICE_INFO_TYPE.DISPLAYCONFIG_DEVICE_INFO_GET_SOURCE_NAME;
			int error = NativeDisplayMethods.DisplayConfigGetDeviceInfo_Source(ref deviceName);
			if (error != NativeMethods.ERROR_SUCCESS)
			{
				return "Unknown";
			}

			return deviceName.viewGdiDeviceName;
		}


		//public static string AdapterName(NativeDisplayMethods.LUID adapterId, uint outletId)
		//{
		//	NativeDisplayMethods.DISPLAYCONFIG_ADAPTER_NAME deviceName = new NativeDisplayMethods.DISPLAYCONFIG_ADAPTER_NAME();
		//	deviceName.header.size = (uint)Marshal.SizeOf(typeof(NativeDisplayMethods.DISPLAYCONFIG_ADAPTER_NAME));
		//	deviceName.header.adapterId = adapterId;
		//	deviceName.header.id = outletId;
		//	deviceName.header.type = NativeDisplayMethods.DISPLAYCONFIG_DEVICE_INFO_TYPE.DISPLAYCONFIG_DEVICE_INFO_GET_ADAPTER_NAME;
		//	int error = NativeDisplayMethods.DisplayConfigGetDeviceInfo_Adapter(ref deviceName);
		//	if (error != NativeMethods.ERROR_SUCCESS)
		//	{
		//		return "Unknown";
		//	}

		//	return deviceName.adapterDevicePath;
		//}


		DisplayDevice FindDevice(NativeDisplayMethods.LUID adapterId, uint outletId)
		{
			foreach (DisplayDevice displayDevice in _displayDevices)
			{
				if (displayDevice.AdapterId.HighPart == adapterId.HighPart && displayDevice.AdapterId.LowPart == adapterId.LowPart && displayDevice.OutletId == outletId)
				{
					return displayDevice;
				}
			}

			return null;
		}

		int CountMonitors(NativeDisplayMethods.DISPLAYCONFIG_MODE_INFO[] modeInfos)
		{
			int count = 0;
			for (int modeIndex = 0; modeIndex < modeInfos.Length; modeIndex++)
			{
				if (modeInfos[modeIndex].infoType == NativeDisplayMethods.DISPLAYCONFIG_MODE_INFO_TYPE_SOURCE)
				{
					count++;
				}
			}

			return count;
		}

		NativeDisplayMethods.DISPLAYCONFIG_SOURCE_MODE GetSourceMode(uint modeInfoIdx)
		{
			System.Diagnostics.Debug.Assert(modeInfoIdx >= 0 && modeInfoIdx < _modeInfos.Length);
			return _modeInfos[modeInfoIdx].sourceMode;
		}

		void CopyOriginalState()
		{
			_pathInfos = new NativeDisplayMethods.DISPLAYCONFIG_PATH_INFO[_originalPathInfos.Length];
			_modeInfos = new NativeDisplayMethods.DISPLAYCONFIG_MODE_INFO[_originalModeInfos.Length];
			for (int i = 0; i < _originalPathInfos.Length; i++)
			{
				_pathInfos[i] = _originalPathInfos[i];
			}
			for (int i = 0; i < _originalModeInfos.Length; i++)
			{
				_modeInfos[i] = _originalModeInfos[i];
			}
		}

		#region Debug code
		 void Dump()
		{
#if DEBUG
			string line;
			for (int pathIdx = 0; pathIdx < _pathInfos.Length; pathIdx++)
			{
				line = string.Format("PATH {0} flags:{1}", pathIdx, _pathInfos[pathIdx].flags);
				System.Diagnostics.Debug.WriteLine(line);
				line = string.Format("source: aId:{0}\tid:{1}\tmodeInfoIdx:{2}\tstatus:{3}",
					_pathInfos[pathIdx].sourceInfo.adapterId,
					_pathInfos[pathIdx].sourceInfo.id,
					_pathInfos[pathIdx].sourceInfo.modeInfoIdx,
					_pathInfos[pathIdx].sourceInfo.statusFlags);
				System.Diagnostics.Debug.WriteLine(line);
				line = string.Format("target: aId:{0}\tid:{1}\tmodeInfoIdx:{2}\ttech:{3}\tavail:{4}\tstaus:{5}",
					_pathInfos[pathIdx].targetInfo.adapterId,
					_pathInfos[pathIdx].targetInfo.id,
					_pathInfos[pathIdx].targetInfo.modeInfoIdx,
					_pathInfos[pathIdx].targetInfo.outputTechnology,
					_pathInfos[pathIdx].targetInfo.targetAvailable,
					_pathInfos[pathIdx].targetInfo.statusFlags);
				System.Diagnostics.Debug.WriteLine(line);
				System.Diagnostics.Debug.WriteLine("");
			}

			for (int modeIdx = 0; modeIdx < _modeInfos.Length; modeIdx++)
			{
				line = string.Format("MODE {0} infoType:{1}", modeIdx, _modeInfos[modeIdx].infoType);
				System.Diagnostics.Debug.WriteLine(line);
				line = string.Format("id:{0} adapterId:{1}", _modeInfos[modeIdx].id, _modeInfos[modeIdx].adapterId);
				System.Diagnostics.Debug.WriteLine(line);
				if (_modeInfos[modeIdx].infoType == NativeDisplayMethods.DISPLAYCONFIG_MODE_INFO_TYPE_SOURCE)
				{
					line = string.Format("source: ({0},{1})\tfmt:{2}\t({3},{4})",
						_modeInfos[modeIdx].sourceMode.width,
						_modeInfos[modeIdx].sourceMode.height,
						_modeInfos[modeIdx].sourceMode.pixelFormat,
						_modeInfos[modeIdx].sourceMode.position.x,
						_modeInfos[modeIdx].sourceMode.position.y);
					System.Diagnostics.Debug.WriteLine(line);
				}
				else
				{
					// activeSize & totalSize
					line = string.Format("target: ({0},{1})\t({2},{3})",
						_modeInfos[modeIdx].sourceMode.padding2,
						_modeInfos[modeIdx].sourceMode.padding3,
						_modeInfos[modeIdx].sourceMode.padding4,
						_modeInfos[modeIdx].sourceMode.padding5);
					System.Diagnostics.Debug.WriteLine(line);
				}
				System.Diagnostics.Debug.WriteLine("");
			}
#endif

		}
		#endregion
	}
}
