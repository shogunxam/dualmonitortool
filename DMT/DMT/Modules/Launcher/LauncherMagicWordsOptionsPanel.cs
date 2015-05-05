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
using DMT.Resources;

namespace DMT.Modules.Launcher
{
	partial class LauncherMagicWordsOptionsPanel : UserControl
	{
		LauncherModule _launcherModule;
		BindingSource _bindingSource;

		public LauncherMagicWordsOptionsPanel(LauncherModule launcherModule)
		{
			_launcherModule = launcherModule;

			InitializeComponent();

		}

		private void LauncherMagicWordsOptionsPanel_Load(object sender, EventArgs e)
		{
			InitGrid();

			ParentForm.FormClosing += new FormClosingEventHandler(ParentForm_FormClosing);
		}

		void ParentForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			// this is a good time to try and save any changes to the magic words
			_launcherModule.SaveMagicWords();
		}

		private void buttonAdd_Click(object sender, EventArgs e)
		{
			MagicWord newMagicWord = new MagicWord();
			MagicWordForm dlg = new MagicWordForm(_launcherModule, newMagicWord);
			if (dlg.ShowDialog() == DialogResult.OK)
			{
				// need to add new word
				_launcherModule.MagicWords.Insert(newMagicWord);
			}
		}

		private void buttonEdit_Click(object sender, EventArgs e)
		{
			DataGridViewSelectedRowCollection selectedRows = dataGridView.SelectedRows;
			if (selectedRows.Count == 1)
			{
				DataGridViewRow row = selectedRows[0];
				EditMagicWord(row.Index);
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
					// get the magic word being deleted
					string alias = "?";
					DataGridViewRow row = selectedRows[0];
					Object rowObject = row.DataBoundItem;
					MagicWord curMagicWord = rowObject as MagicWord;
					if (curMagicWord != null)
					{
						alias = curMagicWord.Alias;
					}

					msg = string.Format(LauncherStrings.ConfirmDel1MW, alias);
				}
				else
				{
					msg = string.Format(LauncherStrings.ConfirmDel2MW, selectedRows.Count);
				}

				if (MessageBox.Show(msg,
					CommonStrings.MyTitle,
					MessageBoxButtons.YesNo,
					MessageBoxIcon.Question,
					MessageBoxDefaultButton.Button2) == DialogResult.Yes)
				{
					foreach (DataGridViewRow row in selectedRows)
					{
						dataGridView.Rows.Remove(row);
					}
					dataGridView.Refresh();
				}
			}
		}

		private void buttonResetCounts_Click(object sender, EventArgs e)
		{
			foreach (MagicWord mw in _launcherModule.MagicWords)
			{
				mw.UseCount = 0;
			}
		}

		private void dataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.RowIndex >= 0)
			{
				EditMagicWord(e.RowIndex);
			}
		}

		private void dataGridView_KeyPress(object sender, KeyPressEventArgs e)
		{
			// convert the key to a string for easier comparison
			string keyChar = e.KeyChar.ToString();

			int totalRows = dataGridView.Rows.Count;
			// no point in doing anything if no rows
			if (totalRows > 0)
			{
				// normally we would start searching from the first row
				int curRowIndex = 0;
				DataGridViewSelectedRowCollection selectedRows = dataGridView.SelectedRows;
				if (selectedRows.Count >= 1)
				{
					// but if something is selected, we start from the next row after the first selected item
					curRowIndex = (selectedRows[0].Index + 1) % totalRows;
				}
				int newRowIndex = curRowIndex;
				do
				{
					DataGridViewRow row = dataGridView.Rows[newRowIndex];
					Object rowObject = row.DataBoundItem;
					MagicWord magicWord = rowObject as MagicWord;
					if (magicWord != null)
					{
						string alias = magicWord.Alias;
						if (alias.ToString().StartsWith(keyChar, true, null))
						{
							// unselect the currently selected rows
							foreach (DataGridViewRow selectedRow in selectedRows)
							{
								selectedRow.Selected = false;
							}
							// select the new row
							dataGridView.Rows[newRowIndex].Selected = true;
							// and make sure it is scrolled into view
							dataGridView.CurrentCell = dataGridView.Rows[newRowIndex].Cells[0];
							break;
						}
					}
					newRowIndex++;
					// make sure we wrap around at the end (we already know totalRows is not zero)
					newRowIndex %= totalRows;
				}
				while (newRowIndex != curRowIndex);
			}
		}

		private void dataGridView_SelectionChanged(object sender, EventArgs e)
		{
			UpdateMagicWordButtons();
		}



		void InitGrid()
		{
			// first make sure magic words are initially sorted (by alias ascending)
			PropertyDescriptor property = TypeDescriptor.GetProperties(typeof(MagicWord)).Find("Alias", false);
			//Debug.Assert(property != null);
			_launcherModule.MagicWords.Sort(property, ListSortDirection.Ascending);

			// bind the magic word list
			//dataGridView.DataSource = MagicWords.Instance.IList;
			_bindingSource = new BindingSource();
			_bindingSource.DataSource =_launcherModule. MagicWords.DataSource;
			dataGridView.DataSource = _bindingSource;

			UpdateMagicWordButtons();
		}

		void EditMagicWord(int rowIndex)
		{
			//// we work on a clone of the magic word in case user decides
			//// to cancel edits
			//MagicWord editMagicWord = MagicWords.Instance[rowIndex].Clone();
			//MagicWordForm dlg = new MagicWordForm(editMagicWord);
			//if (dlg.ShowDialog() == DialogResult.OK)
			//{
			//    // update...
			//    MagicWords.Instance[rowIndex] = editMagicWord;
			//}

			// we work on a clone of the magic word in case user decides
			// to cancel edits
			MagicWord editMagicWord = _launcherModule.MagicWords[rowIndex];
			MagicWordForm dlg = new MagicWordForm(_launcherModule, editMagicWord);
			if (dlg.ShowDialog() == DialogResult.OK)
			{
				// dialog has already updated the MagicWord within MagicWords
			}
		}

		void UpdateMagicWordButtons()
		{
			// can only edit if one and only one row is selected
			buttonEdit.Enabled = (dataGridView.SelectedRows.Count == 1);

			// can delete if one or more rows are selected
			buttonDelete.Enabled = (dataGridView.SelectedRows.Count > 0);
		}


	}
}
