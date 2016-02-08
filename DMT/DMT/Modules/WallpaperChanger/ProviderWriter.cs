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

namespace DMT.Modules.WallpaperChanger
{
	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.IO;
	using System.Text;
	using System.Xml;

	using DMT.Library.Wallpaper;
	using DMT.Library.WallpaperPlugin;

	/// <summary>
	/// Saves the users providers to a stream in XML format
	/// This is the reverse of ProviderReader.
	/// </summary>
	class ProviderWriter
	{
		XmlWriter _writer;

		/// <summary>
		/// Writes the providers to a stream
		/// </summary>
		/// <param name="providers">Providers to write</param>
		/// <param name="stream">Stream to write them to</param>
		public void Write(Collection<IImageProvider> providers, Stream stream)
		{
			XmlWriterSettings settings = new XmlWriterSettings();
			settings.Indent = true;
			settings.IndentChars = "\t";

			_writer = XmlWriter.Create(stream, settings);
			_writer.WriteStartDocument();

			WriteProviders(providers);

			_writer.Flush();
			_writer.Close();
			_writer = null;
		}

		void WriteProviders(Collection<IImageProvider> providers)
		{
			_writer.WriteStartElement("providers");

			foreach (IImageProvider provider in providers)
			{
				WriteProvider(provider);
			}

			_writer.WriteEndElement();
		}

		void WriteProvider(IImageProvider provider)
		{
			_writer.WriteStartElement("provider");
			_writer.WriteAttributeString("name", provider.ProviderName);

			foreach (KeyValuePair<string, string> kvp in provider.Config)
			{
				_writer.WriteStartElement("config");
				_writer.WriteAttributeString("name", kvp.Key);
				_writer.WriteAttributeString("value", kvp.Value);
				_writer.WriteEndElement();
			}

			_writer.WriteEndElement();
		}
	}
}
