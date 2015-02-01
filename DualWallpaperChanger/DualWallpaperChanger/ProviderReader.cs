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

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Xml;

namespace DualMonitorTools.DualWallpaperChanger
{
	/// <summary>
	/// Reads the users providers from an XML formatted stream.
	/// This is the reverse of ProviderWriter.
	/// </summary>
	class ProviderReader
	{
		IProviderFactory _providerFactory;

		public ProviderReader(IProviderFactory providerFactory)
		{
			_providerFactory = providerFactory;
		}


		public Collection<IImageProvider> Read(Stream stream)
		{
			Collection<IImageProvider> providers = new Collection<IImageProvider>();

			XmlTextReader reader = new XmlTextReader(stream);

			string curName = null;
			Dictionary<string, string> curConfig = null;

			while (reader.Read())
			{
				if (reader.NodeType == XmlNodeType.Element)
				{
					bool isEmptyElement = reader.IsEmptyElement;
					if (reader.LocalName == "provider")
					{
						curName = reader.GetAttribute("name");
						curConfig = new Dictionary<string, string>();
					}
					else if (reader.LocalName == "config")
					{
						if (curConfig != null)
						{
							string name = reader.GetAttribute("name");
							string value = reader.GetAttribute("value");
							if (!string.IsNullOrEmpty(name))
							{
								curConfig[name] = value;
							}
						}
					}
				}
				else if (reader.NodeType == XmlNodeType.EndElement)
				{
					if (reader.LocalName == "provider")
					{
						IImageProvider provider = _providerFactory.CreateProvider(curName, curConfig);
						if (provider != null)
						{
							providers.Add(provider);
						}
						curName = null;
						curConfig = null;
					}
				}
			}

			return providers;
		}
	}
}
