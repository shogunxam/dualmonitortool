#region copyright
// This file is part of Dual Monitor Tools which is a set of tools to assist
// users with multiple monitor setups.
// Copyright (C) 2010  Gerald Evans
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
using System.Text;
using System.Windows.Forms;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace DualLauncher
{
	public partial class OptionsForm : Form
	{
		private EntryForm entryForm;

		// unique name for for a key for use within the Run section of the registry
		private const string autoStartKeyName = "GNE_DualLauncher";


		public OptionsForm(EntryForm entryForm)
		{
			this.entryForm = entryForm;
			InitializeComponent();
		}

		private void OptionsForm_Load(object sender, EventArgs e)
		{
			InitMagicWordsTab();
			InitKeysTab();
			InitGeneralTab();
			InitImportTab();

			dataGridView.Focus();
			dataGridView.Select();
		}

		private BindingSource bindingSource;

		private void InitMagicWordsTab()
		{
			// first make sure magic words are iniially sorted (by alias ascending)
			PropertyDescriptor property = TypeDescriptor.GetProperties(typeof(MagicWord)).Find("Alias", false);
			Debug.Assert(property != null);
			MagicWords.Instance.Sort(property, ListSortDirection.Ascending);

			// bind the the magic word list
			//dataGridView.DataSource = MagicWords.Instance.IList;
			bindingSource = new BindingSource();
			bindingSource.DataSource = MagicWords.Instance.DataSource;
			dataGridView.DataSource = bindingSource;

			UpdateMagicWordButtons();
		}

		private void InitKeysTab()
		{
			labelActivate.Text = entryForm.ActivateHotKeyController.ToString();
			labelAddMagicWord.Text = entryForm.AddMagicWordHotKeyController.ToString();

			labelPos1.Text = KeyComboPropertyValueToString(Properties.Settings.Default.Position1Key);
			labelPos2.Text = KeyComboPropertyValueToString(Properties.Settings.Default.Position2Key);
			labelPos3.Text = KeyComboPropertyValueToString(Properties.Settings.Default.Position3Key);
			labelPos4.Text = KeyComboPropertyValueToString(Properties.Settings.Default.Position4Key);
		}

		private string KeyComboPropertyValueToString(uint propertyValue)
		{
			// TODO: KeyCombo should have a ctor that takes the property value
			KeyCombo keyCombo = new KeyCombo();
			keyCombo.FromPropertyValue(propertyValue);
			return keyCombo.ToString();
		}

		private void InitGeneralTab()
		{
			UpdateAutoStartCheckBox();
			checkBoxMru.Checked = Properties.Settings.Default.UseMru;
			numericUpDownIcons.Value = (decimal)Properties.Settings.Default.MaxMostUsedSize;

			if (Properties.Settings.Default.IconView == View.Details)
			{
				radioButtonIconDetails.Checked = true;
			}
			else if (Properties.Settings.Default.IconView == View.List)
			{
				radioButtonIconList.Checked = true;
			}
			else
			{
				radioButtonIconLargeIcon.Checked = true;
			}

			numericUpDownTimeout.Value = (decimal)Properties.Settings.Default.StartupTimeout;
		}

		private void InitImportTab()
		{
		}

		#region MagicWords grid events


		private void dataGridView_SelectionChanged(object sender, EventArgs e)
		{
			UpdateMagicWordButtons();
		}

		private void dataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.RowIndex >= 0)
			{
				EditMagicWord(e.RowIndex);
			}
		}
		#endregion

		#region MagicWords button events
		private void buttonAdd_Click(object sender, EventArgs e)
		{
			MagicWord newMagicWord = new MagicWord();
			MagicWordForm dlg = new MagicWordForm(newMagicWord);
			if (dlg.ShowDialog() == DialogResult.OK)
			{
				// need to add new word
				//MagicWords.Instance.Add(newMagicWord);
				MagicWords.Instance.Insert(newMagicWord);
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

					msg = string.Format(Properties.Resources.ConfirmDel1MW, alias);
				}
				else
				{
					msg = string.Format(Properties.Resources.ConfirmDel2MW, selectedRows.Count);
				}

				if (MessageBox.Show(msg,
					Program.MyTitle,
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
			foreach (MagicWord mw in MagicWords.Instance)
			{
				mw.UseCount = 0;
			}
		}
		#endregion

		#region MagicWords
		private void EditMagicWord(int rowIndex)
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
			MagicWord editMagicWord = MagicWords.Instance[rowIndex];
			MagicWordForm dlg = new MagicWordForm(editMagicWord);
			if (dlg.ShowDialog() == DialogResult.OK)
			{
				// dialog has already updated the MagicWord within MagicWords
			}
		}

		private void UpdateMagicWordButtons()
		{
			// can only edit if one and only one row is selected
			buttonEdit.Enabled = (dataGridView.SelectedRows.Count == 1);

			// can delete if one or more rows are selected
			buttonDelete.Enabled = (dataGridView.SelectedRows.Count > 0);
		}
		#endregion

		#region Keys
		private void buttonActivate_Click(object sender, EventArgs e)
		{
			if (entryForm.ActivateHotKeyController.Edit())
			{
				labelActivate.Text = entryForm.ActivateHotKeyController.ToString();
			}
		}

		private void buttonAddMagicWord_Click(object sender, EventArgs e)
		{
			if (entryForm.AddMagicWordHotKeyController.Edit())
			{
				labelAddMagicWord.Text = entryForm.AddMagicWordHotKeyController.ToString();
			}
		}

		private void buttonPos1_Click(object sender, EventArgs e)
		{
			KeyCombo keyCombo = new KeyCombo();
			keyCombo.FromPropertyValue(Properties.Settings.Default.Position1Key);
			string description = labelPos1Prompt.Text;
			string note = "";

			KeyComboForm dlg = new KeyComboForm(keyCombo, description, note);
			if (dlg.ShowDialog() == DialogResult.OK)
			{
				keyCombo = dlg.KeyCombo;
				// persist the new value
				Properties.Settings.Default.Position1Key = keyCombo.ToPropertyValue();
				// and commit it now
				Properties.Settings.Default.Save();
				//// update display
				labelPos1.Text = keyCombo.ToString();
			}
		}

		private void buttonPos2_Click(object sender, EventArgs e)
		{
			ChangePosKey("Position2Key", labelPos2Prompt.Text, labelPos2);
		}

		private void buttonPos3_Click(object sender, EventArgs e)
		{
			ChangePosKey("Position3Key", labelPos3Prompt.Text, labelPos3);
		}

		private void buttonPos4_Click(object sender, EventArgs e)
		{
			ChangePosKey("Position4Key", labelPos4Prompt.Text, labelPos4);
		}

		private void ChangePosKey(string propertyName, string description, Label label)
		{
			KeyCombo keyCombo = new KeyCombo();
			try
			{
				keyCombo.FromPropertyValue((uint)Properties.Settings.Default[propertyName]);
			}
			catch (Exception ex)
			{
				// looks like the property name is mis-spelt or the wrong type
				Debug.Assert(true, ex.Message);
			}
			string note = "";

			KeyComboForm dlg = new KeyComboForm(keyCombo, description, note);
			if (dlg.ShowDialog() == DialogResult.OK)
			{
				keyCombo = dlg.KeyCombo;
				// persist the new value
				Properties.Settings.Default[propertyName] = keyCombo.ToPropertyValue();
				// and commit it now
				Properties.Settings.Default.Save();
				//// update display
				label.Text = keyCombo.ToString();
			}
		}
		#endregion

		#region General
		private void checkBoxAutoStart_CheckedChanged(object sender, EventArgs e)
		{
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

		private void checkBoxMru_CheckedChanged(object sender, EventArgs e)
		{
			Properties.Settings.Default.UseMru = checkBoxMru.Checked;
			Properties.Settings.Default.Save();
		}

		private void numericUpDownIcons_ValueChanged(object sender, EventArgs e)
		{
			Properties.Settings.Default.MaxMostUsedSize = (int)numericUpDownIcons.Value;
			Properties.Settings.Default.Save();
		}

		private void radioButton_CheckedChanged(object sender, EventArgs e)
		{
			View iconView = View.LargeIcon;	// the default
			if (radioButtonIconDetails.Checked)
			{
				iconView = View.Details;
			}
			else if (radioButtonIconList.Checked)
			{
				iconView = View.List;
			}

			if (iconView != Properties.Settings.Default.IconView)
			{
				Properties.Settings.Default.IconView = iconView;
				Properties.Settings.Default.Save();
			}
		}

		private void numericUpDownTimeout_ValueChanged(object sender, EventArgs e)
		{
			Properties.Settings.Default.StartupTimeout = (uint)numericUpDownTimeout.Value;
			Properties.Settings.Default.Save();
		}
		#endregion

		#region Importing/exporting
		private void buttonDeleteAll_Click(object sender, EventArgs e)
		{
			if (MagicWords.Instance.Count > 0)
			{
				if (MessageBox.Show(Properties.Resources.ConfirmDelAllMW,
					Program.MyTitle,
					MessageBoxButtons.YesNo,
					MessageBoxIcon.Question,
					MessageBoxDefaultButton.Button2) == DialogResult.Yes)
				{
					MagicWords.Instance.Clear();
					//dataGridView.Refresh();
				}
			}
		}

		private void buttonImportXml_Click(object sender, EventArgs e)
		{
			OpenFileDialog dlg = new OpenFileDialog();
			dlg.Filter = Properties.Resources.XmlFilter;
			if (dlg.ShowDialog() == DialogResult.OK)
			{
				try
				{
					Collection<MagicWord> importedWords = XmlImporter.Import(dlg.FileName);
					MagicWords.Instance.Merge(importedWords);
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message, Program.MyTitle);
				}
			}
		}

		private void buttonExportXml_Click(object sender, EventArgs e)
		{
			SaveFileDialog dlg = new SaveFileDialog();
			dlg.Filter = Properties.Resources.XmlFilter;
			if (dlg.ShowDialog() == DialogResult.OK)
			{
				try
				{
					XmlImporter.Export(MagicWords.Instance, dlg.FileName);
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message, Program.MyTitle);
				}
			}
		}

		private void buttonImportQrs_Click(object sender, EventArgs e)
		{
			OpenFileDialog dlg = new OpenFileDialog();
			dlg.Filter = Properties.Resources.QrsFilter;
			if (dlg.ShowDialog() == DialogResult.OK)
			{
				try
				{
					Collection<MagicWord> importedWords = QrsImporter.Import(dlg.FileName);
					MagicWords.Instance.Merge(importedWords);
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message, Program.MyTitle);
				}
			}
		}
		#endregion

		private void OptionsForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			// Good time to save any upadtes to the magic words
			try
			{
				MagicWords.Instance.SaveIfDirty(Program.MyDbFnm);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, Program.MyTitle);
			}

		}

		private void dataGridView_KeyPress(object sender, KeyPressEventArgs e)
		{
			// convert the key to s atring for easier comparison
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
							// and make sure it is scolled into view
							dataGridView.CurrentCell = dataGridView.Rows[newRowIndex].Cells[0];
							break;
						}
					}
					newRowIndex++;
					// make sure we wrap around at the end (we already know totalRoes is not zero)
					newRowIndex %= totalRows;
				}
				while (newRowIndex != curRowIndex);
			}
		}
	}
}