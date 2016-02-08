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

namespace DMT.Modules.Cursor
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

	using DMT.Library.HotKeys;
	using DMT.Resources;

	/// <summary>
	/// General options panel for the cursor module
	/// </summary>
	partial class CursorGeneralOptionsPanel : UserControl
	{
		CursorModule _cursorModule;

		/// <summary>
		/// Initialises a new instance of the <see cref="CursorGeneralOptionsPanel" /> class.
		/// </summary>
		/// <param name="cursorModule">The cursor module</param>
		public CursorGeneralOptionsPanel(CursorModule cursorModule)
		{
			_cursorModule = cursorModule;

			InitializeComponent();

			InitHotKeys();
			InitOtherOptions();
		}

		static string FreeMovementKeyToString(Keys key)
		{
			if (key == Keys.RControlKey)
			{
				return CommonStrings.RControlKey;
			}
			else if (key == Keys.RControlKey)
			{
				return CommonStrings.LShiftKey;
			}
			else if (key == Keys.LShiftKey)
			{
				return CommonStrings.RShiftKey;
			}

			// else if (key == Keys.LControlKey) - default case
			return CommonStrings.LControlKey;
		}

		static Keys FreeMovementKeyFromString(string keyName)
		{
			if (keyName == CommonStrings.RControlKey)
			{
				return Keys.RControlKey;
			}
			else if (keyName == CommonStrings.LShiftKey)
			{
				return Keys.LShiftKey;
			}
			else if (keyName == CommonStrings.RShiftKey)
			{
				return Keys.RShiftKey;
			}

			// else -  anything else - leave as left control
			return Keys.LControlKey;
		}

		static string FreeMovementButtonToString(MouseButtons mouseButton)
		{
			if (mouseButton == MouseButtons.Middle)
			{
				return CommonStrings.MidMouseButton;
			}
			else if (mouseButton == MouseButtons.Right)
			{
				return CommonStrings.RightMouseButton;
			}
			else if (mouseButton == MouseButtons.XButton1)
			{
				return CommonStrings.X1MouseButton;
			}
			else if (mouseButton == MouseButtons.XButton2)
			{
				return CommonStrings.X2MouseButton;
			}

			// else if (mouseButton == MouseButtons.Left) - default case
			return CommonStrings.LeftMouseButton;
		}

		static MouseButtons FreeMovementButtonFromString(string buttonName)
		{
			if (buttonName == CommonStrings.MidMouseButton)
			{
				return MouseButtons.Middle;
			}
			else if (buttonName == CommonStrings.RightMouseButton)
			{
				return MouseButtons.Right;
			}
			else if (buttonName == CommonStrings.X1MouseButton)
			{
				return MouseButtons.XButton1;
			}
			else if (buttonName == CommonStrings.X2MouseButton)
			{
				return MouseButtons.XButton2;
			}

			// else -  anything else - leave as left mouse button
			return MouseButtons.Left;
		}

		static string CursorModeToString(CursorModule.CursorType cursorMode)
		{
			if (cursorMode == CursorModule.CursorType.Sticky)
			{
				return CursorStrings.StickyCursorDescription;
			}
			else if (cursorMode == CursorModule.CursorType.Lock)
			{
				return CursorStrings.LockCursorDescription;
			}

			// else if (cursorMode == CursorModule.CursorType.Free) - default case
			return CursorStrings.FreeCursorDescription;
		}

		static CursorModule.CursorType CursorModeFromString(string cursorModeName)
		{
			if (cursorModeName == CursorStrings.StickyCursorDescription)
			{
				return CursorModule.CursorType.Sticky;
			}
			else if (cursorModeName == CursorStrings.LockCursorDescription)
			{
				return CursorModule.CursorType.Lock;
			}

			// else -  anything else - leave as free
			return CursorModule.CursorType.Free;
		}

		void InitHotKeys()
		{
			InitHotKey(hotKeyPanelFreeCursor, _cursorModule.FreeCursorHotKeyController);
			InitHotKey(hotKeyPanelStickyCursor, _cursorModule.StickyCursorHotKeyController);
			InitHotKey(hotKeyPanelLockCursor, _cursorModule.LockCursorHotKeyController);
			InitHotKey(hotKeyPanelCursorNextScreen, _cursorModule.CursorNextScreenHotKeyController);
			InitHotKey(hotKeyPanelCursorPrevScreen, _cursorModule.CursorPrevScreenHotKeyController);
			InitHotKey(hotKeyPanelCursorToPrimaryScreen, _cursorModule.CursorToPrimaryScreenHotKeyController);
		}

		void InitHotKey(HotKeyPanel hotKeyPanel, HotKeyController hotKeyController)
		{
			hotKeyPanel.SetHotKeyController(hotKeyController);
		}

		void InitOtherOptions()
		{
			scrollBarSticky.Value = _cursorModule.MinStickyForce;
			checkBoxControlUnhindersCursor.Checked = _cursorModule.AllowFreeMovementKey;
			InitFreMovementKey();
			checkBoxAllowButton.Checked = _cursorModule.AllowFreeMovementButton;
			InitFreMovementButton();
			checkBoxPrimaryReturnUnhindered.Checked = _cursorModule.PrimaryReturnUnhindered;
			InitDefaultCursorMode();
		}

		void InitFreMovementKey()
		{
			// add available keys to the listbox
			comboBoxFreeMovementKey.BeginUpdate();
			comboBoxFreeMovementKey.Items.Clear();
			comboBoxFreeMovementKey.Items.Add(CommonStrings.LControlKey);
			comboBoxFreeMovementKey.Items.Add(CommonStrings.RControlKey);
			comboBoxFreeMovementKey.Items.Add(CommonStrings.LShiftKey);
			comboBoxFreeMovementKey.Items.Add(CommonStrings.RShiftKey);
			comboBoxFreeMovementKey.SelectedItem = FreeMovementKeyToString(_cursorModule.FreeMovementKey);
			comboBoxFreeMovementKey.EndUpdate();
		}

		void InitFreMovementButton()
		{
			// add available buttons to the listbox
			comboBoxFreeMovementButton.BeginUpdate();
			comboBoxFreeMovementButton.Items.Clear();
			comboBoxFreeMovementButton.Items.Add(CommonStrings.LeftMouseButton);
			comboBoxFreeMovementButton.Items.Add(CommonStrings.MidMouseButton);
			comboBoxFreeMovementButton.Items.Add(CommonStrings.RightMouseButton);
			comboBoxFreeMovementButton.Items.Add(CommonStrings.X1MouseButton);
			comboBoxFreeMovementButton.Items.Add(CommonStrings.X2MouseButton);
			comboBoxFreeMovementButton.SelectedItem = FreeMovementButtonToString(_cursorModule.FreeMovementButton);
			comboBoxFreeMovementButton.EndUpdate();
		}

		void InitDefaultCursorMode()
		{
			comboBoxCursorMode.BeginUpdate();
			comboBoxCursorMode.Items.Clear();
			comboBoxCursorMode.Items.Add(CursorStrings.FreeCursorDescription);
			comboBoxCursorMode.Items.Add(CursorStrings.StickyCursorDescription);
			comboBoxCursorMode.Items.Add(CursorStrings.LockCursorDescription);
			comboBoxCursorMode.SelectedItem = CursorModeToString(_cursorModule.DefaultCursorMode);
			comboBoxCursorMode.EndUpdate();
		}

		private void scrollBarSticky_ValueChanged(object sender, EventArgs e)
		{
			_cursorModule.MinStickyForce = scrollBarSticky.Value;
		}

		private void checkBoxControlUnhindersCursor_CheckedChanged(object sender, EventArgs e)
		{
			_cursorModule.AllowFreeMovementKey = checkBoxControlUnhindersCursor.Checked;
		}

		private void comboBoxFreeMovementKey_SelectedIndexChanged(object sender, EventArgs e)
		{
			_cursorModule.FreeMovementKey = FreeMovementKeyFromString(comboBoxFreeMovementKey.SelectedItem.ToString());
		}

		private void checkBoxAllowButton_CheckedChanged(object sender, EventArgs e)
		{
			_cursorModule.AllowFreeMovementButton = checkBoxAllowButton.Checked;
		}

		private void comboBoxFreeMovementButton_SelectedIndexChanged(object sender, EventArgs e)
		{
			_cursorModule.FreeMovementButton = FreeMovementButtonFromString(comboBoxFreeMovementButton.SelectedItem.ToString());
		}

		private void checkBoxPrimaryReturnUnhindered_CheckedChanged(object sender, EventArgs e)
		{
			_cursorModule.PrimaryReturnUnhindered = checkBoxPrimaryReturnUnhindered.Checked;
		}

		private void comboBoxCursorMode_SelectedIndexChanged(object sender, EventArgs e)
		{
			_cursorModule.DefaultCursorMode = CursorModeFromString(comboBoxCursorMode.SelectedItem.ToString());
		}
	}
}
