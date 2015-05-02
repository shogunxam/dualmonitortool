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

namespace DMT.Modules.SwapScreen
{
	partial class UdaPanel : UserControl
	{
		UdaController _udaController = null;

		public UdaPanel()
		{
			InitializeComponent();
		}

		//public string Description
		//{
		//	get { return labelDescription.Text; }
		//	set { labelDescription.Text = value; }
		//}

		//[Description("Description of hotkey"), Category("Data")]
		//public string KeyCombo
		//{
		//	get { return labelDescription.Text; }
		//	set { labelDescription.Text = value; }
		//}

		//KeyCombo _keyCombo;

		//HotKey _hotKey;
		//public HotKey HotKey { get; set; }


		public void SetUdaController(UdaController udaController)
		{
			_udaController = udaController;

			labelDescription.Text = udaController.Description;
			labelKeyCombo.Text = udaController.ToString();
		}

		private void buttonChange_Click(object sender, EventArgs e)
		{
			// show edit box
			if (_udaController != null)
			{
				if (_udaController.Edit())
				{
					labelDescription.Text = _udaController.Description;
					labelKeyCombo.Text = _udaController.ToString();
				}
			}
		}
	}
}
