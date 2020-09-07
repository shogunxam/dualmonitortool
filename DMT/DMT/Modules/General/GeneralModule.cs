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
	using DMT.Library.GuiUtils;
	using DMT.Library.HotKeys;
	using DMT.Library.Logging;
	using DMT.Library.PInvoke;
	using DMT.Library.Settings;
	using DMT.Resources;

	using Microsoft.Win32;

	/// <summary>
	/// The general module
	/// </summary>
	class GeneralModule : DMT.Modules.Module
	{
		const string AutoStartKeyName = "GNE_DualMonitorTools";
		const string InstalledKeyName = @"SOFTWARE\GNE\Dual Monitor Tools";
		const string Installed6432KeyName = @"SOFTWARE\WOW6432Node\GNE\Dual Monitor Tools";
		const string InstalledValueName = "installed";

		//public enum EMonitorOrder { DotNet = 0, LeftRight = 1 };

		ISettingsService _settingsService;
		ILocalEnvironment _localEnvironment;
		ILogger _logger;
		AppForm _appForm;

		GeneralMonitorOrderingOptionsPanel _generalMonitorOrderingOptionsPanel;
		GeneralMonitorsOptionsPanel _generalMonitorsOptionsPanel;

		IntSetting MonitorOrderSetting { get; set; }

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

		public Monitor.EMonitorOrder MonitorOrder 
		{ 
			get
			{
				return (Monitor.EMonitorOrder)MonitorOrderSetting.Value;
			}
			
			set
			{
				MonitorOrderSetting.Value = (int)value;
				Monitor.MonitorOrder = value;
				//if (_generalMonitorOrderingOptionsPanel != null)
				//{
				//	_generalMonitorOrderingOptionsPanel.ShowCurrentLayout();
				//}

				// this will inform all modules that the ordering has changed
				_appForm.MonitorOrderingChanged();
			}
		}

		/// <summary>
		/// Called when the display resolution or monitor layout changes
		/// </summary>
		public override void DisplayResolutionChanged()
		{
			if (_generalMonitorOrderingOptionsPanel != null)
			{
				_generalMonitorOrderingOptionsPanel.ShowCurrentLayout();
			}

			if (_generalMonitorsOptionsPanel != null)
			{
				_generalMonitorsOptionsPanel.ShowCurrentInfo();
			}
		}

		//public bool AllowShowVirtualMonitors
		//{
		//	get
		//	{
		//		// we only need to allow the user to be able to select virtual monitors if running Vista or later
		//		return _localEnvironment.IsVistaOrLater();
		//	}
		//}

		public IList<DisplayDevice> GetAllMonitorProperties()
		{
			// TODO: any performance advantage in keeping this at the class level?
			DisplayDevices displayDevices = new DisplayDevices();

			return displayDevices.Items;
		}

		public DisplayDevices GetDisplayDevices()
		{
			DisplayDevices displayDevices = new DisplayDevices();

			return displayDevices;
		}

		public void MakePrimary(int monitorIndex)	// 0 based
		{
			DisplayDevices displayDevices = new DisplayDevices();

			displayDevices.MakePrimary(monitorIndex);
		}

		public void ChangeMonitorBrightness(int monitorIndex, uint brightness)
		{
			DisplayDevices displayDevices = new DisplayDevices();

			displayDevices.ChangeMonitorBrightness(monitorIndex, brightness);
		}

		/// <summary>
		/// Starts the module up
		/// </summary>
		public override void Start()
		{
			// hot keys / commands
			AddCommand("Options", GeneralStrings.OptionsDescription, string.Empty, ShowOptions, false, true);
			if (_localEnvironment.IsWin7OrLater())
			{
				AddCommand("ChangePrimary", GeneralStrings.ChangePrimaryDescription, string.Empty, nop, ChangePrimary);
				AddCommand("ChangeBrightness", GeneralStrings.ChangeBrightnessDescription, string.Empty, nop, ChangeBrightness);
			}

			// settings
			MonitorOrderSetting = new IntSetting(_settingsService, ModuleName, "MonitorOrder", (int)Monitor.EMonitorOrder.LeftRight);

			// set the monitor order now, so can be used by the other modules
			Monitor.MonitorOrder = (Monitor.EMonitorOrder)MonitorOrderSetting.Value;
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

			_generalMonitorOrderingOptionsPanel = new GeneralMonitorOrderingOptionsPanel(this);
			options.Nodes.Add(new ModuleOptionNodeLeaf("Monitor Order", image, _generalMonitorOrderingOptionsPanel));

			// To simplify logic, we only support Win 7 or later
			// if needed, limited support for XP/Vista could be added at a later date
			if (_localEnvironment.IsWin7OrLater())
			{
				_generalMonitorsOptionsPanel = new GeneralMonitorsOptionsPanel(this);
				options.Nodes.Add(new ModuleOptionNodeLeaf("Monitors", image, _generalMonitorsOptionsPanel));
			}

			return options;
		}

		void ShowOptions()
		{
			_appForm.ShowOptions();
		}

		void ChangePrimary(string parameters)
		{
			int monitorNum;
			if (int.TryParse(parameters, out monitorNum))
			{
				// want zero based values
				MakePrimary(monitorNum - 1);
			}
		}

		void ChangeBrightness(string parameters)
		{
			string[] fields = parameters.Split(new char[] { ':' });
			if (fields.Length >= 2)
			{
				int monitorNum;
				uint brightness;
				if (int.TryParse(fields[0], out monitorNum))
				{
					if (uint.TryParse(fields[1], out brightness))
					{
						ChangeMonitorBrightness(monitorNum - 1, brightness);
					}
				}
			}
		}
	}
}
