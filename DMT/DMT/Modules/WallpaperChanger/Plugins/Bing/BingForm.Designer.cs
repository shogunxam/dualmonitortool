﻿
namespace DMT.Modules.WallpaperChanger.Plugins.Bing
{
	using System.Collections;
	using System.Windows.Forms;

	partial class BingForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		public class Market
		{
			public Market(string key, string value) { Key = key; Value = value; }
			public string Key { get; set; }
			public string Value { get; set; }
		};

		public IList markets = null;

		public class KeySearch
		{
			string _s;

			public KeySearch(string key)
			{
				_s = key;
			}

			public bool KeyMatch(Market e)
			{
				return e.Key.Equals(_s);
			}
		}

		private int indexOfMarket(string key)
		{
			KeySearch s = new KeySearch(key);
			System.Collections.Generic.List<Market> m = markets as System.Collections.Generic.List<Market>;
			return m.FindIndex(s.KeyMatch);
		}
		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BingForm));
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.buttonCancel = new System.Windows.Forms.Button();
			this.buttonOK = new System.Windows.Forms.Button();
			this.textBoxDescription = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.numericUpDownWeight = new System.Windows.Forms.NumericUpDown();
			this.label4 = new System.Windows.Forms.Label();
			this.linkLabel = new System.Windows.Forms.LinkLabel();
			this.checkBoxEnabled = new System.Windows.Forms.CheckBox();
			this.label1 = new System.Windows.Forms.Label();
			this.bingLocale = new System.Windows.Forms.ComboBox();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownWeight)).BeginInit();
			this.SuspendLayout();
			// 
			// pictureBox1
			// 
			this.pictureBox1.Image = global::DMT.Properties.Resources.BingPlugin;
			this.pictureBox1.Location = new System.Drawing.Point(12, 12);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(48, 48);
			this.pictureBox1.TabIndex = 14;
			this.pictureBox1.TabStop = false;
			// 
			// buttonCancel
			// 
			this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonCancel.Location = new System.Drawing.Point(382, 182);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new System.Drawing.Size(75, 23);
			this.buttonCancel.TabIndex = 8;
			this.buttonCancel.Text = "Cancel";
			this.buttonCancel.UseVisualStyleBackColor = true;
			this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
			// 
			// buttonOK
			// 
			this.buttonOK.Location = new System.Drawing.Point(236, 182);
			this.buttonOK.Name = "buttonOK";
			this.buttonOK.Size = new System.Drawing.Size(75, 23);
			this.buttonOK.TabIndex = 7;
			this.buttonOK.Text = "OK";
			this.buttonOK.UseVisualStyleBackColor = true;
			this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
			// 
			// textBoxDescription
			// 
			this.textBoxDescription.Location = new System.Drawing.Point(112, 105);
			this.textBoxDescription.Name = "textBoxDescription";
			this.textBoxDescription.Size = new System.Drawing.Size(470, 20);
			this.textBoxDescription.TabIndex = 4;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(11, 108);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(63, 13);
			this.label2.TabIndex = 3;
			this.label2.Text = "Description:";
			// 
			// numericUpDownWeight
			// 
			this.numericUpDownWeight.Location = new System.Drawing.Point(112, 79);
			this.numericUpDownWeight.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
			this.numericUpDownWeight.Name = "numericUpDownWeight";
			this.numericUpDownWeight.Size = new System.Drawing.Size(120, 20);
			this.numericUpDownWeight.TabIndex = 2;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(11, 81);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(44, 13);
			this.label4.TabIndex = 1;
			this.label4.Text = "Weight:";
			// 
			// linkLabel
			// 
			this.linkLabel.Location = new System.Drawing.Point(109, 31);
			this.linkLabel.Name = "linkLabel";
			this.linkLabel.Size = new System.Drawing.Size(473, 13);
			this.linkLabel.TabIndex = 0;
			this.linkLabel.TabStop = true;
			this.linkLabel.Text = "Plugin for DualWallpaperChanger to get images from ";
			this.linkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel_LinkClicked);
			// 
			// checkBoxEnabled
			// 
			this.checkBoxEnabled.AutoSize = true;
			this.checkBoxEnabled.Location = new System.Drawing.Point(282, 82);
			this.checkBoxEnabled.Name = "checkBoxEnabled";
			this.checkBoxEnabled.Size = new System.Drawing.Size(65, 17);
			this.checkBoxEnabled.TabIndex = 15;
			this.checkBoxEnabled.Text = "Enabled";
			this.checkBoxEnabled.UseVisualStyleBackColor = true;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 140);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(66, 13);
			this.label1.TabIndex = 16;
			this.label1.Text = "Bing Locale:";
			// 
			// bingLocale
			// 
			this.bingLocale.FormattingEnabled = true;
			this.bingLocale.Location = new System.Drawing.Point(112, 140);
			this.bingLocale.Name = "bingLocale";
			this.bingLocale.Size = new System.Drawing.Size(199, 21);
			this.bingLocale.TabIndex = 17;

			this.markets = new System.Collections.Generic.List<Market>();

			this.markets.Add(new Market(@"ar-XA", @"(شبه الجزيرة العربية‎) العربية"));
			this.markets.Add(new Market(@"bg-BG", @"български (България)"));
			this.markets.Add(new Market(@"cs-CZ", @"čeština (Česko)"));
			this.markets.Add(new Market(@"da-DK", @"dansk (Danmark)"));
			this.markets.Add(new Market(@"de-AT", @"Deutsch (Österreich)"));
			this.markets.Add(new Market(@"de-CH", @"Deutsch (Schweiz)"));
			this.markets.Add(new Market(@"de-DE", @"Deutsch (Deutschland)"));
			this.markets.Add(new Market(@"el-GR", @"Ελληνικά (Ελλάδα)"));
			this.markets.Add(new Market(@"en-AU", @"English (Australia)"));
			this.markets.Add(new Market(@"en-CA", @"English (Canada)"));
			this.markets.Add(new Market(@"en-GB", @"English (United Kingdom)"));
			this.markets.Add(new Market(@"en-ID", @"English (Indonesia)"));
			this.markets.Add(new Market(@"en-IE", @"English (Ireland)"));
			this.markets.Add(new Market(@"en-IN", @"English (India)"));
			this.markets.Add(new Market(@"en-MY", @"English (Malaysia)"));
			this.markets.Add(new Market(@"en-NZ", @"English (New Zealand)"));
			this.markets.Add(new Market(@"en-PH", @"English (Philippines)"));
			this.markets.Add(new Market(@"en-SG", @"English (Singapore)"));
			this.markets.Add(new Market(@"en-US", @"English (United States)"));
			this.markets.Add(new Market(@"en-WW", @"English (International)"));
			this.markets.Add(new Market(@"en-XA", @"English (Arabia)"));
			this.markets.Add(new Market(@"en-ZA", @"English (South Africa)"));
			this.markets.Add(new Market(@"es-AR", @"español (Argentina)"));
			this.markets.Add(new Market(@"es-CL", @"español (Chile)"));
			this.markets.Add(new Market(@"es-ES", @"español (España)"));
			this.markets.Add(new Market(@"es-MX", @"español (México)"));
			this.markets.Add(new Market(@"es-US", @"español (Estados Unidos)"));
			this.markets.Add(new Market(@"es-XL", @"español (Latinoamérica)"));
			this.markets.Add(new Market(@"et-EE", @"eesti (Eesti)"));
			this.markets.Add(new Market(@"fi-FI", @"suomi (Suomi)"));
			this.markets.Add(new Market(@"fr-BE", @"français (Belgique)"));
			this.markets.Add(new Market(@"fr-CA", @"français (Canada)"));
			this.markets.Add(new Market(@"fr-CH", @"français (Suisse)"));
			this.markets.Add(new Market(@"fr-FR", @"français (France)"));
			this.markets.Add(new Market(@"he-IL", @"(עברית (ישראל"));
			this.markets.Add(new Market(@"hr-HR", @"hrvatski (Hrvatska)"));
			this.markets.Add(new Market(@"hu-HU", @"magyar (Magyarország)"));
			this.markets.Add(new Market(@"it-IT", @"italiano (Italia)"));
			this.markets.Add(new Market(@"ja-JP", @"日本語 (日本)"));
			this.markets.Add(new Market(@"ko-KR", @"한국어(대한민국)"));
			this.markets.Add(new Market(@"lt-LT", @"lietuvių (Lietuva)"));
			this.markets.Add(new Market(@"lv-LV", @"latviešu (Latvija)"));
			this.markets.Add(new Market(@"nb-NO", @"norsk bokmål (Norge)"));
			this.markets.Add(new Market(@"nl-BE", @"Nederlands (België)"));
			this.markets.Add(new Market(@"nl-NL", @"Nederlands (Nederland)"));
			this.markets.Add(new Market(@"pl-PL", @"polski (Polska)"));
			this.markets.Add(new Market(@"pt-BR", @"português (Brasil)"));
			this.markets.Add(new Market(@"pt-PT", @"português (Portugal)"));
			this.markets.Add(new Market(@"ro-RO", @"română (România)"));
			this.markets.Add(new Market(@"ru-RU", @"русский (Россия)"));
			this.markets.Add(new Market(@"sk-SK", @"slovenčina (Slovensko)"));
			this.markets.Add(new Market(@"sl-SL", @"slovenščina (Slovenija)"));
			this.markets.Add(new Market(@"sv-SE", @"svenska (Sverige)"));
			this.markets.Add(new Market(@"th-TH", @"ไทย (ไทย)"));
			this.markets.Add(new Market(@"tr-TR", @"Türkçe (Türkiye)"));
			this.markets.Add(new Market(@"uk-UA", @"українська (Україна)"));
			this.markets.Add(new Market(@"zh-CN", @"中文（中国）"));
			this.markets.Add(new Market(@"zh-HK", @"中文（中國香港特別行政區）"));
			this.markets.Add(new Market(@"zh-TW", @"中文（台灣）"));

			this.bingLocale.DataSource = new BindingSource(this.markets, null);
			this.bingLocale.DisplayMember = "Value";
			this.bingLocale.ValueMember = "Key";
			this.bingLocale.CreateControl();
			// 
			// BingForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(640, 233);
			this.Controls.Add(this.bingLocale);


			this.Controls.Add(this.label1);
			this.Controls.Add(this.checkBoxEnabled);
			this.Controls.Add(this.linkLabel);
			this.Controls.Add(this.buttonCancel);
			this.Controls.Add(this.buttonOK);
			this.Controls.Add(this.textBoxDescription);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.numericUpDownWeight);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.pictureBox1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "BingForm";
			this.ShowInTaskbar = false;
			this.Text = "Bing";
			this.Load += new System.EventHandler(this.BingForm_Load);
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownWeight)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.Button buttonCancel;
		private System.Windows.Forms.Button buttonOK;
		private System.Windows.Forms.TextBox textBoxDescription;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.NumericUpDown numericUpDownWeight;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.LinkLabel linkLabel;
		private System.Windows.Forms.CheckBox checkBoxEnabled;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ComboBox bingLocale;
	}
}
