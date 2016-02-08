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
	using System.Collections.ObjectModel;
	using System.ComponentModel;
	using System.Data;
	using System.Drawing;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;
	using System.Windows.Forms;

	using DMT.Resources;

	/// <summary>
	/// Option panel for Launcher's import options
	/// </summary>
	partial class LauncherImportOptionsPanel : UserControl
	{
		LauncherModule _launcherModule;

		/// <summary>
		/// Initialises a new instance of the <see cref="LauncherImportOptionsPanel" /> class.
		/// </summary>
		/// <param name="launcherModule">Launcher module</param>
		public LauncherImportOptionsPanel(LauncherModule launcherModule)
		{
			_launcherModule = launcherModule;

			InitializeComponent();
		}

		private void buttonDeleteAll_Click(object sender, EventArgs e)
		{
			if (_launcherModule.MagicWords.Count > 0)
			{
				if (MessageBox.Show(
					LauncherStrings.ConfirmDelAllMW,
					CommonStrings.MyTitle,
					MessageBoxButtons.YesNo,
					MessageBoxIcon.Question,
					MessageBoxDefaultButton.Button2) == DialogResult.Yes)
				{
					_launcherModule.MagicWords.Clear();
				}
			}
		}

		private void buttonImportXml_Click(object sender, EventArgs e)
		{
			OpenFileDialog dlg = new OpenFileDialog();
			dlg.Filter = LauncherStrings.XmlFilter;
			if (dlg.ShowDialog() == DialogResult.OK)
			{
				try
				{
					Collection<MagicWord> importedWords = XmlImporter.Import(dlg.FileName);
					_launcherModule.MagicWords.Merge(importedWords);
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message, CommonStrings.MyTitle);
				}
			}
		}

		private void buttonExportXml_Click(object sender, EventArgs e)
		{
			SaveFileDialog dlg = new SaveFileDialog();
			dlg.Filter = LauncherStrings.XmlFilter;
			if (dlg.ShowDialog() == DialogResult.OK)
			{
				try
				{
					XmlImporter.Export(_launcherModule.MagicWords, dlg.FileName);
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message, CommonStrings.MyTitle);
				}
			}
		}

		private void buttonImportQrs_Click(object sender, EventArgs e)
		{
			OpenFileDialog dlg = new OpenFileDialog();
			dlg.Filter = LauncherStrings.QrsFilter;
			if (dlg.ShowDialog() == DialogResult.OK)
			{
				try
				{
					Collection<MagicWord> importedWords = QrsImporter.Import(dlg.FileName);
					_launcherModule.MagicWords.Merge(importedWords);
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message, CommonStrings.MyTitle);
				}
			}
		}
	}
}
