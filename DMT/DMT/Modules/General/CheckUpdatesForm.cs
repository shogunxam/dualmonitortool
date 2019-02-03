#region copyright
// This file is part of Dual Monitor Tools which is a set of tools to assist
// users with multiple monitor setups.
// Copyright (C) 2016  Gerald Evans
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

namespace DMT.Modules.General
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel;
	using System.Data;
	using System.Diagnostics;
	using System.Drawing;
	using System.IO;
	using System.Linq;
	using System.Net;
	using System.Text;
	using System.Threading;
	using System.Windows.Forms;

	using DMT.Library.Html;
	using DMT.Library.Utils;
	using Library.GuiUtils;

	/// <summary>
	/// Dialog used when checking if a later version of DMT is available
	/// </summary>
	partial class CheckUpdatesForm : Form
	{
		const string LatestVersionFilename = "http://dualmonitortool.sourceforge.net/LatestVersion.xml";

		GeneralModule _generalModule;

		LatestVersion _latestVersion;

		/// <summary>
		/// Initialises a new instance of the <see cref="Controller" /> class.
		/// </summary>
		/// <param name="generalModule">General module</param>
		public CheckUpdatesForm(GeneralModule generalModule)
		{
			_generalModule = generalModule;

			InitializeComponent();

			// hide the download + install buttons for now
			buttonOpenDownload.Visible = false;
			buttonInstall.Visible = false;

			StartUpdateCheck();
		}

		// delegate for when we have the atest version
		delegate void ThreadCompletedDelegate(bool ok, string errMsg);

		void StartUpdateCheck()
		{
			Thread t = new Thread(new ThreadStart(GetLatestVersionThread));
			t.IsBackground = true;
			t.Start();
		}

		void GetLatestVersionThread()
		{
			bool ok = true;
			string errMsg = null;

			try
			{
				string url = LatestVersionFilename;

				// stop this from being cached
				url += "?_=" + DateTime.Now.Ticks.ToString();
				LatestVersionFile laestVersionFile = new LatestVersionFile(url);
				_latestVersion = laestVersionFile.GetLatestVersion();
			}
			catch (Exception ex)
			{
				ok = false;
				errMsg = ex.Message;
			}

			// inform the UI that we have the latest version
			GotLatestVersionEvent(ok, errMsg);
		}

		void GotLatestVersionEvent(bool ok, string errMsg)
		{
			if (InvokeRequired)
			{
				BeginInvoke(new ThreadCompletedDelegate(GotLatestVersionEvent), new object[] { ok, errMsg });
				return;
			}

			if (ok)
			{
				if (_latestVersion.Version > _generalModule.Version)
				{
					labelStatus.Text = string.Format(Resources.GeneralStrings.LatestVersionAvailable, _latestVersion.Version.ToString());

					// show button to visit the download page
					buttonOpenDownload.Visible = true;

					// if we were installed with msi, allow the user to upgrade using msi
					if (_generalModule.IsMsiInstall)
					{
						buttonInstall.Visible = true;
					}
				}
				else
				{
					labelStatus.Text = Resources.GeneralStrings.LatestVersionHave;
				}
			}
			else
			{
				labelStatus.Text = string.Format(Resources.GeneralStrings.LatestVersionUnavailable, errMsg);
			}

			// change cancel button text from "Cancel" to "Close"
			buttonCancel.Text = Resources.GeneralStrings.CloseButtonText;
		}

		private void buttonOpenDownload_Click(object sender, EventArgs e)
		{
			if (_latestVersion != null)
			{
				if (!string.IsNullOrEmpty(_latestVersion.DownloadPage))
				{
					try
					{
						System.Diagnostics.Process.Start(_latestVersion.DownloadPage);
					}
					catch (Exception ex)
					{
						MsgDlg.Error(ex.Message);
					}
				}
			}
		}

		private void buttonInstall_Click(object sender, EventArgs e)
		{
			if (_latestVersion != null)
			{
				if (_generalModule.IsMsiInstall)
				{
					if (!string.IsNullOrEmpty(_latestVersion.MsiInstaller))
					{
						StartInstallNewVersion();
					}
				}
			}
		}

		private void buttonCancel_Click(object sender, EventArgs e)
		{
		}

		void StartInstallNewVersion()
		{
			Thread t = new Thread(new ThreadStart(InstallNewVersionThread));
			t.IsBackground = true;
			t.Start();
		}

		void InstallNewVersionThread()
		{
			bool ok = true;
			string errMsg = null;

			try
			{
				IHttpConnectionManager connectionManager = new HttpConnectionManager();
				IHttpRequester httpRequester = new HttpRequester(connectionManager);

				// get the file
				Uri uri = new Uri(_latestVersion.MsiInstaller);
				byte[] msiFile = null;
				HttpStatusCode httpStatusCode = httpRequester.GetData(uri, ref msiFile);
				if (httpStatusCode == HttpStatusCode.OK)
				{
					// save it to disk
					string msiFileName = _generalModule.TempMsiInstallPath;
					File.WriteAllBytes(msiFileName, msiFile);

					// run it
					Process.Start(msiFileName);
				}
				else
				{
					ok = false;

					// This requires system.web from the full version of .Net 4.0
					// errMsg = HttpWorkerRequest.GetStatusDescription(httpStatusCode);
					// for time being, just use enum name as msg
					errMsg = httpStatusCode.ToString();
				}
			}
			catch (Exception ex)
			{
				ok = false;
				errMsg = ex.Message;
			}

			// inform the UI that we have the latest version, or that there was an error
			InstallingNewVersionEvent(ok, errMsg);
		}

		void InstallingNewVersionEvent(bool ok, string errMsg)
		{
			if (InvokeRequired)
			{
				BeginInvoke(new ThreadCompletedDelegate(InstallingNewVersionEvent), new object[] { ok, errMsg });
				return;
			}

			if (ok)
			{
				// msi installer should be running now,
				// so shut ourself down so we can be updated
				_generalModule.StartShutdown();
			}
			else
			{
				labelStatus.Text = string.Format(Resources.GeneralStrings.LatestVersionUnavailable, errMsg);
			}
		}
	}
}
