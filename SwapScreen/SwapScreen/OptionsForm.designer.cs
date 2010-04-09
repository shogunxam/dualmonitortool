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
			this.visitSwapScreenWebsiteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemExit = new System.Windows.Forms.ToolStripMenuItem();
			this.buttonCancel = new System.Windows.Forms.Button();
			this.buttonNextScreen = new System.Windows.Forms.Button();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.buttonDesktop2 = new System.Windows.Forms.Button();
			this.labelShowDesktop2 = new System.Windows.Forms.Label();
			this.label20 = new System.Windows.Forms.Label();
			this.buttonDesktop1 = new System.Windows.Forms.Button();
			this.labelShowDesktop1 = new System.Windows.Forms.Label();
			this.label18 = new System.Windows.Forms.Label();
			this.buttonRotatePrev = new System.Windows.Forms.Button();
			this.labelRotatePrev = new System.Windows.Forms.Label();
			this.label16 = new System.Windows.Forms.Label();
			this.buttonRotateNext = new System.Windows.Forms.Button();
			this.labelRotateNext = new System.Windows.Forms.Label();
			this.label14 = new System.Windows.Forms.Label();
			this.buttonSuperSize = new System.Windows.Forms.Button();
			this.labelSupersize = new System.Windows.Forms.Label();
			this.label12 = new System.Windows.Forms.Label();
			this.buttonMaximise = new System.Windows.Forms.Button();
			this.labelMaximise = new System.Windows.Forms.Label();
			this.label10 = new System.Windows.Forms.Label();
			this.buttonMinimiseAllBut = new System.Windows.Forms.Button();
			this.labelMinimiseAllBut = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.buttonMinimise = new System.Windows.Forms.Button();
			this.labelMinimise = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.buttonPreviousScreen = new System.Windows.Forms.Button();
			this.labelPrevScreen = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.labelNextScreen = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.contextMenuStrip.SuspendLayout();
			this.groupBox2.SuspendLayout();
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
			// visitSwapScreenWebsiteToolStripMenuItem
			// 
			this.visitSwapScreenWebsiteToolStripMenuItem.Name = "visitSwapScreenWebsiteToolStripMenuItem";
			this.visitSwapScreenWebsiteToolStripMenuItem.Size = new System.Drawing.Size(210, 22);
			this.visitSwapScreenWebsiteToolStripMenuItem.Text = "Visit Swap Screen Website";
			this.visitSwapScreenWebsiteToolStripMenuItem.Click += new System.EventHandler(this.visitSwapScreenWebsiteToolStripMenuItem_Click);
			// 
			// toolStripMenuItemExit
			// 
			this.toolStripMenuItemExit.Name = "toolStripMenuItemExit";
			this.toolStripMenuItemExit.Size = new System.Drawing.Size(210, 22);
			this.toolStripMenuItemExit.Text = "Exit";
			this.toolStripMenuItemExit.Click += new System.EventHandler(this.toolStripMenuItemExit_Click);
			// 
			// buttonCancel
			// 
			this.buttonCancel.Location = new System.Drawing.Point(222, 283);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new System.Drawing.Size(75, 23);
			this.buttonCancel.TabIndex = 3;
			this.buttonCancel.Text = "Close";
			this.buttonCancel.UseVisualStyleBackColor = true;
			this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
			// 
			// buttonNextScreen
			// 
			this.buttonNextScreen.Location = new System.Drawing.Point(395, 14);
			this.buttonNextScreen.Name = "buttonNextScreen";
			this.buttonNextScreen.Size = new System.Drawing.Size(75, 23);
			this.buttonNextScreen.TabIndex = 4;
			this.buttonNextScreen.Text = "Change...";
			this.buttonNextScreen.UseVisualStyleBackColor = true;
			this.buttonNextScreen.Click += new System.EventHandler(this.buttonNextScreen_Click);
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.buttonDesktop2);
			this.groupBox2.Controls.Add(this.labelShowDesktop2);
			this.groupBox2.Controls.Add(this.label20);
			this.groupBox2.Controls.Add(this.buttonDesktop1);
			this.groupBox2.Controls.Add(this.labelShowDesktop1);
			this.groupBox2.Controls.Add(this.label18);
			this.groupBox2.Controls.Add(this.buttonRotatePrev);
			this.groupBox2.Controls.Add(this.labelRotatePrev);
			this.groupBox2.Controls.Add(this.label16);
			this.groupBox2.Controls.Add(this.buttonRotateNext);
			this.groupBox2.Controls.Add(this.labelRotateNext);
			this.groupBox2.Controls.Add(this.label14);
			this.groupBox2.Controls.Add(this.buttonSuperSize);
			this.groupBox2.Controls.Add(this.labelSupersize);
			this.groupBox2.Controls.Add(this.label12);
			this.groupBox2.Controls.Add(this.buttonMaximise);
			this.groupBox2.Controls.Add(this.labelMaximise);
			this.groupBox2.Controls.Add(this.label10);
			this.groupBox2.Controls.Add(this.buttonMinimiseAllBut);
			this.groupBox2.Controls.Add(this.labelMinimiseAllBut);
			this.groupBox2.Controls.Add(this.label8);
			this.groupBox2.Controls.Add(this.buttonMinimise);
			this.groupBox2.Controls.Add(this.labelMinimise);
			this.groupBox2.Controls.Add(this.label6);
			this.groupBox2.Controls.Add(this.buttonPreviousScreen);
			this.groupBox2.Controls.Add(this.labelPrevScreen);
			this.groupBox2.Controls.Add(this.label3);
			this.groupBox2.Controls.Add(this.labelNextScreen);
			this.groupBox2.Controls.Add(this.label1);
			this.groupBox2.Controls.Add(this.buttonNextScreen);
			this.groupBox2.Location = new System.Drawing.Point(12, 12);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(483, 265);
			this.groupBox2.TabIndex = 5;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Hotkeys";
			// 
			// buttonDesktop2
			// 
			this.buttonDesktop2.Location = new System.Drawing.Point(395, 230);
			this.buttonDesktop2.Name = "buttonDesktop2";
			this.buttonDesktop2.Size = new System.Drawing.Size(75, 23);
			this.buttonDesktop2.TabIndex = 33;
			this.buttonDesktop2.Text = "Change...";
			this.buttonDesktop2.UseVisualStyleBackColor = true;
			this.buttonDesktop2.Click += new System.EventHandler(this.buttonDesktop2_Click);
			// 
			// labelShowDesktop2
			// 
			this.labelShowDesktop2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
			this.labelShowDesktop2.Location = new System.Drawing.Point(236, 235);
			this.labelShowDesktop2.Name = "labelShowDesktop2";
			this.labelShowDesktop2.Size = new System.Drawing.Size(153, 13);
			this.labelShowDesktop2.TabIndex = 32;
			this.labelShowDesktop2.Text = "labelShowDesktop2";
			// 
			// label20
			// 
			this.label20.Location = new System.Drawing.Point(6, 235);
			this.label20.Name = "label20";
			this.label20.Size = new System.Drawing.Size(224, 13);
			this.label20.TabIndex = 31;
			this.label20.Text = "Show desktop 2";
			// 
			// buttonDesktop1
			// 
			this.buttonDesktop1.Location = new System.Drawing.Point(395, 206);
			this.buttonDesktop1.Name = "buttonDesktop1";
			this.buttonDesktop1.Size = new System.Drawing.Size(75, 23);
			this.buttonDesktop1.TabIndex = 30;
			this.buttonDesktop1.Text = "Change...";
			this.buttonDesktop1.UseVisualStyleBackColor = true;
			this.buttonDesktop1.Click += new System.EventHandler(this.buttonDesktop1_Click);
			// 
			// labelShowDesktop1
			// 
			this.labelShowDesktop1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
			this.labelShowDesktop1.Location = new System.Drawing.Point(236, 211);
			this.labelShowDesktop1.Name = "labelShowDesktop1";
			this.labelShowDesktop1.Size = new System.Drawing.Size(153, 13);
			this.labelShowDesktop1.TabIndex = 29;
			this.labelShowDesktop1.Text = "labelShowDesktop1";
			// 
			// label18
			// 
			this.label18.Location = new System.Drawing.Point(6, 211);
			this.label18.Name = "label18";
			this.label18.Size = new System.Drawing.Size(224, 13);
			this.label18.TabIndex = 28;
			this.label18.Text = "Show desktop 1";
			// 
			// buttonRotatePrev
			// 
			this.buttonRotatePrev.Location = new System.Drawing.Point(395, 182);
			this.buttonRotatePrev.Name = "buttonRotatePrev";
			this.buttonRotatePrev.Size = new System.Drawing.Size(75, 23);
			this.buttonRotatePrev.TabIndex = 27;
			this.buttonRotatePrev.Text = "Change...";
			this.buttonRotatePrev.UseVisualStyleBackColor = true;
			this.buttonRotatePrev.Click += new System.EventHandler(this.buttonRotatePrev_Click);
			// 
			// labelRotatePrev
			// 
			this.labelRotatePrev.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
			this.labelRotatePrev.Location = new System.Drawing.Point(236, 187);
			this.labelRotatePrev.Name = "labelRotatePrev";
			this.labelRotatePrev.Size = new System.Drawing.Size(153, 13);
			this.labelRotatePrev.TabIndex = 26;
			this.labelRotatePrev.Text = "labelRotatePrev";
			// 
			// label16
			// 
			this.label16.Location = new System.Drawing.Point(6, 187);
			this.label16.Name = "label16";
			this.label16.Size = new System.Drawing.Size(224, 13);
			this.label16.TabIndex = 25;
			this.label16.Text = "Rotate screens backwards";
			// 
			// buttonRotateNext
			// 
			this.buttonRotateNext.Location = new System.Drawing.Point(395, 158);
			this.buttonRotateNext.Name = "buttonRotateNext";
			this.buttonRotateNext.Size = new System.Drawing.Size(75, 23);
			this.buttonRotateNext.TabIndex = 24;
			this.buttonRotateNext.Text = "Change...";
			this.buttonRotateNext.UseVisualStyleBackColor = true;
			this.buttonRotateNext.Click += new System.EventHandler(this.buttonRotateNext_Click);
			// 
			// labelRotateNext
			// 
			this.labelRotateNext.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
			this.labelRotateNext.Location = new System.Drawing.Point(236, 163);
			this.labelRotateNext.Name = "labelRotateNext";
			this.labelRotateNext.Size = new System.Drawing.Size(153, 13);
			this.labelRotateNext.TabIndex = 23;
			this.labelRotateNext.Text = "labelRotateNext";
			// 
			// label14
			// 
			this.label14.Location = new System.Drawing.Point(6, 163);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(224, 13);
			this.label14.TabIndex = 22;
			this.label14.Text = "Rotate screens forward";
			// 
			// buttonSuperSize
			// 
			this.buttonSuperSize.Location = new System.Drawing.Point(395, 134);
			this.buttonSuperSize.Name = "buttonSuperSize";
			this.buttonSuperSize.Size = new System.Drawing.Size(75, 23);
			this.buttonSuperSize.TabIndex = 21;
			this.buttonSuperSize.Text = "Change...";
			this.buttonSuperSize.UseVisualStyleBackColor = true;
			this.buttonSuperSize.Click += new System.EventHandler(this.buttonSuperSize_Click);
			// 
			// labelSupersize
			// 
			this.labelSupersize.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
			this.labelSupersize.Location = new System.Drawing.Point(236, 139);
			this.labelSupersize.Name = "labelSupersize";
			this.labelSupersize.Size = new System.Drawing.Size(153, 13);
			this.labelSupersize.TabIndex = 20;
			this.labelSupersize.Text = "labelSupersize";
			// 
			// label12
			// 
			this.label12.Location = new System.Drawing.Point(6, 139);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(224, 13);
			this.label12.TabIndex = 19;
			this.label12.Text = "Supersize active window";
			// 
			// buttonMaximise
			// 
			this.buttonMaximise.Location = new System.Drawing.Point(395, 110);
			this.buttonMaximise.Name = "buttonMaximise";
			this.buttonMaximise.Size = new System.Drawing.Size(75, 23);
			this.buttonMaximise.TabIndex = 18;
			this.buttonMaximise.Text = "Change...";
			this.buttonMaximise.UseVisualStyleBackColor = true;
			this.buttonMaximise.Click += new System.EventHandler(this.buttonMaximise_Click);
			// 
			// labelMaximise
			// 
			this.labelMaximise.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
			this.labelMaximise.Location = new System.Drawing.Point(236, 115);
			this.labelMaximise.Name = "labelMaximise";
			this.labelMaximise.Size = new System.Drawing.Size(153, 13);
			this.labelMaximise.TabIndex = 17;
			this.labelMaximise.Text = "labelMaximise";
			// 
			// label10
			// 
			this.label10.Location = new System.Drawing.Point(6, 115);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(224, 13);
			this.label10.TabIndex = 16;
			this.label10.Text = "Maximise active window";
			// 
			// buttonMinimiseAllBut
			// 
			this.buttonMinimiseAllBut.Location = new System.Drawing.Point(395, 86);
			this.buttonMinimiseAllBut.Name = "buttonMinimiseAllBut";
			this.buttonMinimiseAllBut.Size = new System.Drawing.Size(75, 23);
			this.buttonMinimiseAllBut.TabIndex = 15;
			this.buttonMinimiseAllBut.Text = "Change...";
			this.buttonMinimiseAllBut.UseVisualStyleBackColor = true;
			this.buttonMinimiseAllBut.Click += new System.EventHandler(this.buttonMinimiseAllBut_Click);
			// 
			// labelMinimiseAllBut
			// 
			this.labelMinimiseAllBut.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
			this.labelMinimiseAllBut.Location = new System.Drawing.Point(236, 91);
			this.labelMinimiseAllBut.Name = "labelMinimiseAllBut";
			this.labelMinimiseAllBut.Size = new System.Drawing.Size(153, 13);
			this.labelMinimiseAllBut.TabIndex = 14;
			this.labelMinimiseAllBut.Text = "labelMinimiseAllBut";
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(6, 91);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(224, 13);
			this.label8.TabIndex = 13;
			this.label8.Text = "Minimise all but active window";
			// 
			// buttonMinimise
			// 
			this.buttonMinimise.Location = new System.Drawing.Point(395, 62);
			this.buttonMinimise.Name = "buttonMinimise";
			this.buttonMinimise.Size = new System.Drawing.Size(75, 23);
			this.buttonMinimise.TabIndex = 12;
			this.buttonMinimise.Text = "Change...";
			this.buttonMinimise.UseVisualStyleBackColor = true;
			this.buttonMinimise.Click += new System.EventHandler(this.buttonMinimise_Click);
			// 
			// labelMinimise
			// 
			this.labelMinimise.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
			this.labelMinimise.Location = new System.Drawing.Point(236, 67);
			this.labelMinimise.Name = "labelMinimise";
			this.labelMinimise.Size = new System.Drawing.Size(153, 13);
			this.labelMinimise.TabIndex = 11;
			this.labelMinimise.Text = "labelMinimise";
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(6, 67);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(224, 13);
			this.label6.TabIndex = 10;
			this.label6.Text = "Minimise active Window";
			// 
			// buttonPreviousScreen
			// 
			this.buttonPreviousScreen.Location = new System.Drawing.Point(395, 38);
			this.buttonPreviousScreen.Name = "buttonPreviousScreen";
			this.buttonPreviousScreen.Size = new System.Drawing.Size(75, 23);
			this.buttonPreviousScreen.TabIndex = 9;
			this.buttonPreviousScreen.Text = "Change...";
			this.buttonPreviousScreen.UseVisualStyleBackColor = true;
			this.buttonPreviousScreen.Click += new System.EventHandler(this.buttonPreviousScreen_Click);
			// 
			// labelPrevScreen
			// 
			this.labelPrevScreen.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
			this.labelPrevScreen.Location = new System.Drawing.Point(236, 43);
			this.labelPrevScreen.Name = "labelPrevScreen";
			this.labelPrevScreen.Size = new System.Drawing.Size(153, 13);
			this.labelPrevScreen.TabIndex = 8;
			this.labelPrevScreen.Text = "labelPrevScreen";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(6, 43);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(224, 13);
			this.label3.TabIndex = 7;
			this.label3.Text = "Move active window to previous screen";
			// 
			// labelNextScreen
			// 
			this.labelNextScreen.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
			this.labelNextScreen.Location = new System.Drawing.Point(236, 19);
			this.labelNextScreen.Name = "labelNextScreen";
			this.labelNextScreen.Size = new System.Drawing.Size(153, 13);
			this.labelNextScreen.TabIndex = 6;
			this.labelNextScreen.Text = "labelNextScreen";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(6, 19);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(224, 13);
			this.label1.TabIndex = 5;
			this.label1.Text = "Move active window to next screen";
			// 
			// OptionsForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(513, 325);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.buttonCancel);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "OptionsForm";
			this.Text = "Options for SwapScreen";
			this.Shown += new System.EventHandler(this.OptionsForm_Shown);
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OptionsForm_FormClosing);
			this.contextMenuStrip.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.NotifyIcon notifyIcon;
		private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemOptions;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemAbout;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemExit;
		private System.Windows.Forms.Button buttonCancel;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripMenuItem swapScreensToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripMenuItem visitSwapScreenWebsiteToolStripMenuItem;
		private System.Windows.Forms.Button buttonNextScreen;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Label labelNextScreen;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button buttonPreviousScreen;
		private System.Windows.Forms.Label labelPrevScreen;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Button buttonDesktop2;
		private System.Windows.Forms.Label labelShowDesktop2;
		private System.Windows.Forms.Label label20;
		private System.Windows.Forms.Button buttonDesktop1;
		private System.Windows.Forms.Label labelShowDesktop1;
		private System.Windows.Forms.Label label18;
		private System.Windows.Forms.Button buttonRotatePrev;
		private System.Windows.Forms.Label labelRotatePrev;
		private System.Windows.Forms.Label label16;
		private System.Windows.Forms.Button buttonRotateNext;
		private System.Windows.Forms.Label labelRotateNext;
		private System.Windows.Forms.Label label14;
		private System.Windows.Forms.Button buttonSuperSize;
		private System.Windows.Forms.Label labelSupersize;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.Button buttonMaximise;
		private System.Windows.Forms.Label labelMaximise;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Button buttonMinimiseAllBut;
		private System.Windows.Forms.Label labelMinimiseAllBut;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Button buttonMinimise;
		private System.Windows.Forms.Label labelMinimise;
		private System.Windows.Forms.Label label6;
	}
}

