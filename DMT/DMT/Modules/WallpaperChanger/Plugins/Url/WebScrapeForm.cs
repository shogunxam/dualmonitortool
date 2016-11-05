using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DMT.Modules.WallpaperChanger.Plugins.Url
{
	/// <summary>
	/// Url configuration dialog
	/// </summary>
	public partial class WebScrapeForm : Form
	{
		public WebScrapeForm(WebScrapeConfig config)
		{
			InitializeComponent();

			numericUpDownWeight.Value = config.Weight;
			textBoxDescription.Text = config.Description;
			textBoxUrl.Text = config.Url;
			checkBoxEnableEscapes.Checked = config.AllowEscapes;
		}

		/// <summary>
		/// Gets the current configuration
		/// </summary>
		/// <returns>current configuration</returns>
		public WebScrapeConfig GetConfig()
		{
			WebScrapeConfig config = new WebScrapeConfig();
			config.Weight = (int)numericUpDownWeight.Value;
			config.Description = textBoxDescription.Text;
			config.Url = textBoxUrl.Text;
			config.AllowEscapes = checkBoxEnableEscapes.Checked;

			return config;
		}

		private void buttonOK_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.OK;
			Close();
		}

		private void labelEscapeWidth_Click(object sender, EventArgs e)
		{
			if (checkBoxEnableEscapes.Checked)
			{
				textBoxUrl.Text += "%W";
			}
		}

		private void labelEscapeHeight_Click(object sender, EventArgs e)
		{
			if (checkBoxEnableEscapes.Checked)
			{
				textBoxUrl.Text += "%H";
			}
		}

		private void labelEscapeEscape_Click(object sender, EventArgs e)
		{
			if (checkBoxEnableEscapes.Checked)
			{
				textBoxUrl.Text += "%%";
			}
		}

	}
}
