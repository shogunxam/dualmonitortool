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
using System.Collections.Specialized;
using System.Configuration;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml;

namespace SwapScreen
{
	/// <summary>
	/// A settings provider that allows the settings to be loaded and saved to a configuration
	/// file that lives in the same directory as the executable.
	/// If the configuration file does not exist in the same directory as the executable,
	/// then the standard LocalFileSettingsProvider is used to access the user specific 
	/// configuration file.
	/// This allows the executable and configuration files to live on a portable device
	/// that can be moved between computers and still retain the same settings.
	/// 
	/// This code is based on the article at
	/// http://www.codeproject.com/KB/vb/CustomSettingsProvider.aspx?msg=3268307#xx3268307xx
	/// </summary>
	public class PortableFileSettingsProvider : LocalFileSettingsProvider
	{
		// The portable configuration name should be called: <AppName>.config
		private const string portableFileExtension = ".config";

		// indicates if using a portable configuration file
		private bool usingPortableFile = false;

		public override void Initialize(string name, NameValueCollection values)
		{
			CheckForPortableFile();
			base.Initialize(name, values);
		}

		public override string ApplicationName
		{
			get
			{
				return System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
			}
			set {}
		}

		public override string Description
		{
			get
			{
				return "Portable File Settings";
			}
		}

		public override string Name
		{
			get
			{
				return "GnePortableFileSettingsProvider";
			}
		}

		public override SettingsPropertyValueCollection GetPropertyValues(SettingsContext context, SettingsPropertyCollection properties)
		{
			if (usingPortableFile)
			{
				PortableFileSettings portableFile = new PortableFileSettings(ApplicationName);
				// allow exceptions to be passed to the caller
				portableFile.Load(GetPortableFileName());

				SettingsPropertyValueCollection values = new SettingsPropertyValueCollection();
				foreach (SettingsProperty property in properties)
				{
					SettingsPropertyValue value = new SettingsPropertyValue(property);
					// TODO - check this
					value.IsDirty = false;
					value.SerializedValue = GetPortableValue(portableFile, property);
					values.Add(value);
				}

				return values;
			}
			else
			{
				return base.GetPropertyValues(context, properties);
			}
		}

		public override void SetPropertyValues(SettingsContext context, SettingsPropertyValueCollection values)
		{
			if (usingPortableFile)
			{
				PortableFileSettings portableFile = new PortableFileSettings(ApplicationName);
				// allow exceptions to be passed to the caller
				portableFile.Load(GetPortableFileName());

				foreach (SettingsPropertyValue value in values)
				{
					SetPortableValue(portableFile, value);
				}
				try
				{
					portableFile.Save(GetPortableFileName());
				}
				catch (Exception)
				{
				}
			}
			else
			{
				base.SetPropertyValues(context, values);
			}
		}

		// Cannot override Upgrade()
		//public override void Upgrade(SettingsContext context, SettingsPropertyCollection properties)
		//{
		//    base.Upgrade(context, properties);
		//}

		/// <summary>
		/// Checks is a portable file exists
		/// </summary>
		private void CheckForPortableFile()
		{
			usingPortableFile = File.Exists(GetPortableFileName());
		}

		/// <summary>
		/// Gets the full pathname to where the portable file would live
		/// </summary>
		/// <returns></returns>
		private string GetPortableFileName()
		{
			return Path.ChangeExtension(Application.ExecutablePath, portableFileExtension);
		}

		private string GetPortableValue(PortableFileSettings portableFile, SettingsProperty property)
		{
			string settingValue = portableFile.GetSetting(property.Name);
			if (settingValue == null)
			{
				// property not found in portable file, so return default value
				if (property.DefaultValue != null)
				{
					settingValue = property.DefaultValue.ToString();
				}
				else
				{
					// not in portable file and no default value
					settingValue = "";
				}
			}

			return settingValue;
		}

		private void SetPortableValue(PortableFileSettings portableFile, SettingsPropertyValue value)
		{
			portableFile.SetSetting(value.Name, value.SerializedValue.ToString());
		}
	}
}
