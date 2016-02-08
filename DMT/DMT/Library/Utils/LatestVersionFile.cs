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

namespace DMT.Library.Utils
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using System.Xml;

	/// <summary>
	/// The file containing the information about the latest version of DMT
	/// </summary>
	class LatestVersionFile
	{
		string _fileLocation;

		/// <summary>
		/// Initialises a new instance of the <see cref="LatestVersionFile" /> class.
		/// </summary>
		/// <param name="fileLocation">Path to file containing information about the latest version</param>
		public LatestVersionFile(string fileLocation)
		{
			_fileLocation = fileLocation;
		}

		/// <summary>
		/// Gets the file, parses it and returns the latest version information
		/// </summary>
		/// <returns>Information on latest version</returns>
		public LatestVersion GetLatestVersion()
		{
			XmlTextReader reader = null;
			string curElementName = null;
			Version version = null;
			string downloadPage = null;
			string msiInstaller = null;
			string zipInstaller = null;

			try
			{
				reader = new XmlTextReader(_fileLocation);
				reader.MoveToContent();
				while (reader.Read())
				{
					if (reader.NodeType == XmlNodeType.Element)
					{
						curElementName = reader.Name;
					}
					else if (reader.NodeType == XmlNodeType.EndElement)
					{
						curElementName = null;
					}
					else if (reader.NodeType == XmlNodeType.Text)
					{
						switch (curElementName)
						{
							case "version":
								version = new Version(reader.Value);
								break;
							case "downloadpage":
								downloadPage = reader.Value;
								break;
							case "msiinstaller":
								msiInstaller = reader.Value;
								break;
							case "zipinstaller":
								zipInstaller = reader.Value;
								break;
							default:
								break;
						}
					}
				}
			}
			catch (Exception)
			{
				throw;
			}
			finally
			{
				if (reader != null)
				{
					reader.Close();
				}
			}

			return new LatestVersion(version, downloadPage, msiInstaller, zipInstaller);
		}
	}
}
