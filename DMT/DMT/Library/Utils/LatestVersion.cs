using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DMT.Library.Utils
{
	class LatestVersion
	{
		string _fileLocation;

		public Version Version { get; protected set; }
		public string DownloadPage { get; protected set; }
		public string MsiInstaller { get; protected set; }
		public string ZipInstaller { get;  protected set; }

		public LatestVersion(Version version, string downloadPage, string msiInstaller, string zipInstaller)
		{
			Version = version;
			DownloadPage = downloadPage;
			MsiInstaller = msiInstaller;
			ZipInstaller = zipInstaller;
		}
	}
}
