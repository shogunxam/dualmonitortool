namespace DualMonitorTools.DualWallpaperChanger.Unsplash
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
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.buttonAbout = new System.Windows.Forms.Button();
			this.buttonCancel = new System.Windows.Forms.Button();
			this.buttonOK = new System.Windows.Forms.Button();
			this.textBoxDescription = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.numericUpDownWeight = new System.Windows.Forms.NumericUpDown();
			this.label4 = new System.Windows.Forms.Label();
			this.checkBoxFirstPageOnly = new System.Windows.Forms.CheckBox();
			this.linkLabel = new System.Windows.Forms.LinkLabel();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownWeight)).BeginInit();
			this.SuspendLayout();
			// 
			// pictureBox1
			// 
			this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
			this.pictureBox1.Location = new System.Drawing.Point(12, 12);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(48, 48);
			this.pictureBox1.TabIndex = 14;
			this.pictureBox1.TabStop = false;
			// 
			// buttonAbout
			// 
			this.buttonAbout.Location = new System.Drawing.Point(26, 182);
			this.buttonAbout.Name = "buttonAbout";
			this.buttonAbout.Size = new System.Drawing.Size(75, 23);
			this.buttonAbout.TabIndex = 27;
			this.buttonAbout.Text = "About";
			this.buttonAbout.UseVisualStyleBackColor = true;
			this.buttonAbout.Click += new System.EventHandler(this.buttonAbout_Click);
			// 
			// buttonCancel
			// 
			this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonCancel.Location = new System.Drawing.Point(382, 182);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new System.Drawing.Size(75, 23);
			this.buttonCancel.TabIndex = 26;
			this.buttonCancel.Text = "Cancel";
			this.buttonCancel.UseVisualStyleBackColor = true;
			this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
			// 
			// buttonOK
			// 
			this.buttonOK.Location = new System.Drawing.Point(236, 182);
			this.buttonOK.Name = "buttonOK";
			this.buttonOK.Size = new System.Drawing.Size(75, 23);
			this.buttonOK.TabIndex = 25;
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
			// checkBoxFirstPageOnly
			// 
			this.checkBoxFirstPageOnly.AutoSize = true;
			this.checkBoxFirstPageOnly.Location = new System.Drawing.Point(14, 137);
			this.checkBoxFirstPageOnly.Name = "checkBoxFirstPageOnly";
			this.checkBoxFirstPageOnly.Size = new System.Drawing.Size(248, 17);
			this.checkBoxFirstPageOnly.TabIndex = 28;
			this.checkBoxFirstPageOnly.Text = "Take image from first page (ignore other pages)";
			this.checkBoxFirstPageOnly.UseVisualStyleBackColor = true;
			// 
			// linkLabel
			// 
			this.linkLabel.Location = new System.Drawing.Point(109, 31);
			this.linkLabel.Name = "linkLabel";
			this.linkLabel.Size = new System.Drawing.Size(473, 13);
			this.linkLabel.TabIndex = 29;
			this.linkLabel.TabStop = true;
			this.linkLabel.Text = "Plugin for DualWallpaperChanger to get images from ";
			this.linkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel_LinkClicked);
			// 
			// WebScrapeForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(640, 233);
			this.Controls.Add(this.linkLabel);
			this.Controls.Add(this.checkBoxFirstPageOnly);
			this.Controls.Add(this.buttonAbout);
			this.Controls.Add(this.buttonCancel);
			this.Controls.Add(this.buttonOK);
			this.Controls.Add(this.textBoxDescription);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.numericUpDownWeight);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.pictureBox1);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "WebScrapeForm";
			this.Text = "www.unsplash.com";
			this.Load += new System.EventHandler(this.WebScrapeForm_Load);
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownWeight)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.Button buttonAbout;
		private System.Windows.Forms.Button buttonCancel;
		private System.Windows.Forms.Button buttonOK;
		private System.Windows.Forms.TextBox textBoxDescription;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.NumericUpDown numericUpDownWeight;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.CheckBox checkBoxFirstPageOnly;
		private System.Windows.Forms.LinkLabel linkLabel;
	}
}