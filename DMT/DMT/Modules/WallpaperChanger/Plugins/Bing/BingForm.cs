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
namespace DMT.Modules.WallpaperChanger.Plugins.Bing
{
	using System;
	using System.Diagnostics;
	using System.Windows.Forms;


	/// <summary>
	/// Configuration form for a single provider from the Bing plugin
	/// </summary>
	public partial class BingForm : Form
	{
		const string HomePageUrl = "www.bing.com";

		public BingForm(BingConfig config)
		{
			InitializeComponent();

			checkBoxEnabled.Checked = config.Enabled;
			numericUpDownWeight.Value = config.Weight;
			textBoxDescription.Text = config.Description;
			bingLocale.SelectedIndex = indexOfMarket(config.Market);
		}

		private void BingForm_Load(object sender, EventArgs e)
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

		public BingConfig GetConfig()
		{
			BingConfig config = new BingConfig();
			config.Enabled = checkBoxEnabled.Checked;
			config.Weight = (int)numericUpDownWeight.Value;
			config.Description = textBoxDescription.Text;
			config.Market = bingLocale.SelectedValue.ToString();

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

	}
}
