namespace DMT.Modules.WallpaperChanger
{
	partial class WallpaperChangerPropertiesOptionsPanel
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.picPreview = new System.Windows.Forms.PictureBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.linkLabelProvider = new System.Windows.Forms.LinkLabel();
			this.linkLabelDetails = new System.Windows.Forms.LinkLabel();
			this.label4 = new System.Windows.Forms.Label();
			this.linkLabelPhotographer = new System.Windows.Forms.LinkLabel();
			this.label3 = new System.Windows.Forms.Label();
			this.linkLabelSource = new System.Windows.Forms.LinkLabel();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.toolTip = new System.Windows.Forms.ToolTip(this.components);
			((System.ComponentModel.ISupportInitialize)(this.picPreview)).BeginInit();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// picPreview
			// 
			this.picPreview.Location = new System.Drawing.Point(3, 3);
			this.picPreview.Name = "picPreview";
			this.picPreview.Size = new System.Drawing.Size(494, 161);
			this.picPreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
			this.picPreview.TabIndex = 0;
			this.picPreview.TabStop = false;
			this.toolTip.SetToolTip(this.picPreview, "Click on monitor to see information about the image displayed on it.");
			this.picPreview.MouseClick += new System.Windows.Forms.MouseEventHandler(this.picPreview_MouseClick);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.linkLabelProvider);
			this.groupBox1.Controls.Add(this.linkLabelDetails);
			this.groupBox1.Controls.Add(this.label4);
			this.groupBox1.Controls.Add(this.linkLabelPhotographer);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.linkLabelSource);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Location = new System.Drawing.Point(3, 170);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(494, 101);
			this.groupBox1.TabIndex = 1;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Source Image Properties";
			// 
			// linkLabelProvider
			// 
			this.linkLabelProvider.AutoSize = true;
			this.linkLabelProvider.Location = new System.Drawing.Point(127, 18);
			this.linkLabelProvider.Name = "linkLabelProvider";
			this.linkLabelProvider.Size = new System.Drawing.Size(55, 13);
			this.linkLabelProvider.TabIndex = 8;
			this.linkLabelProvider.TabStop = true;
			this.linkLabelProvider.Text = "linkLabel1";
			this.linkLabelProvider.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel_LinkClicked);
			// 
			// linkLabelDetails
			// 
			this.linkLabelDetails.AutoSize = true;
			this.linkLabelDetails.Location = new System.Drawing.Point(127, 72);
			this.linkLabelDetails.Name = "linkLabelDetails";
			this.linkLabelDetails.Size = new System.Drawing.Size(55, 13);
			this.linkLabelDetails.TabIndex = 7;
			this.linkLabelDetails.TabStop = true;
			this.linkLabelDetails.Text = "linkLabel1";
			this.linkLabelDetails.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel_LinkClicked);
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(6, 72);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(71, 13);
			this.label4.TabIndex = 6;
			this.label4.Text = "Image Details";
			// 
			// linkLabelPhotographer
			// 
			this.linkLabelPhotographer.AutoSize = true;
			this.linkLabelPhotographer.Location = new System.Drawing.Point(127, 54);
			this.linkLabelPhotographer.Name = "linkLabelPhotographer";
			this.linkLabelPhotographer.Size = new System.Drawing.Size(55, 13);
			this.linkLabelPhotographer.TabIndex = 5;
			this.linkLabelPhotographer.TabStop = true;
			this.linkLabelPhotographer.Text = "linkLabel1";
			this.linkLabelPhotographer.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel_LinkClicked);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(6, 54);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(74, 13);
			this.label3.TabIndex = 4;
			this.label3.Text = "Photographer:";
			// 
			// linkLabelSource
			// 
			this.linkLabelSource.AutoSize = true;
			this.linkLabelSource.Location = new System.Drawing.Point(127, 36);
			this.linkLabelSource.Name = "linkLabelSource";
			this.linkLabelSource.Size = new System.Drawing.Size(55, 13);
			this.linkLabelSource.TabIndex = 3;
			this.linkLabelSource.TabStop = true;
			this.linkLabelSource.Text = "linkLabel1";
			this.linkLabelSource.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel_LinkClicked);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(6, 36);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(76, 13);
			this.label2.TabIndex = 2;
			this.label2.Text = "Image Source:";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(6, 18);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(49, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Provider:";
			// 
			// WallpaperChangerPropertiesOptionsPanel
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.picPreview);
			this.Name = "WallpaperChangerPropertiesOptionsPanel";
			this.Size = new System.Drawing.Size(500, 338);
			this.Load += new System.EventHandler(this.WallpaperChangerPropertiesOptionsPanel_Load);
			((System.ComponentModel.ISupportInitialize)(this.picPreview)).EndInit();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.PictureBox picPreview;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.LinkLabel linkLabelDetails;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.LinkLabel linkLabelPhotographer;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.LinkLabel linkLabelSource;
		private System.Windows.Forms.ToolTip toolTip;
		private System.Windows.Forms.LinkLabel linkLabelProvider;
	}
}
