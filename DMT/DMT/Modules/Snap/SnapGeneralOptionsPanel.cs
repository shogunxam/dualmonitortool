#region copyright
// This file is part of Dual Monitor Tools which is a set of tools to assist
// users with multiple monitor setups.
// Copyright (C) 2009-2015  Gerald Evans
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

namespace DMT.Modules.Snap
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
	/// Options panel for the snap general options
	/// </summary>
	partial class SnapGeneralOptionsPanel : UserControl
	{
		SnapModule _snapModule;

		/// <summary>
		/// Initialises a new instance of the <see cref="SnapGeneralOptionsPanel" /> class.
		/// </summary>
		/// <param name="snapModule">The snap module</param>
		public SnapGeneralOptionsPanel(SnapModule snapModule)
		{
			_snapModule = snapModule;

			InitializeComponent();

			SetupHotKeys();
		}

		private void SnapGeneralOptionsPanel_Load(object sender, EventArgs e)
		{
			numericUpDownSnaps.Value = (decimal)_snapModule.MaxSnaps;
			checkBoxShowSnap.Checked = _snapModule.AutoShowSnap;
			checkBoxExpandSnap.Checked = _snapModule.ExpandSnap;
			checkBoxShrinkSnap.Checked = _snapModule.ShrinkSnap;
			checkBoxMaintainAspectRatio.Checked = _snapModule.MaintainAspectRatio;
		}

		private void numericUpDownSnaps_ValueChanged(object sender, EventArgs e)
		{
			_snapModule.MaxSnaps = (int)_snapModule.MaxSnaps;
		}

		private void checkBoxShowSnap_CheckedChanged(object sender, EventArgs e)
		{
			_snapModule.AutoShowSnap = checkBoxShowSnap.Checked;
		}

		private void checkBoxExpandSnap_CheckedChanged(object sender, EventArgs e)
		{
			_snapModule.ExpandSnap = checkBoxExpandSnap.Checked;
		}

		private void checkBoxShrinkSnap_CheckedChanged(object sender, EventArgs e)
		{
			_snapModule.ShrinkSnap = checkBoxShrinkSnap.Checked;
		}

		private void checkBoxMaintainAspectRatio_CheckedChanged(object sender, EventArgs e)
		{
			_snapModule.MaintainAspectRatio = checkBoxMaintainAspectRatio.Checked;
		}

		void SetupHotKeys()
		{
			SetupHotKey(hotKeyPanelShowSnap, _snapModule.ShowSnapHotKeyController);
			SetupHotKey(hotKeyPanelTakeSnap, _snapModule.TakeScreenSnapHotKeyController);
			SetupHotKey(hotKeyPanelTakeWinSnap, _snapModule.TakeWindowSnapHotKeyController);
		}

		void SetupHotKey(HotKeyPanel hotKeyPanel, HotKeyController hotKeyController)
		{
			hotKeyPanel.SetHotKeyController(hotKeyController);
		}
	}
}
