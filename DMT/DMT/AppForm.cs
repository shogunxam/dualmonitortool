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
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
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

		public AppForm()
		{
			InitializeComponent();

			InitContextMenu();

			StartController();
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

		private void exitToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ShutDown();
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

		void ShowOptions()
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
		}
	}
}
