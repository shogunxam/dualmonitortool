namespace SwapScreen
{
	partial class OptionsForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OptionsForm));
			this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
			this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.swapScreensToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripMenuItemOptions = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemAbout = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemExit = new System.Windows.Forms.ToolStripMenuItem();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.keyComboPanel = new SwapScreen.KeyComboPanel();
			this.buttonOk = new System.Windows.Forms.Button();
			this.buttonCancel = new System.Windows.Forms.Button();
			this.visitSwapScreenWebsiteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.contextMenuStrip.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// notifyIcon
			// 
			this.notifyIcon.ContextMenuStrip = this.contextMenuStrip;
			this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
			this.notifyIcon.Text = "Swap Screen";
			this.notifyIcon.Visible = true;
			this.notifyIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon_MouseDoubleClick);
			// 
			// contextMenuStrip
			// 
			this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator2,
            this.swapScreensToolStripMenuItem,
            this.toolStripSeparator1,
            this.toolStripMenuItemOptions,
            this.toolStripMenuItemAbout,
            this.visitSwapScreenWebsiteToolStripMenuItem,
            this.toolStripMenuItemExit});
			this.contextMenuStrip.Name = "contextMenuStrip";
			this.contextMenuStrip.Size = new System.Drawing.Size(211, 126);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(207, 6);
			// 
			// swapScreensToolStripMenuItem
			// 
			this.swapScreensToolStripMenuItem.Name = "swapScreensToolStripMenuItem";
			this.swapScreensToolStripMenuItem.Size = new System.Drawing.Size(210, 22);
			this.swapScreensToolStripMenuItem.Text = "Swap Screens";
			this.swapScreensToolStripMenuItem.ToolTipText = "Move all windows to their next screen";
			this.swapScreensToolStripMenuItem.Click += new System.EventHandler(this.swapScreensToolStripMenuItem_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(207, 6);
			// 
			// toolStripMenuItemOptions
			// 
			this.toolStripMenuItemOptions.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
			this.toolStripMenuItemOptions.Name = "toolStripMenuItemOptions";
			this.toolStripMenuItemOptions.Size = new System.Drawing.Size(210, 22);
			this.toolStripMenuItemOptions.Text = "Options...";
			this.toolStripMenuItemOptions.ToolTipText = "Configure hotkey to move the active window to the next screen";
			this.toolStripMenuItemOptions.Click += new System.EventHandler(this.toolStripMenuItemOptions_Click);
			// 
			// toolStripMenuItemAbout
			// 
			this.toolStripMenuItemAbout.Name = "toolStripMenuItemAbout";
			this.toolStripMenuItemAbout.Size = new System.Drawing.Size(210, 22);
			this.toolStripMenuItemAbout.Text = "About SwapScreen";
			this.toolStripMenuItemAbout.Click += new System.EventHandler(this.toolStripMenuItemAbout_Click);
			// 
			// toolStripMenuItemExit
			// 
			this.toolStripMenuItemExit.Name = "toolStripMenuItemExit";
			this.toolStripMenuItemExit.Size = new System.Drawing.Size(210, 22);
			this.toolStripMenuItemExit.Text = "Exit";
			this.toolStripMenuItemExit.Click += new System.EventHandler(this.toolStripMenuItemExit_Click);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.keyComboPanel);
			this.groupBox1.Location = new System.Drawing.Point(12, 12);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(252, 121);
			this.groupBox1.TabIndex = 1;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Hot Key to move active window to next screen";
			// 
			// keyComboPanel
			// 
			this.keyComboPanel.Location = new System.Drawing.Point(6, 19);
			this.keyComboPanel.Name = "keyComboPanel";
			this.keyComboPanel.Size = new System.Drawing.Size(238, 98);
			this.keyComboPanel.TabIndex = 0;
			// 
			// buttonOk
			// 
			this.buttonOk.Location = new System.Drawing.Point(18, 139);
			this.buttonOk.Name = "buttonOk";
			this.buttonOk.Size = new System.Drawing.Size(75, 23);
			this.buttonOk.TabIndex = 2;
			this.buttonOk.Text = "OK";
			this.buttonOk.UseVisualStyleBackColor = true;
			this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
			// 
			// buttonCancel
			// 
			this.buttonCancel.Location = new System.Drawing.Point(181, 139);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new System.Drawing.Size(75, 23);
			this.buttonCancel.TabIndex = 3;
			this.buttonCancel.Text = "Cancel";
			this.buttonCancel.UseVisualStyleBackColor = true;
			this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
			// 
			// visitSwapScreenWebsiteToolStripMenuItem
			// 
			this.visitSwapScreenWebsiteToolStripMenuItem.Name = "visitSwapScreenWebsiteToolStripMenuItem";
			this.visitSwapScreenWebsiteToolStripMenuItem.Size = new System.Drawing.Size(210, 22);
			this.visitSwapScreenWebsiteToolStripMenuItem.Text = "Visit Swap Screen Website";
			this.visitSwapScreenWebsiteToolStripMenuItem.Click += new System.EventHandler(this.visitSwapScreenWebsiteToolStripMenuItem_Click);
			// 
			// OptionsForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(279, 171);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.buttonCancel);
			this.Controls.Add(this.buttonOk);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "OptionsForm";
			this.Text = "Options for SwapScreen";
			this.Shown += new System.EventHandler(this.OptionsForm_Shown);
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OptionsForm_FormClosing);
			this.contextMenuStrip.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.NotifyIcon notifyIcon;
		private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemOptions;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemAbout;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemExit;
		private System.Windows.Forms.GroupBox groupBox1;
		private KeyComboPanel keyComboPanel;
		private System.Windows.Forms.Button buttonOk;
		private System.Windows.Forms.Button buttonCancel;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripMenuItem swapScreensToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripMenuItem visitSwapScreenWebsiteToolStripMenuItem;
	}
}

