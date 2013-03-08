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
using Microsoft.Win32;

namespace SwapScreen
{
	/// <summary>
	/// Main form of application.
	/// This is used to show the options dialog,
	/// and handle the context menu.
	/// 
	/// Note: changes made in the options dialog have immediate effect.
	/// There is no 'OK' or 'Cancel' button.
	/// </summary>
	public partial class OptionsForm : Form
	{
		private bool shutDown = false;

		// unique name for for a key for use within the Run section of the registry
		private const string autoStartKeyName = "GNE_SwapScreen";

		private const int IDM_ABOUTBOX = 0x100;
		// ID's 0x2?? are used by Controller for HotKey Id's

		/// <summary>
		/// Initialises the form, the controller and the context menu.
		/// </summary>
		public OptionsForm()
		{
			InitializeComponent();

			this.Text = Program.MyTitle;

			Controller.Instance.Init(this);
			InitContextMenu();

			SystemEvents.DisplaySettingsChanged += new EventHandler(SystemEvents_DisplaySettingsChanged);
		}

		// This is what we do just before we exit
		private void CleanUp()
		{
			SystemEvents.DisplaySettingsChanged -= new EventHandler(SystemEvents_DisplaySettingsChanged);

			// Let the controller release all the hotkeys
			// and any other resources it has
			Controller.Instance.Term();
		}

		// dynamically add any needed menu items to the context menu
		private void InitContextMenu()
		{
			// add a 'Show Desktop n' for each known screen
			for (int screenIndex = 0; screenIndex < Screen.AllScreens.Length; screenIndex++)
			{
				AddShowDesktopMenuItem(screenIndex);
			}
		}

		// add a 'Show Desktop n' for each the given screen
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

		// Finish off initialising the form
		private void OptionsForm_Load(object sender, EventArgs e)
		{
			InitDialogValues();
			UpdateAutoStartCheckBox();

			// add 'About...' menuitem to system menu
			SystemMenuHelper.AppendSeparator(this);
			SystemMenuHelper.Append(this, IDM_ABOUTBOX, Properties.Resources.AboutMenuItem);
		}

		// displays the current hotkey strings in the form
		private void InitDialogValues()
		{
			// active window tab
			labelNextScreen.Text = Controller.Instance.NextScreenHotKeyController.ToString();
			labelPrevScreen.Text = Controller.Instance.PrevScreenHotKeyController.ToString();
			labelMinimise.Text = Controller.Instance.MinimiseHotKeyController.ToString();
			labelMaximise.Text = Controller.Instance.MaximiseHotKeyController.ToString();
			labelSupersize.Text = Controller.Instance.SupersizeHotKeyController.ToString();
			labelSwapTop2.Text = Controller.Instance.SwapTop2HotKeyController.ToString();
			labelSnapLeft.Text = Controller.Instance.SnapLeftHotKeyController.ToString();
			labelSnapRight.Text = Controller.Instance.SnapRightHotKeyController.ToString();
			labelSnapUp.Text = Controller.Instance.SnapUpHotKeyController.ToString();
			labelSnapDown.Text = Controller.Instance.SnapDownHotKeyController.ToString();

			// UDA tab
			InitUdaValues();

			// other window tab
			labelMinimiseAllBut.Text = Controller.Instance.MinimiseAllButHotKeyController.ToString();
			labelRotateNext.Text = Controller.Instance.RotateNextHotKeyController.ToString();
			labelRotatePrev.Text = Controller.Instance.RotatePrevHotKeyController.ToString();
			labelShowDesktop1.Text = Controller.Instance.ShowDesktop1HotKeyController.ToString();
			labelShowDesktop2.Text = Controller.Instance.ShowDesktop2HotKeyController.ToString();

			// cursor tab
			labelFreeCursor.Text = Controller.Instance.FreeCursorHotKeyController.ToString();
			labelStickyCursor.Text = Controller.Instance.StickyCursorHotKeyController.ToString();
			labelLockCursor.Text = Controller.Instance.LockCursorHotKeyController.ToString();
			labelCursorNextScreen.Text = Controller.Instance.CursorNextScreenHotKeyController.ToString();
			labelCursorPrevScreen.Text = Controller.Instance.CursorPrevScreenHotKeyController.ToString();
			scrollBarSticky.Value = Properties.Settings.Default.MinStickyForce;
			checkBoxControlUnhindersCursor.Checked = Properties.Settings.Default.ControlUnhindersCursor;
			checkBoxPrimaryReturnUnhindered.Checked = Properties.Settings.Default.PrimaryReturnUnhindered;
			InitDefaultCursorMode();
		}

