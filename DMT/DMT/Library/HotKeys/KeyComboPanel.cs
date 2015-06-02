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

namespace DMT.Library.HotKeys
{
	public partial class KeyComboPanel : UserControl
	{
		/// <summary>
		/// Constructor which initialises the panel
		/// </summary>
		public KeyComboPanel()
		{
			InitializeComponent();
			FillKeysCombo();
		}

		/// <summary>
		/// Used to show a KeyCombo in the panel,
		/// or to retieve the KeyCombo currently shown in the panel.
		/// </summary>
		public KeyCombo KeyCombo
		{
			get
			{
				return KeyComboFromPanel();
			}
			set
			{
				KeyComboToPanel(value);
			}
		}

		/// <summary>
		/// Writeable value indicating if the Win modifier key is allowed
		/// </summary>
		public bool AllowWin
		{
			// TODO: use Enabled or Visible?
			set { chkWin.Enabled = value; }
		}

		private KeyCombo KeyComboFromPanel()
		{
			KeyCombo keyCombo = new KeyCombo();

			if (comboKey.SelectedItem != null)
			{
				keyCombo.KeyCode = VirtualKey.NameToCode(this.comboKey.SelectedItem.ToString());
				keyCombo.AltMod = chkAlt.Checked;
				keyCombo.ControlMod = chkCtrl.Checked;
				keyCombo.ShiftMod = chkShift.Checked;
				keyCombo.WinMod = chkWin.Checked;
			}

			return keyCombo;
		}

		private void KeyComboToPanel(KeyCombo keyCombo)
		{
			chkAlt.Checked = keyCombo.AltMod;
			chkCtrl.Checked = keyCombo.ControlMod;
			chkShift.Checked = keyCombo.ShiftMod;
			chkWin.Checked = keyCombo.WinMod;

			string keyName = VirtualKey.CodeToName(keyCombo.KeyCode);
			comboKey.SelectedItem = keyName;
		}

		private void FillKeysCombo()
		{
			comboKey.BeginUpdate();
			comboKey.Items.Clear();
			//foreach (VirtualKey virtualKey in virtualKeys)
			foreach (VirtualKey virtualKey in VirtualKey.AllVirtualKeys)
			{
				comboKey.Items.Add(virtualKey.Name);
			}
			comboKey.EndUpdate();
		}
	}
}

