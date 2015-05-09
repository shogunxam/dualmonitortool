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

using DMT.Library;
using DMT.Library.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMT.Library.Settings
{
	class SettingsRepository : ISettingsService
	{
		Dictionary<string, string> _settings;

		//#region Singleton support
		//// the single instance of this object
		//static readonly SettingsRepository instance = new SettingsRepository();

		//// Explicit static constructor to tell C# compiler
		//// not to mark type as beforefieldinit
		//static SettingsRepository()
		//{
		//}

		//SettingsRepository()
		//{
		//	LoadSettings();
		//}

		//public static SettingsRepository Instance
		//{
		//	get
		//	{
		//		return instance;
		//	}
		//}
		//#endregion

		public SettingsRepository()
		{
			LoadSettings();
		}

		void LoadSettings()
		{
			_settings = Load(GetSettingsFilename());
		}

		public void SaveSettings()
		{
			Save(_settings, GetSettingsFilename());
		}

		public bool SettingExists(string moduleName, string settingName)
		{
			string value;
			return _settings.TryGetValue(GetDictionaryName(moduleName, settingName), out value);
		}


		#region int settings
		public int GetSettingAsInt(string moduleName, string settingName, int defaultValue = 0)
		{
		
			return StringUtils.ToInt(GetSetting(moduleName, settingName), defaultValue);
		}

		public void SetSetting(string moduleName, string settingName, int value)
		{
			SetSetting(moduleName, settingName, value.ToString());
		}
		#endregion

		#region uint settings
		public uint GetSettingAsUInt(string moduleName, string settingName, uint defaultValue = 0)
		{
			return StringUtils.ToUInt(GetSetting(moduleName, settingName), defaultValue);
		}

		public void SetSetting(string moduleName, string settingName, uint value)
		{
			SetSetting(moduleName, settingName, value.ToString());
		}
		#endregion

		#region bool settings
		public bool GetSettingAsBool(string moduleName, string settingName, bool defaultValue = false)
		{
			return StringUtils.ToBool(GetSetting(moduleName, settingName), defaultValue);
		}

		public void SetSetting(string moduleName, string settingName, bool set)
		{
			SetSetting(moduleName, settingName, set.ToString());
		}
		#endregion

		#region string settings
		public string GetSetting(string moduleName, string settingName)
		{
			string value;
			if (_settings.TryGetValue(GetDictionaryName(moduleName, settingName), out value))
			{
				return value;
			}
			return "";
		}

		public void SetSetting(string moduleName, string settingName, string settingValue)
		{
			_settings[GetDictionaryName(moduleName, settingName)] = settingValue;
		}
		#endregion

		string GetDictionaryName(string moduleName, string settingName)
		{
			return moduleName + "__" + settingName;
		}

		string GetSettingsFilename()
		{
			return DataLocations.Instance.SettingsFilename;

		}

		//static void Save(Dictionary<string, string> settings, string filename)
		//{
		//	using (Stream stream = File.Open(filename, FileMode.Create))
		//	{
		//		SettingsWriter writer = new SettingsWriter();
		//		writer.Write(settings, stream);
		//	}
		//}

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
			Dictionary<string, string> settings = new Dictionary<string,string>();

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
			catch (Exception e)
			{
			}

			return settings;
		}
	}
}
