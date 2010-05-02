using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DualSnap
{
	public partial class OptionsForm : Form
	{
		private const string autoStartKeyName = "GNE_DualSnap";

		public KeyCombo DualSnapHotKey
		{
			get { return keyComboPanel.KeyCombo; }
			set { keyComboPanel.KeyCombo = value; }
		}

		public int MaxSnaps
		{
			get { return Convert.ToInt32(numericUpDown.Value); }
			set { numericUpDown.Value = value; }
		}

		public bool AutoShowSnap
		{
			get { return checkBoxShowSnap.Checked; }
			set { checkBoxShowSnap.Checked = value; }
		}
	
		public OptionsForm()
		{
			InitializeComponent();
			UpdateAutoStartCheckBox();
		}

		#region AutoStart
		private void checkBoxAutoStart_CheckedChanged(object sender, EventArgs e)
		{
			// Note: this updates the auto start status immediatedly
			// rather than waiting for the OK button to be pressed
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
		#endregion
	}
}