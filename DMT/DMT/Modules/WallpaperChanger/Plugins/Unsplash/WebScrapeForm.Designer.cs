namespace DMT.Modules.WallpaperChanger.Plugins.Unsplash
{
	partial class WebScrapeForm
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

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WebScrapeForm));
			this.linkLabel = new System.Windows.Forms.LinkLabel();
			this.buttonCancel = new System.Windows.Forms.Button();
			this.buttonOK = new System.Windows.Forms.Button();
			this.textBoxDescription = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.numericUpDownWeight = new System.Windows.Forms.NumericUpDown();
			this.label4 = new System.Windows.Forms.Label();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.radioButtonAll = new System.Windows.Forms.RadioButton();
			this.radioButtonFeatured = new System.Windows.Forms.RadioButton();
			this.radioButtonCategory = new System.Windows.Forms.RadioButton();
			this.comboBoxCategory = new System.Windows.Forms.ComboBox();
			this.radioButtonUser = new System.Windows.Forms.RadioButton();
			this.textBoxUser = new System.Windows.Forms.TextBox();
			this.radioButtonUserLike = new System.Windows.Forms.RadioButton();
			this.textBoxLikedByUser = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.textBoxFilter = new System.Windows.Forms.TextBox();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownWeight)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// linkLabel
			// 
			this.linkLabel.Location = new System.Drawing.Point(109, 31);
			this.linkLabel.Name = "linkLabel";
			this.linkLabel.Size = new System.Drawing.Size(473, 13);
			this.linkLabel.TabIndex = 15;
			this.linkLabel.TabStop = true;
			this.linkLabel.Text = "Image provider to get images from ";
			this.linkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel_LinkClicked);
			// 
			// buttonCancel
			// 
			this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonCancel.Location = new System.Drawing.Point(310, 340);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new System.Drawing.Size(75, 23);
			this.buttonCancel.TabIndex = 23;
			this.buttonCancel.Text = "Cancel";
			this.buttonCancel.UseVisualStyleBackColor = true;
			// 
			// buttonOK
			// 
			this.buttonOK.Location = new System.Drawing.Point(229, 340);
			this.buttonOK.Name = "buttonOK";
			this.buttonOK.Size = new System.Drawing.Size(75, 23);
			this.buttonOK.TabIndex = 22;
			this.buttonOK.Text = "OK";
			this.buttonOK.UseVisualStyleBackColor = true;
			this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
			// 
			// textBoxDescription
			// 
			this.textBoxDescription.Location = new System.Drawing.Point(112, 105);
			this.textBoxDescription.Name = "textBoxDescription";
			this.textBoxDescription.Size = new System.Drawing.Size(470, 20);
			this.textBoxDescription.TabIndex = 19;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(11, 108);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(63, 13);
			this.label2.TabIndex = 18;
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
			this.numericUpDownWeight.TabIndex = 17;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(11, 81);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(44, 13);
			this.label4.TabIndex = 16;
			this.label4.Text = "Weight:";
			// 
			// pictureBox1
			// 
			this.pictureBox1.Image = global::DMT.Properties.Resources.UnsplashPlugin;
			this.pictureBox1.Location = new System.Drawing.Point(12, 12);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(48, 48);
			this.pictureBox1.TabIndex = 24;
			this.pictureBox1.TabStop = false;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.textBoxLikedByUser);
			this.groupBox1.Controls.Add(this.radioButtonUserLike);
			this.groupBox1.Controls.Add(this.textBoxUser);
			this.groupBox1.Controls.Add(this.radioButtonUser);
			this.groupBox1.Controls.Add(this.comboBoxCategory);
			this.groupBox1.Controls.Add(this.radioButtonCategory);
			this.groupBox1.Controls.Add(this.radioButtonFeatured);
			this.groupBox1.Controls.Add(this.radioButtonAll);
			this.groupBox1.Location = new System.Drawing.Point(14, 141);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(423, 155);
			this.groupBox1.TabIndex = 25;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Take random image from:";
			// 
			// radioButtonAll
			// 
			this.radioButtonAll.AutoSize = true;
			this.radioButtonAll.Location = new System.Drawing.Point(6, 19);
			this.radioButtonAll.Name = "radioButtonAll";
			this.radioButtonAll.Size = new System.Drawing.Size(36, 17);
			this.radioButtonAll.TabIndex = 0;
			this.radioButtonAll.TabStop = true;
			this.radioButtonAll.Text = "All";
			this.radioButtonAll.UseVisualStyleBackColor = true;
			this.radioButtonAll.CheckedChanged += new System.EventHandler(this.OnRadioChange);
			// 
			// radioButtonFeatured
			// 
			this.radioButtonFeatured.AutoSize = true;
			this.radioButtonFeatured.Location = new System.Drawing.Point(6, 45);
			this.radioButtonFeatured.Name = "radioButtonFeatured";
			this.radioButtonFeatured.Size = new System.Drawing.Size(70, 17);
			this.radioButtonFeatured.TabIndex = 1;
			this.radioButtonFeatured.TabStop = true;
			this.radioButtonFeatured.Text = "Feautued";
			this.radioButtonFeatured.UseVisualStyleBackColor = true;
			this.radioButtonFeatured.CheckedChanged += new System.EventHandler(this.OnRadioChange);
			// 
			// radioButtonCategory
			// 
			this.radioButtonCategory.AutoSize = true;
			this.radioButtonCategory.Location = new System.Drawing.Point(6, 71);
			this.radioButtonCategory.Name = "radioButtonCategory";
			this.radioButtonCategory.Size = new System.Drawing.Size(70, 17);
			this.radioButtonCategory.TabIndex = 2;
			this.radioButtonCategory.TabStop = true;
			this.radioButtonCategory.Text = "Category:";
			this.radioButtonCategory.UseVisualStyleBackColor = true;
			this.radioButtonCategory.CheckedChanged += new System.EventHandler(this.OnRadioChange);
			// 
			// comboBoxCategory
			// 
			this.comboBoxCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxCategory.FormattingEnabled = true;
			this.comboBoxCategory.Location = new System.Drawing.Point(145, 67);
			this.comboBoxCategory.Name = "comboBoxCategory";
			this.comboBoxCategory.Size = new System.Drawing.Size(272, 21);
			this.comboBoxCategory.TabIndex = 3;
			// 
			// radioButtonUser
			// 
			this.radioButtonUser.AutoSize = true;
			this.radioButtonUser.Location = new System.Drawing.Point(6, 96);
			this.radioButtonUser.Name = "radioButtonUser";
			this.radioButtonUser.Size = new System.Drawing.Size(93, 17);
			this.radioButtonUser.TabIndex = 4;
			this.radioButtonUser.TabStop = true;
			this.radioButtonUser.Text = "User\'s Photos:";
			this.radioButtonUser.UseVisualStyleBackColor = true;
			this.radioButtonUser.CheckedChanged += new System.EventHandler(this.OnRadioChange);
			// 
			// textBoxUser
			// 
			this.textBoxUser.Location = new System.Drawing.Point(145, 95);
			this.textBoxUser.Name = "textBoxUser";
			this.textBoxUser.Size = new System.Drawing.Size(272, 20);
			this.textBoxUser.TabIndex = 5;
			// 
			// radioButtonUserLike
			// 
			this.radioButtonUserLike.AutoSize = true;
			this.radioButtonUserLike.Location = new System.Drawing.Point(6, 122);
			this.radioButtonUserLike.Name = "radioButtonUserLike";
			this.radioButtonUserLike.Size = new System.Drawing.Size(123, 17);
			this.radioButtonUserLike.TabIndex = 6;
			this.radioButtonUserLike.TabStop = true;
			this.radioButtonUserLike.Text = "Photos liked by user:";
			this.radioButtonUserLike.UseVisualStyleBackColor = true;
			this.radioButtonUserLike.CheckedChanged += new System.EventHandler(this.OnRadioChange);
			// 
			// textBoxLikedByUser
			// 
			this.textBoxLikedByUser.Location = new System.Drawing.Point(145, 121);
			this.textBoxLikedByUser.Name = "textBoxLikedByUser";
			this.textBoxLikedByUser.Size = new System.Drawing.Size(272, 20);
			this.textBoxLikedByUser.TabIndex = 7;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(11, 305);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(46, 13);
			this.label1.TabIndex = 26;
			this.label1.Text = "Filter by:";
			// 
			// textBoxFilter
			// 
			this.textBoxFilter.Location = new System.Drawing.Point(112, 302);
			this.textBoxFilter.Name = "textBoxFilter";
			this.textBoxFilter.Size = new System.Drawing.Size(470, 20);
			this.textBoxFilter.TabIndex = 27;
			// 
			// WebScrapeForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(640, 377);
			this.Controls.Add(this.textBoxFilter);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.groupBox1);
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
			this.Name = "WebScrapeForm";
			this.ShowInTaskbar = false;
			this.Text = "Unsplash";
			this.Load += new System.EventHandler(this.WebScrapeForm_Load);
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownWeight)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.LinkLabel linkLabel;
		private System.Windows.Forms.Button buttonCancel;
		private System.Windows.Forms.Button buttonOK;
		private System.Windows.Forms.TextBox textBoxDescription;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.NumericUpDown numericUpDownWeight;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.TextBox textBoxLikedByUser;
		private System.Windows.Forms.RadioButton radioButtonUserLike;
		private System.Windows.Forms.TextBox textBoxUser;
		private System.Windows.Forms.RadioButton radioButtonUser;
		private System.Windows.Forms.ComboBox comboBoxCategory;
		private System.Windows.Forms.RadioButton radioButtonCategory;
		private System.Windows.Forms.RadioButton radioButtonFeatured;
		private System.Windows.Forms.RadioButton radioButtonAll;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox textBoxFilter;
	}
}