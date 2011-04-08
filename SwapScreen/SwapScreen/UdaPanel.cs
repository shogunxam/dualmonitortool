#region copyright
// This file is part of Dual Monitor Tools which is a set of tools to assist
// users with multiple monitor setups.
// Copyright (C) 2011  Gerald Evans
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
using System.Text;
using System.Windows.Forms;

namespace SwapScreen
{
	public partial class UdaPanel : UserControl
	{
		public UdaPanel()
		{
			InitializeComponent();
		}

		private UdaController udaController;

		public void AssociateWith(UdaController udaController)
		{
			this.udaController = udaController;

			ShowControllerValues();
		}

		private void buttonChange_Click(object sender, EventArgs e)
		{
			if (udaController != null)
			{
				UdaForm dlg = new UdaForm(udaController);
				if (dlg.ShowDialog() == DialogResult.OK)
				{
					// TODO: inform master controller value has changed

					// mustrefresh our display
					ShowControllerValues();
				}
			}
		}

		private void ShowControllerValues()
		{
			string description = "";
			string keyCombo = "";
			if (udaController != null)
			{
				description = udaController.Description;
				keyCombo = udaController.HotKeyCombo.ToString();
			}
			labelDescription.Text = description;
			labelKeyCombo.Text = keyCombo;
		}

	}
}
