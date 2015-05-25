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

using DMT.Library.WallpaperPlugin;
using DMT.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DMT.Modules.WallpaperChanger
{
	partial class ProviderForm : Form
	{
		WallpaperChangerModule _wallpaperChangerModule;

		public string SelectedProviderName { get; set; }

		public ProviderForm(WallpaperChangerModule wallpaperChangerModule)
		{
			_wallpaperChangerModule = wallpaperChangerModule;

			InitializeComponent();

			InitializeProviders();
		}

		private void dataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
		{
			if (!AcceptSelectedProvider())
			{
				MessageBox.Show(WallpaperStrings.PleaseSelectProvider, this.Text);
			}
		}

		private void buttonOK_Click(object sender, EventArgs e)
		{
			if (!AcceptSelectedProvider())
			{
				MessageBox.Show(WallpaperStrings.PleaseSelectProvider, this.Text);
			}
		}

		void InitializeProviders()
		{
			dataGridView.AutoGenerateColumns = false;
			List<IDWC_Plugin> plugins = _wallpaperChangerModule.GetPluginsDataSource();
			dataGridView.DataSource = plugins;
		}

		bool AcceptSelectedProvider()
		{
			DataGridViewRow curRow = dataGridView.CurrentRow;
			if (curRow != null)
			{
				SelectedProviderName = curRow.Cells[1].Value.ToString();
				DialogResult = DialogResult.OK;
				Close();
				return true;
			}

			return false;
		}

	}
}
