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
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DMT.Modules.General
{
	class GeneralModule : Module
	{
		const string _autoStartKeyName = "GNE_DualMonitorTools";

		ISettingsService _settingsService;
		ILogger _logger;
		AppForm _appForm;

		public bool StartWhenWindowsStarts
		{
			get
			{
				return AutoStart.IsAutoStart(_autoStartKeyName);
			}
			set
			{
				if (value)
				{
					AutoStart.SetAutoStart(_autoStartKeyName);
				}
				else
				{
					AutoStart.UnsetAutoStart(_autoStartKeyName);
				}
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
