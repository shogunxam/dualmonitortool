using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SwapScreen
{
	public partial class OptionsForm : Form
	{
		private bool shutDown = false;

		private const int IDM_ABOUTBOX = 0x100;
		private const int ID_HOTKEY_SWAPSCREEN = 0x101;

		private HotKey swapScreenHotKey;

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
			keyComboPanel.KeyCombo = defaultKeyCombo;
			swapScreenHotKey.RegisterHotKey();

			swapScreenHotKey.HotKeyPressed += new HotKey.HotKeyHandler(ScreenHelper.MoveActiveWindow);
		}

		private void TermHotKey()
		{
			swapScreenHotKey.CleanUp();
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
			keyComboPanel.SaveKeyCombo();

			swapScreenHotKey.HotKeyCombo = keyComboPanel.KeyCombo;

			// save it to the config file
			Properties.Settings.Default.HotKeyValue = swapScreenHotKey.HotKeyCombo.ToPropertyValue();
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
				keyComboPanel.ShowKeyCombo();
				this.Visible = true;
			}
		}

		#region menu event handlers
		void toolStripMenuItemShowDesktop_Click(object sender, EventArgs e)
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