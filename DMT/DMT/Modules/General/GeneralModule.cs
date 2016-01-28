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

using DMT.Library.Environment;
using DMT.Library.HotKeys;
using DMT.Library.Logging;
using DMT.Library.Settings;
using DMT.Resources;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DMT.Modules.General
{
	class GeneralModule : Module
	{
		const string AutoStartKeyName = "GNE_DualMonitorTools";
		const string InstalledKeyName = @"SOFTWARE\GNE\Dual Monitor Tools";
		const string Installed6432KeyName = @"SOFTWARE\WOW6432Node\GNE\Dual Monitor Tools";
		const string InstalledValueName = "installed";

		ISettingsService _settingsService;
		ILogger _logger;
		AppForm _appForm;

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

		public Version Version
		{
			get
			{
				return Assembly.GetExecutingAssembly().GetName().Version;
			}
		}

		public bool IsMsiInstall
		{
			get
			{
				object keyValue = null;
				try
				{
					keyValue = Registry.LocalMachine.GetValue(InstalledKeyName, InstalledValueName);
					if (keyValue == null)
					{
						// installer is 32bit, but we could be running on a 64 bit O/S
						keyValue = Registry.LocalMachine.GetValue(Installed6432KeyName, InstalledValueName);
					}
				}
				catch (Exception)
				{
					// if we can't read the registry, assume this is a portable install.
					keyValue = null;
				}
				return (keyValue != null);
			}
		}

		public string TempMsiInstallPath
		{
			get
			{
				return Path.Combine(Path.GetTempPath(), "DualMonitorTools.msi");
			}
		}

		public GeneralModule(ISettingsService settingsService, IHotKeyService hotKeyService, ILogger logger, AppForm appForm)
			: base(hotKeyService)
		{
			_settingsService = settingsService;
			_logger = logger;
			_appForm = appForm;

			ModuleName = "General";
		}

		public override void Start()
		{
			AddCommand("Options", GeneralStrings.OptionsDescription, "", ShowOptions, false, true);
		}

		public void StartShutdown()
		{
			//_appForm.Close();
			Application.Exit();
		}

		public override ModuleOptionNode GetOptionNodes(/*Form form*/)
		{
			Image image = new Bitmap(Properties.Resources.DMT_16_16);
			ModuleOptionNodeBranch options = new ModuleOptionNodeBranch("Dual Monitor Tools", image, new GeneralRootOptionsPanel());
			options.Nodes.Add(new ModuleOptionNodeLeaf("General", image, new GeneralOptionsPanel(this)));

			return options;
		}

		void ShowOptions()
		{
			_appForm.ShowOptions();
		}

	}
}
