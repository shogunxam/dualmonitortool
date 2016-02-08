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

namespace DMT.Library.HotKeys
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel;
	using System.Data;
	using System.Drawing;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;
	using System.Windows.Forms;

	/// <summary>
	/// Panel to display a hotkey
	/// </summary>
	partial class HotKeyPanel : UserControl
	{
		HotKeyController _hotKeyController = null;

		/// <summary>
		/// Initialises a new instance of the <see cref="HotKeyPanel" /> class.
		/// </summary>
		public HotKeyPanel()
		{
			InitializeComponent();
		}

		/// <summary>
		/// Gets or sets the description of the hotkey
		/// </summary>
		[Description("Description of hotkey"), Category("Data")]
		public string Description
		{
			get { return labelDescription.Text; }
			set { labelDescription.Text = value; }
		}

		/// <summary>
		/// Associates the hot key controller with this panel
		/// </summary>
		/// <param name="hotKeyController">Hot key controller to use</param>
		public void SetHotKeyController(HotKeyController hotKeyController)
		{
			_hotKeyController = hotKeyController;

			labelDescription.Text = hotKeyController.Description;
			labelKeyCombo.Text = hotKeyController.ToString();
		}

		private void buttonChange_Click(object sender, EventArgs e)
		{
			// show edit box
			if (_hotKeyController != null)
			{
				if (_hotKeyController.Edit())
				{
					labelKeyCombo.Text = _hotKeyController.ToString();
				}
			}
		}
	}
}
