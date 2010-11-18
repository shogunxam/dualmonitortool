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

		private void OptionsForm_HelpRequested(object sender, HelpEventArgs hlpevent)
		{
			Program.VisitDualSnapWebsite();
		}
	}
}