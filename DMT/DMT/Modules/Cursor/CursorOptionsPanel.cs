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
using DMT.Library.HotKeys;
using DMT.Resources;

namespace DMT.Modules.Cursor
{
	partial class CursorOptionsPanel : UserControl
	{
		CursorModule _cursorModule;

		public CursorOptionsPanel(CursorModule cursorModule)
		{
			_cursorModule = cursorModule;

			InitializeComponent();

			InitHotKeys();
			InitOtherOptions();
		}

		void InitHotKeys()
		{
			InitHotKey(hotKeyPanelFreeCursor, _cursorModule.FreeCursorHotKeyController);
			InitHotKey(hotKeyPanelStickyCursor, _cursorModule.StickyCursorHotKeyController);
			InitHotKey(hotKeyPanelLockCursor, _cursorModule.LockCursorHotKeyController);
			InitHotKey(hotKeyPanelCursorNextScreen, _cursorModule.CursorNextScreenHotKeyController);
			InitHotKey(hotKeyPanelCursorPrevScreen, _cursorModule.CursorPrevScreenHotKeyController);
		}

		void InitHotKey(HotKeyPanel hotKeyPanel, HotKeyController hotKeyController)
		{
			hotKeyPanel.SetHotKeyController(hotKeyController);
		}

		void InitOtherOptions()
		{
			scrollBarSticky.Value = _cursorModule.MinStickyForce;
			checkBoxControlUnhindersCursor.Checked = _cursorModule.ControlUnhindersCursor;
			InitFreMovementKey();
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
			_cursorModule.ControlUnhindersCursor = checkBoxControlUnhindersCursor.Checked;
		}

		private void comboBoxFreeMovementKey_SelectedIndexChanged(object sender, EventArgs e)
		{
			_cursorModule.FreeMovementKey = FreeMovementKeyFromString(comboBoxFreeMovementKey.SelectedItem.ToString());
		}

		private void checkBoxPrimaryReturnUnhindered_CheckedChanged(object sender, EventArgs e)
		{
			_cursorModule.PrimaryReturnUnhindered = checkBoxPrimaryReturnUnhindered.Checked;
		}

		private void comboBoxCursorMode_SelectedIndexChanged(object sender, EventArgs e)
		{
			_cursorModule.DefaultCursorMode = CursorModeFromString(comboBoxCursorMode.SelectedItem.ToString());
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
			//else -  anything else - leave as left control

			return Keys.LControlKey;
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
			//else -  anything else - leave as free

			return CursorModule.CursorType.Free;
		}
	}
}
