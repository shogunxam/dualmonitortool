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
	using System.ComponentModel;
	using System.Data;
	using System.Drawing;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;
	using System.Windows.Forms;

	using DMT.Library.Settings;
	using DMT.Library.Utils;

	/// <summary>
	/// Options panel for the general options for DMT
	/// </summary>
	partial class GeneralOptionsPanel : UserControl
	{
		GeneralModule _generalModule;

		/// <summary>
		/// Initialises a new instance of the <see cref="GeneralOptionsPanel" /> class.
		/// </summary>
		/// <param name="generalModule">The general module</param>
		public GeneralOptionsPanel(GeneralModule generalModule)
		{
			_generalModule = generalModule;

			InitializeComponent();

			checkBoxAutoStart.Checked = _generalModule.StartWhenWindowsStarts;

			// "Installed version" group
			labelVersion.Text = generalModule.Version.ToString();
			labelInstallType.Text = generalModule.IsMsiInstall ? Resources.GeneralStrings.MsiInstall : Resources.GeneralStrings.PortableInstall;

			// "File locations" group
			textBoxExecutable.Text = CleanFileLocation(FileLocations.Instance.ExecutableFilename);
			textBoxSettings.Text = CleanFileLocation(FileLocations.Instance.SettingsFilename);
			textBoxMagicWords.Text = CleanFileLocation(FileLocations.Instance.MagicWordsFilename);
			textBoxWallpaperProviders.Text = CleanFileLocation(FileLocations.Instance.WallpaperProvidersFilename);
			textBoxLog.Text = CleanFileLocation(FileLocations.Instance.LogFilename);
		}

		string CleanFileLocation(string fileLocation)
		{
			// This hould only apply to the log file
			if (string.IsNullOrEmpty(fileLocation))
			{
				return Resources.GeneralStrings.FileNotUsed;
			}

			return fileLocation;
		}

		private void checkBoxAutoStart_CheckedChanged(object sender, EventArgs e)
		{
			_generalModule.StartWhenWindowsStarts = checkBoxAutoStart.Checked;
		}

		private void buttonCheckUpdates_Click(object sender, EventArgs e)
		{
			CheckUpdatesForm dlg = new CheckUpdatesForm(_generalModule);
			dlg.ShowDialog(this);
		}
	}
}
