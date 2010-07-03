namespace DualSnap
{
	partial class SnapForm
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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SnapForm));
			this.pictureBox = new System.Windows.Forms.PictureBox();
			this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.snapToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.showLastSnapToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.snapsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.xToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.aboutDualSnapToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.visitDualSnapWebsiteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
			((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
			this.contextMenuStrip.SuspendLayout();
			this.SuspendLayout();
			// 
			// pictureBox
			// 
			this.pictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.pictureBox.ContextMenuStrip = this.contextMenuStrip;
			this.pictureBox.Location = new System.Drawing.Point(0, 0);
			this.pictureBox.Name = "pictureBox";
			this.pictureBox.Size = new System.Drawing.Size(387, 313);
			this.pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.pictureBox.TabIndex = 1;
			this.pictureBox.TabStop = false;
			// 
			// contextMenuStrip
			// 
			this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.snapToolStripMenuItem,
            this.showLastSnapToolStripMenuItem,
            this.snapsToolStripMenuItem,
            this.toolStripSeparator1,
            this.copyToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.toolStripSeparator2,
            this.optionsToolStripMenuItem,
            this.aboutDualSnapToolStripMenuItem,
            this.visitDualSnapWebsiteToolStripMenuItem,
            this.exitToolStripMenuItem});
			this.contextMenuStrip.Name = "contextMenuStrip";
			this.contextMenuStrip.Size = new System.Drawing.Size(198, 214);
			this.contextMenuStrip.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip_Opening);
			// 
			// snapToolStripMenuItem
			// 
			this.snapToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
			this.snapToolStripMenuItem.Name = "snapToolStripMenuItem";
			this.snapToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
			this.snapToolStripMenuItem.Text = "Snap!";
			this.snapToolStripMenuItem.ToolTipText = "Take a snap of the primary screen";
			this.snapToolStripMenuItem.Click += new System.EventHandler(this.snapToolStripMenuItem_Click);
			// 
			// showLastSnapToolStripMenuItem
			// 
			this.showLastSnapToolStripMenuItem.Name = "showLastSnapToolStripMenuItem";
			this.showLastSnapToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
			this.showLastSnapToolStripMenuItem.Text = "Show Snap";
			this.showLastSnapToolStripMenuItem.ToolTipText = "Show/hide the current snap";
			this.showLastSnapToolStripMenuItem.Click += new System.EventHandler(this.showSnapToolStripMenuItem_Click);
			// 
			// snapsToolStripMenuItem
			// 
			this.snapsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.xToolStripMenuItem});
			this.snapsToolStripMenuItem.Name = "snapsToolStripMenuItem";
			this.snapsToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
			this.snapsToolStripMenuItem.Text = "Snaps";
			this.snapsToolStripMenuItem.ToolTipText = "Show previously taken snaps";
			this.snapsToolStripMenuItem.DropDownOpening += new System.EventHandler(this.snapsToolStripMenuItem_DropDownOpening);
			// 
			// xToolStripMenuItem
			// 
			this.xToolStripMenuItem.AutoSize = false;
			this.xToolStripMenuItem.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
			this.xToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.xToolStripMenuItem.Name = "xToolStripMenuItem";
			this.xToolStripMenuItem.Size = new System.Drawing.Size(128, 102);
			this.xToolStripMenuItem.Text = "x";
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(194, 6);
			// 
			// copyToolStripMenuItem
			// 
			this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
			this.copyToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
			this.copyToolStripMenuItem.Text = "Copy";
			this.copyToolStripMenuItem.ToolTipText = "Copy current snap to the clipboard";
			this.copyToolStripMenuItem.Click += new System.EventHandler(this.copyToolStripMenuItem_Click);
			// 
			// saveAsToolStripMenuItem
			// 
			this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
			this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
			this.saveAsToolStripMenuItem.Text = "Save As...";
			this.saveAsToolStripMenuItem.ToolTipText = "Save current snap as a PNG file";
			this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(194, 6);
			// 
			// optionsToolStripMenuItem
			// 
			this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
			this.optionsToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
			this.optionsToolStripMenuItem.Text = "Options...";
			this.optionsToolStripMenuItem.ToolTipText = "Show Options";
			this.optionsToolStripMenuItem.Click += new System.EventHandler(this.optionsToolStripMenuItem_Click);
			// 
			// aboutDualSnapToolStripMenuItem
			// 
			this.aboutDualSnapToolStripMenuItem.Name = "aboutDualSnapToolStripMenuItem";
			this.aboutDualSnapToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
			this.aboutDualSnapToolStripMenuItem.Text = "About Dual Snap";
			this.aboutDualSnapToolStripMenuItem.Click += new System.EventHandler(this.aboutDualSnapToolStripMenuItem_Click);
			// 
			// visitDualSnapWebsiteToolStripMenuItem
			// 
			this.visitDualSnapWebsiteToolStripMenuItem.Name = "visitDualSnapWebsiteToolStripMenuItem";
			this.visitDualSnapWebsiteToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
			this.visitDualSnapWebsiteToolStripMenuItem.Text = "Visit Dual Snap Website";
			this.visitDualSnapWebsiteToolStripMenuItem.ToolTipText = "Open Dual Snap\'s website in your browser";
			this.visitDualSnapWebsiteToolStripMenuItem.Click += new System.EventHandler(this.visitDualSnapWebsiteToolStripMenuItem_Click);
			// 
			// exitToolStripMenuItem
			// 
			this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
			this.exitToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
			this.exitToolStripMenuItem.Text = "Exit";
			this.exitToolStripMenuItem.ToolTipText = "Exit Dual Snap";
			this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
			// 
			// notifyIcon
			// 
			this.notifyIcon.ContextMenuStrip = this.contextMenuStrip;
			this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
			this.notifyIcon.Text = "Dual Snap";
			this.notifyIcon.Visible = true;
			this.notifyIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon_MouseDoubleClick);
			// 
			// SnapForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(387, 313);
			this.Controls.Add(this.pictureBox);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "SnapForm";
			this.ShowInTaskbar = false;
			this.Text = "Form1";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SnapForm_FormClosing);
			this.HelpRequested += new System.Windows.Forms.HelpEventHandler(this.SnapForm_HelpRequested);
			((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
			this.contextMenuStrip.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.PictureBox pictureBox;
		private System.Windows.Forms.NotifyIcon notifyIcon;
		private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
		private System.Windows.Forms.ToolStripMenuItem snapToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem showLastSnapToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem aboutDualSnapToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripMenuItem snapsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem xToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem visitDualSnapWebsiteToolStripMenuItem;
	}
}

