#region copyright
// This file is part of Dual Monitor Tools which is a set of tools to assist
// users with multiple monitor setups.
// Copyright (C) 2010-2015  Gerald Evans
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

using DMT.Resources;
using DMT.Library.Settings;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;

namespace DMT.Library.HotKeys
{
	/// <summary>
	/// This is a small wrapper for the HotKey which links the
	/// Hotkey itself with the form to edit it and the property
	/// required to make its key combination persistant.
	/// </summary>
	class HotKeyController : IDisposable
	{
		ISettingsService _settingsService;

		// The HotKey does the real work
		public HotKey HotKey { get; protected set; }

		//// The name used in Properties.Settings.Default. to persist
		//// the value of the hotkey between sessions.
		//// Note: we cannot use a reference direct to the setting as it is
		//// a property.  This means we have no compile time checks that
		//// the name is spelt correctly or the type is correct as we would
		//// have if we use the property directly.
		//private string propertyName;

		string _moduleName;
		string _settingName;

		// short description of the hotkey
		public string Description { get; protected set; }

		// displayable string representing the hotkey that Windows 7
		// uses for this hotkey operation.  Leave empty if Windows 7 does
		// not have such a key.
		private string _win7Key;

		/// <summary>
		/// Ctor
		/// </summary>
		/// <param name="form">Form to receive hotkey notifications</param>
		/// <param name="id">Unique identifier for this hotkey</param>
		/// <param name="propertyName">Name within Properties.Settings used to persist the hotkey combo</param>
		/// <param name="description">Short description of the hotkey 
		/// - used when editing the hotkey to remind the user what they are changing.</param>
		/// <param name="win7Key">String representation of what Windows 7 uses for this hotkey 
		/// - again for display to the user when they are changing the hotkey</param>
		/// <param name="handler">The delegate to handle events for this hotkey</param>
		public HotKeyController(Form form, int id, ISettingsService settingsService, string moduleName, string settingName, string description, string win7Key, HotKey.HotKeyHandler handler)
		{
			// remember these for use later on
			_settingsService = settingsService;
			_moduleName = moduleName;
			_settingName = settingName;
			Description = description;
			_win7Key = win7Key;

			// now create the hotkey and hook it up with the handler
			HotKey = new HotKey(form, id);
			HotKey.RegisterHotKey(GetSavedKeyCombo());
			HotKey.HotKeyPressed += handler;
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
				HotKey.Dispose();
			}
		}

		/// <summary>
		/// Show the HotKeyFrom to allow the hotkey to be chnaged.
		/// </summary>
		/// <returns>true if user OK'd the HotKeyForm</returns>
		public bool Edit()
		{
			bool edited = false;

			// If Windows 7 has its own hotkey for this,
			// then we display a note to the user about it.
			// The content of the note will depend on whether they
			// are running Windows 7 or not.
			string note = "";
			if (_win7Key != null && _win7Key.Length > 0)
			{
				if (OsHelper.IsWin7OrLater())
				{
					note = string.Format(CommonStrings.Win7, _win7Key);
				}
				else
				{
					note = string.Format(CommonStrings.NotWin7, _win7Key);
				}
			}
			HotKeyForm dlg = new HotKeyForm(HotKey, Description, note);
			if (dlg.ShowDialog() == DialogResult.OK)
			{
				// persist the new value
				SaveKeyCombo();
				//// and commit it now
				////Properties.Settings.Default.Save();
				//Controller.Instance.SaveSettings();
				////// update display
				////lbl.Text = hotKey.HotKeyCombo.ToString();
				// indicate OK has been pressed
				edited = true;
			}

			return edited;
		}

		/// <summary>
		/// Checks if the hotkey is enabled
		/// </summary>
		/// <returns>true if the hotkey is in use</returns>
		public bool IsEnabled()
		{
			//return hotKey.HotKeyCombo.ToPropertyValue() != KeyCombo.DisabledComboValue;
			return HotKey.HotKeyCombo.Enabled;
		}

		// Gets the HotKey combo value from the persisted value in Poperties.Settings
		private KeyCombo GetSavedKeyCombo()
		{
			KeyCombo keyCombo = new KeyCombo();
			// if we have any trouble accessing the saved value of the KeyCombo
			// we default to disabling it
			uint hotKeyValue = KeyCombo.DisabledComboValue;
			try
			{
				//hotKeyValue = (uint)Properties.Settings.Default[propertyName];
				hotKeyValue = _settingsService.GetSettingAsUInt(_moduleName, _settingName);
			}
			catch (Exception ex)
			{
				// looks like the property name is mis-spelt or the wrong type
				Debug.Assert(true, ex.Message);
			}
			keyCombo.FromPropertyValue(hotKeyValue);
			return keyCombo;
		}

		// Persists the hotkey combo value to Properties.Settings
		private void SaveKeyCombo()
		{
			uint hotKeyValue = HotKey.HotKeyCombo.ToPropertyValue();
			try
			{
				//Properties.Settings.Default[propertyName] = hotKeyValue;
				_settingsService.SetSetting(_moduleName, _settingName, hotKeyValue);
				_settingsService.SaveSettings();
			}
			catch (Exception ex)
			{
				// looks like the property name is mis-spelt or the wrong type
				Debug.Assert(true, ex.Message);
			}
		}

		/// <summary>
		/// Gets a user displayable string of the key combination of this hotkey.
		/// </summary>
		/// <returns>User displayable string of the key combination for this hotkey</returns>
		public override string ToString()
		{
			return HotKey.HotKeyCombo.ToString();
		}

	}
}
