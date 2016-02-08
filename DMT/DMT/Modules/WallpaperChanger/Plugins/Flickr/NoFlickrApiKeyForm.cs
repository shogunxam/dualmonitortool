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

	/// <summary>
	/// Dialog for when a Flickr API key has not been found
	/// </summary>
	public partial class NoFlickrApiKeyForm : Form
	{
		const string GetFlickrApiKeyUrl = "https://www.flickr.com/services/api/keys/apply/";

		/// <summary>
		/// Initialises a new instance of the <see cref="NoFlickrApiKeyForm" /> class.
		/// </summary>
		public NoFlickrApiKeyForm()
		{
			InitializeComponent();
		}

		private void NoFlickrApiKeyForm_Load(object sender, EventArgs e)
		{
			int startOfLink = linkLabelFlickr.Text.Length;
			linkLabelFlickr.Text += GetFlickrApiKeyUrl;
			linkLabelFlickr.Links.Add(startOfLink, GetFlickrApiKeyUrl.Length, GetFlickrApiKeyUrl);
		}

		private void linkLabelFlickr_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			ProcessStartInfo sInfo = new ProcessStartInfo(e.Link.LinkData.ToString());
			Process.Start(sInfo);
		}

		private void buttonOK_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.OK;
			Close();
		}
	}
}
