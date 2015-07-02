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

using DMT.Library.Settings;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DMT.Modules.WallpaperChanger.Plugins.Flickr
{
	public partial class FlickrForm : Form
	{
		const string HomePageUrl = "www.flickr.com";

		public FlickrForm(FlickrConfig config)
		{
			//_apiKeySetting = apiKeySetting;

			InitializeComponent();

			numericUpDownWeight.Value = config.Weight;
			textBoxDescription.Text = config.Description;
			textBoxTags.Text = config.Tags;
			checkBoxTagMode.Checked = config.TagModeAll;
			textBoxText.Text = config.Text;
			textBoxUserId.Text = config.UserId;
			textBoxGroupId.Text = config.GroupId;
			checkBoxRandomPage.Checked = config.RandomPage;

			//textBoxApiKey.Text = _apiKeySetting.Value;
		}

		private void FlickrForm_Load(object sender, EventArgs e)
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

		public FlickrConfig GetConfig()
		{
			FlickrConfig config = new FlickrConfig();
			config.Weight = (int)numericUpDownWeight.Value;
			config.Description = textBoxDescription.Text;
			config.Tags = textBoxTags.Text;
			config.TagModeAll = checkBoxTagMode.Checked;
			config.Text = textBoxText.Text;
			config.UserId = textBoxUserId.Text;
			config.GroupId = textBoxGroupId.Text;
			config.RandomPage = checkBoxRandomPage.Checked;

			return config;
		}

		private void buttonOK_Click(object sender, EventArgs e)
		{
			//_apiKeySetting.Value = textBoxApiKey.Text;

			DialogResult = DialogResult.OK;
			Close();
		}
	}
}
