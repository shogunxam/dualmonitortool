namespace DMT.Modules.Launcher
{
	partial class LauncheGeneralOptionsPanel
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
			this.label6 = new System.Windows.Forms.Label();
			this.numericUpDownTimeout = new System.Windows.Forms.NumericUpDown();
			this.label5 = new System.Windows.Forms.Label();
			this.groupBox5 = new System.Windows.Forms.GroupBox();
			this.radioButtonIconDetails = new System.Windows.Forms.RadioButton();
			this.radioButtonIconList = new System.Windows.Forms.RadioButton();
			this.radioButtonIconLargeIcon = new System.Windows.Forms.RadioButton();
			this.label3 = new System.Windows.Forms.Label();
			this.numericUpDownIcons = new System.Windows.Forms.NumericUpDown();
			this.checkBoxMru = new System.Windows.Forms.CheckBox();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownTimeout)).BeginInit();
			this.groupBox5.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownIcons)).BeginInit();
			this.SuspendLayout();
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(284, 165);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(47, 13);
			this.label6.TabIndex = 14;
			this.label6.Text = "seconds";
			// 
			// numericUpDownTimeout
			// 
			this.numericUpDownTimeout.Location = new System.Drawing.Point(214, 163);
			this.numericUpDownTimeout.Maximum = new decimal(new int[] {
            3600,
            0,
            0,
            0});
			this.numericUpDownTimeout.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.numericUpDownTimeout.Name = "numericUpDownTimeout";
			this.numericUpDownTimeout.Size = new System.Drawing.Size(64, 20);
			this.numericUpDownTimeout.TabIndex = 13;
			this.numericUpDownTimeout.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.numericUpDownTimeout.ValueChanged += new System.EventHandler(this.numericUpDownTimeout_ValueChanged);
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(4, 165);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(204, 13);
			this.label5.TabIndex = 12;
			this.label5.Text = "Timeout for application window to appear:";
			// 
			// groupBox5
			// 
			this.groupBox5.Controls.Add(this.radioButtonIconDetails);
			this.groupBox5.Controls.Add(this.radioButtonIconList);
			this.groupBox5.Controls.Add(this.radioButtonIconLargeIcon);
			this.groupBox5.Location = new System.Drawing.Point(3, 58);
			this.groupBox5.Name = "groupBox5";
			this.groupBox5.Size = new System.Drawing.Size(163, 100);
			this.groupBox5.TabIndex = 11;
			this.groupBox5.TabStop = false;
			this.groupBox5.Text = "Display icons in:";
			// 
			// radioButtonIconDetails
			// 
			this.radioButtonIconDetails.AutoSize = true;
			this.radioButtonIconDetails.Location = new System.Drawing.Point(11, 67);
			this.radioButtonIconDetails.Name = "radioButtonIconDetails";
			this.radioButtonIconDetails.Size = new System.Drawing.Size(41, 17);
			this.radioButtonIconDetails.TabIndex = 2;
			this.radioButtonIconDetails.TabStop = true;
			this.radioButtonIconDetails.Text = "List";
			this.radioButtonIconDetails.UseVisualStyleBackColor = true;
			this.radioButtonIconDetails.CheckedChanged += new System.EventHandler(this.radioButton_CheckedChanged);
			// 
			// radioButtonIconList
			// 
			this.radioButtonIconList.AutoSize = true;
			this.radioButtonIconList.Location = new System.Drawing.Point(11, 44);
			this.radioButtonIconList.Name = "radioButtonIconList";
			this.radioButtonIconList.Size = new System.Drawing.Size(103, 17);
			this.radioButtonIconList.TabIndex = 1;
			this.radioButtonIconList.TabStop = true;
			this.radioButtonIconList.Text = "Multi-column List";
			this.radioButtonIconList.UseVisualStyleBackColor = true;
			this.radioButtonIconList.CheckedChanged += new System.EventHandler(this.radioButton_CheckedChanged);
			// 
			// radioButtonIconLargeIcon
			// 
			this.radioButtonIconLargeIcon.AutoSize = true;
			this.radioButtonIconLargeIcon.Location = new System.Drawing.Point(11, 20);
			this.radioButtonIconLargeIcon.Name = "radioButtonIconLargeIcon";
			this.radioButtonIconLargeIcon.Size = new System.Drawing.Size(44, 17);
			this.radioButtonIconLargeIcon.TabIndex = 0;
			this.radioButtonIconLargeIcon.TabStop = true;
			this.radioButtonIconLargeIcon.Text = "Grid";
			this.radioButtonIconLargeIcon.UseVisualStyleBackColor = true;
			this.radioButtonIconLargeIcon.CheckedChanged += new System.EventHandler(this.radioButton_CheckedChanged);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(11, 34);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(159, 13);
			this.label3.TabIndex = 10;
			this.label3.Text = "Initial number of icons to display:";
			// 
			// numericUpDownIcons
			// 
			this.numericUpDownIcons.Location = new System.Drawing.Point(196, 32);
			this.numericUpDownIcons.Name = "numericUpDownIcons";
			this.numericUpDownIcons.Size = new System.Drawing.Size(54, 20);
			this.numericUpDownIcons.TabIndex = 9;
			this.numericUpDownIcons.ValueChanged += new System.EventHandler(this.numericUpDownIcons_ValueChanged);
			// 
			// checkBoxMru
			// 
			this.checkBoxMru.AutoSize = true;
			this.checkBoxMru.Location = new System.Drawing.Point(14, 9);
			this.checkBoxMru.Name = "checkBoxMru";
			this.checkBoxMru.Size = new System.Drawing.Size(331, 17);
			this.checkBoxMru.TabIndex = 8;
			this.checkBoxMru.Text = "Use most recently used in auto completion (instead of most used)";
			this.checkBoxMru.UseVisualStyleBackColor = true;
			this.checkBoxMru.CheckedChanged += new System.EventHandler(this.checkBoxMru_CheckedChanged);
			// 
			// LauncherImportOptionsPanel
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.label6);
			this.Controls.Add(this.numericUpDownTimeout);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.groupBox5);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.numericUpDownIcons);
			this.Controls.Add(this.checkBoxMru);
			this.Name = "LauncherImportOptionsPanel";
			this.Size = new System.Drawing.Size(554, 320);
			this.Load += new System.EventHandler(this.LauncherImportOptionsPanel_Load);
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownTimeout)).EndInit();
			this.groupBox5.ResumeLayout(false);
			this.groupBox5.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownIcons)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.NumericUpDown numericUpDownTimeout;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.GroupBox groupBox5;
		private System.Windows.Forms.RadioButton radioButtonIconDetails;
		private System.Windows.Forms.RadioButton radioButtonIconList;
		private System.Windows.Forms.RadioButton radioButtonIconLargeIcon;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.NumericUpDown numericUpDownIcons;
		private System.Windows.Forms.CheckBox checkBoxMru;
	}
}
