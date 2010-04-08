using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SwapScreen
{
	public partial class HotKeyForm : Form
	{
		private HotKey hotKey;

		public HotKeyForm(HotKey hotKey, string decription, string note)
		{
			InitializeComponent();

			labelDescription.Text = decription;

			if (note != null && note.Length > 0)
			{
				lblNote.Text = note;
			}
			else
			{
				HideNoteBox();
			}
			this.hotKey = hotKey;
			keyComboPanel.KeyCombo = hotKey.HotKeyCombo;
			checkBoxEnable.Checked = hotKey.HotKeyCombo.Enabled;
			UpdateEnableStatus();
		}

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
			KeyCombo keyCombo = keyComboPanel.KeyCombo;

			// and disable/enable it depending on the checkbox
			keyCombo.Enabled = checkBoxEnable.Checked;

			if (keyCombo.Enabled)
			{
				if (keyCombo.KeyCode == (Keys)0)
				{
					MessageBox.Show(Properties.Resources.NoKey, Program.MyTitle);
					return;
				}
			}

			// attempt to register this hotkey
			if (!hotKey.RegisterHotKey(keyCombo))
			{
				MessageBox.Show(Properties.Resources.RegisterFail, Program.MyTitle);
				return;
			}

			// hotkey is OK

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