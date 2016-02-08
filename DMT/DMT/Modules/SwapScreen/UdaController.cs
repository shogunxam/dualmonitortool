#region copyright
// This file is part of Dual Monitor Tools which is a set of tools to assist
// users with multiple monitor setups.
// Copyright (C) 2011-2015  Gerald Evans
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

namespace DMT.Modules.SwapScreen
{
	using System;
	using System.Collections.Generic;
	using System.Drawing;
	using System.Text;
	using System.Windows.Forms;

	using DMT.Library;
	using DMT.Library.GuiUtils;
	using DMT.Library.HotKeys;
	using DMT.Library.Settings;
	using DMT.Library.Utils;

	/// <summary>
	/// Controller for single user defined area
	/// </summary>
	class UdaController
	{
		string _moduleName;
		Command _command;
		ISettingsService _settingsService;
		IHotKeyService _hotKeyService;

		HotKeyController _hotKeyController;

		/// <summary>
		/// Initialises a new instance of the <see cref="UdaController" /> class.
		/// </summary>
		/// <param name="moduleName">Module name</param>
		/// <param name="command">The command associated with the user defined area</param>
		/// <param name="settingsService">Settings repository</param>
		/// <param name="hotKeyService">Service for registering hot keys</param>
		public UdaController(string moduleName, Command command, ISettingsService settingsService, IHotKeyService hotKeyService)
		{
			_moduleName = moduleName;
			_command = command;
			_settingsService = settingsService;
			_hotKeyService = hotKeyService;

			Description = _settingsService.GetSetting(moduleName, GetDescriptionSettingName());

			string positionSetting = _settingsService.GetSetting(moduleName, GetRectangleSettingName());
			Position = StringUtils.ToRectangle(positionSetting);

			string settingName = GetHotKeySettingName();
			string win7Key = string.Empty;
			_command.Handler = HotKeyHandler;
			_hotKeyController = hotKeyService.CreateHotKeyController(moduleName, settingName, Description, win7Key, HotKeyHandler);
		}

		~UdaController()
		{
			Dispose(false);
		}

		/// <summary>
		/// Gets or sets the location of the user defined area
		/// </summary>
		public Rectangle Position { get; protected set; }

		/// <summary>
		/// Gets or sets the description of the user defined area
		/// </summary>
		public string Description 
		{
			get
			{
				return _command.Description;
			}

			protected set
			{
				_command.Description = value;
			}
		}

		/// <summary>
		/// Gets the hot key for the user defined area
		/// </summary>
		public HotKey HotKey
		{
			get
			{
				return _hotKeyController.HotKey;
			}
		}

		string Name
		{
			get
			{
				return _command.Name;
			}
		}

		/// <summary>
		/// Setting name of a marker that we use to determine if user defined areas exist in the settings file.
		/// If they don't, we want to create a default set so the user has a starting point
		/// </summary>
		/// <returns>Marker name</returns>
		public static string GetUdaMarkerSettingName()
		{
			return "UDA1Description";
		}

		/// <summary>
		/// Dispose of the controller and associated resources
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

			UdaForm dlg = new UdaForm(this);
			if (dlg.ShowDialog() == DialogResult.OK)
			{
				// persist the new values
				_settingsService.SaveSettings();
				edited = true;
			}

			return edited;
		}

		/// <summary>
		/// Initialises values for the user defined area
		/// </summary>
		/// <param name="keyCode"> key code used by the hot key</param>
		/// <param name="position">Location of the user defined area</param>
		/// <param name="description">Description of the user defined area</param>
		/// <returns>True if managed to set up the user defined area</returns>
		public bool SetValues(uint keyCode, Rectangle position, string description)
		{
			KeyCombo keyCombo = new KeyCombo();
			keyCombo.ComboValue = keyCode;
			if (!_hotKeyController.HotKey.RegisterHotKey(keyCombo))
			{
				return false;
			}

			Position = position;
			Description = description;

			// update these values in the settings, but don't persist yet
			UpdateSettings();
			return true;
		}

		/// <summary>
		/// Hot key handler for the user defined area
		/// </summary>
		public void HotKeyHandler()
		{
			ScreenHelper.MoveActiveToRectangle(Position);
		}

		/// <summary>
		/// Gets a user displayable string of the key combination of this hotkey.
		/// </summary>
		/// <returns>User displayable string of the key combination for this hotkey</returns>
		public override string ToString()
		{
			return _hotKeyController.ToString();
		}

		private void Dispose(bool disposing)
		{
			if (disposing)
			{
				_hotKeyController.Dispose();
			}
		}

		void UpdateSettings()
		{
			_settingsService.SetSetting(_moduleName, GetDescriptionSettingName(), Description);
			uint hotKeyValue = HotKey.HotKeyCombo.ToPropertyValue();
			_settingsService.SetSetting(_moduleName, GetHotKeySettingName(), hotKeyValue);
			string rectangleSetting = StringUtils.FromRectangle(Position);
			_settingsService.SetSetting(_moduleName, GetRectangleSettingName(), rectangleSetting);
		}

		string GetDescriptionSettingName()
		{
			return Name + "Description";
		}

		string GetHotKeySettingName()
		{
			return Name + "HotKey";
		}

		string GetRectangleSettingName()
		{
			return Name + "Rectangle";
		}
	}
}
