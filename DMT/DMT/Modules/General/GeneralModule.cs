#region copyright
// This file is part of Dual Monitor Tools which is a set of tools to assist
// users with multiple monitor setups.
// Copyright (C) 2015  Gerald Evans
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

namespace DMT.Modules.General
{
	using System;
	using System.Collections.Generic;
	using System.Drawing;
	using System.IO;
	using System.Linq;
	using System.Reflection;
	using System.Text;
	using System.Threading.Tasks;
	using System.Windows.Forms;

	using DMT.Library.Environment;
	using DMT.Library.HotKeys;
	using DMT.Library.Logging;
	using DMT.Library.Settings;
	using DMT.Resources;

	using Microsoft.Win32;
using DMT.Library.PInvoke;
	using DMT.Library.GuiUtils;

	/// <summary>
	/// The general module
	/// </summary>
	class GeneralModule : DMT.Modules.Module
	{
		const string AutoStartKeyName = "GNE_DualMonitorTools";
		const string InstalledKeyName = @"SOFTWARE\GNE\Dual Monitor Tools";
		const string Installed6432KeyName = @"SOFTWARE\WOW6432Node\GNE\Dual Monitor Tools";
		const string InstalledValueName = "installed";

		ISettingsService _settingsService;
		ILocalEnvironment _localEnvironment;
		ILogger _logger;
		AppForm _appForm;

		/// <summary>
		/// Initialises a new instance of the <see cref="GeneralModule" /> class.
		/// </summary>
		/// <param name="settingsService">The settings service</param>
		/// <param name="hotKeyService">The hotkey service</param>
		/// <param name="logger">Application logger</param>
		/// <param name="appForm">Application (hidden) window</param>
		public GeneralModule(ISettingsService settingsService, IHotKeyService hotKeyService, ILocalEnvironment localEnvironment, ILogger logger, AppForm appForm)
			: base(hotKeyService)
		{
			_settingsService = settingsService;
			_localEnvironment = localEnvironment;
			_logger = logger;
			_appForm = appForm;

			ModuleName = "General";
		}

		/// <summary>
		/// Gets or sets a value indicating whether DMT to start when windows starts
		/// </summary>
		public bool StartWhenWindowsStarts
		{
			get
			{
				return AutoStart.IsAutoStart(AutoStartKeyName);
			}

			set
			{
				if (value)
				{
					AutoStart.SetAutoStart(AutoStartKeyName);
				}
				else
				{
					AutoStart.UnsetAutoStart(AutoStartKeyName);
				}
			}
		}

		/// <summary>
		/// Gets the version number currently running
		/// </summary>
		public Version Version
		{
			get
			{
				return Assembly.GetExecutingAssembly().GetName().Version;
			}
		}

		/// <summary>
		/// Gets a value indicating whether this version was installed with an msi installer
		/// </summary>
		public bool IsMsiInstall
		{
			get
			{
				object keyValue = null;
				try
				{
					RegistryKey key = Registry.LocalMachine.OpenSubKey(InstalledKeyName);
					if (key != null)
					{
						keyValue = key.GetValue(InstalledValueName);
					}

					if (keyValue == null)
					{
						// installer is 32bit, but we could be running on a 64 bit O/S
						key = Registry.LocalMachine.OpenSubKey(Installed6432KeyName);
						if (key != null)
						{
							keyValue = key.GetValue(InstalledValueName);
						}
					}
				}
				catch (Exception)
				{
					// if we can't read the registry, assume this is a portable install.
					keyValue = null;
				}

				return keyValue != null;
			}
		}

		/// <summary>
		/// Gets a temporary path to save the msi installer to
		/// </summary>
		public string TempMsiInstallPath
		{
			get
			{
				return Path.Combine(Path.GetTempPath(), "DualMonitorTools.msi");
			}
		}

		//public List<MonitorProperties> GetAllMonitorProperties()
		//{
		//	List<MonitorProperties> allMonitorProperties = new List<MonitorProperties>();


