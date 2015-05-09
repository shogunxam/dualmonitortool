using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DMT.Modules.WallpaperChanger.Plugins.Unsplash
{
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
	}
}
