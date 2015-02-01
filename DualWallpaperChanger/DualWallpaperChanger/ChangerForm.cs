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

using DualMonitorTools.DualWallpaperChanger.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DualMonitorTools.DualWallpaperChanger
{
	/// <summary>
	/// Main form for the application
	/// that allows the options to be set
	/// handles the timer
	/// and performs any error logging
	/// </summary>
	public partial class ChangerForm : Form, ILogger
	{
		bool _shutDown = false;
		bool _paused = false;
		int _minutesSinceLastChange = 0;

		private const string autoStartKeyName = "GNE_DualWallpaperChanger";

		Controller _controller;	// This does the work of generating the walpapers

		public ChangerForm()
		{
			_controller = new Controller(this);

			InitializeComponent();

			InitializeDialog();

			InitializeChanging();

			if (Settings.Default.FirstRun)
			{
				// First time user has run program 
				// so show options in case they don't spot that it has been put in the notification area
				ShowOptions();
				Settings.Default.FirstRun = false;
				Settings.Default.Save();
			}
		}

		void InitializeDialog()
		{
			UpdateAutoStartCheckBox();

			checkBoxChangeOnStart.Checked = Settings.Default.ChangeOnStartup;
			checkBoxChangePeriodically.Checked = Settings.Default.ChangePeriodically;
			numericUpDownHours.Value = Settings.Default.IntervalHours;
			numericUpDownMinutes.Value = Settings.Default.IntervalMinutes;
			UpdatePeriodEnableStatus();

			FillFitComboBox();
			FillMultiMonitorComboBox();

			Color color = Color.FromArgb(Settings.Default.BackgroundColour);
			pictureBoxBackgroundColour.BackColor = color;

			dataGridView.AutoGenerateColumns = false;
			dataGridView.DataSource = _controller.GetProvidersDataSource();

			UpdateProviderButtons();
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
				if ((int)stretchType.Type == Settings.Default.Fit)
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
		void FillMultiMonitorComboBox()
		{
			comboBoxMultiMonitor.Items.Clear();
			List<SwitchType> allSwitchTypes = SwitchType.AllTypes();
			SwitchType selectedItem = null;
			foreach (SwitchType switchType in allSwitchTypes)
			{
				comboBoxMultiMonitor.Items.Add(switchType);
				if ((int)switchType.Mapping == Settings.Default.MultiMonitors)
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

		/// <summary>
		/// Once only initialisation of the timig mechanism to periodically update the wallpaper
		/// </summary>
		void InitializeChanging()
		{
			if (Properties.Settings.Default.ChangeOnStartup)
			{
				// perform initial change of wallpaper
				UpdateWallpaper();
			}

			// start a minute timer
			timer.Interval = 60 * 1000;
			timer.Tick += new EventHandler(Timer_Tick);
			timer.Start();
			UpdateTimeToChange();
		}

		void Timer_Tick(object sender, EventArgs e)
		{
			if (!_paused)
			{
				_minutesSinceLastChange++;
				int period = GetMinutesBetweenChanges();
				if (Settings.Default.ChangePeriodically && _minutesSinceLastChange >= period)
				{
					UpdateWallpaper();
				}
				else
				{
					// need to update time till next change
					UpdateTimeToChange();
				}
			}
		}

		/// <summary>
		/// Provides a visual indication of when the wallpaper will next be changed
		/// </summary>
		void UpdateTimeToChange()
		{
			string msgText;
			Color msgColor = SystemColors.ControlText;

			if (_controller.GetProvidersDataSource().Count == 0)
			{
				msgText = Properties.Resources.MsgNoProviders;
				msgColor = Color.Red;
			}
			else if (_paused)
			{
				msgText = Properties.Resources.MsgIsPaused;
			}
			else if (Settings.Default.ChangePeriodically)
			{
				int period = GetMinutesBetweenChanges();
				int timeLeft = period - _minutesSinceLastChange;
				if (timeLeft < 0)
				{
					timeLeft = 0;
				}
				string formatString = (timeLeft <= 1) ? Properties.Resources.MsgTimeToChange1 : Properties.Resources.MsgTimeToChange2;
				msgText = string.Format(formatString, timeLeft);
			}
			else
			{
				msgText = Properties.Resources.MsgNoChanging;
			}

			labelNextChange.Text = msgText;
			labelNextChange.ForeColor = msgColor;
		}


		private void exitToolStripMenuItem_Click(object sender, EventArgs e)
		{
			_shutDown = true;
			this.Close();
			Application.Exit();
		}

		private void SwitcherForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			// don't shutdown if the form is just being closed 
			if (_shutDown || e.CloseReason != CloseReason.UserClosing)
			{
				CleanUpBeforeExiting();
			}
			else
			{
				// just hide the form and stop it from closing
				this.Visible = false;
				e.Cancel = true;
			}
		}

		void CleanUpBeforeExiting()
		{
		}

		private void nextToolStripMenuItem_Click(object sender, EventArgs e)
		{
			UpdateWallpaper();
		}

		private void pauseToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (_paused)
			{
				// already paused, so resume
				this.pauseToolStripMenuItem.Checked = false;
				_paused = false;
			}
			else
			{
				// pause
				this.pauseToolStripMenuItem.Checked = true;
				_paused = true;
			}
			UpdateTimeToChange();
		}

		/// <summary>
		/// Shows the options dialog
		/// </summary>
		void ShowOptions()
		{
			this.Visible = true;
			this.Activate();
		}

		/// <summary>
		/// Call this to update the wallpaper now
		/// </summary>
		void UpdateWallpaper()
		{
			_controller.UpdateWallpaper();
			_minutesSinceLastChange = 0;
			UpdateTimeToChange();
		}

		private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ShowOptions();
		}

		private void buttonClose_Click(object sender, EventArgs e)
		{
			this.Visible = false;
		}

		private void checkBoxChangeOnStart_CheckedChanged(object sender, EventArgs e)
		{
			Settings.Default.ChangeOnStartup = checkBoxChangeOnStart.Checked;
			Settings.Default.Save();
		}

		private void checkBoxChangePeriodically_CheckedChanged(object sender, EventArgs e)
		{
			Settings.Default.ChangePeriodically = checkBoxChangePeriodically.Checked;
			Settings.Default.Save();
			UpdatePeriodEnableStatus();
			UpdateTimeToChange();
		}

		void UpdatePeriodEnableStatus()
		{
			bool enabled = checkBoxChangePeriodically.Checked;
			numericUpDownHours.Enabled = enabled;
			numericUpDownMinutes.Enabled = enabled;
		}

		private void numericUpDownHours_ValueChanged(object sender, EventArgs e)
		{
			Settings.Default.IntervalHours = (int)numericUpDownHours.Value;
			Settings.Default.Save();
			//UpdateTimer();
			UpdateTimeToChange();
		}

		private void numericUpDownMinutes_ValueChanged(object sender, EventArgs e)
		{
			Settings.Default.IntervalMinutes = (int)numericUpDownMinutes.Value;
			Settings.Default.Save();
			//UpdateTimer();
			UpdateTimeToChange();
		}

		int GetMinutesBetweenChanges()
		{
			int ret = Settings.Default.IntervalHours * 60 + Settings.Default.IntervalMinutes;
			return ret;
		}

		private void pictureBoxBackgroundColour_Click(object sender, EventArgs e)
		{
			if (colorDialog.ShowDialog() == DialogResult.OK)
			{
				pictureBoxBackgroundColour.BackColor = colorDialog.Color;
				Settings.Default.BackgroundColour = colorDialog.Color.ToArgb();
				Settings.Default.Save();
			}
		}

		private void comboBoxFit_SelectedIndexChanged(object sender, EventArgs e)
		{
			StretchType stretchType = comboBoxFit.SelectedItem as StretchType;
			if (stretchType != null)
			{
				Settings.Default.Fit = (int)stretchType.Type;
				Settings.Default.Save();
			}
		}

		private void comboBoxMultiMonitor_SelectedIndexChanged(object sender, EventArgs e)
		{
			SwitchType switchType = comboBoxMultiMonitor.SelectedItem as SwitchType;
			if (switchType != null)
			{
				Settings.Default.MultiMonitors = (int)switchType.Mapping;
				Settings.Default.Save();
			}
		}

		private void notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			ShowOptions();
		}

		private void dataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.RowIndex >= 0)
			{
				EditProvider(e.RowIndex);
			}
		}

		private void buttonAdd_Click(object sender, EventArgs e)
		{
			ProviderForm dlg = new ProviderForm(_controller);
			if (dlg.ShowDialog(this) == DialogResult.OK)
			{
				string providerName = dlg.SelectedProviderName;
				_controller.AddProvider(providerName);
				UpdateTimeToChange();
			}
		}

		private void buttonEdit_Click(object sender, EventArgs e)
		{
			DataGridViewSelectedRowCollection selectedRows = dataGridView.SelectedRows;
			if (selectedRows.Count == 1)
			{
				DataGridViewRow row = selectedRows[0];
				EditProvider(row.Index);
			}

		}

		private void buttonDelete_Click(object sender, EventArgs e)
		{
			DataGridViewSelectedRowCollection selectedRows = dataGridView.SelectedRows;
			if (selectedRows.Count > 0)
			{
				string msg;
				if (selectedRows.Count == 1)
				{
					// get the provider being deleted
					string description = "?";
					DataGridViewRow row = selectedRows[0];
					Object rowObject = row.DataBoundItem;
					IImageProvider provider = rowObject as IImageProvider;
					if (provider != null)
					{
						description = provider.Description;
					}

					msg = string.Format(Properties.Resources.ConfirmDelProvider, description);
				}
				else
				{
					msg = string.Format(Properties.Resources.ConfirmDelProviders, selectedRows.Count);
				}

				if (MessageBox.Show(msg,
					Program.MyTitle,
					MessageBoxButtons.YesNo,
					MessageBoxIcon.Question,
					MessageBoxDefaultButton.Button2) == DialogResult.Yes)
				{
					List<int> rowIndexesToDelete = new List<int>();
					foreach (DataGridViewRow row in selectedRows)
					{
						rowIndexesToDelete.Add(row.Index);
					}
					_controller.DeleteProviders(rowIndexesToDelete);
					dataGridView.Refresh();
					UpdateTimeToChange();

				}
			}
		}

		void EditProvider(int rowIndex)
		{
			if (_controller.EditProvider(rowIndex))
			{
				dataGridView.Refresh();
			}
		}

		private void dataGridView_SelectionChanged(object sender, EventArgs e)
		{
			UpdateProviderButtons();
		}

		void UpdateProviderButtons()
		{
			// can only edit if one and only one row is selected
			buttonEdit.Enabled = (dataGridView.SelectedRows.Count == 1);

			// can delete if one or more rows are selected
			buttonDelete.Enabled = (dataGridView.SelectedRows.Count > 0);
		}

		private void aboutDualWallpaperChangerToolStripMenuItem_Click(object sender, EventArgs e)
		{
			AboutForm dlg = new AboutForm();
			// TODO: why doesn't this appear to be modal?
			dlg.ShowDialog();
		}

		private void visitDualWallpaperChangerWebsiteToolStripMenuItem_Click(object sender, EventArgs e)
		{
			VisitDualMonitorToolsWebsite("duallauncher.html");
		}

		private void VisitDualMonitorToolsWebsite(string pageName)
		{
			try
			{
				string url = "http://dualmonitortool.sourceforge.net/";
				if (!string.IsNullOrEmpty(pageName))
				{
					url += pageName;
				}
				System.Diagnostics.Process.Start(url);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, Program.MyTitle);
			}
		}

		private void buttonChangeWallpaper_Click(object sender, EventArgs e)
		{
			UpdateWallpaper();
		}


		#region AutoStart
		private void checkBoxAutoStart_CheckedChanged(object sender, EventArgs e)
		{
			// Note: this updates the auto start status immediatedly
			// rather than waiting for the OK button to be pressed
			if (this.checkBoxAutoStart.Checked)
			{
				AutoStart.SetAutoStart(autoStartKeyName);
			}
			else
			{
				AutoStart.UnsetAutoStart(autoStartKeyName);
			}

			// refresh checkbox in case set/unset AutoStart failed
			UpdateAutoStartCheckBox();

		}
		private void UpdateAutoStartCheckBox()
		{
			this.checkBoxAutoStart.Checked = AutoStart.IsAutoStart(autoStartKeyName);
		}
		#endregion

		#region Error display
		/// <summary>
		/// Log the exception
		/// </summary>
		/// <param name="source">Eg. name of plugin or source filename</param>
		/// <param name="ex">The exception to log</param>
		public void LogException(string source, Exception ex)
		{
			AddMessage(source, ex.Message);
		}

		/// <summary>
		/// Log a message
		/// </summary>
		/// <param name="source">Eg. name of plugin or source filename</param>
		/// <param name="format">String.Format string</param>
		/// <param name="formatParams">args for String.Format string</param>
		public void LogMessage(string source, string format, params object[] formatParams)
		{
			AddMessage(source, string.Format(format, formatParams));
		}

		void AddMessage(string source, string message)
		{
			listBoxErrors.Items.Add(source + ": " + message);
		}

		private void buttonCopyErrors_Click(object sender, EventArgs e)
		{
			StringBuilder sb = new StringBuilder();
			sb.AppendLine("Provider errors:");
			foreach (string s in listBoxErrors.Items)
			{
				sb.AppendLine(s);
			}
			Clipboard.SetText(sb.ToString());
		}

		private void buttonClearErrors_Click(object sender, EventArgs e)
		{
			listBoxErrors.Items.Clear();
		}

		#endregion Error display

	}
}
