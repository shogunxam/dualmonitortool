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

	/// <summary>
	/// Holds information about the latest version of DMT available
	/// </summary>
	class LatestVersion
	{
		/// <summary>
		/// Initialises a new instance of the <see cref="LatestVersion" /> class.
		/// </summary>
		/// <param name="version">Latest version</param>
		/// <param name="downloadPage">Download page</param>
		/// <param name="msiInstaller">Url for msi installer</param>
		/// <param name="zipInstaller">Url for zip package</param>
		public LatestVersion(Version version, string downloadPage, string msiInstaller, string zipInstaller)
		{
			Version = version;
			DownloadPage = downloadPage;
			MsiInstaller = msiInstaller;
			ZipInstaller = zipInstaller;
		}

		/// <summary>
		/// Gets or sets the latest version of DMT
		/// </summary>
		public Version Version { get; protected set; }

		/// <summary>
		/// Gets or sets the download page containing the latest version
		/// </summary>
		public string DownloadPage { get; protected set; }

		/// <summary>
		/// Gets or sets the path to the msi installer
		/// </summary>
		public string MsiInstaller { get; protected set; }

		/// <summary>
		/// Gets or sets the path to the zip package
		/// </summary>
		public string ZipInstaller { get;  protected set; }
	}
}
