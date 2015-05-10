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

using DMT.Library.HotKeys;
using DMT.Library.Logging;
using DMT.Library.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DMT.Modules.General
{
	class GeneralModule : Module
	{
		const string _moduleName = "General";
		const string _autoStartKeyName = "GNE_DualMonitorTools";

		ISettingsService _settingsService;
		IHotKeyService _hotKeyService;
		ILogger _logger;

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

		public GeneralModule(ISettingsService settingsService, IHotKeyService hotKeyService, ILogger logger)
		{
			_settingsService = settingsService;
			_hotKeyService = hotKeyService;
			_logger = logger;
		}

		public override ModuleOptionNode GetOptionNodes(/*Form form*/)
		{
			//ContainerControl panel = new GeneralOptionsPanel(this);
			////panel.Parent = form;
			//ModuleOptionNodeLeaf options = new ModuleOptionNodeLeaf("General", panel);

			ModuleOptionNodeBranch options = new ModuleOptionNodeBranch("Dual Monitor Tools", new GeneralRootOptionsPanel());
			options.Nodes.Add(new ModuleOptionNodeLeaf("General", new GeneralOptionsPanel(this)));
		

			return options;
		}
	}
}
