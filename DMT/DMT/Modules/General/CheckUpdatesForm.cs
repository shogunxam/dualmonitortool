using DMT.Library.Html;
using DMT.Library.Utils;
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

namespace DMT.Modules.General
{
	partial class CheckUpdatesForm : Form
	{
		const string LatestVersionFilename = "http://dualmonitortool.sourceforge.net/LatestVersion.xml";

		GeneralModule _generalModule;

		LatestVersion _latestVersion;

		// delegate for when we have the atest version
		delegate void ThreadCompletedDelegate(bool ok, string errMsg);


		public CheckUpdatesForm(GeneralModule generalModule)
		{
			_generalModule = generalModule;

			InitializeComponent();

			// hide the download + install buttons for now
			buttonOpenDownload.Visible = false;
			buttonInstall.Visible = false;

			StartUpdateCheck();
		}

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
				labelStatus.Text = String.Format(Resources.GeneralStrings.LatestVersionUnavailable, errMsg);
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
						MessageBox.Show(ex.Message, Resources.CommonStrings.MyTitle);
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
						//try
						//{
						//	InstallNewVersion();
						//}
						//catch (Exception ex)
						//{
						//	MessageBox.Show(ex.Message, Resources.CommonStrings.MyTitle);
						//}
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
					//errMsg = HttpWorkerRequest.GetStatusDescription(httpStatusCode);
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
				labelStatus.Text = String.Format(Resources.GeneralStrings.LatestVersionUnavailable, errMsg);
			}
		}
	}
}
