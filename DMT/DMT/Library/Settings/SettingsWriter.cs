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
	using System.Xml;

	/// <summary>
	/// Writes settings to a stream
	/// </summary>
	class SettingsWriter
	{
		XmlWriter _writer;

		/// <summary>
		/// Writes the supplied settings to a stream
		/// </summary>
		/// <param name="settings">The settings to be written</param>
		/// <param name="stream">The stream to write them to</param>
		public void Write(Dictionary<string, string> settings, Stream stream)
		{
			XmlWriterSettings writerSettings = new XmlWriterSettings();
			writerSettings.Indent = true;
			writerSettings.IndentChars = "\t";

			_writer = XmlWriter.Create(stream, writerSettings);
			_writer.WriteStartDocument();

			WriteSettings(settings);

			_writer.Flush();
			_writer.Close();
			_writer = null;
		}

		void WriteSettings(Dictionary<string, string> settings)
		{
			_writer.WriteStartElement("settings");

			foreach (KeyValuePair<string, string> setting in settings)
			{
				WriteSetting(setting);
			}

			_writer.WriteEndElement();
		}

		void WriteSetting(KeyValuePair<string, string> setting)
		{
			_writer.WriteStartElement("setting");
			_writer.WriteAttributeString("name", setting.Key);
			_writer.WriteAttributeString("value", setting.Value);
			_writer.WriteEndElement();
		}
	}
}
