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

using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using DMT.Library.HotKeys;
using DMT.Library;
using DMT.Library.Settings;

namespace DMT.Modules.SwapScreen
{
	class UdaController
	{
		string _moduleName;
		int _udaIndex;
		ISettingsService _settingsService;
		IHotKeyService _hotKeyService;

		HotKeyController _hotKeyController;

		public string Description { get; protected set; }

		public Rectangle Position { get; protected set; }

		public HotKey HotKey
		{
			get
			{
				return _hotKeyController.HotKey;
			}
		}

		// The HotKey does the real work
		//private HotKey hotKey;

		///// <summary>
		///// The KeyCombo that we will be using as the hotkey.
		///// </summary>
		//public KeyCombo HotKeyCombo
		//{
		//	get { return hotKey.HotKeyCombo; }
		//}

		public UdaController(string moduleName, int idx, ISettingsService settingsService, IHotKeyService hotKeyService)
		{
			_moduleName = moduleName;
			_udaIndex = idx;
			_settingsService = settingsService;
			_hotKeyService = hotKeyService;

			Description = _settingsService.GetSetting(moduleName, GetDescriptionSettingName());

			string positionSetting = _settingsService.GetSetting(moduleName, GetRectangleSettingName());
			Position = StringUtils.ToRectangle(positionSetting);

			string settingName = GetHotKeySettingName();
			string win7Key = "";
			_hotKeyController = hotKeyService.CreateHotKeyController(moduleName, settingName, Description, win7Key, HotKeyHandler);
		}

		~UdaController()
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
				_hotKeyController.Dispose();
			}
		}


		/// <summary>
		/// Show the HotKeyFrom to allow the hotkey to be chnaged.
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

		void UpdateSettings()
		{
			_settingsService.SetSetting(_moduleName, GetDescriptionSettingName(), Description);
			uint hotKeyValue = HotKey.HotKeyCombo.ToPropertyValue();
			_settingsService.SetSetting(_moduleName, GetHotKeySettingName(), hotKeyValue);
			string rectangleSetting = StringUtils.FromRectangle(Position);
			_settingsService.SetSetting(_moduleName, GetRectangleSettingName(), rectangleSetting);
		}

		public static string GetUdaMarkerSettingName()
		{
			return string.Format("UDA_{0}_Description", 0);
		}

		string GetDescriptionSettingName()
		{
			return string.Format("UDA_{0}_Description", _udaIndex);
		}

		string GetHotKeySettingName()
		{
			return string.Format("UDA_{0}_HotKey", _udaIndex);
		}

		string GetRectangleSettingName()
		{
			return string.Format("UDA_{0}_Rectangle", _udaIndex);
		}


		//public bool SetValues(uint keyCode, Rectangle position, string description)
		//{
		//	KeyCombo keyCombo = new KeyCombo();
		//	keyCombo.ComboValue = keyCode;
		//	if (!hotKey.RegisterHotKey(keyCombo))
		//	{
		//		return false;
		//	}

		//	this._position = position;
		//	this.Description = description;
		//	return true;
		//}


		//public string GetPropertyValue()
		//{
		//	return ToPropertyValue(hotKey.HotKeyCombo.ToPropertyValue(), _position, Description);
		//}

		//public static string ToPropertyValue(uint keyCode, Rectangle rect, string description)
		//{
		//	string ret = string.Format("{0}|{1}|{2}|{3}|{4}|{5}",
		//								keyCode,
		//								rect.Left, rect.Top, rect.Width, rect.Height,
		//								description);

		//	return ret;
		//}

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
	}
}
