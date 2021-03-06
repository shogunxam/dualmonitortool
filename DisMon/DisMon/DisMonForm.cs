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

namespace DisMon
{
	public partial class DisMonForm : Form
	{
		// IDs for menu items added to system menu
		private const int IDM_ABOUTBOX = 0x100;
		private const int IDM_VISITWEBSITE = 0x101;

		//private bool disabledSecondary = false;

		public DisMonForm()
		{
			InitializeComponent();
			UpdateEnabledStates();
		}

		private void buttonPS_Click(object sender, EventArgs e)
		{
			try
			{
				DisMon.Instance.Reset();
				DisMon.Instance.MarkAsPrimary(0);
				DisMon.Instance.ApplyChanges();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, Program.MyTitle);
			}
		}

		private void buttonSP_Click(object sender, EventArgs e)
		{
			try
			{
				DisMon.Instance.Reset();
				DisMon.Instance.MarkAsPrimary(1);
				DisMon.Instance.ApplyChanges();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, Program.MyTitle);
			}
		}

		private void buttonPX_Click(object sender, EventArgs e)
		{
			try
			{
				DisMon.Instance.Reset();
				DisMon.Instance.MarkAsPrimary(0);
				DisMon.Instance.MarkAsDisabled(1);
				DisMon.Instance.ApplyChanges();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, Program.MyTitle);
			}
		}

		private void buttonXP_Click(object sender, EventArgs e)
		{
			try
			{
				DisMon.Instance.Reset();
				DisMon.Instance.MarkAsPrimary(1);
				DisMon.Instance.MarkAsDisabled(0);
				DisMon.Instance.ApplyChanges();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, Program.MyTitle);
			}
		}

		//private void buttonDisable_Click(object sender, EventArgs e)
		//{
		//    DisMon.Instance.MarkAsPrimary(1);
		//    DisMon.Instance.MarkAllSecondaryAsDisabled();
		//    DisMon.Instance.ApplyChanges();
		//    disabledSecondary = true;
		//    UpdateEnabledStates();
		//}

		//private void buttonEnable_Click(object sender, EventArgs e)
		//{
		//    DisMon.Instance.Restore();
		//    disabledSecondary = false;
		//    UpdateEnabledStates();
		//}

		private void UpdateEnabledStates()
		{
			//if (disabledSecondary)
			//{
			//    buttonEnable.Enabled = true;
			//    buttonEnable.Focus();
			//    buttonDisable.Enabled = false;
			//}
			//else
			//{
			//    buttonDisable.Enabled = true;
			//    buttonDisable.Focus();
			//    buttonEnable.Enabled = false;
			//}
		}

		private void DisMonForm_Shown(object sender, EventArgs e)
		{
			SystemMenuHelper.AppendSeparator(this);
			SystemMenuHelper.Append(this, IDM_ABOUTBOX, Properties.Resources.AboutMenuItem);
			SystemMenuHelper.Append(this, IDM_VISITWEBSITE, Properties.Resources.WebsiteMenuItem);
		}

		protected override void WndProc(ref Message m)
		{
			if (m.Msg == Win32.WM_SYSCOMMAND)
			{
				if (m.WParam.ToInt32() == IDM_ABOUTBOX)
				{
					AboutForm dlg = new AboutForm();
					dlg.ShowDialog();
				}
				else if (m.WParam.ToInt32() == IDM_VISITWEBSITE)
				{
					VisitDisMonWebsite();
				}
			}

			base.WndProc(ref m);
		}


		private void DisMonForm_HelpRequested(object sender, HelpEventArgs hlpevent)
		{
			VisitDisMonWebsite();
		}

		private void VisitDisMonWebsite()
		{
			try
			{
				System.Diagnostics.Process.Start("http://dualmonitortool.sourceforge.net/dismon.html");
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, Program.MyTitle);
			}
		}
	}
}