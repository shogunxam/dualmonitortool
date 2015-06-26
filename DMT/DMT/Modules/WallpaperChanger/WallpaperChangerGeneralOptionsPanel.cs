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

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DMT.Library.Wallpaper;
using DMT.Library.HotKeys;

namespace DMT.Modules.WallpaperChanger
{
	partial class WallpaperChangerGeneralOptionsPanel : UserControl
	{
		WallpaperChangerModule _wallpaperChangerModule;

		public WallpaperChangerGeneralOptionsPanel(WallpaperChangerModule wallpaperChangerModule)
		{
			_wallpaperChangerModule = wallpaperChangerModule;

			InitializeComponent();

			SetupHotKeys();
		}

		public void ShowNextChange(string nextChangeMsg, Color foreColor)
		{
			if (labelNextChange != null)
			{
				labelNextChange.Text = nextChangeMsg;
				labelNextChange.ForeColor = foreColor;
			}
		}

		private void WallpaperChangerGeneralOptionsPanel_Load(object sender, EventArgs e)
		{
			checkBoxChangeOnStart.Checked = _wallpaperChangerModule.ChangeOnStartup;
			checkBoxChangePeriodically.Checked = _wallpaperChangerModule.ChangePeriodically;
			checkBoxFade.Checked = _wallpaperChangerModule.SmoothFade;
			numericUpDownHours.Value = (decimal)_wallpaperChangerModule.IntervalHours;
			numericUpDownMinutes.Value = (decimal)_wallpaperChangerModule.IntervalMinutes;

			FillFitComboBox();
			FillMonitorMappingComboBox();

			pictureBoxBackgroundColour.BackColor = _wallpaperChangerModule.BackgroundColour;

			// ask the module to update time to next change
			_wallpaperChangerModule.UpdateTimeToChange();
		}

		/// <summary>
		/// Once only population of the "Fit" (stretch/shrink type) combo box
		/// </summary>
		void FillFitComboBox()
		{
			comboBoxFit.Items.Clear();
			List<StretchType> allStretchTypes = StretchType.AllTypes();
			StretchType selectedItem = null;
			foreach (StretchType stretchType in allStretchTypes)
			{
				comboBoxFit.Items.Add(stretchType);
				if (stretchType.Type == _wallpaperChangerModule.Fit)
				{
					selectedItem = stretchType;
				}
			}
			if (selectedItem != null)
			{
				comboBoxFit.SelectedItem = selectedItem;
			}
			else
			{
				// select first item
				comboBoxFit.SelectedIndex = 0;
			}
		}

		/// <summary>
		/// Once only population of the "Multi Monitor" (mode) combo box
		/// </summary>
		void FillMonitorMappingComboBox()
		{
			comboBoxMultiMonitor.Items.Clear();
			List<SwitchType> allSwitchTypes = SwitchType.AllTypes();
			SwitchType selectedItem = null;
			foreach (SwitchType switchType in allSwitchTypes)
			{
				comboBoxMultiMonitor.Items.Add(switchType);
				if (switchType.Mapping == _wallpaperChangerModule.MonitorMapping)
				{
					selectedItem = switchType;
				}
			}
			if (selectedItem != null)
			{
				comboBoxMultiMonitor.SelectedItem = selectedItem;
			}
			else
			{
				// select first item
				comboBoxMultiMonitor.SelectedIndex = 0;
			}
		}

		void SetupHotKeys()
		{
			SetupHotKey(hotKeyPanelChangeWallpaper, _wallpaperChangerModule.ChangeWallpaperHotKeyController);
		}

		void SetupHotKey(HotKeyPanel hotKeyPanel, HotKeyController hotKeyController)
		{
			hotKeyPanel.SetHotKeyController(hotKeyController);
		}

		private void checkBoxChangeOnStart_CheckedChanged(object sender, EventArgs e)
		{
			_wallpaperChangerModule.ChangeOnStartup = checkBoxChangeOnStart.Checked;
		}

		private void checkBoxChangePeriodically_CheckedChanged(object sender, EventArgs e)
		{
			// save the setting - will also trigger an update of time to next change
			_wallpaperChangerModule.ChangePeriodically = checkBoxChangePeriodically.Checked;

			// enable/disable hour/minute selection accordingly
			UpdatePeriodEnableStatus();
		}

		private void checkBoxFade_CheckedChanged(object sender, EventArgs e)
		{
			_wallpaperChangerModule.SmoothFade = checkBoxFade.Checked;
		}

		private void numericUpDownHours_ValueChanged(object sender, EventArgs e)
		{
			_wallpaperChangerModule.IntervalHours = (int)numericUpDownHours.Value;
		}

		private void numericUpDownMinutes_ValueChanged(object sender, EventArgs e)
		{
			_wallpaperChangerModule.IntervalMinutes = (int)numericUpDownMinutes.Value;
		}

		private void comboBoxMultiMonitor_SelectedIndexChanged(object sender, EventArgs e)
		{
			SwitchType switchType = comboBoxMultiMonitor.SelectedItem as SwitchType;
			if (switchType != null)
			{
				_wallpaperChangerModule.MonitorMapping = switchType.Mapping;
			}
		}

		private void comboBoxFit_SelectedIndexChanged(object sender, EventArgs e)
		{
			StretchType stretchType = comboBoxFit.SelectedItem as StretchType;
			if (stretchType != null)
			{
				_wallpaperChangerModule.Fit = stretchType.Type;
			}
		}

		private void pictureBoxBackgroundColour_Click(object sender, EventArgs e)
		{
			ColorDialog colorDialog = new ColorDialog();
			colorDialog.Color = _wallpaperChangerModule.BackgroundColour;
			if (colorDialog.ShowDialog() == DialogResult.OK)
			{
				pictureBoxBackgroundColour.BackColor = colorDialog.Color;
				_wallpaperChangerModule.BackgroundColour = colorDialog.Color;
			}
		}

		private void buttonChangeWallpaper_Click(object sender, EventArgs e)
		{
			_wallpaperChangerModule.UpdateWallpaper();
		}

		void UpdatePeriodEnableStatus()
		{
			bool enabled = checkBoxChangePeriodically.Checked;
			numericUpDownHours.Enabled = enabled;
			numericUpDownMinutes.Enabled = enabled;
		}


	}
}
