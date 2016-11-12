namespace DmtWallpaper
{
	partial class SettingsForm
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
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.textBoxFilename = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.buttonClose = new System.Windows.Forms.Button();
			this.labelProductName = new System.Windows.Forms.Label();
			this.labelCopyright = new System.Windows.Forms.Label();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.pictureBox2 = new System.Windows.Forms.PictureBox();
			this.labelLicense = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(17, 80);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(675, 39);
			this.label1.TabIndex = 0;
			this.label1.Text = "This screen saver is part of Dual Monitor Tools and shows the wallpaper generated" +
    " by DMT Wallpaper Changer when the screen saver is running.";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(17, 159);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(142, 13);
			this.label2.TabIndex = 1;
			this.label2.Text = "Wallpaper image taken from:";
			// 
			// textBoxFilename
			// 
			this.textBoxFilename.Location = new System.Drawing.Point(165, 156);
			this.textBoxFilename.Name = "textBoxFilename";
			this.textBoxFilename.ReadOnly = true;
			this.textBoxFilename.Size = new System.Drawing.Size(527, 20);
			this.textBoxFilename.TabIndex = 2;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(17, 119);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(349, 13);
			this.label3.TabIndex = 3;
			this.label3.Text = "DMT must be running and the Wallpaper Changer active for this to work.";
			// 
			// buttonClose
			// 
			this.buttonClose.Location = new System.Drawing.Point(311, 301);
			this.buttonClose.Name = "buttonClose";
			this.buttonClose.Size = new System.Drawing.Size(75, 23);
			this.buttonClose.TabIndex = 4;
			this.buttonClose.Text = "Close";
			this.buttonClose.UseVisualStyleBackColor = true;
			this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
			// 
			// labelProductName
			// 
			this.labelProductName.Location = new System.Drawing.Point(122, 12);
			this.labelProductName.Name = "labelProductName";
			this.labelProductName.Size = new System.Drawing.Size(570, 13);
			this.labelProductName.TabIndex = 6;
			this.labelProductName.Text = "labelProductName";
			// 
			// labelCopyright
			// 
			this.labelCopyright.AutoSize = true;
			this.labelCopyright.Location = new System.Drawing.Point(124, 47);
			this.labelCopyright.Name = "labelCopyright";
			this.labelCopyright.Size = new System.Drawing.Size(73, 13);
			this.labelCopyright.TabIndex = 7;
			this.labelCopyright.Text = "labelCopyright";
			// 
			// pictureBox1
			// 
			this.pictureBox1.Image = global::DmtWallpaper.Properties.Resources.dual_96_48;
			this.pictureBox1.Location = new System.Drawing.Point(20, 12);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(96, 48);
			this.pictureBox1.TabIndex = 5;
			this.pictureBox1.TabStop = false;
			// 
			// pictureBox2
			// 
			this.pictureBox2.Image = global::DmtWallpaper.Properties.Resources.gplv3_127x51;
			this.pictureBox2.Location = new System.Drawing.Point(20, 213);
			this.pictureBox2.Name = "pictureBox2";
			this.pictureBox2.Size = new System.Drawing.Size(127, 51);
			this.pictureBox2.TabIndex = 8;
			this.pictureBox2.TabStop = false;
			// 
			// labelLicense
			// 
			this.labelLicense.Location = new System.Drawing.Point(162, 213);
			this.labelLicense.Name = "labelLicense";
			this.labelLicense.Size = new System.Drawing.Size(530, 51);
			this.labelLicense.TabIndex = 9;
			this.labelLicense.Text = "labelLicense";
			// 
			// SettingsForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(704, 336);
			this.Controls.Add(this.labelLicense);
			this.Controls.Add(this.pictureBox2);
			this.Controls.Add(this.labelCopyright);
			this.Controls.Add(this.labelProductName);
			this.Controls.Add(this.pictureBox1);
			this.Controls.Add(this.buttonClose);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.textBoxFilename);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "SettingsForm";
			this.Text = "DMT Wallpaper Screen Saver Settings";
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox textBoxFilename;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Button buttonClose;
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.Label labelProductName;
		private System.Windows.Forms.Label labelCopyright;
		private System.Windows.Forms.PictureBox pictureBox2;
		private System.Windows.Forms.Label labelLicense;
	}
}