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

namespace DMT.Modules.WallpaperChanger
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

	using DMT.Library.WallpaperPlugin;
	using DMT.Resources;

	/// <summary>
	/// Options panel for the wallpaper changers providers
	/// </summary>
	partial class WallpaperChangerProvidersOptionsPanel : UserControl
	{
		WallpaperChangerModule _wallpaperChangerModule;

		/// <summary>
		/// Initialises a new instance of the <see cref="WallpaperChangerProvidersOptionsPanel" /> class.
		/// </summary>
		/// <param name="wallpaperChangerModule">The wallpaper changer module</param>
		public WallpaperChangerProvidersOptionsPanel(WallpaperChangerModule wallpaperChangerModule)
		{
			_wallpaperChangerModule = wallpaperChangerModule;

			InitializeComponent();
		}

		private void WallpaperChangerProvidersOptionsPanel_Load(object sender, EventArgs e)
		{
			dataGridView.AutoGenerateColumns = false;
			dataGridView.DataSource = _wallpaperChangerModule.GetProvidersDataSource();
		}

		private void buttonAdd_Click(object sender, EventArgs e)
		{
			ProviderForm dlg = new ProviderForm(_wallpaperChangerModule);
			if (dlg.ShowDialog(this) == DialogResult.OK)
			{
				string providerName = dlg.SelectedProviderName;
				_wallpaperChangerModule.AddProvider(providerName);
				////UpdateTimeToChange();
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
					object rowObject = row.DataBoundItem;
					IImageProvider provider = rowObject as IImageProvider;
					if (provider != null)
					{
						description = provider.Description;
					}

					msg = string.Format(WallpaperStrings.ConfirmDelProvider, description);
				}
				else
				{
					msg = string.Format(WallpaperStrings.ConfirmDelProviders, selectedRows.Count);
				}

				if (MessageBox.Show(msg, CommonStrings.MyTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
				{
					List<int> rowIndexesToDelete = new List<int>();
					foreach (DataGridViewRow row in selectedRows)
					{
						rowIndexesToDelete.Add(row.Index);
					}

					_wallpaperChangerModule.DeleteProviders(rowIndexesToDelete);
					dataGridView.Refresh();
					////UpdateTimeToChange();
				}
			}
		}

		private void dataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.RowIndex >= 0)
			{
				EditProvider(e.RowIndex);
			}
		}

		private void dataGridView_SelectionChanged(object sender, EventArgs e)
		{
			UpdateProviderButtons();
		}

		void EditProvider(int rowIndex)
		{
			if (_wallpaperChangerModule.EditProvider(rowIndex))
			{
				dataGridView.Refresh();
			}
		}

		void UpdateProviderButtons()
		{
			// can only edit if one and only one row is selected
			buttonEdit.Enabled = dataGridView.SelectedRows.Count == 1;

			// can delete if one or more rows are selected
			buttonDelete.Enabled = dataGridView.SelectedRows.Count > 0;
		}

		private void dataGridView_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
		{
			// dataGridView_CellValueChanged() does not get called until afte the user has left the cell.
			// This is all very well for a text field, but for a checkbox, we want the changes to take place 
			// as soon as the checked state of the box changes, so we catch mouse up here and end editing.
			// See http://geekswithblogs.net/FrostRed/archive/2008/09/07/125001.aspx for more details
			if (e.ColumnIndex == 0 && e.RowIndex >= 0)
			{
				BindingList<IImageProvider> providers = _wallpaperChangerModule.GetProvidersDataSource();
				dataGridView.EndEdit();
			}
		}

		private void dataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
		{
			if (e.ColumnIndex == 0 && e.RowIndex >= 0)
			{
				// checkbox has been clicked
				//BindingList<IImageProvider> providers = _wallpaperChangerModule.GetProvidersDataSource();
				//bool enabled = providers[e.RowIndex].Enabled;

				_wallpaperChangerModule.SaveProvidersConfiguration();
			}
		}
	}
}
