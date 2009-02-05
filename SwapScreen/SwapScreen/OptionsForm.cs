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
	/// Main form of application.
	/// This is used to show the options dialog,
	/// handle the context menu
	/// and also contains the hot key and associated handling.
	/// </summary>
	public partial class OptionsForm : Form
	{
		private bool shutDown = false;

		private const int IDM_ABOUTBOX = 0x100;
		private const int ID_HOTKEY_SWAPSCREEN = 0x101;

		private HotKey swapScreenHotKey;

		/// <summary>
		/// Initialises the form, hot key and the context menu.
		/// </summary>
		public OptionsForm()
		{
			InitializeComponent();

			this.Text = Program.MyTitle;

			InitHotKey();
			InitContextMenu();
		}

		private void InitHotKey()
		{
			KeyCombo defaultKeyCombo = new KeyCombo();
			defaultKeyCombo.FromPropertyValue(Properties.Settings.Default.HotKeyValue);

			swapScreenHotKey = new HotKey(defaultKeyCombo, this, ID_HOTKEY_SWAPSCREEN);
			//keyComboPanel.KeyCombo = defaultKeyCombo;
			swapScreenHotKey.RegisterHotKey(defaultKeyCombo);

			swapScreenHotKey.HotKeyPressed += new HotKey.HotKeyHandler(ScreenHelper.MoveActiveWindow);
		}

		private void TermHotKey()
		{
			swapScreenHotKey.Dispose();
		}

		private void InitContextMenu()
		{
			for (int screenIndex = 0; screenIndex < Screen.AllScreens.Length; screenIndex++)
			{
				AddShowDesktopMenuItem(screenIndex);
			}
		}

		// screenIndex is 0 based
		private void AddShowDesktopMenuItem(int screenIndex)
		{
			string menuText = string.Format("Show desktop {0}", screenIndex + 1);
			ToolStripItem menuItem = new ToolStripMenuItem(menuText, null);
			menuItem.Tag = screenIndex;
			menuItem.Click += new EventHandler(toolStripMenuItemShowDesktop_Click);
			contextMenuStrip.Items.Insert(screenIndex, menuItem);
		}

		private void OptionsForm_Shown(object sender, EventArgs e)
		{
			SystemMenuHelper.AppendSeparator(this);
			SystemMenuHelper.Append(this, IDM_ABOUTBOX, Properties.Resources.AboutMenuItem);
		}

		private void buttonOk_Click(object sender, EventArgs e)
		{
			// update the hotkey
			if (!swapScreenHotKey.RegisterHotKey(keyComboPanel.KeyCombo))
			{
				MessageBox.Show(Properties.Resources.RegisterFail, Program.MyTitle);
				return;
			}

			// save it to the config file
			Properties.Settings.Default.HotKeyValue = keyComboPanel.KeyCombo.ToPropertyValue();
			Properties.Settings.Default.Save();
			// and hide ourself
			this.Visible = false;
		}

		private void buttonCancel_Click(object sender, EventArgs e)
		{
			// don't update the HotKey
			// now hide ourself
			this.Visible = false;
		}

		private void OptionsForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (shutDown)
			{
				TermHotKey();
			}
			else
			{
				// just hide the form and stop it from closing
				this.Visible = false;
				e.Cancel = true;
			}
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
			}

			base.WndProc(ref m);
		}


		private void notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			ShowOptions();
		}

		private void ShowOptions()
		{
			if (Visible)
			{
				this.Focus();
			}
			else
			{
				// refresh the display from the HotKey in use
				// (the user may have changed the hotkey in the panel,
				//  but then cancelled this dialog)
				keyComboPanel.KeyCombo = swapScreenHotKey.HotKeyCombo;
				this.Visible = true;
			}
		}

		#region menu event handlers
		private void toolStripMenuItemShowDesktop_Click(object sender, EventArgs e)
		{
			ToolStripMenuItem menuItem = sender as ToolStripMenuItem;
			if (menuItem != null)
			{
				int screenIndex = (int)menuItem.Tag;
				//MessageBox.Show(string.Format("desktop {0} clicked", screenIndex));
				ScreenHelper.ShowDestktop(screenIndex);
			}
		}

		private void swapScreensToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ScreenHelper.SwapScreens();
		}

		private void toolStripMenuItemOptions_Click(object sender, EventArgs e)
		{
			ShowOptions();
		}

		private void toolStripMenuItemAbout_Click(object sender, EventArgs e)
		{
			AboutForm dlg = new AboutForm();
			// TODO: why doesn't this appear to be modal?
			dlg.ShowDialog();
		}

		private void toolStripMenuItemExit_Click(object sender, EventArgs e)
		{
			shutDown = true;
			this.Close();
			Application.Exit();
		}
		#endregion
	}
}