		//	NativeMethods.EnumDisplayMonitors(IntPtr.Zero, IntPtr.Zero,
		//		delegate(IntPtr hMonitor, IntPtr hdcMonitor, ref NativeMethods.RECT lprcMonitor, IntPtr dwData)
		//		{
		//			IntPtr hdcScreen = hdcMonitor;

		//			// get details from the virtual monitor
		//			// it is assumed that if this virtual monitor maps to multiple physical monitors
		//			// then this info is the same for all physical monitors


		//			NativeMethods.MONITORINFOEX monitorInfo = new NativeMethods.MONITORINFOEX(0);
		//			NativeMethods.GetMonitorInfo(hMonitor, ref monitorInfo);

		//			bool isPrimary = (monitorInfo.dwFlags & NativeMethods.MONITORINFOF_PRIMARY) != 0;

		//			//monitorProperties.DeviceName = new String(monitorInfo.szDevice);
		//			StringBuilder sb = new StringBuilder(monitorInfo.szDevice);
		//			string deviceName = sb.ToString();
		//			deviceName.TrimEnd('\0');

		//			if (hdcScreen == IntPtr.Zero)
		//			{
		//				string s = null;
		//				hdcScreen = NativeMethods.CreateDC(s, deviceName, s, IntPtr.Zero);
		//			}
		//			int bitsPerPixel = NativeMethods.GetDeviceCaps(hdcScreen, NativeMethods.BITSPIXEL);
		//			bitsPerPixel *= NativeMethods.GetDeviceCaps(hdcScreen, NativeMethods.PLANES);

		//			uint numPhysicalMonitors = 0;
		//			NativeMethods.GetNumberOfPhysicalMonitorsFromHMONITOR(hMonitor, ref numPhysicalMonitors);
		//			NativeMethods.PHYSICAL_MONITOR[] physicalMonitors = new NativeMethods.PHYSICAL_MONITOR[numPhysicalMonitors];
		//			if (NativeMethods.GetPhysicalMonitorsFromHMONITOR(hMonitor, numPhysicalMonitors, physicalMonitors))
		//			{

		//				int monitorNumber = 1;

		//				for (int i = 0; i < numPhysicalMonitors; i++)
		//				{
		//					MonitorProperties physicalMonitorProperties = new MonitorProperties();

		//					physicalMonitorProperties.Number = monitorNumber++;
		//					physicalMonitorProperties.MonitorType = MonitorProperties.EMonitorType.Physical;

		//					IntPtr hPhysicalMonitor = physicalMonitors[i].hPhysicalMonitor;
		//					physicalMonitorProperties.Handle = (uint)hPhysicalMonitor;

		//					physicalMonitorProperties.NumPhysicalMonitors = 0;	// only applies to virtual monitors

		//					// NO WORK - get from virtual?????????
		//					NativeMethods.MONITORINFOEX physicalMonitorInfo = new NativeMethods.MONITORINFOEX(0);
		//					NativeMethods.GetMonitorInfo(hPhysicalMonitor, ref physicalMonitorInfo);
		//					physicalMonitorProperties.Bounds = ScreenHelper.RectToRectangle(ref physicalMonitorInfo.rcMonitor);
		//					physicalMonitorProperties.WorkingArea = ScreenHelper.RectToRectangle(ref physicalMonitorInfo.rcWork);
		//					physicalMonitorProperties.Primary = (physicalMonitorInfo.dwFlags & NativeMethods.MONITORINFOF_PRIMARY) != 0;
		//					//
		//					physicalMonitorProperties.Primary = isPrimary;


		//					physicalMonitorProperties.DeviceName = deviceName;

		//					sb = new StringBuilder(physicalMonitors[i].szPhysicalMonitorDescription);
		//					physicalMonitorProperties.Description = sb.ToString();

		//					physicalMonitorProperties.BitsPerPixel = bitsPerPixel;


