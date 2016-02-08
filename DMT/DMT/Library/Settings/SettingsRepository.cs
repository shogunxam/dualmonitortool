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
	using System.IO;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;

	using DMT.Library;
	using DMT.Library.Utils;

	/// <summary>
	/// Repository for holding the settings
	/// </summary>
	class SettingsRepository : ISettingsService
	{
		Dictionary<string, string> _settings;

		/// <summary>
		/// Initialises a new instance of the <see cref="SettingsRepository" /> class.
		/// </summary>
		public SettingsRepository()
		{
			LoadSettings();
		}

		/// <summary>
		/// Saves the current settings
		/// </summary>
		public void SaveSettings()
		{
			Save(_settings, GetSettingsFilename());
		}

		/// <summary>
		/// Checks if the setting currently exists for the module
		/// </summary>
		/// <param name="moduleName">Module name</param>
		/// <param name="settingName">Setting name</param>
		/// <returns>True if it exists</returns>
		public bool SettingExists(string moduleName, string settingName)
		{
			string value;
			return _settings.TryGetValue(GetDictionaryName(moduleName, settingName), out value);
		}

		#region int settings
		/// <summary>
		/// Gets the setting value as an integer
		/// </summary>
		/// <param name="moduleName">Module name</param>
		/// <param name="settingName">Setting name</param>
		/// <param name="defaultValue">Default value if setting doesn't exist</param>
		/// <returns>The value of the setting</returns>
		public int GetSettingAsInt(string moduleName, string settingName, int defaultValue = 0)
		{
			return StringUtils.ToInt(GetSetting(moduleName, settingName), defaultValue);
		}

		/// <summary>
		/// Sets the setting value as an integer
		/// </summary>
		/// <param name="moduleName">Module name</param>
		/// <param name="settingName">Setting name</param>
		/// <param name="value">Value for the setting</param>
		public void SetSetting(string moduleName, string settingName, int value)
		{
			SetSetting(moduleName, settingName, value.ToString());
		}
		#endregion

		#region uint settings
		/// <summary>
		/// Gets the setting value as an unsigned integer
		/// </summary>
		/// <param name="moduleName">Module name</param>
		/// <param name="settingName">Setting name</param>
		/// <param name="defaultValue">Default value if it doesn't exist</param>
		/// <returns>The value of the setting</returns>
		public uint GetSettingAsUInt(string moduleName, string settingName, uint defaultValue = 0)
		{
			return StringUtils.ToUInt(GetSetting(moduleName, settingName), defaultValue);
		}

		/// <summary>
		/// Sets the setting value as an unsigned integer
		/// </summary>
		/// <param name="moduleName">Module name</param>
		/// <param name="settingName">Setting name</param>
		/// <param name="value">Value for the setting</param>
		public void SetSetting(string moduleName, string settingName, uint value)
		{
			SetSetting(moduleName, settingName, value.ToString());
		}
		#endregion

		#region bool settings
		/// <summary>
		/// Gets the setting value as a boolean
		/// </summary>
		/// <param name="moduleName">Module name</param>
		/// <param name="settingName">Setting name</param>
		/// <param name="defaultValue">Default value if it doesn't exist</param>
		/// <returns>The value of the setting</returns>
		public bool GetSettingAsBool(string moduleName, string settingName, bool defaultValue = false)
		{
			return StringUtils.ToBool(GetSetting(moduleName, settingName), defaultValue);
		}

		/// <summary>
		/// Sets the setting value as a boolean
		/// </summary>
		/// <param name="moduleName">Module name</param>
		/// <param name="settingName">Setting name</param>
		/// <param name="set">Value for the setting</param>
		public void SetSetting(string moduleName, string settingName, bool set)
		{
			SetSetting(moduleName, settingName, set.ToString());
		}
		#endregion

		#region string settings
		/// <summary>
		/// Gets the setting value as a string
		/// </summary>
		/// <param name="moduleName">Module name</param>
		/// <param name="settingName">Setting name</param>
		/// <param name="defaultValue">Default value if it doesn't exist</param>
		/// <returns>The value of the setting</returns>
		public string GetSettingAsString(string moduleName, string settingName, string defaultValue)
		{
			string value;
			if (_settings.TryGetValue(GetDictionaryName(moduleName, settingName), out value))
			{
				return value;
			}

			return defaultValue;
		}

		/// <summary>
		/// Gets the setting value as a string
		/// </summary>
		/// <param name="moduleName">Module name</param>
		/// <param name="settingName">Setting name</param>
		/// <returns>The value of the setting</returns>
		public string GetSetting(string moduleName, string settingName)
		{
			return GetSettingAsString(moduleName, settingName, string.Empty);
		}

		/// <summary>
		/// Sets the setting value as a string
		/// </summary>
		/// <param name="moduleName">Module name</param>
		/// <param name="settingName">Setting name</param>
		/// <param name="settingValue">Value for the setting</param>
		public void SetSetting(string moduleName, string settingName, string settingValue)
		{
			_settings[GetDictionaryName(moduleName, settingName)] = settingValue;
		}
		#endregion

		static void Save(Dictionary<string, string> settings, string filename)
		{
			SafeFileWriter newFile = new SafeFileWriter(filename);
			using (Stream stream = newFile.OpenForWriting())
			{
				SettingsWriter writer = new SettingsWriter();
				writer.Write(settings, stream);
			}

			newFile.CompleteWrite();
		}

		static Dictionary<string, string> Load(string filename)
		{
			Dictionary<string, string> settings = new Dictionary<string, string>();

			try
			{
				if (File.Exists(filename))
				{
					using (Stream stream = File.Open(filename, FileMode.Open))
					{
						SettingsReader reader = new SettingsReader();
						settings = reader.Read(stream);
					}
				}
			}
			catch (Exception)
			{
			}

			return settings;
		}

		void LoadSettings()
		{
			_settings = Load(GetSettingsFilename());
		}

		string GetDictionaryName(string moduleName, string settingName)
		{
			return moduleName + "__" + settingName;
		}

		string GetSettingsFilename()
		{
			return FileLocations.Instance.SettingsFilename;
		}
	}
}
