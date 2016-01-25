namespace DMT.Modules.General
{
	partial class GeneralOptionsPanel
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
			this.checkBoxAutoStart = new System.Windows.Forms.CheckBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.textBoxExecutable = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.textBoxLog = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.textBoxWallpaperProviders = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.textBoxMagicWords = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.textBoxSettings = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.buttonCheckUpdates = new System.Windows.Forms.Button();
			this.labelInstallType = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.labelVersion = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.SuspendLayout();
			// 
			// checkBoxAutoStart
			// 
			this.checkBoxAutoStart.AutoSize = true;
			this.checkBoxAutoStart.Location = new System.Drawing.Point(3, 12);
			this.checkBoxAutoStart.Name = "checkBoxAutoStart";
			this.checkBoxAutoStart.Size = new System.Drawing.Size(152, 17);
			this.checkBoxAutoStart.TabIndex = 7;
			this.checkBoxAutoStart.Text = "Start when Windows starts";
			this.checkBoxAutoStart.UseVisualStyleBackColor = true;
			this.checkBoxAutoStart.CheckedChanged += new System.EventHandler(this.checkBoxAutoStart_CheckedChanged);
			// 
			// groupBox1
			// 
			this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox1.Controls.Add(this.textBoxExecutable);
			this.groupBox1.Controls.Add(this.label5);
			this.groupBox1.Controls.Add(this.textBoxLog);
			this.groupBox1.Controls.Add(this.label4);
			this.groupBox1.Controls.Add(this.textBoxWallpaperProviders);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.textBoxMagicWords);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.textBoxSettings);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Location = new System.Drawing.Point(3, 167);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(479, 159);
			this.groupBox1.TabIndex = 8;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "File Locations";
			// 
			// textBoxExecutable
			// 
			this.textBoxExecutable.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxExecutable.Location = new System.Drawing.Point(117, 17);
			this.textBoxExecutable.Name = "textBoxExecutable";
			this.textBoxExecutable.ReadOnly = true;
			this.textBoxExecutable.Size = new System.Drawing.Size(348, 20);
			this.textBoxExecutable.TabIndex = 9;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(6, 20);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(63, 13);
			this.label5.TabIndex = 8;
			this.label5.Text = "Executable:";
			// 
			// textBoxLog
			// 
			this.textBoxLog.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxLog.Location = new System.Drawing.Point(117, 121);
			this.textBoxLog.Name = "textBoxLog";
			this.textBoxLog.ReadOnly = true;
			this.textBoxLog.Size = new System.Drawing.Size(348, 20);
			this.textBoxLog.TabIndex = 7;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(6, 124);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(28, 13);
			this.label4.TabIndex = 6;
			this.label4.Text = "Log:";
			// 
			// textBoxWallpaperProviders
			// 
			this.textBoxWallpaperProviders.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxWallpaperProviders.Location = new System.Drawing.Point(117, 95);
			this.textBoxWallpaperProviders.Name = "textBoxWallpaperProviders";
			this.textBoxWallpaperProviders.ReadOnly = true;
			this.textBoxWallpaperProviders.Size = new System.Drawing.Size(348, 20);
			this.textBoxWallpaperProviders.TabIndex = 5;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(6, 98);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(105, 13);
			this.label3.TabIndex = 4;
			this.label3.Text = "Wallpaper Providers:";
			// 
			// textBoxMagicWords
			// 
			this.textBoxMagicWords.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxMagicWords.Location = new System.Drawing.Point(117, 69);
			this.textBoxMagicWords.Name = "textBoxMagicWords";
			this.textBoxMagicWords.ReadOnly = true;
			this.textBoxMagicWords.Size = new System.Drawing.Size(348, 20);
			this.textBoxMagicWords.TabIndex = 3;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(6, 72);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(73, 13);
			this.label2.TabIndex = 2;
			this.label2.Text = "Magic Words:";
			// 
			// textBoxSettings
			// 
			this.textBoxSettings.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxSettings.Location = new System.Drawing.Point(117, 43);
			this.textBoxSettings.Name = "textBoxSettings";
			this.textBoxSettings.ReadOnly = true;
			this.textBoxSettings.Size = new System.Drawing.Size(348, 20);
			this.textBoxSettings.TabIndex = 1;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(6, 46);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(48, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Settings:";
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.buttonCheckUpdates);
			this.groupBox2.Controls.Add(this.labelInstallType);
			this.groupBox2.Controls.Add(this.label8);
			this.groupBox2.Controls.Add(this.labelVersion);
			this.groupBox2.Controls.Add(this.label6);
			this.groupBox2.Location = new System.Drawing.Point(3, 35);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(479, 111);
			this.groupBox2.TabIndex = 9;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Installed Version";
			// 
			// buttonCheckUpdates
			// 
			this.buttonCheckUpdates.Location = new System.Drawing.Point(9, 71);
			this.buttonCheckUpdates.Name = "buttonCheckUpdates";
			this.buttonCheckUpdates.Size = new System.Drawing.Size(143, 23);
			this.buttonCheckUpdates.TabIndex = 4;
			this.buttonCheckUpdates.Text = "Check for Updates";
			this.buttonCheckUpdates.UseVisualStyleBackColor = true;
			this.buttonCheckUpdates.Click += new System.EventHandler(this.buttonCheckUpdates_Click);
			// 
			// labelInstallType
			// 
			this.labelInstallType.Location = new System.Drawing.Point(114, 46);
			this.labelInstallType.Name = "labelInstallType";
			this.labelInstallType.Size = new System.Drawing.Size(351, 13);
			this.labelInstallType.TabIndex = 3;
			this.labelInstallType.Text = "label9";
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(6, 46);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(64, 13);
			this.label8.TabIndex = 2;
			this.label8.Text = "Install Type:";
			// 
			// labelVersion
			// 
			this.labelVersion.Location = new System.Drawing.Point(114, 20);
			this.labelVersion.Name = "labelVersion";
			this.labelVersion.Size = new System.Drawing.Size(351, 13);
			this.labelVersion.TabIndex = 1;
			this.labelVersion.Text = "label7";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(6, 20);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(45, 13);
			this.label6.TabIndex = 0;
			this.label6.Text = "Version:";
			// 
			// GeneralOptionsPanel
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.checkBoxAutoStart);
			this.Name = "GeneralOptionsPanel";
			this.Size = new System.Drawing.Size(500, 338);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.CheckBox checkBoxAutoStart;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.TextBox textBoxLog;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox textBoxWallpaperProviders;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox textBoxMagicWords;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox textBoxSettings;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox textBoxExecutable;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Button buttonCheckUpdates;
		private System.Windows.Forms.Label labelInstallType;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label labelVersion;
		private System.Windows.Forms.Label label6;

	}
}
