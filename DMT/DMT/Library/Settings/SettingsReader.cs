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
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace DMT.Library.Settings
{
	class SettingsReader
	{
		public Dictionary<string, string> Read(Stream stream)
		{
			Dictionary<string, string> settings = new Dictionary<string, string>();

			XmlTextReader reader = new XmlTextReader(stream);

			while (reader.Read())
			{
				if (reader.NodeType == XmlNodeType.Element)
				{
					bool isEmptyElement = reader.IsEmptyElement;
					if (reader.LocalName == "setting")
					{
						string name = reader.GetAttribute("name");
						string value = reader.GetAttribute("value");
						settings[name] = value;
					}
				}
			}

			return settings;
		}
	}
}
