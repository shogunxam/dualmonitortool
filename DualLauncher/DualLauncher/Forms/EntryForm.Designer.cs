namespace DualLauncher
{
	partial class EntryForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EntryForm));
			this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
			this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.enterMagicWordToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.addNewMagicWordToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
			this.aboutDualLauncherToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.visitDualLauncherWebsiteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
			this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.timer = new System.Windows.Forms.Timer(this.components);
			this.pictureBoxIcon = new System.Windows.Forms.PictureBox();
			this.toolTip = new System.Windows.Forms.ToolTip(this.components);
			this.buttonOptions = new System.Windows.Forms.Button();
			this.magicWordListBox = new DualLauncher.MagicWordListBox();
			this.columnHeaderAlias = new System.Windows.Forms.ColumnHeader();
			this.columnHeaderFilename = new System.Windows.Forms.ColumnHeader();
			this.Input = new DualLauncher.MyTextBox();
			this.contextMenuStrip.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxIcon)).BeginInit();
			this.SuspendLayout();
			// 
			// notifyIcon
			// 
			this.notifyIcon.ContextMenuStrip = this.contextMenuStrip;
			this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
			this.notifyIcon.Text = "Dual Launcher";
			this.notifyIcon.Visible = true;
			this.notifyIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon_MouseDoubleClick);
			// 
			// contextMenuStrip
			// 
			this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.enterMagicWordToolStripMenuItem,
            this.addNewMagicWordToolStripMenuItem,
            this.optionsToolStripMenuItem,
            this.toolStripMenuItem2,
            this.aboutDualLauncherToolStripMenuItem,
            this.visitDualLauncherWebsiteToolStripMenuItem,
            this.toolStripMenuItem1,
            this.exitToolStripMenuItem});
			this.contextMenuStrip.Name = "contextMenuStrip";
			this.contextMenuStrip.Size = new System.Drawing.Size(221, 148);
			// 
			// enterMagicWordToolStripMenuItem
			// 
			this.enterMagicWordToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
			this.enterMagicWordToolStripMenuItem.Name = "enterMagicWordToolStripMenuItem";
			this.enterMagicWordToolStripMenuItem.Size = new System.Drawing.Size(220, 22);
			this.enterMagicWordToolStripMenuItem.Text = "Enter Magic Word...";
			this.enterMagicWordToolStripMenuItem.Click += new System.EventHandler(this.enterMagicWordToolStripMenuItem_Click);
			// 
			// addNewMagicWordToolStripMenuItem
			// 
			this.addNewMagicWordToolStripMenuItem.Name = "addNewMagicWordToolStripMenuItem";
			this.addNewMagicWordToolStripMenuItem.Size = new System.Drawing.Size(220, 22);
			this.addNewMagicWordToolStripMenuItem.Text = "Add New Magic Word...";
			this.addNewMagicWordToolStripMenuItem.Click += new System.EventHandler(this.addNewMagicWordToolStripMenuItem_Click);
			// 
			// optionsToolStripMenuItem
			// 
			this.optionsToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("optionsToolStripMenuItem.Image")));
			this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
			this.optionsToolStripMenuItem.Size = new System.Drawing.Size(220, 22);
			this.optionsToolStripMenuItem.Text = "Options";
			this.optionsToolStripMenuItem.Click += new System.EventHandler(this.optionsToolStripMenuItem_Click);
			// 
			// toolStripMenuItem2
			// 
			this.toolStripMenuItem2.Name = "toolStripMenuItem2";
			this.toolStripMenuItem2.Size = new System.Drawing.Size(217, 6);
			// 
			// aboutDualLauncherToolStripMenuItem
			// 
			this.aboutDualLauncherToolStripMenuItem.Name = "aboutDualLauncherToolStripMenuItem";
			this.aboutDualLauncherToolStripMenuItem.Size = new System.Drawing.Size(220, 22);
			this.aboutDualLauncherToolStripMenuItem.Text = "About Dual Launcher";
			this.aboutDualLauncherToolStripMenuItem.Click += new System.EventHandler(this.aboutDualLauncherToolStripMenuItem_Click);
			// 
			// visitDualLauncherWebsiteToolStripMenuItem
			// 
			this.visitDualLauncherWebsiteToolStripMenuItem.Name = "visitDualLauncherWebsiteToolStripMenuItem";
			this.visitDualLauncherWebsiteToolStripMenuItem.Size = new System.Drawing.Size(220, 22);
			this.visitDualLauncherWebsiteToolStripMenuItem.Text = "Visit Dual Launcher Website";
			this.visitDualLauncherWebsiteToolStripMenuItem.Click += new System.EventHandler(this.visitDualLauncherWebsiteToolStripMenuItem_Click);
			// 
			// toolStripMenuItem1
			// 
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new System.Drawing.Size(217, 6);
			// 
			// exitToolStripMenuItem
			// 
			this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
			this.exitToolStripMenuItem.Size = new System.Drawing.Size(220, 22);
			this.exitToolStripMenuItem.Text = "Exit";
			this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
			// 
			// timer
			// 
			this.timer.Tick += new System.EventHandler(this.timer_Tick);
			// 
			// pictureBoxIcon
			// 
			this.pictureBoxIcon.Location = new System.Drawing.Point(0, 0);
			this.pictureBoxIcon.Name = "pictureBoxIcon";
			this.pictureBoxIcon.Size = new System.Drawing.Size(32, 32);
			this.pictureBoxIcon.TabIndex = 1;
			this.pictureBoxIcon.TabStop = false;
			// 
			// buttonOptions
			// 
			this.buttonOptions.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonOptions.Image = ((System.Drawing.Image)(resources.GetObject("buttonOptions.Image")));
			this.buttonOptions.Location = new System.Drawing.Point(337, 3);
			this.buttonOptions.Name = "buttonOptions";
			this.buttonOptions.Size = new System.Drawing.Size(24, 24);
			this.buttonOptions.TabIndex = 3;
			this.toolTip.SetToolTip(this.buttonOptions, "Click for Options");
			this.buttonOptions.UseVisualStyleBackColor = true;
			this.buttonOptions.Click += new System.EventHandler(this.buttonOptions_Click);
			// 
			// magicWordListBox
			// 
			this.magicWordListBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.magicWordListBox.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderAlias,
            this.columnHeaderFilename});
			this.magicWordListBox.Location = new System.Drawing.Point(12, 38);
			this.magicWordListBox.Name = "magicWordListBox";
			this.magicWordListBox.Size = new System.Drawing.Size(343, 146);
			this.magicWordListBox.TabIndex = 2;
			this.toolTip.SetToolTip(this.magicWordListBox, "Click on an icon to run the application");
			this.magicWordListBox.UseCompatibleStateImageBehavior = false;
			this.magicWordListBox.MouseClick += new System.Windows.Forms.MouseEventHandler(this.magicWordListBox_MouseClick);
			this.magicWordListBox.DoubleClick += new System.EventHandler(this.magicWordListBox_DoubleClick);
			this.magicWordListBox.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.magicWordListBox_ItemSelectionChanged);
			this.magicWordListBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.magicWordListBox_KeyDown);
			// 
			// columnHeaderAlias
			// 
			this.columnHeaderAlias.Text = "Magic Word";
			this.columnHeaderAlias.Width = 120;
			// 
			// columnHeaderFilename
			// 
			this.columnHeaderFilename.Text = "Filename";
			this.columnHeaderFilename.Width = 200;
			// 
			// Input
			// 
			this.Input.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.Input.Location = new System.Drawing.Point(38, 6);
			this.Input.Margin = new System.Windows.Forms.Padding(0);
			this.Input.Name = "Input";
			this.Input.Size = new System.Drawing.Size(296, 20);
			this.Input.TabIndex = 0;
			this.toolTip.SetToolTip(this.Input, "Start entering the magic word here and a guess will be made of the correct word.\r" +
					"\nUse up and down arrow keys to cycle through alternatives.");
			this.Input.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Input_KeyDown);
			// 
			// EntryForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CausesValidation = false;
			this.ClientSize = new System.Drawing.Size(361, 195);
			this.ContextMenuStrip = this.contextMenuStrip;
			this.Controls.Add(this.buttonOptions);
			this.Controls.Add(this.pictureBoxIcon);
			this.Controls.Add(this.magicWordListBox);
			this.Controls.Add(this.Input);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size(128, 55);
			this.Name = "EntryForm";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.Text = "Dual Launcher";
			this.TopMost = true;
			this.Deactivate += new System.EventHandler(this.EntryForm_Deactivate);
			this.Load += new System.EventHandler(this.EntryForm_Load);
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.EntryForm_FormClosing);
			this.contextMenuStrip.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxIcon)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private MyTextBox Input;
		private System.Windows.Forms.NotifyIcon notifyIcon;
		private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
		private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem aboutDualLauncherToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
		private System.Windows.Forms.Timer timer;
		private System.Windows.Forms.PictureBox pictureBoxIcon;
		private System.Windows.Forms.ToolStripMenuItem enterMagicWordToolStripMenuItem;
		private MagicWordListBox magicWordListBox;
		private System.Windows.Forms.ToolTip toolTip;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
		private System.Windows.Forms.Button buttonOptions;
		private System.Windows.Forms.ColumnHeader columnHeaderAlias;
		private System.Windows.Forms.ColumnHeader columnHeaderFilename;
		private System.Windows.Forms.ToolStripMenuItem addNewMagicWordToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
		private System.Windows.Forms.ToolStripMenuItem visitDualLauncherWebsiteToolStripMenuItem;
	}
}

