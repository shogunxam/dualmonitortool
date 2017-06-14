namespace DMT.Modules.WallpaperChanger.Plugins.LocalDisk
{
	partial class LocalDiskForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LocalDiskForm));
			this.buttonBrowse = new System.Windows.Forms.Button();
			this.textBoxDirectory = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.textBoxDescription = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.numericUpDownWeight = new System.Windows.Forms.NumericUpDown();
			this.label1 = new System.Windows.Forms.Label();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.label4 = new System.Windows.Forms.Label();
			this.buttonCancel = new System.Windows.Forms.Button();
			this.buttonOK = new System.Windows.Forms.Button();
			this.checkBoxRescan = new System.Windows.Forms.CheckBox();
			this.checkBoxRecursive = new System.Windows.Forms.CheckBox();
			this.buttonBrowsePortrait = new System.Windows.Forms.Button();
			this.textBoxPortraitDirectory = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.buttonBrowseMonitor4 = new System.Windows.Forms.Button();
			this.textBoxMonitor4Directory = new System.Windows.Forms.TextBox();
			this.label9 = new System.Windows.Forms.Label();
			this.buttonBrowseMonitor3 = new System.Windows.Forms.Button();
			this.textBoxMonitor3Directory = new System.Windows.Forms.TextBox();
			this.label8 = new System.Windows.Forms.Label();
			this.buttonBrowseMonitor2 = new System.Windows.Forms.Button();
			this.textBoxMonitor2Directory = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.checkBoxAllowFileBrowse = new System.Windows.Forms.CheckBox();
			this.buttonBrowseMonitor1 = new System.Windows.Forms.Button();
			this.textBoxMonitor1Directory = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.checkBoxCycle = new System.Windows.Forms.CheckBox();
			this.checkBoxEnabled = new System.Windows.Forms.CheckBox();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownWeight)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// buttonBrowse
			// 
			this.buttonBrowse.Location = new System.Drawing.Point(579, 167);
			this.buttonBrowse.Name = "buttonBrowse";
			this.buttonBrowse.Size = new System.Drawing.Size(75, 23);
			this.buttonBrowse.TabIndex = 18;
			this.buttonBrowse.Text = "Browse...";
			this.buttonBrowse.UseVisualStyleBackColor = true;
			this.buttonBrowse.Click += new System.EventHandler(this.buttonBrowse_Click);
			// 
			// textBoxDirectory
			// 
			this.textBoxDirectory.Location = new System.Drawing.Point(146, 169);
			this.textBoxDirectory.Name = "textBoxDirectory";
			this.textBoxDirectory.Size = new System.Drawing.Size(422, 20);
			this.textBoxDirectory.TabIndex = 17;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(6, 172);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(44, 13);
			this.label3.TabIndex = 16;
			this.label3.Text = "Default:";
			// 
			// textBoxDescription
			// 
			this.textBoxDescription.Location = new System.Drawing.Point(158, 112);
			this.textBoxDescription.Name = "textBoxDescription";
			this.textBoxDescription.Size = new System.Drawing.Size(422, 20);
			this.textBoxDescription.TabIndex = 5;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(9, 115);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(63, 13);
			this.label2.TabIndex = 4;
			this.label2.Text = "Description:";
			// 
			// numericUpDownWeight
			// 
			this.numericUpDownWeight.Location = new System.Drawing.Point(158, 86);
			this.numericUpDownWeight.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
			this.numericUpDownWeight.Name = "numericUpDownWeight";
			this.numericUpDownWeight.Size = new System.Drawing.Size(120, 20);
			this.numericUpDownWeight.TabIndex = 2;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 89);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(44, 13);
			this.label1.TabIndex = 1;
			this.label1.Text = "Weight:";
			// 
			// pictureBox1
			// 
			this.pictureBox1.Image = global::DMT.Properties.Resources.LocalDiskPlugin;
			this.pictureBox1.Location = new System.Drawing.Point(12, 12);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(48, 48);
			this.pictureBox1.TabIndex = 27;
			this.pictureBox1.TabStop = false;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(107, 30);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(237, 13);
			this.label4.TabIndex = 0;
			this.label4.Text = "Image provider to get images stored on local disk";
			// 
			// buttonCancel
			// 
			this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonCancel.Location = new System.Drawing.Point(364, 457);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new System.Drawing.Size(75, 23);
			this.buttonCancel.TabIndex = 11;
			this.buttonCancel.Text = "Cancel";
			this.buttonCancel.UseVisualStyleBackColor = true;
			this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
			// 
			// buttonOK
			// 
			this.buttonOK.Location = new System.Drawing.Point(218, 457);
			this.buttonOK.Name = "buttonOK";
			this.buttonOK.Size = new System.Drawing.Size(75, 23);
			this.buttonOK.TabIndex = 10;
			this.buttonOK.Text = "OK";
			this.buttonOK.UseVisualStyleBackColor = true;
			this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
			// 
			// checkBoxRescan
			// 
			this.checkBoxRescan.AutoSize = true;
			this.checkBoxRescan.Location = new System.Drawing.Point(12, 384);
			this.checkBoxRescan.Name = "checkBoxRescan";
			this.checkBoxRescan.Size = new System.Drawing.Size(229, 17);
			this.checkBoxRescan.TabIndex = 8;
			this.checkBoxRescan.Text = "Rescan folder(s) before getting each image";
			this.checkBoxRescan.UseVisualStyleBackColor = true;
			this.checkBoxRescan.CheckedChanged += new System.EventHandler(this.checkBoxRescan_CheckedChanged);
			// 
			// checkBoxRecursive
			// 
			this.checkBoxRecursive.AutoSize = true;
			this.checkBoxRecursive.Location = new System.Drawing.Point(12, 361);
			this.checkBoxRecursive.Name = "checkBoxRecursive";
			this.checkBoxRecursive.Size = new System.Drawing.Size(115, 17);
			this.checkBoxRecursive.TabIndex = 7;
			this.checkBoxRecursive.Text = "Look in sub-folders";
			this.checkBoxRecursive.UseVisualStyleBackColor = true;
			// 
			// buttonBrowsePortrait
			// 
			this.buttonBrowsePortrait.Location = new System.Drawing.Point(579, 141);
			this.buttonBrowsePortrait.Name = "buttonBrowsePortrait";
			this.buttonBrowsePortrait.Size = new System.Drawing.Size(75, 23);
			this.buttonBrowsePortrait.TabIndex = 15;
			this.buttonBrowsePortrait.Text = "Browse...";
			this.buttonBrowsePortrait.UseVisualStyleBackColor = true;
			this.buttonBrowsePortrait.Click += new System.EventHandler(this.buttonBrowsePortrait_Click);
			// 
			// textBoxPortraitDirectory
			// 
			this.textBoxPortraitDirectory.Location = new System.Drawing.Point(146, 144);
			this.textBoxPortraitDirectory.Name = "textBoxPortraitDirectory";
			this.textBoxPortraitDirectory.Size = new System.Drawing.Size(422, 20);
			this.textBoxPortraitDirectory.TabIndex = 14;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(6, 146);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(86, 13);
			this.label5.TabIndex = 13;
			this.label5.Text = "Portrait Monitors:";
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.buttonBrowseMonitor4);
			this.groupBox1.Controls.Add(this.buttonBrowsePortrait);
			this.groupBox1.Controls.Add(this.textBoxMonitor4Directory);
			this.groupBox1.Controls.Add(this.textBoxPortraitDirectory);
			this.groupBox1.Controls.Add(this.label9);
			this.groupBox1.Controls.Add(this.label5);
			this.groupBox1.Controls.Add(this.buttonBrowseMonitor3);
			this.groupBox1.Controls.Add(this.buttonBrowse);
			this.groupBox1.Controls.Add(this.textBoxMonitor3Directory);
			this.groupBox1.Controls.Add(this.textBoxDirectory);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.label8);
			this.groupBox1.Controls.Add(this.buttonBrowseMonitor2);
			this.groupBox1.Controls.Add(this.textBoxMonitor2Directory);
			this.groupBox1.Controls.Add(this.label7);
			this.groupBox1.Controls.Add(this.checkBoxAllowFileBrowse);
			this.groupBox1.Controls.Add(this.buttonBrowseMonitor1);
			this.groupBox1.Controls.Add(this.textBoxMonitor1Directory);
			this.groupBox1.Controls.Add(this.label6);
			this.groupBox1.Location = new System.Drawing.Point(12, 138);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(668, 206);
			this.groupBox1.TabIndex = 6;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Folder(s) to take images from (in priority order)";
			// 
			// buttonBrowseMonitor4
			// 
			this.buttonBrowseMonitor4.Location = new System.Drawing.Point(579, 115);
			this.buttonBrowseMonitor4.Name = "buttonBrowseMonitor4";
			this.buttonBrowseMonitor4.Size = new System.Drawing.Size(75, 23);
			this.buttonBrowseMonitor4.TabIndex = 12;
			this.buttonBrowseMonitor4.Text = "Browse...";
			this.buttonBrowseMonitor4.UseVisualStyleBackColor = true;
			this.buttonBrowseMonitor4.Click += new System.EventHandler(this.buttonBrowseMonitor4_Click);
			// 
			// textBoxMonitor4Directory
			// 
			this.textBoxMonitor4Directory.Location = new System.Drawing.Point(146, 117);
			this.textBoxMonitor4Directory.Name = "textBoxMonitor4Directory";
			this.textBoxMonitor4Directory.Size = new System.Drawing.Size(422, 20);
			this.textBoxMonitor4Directory.TabIndex = 11;
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.Location = new System.Drawing.Point(6, 120);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(54, 13);
			this.label9.TabIndex = 10;
			this.label9.Text = "Monitor 4:";
			// 
			// buttonBrowseMonitor3
			// 
			this.buttonBrowseMonitor3.Location = new System.Drawing.Point(579, 89);
			this.buttonBrowseMonitor3.Name = "buttonBrowseMonitor3";
			this.buttonBrowseMonitor3.Size = new System.Drawing.Size(75, 23);
			this.buttonBrowseMonitor3.TabIndex = 9;
			this.buttonBrowseMonitor3.Text = "Browse...";
			this.buttonBrowseMonitor3.UseVisualStyleBackColor = true;
			this.buttonBrowseMonitor3.Click += new System.EventHandler(this.buttonBrowseMonitor3_Click);
			// 
			// textBoxMonitor3Directory
			// 
			this.textBoxMonitor3Directory.Location = new System.Drawing.Point(146, 91);
			this.textBoxMonitor3Directory.Name = "textBoxMonitor3Directory";
			this.textBoxMonitor3Directory.Size = new System.Drawing.Size(422, 20);
			this.textBoxMonitor3Directory.TabIndex = 8;
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(6, 94);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(54, 13);
			this.label8.TabIndex = 7;
			this.label8.Text = "Monitor 3:";
			// 
			// buttonBrowseMonitor2
			// 
			this.buttonBrowseMonitor2.Location = new System.Drawing.Point(579, 63);
			this.buttonBrowseMonitor2.Name = "buttonBrowseMonitor2";
			this.buttonBrowseMonitor2.Size = new System.Drawing.Size(75, 23);
			this.buttonBrowseMonitor2.TabIndex = 6;
			this.buttonBrowseMonitor2.Text = "Browse...";
			this.buttonBrowseMonitor2.UseVisualStyleBackColor = true;
			this.buttonBrowseMonitor2.Click += new System.EventHandler(this.buttonBrowseMonitor2_Click);
			// 
			// textBoxMonitor2Directory
			// 
			this.textBoxMonitor2Directory.Location = new System.Drawing.Point(146, 65);
			this.textBoxMonitor2Directory.Name = "textBoxMonitor2Directory";
			this.textBoxMonitor2Directory.Size = new System.Drawing.Size(422, 20);
			this.textBoxMonitor2Directory.TabIndex = 5;
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(6, 68);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(54, 13);
			this.label7.TabIndex = 4;
			this.label7.Text = "Monitor 2:";
			// 
			// checkBoxAllowFileBrowse
			// 
			this.checkBoxAllowFileBrowse.AutoSize = true;
			this.checkBoxAllowFileBrowse.Location = new System.Drawing.Point(506, 13);
			this.checkBoxAllowFileBrowse.Name = "checkBoxAllowFileBrowse";
			this.checkBoxAllowFileBrowse.Size = new System.Drawing.Size(148, 17);
			this.checkBoxAllowFileBrowse.TabIndex = 0;
			this.checkBoxAllowFileBrowse.Text = "File Browse (experimental)";
			this.checkBoxAllowFileBrowse.UseVisualStyleBackColor = true;
			// 
			// buttonBrowseMonitor1
			// 
			this.buttonBrowseMonitor1.Location = new System.Drawing.Point(579, 37);
			this.buttonBrowseMonitor1.Name = "buttonBrowseMonitor1";
			this.buttonBrowseMonitor1.Size = new System.Drawing.Size(75, 23);
			this.buttonBrowseMonitor1.TabIndex = 3;
			this.buttonBrowseMonitor1.Text = "Browse...";
			this.buttonBrowseMonitor1.UseVisualStyleBackColor = true;
			this.buttonBrowseMonitor1.Click += new System.EventHandler(this.buttonBrowseMonitor1_Click);
			// 
			// textBoxMonitor1Directory
			// 
			this.textBoxMonitor1Directory.Location = new System.Drawing.Point(146, 39);
			this.textBoxMonitor1Directory.Name = "textBoxMonitor1Directory";
			this.textBoxMonitor1Directory.Size = new System.Drawing.Size(422, 20);
			this.textBoxMonitor1Directory.TabIndex = 2;
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(6, 42);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(54, 13);
			this.label6.TabIndex = 1;
			this.label6.Text = "Monitor 1:";
			// 
			// checkBoxCycle
			// 
			this.checkBoxCycle.AutoSize = true;
			this.checkBoxCycle.Location = new System.Drawing.Point(12, 407);
			this.checkBoxCycle.Name = "checkBoxCycle";
			this.checkBoxCycle.Size = new System.Drawing.Size(369, 17);
			this.checkBoxCycle.TabIndex = 9;
			this.checkBoxCycle.Text = "Cycle through all images before repeating (mutually exclusive with above)";
			this.checkBoxCycle.UseVisualStyleBackColor = true;
			this.checkBoxCycle.CheckedChanged += new System.EventHandler(this.checkBoxCycle_CheckedChanged);
			// 
			// checkBoxEnabled
			// 
			this.checkBoxEnabled.AutoSize = true;
			this.checkBoxEnabled.Location = new System.Drawing.Point(316, 88);
			this.checkBoxEnabled.Name = "checkBoxEnabled";
			this.checkBoxEnabled.Size = new System.Drawing.Size(65, 17);
			this.checkBoxEnabled.TabIndex = 3;
			this.checkBoxEnabled.Text = "Enabled";
			this.checkBoxEnabled.UseVisualStyleBackColor = true;
			// 
			// LocalDiskForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(692, 492);
			this.Controls.Add(this.checkBoxEnabled);
			this.Controls.Add(this.checkBoxCycle);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.textBoxDescription);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.numericUpDownWeight);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.pictureBox1);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.buttonCancel);
			this.Controls.Add(this.buttonOK);
			this.Controls.Add(this.checkBoxRescan);
			this.Controls.Add(this.checkBoxRecursive);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "LocalDiskForm";
			this.ShowInTaskbar = false;
			this.Text = "Local Disk";
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownWeight)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button buttonBrowse;
		private System.Windows.Forms.TextBox textBoxDirectory;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox textBoxDescription;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.NumericUpDown numericUpDownWeight;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Button buttonCancel;
		private System.Windows.Forms.Button buttonOK;
		private System.Windows.Forms.CheckBox checkBoxRescan;
		private System.Windows.Forms.CheckBox checkBoxRecursive;
		private System.Windows.Forms.Button buttonBrowsePortrait;
		private System.Windows.Forms.TextBox textBoxPortraitDirectory;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Button buttonBrowseMonitor1;
		private System.Windows.Forms.TextBox textBoxMonitor1Directory;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Button buttonBrowseMonitor4;
		private System.Windows.Forms.TextBox textBoxMonitor4Directory;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Button buttonBrowseMonitor3;
		private System.Windows.Forms.TextBox textBoxMonitor3Directory;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Button buttonBrowseMonitor2;
		private System.Windows.Forms.TextBox textBoxMonitor2Directory;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.CheckBox checkBoxAllowFileBrowse;
		private System.Windows.Forms.CheckBox checkBoxCycle;
		private System.Windows.Forms.CheckBox checkBoxEnabled;
	}
}