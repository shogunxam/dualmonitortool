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

namespace DMT
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel;
	using System.Data;
	using System.Drawing;
	using System.Linq;
	using System.Runtime.InteropServices;
	using System.Text;
	using System.Threading.Tasks;
	using System.Windows.Forms;

	using DMT.Library.Command;
	using DMT.Library.Logging;
	using DMT.Library.PInvoke;
	using DMT.Resources;

	using Microsoft.Win32;

	/// <summary>
	/// Main (hidden) window.
	/// Handles notification icon/events
	/// </summary>
	public partial class AppForm : Form
	{
		Controller _controller = null;
		OptionsForm _optionsForm = null;
		AboutForm _aboutForm = null;

		/// <summary>
		/// Initialises a new instance of the <see cref="AppForm" /> class.
		/// </summary>
		public AppForm()
		{
			InitializeComponent();

			InitContextMenu();

			StartController();

			// finish off the menu
			AddMenuItem("About", null, aboutToolStripMenuItem_Click);
			AddMenuItem("Visit Website", null, visitWebSiteToolStripMenuItem_Click);
			AddMenuItem("-", null, null);
			AddMenuItem("Exit", null, exitToolStripMenuItem_Click);

			// handle system events
			SystemEvents.SessionEnding += new SessionEndingEventHandler(SystemEvents_SessionEnding);
			SystemEvents.SessionEnded += new SessionEndedEventHandler(SystemEvents_SessionEnded);
			SystemEvents.DisplaySettingsChanged += new EventHandler(SystemEvents_DisplaySettingsChanged);
		}

		/// <summary>
		/// Adds an item to the menu used in the notification area
		/// </summary>
		/// <param name="text">menu item text</param>
		/// <param name="image">image (null if not needed)</param>
		/// <param name="eventHandler">handler for when menu item clicked</param>
		/// <returns>The created menu item</returns>
		public ToolStripMenuItem AddMenuItem(string text, Image image, EventHandler eventHandler)
		{
			return contextMenuStrip.Items.Add(text, image, eventHandler) as ToolStripMenuItem;
		}

		/// <summary>
		/// Shows the options dialog
		/// </summary>
		public void ShowOptions()
		{
			if (_optionsForm != null && _optionsForm.Visible)
			{
				_optionsForm.Activate();
			}
			else
			{
				_optionsForm = _controller.CreateOptionsForm();
				_optionsForm.ShowDialog();
			}
		}

		/// <summary>
		/// Called on first run to show a balloon tip
		/// so that the user can see that the app has been added to the notification area
		/// </summary>
		public void ShowFirstRun()
		{
			notifyIcon.BalloonTipTitle = AppStrings.FirstRunTitle;
			notifyIcon.BalloonTipText = AppStrings.FirstRunText;
			notifyIcon.BalloonTipIcon = ToolTipIcon.Info;
			notifyIcon.ShowBalloonTip(5 * 1000);
		}

		/// <summary>
		/// Window procedure
		/// We use this to detect requests from command line instances 
		/// that want to run a command.
		/// </summary>
		/// <param name="m">Windows message</param>
		protected override void WndProc(ref Message m)
		{
			if (m.Msg == NativeMethods.WM_COPYDATA)
			{
				NativeMethods.COPYDATASTRUCT cds = (NativeMethods.COPYDATASTRUCT)m.GetLParam(typeof(NativeMethods.COPYDATASTRUCT));

				if (cds.dwData == (IntPtr)CommandMessaging.DmtCommandMessage)
				{
					string command = Marshal.PtrToStringAnsi(cds.lpData);
					string parameters = null;

					// if there is a space in the command, the first such space separates the command from the parameters
					int index = command.IndexOf(' ');
					if (index > 0)
					{
						parameters = command.Substring(index + 1).Trim();
						command = command.Substring(0, index);
					}

					if (!_controller.RunInternalCommand(command, parameters))
					{
						// indicate an error
						m.Result = (IntPtr)1;
					}
				}
			}
			else
			{
				base.WndProc(ref m);
			}
		}

		void SystemEvents_SessionEnding(object sender, SessionEndingEventArgs e)
		{
			LogInfo("SessionEnding - in");

			if (_controller != null)
			{
				_controller.Flush();
			}

			LogInfo("SessionEnding - out");
		}

		void SystemEvents_SessionEnded(object sender, SessionEndedEventArgs e)
		{
			LogInfo("SessionEnded - in");
			CleanUp();
			LogInfo("SessionEnded - out");
		}

		void SystemEvents_DisplaySettingsChanged(object sender, EventArgs e)
		{
			if (_controller != null)
			{
				_controller.DisplayResolutionChanged();
			}
		}

		void StartController()
		{
			_controller = new Controller(this);
			_controller.Start();
		}

		private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ShowOptions();
		}

		private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ShowAbout();
		}

		private void visitWebSiteToolStripMenuItem_Click(object sender, EventArgs e)
		{
			VisitWebsite();
		}

		private void exitToolStripMenuItem_Click(object sender, EventArgs e)
		{
			LogInfo("exitToolStripMenuItem_Click");
			//ShutDown();
			this.Close();
		}

		private void AppForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			LogInfo("AppForm_FormClosing");
			ShutDown();
		}

		// dynamically add any needed menu items to the context menu
		void InitContextMenu()
		{
			// add a 'Show Desktop n' for each known screen
			for (int screenIndex = 0; screenIndex < Screen.AllScreens.Length; screenIndex++)
			{
				// This is no longer needed?
				////AddShowDesktopMenuItem(screenIndex);
			}
		}

		private void notifyIcon_DoubleClick(object sender, EventArgs e)
		{
			ShowOptions();
		}

		void VisitWebsite()
		{
			try
			{
				System.Diagnostics.Process.Start("http://dualmonitortool.sourceforge.net");
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, CommonStrings.MyTitle);
			}
		}

		void ShowAbout()
		{
			if (_aboutForm != null && _aboutForm.Visible)
			{
				_aboutForm.Activate();
			}
			else
			{
				_aboutForm = new AboutForm();
				_aboutForm.ShowDialog();
			}
		}

		void ShutDown()
		{
			CleanUp();
			//this.Close();
			Application.Exit();
		}

		// This is what we do just before we exit
		void CleanUp()
		{
			if (_controller != null)
			{
				_controller.Stop();
			}

			// is this really necessary if called from SessionEnded ?
			SystemEvents.DisplaySettingsChanged -= new EventHandler(SystemEvents_DisplaySettingsChanged);
			SystemEvents.SessionEnding -= new SessionEndingEventHandler(SystemEvents_SessionEnding);
			SystemEvents.SessionEnded -= new SessionEndedEventHandler(SystemEvents_SessionEnded);
		}

		void LogInfo(string format, params object[] formatParams)
		{
			ILogger logger = null;
			logger = new Logger();
			logger.LogInfo("AppForm", format, formatParams);
		}
	}
}