		//					uint minBrightness;
		//					uint maxBrightness;
		//					uint curBrightness;
		//					NativeMethods.GetMonitorBrightness(hPhysicalMonitor, out minBrightness, out curBrightness, out maxBrightness);

		//					physicalMonitorProperties.MinBrightness = minBrightness;
		//					physicalMonitorProperties.MaxBrightness = maxBrightness;
		//					physicalMonitorProperties.CurBrightness = curBrightness;

		//					allMonitorProperties.Add(physicalMonitorProperties);
		//				}
		//			}

		//			// release any resources used while looking at this virtual monitor
		//			NativeMethods.DestroyPhysicalMonitors(numPhysicalMonitors, physicalMonitors);

		//			if (hdcScreen != hdcMonitor)
		//			{
		//				NativeMethods.DeleteDC(hdcScreen);
		//			}

		//			return true;
		//		}, IntPtr.Zero);


		//	return allMonitorProperties;
		//}

		public List<MonitorProperties> GetAllMonitorProperties(bool showVirtual)
		{
			List<MonitorProperties> allMonitorProperties = new List<MonitorProperties>();
			int virtualMonitorNumber = 0;
			int physicalMonitorNumber = 0;

			NativeMethods.EnumDisplayMonitors(IntPtr.Zero, IntPtr.Zero,
				delegate(IntPtr hMonitor, IntPtr hdcMonitor, ref NativeMethods.RECT lprcMonitor, IntPtr dwData)
				{
					// get details from the virtual monitor
					// it is assumed that if this virtual monitor maps to multiple physical monitors
					// then this info is the same for all physical monitors

					MonitorProperties virtualMonitorProperties = new MonitorProperties();
					virtualMonitorProperties.VirtualNumber = ++virtualMonitorNumber;
					virtualMonitorProperties.ChildNumber = 0;

					AddVirtualMonitorProperties(hMonitor, hdcMonitor, virtualMonitorProperties);

					uint numPhysicalMonitors = 0;
					NativeMethods.GetNumberOfPhysicalMonitorsFromHMONITOR(hMonitor, ref numPhysicalMonitors);
					virtualMonitorProperties.NumPhysicalMonitors = numPhysicalMonitors;

					if (showVirtual)
					{
						allMonitorProperties.Add(virtualMonitorProperties);
					}

					NativeMethods.PHYSICAL_MONITOR[] physicalMonitors = new NativeMethods.PHYSICAL_MONITOR[numPhysicalMonitors];
					if (NativeMethods.GetPhysicalMonitorsFromHMONITOR(hMonitor, numPhysicalMonitors, physicalMonitors))
					{
						for (int i = 0; i < numPhysicalMonitors; i++)
						{
							MonitorProperties physicalMonitorProperties = virtualMonitorProperties.Clone();

							physicalMonitorProperties.ChildNumber = i + 1;
							physicalMonitorProperties.PhysicalNumber = ++physicalMonitorNumber;

							AddPhysicalMonitorProperties(physicalMonitors[i], physicalMonitorProperties);

							allMonitorProperties.Add(physicalMonitorProperties);
						}
					}

					// release any resources used while looking at this virtual monitor
					NativeMethods.DestroyPhysicalMonitors(numPhysicalMonitors, physicalMonitors);

					//if (hdcScreen != hdcMonitor)
					//{
					//	NativeMethods.DeleteDC(hdcScreen);
					//}

					return true;
				}, IntPtr.Zero);


			return allMonitorProperties;
		}


