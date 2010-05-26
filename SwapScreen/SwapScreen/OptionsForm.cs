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

		private const string autoStartKeyName = "GNE_SwapScreen";

		private const int IDM_ABOUTBOX = 0x100;
		private const int ID_HOTKEY_NEXTSCREEN = 0x101;
		private const int ID_HOTKEY_PREVSCREEN = 0x102;
		private const int ID_HOTKEY_MINIMISE = 0x103;
		private const int ID_HOTKEY_MINIMISE_ALL_BUT = 0x104;
		private const int ID_HOTKEY_MAXIMISE = 0x105;
		private const int ID_HOTKEY_SUPERSIZE = 0x106;
		private const int ID_HOTKEY_ROTATENEXT = 0x107;
		private const int ID_HOTKEY_ROTATEPREV = 0x108;
		private const int ID_HOTKEY_SHOWDESKTOP1 = 0x109;
		private const int ID_HOTKEY_SHOWDESKTOP2 = 0x10A;


		private HotKeyController nextScreenHotKeyController;
		private HotKeyController prevScreenHotKeyController;
		private HotKeyController minimiseHotKeyController;
		private HotKeyController minimiseAllButHotKeyController;
		private HotKeyController maximiseHotKeyController;
		private HotKeyController supersizeHotKeyController;
		private HotKeyController rotateNextHotKeyController;
		private HotKeyController rotatePrevHotKeyController;
		private HotKeyController showDesktop1HotKeyController;
		private HotKeyController showDesktop2HotKeyController;

		/// <summary>
		/// Initialises the form, hot key and the context menu.
		/// </summary>
		public OptionsForm()
		{
			InitializeComponent();

			this.Text = Program.MyTitle;

			InitHotKeys();
			InitContextMenu();
		}

		private void InitHotKeys()
		{
			nextScreenHotKeyController = new HotKeyController(this, ID_HOTKEY_NEXTSCREEN,
				"HotKeyValue",
				Properties.Resources.NextScreenDescription,
				Properties.Resources.NextScreenWin7,
				new HotKey.HotKeyHandler(ScreenHelper.MoveActiveToNextScreen));

			prevScreenHotKeyController = new HotKeyController(this, ID_HOTKEY_PREVSCREEN,
				"PrevScreenHotKey",
				Properties.Resources.PrevScreenDescription,
				Properties.Resources.PrevScreenWin7,
				new HotKey.HotKeyHandler(ScreenHelper.MoveActiveToPrevScreen));

			minimiseHotKeyController = new HotKeyController(this, ID_HOTKEY_MINIMISE,
				"MinimiseHotKey",
				Properties.Resources.MinimiseDescription,
				Properties.Resources.MinimiseWin7,
				new HotKey.HotKeyHandler(ScreenHelper.MinimiseActive));

			minimiseAllButHotKeyController = new HotKeyController(this, ID_HOTKEY_MINIMISE_ALL_BUT,
				"MinimiseAllButHotKey",
				Properties.Resources.MinimiseAllButDescription,
				Properties.Resources.MinimiseAllButWin7,
				new HotKey.HotKeyHandler(ScreenHelper.MinimiseAllButActive));

			maximiseHotKeyController = new HotKeyController(this, ID_HOTKEY_MAXIMISE,
				"MaximiseHotKey",
				Properties.Resources.MaximiseDescription,
				Properties.Resources.MaximiseWin7,
				new HotKey.HotKeyHandler(ScreenHelper.MaximiseActive));

			supersizeHotKeyController = new HotKeyController(this, ID_HOTKEY_SUPERSIZE,
				"SupersizeHotKey",
				Properties.Resources.SupersizeDescription,
				Properties.Resources.SupersizeWin7,
				new HotKey.HotKeyHandler(ScreenHelper.SupersizeActive));

			rotateNextHotKeyController = new HotKeyController(this, ID_HOTKEY_ROTATENEXT,
				"RotateNextHotKey",
				Properties.Resources.RotateNextDescription,
				Properties.Resources.RotateNextWin7,
				new HotKey.HotKeyHandler(ScreenHelper.RotateScreensNext));

			rotatePrevHotKeyController = new HotKeyController(this, ID_HOTKEY_ROTATEPREV,
				"RotatePrevHotKey",
				Properties.Resources.RotatePrevDescription,
				Properties.Resources.RotatePrevWin7,
				new HotKey.HotKeyHandler(ScreenHelper.RotateScreensPrev));

			showDesktop1HotKeyController = new HotKeyController(this, ID_HOTKEY_SHOWDESKTOP1,
				"ShowDesktop1HotKey",
				Properties.Resources.ShowDesktop1Description,
				Properties.Resources.ShowDesktop1Win7,
				new HotKey.HotKeyHandler(ScreenHelper.ShowDesktop1));

			showDesktop2HotKeyController = new HotKeyController(this, ID_HOTKEY_SHOWDESKTOP2,
				"ShowDesktop2HotKey",
				Properties.Resources.ShowDesktop2Description,
				Properties.Resources.ShowDesktop2Win7,
				new HotKey.HotKeyHandler(ScreenHelper.ShowDesktop2));
		}

		private void TermHotKeys()
		{
			showDesktop2HotKeyController.Dispose();
			showDesktop1HotKeyController.Dispose();
			rotatePrevHotKeyController.Dispose();
			rotateNextHotKeyController.Dispose();
			supersizeHotKeyController.Dispose();
			maximiseHotKeyController.Dispose();
			minimiseAllButHotKeyController.Dispose();
			minimiseHotKeyController.Dispose();
			prevScreenHotKeyController.Dispose();
			nextScreenHotKeyController.Dispose();
		}

		private void InitContextMenu()
		{
			for (int screenIndex = 0; screenIndex < Screen.AllScreens.Length; screenIndex++)
			{
				AddShowDesktopMenuItem(screenIndex);
			}
		}

		private void OptionsForm_Load(object sender, EventArgs e)
		{
			InitHotKeyLabels();
			UpdateAutoStartCheckBox();
		}

		private void InitHotKeyLabels()
		{
			labelNextScreen.Text = nextScreenHotKeyController.ToString();
			labelPrevScreen.Text = prevScreenHotKeyController.ToString();
			labelMinimise.Text = minimiseHotKeyController.ToString();
			labelMinimiseAllBut.Text = minimiseAllButHotKeyController.ToString();
			labelMaximise.Text = maximiseHotKeyController.ToString();
			labelSupersize.Text = supersizeHotKeyController.ToString();
			labelRotateNext.Text = rotateNextHotKeyController.ToString();
			labelRotatePrev.Text = rotatePrevHotKeyController.ToString();
			labelShowDesktop1.Text = showDesktop1HotKeyController.ToString();
			labelShowDesktop2.Text = showDesktop2HotKeyController.ToString();
		}

		// screenIndex is 0 based
		private void AddShowDesktopMenuItem(int screenIndex)
		{
			string showDesktopFormat = Properties.Resources.ShowDesktopMenuItem;
			string menuText = string.Format(showDesktopFormat, screenIndex + 1);
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

		private void buttonCancel_Click(object sender, EventArgs e)
		{
			// don't update the HotKey
			// now hide ourself
			this.Visible = false;
		}

		private void OptionsForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			// don't shutdown if the form is just being closed 
			if (shutDown || e.CloseReason != CloseReason.UserClosing)
			{
				TermHotKeys();
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
			this.Visible = true;
			this.Activate();
		}

		#region menu event handlers
		private void toolStripMenuItemShowDesktop_Click(object sender, EventArgs e)
		{
			ToolStripMenuItem menuItem = sender as ToolStripMenuItem;
			if (menuItem != null)
			{
				int screenIndex = (int)menuItem.Tag;
				//MessageBox.Show(string.Format("desktop {0} clicked", screenIndex));
				ScreenHelper.ShowDesktop(screenIndex);
			}
		}

		private void swapScreensToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ScreenHelper.RotateScreensNext();
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

		private void visitSwapScreenWebsiteToolStripMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				System.Diagnostics.Process.Start("http://dualmonitortool.sourceforge.net/swapscreen.html");
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, Program.MyTitle);
			}
		}

		private void toolStripMenuItemExit_Click(object sender, EventArgs e)
		{
			shutDown = true;
			this.Close();
			Application.Exit();
		}
		#endregion

		private void buttonNextScreen_Click(object sender, EventArgs e)
		{
			if (nextScreenHotKeyController.Edit())
			{
				labelNextScreen.Text = nextScreenHotKeyController.ToString();
			}
		}

		private void buttonPreviousScreen_Click(object sender, EventArgs e)
		{
			if (prevScreenHotKeyController.Edit())
			{
				labelPrevScreen.Text = prevScreenHotKeyController.ToString();
			}
		}

		private void buttonMinimise_Click(object sender, EventArgs e)
		{
			if (minimiseHotKeyController.Edit())
			{
				labelMinimise.Text = minimiseHotKeyController.ToString();
			}
		}

		private void buttonMinimiseAllBut_Click(object sender, EventArgs e)
		{
			if (minimiseAllButHotKeyController.Edit())
			{
				labelMinimiseAllBut.Text = minimiseAllButHotKeyController.ToString();
			}
		}

		private void buttonMaximise_Click(object sender, EventArgs e)
		{
			if (maximiseHotKeyController.Edit())
			{
				labelMaximise.Text = maximiseHotKeyController.ToString();
			}
		}

		private void buttonSuperSize_Click(object sender, EventArgs e)
		{
			if (supersizeHotKeyController.Edit())
			{
				labelSupersize.Text = supersizeHotKeyController.ToString();
			}
		}

		private void buttonRotateNext_Click(object sender, EventArgs e)
		{
			if (rotateNextHotKeyController.Edit())
			{
				labelRotateNext.Text = rotateNextHotKeyController.ToString();
			}
		}

		private void buttonRotatePrev_Click(object sender, EventArgs e)
		{
			if (rotatePrevHotKeyController.Edit())
			{
				labelRotatePrev.Text = rotatePrevHotKeyController.ToString();
			}
		}

		private void buttonDesktop1_Click(object sender, EventArgs e)
		{
			if (showDesktop1HotKeyController.Edit())
			{
				labelShowDesktop1.Text = showDesktop1HotKeyController.ToString();
			}
		}

		private void buttonDesktop2_Click(object sender, EventArgs e)
		{
			if (showDesktop2HotKeyController.Edit())
			{
				labelShowDesktop2.Text = showDesktop2HotKeyController.ToString();
			}
		}

		#region AutoStart
		private void checkBoxAutoStart_CheckedChanged(object sender, EventArgs e)
		{
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
	}
}