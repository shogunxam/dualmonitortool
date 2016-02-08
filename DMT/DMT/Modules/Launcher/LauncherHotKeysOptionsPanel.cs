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

namespace DMT.Modules.Launcher
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel;
	using System.Data;
	using System.Drawing;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;
	using System.Windows.Forms;

	using DMT.Library.HotKeys;

	/// <summary>
	/// Options panel for the launchers hot keys to be edited
	/// </summary>
	partial class LauncherHotKeysOptionsPanel : UserControl
	{
		LauncherModule _launcherModule;

		/// <summary>
		/// Initialises a new instance of the <see cref="LauncherHotKeysOptionsPanel" /> class.
		/// </summary>
		/// <param name="launcherModule">Launcher module</param>
		public LauncherHotKeysOptionsPanel(LauncherModule launcherModule)
		{
			_launcherModule = launcherModule;

			InitializeComponent();

			SetupHotKeys();
		}

		void SetupHotKeys()
		{
			SetupHotKey(hotKeyPanelActivate, _launcherModule.ActivateHotKeyController);
			SetupHotKey(hotKeyPanelAddMagicWord, _launcherModule.AddMagicWordHotKeyController);
		}

		void SetupHotKey(HotKeyPanel hotKeyPanel, HotKeyController hotKeyController)
		{
			hotKeyPanel.SetHotKeyController(hotKeyController);
		}
	}
}