		private void InitDefaultCursorMode()
		{
			comboBoxCursorMode.BeginUpdate();
			comboBoxCursorMode.Items.Clear();
			comboBoxCursorMode.Items.Add(Properties.Resources.FreeCursorDescription);
			comboBoxCursorMode.Items.Add(Properties.Resources.StickyCursorDescription);
			comboBoxCursorMode.Items.Add(Properties.Resources.LockCursorDescription);

			// Using SwapScreen.CursorHelper.CursorType in Properties.Settings lead to 
			// occasional compilation problems (usually just a load of warings), 
			// so int is used in the settings, and we cast to a CursorType when required.
			// TODO: need to find out the cause of the problems in Properties.Settings
			SwapScreen.CursorHelper.CursorType t = (CursorHelper.CursorType)Properties.Settings.Default.DefaultCursorType;
			if (t == CursorHelper.CursorType.Sticky)
			{
				comboBoxCursorMode.SelectedItem = Properties.Resources.StickyCursorDescription;
			}
			else if (t == CursorHelper.CursorType.Lock)
			{
				comboBoxCursorMode.SelectedItem = Properties.Resources.LockCursorDescription;
			}
			else
			{
				comboBoxCursorMode.SelectedItem = Properties.Resources.FreeCursorDescription;
			}
			comboBoxCursorMode.EndUpdate();
		}

		private void OptionsForm_Shown(object sender, EventArgs e)
		{
			// This is now performed in Form_Load()
			//SystemMenuHelper.AppendSeparator(this);
			//SystemMenuHelper.Append(this, IDM_ABOUTBOX, Properties.Resources.AboutMenuItem);
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
				CleanUp();
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
			ScreenHelper.DumpDesktopInfo(this);

			AboutForm dlg = new AboutForm();
			// TODO: why doesn't this appear to be modal?
			dlg.ShowDialog();
		}

		private void OptionsForm_HelpRequested(object sender, HelpEventArgs hlpevent)
		{
			visitSwapScreenWebsite();
		}

		private void visitSwapScreenWebsiteToolStripMenuItem_Click(object sender, EventArgs e)
		{
			visitSwapScreenWebsite();
		}

		private void visitSwapScreenWebsite()
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

		#region HotKey 'Change...' button notifications
		private void buttonNextScreen_Click(object sender, EventArgs e)
		{
			if (Controller.Instance.NextScreenHotKeyController.Edit())
			{
				labelNextScreen.Text = Controller.Instance.NextScreenHotKeyController.ToString();
			}
		}

		private void buttonPreviousScreen_Click(object sender, EventArgs e)
		{
			if (Controller.Instance.PrevScreenHotKeyController.Edit())
			{
				labelPrevScreen.Text = Controller.Instance.PrevScreenHotKeyController.ToString();
			}
		}

		private void buttonMinimise_Click(object sender, EventArgs e)
		{
			if (Controller.Instance.MinimiseHotKeyController.Edit())
			{
				labelMinimise.Text = Controller.Instance.MinimiseHotKeyController.ToString();
			}
		}

		private void buttonMinimiseAllBut_Click(object sender, EventArgs e)
		{
			if (Controller.Instance.MinimiseAllButHotKeyController.Edit())
			{
				labelMinimiseAllBut.Text = Controller.Instance.MinimiseAllButHotKeyController.ToString();
			}
		}

