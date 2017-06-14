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

namespace DMT.Modules.WallpaperChanger.Plugins.LocalDisk
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel;
	using System.Data;
	using System.Drawing;
	using System.IO;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;
	using System.Windows.Forms;

	using DMT.Library.GuiUtils;
	using DMT.Resources;

	/// <summary>
	/// This is used to edit the configuration of a local disk provider.
	/// </summary>
	public partial class LocalDiskForm : Form
	{
		/// <summary>
		/// Initialises a new instance of the <see cref="LocalDiskForm" /> class.
		/// </summary>
		/// <param name="config">Configuration to be edited</param>
		public LocalDiskForm(LocalDiskConfig config)
		{
			InitializeComponent();

			checkBoxEnabled.Checked = config.Enabled;
			numericUpDownWeight.Value = config.Weight;
			textBoxDescription.Text = config.Description;
			textBoxMonitor1Directory.Text = config.Monitor1Directory;
			textBoxMonitor2Directory.Text = config.Monitor2Directory;
			textBoxMonitor3Directory.Text = config.Monitor3Directory;
			textBoxMonitor4Directory.Text = config.Monitor4Directory;
			textBoxPortraitDirectory.Text = config.PortraitDirectory;
			textBoxDirectory.Text = config.DefaultDirectory;
			checkBoxRecursive.Checked = config.Recursive;
			checkBoxRescan.Checked = config.Rescan;
			checkBoxCycle.Checked = config.Cycle;
		}

		/// <summary>
		/// Gets the current (possibly edited) configuration
		/// </summary>
		/// <returns>Edited configuration</returns>
		public LocalDiskConfig GetConfig()
		{
			// ALT: could save original config and update it directly
			LocalDiskConfig config = new LocalDiskConfig();
			config.Enabled = checkBoxEnabled.Checked;
			config.Weight = (int)numericUpDownWeight.Value;
			config.Description = textBoxDescription.Text;
			config.Monitor1Directory = textBoxMonitor1Directory.Text;
			config.Monitor2Directory = textBoxMonitor2Directory.Text;
			config.Monitor3Directory = textBoxMonitor3Directory.Text;
			config.Monitor4Directory = textBoxMonitor4Directory.Text;
			config.PortraitDirectory = textBoxPortraitDirectory.Text;
			config.DefaultDirectory = textBoxDirectory.Text;
			config.Recursive = checkBoxRecursive.Checked;
			config.Rescan = checkBoxRescan.Checked;
			config.Cycle = checkBoxCycle.Checked;

			return config;
		}

		private void buttonBrowseMonitor1_Click(object sender, EventArgs e)
		{
			string sel = SelectFolder(textBoxMonitor1Directory.Text);
			if (sel != null)
			{
				textBoxMonitor1Directory.Text = sel;
			}
		}

		private void buttonBrowseMonitor2_Click(object sender, EventArgs e)
		{
			string sel = SelectFolder(textBoxMonitor2Directory.Text);
			if (sel != null)
			{
				textBoxMonitor2Directory.Text = sel;
			}
		}

		private void buttonBrowseMonitor3_Click(object sender, EventArgs e)
		{
			string sel = SelectFolder(textBoxMonitor3Directory.Text);
			if (sel != null)
			{
				textBoxMonitor3Directory.Text = sel;
			}
		}

		private void buttonBrowseMonitor4_Click(object sender, EventArgs e)
		{
			string sel = SelectFolder(textBoxMonitor4Directory.Text);
			if (sel != null)
			{
				textBoxMonitor4Directory.Text = sel;
			}
		}

		private void buttonBrowsePortrait_Click(object sender, EventArgs e)
		{
			string sel = SelectFolder(textBoxPortraitDirectory.Text);
			if (sel != null)
			{
				textBoxPortraitDirectory.Text = sel;
			}
		}

		private void buttonBrowse_Click(object sender, EventArgs e)
		{
			string sel = SelectFolder(textBoxDirectory.Text);
			if (sel != null)
			{
				textBoxDirectory.Text = sel;
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

		string SelectFolder(string initialFolder)
		{
			string ret = null;
			bool allowFileSelection = checkBoxAllowFileBrowse.Checked;

			if (allowFileSelection)
			{
#if _COMBINED_FILE_FOLDER_SELECTION
				// This does work, but the UI can be confusing so not using it ATM

				// use OpenFileDialog to select a file or a folder
				// This involves a well known trick by specifying a filename that doesn't exist
				// and turning off file exists checks.
				// When this non-existant file is returned back to us, 
				// we just take the directory that it is in.
				OpenFileDialog dlg = new OpenFileDialog();
				dlg.CheckPathExists = true;
				dlg.CheckFileExists = false;
				dlg.ValidateNames = false;
				dlg.InitialDirectory = initialFolder;
				string folderMarker = "Select this folder";
				dlg.FileName = folderMarker;
				if (dlg.ShowDialog(this) == DialogResult.OK)
				{
					if (File.Exists(dlg.FileName))
					{
						// user has selected a file
						ret = dlg.FileName;
					}
					else if (Directory.Exists(dlg.FileName))
					{
						// user has selected a directory (probably by typing in its name)
						ret = dlg.FileName;
					}
					else
					{
						// we use the base directory
						ret = Path.GetDirectoryName(dlg.FileName);
					}
				}
#else
				// use OpenFileDialog to select a file
				OpenFileDialog dlg = new OpenFileDialog();
				FileSelectionHelper.SetInitialFileNameInDialog(dlg, initialFolder);
				dlg.Filter = WallpaperStrings.OpenImageFilter;
				if (dlg.ShowDialog(this) == DialogResult.OK)
				{
					ret = dlg.FileName;
				}
#endif
			}
			else
			{
				// use FolderBrowserDialog to select a folder
				FolderBrowserDialog dlg = new FolderBrowserDialog();
				dlg.RootFolder = Environment.SpecialFolder.Desktop;
				dlg.SelectedPath = initialFolder;
				if (dlg.ShowDialog(this) == DialogResult.OK)
				{
					ret = dlg.SelectedPath;
				}
			}

			return ret;
		}

		private void checkBoxRescan_CheckedChanged(object sender, EventArgs e)
		{
			if (checkBoxRescan.Checked)
			{
				checkBoxCycle.Checked = false;
			}
		}

		private void checkBoxCycle_CheckedChanged(object sender, EventArgs e)
		{
			if (checkBoxCycle.Checked)
			{
				checkBoxRescan.Checked = false;
			}
		}
	}
}
