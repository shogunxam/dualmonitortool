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

namespace DMT.Library.HotKeys
{
	using System;
	using System.Collections.Generic;
	using System.Diagnostics;
	using System.Text;
	using System.Windows.Forms;

	using DMT.Library.Environment;
	using DMT.Library.Settings;
	using DMT.Resources;

	/// <summary>
	/// This is a small wrapper for the HotKey which links the
	/// Hotkey itself with the form to edit it and the property
	/// required to make its key combination persistent.
	/// </summary>
	class HotKeyController : IDisposable
	{
		ISettingsService _settingsService;
		string _moduleName;
		string _settingName;

		// displayable string representing the hotkey that Windows 7
		// uses for this hotkey operation.  Leave empty if Windows 7 does
		// not have such a key.
		string _win7Key;

		/// <summary>
		/// Initialises a new instance of the <see cref="HotKeyController" /> class.
		/// </summary>
		/// <param name="form">Form to receive hotkey notifications</param>
		/// <param name="id">Unique identifier for this hotkey</param>
		/// <param name="settingsService">Settings repository</param>
		/// <param name="moduleName">Module name</param>
		/// <param name="settingName">Setting name</param>
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

		/// <summary>
		/// Gets or sets the hotkey that does the real work
		/// </summary>
		public HotKey HotKey { get; protected set; }

		/// <summary>
		/// Gets or sets the short description of the hotkey
		/// </summary>
		public string Description { get; protected set; }

		/// <summary>
		/// Disposes of the object
		/// </summary>
		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>
		/// Show the HotKeyFrom to allow the hotkey to be changed.
		/// </summary>
		/// <returns>true if user OK'd the HotKeyForm</returns>
		public bool Edit()
		{
			bool edited = false;

			// If Windows 7 has its own hotkey for this,
			// then we display a note to the user about it.
			// The content of the note will depend on whether they
			// are running Windows 7 or not.
			string note = string.Empty;
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
			return HotKey.HotKeyCombo.Enabled;
		}

		/// <summary>
		/// Gets a user displayable string of the key combination of this hotkey.
		/// </summary>
		/// <returns>User displayable string of the key combination for this hotkey</returns>
		public override string ToString()
		{
			return HotKey.HotKeyCombo.ToString();
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
				_settingsService.SetSetting(_moduleName, _settingName, hotKeyValue);
				_settingsService.SaveSettings();
			}
			catch (Exception ex)
			{
				// looks like the property name is mis-spelt or the wrong type
				Debug.Assert(true, ex.Message);
			}
		}

		private void Dispose(bool disposing)
		{
			if (disposing)
			{
				HotKey.Dispose();
			}
		}
	}
}
