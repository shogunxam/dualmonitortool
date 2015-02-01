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
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DualMonitorTools.DualWallpaperChanger.Unsplash
{
	/// <summary>
	/// Configuration form for a single provider from the unsplash plugin
	/// </summary>
	public partial class WebScrapeForm : Form
	{
		const string HomePageUrl = "www.unsplash.com";

		public WebScrapeForm(WebScrapeConfig config)
		{
			InitializeComponent();

			numericUpDownWeight.Value = config.Weight;
			textBoxDescription.Text = config.Description;
			checkBoxFirstPageOnly.Checked = config.FirstPageOnly;
		}

		private void WebScrapeForm_Load(object sender, EventArgs e)
		{
			int startOfLink = linkLabel.Text.Length;
			linkLabel.Text += HomePageUrl;
			linkLabel.Links.Add(startOfLink, HomePageUrl.Length, HomePageUrl);
		}

		private void linkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			ProcessStartInfo sInfo = new ProcessStartInfo(e.Link.LinkData.ToString());
			Process.Start(sInfo);
		}

		public WebScrapeConfig GetConfig()
		{
			WebScrapeConfig config = new WebScrapeConfig();
			config.Weight = (int)numericUpDownWeight.Value;
			config.Description = textBoxDescription.Text;
			config.FirstPageOnly = checkBoxFirstPageOnly.Checked;

			return config;
		}

		private void buttonOK_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.OK;
			Close();
		}

		private void buttonCancel_Click(object sender, EventArgs e)
		{
		}

		private void buttonAbout_Click(object sender, EventArgs e)
		{
			AboutPluginForm dlg = new AboutPluginForm(Assembly.GetExecutingAssembly(), WebScrapePlugin.PluginImage);
			dlg.ShowDialog(this);
		}

	}
}
