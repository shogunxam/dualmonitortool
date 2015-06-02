using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace DMT.Library.Settings
{
	public class SettingsWriter
	{
		XmlWriter _writer;

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
