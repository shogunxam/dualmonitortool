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

namespace DMT.Modules.WallpaperChanger.Plugins.Flickr
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel;
	using System.Data;
	using System.Diagnostics;
	using System.Drawing;
	using System.Linq;
	using System.Text;
	using System.Windows.Forms;

	using DMT.Library.Settings;
	using Library.GuiUtils;

	/// <summary>
	/// This is used to edit the configuration of a Flickr provider.
	/// </summary>
	partial class FlickrForm : Form
	{
		const string HomePageUrl = "www.flickr.com";
		FlickrProvider _flickrProvider;

		/// <summary>
		/// Initialises a new instance of the <see cref="FlickrForm" /> class.
		/// </summary>
		/// <param name="config">Configuration to be edited</param>
		/// <param name="flickrProvider">Provider we are editing the configuration for</param>
		public FlickrForm(FlickrConfig config, FlickrProvider flickrProvider)
		{
			_flickrProvider = flickrProvider;

			InitializeComponent();

			checkBoxEnabled.Checked = config.Enabled;
			numericUpDownWeight.Value = config.Weight;
			textBoxDescription.Text = config.Description;
			textBoxTags.Text = config.Tags;
			checkBoxTagMode.Checked = config.TagModeAll;
			textBoxText.Text = config.Text;
			textBoxUserId.Text = config.UserId;
			textBoxGroupId.Text = config.GroupId;
			checkBoxRandomPage.Checked = config.RandomPage;
		}

		/// <summary>
		/// Gets the current (possibly edited) configuration
		/// </summary>
		/// <returns>Edited configuration</returns>
		public FlickrConfig GetConfig()
		{
			FlickrConfig config = new FlickrConfig();
			config.Enabled = checkBoxEnabled.Checked;
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

		private void FlickrForm_Load(object sender, EventArgs e)
		{
			int startOfLink = linkLabel.Text.Length;
			linkLabel.Text += HomePageUrl;
			linkLabel.Links.Add(startOfLink, HomePageUrl.Length, HomePageUrl);
		}

		private void linkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			ProcessStartInfo startInfo = new ProcessStartInfo(e.Link.LinkData.ToString());
			Process.Start(startInfo);
		}

		private void buttonOK_Click(object sender, EventArgs e)
		{
			if (string.IsNullOrWhiteSpace(textBoxApiKey.Text))
			{
				ShowNoApiKeyMsg();
				return;
			}

			DialogResult = DialogResult.OK;
			Close();
		}

		private void buttonTest_Click(object sender, EventArgs e)
		{
			FlickrConfig config = GetConfig();
			string apiKey = textBoxApiKey.Text;

			if (string.IsNullOrWhiteSpace(apiKey))
			{
				ShowNoApiKeyMsg();
				return;
			}

			string msg = _flickrProvider.Validate(apiKey, config);
			MsgDlg.UserError(msg);
		}

		void ShowNoApiKeyMsg()
		{
			NoFlickrApiKeyForm dlg = new NoFlickrApiKeyForm();
			dlg.ShowDialog();
		}
	}
}
