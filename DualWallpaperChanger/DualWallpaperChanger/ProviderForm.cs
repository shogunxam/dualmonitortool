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
	/// Form to show the list of plugins 
	/// so that the user can choose when as a new provider
	/// </summary>
	public partial class ProviderForm : Form
	{
		Controller _controller;

		public string SelectedProviderName { get; set; }

		public ProviderForm(Controller controller)
		{
			_controller = controller;

			InitializeComponent();

			InitializeProviders();
		}

		void InitializeProviders()
		{
			dataGridView.AutoGenerateColumns = false;
			List<IDWC_Plugin> plugins = _controller.GetPluginsDataSource();
			dataGridView.DataSource = plugins;
		}

		private void buttonOK_Click(object sender, EventArgs e)
		{
			if (!AcceptSelectedProvider())
			{
				MessageBox.Show(Resources.PleaseSelectProvider, this.Text);
			}
		}

		private void dataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
		{
			AcceptSelectedProvider();
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
