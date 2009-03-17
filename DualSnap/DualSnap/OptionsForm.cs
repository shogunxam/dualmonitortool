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
		}
	}
}