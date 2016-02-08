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

namespace DMT.Library.Settings
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;

	/// <summary>
	/// A setting that has a boolean value
	/// </summary>
	class BoolSetting
	{
		ISettingsService _settingsService;
		string _moduleName;
		string _settingName;
		bool _value;

		/// <summary>
		/// Initialises a new instance of the <see cref="BoolSetting" /> class.
		/// </summary>
		/// <param name="settingsService">A settings repository</param>
		/// <param name="moduleName">The module name</param>
		/// <param name="settingName">The setting name</param>
		/// <param name="defaultValue">Default value to use if setting not currently set</param>
		public BoolSetting(ISettingsService settingsService, string moduleName, string settingName, bool defaultValue = false)
		{
			_settingsService = settingsService;
			_moduleName = moduleName;
			_settingName = settingName;

			_value = _settingsService.GetSettingAsBool(_moduleName, _settingName, defaultValue);
		}

		/// <summary>
		/// Gets or sets the value of the boolean setting
		/// </summary>
		public bool Value
		{
			get
			{
				return _value;
			}

			set
			{
				_value = value;
				_settingsService.SetSetting(_moduleName, _settingName, value);
				_settingsService.SaveSettings();
			}
		}
	}
}
