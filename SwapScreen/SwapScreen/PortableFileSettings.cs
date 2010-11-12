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
using System.Text;
using System.Xml;

namespace SwapScreen
{
	/// <summary>
	/// This is the XML file used to store the settings to the portable file.
	/// This is used by PortableFileSettingsProvider.
	/// The XML file format is the same as that used by LocalFileSettingsProvider,
	/// although there is no need to use the same format.
	/// 
	/// TODO: put the strings into constants at the top of the code
	/// </summary>
	public class PortableFileSettings
	{
		// XML DOM
		private XmlDocument portableFile;
		// application name is needed as one of the XML elements uses it
		private string appName;

		/// <summary>
		/// ctor creates an empty XML DOM and remembers the application name
		/// </summary>
		/// <param name="appName">Application name</param>
		public PortableFileSettings(string appName)
		{
			this.appName = appName;
			portableFile = new XmlDocument();
		}

		/// <summary>
		/// Tries to load the existing settings file into the XML DOM.
		/// If it fails (due to the XML being invalid - eg. an empty file)
		/// then a skeleton XML DOM is created.
		/// </summary>
		/// <param name="filename"></param>
		public void Load(string filename)
		{
			try
			{
				portableFile.Load(filename);
			}
			catch (Exception)
			{
				// if there is any problem loading the settings
				// (eg. the file may be empty)
				// then we create a skeleton xml document
				Create();
			}
		}

		/// <summary>
		/// Saves the XML DOM to the specified filename.
		/// </summary>
		/// <param name="filename"></param>
		public void Save(string filename)
		{
			portableFile.Save(filename);
		}

		/// <summary>
		/// Creates a skeleton DOM
		/// </summary>
		private void Create()
		{
			// make sure we start with an empty document
			portableFile = new XmlDocument();

			XmlDeclaration xmlDeclaration = portableFile.CreateXmlDeclaration("1.0", "utf-8", null);
			portableFile.AppendChild(xmlDeclaration);

			// create the elements
			XmlElement rootNode = portableFile.CreateElement("configuration");
			XmlElement userSettings = portableFile.CreateElement("userSettings");
			XmlElement properties = portableFile.CreateElement(appName + ".Properties.Settings");

			// and add them
			userSettings.AppendChild(properties);
			rootNode.AppendChild(userSettings);
			portableFile.AppendChild(rootNode);
		}

		/// <summary>
		/// Gets the current value for the given settings name.
		/// </summary>
		/// <param name="name">Settings name</param>
		/// <returns>Settings value, or null if settings name not found</returns>
		public string GetSetting(string name)
		{
			XmlNode xmlNode = portableFile.SelectSingleNode(GetXPath(name));
			if (xmlNode != null)
			{
				return xmlNode.InnerText;
			}

			return null;
		}

		/// <summary>
		/// Set the current value for the given settings name.
		/// </summary>
		/// <param name="name">Settings name</param>
		/// <param name="value">Settings value</param>
		public void SetSetting(string name, string value)
		{
			XmlNode xmlNode = portableFile.SelectSingleNode(GetXPath(name));
			if (xmlNode != null)
			{
				// setting already exists - need to change value
				xmlNode.InnerText = value;
			}
			else
			{
				// setting doesn't exist so need to add it
				// This assumes that the parent xml elements already exists!
				XmlElement settingNode = portableFile.CreateElement("setting");
				settingNode.SetAttribute("name", name);
				// TODO: do we need to handle anything other than string?
				settingNode.SetAttribute("serializeAs", "String");

				XmlElement valueNode = portableFile.CreateElement("value");
				valueNode.InnerText = value;

				settingNode.AppendChild(valueNode);

				GetSettingsParentNode().AppendChild(settingNode);
			}
		}

		/// <summary>
		/// Get the full XPath to the given setting
		/// </summary>
		/// <param name="settingName">Settings name</param>
		/// <returns>full XPath to settings name</returns>
		private string GetXPath(string settingName)
		{
			return GetSettingsParentPath() + "/setting[@name='" + settingName + "']/value";
		}

		/// <summary>
		/// Gets the node that is the parent of the individual setting's elements
		/// </summary>
		/// <returns></returns>
		private XmlNode GetSettingsParentNode()
		{
			XmlNode node = portableFile.SelectSingleNode(GetSettingsParentPath());

			if (node == null)
			{
				// TODO: should we force the document structure to be created?
				Create();
				node = portableFile.SelectSingleNode(GetSettingsParentPath());
			}

			return node;
		}

		/// <summary>
		/// Gets the full XPath to the parent of the individual setting's elements
		/// </summary>
		/// <returns></returns>
		private string GetSettingsParentPath()
		{
			return "/configuration/userSettings/" + appName + ".Properties.Settings";
		}
	}
}
