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
			this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.aboutDualLauncherToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.timer = new System.Windows.Forms.Timer(this.components);
			this.pictureBoxIcon = new System.Windows.Forms.PictureBox();
			this.enterMagicWordToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
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
            this.optionsToolStripMenuItem,
            this.aboutDualLauncherToolStripMenuItem,
            this.exitToolStripMenuItem});
			this.contextMenuStrip.Name = "contextMenuStrip";
			this.contextMenuStrip.Size = new System.Drawing.Size(187, 92);
			// 
			// optionsToolStripMenuItem
			// 
			this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
			this.optionsToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
			this.optionsToolStripMenuItem.Text = "Options...";
			this.optionsToolStripMenuItem.Click += new System.EventHandler(this.optionsToolStripMenuItem_Click);
			// 
			// aboutDualLauncherToolStripMenuItem
			// 
			this.aboutDualLauncherToolStripMenuItem.Name = "aboutDualLauncherToolStripMenuItem";
			this.aboutDualLauncherToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
			this.aboutDualLauncherToolStripMenuItem.Text = "About Dual Launcher";
			this.aboutDualLauncherToolStripMenuItem.Click += new System.EventHandler(this.aboutDualLauncherToolStripMenuItem_Click);
			// 
			// exitToolStripMenuItem
			// 
			this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
			this.exitToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
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
			// enterMagicWordToolStripMenuItem
			// 
			this.enterMagicWordToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
			this.enterMagicWordToolStripMenuItem.Name = "enterMagicWordToolStripMenuItem";
			this.enterMagicWordToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
			this.enterMagicWordToolStripMenuItem.Text = "Enter Magic Word...";
			this.enterMagicWordToolStripMenuItem.Click += new System.EventHandler(this.enterMagicWordToolStripMenuItem_Click);
			// 
			// Input
			// 
			this.Input.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.Input.Location = new System.Drawing.Point(38, 6);
			this.Input.Margin = new System.Windows.Forms.Padding(0);
			this.Input.Name = "Input";
			this.Input.Size = new System.Drawing.Size(307, 20);
			this.Input.TabIndex = 0;
			this.Input.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Input_KeyDown);
			// 
			// EntryForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CausesValidation = false;
			this.ClientSize = new System.Drawing.Size(351, 32);
			this.ContextMenuStrip = this.contextMenuStrip;
			this.Controls.Add(this.pictureBoxIcon);
			this.Controls.Add(this.Input);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MaximumSize = new System.Drawing.Size(1280, 85);
			this.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size(128, 55);
			this.Name = "EntryForm";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.Text = "Dual Launcher";
			this.TopMost = true;
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
	}
}

