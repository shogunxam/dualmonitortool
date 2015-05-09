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
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DMT.Modules.WallpaperChanger.Plugins.LocalDisk
{
	/// <summary>
	/// Configuration form for a single provider from the Local Disk plugin
	/// </summary>
	public partial class LocalDiskForm : Form
	{
		public LocalDiskForm(LocalDiskConfig config)
		{
			InitializeComponent();

			numericUpDownWeight.Value = config.Weight;
			textBoxDescription.Text = config.Description;
			textBoxDirectory.Text = config.Directory;
			checkBoxRecursive.Checked = config.Recursive;
			checkBoxRescan.Checked = config.Rescan;
		}

		public LocalDiskConfig GetConfig()
		{
			// ALT: could save original config and update it directly
			LocalDiskConfig config = new LocalDiskConfig();
			config.Weight = (int)numericUpDownWeight.Value;
			config.Description = textBoxDescription.Text;
			config.Directory = textBoxDirectory.Text;
			config.Recursive = checkBoxRecursive.Checked;
			config.Rescan = checkBoxRescan.Checked;

			return config;
		}

		private void buttonBrowse_Click(object sender, EventArgs e)
		{
			folderBrowserDialog.RootFolder = Environment.SpecialFolder.Desktop;
			folderBrowserDialog.SelectedPath = textBoxDirectory.Text;
			if (folderBrowserDialog.ShowDialog(this) == DialogResult.OK)
			{
				textBoxDirectory.Text = folderBrowserDialog.SelectedPath;
			}
		}

		private void buttonAbout_Click(object sender, EventArgs e)
		{

		}

		private void buttonOK_Click(object sender, EventArgs e)
		{
			// TODO: validation

			DialogResult = DialogResult.OK;
			Close();
		}

		private void buttonCancel_Click(object sender, EventArgs e)
		{

		}
	}
}
