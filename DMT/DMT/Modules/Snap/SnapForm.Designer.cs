namespace DMT.Modules.Snap
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
			this.pictureBox = new System.Windows.Forms.PictureBox();
			this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.snapToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.showSnapToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.snapsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.xToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
			this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
			this.deleteCurrentSnapToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.deleteAllSnapsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
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
			this.pictureBox.Size = new System.Drawing.Size(300, 300);
			this.pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.pictureBox.TabIndex = 0;
			this.pictureBox.TabStop = false;
			// 
			// contextMenuStrip
			// 
			this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.snapToolStripMenuItem,
            this.showSnapToolStripMenuItem,
            this.snapsToolStripMenuItem,
            this.toolStripMenuItem1,
            this.copyToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.toolStripMenuItem2,
            this.deleteCurrentSnapToolStripMenuItem,
            this.deleteAllSnapsToolStripMenuItem});
			this.contextMenuStrip.Name = "contextMenuStrip";
			this.contextMenuStrip.Size = new System.Drawing.Size(180, 170);
			this.contextMenuStrip.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip_Opening);
			// 
			// snapToolStripMenuItem
			// 
			this.snapToolStripMenuItem.Name = "snapToolStripMenuItem";
			this.snapToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
			this.snapToolStripMenuItem.Text = "Snap!";
			this.snapToolStripMenuItem.Click += new System.EventHandler(this.snapToolStripMenuItem_Click);
			// 
			// showSnapToolStripMenuItem
			// 
			this.showSnapToolStripMenuItem.Name = "showSnapToolStripMenuItem";
			this.showSnapToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
			this.showSnapToolStripMenuItem.Text = "Show Snap";
			this.showSnapToolStripMenuItem.Click += new System.EventHandler(this.showSnapToolStripMenuItem_Click);
			// 
			// snapsToolStripMenuItem
			// 
			this.snapsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.xToolStripMenuItem});
			this.snapsToolStripMenuItem.Name = "snapsToolStripMenuItem";
			this.snapsToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
			this.snapsToolStripMenuItem.Text = "Snaps";
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
			// toolStripMenuItem1
			// 
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new System.Drawing.Size(176, 6);
			// 
			// copyToolStripMenuItem
			// 
			this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
			this.copyToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
			this.copyToolStripMenuItem.Text = "Copy";
			this.copyToolStripMenuItem.Click += new System.EventHandler(this.copyToolStripMenuItem_Click);
			// 
			// saveAsToolStripMenuItem
			// 
			this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
			this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
			this.saveAsToolStripMenuItem.Text = "Save As...";
			this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
			// 
			// toolStripMenuItem2
			// 
			this.toolStripMenuItem2.Name = "toolStripMenuItem2";
			this.toolStripMenuItem2.Size = new System.Drawing.Size(176, 6);
			// 
			// deleteCurrentSnapToolStripMenuItem
			// 
			this.deleteCurrentSnapToolStripMenuItem.Name = "deleteCurrentSnapToolStripMenuItem";
			this.deleteCurrentSnapToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
			this.deleteCurrentSnapToolStripMenuItem.Text = "Delete Current Snap";
			this.deleteCurrentSnapToolStripMenuItem.Click += new System.EventHandler(this.deleteCurrentSnapToolStripMenuItem_Click);
			// 
			// deleteAllSnapsToolStripMenuItem
			// 
			this.deleteAllSnapsToolStripMenuItem.Name = "deleteAllSnapsToolStripMenuItem";
			this.deleteAllSnapsToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
			this.deleteAllSnapsToolStripMenuItem.Text = "Delete All Snaps...";
			this.deleteAllSnapsToolStripMenuItem.Click += new System.EventHandler(this.deleteAllSnapsToolStripMenuItem_Click);
			// 
			// SnapForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(300, 300);
			this.Controls.Add(this.pictureBox);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "SnapForm";
			this.ShowInTaskbar = false;
			this.Text = "SnapForm";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SnapForm_FormClosing);
			((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
			this.contextMenuStrip.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.PictureBox pictureBox;
		private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
		private System.Windows.Forms.ToolStripMenuItem snapToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem showSnapToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem snapsToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
		private System.Windows.Forms.ToolStripMenuItem deleteCurrentSnapToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem deleteAllSnapsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem xToolStripMenuItem;
	}
}