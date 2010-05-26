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
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;

namespace SwapScreen
{
	class HotKeyController : IDisposable
	{
		// The HotKey does the real work
		private HotKey hotKey;

		// The name used in Properties.Settings.Default. to persist
		// the value of the hotkey between sessions.
		// Note: we cannot use a reference direct to the setting as it is
		// a property.  This means we have no compile time checks that
		// the name is spelt correctly or the type is correct as we would
		// have if we use the property directly.
		private string propertyName;

		// short description of the hotkey
		private string description;
		public string Description
		{
			get { return description; }
		}

		// displayable string representing the hotkey that Windows 7
		// uses for this hotkey operation.  Leave empty if Windows 7 does
		// not have such a key.
		private string win7Key;

		public HotKeyController(Form form, int id, string propertyName, string description, string win7Key, HotKey.HotKeyHandler handler)
		{
			// remember these for use later on
			this.propertyName = propertyName;
			this.description = description;
			this.win7Key = win7Key;

			// now create the hotkey and hook it up with the handler
			hotKey = new HotKey(form, id);
			hotKey.RegisterHotKey(GetSavedKeyCombo());
			hotKey.HotKeyPressed += handler;
		}

		~HotKeyController()
		{
			Dispose(false);
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		private void Dispose(bool disposing)
		{
			if (disposing)
			{
				hotKey.Dispose();
			}
		}

		public bool Edit()
		{
			bool edited = false;
			string note = "";
			if (win7Key != null && win7Key.Length > 0)
			{
				if (OsHelper.IsWin7())
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
				// persist the new value
				SaveKeyCombo();
				// and commit it now
				Properties.Settings.Default.Save();
				//// update display
				//lbl.Text = hotKey.HotKeyCombo.ToString();
				// indicate OK has been pressed
				edited = true;
			}

			return edited;
		}

		private KeyCombo GetSavedKeyCombo()
		{
			KeyCombo keyCombo = new KeyCombo();
			// if we have any trouble accessing the saved value of the KeyCombo
			// we default to disabling it
			uint hotKeyValue = KeyCombo.DisabledComboValue;
			try
			{
				hotKeyValue = (uint)Properties.Settings.Default[propertyName];
			}
			catch (Exception ex)
			{
				// looks like the property name is mis-spelt or the wrong type
				Debug.Assert(true, ex.Message);
			}
			keyCombo.FromPropertyValue(hotKeyValue);
			return keyCombo;
		}

		private void SaveKeyCombo()
		{
			uint hotKeyValue = hotKey.HotKeyCombo.ToPropertyValue();
			try
			{
				Properties.Settings.Default[propertyName] = hotKeyValue;
			}
			catch (Exception ex)
			{
				// looks like the property name is mis-spelt or the wrong type
				Debug.Assert(true, ex.Message);
			}
		}

		public override string ToString()
		{
			return hotKey.HotKeyCombo.ToString();
		}

	}
}