		void AddVirtualMonitorProperties(IntPtr hVirtualMonitor, IntPtr hdcMonitor, MonitorProperties virtualMonitorProperties)
		{
			IntPtr hdcScreen = hdcMonitor;

			virtualMonitorProperties.MonitorType = MonitorProperties.EMonitorType.Virtual;
			virtualMonitorProperties.Handle = (uint)hVirtualMonitor;


			NativeMethods.MONITORINFOEX monitorInfo = new NativeMethods.MONITORINFOEX(0);
			NativeMethods.GetMonitorInfo(hVirtualMonitor, ref monitorInfo);

			virtualMonitorProperties.Primary = (monitorInfo.dwFlags & NativeMethods.MONITORINFOF_PRIMARY) != 0;
			virtualMonitorProperties.Bounds = ScreenHelper.RectToRectangle(ref monitorInfo.rcMonitor);
			virtualMonitorProperties.WorkingArea = ScreenHelper.RectToRectangle(ref monitorInfo.rcWork);

			//monitorProperties.DeviceName = new String(monitorInfo.szDevice);
			StringBuilder sb = new StringBuilder(monitorInfo.szDevice);
			string deviceName = sb.ToString();
			deviceName.TrimEnd('\0');
			virtualMonitorProperties.DeviceName = deviceName;

			if (hdcScreen == IntPtr.Zero)
			{
				string s = null;
				hdcScreen = NativeMethods.CreateDC(s, deviceName, s, IntPtr.Zero);
			}
			int bitsPerPixel = NativeMethods.GetDeviceCaps(hdcScreen, NativeMethods.BITSPIXEL);
			bitsPerPixel *= NativeMethods.GetDeviceCaps(hdcScreen, NativeMethods.PLANES);
			virtualMonitorProperties.BitsPerPixel = bitsPerPixel;

			if (hdcScreen != hdcMonitor)
			{
				NativeMethods.DeleteDC(hdcScreen);
			}
		}

		void AddPhysicalMonitorProperties(NativeMethods.PHYSICAL_MONITOR physicalMonitor, MonitorProperties physicalMonitorProperties)
		{
			physicalMonitorProperties.MonitorType = MonitorProperties.EMonitorType.Physical;

			IntPtr hPhysicalMonitor = physicalMonitor.hPhysicalMonitor;
			physicalMonitorProperties.Handle = (uint)hPhysicalMonitor;

			physicalMonitorProperties.NumPhysicalMonitors = 0;	// only applies to virtual monitors

			// TODO: how do we get physical monitor area ???

			StringBuilder sb = new StringBuilder(physicalMonitor.szPhysicalMonitorDescription);
			physicalMonitorProperties.Description = sb.ToString();

			uint minBrightness;
			uint maxBrightness;
			uint curBrightness;
			NativeMethods.GetMonitorBrightness(hPhysicalMonitor, out minBrightness, out curBrightness, out maxBrightness);

			physicalMonitorProperties.MinBrightness = minBrightness;
			physicalMonitorProperties.MaxBrightness = maxBrightness;
			physicalMonitorProperties.CurBrightness = curBrightness;
		}

		//MonitorProperties FindMonitor(List<MonitorProperties> allMonitorProperties, int monitor)
		//{
		//	return allMonitorProperties[monitor];
		//}

		/// <summary>
		/// Starts the module up
		/// </summary>
		public override void Start()
		{
			AddCommand("Options", GeneralStrings.OptionsDescription, string.Empty, ShowOptions, false, true);
		}

		/// <summary>
		/// Starts the process of shutting DMT down
		/// </summary>
		public void StartShutdown()
		{
			Application.Exit();
		}

		/// <summary>
		/// Gets the option nodes for this module
		/// </summary>
		/// <returns>The root node</returns>
		public override ModuleOptionNode GetOptionNodes()
		{
			Image image = new Bitmap(Properties.Resources.DMT_16_16);
			ModuleOptionNodeBranch options = new ModuleOptionNodeBranch("Dual Monitor Tools", image, new GeneralRootOptionsPanel());
			options.Nodes.Add(new ModuleOptionNodeLeaf("General", image, new GeneralOptionsPanel(this)));
			options.Nodes.Add(new ModuleOptionNodeLeaf("Monitors", image, new GeneralMonitorsOptionsPanel(this)));

			return options;
		}

		void ShowOptions()
		{
			_appForm.ShowOptions();
		}
	}
}
