namespace DMT.Modules.WallpaperChanger.Plugins.Url
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
			this.buttonCancel = new System.Windows.Forms.Button();
			this.buttonOK = new System.Windows.Forms.Button();
			this.textBoxDescription = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.numericUpDownWeight = new System.Windows.Forms.NumericUpDown();
			this.label4 = new System.Windows.Forms.Label();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.label1 = new System.Windows.Forms.Label();
			this.textBoxUrl = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.labelEscapeHeight = new System.Windows.Forms.Label();
			this.labelEscapeWidth = new System.Windows.Forms.Label();
			this.labelEscapeEscape = new System.Windows.Forms.Label();
			this.checkBoxEnableEscapes = new System.Windows.Forms.CheckBox();
			this.checkBoxEnabled = new System.Windows.Forms.CheckBox();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownWeight)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// buttonCancel
			// 
			this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonCancel.Location = new System.Drawing.Point(367, 257);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new System.Drawing.Size(75, 23);
			this.buttonCancel.TabIndex = 10;
			this.buttonCancel.Text = "Cancel";
			this.buttonCancel.UseVisualStyleBackColor = true;
			// 
			// buttonOK
			// 
			this.buttonOK.Location = new System.Drawing.Point(221, 257);
			this.buttonOK.Name = "buttonOK";
			this.buttonOK.Size = new System.Drawing.Size(75, 23);
			this.buttonOK.TabIndex = 9;
			this.buttonOK.Text = "OK";
			this.buttonOK.UseVisualStyleBackColor = true;
			this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
			// 
			// textBoxDescription
			// 
			this.textBoxDescription.Location = new System.Drawing.Point(112, 105);
			this.textBoxDescription.Name = "textBoxDescription";
			this.textBoxDescription.Size = new System.Drawing.Size(470, 20);
			this.textBoxDescription.TabIndex = 5;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(11, 108);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(63, 13);
			this.label2.TabIndex = 4;
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
			this.label4.Location = new System.Drawing.Point(12, 81);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(44, 13);
			this.label4.TabIndex = 1;
			this.label4.Text = "Weight:";
			// 
			// pictureBox1
			// 
			this.pictureBox1.Image = global::DMT.Properties.Resources.UnsplashPlugin;
			this.pictureBox1.Location = new System.Drawing.Point(12, 12);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(48, 48);
			this.pictureBox1.TabIndex = 33;
			this.pictureBox1.TabStop = false;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 134);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(32, 13);
			this.label1.TabIndex = 6;
			this.label1.Text = "URL:";
			// 
			// textBoxUrl
			// 
			this.textBoxUrl.Location = new System.Drawing.Point(112, 131);
			this.textBoxUrl.Name = "textBoxUrl";
			this.textBoxUrl.Size = new System.Drawing.Size(470, 20);
			this.textBoxUrl.TabIndex = 7;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(109, 30);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(99, 13);
			this.label3.TabIndex = 0;
			this.label3.Text = "Get images from Url";
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.labelEscapeHeight);
			this.groupBox1.Controls.Add(this.labelEscapeWidth);
			this.groupBox1.Controls.Add(this.labelEscapeEscape);
			this.groupBox1.Controls.Add(this.checkBoxEnableEscapes);
			this.groupBox1.Location = new System.Drawing.Point(112, 157);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(470, 82);
			this.groupBox1.TabIndex = 8;
			this.groupBox1.TabStop = false;
			// 
			// labelEscapeHeight
			// 
			this.labelEscapeHeight.AutoSize = true;
			this.labelEscapeHeight.Location = new System.Drawing.Point(200, 36);
			this.labelEscapeHeight.Name = "labelEscapeHeight";
			this.labelEscapeHeight.Size = new System.Drawing.Size(104, 13);
			this.labelEscapeHeight.TabIndex = 2;
			this.labelEscapeHeight.Text = "%H -> Screen height";
			this.labelEscapeHeight.Click += new System.EventHandler(this.labelEscapeHeight_Click);
			// 
			// labelEscapeWidth
			// 
			this.labelEscapeWidth.AutoSize = true;
			this.labelEscapeWidth.Location = new System.Drawing.Point(200, 20);
			this.labelEscapeWidth.Name = "labelEscapeWidth";
			this.labelEscapeWidth.Size = new System.Drawing.Size(103, 13);
			this.labelEscapeWidth.TabIndex = 1;
			this.labelEscapeWidth.Text = "%W -> Screen width";
			this.labelEscapeWidth.Click += new System.EventHandler(this.labelEscapeWidth_Click);
			// 
			// labelEscapeEscape
			// 
			this.labelEscapeEscape.AutoSize = true;
			this.labelEscapeEscape.Location = new System.Drawing.Point(200, 52);
			this.labelEscapeEscape.Name = "labelEscapeEscape";
			this.labelEscapeEscape.Size = new System.Drawing.Size(46, 13);
			this.labelEscapeEscape.TabIndex = 3;
			this.labelEscapeEscape.Text = "%% -> %";
			this.labelEscapeEscape.Click += new System.EventHandler(this.labelEscapeEscape_Click);
			// 
			// checkBoxEnableEscapes
			// 
			this.checkBoxEnableEscapes.AutoSize = true;
			this.checkBoxEnableEscapes.Location = new System.Drawing.Point(6, 19);
			this.checkBoxEnableEscapes.Name = "checkBoxEnableEscapes";
			this.checkBoxEnableEscapes.Size = new System.Drawing.Size(174, 17);
			this.checkBoxEnableEscapes.TabIndex = 0;
			this.checkBoxEnableEscapes.Text = "Enable escapes in above URL:";
			this.checkBoxEnableEscapes.UseVisualStyleBackColor = true;
			// 
			// checkBoxEnabled
			// 
			this.checkBoxEnabled.AutoSize = true;
			this.checkBoxEnabled.Location = new System.Drawing.Point(278, 80);
			this.checkBoxEnabled.Name = "checkBoxEnabled";
			this.checkBoxEnabled.Size = new System.Drawing.Size(65, 17);
			this.checkBoxEnabled.TabIndex = 3;
			this.checkBoxEnabled.Text = "Enabled";
			this.checkBoxEnabled.UseVisualStyleBackColor = true;
			// 
			// WebScrapeForm
			// 
			this.AcceptButton = this.buttonOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.buttonCancel;
			this.ClientSize = new System.Drawing.Size(634, 298);
			this.Controls.Add(this.checkBoxEnabled);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.textBoxUrl);
			this.Controls.Add(this.label1);
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
			this.Text = "Url";
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownWeight)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button buttonCancel;
		private System.Windows.Forms.Button buttonOK;
		private System.Windows.Forms.TextBox textBoxDescription;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.NumericUpDown numericUpDownWeight;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox textBoxUrl;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label labelEscapeHeight;
		private System.Windows.Forms.Label labelEscapeWidth;
		private System.Windows.Forms.Label labelEscapeEscape;
		private System.Windows.Forms.CheckBox checkBoxEnableEscapes;
		private System.Windows.Forms.CheckBox checkBoxEnabled;
	}
}