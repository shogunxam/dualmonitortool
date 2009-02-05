#region copyright
// This file is part of Dual Monitor Tools which is a set of tools to assist
// users with multiple monitor setups.
// Copyright (C) 2009  Gerald Evans
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

namespace SwapScreen
{
	/// <summary>
	/// Used for debugging purposes only.
	/// </summary>
	public partial class LogForm : Form
	{
		private List<string> log;

		public LogForm(List<string> log)
		{
			this.log = log;
			InitializeComponent();
		}

		private void LogForm_Load(object sender, EventArgs e)
		{
			ShowLog();
		}

		private void ShowLog()
		{
			listBox.BeginUpdate();
			listBox.Items.Clear();
			foreach (string s in log)
			{
				listBox.Items.Add(s);
			}
			listBox.EndUpdate();
		}
	}
}