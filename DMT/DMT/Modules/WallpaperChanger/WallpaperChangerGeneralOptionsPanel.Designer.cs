namespace DMT.Modules.WallpaperChanger
{
	partial class WallpaperChangerGeneralOptionsPanel
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
			this.labelNextChange = new System.Windows.Forms.Label();
			this.buttonChangeWallpaper = new System.Windows.Forms.Button();
			this.pictureBoxBackgroundColour = new System.Windows.Forms.PictureBox();
			this.comboBoxMultiMonitor = new System.Windows.Forms.ComboBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.checkBoxChangePeriodically = new System.Windows.Forms.CheckBox();
			this.labelPeriod3 = new System.Windows.Forms.Label();
			this.numericUpDownMinutes = new System.Windows.Forms.NumericUpDown();
			this.labelPeriod2 = new System.Windows.Forms.Label();
			this.numericUpDownHours = new System.Windows.Forms.NumericUpDown();
			this.labelPeriod1 = new System.Windows.Forms.Label();
			this.checkBoxChangeOnStart = new System.Windows.Forms.CheckBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.hotKeyPanelChangeWallpaper = new DMT.Library.HotKeys.HotKeyPanel();
			this.checkBoxFade = new System.Windows.Forms.CheckBox();
			this.checkBoxChangeOnResolutionChange = new System.Windows.Forms.CheckBox();
			this.checkBoxFitAspectRatio = new System.Windows.Forms.CheckBox();
			this.checkBoxFitClip = new System.Windows.Forms.CheckBox();
			this.checkBoxFitEnlarge = new System.Windows.Forms.CheckBox();
			this.checkBoxFitShrink = new System.Windows.Forms.CheckBox();
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxBackgroundColour)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownMinutes)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownHours)).BeginInit();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// labelNextChange
			// 
			this.labelNextChange.Location = new System.Drawing.Point(161, 312);
			this.labelNextChange.Name = "labelNextChange";
			this.labelNextChange.Size = new System.Drawing.Size(329, 41);
			this.labelNextChange.TabIndex = 29;
			this.labelNextChange.Text = "Next change in";
			// 
			// buttonChangeWallpaper
			// 
			this.buttonChangeWallpaper.Location = new System.Drawing.Point(10, 307);
			this.buttonChangeWallpaper.Name = "buttonChangeWallpaper";
			this.buttonChangeWallpaper.Size = new System.Drawing.Size(145, 23);
			this.buttonChangeWallpaper.TabIndex = 27;
			this.buttonChangeWallpaper.Text = "Change Wallpaper Now";
			this.buttonChangeWallpaper.UseVisualStyleBackColor = true;
			this.buttonChangeWallpaper.Click += new System.EventHandler(this.buttonChangeWallpaper_Click);
			// 
			// pictureBoxBackgroundColour
			// 
			this.pictureBoxBackgroundColour.BackColor = System.Drawing.Color.Transparent;
			this.pictureBoxBackgroundColour.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pictureBoxBackgroundColour.Location = new System.Drawing.Point(114, 218);
			this.pictureBoxBackgroundColour.Name = "pictureBoxBackgroundColour";
			this.pictureBoxBackgroundColour.Size = new System.Drawing.Size(376, 21);
			this.pictureBoxBackgroundColour.TabIndex = 28;
			this.pictureBoxBackgroundColour.TabStop = false;
			this.pictureBoxBackgroundColour.Click += new System.EventHandler(this.pictureBoxBackgroundColour_Click);
			// 
			// comboBoxMultiMonitor
			// 
			this.comboBoxMultiMonitor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxMultiMonitor.FormattingEnabled = true;
			this.comboBoxMultiMonitor.Location = new System.Drawing.Point(114, 135);
			this.comboBoxMultiMonitor.Name = "comboBoxMultiMonitor";
			this.comboBoxMultiMonitor.Size = new System.Drawing.Size(376, 21);
			this.comboBoxMultiMonitor.TabIndex = 23;
			this.comboBoxMultiMonitor.SelectedIndexChanged += new System.EventHandler(this.comboBoxMultiMonitor_SelectedIndexChanged);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(7, 138);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(70, 13);
			this.label3.TabIndex = 22;
			this.label3.Text = "Multi Monitor:";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(7, 222);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(101, 13);
			this.label2.TabIndex = 26;
			this.label2.Text = "Background Colour:";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(7, 165);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(21, 13);
			this.label1.TabIndex = 24;
			this.label1.Text = "Fit:";
			// 
			// checkBoxChangePeriodically
			// 
			this.checkBoxChangePeriodically.AutoSize = true;
			this.checkBoxChangePeriodically.Location = new System.Drawing.Point(3, 49);
			this.checkBoxChangePeriodically.Name = "checkBoxChangePeriodically";
			this.checkBoxChangePeriodically.Size = new System.Drawing.Size(166, 17);
			this.checkBoxChangePeriodically.TabIndex = 16;
			this.checkBoxChangePeriodically.Text = "Change wallpaper periodically";
			this.checkBoxChangePeriodically.UseVisualStyleBackColor = true;
			this.checkBoxChangePeriodically.CheckedChanged += new System.EventHandler(this.checkBoxChangePeriodically_CheckedChanged);
			// 
			// labelPeriod3
			// 
			this.labelPeriod3.AutoSize = true;
			this.labelPeriod3.Location = new System.Drawing.Point(310, 102);
			this.labelPeriod3.Name = "labelPeriod3";
			this.labelPeriod3.Size = new System.Drawing.Size(44, 13);
			this.labelPeriod3.TabIndex = 21;
			this.labelPeriod3.Text = "Minutes";
			// 
			// numericUpDownMinutes
			// 
			this.numericUpDownMinutes.Location = new System.Drawing.Point(243, 100);
			this.numericUpDownMinutes.Maximum = new decimal(new int[] {
            59,
            0,
            0,
            0});
			this.numericUpDownMinutes.Name = "numericUpDownMinutes";
			this.numericUpDownMinutes.Size = new System.Drawing.Size(61, 20);
			this.numericUpDownMinutes.TabIndex = 20;
			this.numericUpDownMinutes.ValueChanged += new System.EventHandler(this.numericUpDownMinutes_ValueChanged);
			// 
			// labelPeriod2
			// 
			this.labelPeriod2.AutoSize = true;
			this.labelPeriod2.Location = new System.Drawing.Point(188, 102);
			this.labelPeriod2.Name = "labelPeriod2";
			this.labelPeriod2.Size = new System.Drawing.Size(35, 13);
			this.labelPeriod2.TabIndex = 19;
			this.labelPeriod2.Text = "Hours";
			// 
			// numericUpDownHours
			// 
			this.numericUpDownHours.Location = new System.Drawing.Point(129, 100);
			this.numericUpDownHours.Maximum = new decimal(new int[] {
            24,
            0,
            0,
            0});
			this.numericUpDownHours.Name = "numericUpDownHours";
			this.numericUpDownHours.Size = new System.Drawing.Size(53, 20);
			this.numericUpDownHours.TabIndex = 18;
			this.numericUpDownHours.ValueChanged += new System.EventHandler(this.numericUpDownHours_ValueChanged);
			// 
			// labelPeriod1
			// 
			this.labelPeriod1.AutoSize = true;
			this.labelPeriod1.Location = new System.Drawing.Point(2, 102);
			this.labelPeriod1.Name = "labelPeriod1";
			this.labelPeriod1.Size = new System.Drawing.Size(121, 13);
			this.labelPeriod1.TabIndex = 17;
			this.labelPeriod1.Text = "Time between changes:";
			// 
			// checkBoxChangeOnStart
			// 
			this.checkBoxChangeOnStart.AutoSize = true;
			this.checkBoxChangeOnStart.Location = new System.Drawing.Point(3, 3);
			this.checkBoxChangeOnStart.Name = "checkBoxChangeOnStart";
			this.checkBoxChangeOnStart.Size = new System.Drawing.Size(161, 17);
			this.checkBoxChangeOnStart.TabIndex = 15;
			this.checkBoxChangeOnStart.Text = "Change wallpaper on startup";
			this.checkBoxChangeOnStart.UseVisualStyleBackColor = true;
			this.checkBoxChangeOnStart.CheckedChanged += new System.EventHandler(this.checkBoxChangeOnStart_CheckedChanged);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.hotKeyPanelChangeWallpaper);
			this.groupBox1.Location = new System.Drawing.Point(10, 245);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(480, 56);
			this.groupBox1.TabIndex = 30;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "HotKey";
			// 
			// hotKeyPanelChangeWallpaper
			// 
			this.hotKeyPanelChangeWallpaper.Description = "Description";
			this.hotKeyPanelChangeWallpaper.Location = new System.Drawing.Point(6, 19);
			this.hotKeyPanelChangeWallpaper.Name = "hotKeyPanelChangeWallpaper";
			this.hotKeyPanelChangeWallpaper.Size = new System.Drawing.Size(465, 23);
			this.hotKeyPanelChangeWallpaper.TabIndex = 1;
			// 
			// checkBoxFade
			// 
			this.checkBoxFade.AutoSize = true;
			this.checkBoxFade.Location = new System.Drawing.Point(3, 72);
			this.checkBoxFade.Name = "checkBoxFade";
			this.checkBoxFade.Size = new System.Drawing.Size(230, 17);
			this.checkBoxFade.TabIndex = 31;
			this.checkBoxFade.Text = "Use smooth fade when changing wallpaper";
			this.checkBoxFade.UseVisualStyleBackColor = true;
			this.checkBoxFade.CheckedChanged += new System.EventHandler(this.checkBoxFade_CheckedChanged);
			// 
			// checkBoxChangeOnResolutionChange
			// 
			this.checkBoxChangeOnResolutionChange.AutoSize = true;
			this.checkBoxChangeOnResolutionChange.Location = new System.Drawing.Point(3, 26);
			this.checkBoxChangeOnResolutionChange.Name = "checkBoxChangeOnResolutionChange";
			this.checkBoxChangeOnResolutionChange.Size = new System.Drawing.Size(234, 17);
			this.checkBoxChangeOnResolutionChange.TabIndex = 32;
			this.checkBoxChangeOnResolutionChange.Text = "Change when monitors / resolutions change";
			this.checkBoxChangeOnResolutionChange.UseVisualStyleBackColor = true;
			this.checkBoxChangeOnResolutionChange.CheckedChanged += new System.EventHandler(this.checkBoxChangeOnResolutionChange_CheckedChanged);
			// 
			// checkBoxFitAspectRatio
			// 
			this.checkBoxFitAspectRatio.AutoSize = true;
			this.checkBoxFitAspectRatio.Location = new System.Drawing.Point(114, 165);
			this.checkBoxFitAspectRatio.Name = "checkBoxFitAspectRatio";
			this.checkBoxFitAspectRatio.Size = new System.Drawing.Size(124, 17);
			this.checkBoxFitAspectRatio.TabIndex = 33;
			this.checkBoxFitAspectRatio.Text = "Maintain aspect ratio";
			this.checkBoxFitAspectRatio.UseVisualStyleBackColor = true;
			this.checkBoxFitAspectRatio.CheckedChanged += new System.EventHandler(this.UpdateFitMode);
			// 
			// checkBoxFitClip
			// 
			this.checkBoxFitClip.AutoSize = true;
			this.checkBoxFitClip.Location = new System.Drawing.Point(114, 188);
			this.checkBoxFitClip.Name = "checkBoxFitClip";
			this.checkBoxFitClip.Size = new System.Drawing.Size(163, 17);
			this.checkBoxFitClip.TabIndex = 34;
			this.checkBoxFitClip.Text = "Avoid horizontal/vertical bars";
			this.checkBoxFitClip.UseVisualStyleBackColor = true;
			this.checkBoxFitClip.CheckedChanged += new System.EventHandler(this.UpdateFitMode);
			// 
			// checkBoxFitEnlarge
			// 
			this.checkBoxFitEnlarge.AutoSize = true;
			this.checkBoxFitEnlarge.Location = new System.Drawing.Point(313, 165);
			this.checkBoxFitEnlarge.Name = "checkBoxFitEnlarge";
			this.checkBoxFitEnlarge.Size = new System.Drawing.Size(169, 17);
			this.checkBoxFitEnlarge.TabIndex = 35;
			this.checkBoxFitEnlarge.Text = "Expand image (if needed) to fit";
			this.checkBoxFitEnlarge.UseVisualStyleBackColor = true;
			this.checkBoxFitEnlarge.CheckedChanged += new System.EventHandler(this.UpdateFitMode);
			// 
			// checkBoxFitShrink
			// 
			this.checkBoxFitShrink.AutoSize = true;
			this.checkBoxFitShrink.Location = new System.Drawing.Point(313, 188);
			this.checkBoxFitShrink.Name = "checkBoxFitShrink";
			this.checkBoxFitShrink.Size = new System.Drawing.Size(163, 17);
			this.checkBoxFitShrink.TabIndex = 36;
			this.checkBoxFitShrink.Text = "Shrink image (if needed) to fit";
			this.checkBoxFitShrink.UseVisualStyleBackColor = true;
			this.checkBoxFitShrink.CheckedChanged += new System.EventHandler(this.UpdateFitMode);
			// 
			// WallpaperChangerGeneralOptionsPanel
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.checkBoxFitShrink);
			this.Controls.Add(this.checkBoxFitEnlarge);
			this.Controls.Add(this.checkBoxFitClip);
			this.Controls.Add(this.checkBoxFitAspectRatio);
			this.Controls.Add(this.checkBoxChangeOnResolutionChange);
			this.Controls.Add(this.checkBoxFade);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.labelNextChange);
			this.Controls.Add(this.buttonChangeWallpaper);
			this.Controls.Add(this.pictureBoxBackgroundColour);
			this.Controls.Add(this.comboBoxMultiMonitor);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.checkBoxChangePeriodically);
			this.Controls.Add(this.labelPeriod3);
			this.Controls.Add(this.numericUpDownMinutes);
			this.Controls.Add(this.labelPeriod2);
			this.Controls.Add(this.numericUpDownHours);
			this.Controls.Add(this.labelPeriod1);
			this.Controls.Add(this.checkBoxChangeOnStart);
			this.Name = "WallpaperChangerGeneralOptionsPanel";
			this.Size = new System.Drawing.Size(500, 360);
			this.Load += new System.EventHandler(this.WallpaperChangerGeneralOptionsPanel_Load);
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxBackgroundColour)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownMinutes)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownHours)).EndInit();
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label labelNextChange;
		private System.Windows.Forms.Button buttonChangeWallpaper;
		private System.Windows.Forms.PictureBox pictureBoxBackgroundColour;
		private System.Windows.Forms.ComboBox comboBoxMultiMonitor;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.CheckBox checkBoxChangePeriodically;
		private System.Windows.Forms.Label labelPeriod3;
		private System.Windows.Forms.NumericUpDown numericUpDownMinutes;
		private System.Windows.Forms.Label labelPeriod2;
		private System.Windows.Forms.NumericUpDown numericUpDownHours;
		private System.Windows.Forms.Label labelPeriod1;
		private System.Windows.Forms.CheckBox checkBoxChangeOnStart;
		private System.Windows.Forms.GroupBox groupBox1;
		private Library.HotKeys.HotKeyPanel hotKeyPanelChangeWallpaper;
		private System.Windows.Forms.CheckBox checkBoxFade;
		private System.Windows.Forms.CheckBox checkBoxChangeOnResolutionChange;
		private System.Windows.Forms.CheckBox checkBoxFitAspectRatio;
		private System.Windows.Forms.CheckBox checkBoxFitClip;
		private System.Windows.Forms.CheckBox checkBoxFitEnlarge;
		private System.Windows.Forms.CheckBox checkBoxFitShrink;
	}
}
