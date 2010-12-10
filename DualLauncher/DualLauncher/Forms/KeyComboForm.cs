#region copyright
// This file is part of Dual Monitor Tools which is a set of tools to assist
// users with multiple monitor setups.
// Copyright (C) 2009-2010  Gerald Evans
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

// TODO: Make this more generic so that it can be used instead of HotKeyForm
// - need to make validation overridable or event driven

namespace DualLauncher
{
	/// <summary>
	/// Form to edit a single hotkey
	/// </summary>
	public partial class KeyComboForm : Form
	{
		// KeyCombo being edited
		private KeyCombo keyCombo;
		public KeyCombo KeyCombo
		{
			get { return keyCombo; }
		}

		private string description;
		private string note;

		/// <summary>
		/// Ctor for the form
		/// </summary>
		/// <param name="keyCombo">The keyCombo to edit</param>
		/// <param name="description">A short description of the function of the hotkey.</param>
		/// <param name="note">Further information relating to the hotkey.
		/// If the functionality of this hotkey is provided by Windows 7, 
		/// then currently we use this note to advise the user of this fact.</param>
		public KeyComboForm(KeyCombo keyCombo, string description, string note)
		{
			this.keyCombo = keyCombo;
			this.description = description;
			this.note = note;

			InitializeComponent();
		}

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);

			labelDescription.Text = description;

			if (note != null && note.Length > 0)
			{
				lblNote.Text = note;
			}
			else
			{
				HideNoteBox();
			}
			keyComboPanel.KeyCombo = keyCombo;
			checkBoxEnable.Checked = keyCombo.Enabled;
			keyComboPanel.AllowWin = false;

			UpdateEnableStatus();
		}

		// Hides the (yellow) box where we would normally display the note
		private void HideNoteBox()
		{
			// adjust the clent size to hide the note box
			Size clientSize = this.ClientSize;
			clientSize.Height = lblNote.Location.Y;
			this.ClientSize = clientSize;
		}

		private void checkBoxEnable_CheckedChanged(object sender, EventArgs e)
		{
			UpdateEnableStatus();
		}

		private void UpdateEnableStatus()
		{
			keyComboPanel.Enabled = checkBoxEnable.Checked;
		}

		private void buttonOK_Click(object sender, EventArgs e)
		{
			// get the hotkey from the panel
			KeyCombo newKeyCombo = keyComboPanel.KeyCombo;

			// and disable/enable it depending on the checkbox
			newKeyCombo.Enabled = checkBoxEnable.Checked;

			if (newKeyCombo.Enabled)
			{
				if (newKeyCombo.KeyCode == (Keys)0)
				{
					MessageBox.Show(Properties.Resources.NoKey, Program.MyTitle);
					return;
				}
			}

			//// attempt to register this hotkey
			//if (!hotKey.RegisterHotKey(keyCombo))
			//{
			//    MessageBox.Show(Properties.Resources.RegisterFail, Program.MyTitle);
			//    return;
			//}

			// hotkey is OK
			keyCombo = newKeyCombo;

			//// save it to the config file
			//Properties.Settings.Default.HotKeyValue = keyComboPanel.KeyCombo.ToPropertyValue();
			//Properties.Settings.Default.Save();

			DialogResult = DialogResult.OK;
			Close();
		}

		private void buttonCancel_Click(object sender, EventArgs e)
		{

		}
	}
}