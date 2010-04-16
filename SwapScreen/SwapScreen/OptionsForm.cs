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

		private HotKey nextScreenHotKey;
		private HotKey prevScreenHotKey;
		private HotKey minimiseHotKey;
		private HotKey minimiseAllButHotKey;
		private HotKey maximiseHotKey;
		private HotKey supersizeHotKey;
		private HotKey rotateNextHotKey;
		private HotKey rotatePrevHotKey;
		private HotKey showDesktop1HotKey;
		private HotKey showDesktop2HotKey;

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
			nextScreenHotKey = new HotKey(this, ID_HOTKEY_NEXTSCREEN);
			InitHotKey(nextScreenHotKey, 
			           Properties.Settings.Default.HotKeyValue,
			           new HotKey.HotKeyHandler(ScreenHelper.MoveActiveToNextScreen), 
					   labelNextScreen);

			prevScreenHotKey = new HotKey(this, ID_HOTKEY_PREVSCREEN);
			InitHotKey(prevScreenHotKey,
					   Properties.Settings.Default.PrevScreenHotKey,
					   new HotKey.HotKeyHandler(ScreenHelper.MoveActiveToPrevScreen),
					   labelPrevScreen);

			minimiseHotKey = new HotKey(this, ID_HOTKEY_MINIMISE);
			InitHotKey(minimiseHotKey,
					   Properties.Settings.Default.MinimiseHotKey,
					   new HotKey.HotKeyHandler(ScreenHelper.MinimiseActive),
					   labelMinimise);

			minimiseAllButHotKey = new HotKey(this, ID_HOTKEY_MINIMISE_ALL_BUT);
			InitHotKey(minimiseAllButHotKey,
					   Properties.Settings.Default.MinimiseAllButHotKey,
					   new HotKey.HotKeyHandler(ScreenHelper.MinimiseAllButActive),
					   labelMinimiseAllBut);

			maximiseHotKey = new HotKey(this, ID_HOTKEY_MAXIMISE);
			InitHotKey(maximiseHotKey,
					   Properties.Settings.Default.MaximiseHotKey,
					   new HotKey.HotKeyHandler(ScreenHelper.MaximiseActive),
					   labelMaximise);

			supersizeHotKey = new HotKey(this, ID_HOTKEY_SUPERSIZE);
			InitHotKey(supersizeHotKey,
					   Properties.Settings.Default.SupersizeHotKey,
					   new HotKey.HotKeyHandler(ScreenHelper.SupersizeActive),
					   labelSupersize);

			rotateNextHotKey = new HotKey(this, ID_HOTKEY_ROTATENEXT);
			InitHotKey(rotateNextHotKey,
					   Properties.Settings.Default.RotateNextHotKey,
					   new HotKey.HotKeyHandler(ScreenHelper.RotateScreensNext),
					   labelRotateNext);

			rotatePrevHotKey = new HotKey(this, ID_HOTKEY_ROTATEPREV);
			InitHotKey(rotatePrevHotKey,
					   Properties.Settings.Default.RotatePrevHotKey,
					   new HotKey.HotKeyHandler(ScreenHelper.RotateScreensPrev),
					   labelRotatePrev);

			showDesktop1HotKey = new HotKey(this, ID_HOTKEY_SHOWDESKTOP1);
			InitHotKey(showDesktop1HotKey,
					   Properties.Settings.Default.ShowDesktop1HotKey,
					   new HotKey.HotKeyHandler(ScreenHelper.ShowDesktop1),
					   labelShowDesktop1);

			showDesktop2HotKey = new HotKey(this, ID_HOTKEY_SHOWDESKTOP2);
			InitHotKey(showDesktop2HotKey,
					   Properties.Settings.Default.ShowDesktop2HotKey,
					   new HotKey.HotKeyHandler(ScreenHelper.ShowDesktop2),
					   labelShowDesktop2);
		}

		private void InitHotKey(HotKey hotKey, uint savedValue, HotKey.HotKeyHandler handler, Label label)
		{
			hotKey.RegisterHotKey(GetSavedKeyCombo(savedValue));
			hotKey.HotKeyPressed += handler;
			label.Text = hotKey.HotKeyCombo.ToString();
		}

		private KeyCombo GetSavedKeyCombo(uint hotKeyValue)
		{
			KeyCombo keyCombo = new KeyCombo();
			keyCombo.FromPropertyValue(hotKeyValue);
			return keyCombo;
		}

		private void TermHotKeys()
		{
			showDesktop2HotKey.Dispose();
			showDesktop1HotKey.Dispose();
			rotatePrevHotKey.Dispose();
			rotateNextHotKey.Dispose();
			supersizeHotKey.Dispose();
			maximiseHotKey.Dispose();
			minimiseAllButHotKey.Dispose();
			minimiseHotKey.Dispose();
			prevScreenHotKey.Dispose();
			nextScreenHotKey.Dispose();
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
			if (ChangeHotkey(nextScreenHotKey, labelNextScreen,
				Properties.Resources.NextScreenDescription,
				Properties.Resources.NextScreenWin7))
			{
				Properties.Settings.Default.HotKeyValue = nextScreenHotKey.HotKeyCombo.ToPropertyValue();
				Properties.Settings.Default.Save();
			}
		}

		private void buttonPreviousScreen_Click(object sender, EventArgs e)
		{
			if (ChangeHotkey(prevScreenHotKey, labelPrevScreen,
				Properties.Resources.PrevScreenDescription,
				Properties.Resources.PrevScreenWin7))
			{
				Properties.Settings.Default.PrevScreenHotKey = prevScreenHotKey.HotKeyCombo.ToPropertyValue();
				Properties.Settings.Default.Save();
			}
		}

		private void buttonMinimise_Click(object sender, EventArgs e)
		{
			if (ChangeHotkey(minimiseHotKey, labelMinimise,
				Properties.Resources.MinimiseDescription,
				Properties.Resources.MinimiseWin7))
			{
				Properties.Settings.Default.MinimiseHotKey = minimiseHotKey.HotKeyCombo.ToPropertyValue();
				Properties.Settings.Default.Save();
			}
		}

		private void buttonMinimiseAllBut_Click(object sender, EventArgs e)
		{
			if (ChangeHotkey(minimiseAllButHotKey, labelMinimiseAllBut,
				Properties.Resources.MinimiseAllButDescription,
				Properties.Resources.MinimiseAllButWin7))
			{
				Properties.Settings.Default.MinimiseAllButHotKey = minimiseAllButHotKey.HotKeyCombo.ToPropertyValue();
				Properties.Settings.Default.Save();
			}
		}

		private void buttonMaximise_Click(object sender, EventArgs e)
		{
			if (ChangeHotkey(maximiseHotKey, labelMaximise,
				Properties.Resources.MaximiseDescription,
				Properties.Resources.MaximiseWin7))
			{
				Properties.Settings.Default.MaximiseHotKey = maximiseHotKey.HotKeyCombo.ToPropertyValue();
				Properties.Settings.Default.Save();
			}
		}

		private void buttonSuperSize_Click(object sender, EventArgs e)
		{
			if (ChangeHotkey(supersizeHotKey, labelSupersize,
				Properties.Resources.SupersizeDescription,
				Properties.Resources.SupersizeWin7))
			{
				Properties.Settings.Default.SupersizeHotKey = supersizeHotKey.HotKeyCombo.ToPropertyValue();
				Properties.Settings.Default.Save();
			}
		}

		private void buttonRotateNext_Click(object sender, EventArgs e)
		{
			if (ChangeHotkey(rotateNextHotKey, labelRotateNext,
				Properties.Resources.RotateNextDescription,
				Properties.Resources.RotateNextWin7))
			{
				Properties.Settings.Default.RotateNextHotKey = rotateNextHotKey.HotKeyCombo.ToPropertyValue();
				Properties.Settings.Default.Save();
			}
		}

		private void buttonRotatePrev_Click(object sender, EventArgs e)
		{
			if (ChangeHotkey(rotatePrevHotKey, labelRotatePrev,
				Properties.Resources.RotatePrevDescription,
				Properties.Resources.RotatePrevWin7))
			{
				Properties.Settings.Default.RotatePrevHotKey = rotatePrevHotKey.HotKeyCombo.ToPropertyValue();
				Properties.Settings.Default.Save();
			}
		}

		private void buttonDesktop1_Click(object sender, EventArgs e)
		{
			if (ChangeHotkey(showDesktop1HotKey, labelShowDesktop1,
				Properties.Resources.ShowDesktop1Description,
				Properties.Resources.ShowDesktop1Win7))
			{
				Properties.Settings.Default.ShowDesktop1HotKey = showDesktop1HotKey.HotKeyCombo.ToPropertyValue();
				Properties.Settings.Default.Save();
			}
		}

		private void buttonDesktop2_Click(object sender, EventArgs e)
		{
			if (ChangeHotkey(showDesktop2HotKey, labelShowDesktop2,
				Properties.Resources.ShowDesktop2Description,
				Properties.Resources.ShowDesktop2Win7))
			{
				Properties.Settings.Default.ShowDesktop2HotKey = showDesktop2HotKey.HotKeyCombo.ToPropertyValue();
				Properties.Settings.Default.Save();
			}
		}

		private bool ChangeHotkey(HotKey hotKey, Label lbl, string description, string win7Key)
		{
			bool ok = false;
			string note = "";
			if (win7Key != null && win7Key.Length > 0)
			{
				if (IsWin7())
				{
					note = string.Format(Properties.Resources.Win7, win7Key);
				}
				else
				{
					note = string.Format(Properties.Resources.NotWin7, win7Key);
				}
			}
			HotKeyForm dlg = new HotKeyForm(hotKey, description, note);
			if (dlg.ShowDialog() == DialogResult.OK)
			{
				// update display
				lbl.Text = hotKey.HotKeyCombo.ToString();
				// indicate OK has been pressed
				ok = true;
			}

			return ok;
		}

		private bool IsWin7()
		{
			bool isWin7 = false;

			System.OperatingSystem osInfo = System.Environment.OSVersion;

			if (osInfo.Platform == PlatformID.Win32NT)
			{
				if (osInfo.Version.Major == 6)
				{
					if (osInfo.Version.Minor == 1)
					{
						isWin7 = true;
					}
					// TODO: what about future versions of Windows
				}
			}

			return isWin7;
		}
	}
}