		private void buttonMaximise_Click(object sender, EventArgs e)
		{
			if (Controller.Instance.MaximiseHotKeyController.Edit())
			{
				labelMaximise.Text = Controller.Instance.MaximiseHotKeyController.ToString();
			}
		}

		private void buttonSuperSize_Click(object sender, EventArgs e)
		{
			if (Controller.Instance.SupersizeHotKeyController.Edit())
			{
				labelSupersize.Text = Controller.Instance.SupersizeHotKeyController.ToString();
			}
		}

		private void buttonRotateNext_Click(object sender, EventArgs e)
		{
			if (Controller.Instance.RotateNextHotKeyController.Edit())
			{
				labelRotateNext.Text = Controller.Instance.RotateNextHotKeyController.ToString();
			}
		}

		private void buttonRotatePrev_Click(object sender, EventArgs e)
		{
			if (Controller.Instance.RotatePrevHotKeyController.Edit())
			{
				labelRotatePrev.Text = Controller.Instance.RotatePrevHotKeyController.ToString();
			}
		}

		private void buttonDesktop1_Click(object sender, EventArgs e)
		{
			if (Controller.Instance.ShowDesktop1HotKeyController.Edit())
			{
				labelShowDesktop1.Text = Controller.Instance.ShowDesktop1HotKeyController.ToString();
			}
		}

		private void buttonDesktop2_Click(object sender, EventArgs e)
		{
			if (Controller.Instance.ShowDesktop2HotKeyController.Edit())
			{
				labelShowDesktop2.Text = Controller.Instance.ShowDesktop2HotKeyController.ToString();
			}
		}

		private void buttonFreeCursor_Click(object sender, EventArgs e)
		{
			if (Controller.Instance.FreeCursorHotKeyController.Edit())
			{
				labelFreeCursor.Text = Controller.Instance.FreeCursorHotKeyController.ToString();
			}
		}

		private void buttonStickyCursor_Click(object sender, EventArgs e)
		{
			if (Controller.Instance.StickyCursorHotKeyController.Edit())
			{
				labelStickyCursor.Text = Controller.Instance.StickyCursorHotKeyController.ToString();
			}
		}

		private void buttonLockCursor_Click(object sender, EventArgs e)
		{
			if (Controller.Instance.LockCursorHotKeyController.Edit())
			{
				labelLockCursor.Text = Controller.Instance.LockCursorHotKeyController.ToString();
			}
		}

		private void buttonCursorNextScreen_Click(object sender, EventArgs e)
		{
			if (Controller.Instance.CursorNextScreenHotKeyController.Edit())
			{
				labelCursorNextScreen.Text = Controller.Instance.CursorNextScreenHotKeyController.ToString();
			}
		}

		private void buttonCursorPrevScreen_Click(object sender, EventArgs e)
		{
			if (Controller.Instance.CursorPrevScreenHotKeyController.Edit())
			{
				labelCursorPrevScreen.Text = Controller.Instance.CursorPrevScreenHotKeyController.ToString();
			}
		}

		private void buttonSwapTop2_Click(object sender, EventArgs e)
		{
			if (Controller.Instance.SwapTop2HotKeyController.Edit())
			{
				labelSwapTop2.Text = Controller.Instance.SwapTop2HotKeyController.ToString();
			}
		}

		private void buttonSnapLeft_Click(object sender, EventArgs e)
		{
			if (Controller.Instance.SnapLeftHotKeyController.Edit())
			{
				labelSnapLeft.Text = Controller.Instance.SnapLeftHotKeyController.ToString();
			}
		}

		private void buttonSnapRight_Click(object sender, EventArgs e)
		{
			if (Controller.Instance.SnapRightHotKeyController.Edit())
			{
				labelSnapRight.Text = Controller.Instance.SnapRightHotKeyController.ToString();
			}
		}

		private void buttonSnapUp_Click(object sender, EventArgs e)
		{
			if (Controller.Instance.SnapUpHotKeyController.Edit())
			{
				labelSnapUp.Text = Controller.Instance.SnapUpHotKeyController.ToString();
			}
		}

