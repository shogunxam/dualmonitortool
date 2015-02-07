namespace DualMonitorTools.DualWallpaperChanger
{
	partial class AboutPluginForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AboutPluginForm));
			this.labelLicense = new System.Windows.Forms.Label();
			this.pictureBox2 = new System.Windows.Forms.PictureBox();
			this.labelDescription = new System.Windows.Forms.Label();
			this.labelCopyright = new System.Windows.Forms.Label();
			this.labelProductName = new System.Windows.Forms.Label();
			this.pictureBoxPlugin = new System.Windows.Forms.PictureBox();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxPlugin)).BeginInit();
			this.SuspendLayout();
			// 
			// labelLicense
			// 
			this.labelLicense.Location = new System.Drawing.Point(145, 118);
			this.labelLicense.Name = "labelLicense";
			this.labelLicense.Size = new System.Drawing.Size(405, 69);
			this.labelLicense.TabIndex = 45;
			this.labelLicense.Text = "labelLicense";
			// 
			// pictureBox2
			// 
			this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
			this.pictureBox2.Location = new System.Drawing.Point(12, 118);
			this.pictureBox2.Name = "pictureBox2";
			this.pictureBox2.Size = new System.Drawing.Size(127, 51);
			this.pictureBox2.TabIndex = 44;
			this.pictureBox2.TabStop = false;
			// 
			// labelDescription
			// 
			this.labelDescription.Location = new System.Drawing.Point(12, 60);
			this.labelDescription.Name = "labelDescription";
			this.labelDescription.Size = new System.Drawing.Size(538, 55);
			this.labelDescription.TabIndex = 43;
			this.labelDescription.Text = "labelDescription";
			// 
			// labelCopyright
			// 
			this.labelCopyright.Location = new System.Drawing.Point(69, 32);
			this.labelCopyright.Name = "labelCopyright";
			this.labelCopyright.Size = new System.Drawing.Size(481, 13);
			this.labelCopyright.TabIndex = 42;
			this.labelCopyright.Text = "labelCopyright";
			// 
			// labelProductName
			// 
			this.labelProductName.Location = new System.Drawing.Point(66, 9);
			this.labelProductName.Name = "labelProductName";
			this.labelProductName.Size = new System.Drawing.Size(484, 13);
			this.labelProductName.TabIndex = 41;
			this.labelProductName.Text = "labelProductName";
			// 
			// pictureBoxPlugin
			// 
			this.pictureBoxPlugin.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxPlugin.Image")));
			this.pictureBoxPlugin.InitialImage = null;
			this.pictureBoxPlugin.Location = new System.Drawing.Point(12, 9);
			this.pictureBoxPlugin.Name = "pictureBoxPlugin";
			this.pictureBoxPlugin.Size = new System.Drawing.Size(48, 48);
			this.pictureBoxPlugin.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.pictureBoxPlugin.TabIndex = 40;
			this.pictureBoxPlugin.TabStop = false;
			// 
			// AboutPluginForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(570, 205);
			this.Controls.Add(this.labelLicense);
			this.Controls.Add(this.pictureBox2);
			this.Controls.Add(this.labelDescription);
			this.Controls.Add(this.labelCopyright);
			this.Controls.Add(this.labelProductName);
			this.Controls.Add(this.pictureBoxPlugin);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "AboutPluginForm";
			this.ShowInTaskbar = false;
			this.Text = "AboutPluginForm";
			((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxPlugin)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Label labelLicense;
		private System.Windows.Forms.PictureBox pictureBox2;
		private System.Windows.Forms.Label labelDescription;
		private System.Windows.Forms.Label labelCopyright;
		private System.Windows.Forms.Label labelProductName;
		private System.Windows.Forms.PictureBox pictureBoxPlugin;
	}
}