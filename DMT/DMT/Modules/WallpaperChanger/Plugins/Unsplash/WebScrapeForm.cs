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

namespace DMT.Modules.WallpaperChanger.Plugins.Unsplash
{
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

	/// <summary>
	/// Unsplash configuration dialog
	/// </summary>
	public partial class WebScrapeForm : Form
	{
		const string HomePageUrl = "www.unsplash.com";

		static string[] AvailableCategories = { "buildings", "food", "nature", "people", "technology", "objects" };

		/// <summary>
		/// Initialises a new instance of the <see cref="WebScrapeForm" /> class.
		/// </summary>
		/// <param name="config">Configuration to edit</param>
		public WebScrapeForm(WebScrapeConfig config)
		{
			InitializeComponent();

			checkBoxEnabled.Checked = config.Enabled;
			numericUpDownWeight.Value = config.Weight;
			textBoxDescription.Text = config.Description;
			textBoxUser.Text = config.User;
			textBoxLikedByUser.Text = config.LikedByUser;
			textBoxFilter.Text = config.Filter;

			FillCategories(config.Category);
			ShowSelectionType(config.Type);
		}

		/// <summary>
		/// Gets the current configuration
		/// </summary>
		/// <returns>current configuration</returns>
		public WebScrapeConfig GetConfig()
		{
			WebScrapeConfig config = new WebScrapeConfig();
			config.Enabled = checkBoxEnabled.Checked;
			config.Weight = (int)numericUpDownWeight.Value;
			config.Description = textBoxDescription.Text;
			config.Type = GetSelectionType();
			config.Category = GetCategory();
			config.User = textBoxUser.Text;
			config.LikedByUser = textBoxLikedByUser.Text;
			config.Filter = textBoxFilter.Text;

			return config;
		}

		void FillCategories(string selectedCategory)
		{
			comboBoxCategory.Items.AddRange(AvailableCategories);
			if (AvailableCategories.Contains(selectedCategory))
			{
				comboBoxCategory.SelectedItem = selectedCategory;
			}
			else
			{
				comboBoxCategory.SelectedIndex = 0;
			}
		}

		void ShowSelectionType(WebScrapeConfig.UnsplashType type)
		{
			bool enableCategoryCombo = false;
			bool enableUserText = false;
			bool enabbleUserLikeText = false;

			switch (type)
			{
				case WebScrapeConfig.UnsplashType.Featured:
					radioButtonFeatured.Checked = true;
					break;

				case WebScrapeConfig.UnsplashType.Category:
					radioButtonCategory.Checked = true;
					enableCategoryCombo = true;
					break;

				case WebScrapeConfig.UnsplashType.User:
					radioButtonUser.Checked = true;
					enableUserText = true;
					break;

				case WebScrapeConfig.UnsplashType.LikedByUser:
					radioButtonUserLike.Checked = true;
					enabbleUserLikeText = true;
					break;

				case WebScrapeConfig.UnsplashType.Random:
				default:
					radioButtonAll.Checked = true;
					break;
			}

			comboBoxCategory.Enabled = enableCategoryCombo;
			textBoxUser.Enabled = enableUserText;
			textBoxLikedByUser.Enabled = enabbleUserLikeText;
		}

		WebScrapeConfig.UnsplashType GetSelectionType()
		{
			if (radioButtonFeatured.Checked)
			{
				return WebScrapeConfig.UnsplashType.Featured;
			}
			else if (radioButtonCategory.Checked)
			{
				return WebScrapeConfig.UnsplashType.Category;
			}
			else if (radioButtonUser.Checked)
			{
				return WebScrapeConfig.UnsplashType.User;
			}
			else if (radioButtonUserLike.Checked)
			{
				return WebScrapeConfig.UnsplashType.LikedByUser;
			}
			else
			{
				return WebScrapeConfig.UnsplashType.Random;
			}
		}

		string GetCategory()
		{
			return (string)comboBoxCategory.SelectedItem;
		}

		private void WebScrapeForm_Load(object sender, EventArgs e)
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
			DialogResult = DialogResult.OK;
			Close();
		}

		private void OnRadioChange(object sender, EventArgs e)
		{
			WebScrapeConfig.UnsplashType type = GetSelectionType();
			ShowSelectionType(type);
		}
	}
}
