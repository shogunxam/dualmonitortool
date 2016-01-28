using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace DMT.Library.Utils
{
	class LatestVersionFile
	{
		string _fileLocation;

		public LatestVersionFile(string fileLocation)
		{
			_fileLocation = fileLocation;
		}

		public LatestVersion GetLatestVersion()
		{
			XmlTextReader reader= null;
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
