﻿#region copyright
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

using DMT.Library.Command;
using DMT.Library.PInvoke;
using DMT.Resources;
using Microsoft.Win32;
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

namespace DMT
{
	public partial class AppForm : Form
	{
		Controller _controller;
		OptionsForm _optionsForm = null;
		AboutForm _aboutForm = null;
		//uint _commandMessage;

		public AppForm()
		{
			InitializeComponent();

			//// register message to receive commands
			//CommandMessaging commandMessaging = new CommandMessaging();
			//_commandMessage = commandMessaging.GetCommandMessage();

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

		void SystemEvents_SessionEnding(object sender, SessionEndingEventArgs e)
		{
			_controller.Logger.LogInfo("AppForm", "SessionEnding - in");
			_controller.Flush();
			_controller.Logger.LogInfo("AppForm", "SessionEnding - out");
		}

		void SystemEvents_SessionEnded(object sender, SessionEndedEventArgs e)
		{
			_controller.Logger.LogInfo("AppForm", "SessionEnded - in");
			CleanUp();
			_controller.Logger.LogInfo("AppForm", "SessionEnded - out");
		}

		void SystemEvents_DisplaySettingsChanged(object sender, EventArgs e)
		{
			if (_controller != null)
			{
				_controller.DisplayResolutionChanged();
			}
		}

		public ToolStripMenuItem AddMenuItem(string text, Image image, EventHandler eventHandler)
		{
			return contextMenuStrip.Items.Add(text, image, eventHandler) as ToolStripMenuItem;
		}

		void StartController()
		{
			_controller = new Controller(this);
			_controller.Start();
		}


		protected override void WndProc(ref Message m)
		{
			//if (m.Msg == _commandMessage)
			//{
			//	string command = Marshal.PtrToStringUni(m.LParam);
			//	string parameters = null;
			//	_controller.RunInternalCommand(command, parameters);

			//}
			if (m.Msg == Win32.WM_COPYDATA)
			{
				Win32.COPYDATASTRUCT cds = (Win32.COPYDATASTRUCT)m.GetLParam(typeof(Win32.COPYDATASTRUCT));

				if (cds.dwData == (IntPtr)CommandMessaging.DmtCommandMessage)
				{
					string command = Marshal.PtrToStringAnsi(cds.lpData);
					string parameters = null;
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
			ShutDown();
		}


		private void AppForm_FormClosing(object sender, FormClosingEventArgs e)
		{

		}


		// dynamically add any needed menu items to the context menu
		void InitContextMenu()
		{
			// add a 'Show Desktop n' for each known screen
			for (int screenIndex = 0; screenIndex < Screen.AllScreens.Length; screenIndex++)
			{
				//AddShowDesktopMenuItem(screenIndex);
			}
		}

		private void notifyIcon_DoubleClick(object sender, EventArgs e)
		{
			ShowOptions();
		}

		public void ShowOptions()
		{
			if (_optionsForm != null && _optionsForm.Visible)
			{
				_optionsForm.Activate();
			}
			else
			{
				//_optionsForm = new OptionsForm();
				_optionsForm = _controller.CreateOptionsForm();
				_optionsForm.ShowDialog();
			}
		}

		public void ShowFirstRun()
		{
			notifyIcon.BalloonTipTitle = AppStrings.FirstRunTitle;
			notifyIcon.BalloonTipText = AppStrings.FirstRunText;
			notifyIcon.BalloonTipIcon = ToolTipIcon.Info;
			notifyIcon.ShowBalloonTip(5 * 1000);
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
			this.Close();
			Application.Exit();
		}

		// This is what we do just before we exit
		void CleanUp()
		{
			_controller.Stop();

			// is this really necessary if called from SessionEnded ?
			SystemEvents.DisplaySettingsChanged -= new EventHandler(SystemEvents_DisplaySettingsChanged);
			SystemEvents.SessionEnding -= new SessionEndingEventHandler( SystemEvents_SessionEnding);
			SystemEvents.SessionEnded -= new SessionEndedEventHandler(SystemEvents_SessionEnded);
		}
	}
}
