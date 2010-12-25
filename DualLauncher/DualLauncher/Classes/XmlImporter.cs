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
using System.Collections.ObjectModel;
using System.Text;
using System.Xml.Serialization;
using System.IO;

namespace DualLauncher
{
	/// <summary>
	/// Native import/export of DualLauncher files
	/// </summary>
	public class XmlImporter
	{
		/// <summary>
		/// Static method to read the MagicWords from an xml file
		/// </summary>
		/// <param name="filename"></param>
		/// <returns></returns>
		public static Collection<MagicWord> Import(string filename)
		{
			Collection<MagicWord> magicWords = new Collection<MagicWord>();

			using (StreamReader streamReader = new StreamReader(filename))
			{
				XmlSerializer xmlSerializer = new XmlSerializer(typeof(Collection<MagicWord>));
				magicWords = (Collection<MagicWord>)xmlSerializer.Deserialize(streamReader);
			}

			return magicWords;
		}

		/// <summary>
		/// Static method to save MagicWords to an xml file
		/// </summary>
		/// <param name="magicWords"></param>
		/// <param name="filename"></param>
		public static void Export(Collection<MagicWord> magicWords, string filename)
		{
			using (StreamWriter streamWriter = new StreamWriter(filename))
			{
				XmlSerializer xmlSerializer = new XmlSerializer(typeof(Collection<MagicWord>));
				xmlSerializer.Serialize(streamWriter, magicWords);
			}
		}
	}
}
