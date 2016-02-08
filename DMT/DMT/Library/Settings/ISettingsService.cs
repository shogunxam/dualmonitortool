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
	/// Interface for a settings service that can provide read/write access to settings
	/// </summary>
	public interface ISettingsService
	{
		/// <summary>
		/// Checks if the setting currently exists for the module
		/// </summary>
		/// <param name="moduleName">Module name</param>
		/// <param name="settingName">Setting name</param>
		/// <returns>True if it exists</returns>
		bool SettingExists(string moduleName, string settingName);

		/// <summary>
		/// Gets the setting value as an integer
		/// </summary>
		/// <param name="moduleName">Module name</param>
		/// <param name="settingName">Setting name</param>
		/// <param name="defaultValue">Default value if setting doesn't exist</param>
		/// <returns>The value of the setting</returns>
		int GetSettingAsInt(string moduleName, string settingName, int defaultValue = 0);

		/// <summary>
		/// Sets the setting value as an integer
		/// </summary>
		/// <param name="moduleName">Module name</param>
		/// <param name="settingName">Setting name</param>
		/// <param name="value">Value for the setting</param>
		void SetSetting(string moduleName, string settingName, int value);

		/// <summary>
		/// Gets the setting value as an unsigned integer
		/// </summary>
		/// <param name="moduleName">Module name</param>
		/// <param name="settingName">Setting name</param>
		/// <param name="defaultValue">Default value if setting doesn't exist</param>
		/// <returns>The value of the setting</returns>
		uint GetSettingAsUInt(string moduleName, string settingName, uint defaultValue = 0);

		/// <summary>
		/// Sets the setting value as an unsigned integer
		/// </summary>
		/// <param name="moduleName">Module name</param>
		/// <param name="settingName">Setting name</param>
		/// <param name="value">Value for the setting</param>
		void SetSetting(string moduleName, string settingName, uint value);

		/// <summary>
		/// Gets the setting value as a boolean
		/// </summary>
		/// <param name="moduleName">Module name</param>
		/// <param name="settingName">Setting name</param>
		/// <param name="defaultValue">Default value if setting doesn't exist</param>
		/// <returns>The value of the setting</returns>
		bool GetSettingAsBool(string moduleName, string settingName, bool defaultValue = false);

		/// <summary>
		/// Sets the setting value as a boolean
		/// </summary>
		/// <param name="moduleName">Module name</param>
		/// <param name="settingName">Setting name</param>
		/// <param name="set">Value for the setting</param>
		void SetSetting(string moduleName, string settingName, bool set);

		/// <summary>
		/// Gets the setting value as a string
		/// </summary>
		/// <param name="moduleName">Module name</param>
		/// <param name="settingName">Setting name</param>
		/// <param name="defaultValue">Default value if setting doesn't exist</param>
		/// <returns>The value of the setting</returns>
		string GetSettingAsString(string moduleName, string settingName, string defaultValue = "");

		/// <summary>
		/// Gets the setting value as a string
		/// </summary>
		/// <param name="moduleName">Module name</param>
		/// <param name="settingName">Setting name</param>
		/// <returns>The value of the setting</returns>
		string GetSetting(string moduleName, string settingName);

		/// <summary>
		/// Sets the setting value as a string
		/// </summary>
		/// <param name="moduleName">Module name</param>
		/// <param name="settingName">Setting name</param>
		/// <param name="settingValue">Value for the setting</param>
		void SetSetting(string moduleName, string settingName, string settingValue);

		/// <summary>
		/// Saves the current settings
		/// </summary>
		void SaveSettings();
	}
}