		private void buttonSnapDown_Click(object sender, EventArgs e)
		{
			if (Controller.Instance.SnapDownHotKeyController.Edit())
			{
				labelSnapDown.Text = Controller.Instance.SnapDownHotKeyController.ToString();
			}
		}

		private void comboBoxCursorMode_SelectedIndexChanged(object sender, EventArgs e)
		{
			CursorHelper.CursorType cursorType = CursorHelper.CursorType.Free;

			string selected = comboBoxCursorMode.SelectedItem.ToString();
			if (selected == Properties.Resources.StickyCursorDescription)
			{
				cursorType = CursorHelper.CursorType.Sticky;
			}
			else if (selected == Properties.Resources.LockCursorDescription)
			{
				cursorType = CursorHelper.CursorType.Lock;
			}
			else
			{
				// anything else - leave as free
			}

			Properties.Settings.Default.DefaultCursorType = (int)cursorType;
			SaveSettings();
		}

		#endregion

		#region User Defined Areas
		private void InitUdaValues()
		{
			udaPanel1.AssociateWith(Controller.Instance.GetUdaController(0));
			udaPanel2.AssociateWith(Controller.Instance.GetUdaController(1));
			udaPanel3.AssociateWith(Controller.Instance.GetUdaController(2));
			udaPanel4.AssociateWith(Controller.Instance.GetUdaController(3));
			udaPanel5.AssociateWith(Controller.Instance.GetUdaController(4));
			udaPanel6.AssociateWith(Controller.Instance.GetUdaController(5));
			udaPanel7.AssociateWith(Controller.Instance.GetUdaController(6));
			udaPanel8.AssociateWith(Controller.Instance.GetUdaController(7));
			udaPanel9.AssociateWith(Controller.Instance.GetUdaController(8));
			udaPanel10.AssociateWith(Controller.Instance.GetUdaController(9));
		}

	//    private void buttonUda1_Click(object sender, EventArgs e)
	//    {
	//        Button button = sender as Button;
	//        if (button != null)
	//        {
	//            int udaNum = GetUdaNum(button);
	//            if (udaNum > 0)
	//            {
	//                UdaController udaController = Controller.Instance.GetUdaController(udaNum);
	//                if (udaController != null)
	//                {
	//                    if (udaController.Edit())
	//                    {
	////						labelMinimiseAllBut.Text = Controller.Instance.MinimiseAllButHotKeyController.ToString();
	//                    }
	//                }
	//            }
	//        }
	//    }

		//private int GetUdaNum(Button button)
		//{
		//    int ret = 0;

		//    return ret;
		//}
		#endregion

		#region Other dialog events
		private void scrollBarSticky_ValueChanged(object sender, EventArgs e)
		{
			Properties.Settings.Default.MinStickyForce = scrollBarSticky.Value;
			SaveSettings();
			// update the cursor controller in case sticky cursor currently in use
			CursorHelper.MinForce = Properties.Settings.Default.MinStickyForce;
		}

		private void checkBoxControlUnhindersCursor_CheckedChanged(object sender, EventArgs e)
		{
			Properties.Settings.Default.ControlUnhindersCursor = checkBoxControlUnhindersCursor.Checked;
			SaveSettings();
			// update the cursor controller to use this now
			CursorHelper.EnableDisableLocking = Properties.Settings.Default.ControlUnhindersCursor;
		}

		private void checkBoxPrimaryReturnUnhindered_CheckedChanged(object sender, EventArgs e)
		{
			Properties.Settings.Default.PrimaryReturnUnhindered = checkBoxPrimaryReturnUnhindered.Checked;
			SaveSettings();
			// This value is checked directly in the hook, so no need to do anything else here
		}
		#endregion


		void SaveSettings()
		{
			try
			{
				// this can throw an exception if for example the .config file is deleted
				//Properties.Settings.Default.Save();
				Controller.Instance.SaveSettings();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, Program.MyTitle);
			}
		}

		// This method is called when the display settings change.
		static void SystemEvents_DisplaySettingsChanged(object sender, EventArgs e)
		{
			CursorHelper.DisplaySettingsChanged();
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