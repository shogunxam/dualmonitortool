namespace DMT.Modules.WallpaperChanger.Plugins.Flickr
{
	partial class NoFlickrApiKeyForm
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
			this.linkLabelFlickr = new System.Windows.Forms.LinkLabel();
			this.label2 = new System.Windows.Forms.Label();
			this.buttonOK = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(167, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "A flickr API key must be specified.";
			// 
			// linkLabelFlickr
			// 
			this.linkLabelFlickr.AutoSize = true;
			this.linkLabelFlickr.Location = new System.Drawing.Point(12, 34);
			this.linkLabelFlickr.Name = "linkLabelFlickr";
			this.linkLabelFlickr.Size = new System.Drawing.Size(231, 13);
			this.linkLabelFlickr.TabIndex = 1;
			this.linkLabelFlickr.TabStop = true;
			this.linkLabelFlickr.Text = "If you don\'t have one, you may obtain one from ";
			this.linkLabelFlickr.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelFlickr_LinkClicked);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(12, 59);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(202, 13);
			this.label2.TabIndex = 2;
			this.label2.Text = "You will need a flickr/yahoo account first.";
			// 
			// buttonOK
			// 
			this.buttonOK.Location = new System.Drawing.Point(231, 99);
			this.buttonOK.Name = "buttonOK";
			this.buttonOK.Size = new System.Drawing.Size(75, 23);
			this.buttonOK.TabIndex = 3;
			this.buttonOK.Text = "OK";
			this.buttonOK.UseVisualStyleBackColor = true;
			this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
			// 
			// NoFlickrApiKeyForm
			// 
			this.AcceptButton = this.buttonOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(533, 134);
			this.Controls.Add(this.buttonOK);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.linkLabelFlickr);
			this.Controls.Add(this.label1);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "NoFlickrApiKeyForm";
			this.ShowInTaskbar = false;
			this.Text = "flickr API Key required";
			this.Load += new System.EventHandler(this.NoFlickrApiKeyForm_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.LinkLabel linkLabelFlickr;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button buttonOK;
	}
}