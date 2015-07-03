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
	public partial class NoFlickrApiKeyForm : Form
	{
		const string GetFlickrApiKeyUrl = "https://www.flickr.com/services/api/keys/apply/";

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
