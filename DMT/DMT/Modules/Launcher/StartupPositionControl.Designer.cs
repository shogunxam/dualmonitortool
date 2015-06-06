namespace DMT.Modules.Launcher
{
	partial class StartupPositionControl
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
			this.checkBoxEnable = new System.Windows.Forms.CheckBox();
			this.comboBoxWinType = new System.Windows.Forms.ComboBox();
			this.label12 = new System.Windows.Forms.Label();
			this.textBoxCY = new System.Windows.Forms.TextBox();
			this.label11 = new System.Windows.Forms.Label();
			this.textBoxCX = new System.Windows.Forms.TextBox();
			this.label10 = new System.Windows.Forms.Label();
			this.textBoxY = new System.Windows.Forms.TextBox();
			this.label9 = new System.Windows.Forms.Label();
			this.textBoxX = new System.Windows.Forms.TextBox();
			this.label8 = new System.Windows.Forms.Label();
			this.windowPicker = new DMT.Library.GuiUtils.WindowPicker();
			((System.ComponentModel.ISupportInitialize)(this.windowPicker)).BeginInit();
			this.SuspendLayout();
			// 
			// checkBoxEnable
			// 
			this.checkBoxEnable.AutoSize = true;
			this.checkBoxEnable.Location = new System.Drawing.Point(2, 7);
			this.checkBoxEnable.Name = "checkBoxEnable";
			this.checkBoxEnable.Size = new System.Drawing.Size(152, 17);
			this.checkBoxEnable.TabIndex = 33;
			this.checkBoxEnable.Text = "Position window on startup";
			this.checkBoxEnable.UseVisualStyleBackColor = true;
			this.checkBoxEnable.CheckedChanged += new System.EventHandler(this.checkBoxEnable_CheckedChanged);
			// 
			// comboBoxWinType
			// 
			this.comboBoxWinType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxWinType.FormattingEnabled = true;
			this.comboBoxWinType.Items.AddRange(new object[] {
            "Normal Window",
            "Minimised",
            "Maximised"});
			this.comboBoxWinType.Location = new System.Drawing.Point(76, 58);
			this.comboBoxWinType.Name = "comboBoxWinType";
			this.comboBoxWinType.Size = new System.Drawing.Size(148, 21);
			this.comboBoxWinType.TabIndex = 32;
			// 
			// label12
			// 
			this.label12.AutoSize = true;
			this.label12.Location = new System.Drawing.Point(2, 61);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(27, 13);
			this.label12.TabIndex = 31;
			this.label12.Text = "Run";
			// 
			// textBoxCY
			// 
			this.textBoxCY.Location = new System.Drawing.Point(341, 33);
			this.textBoxCY.Name = "textBoxCY";
			this.textBoxCY.Size = new System.Drawing.Size(48, 20);
			this.textBoxCY.TabIndex = 30;
			// 
			// label11
			// 
			this.label11.AutoSize = true;
			this.label11.Location = new System.Drawing.Point(325, 36);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(10, 13);
			this.label11.TabIndex = 29;
			this.label11.Text = ",";
			// 
			// textBoxCX
			// 
			this.textBoxCX.Location = new System.Drawing.Point(271, 33);
			this.textBoxCX.Name = "textBoxCX";
			this.textBoxCX.Size = new System.Drawing.Size(48, 20);
			this.textBoxCX.TabIndex = 28;
			// 
			// label10
			// 
			this.label10.AutoSize = true;
			this.label10.Location = new System.Drawing.Point(235, 36);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(30, 13);
			this.label10.TabIndex = 27;
			this.label10.Text = "Size:";
			// 
			// textBoxY
			// 
			this.textBoxY.Location = new System.Drawing.Point(146, 33);
			this.textBoxY.Name = "textBoxY";
			this.textBoxY.Size = new System.Drawing.Size(48, 20);
			this.textBoxY.TabIndex = 26;
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.Location = new System.Drawing.Point(130, 36);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(10, 13);
			this.label9.TabIndex = 25;
			this.label9.Text = ",";
			// 
			// textBoxX
			// 
			this.textBoxX.Location = new System.Drawing.Point(76, 33);
			this.textBoxX.Name = "textBoxX";
			this.textBoxX.Size = new System.Drawing.Size(48, 20);
			this.textBoxX.TabIndex = 24;
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(-1, 36);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(72, 13);
			this.label8.TabIndex = 23;
			this.label8.Text = "Start Position:";
			// 
			// windowPicker
			// 
			this.windowPicker.Location = new System.Drawing.Point(614, 22);
			this.windowPicker.Name = "windowPicker";
			this.windowPicker.Size = new System.Drawing.Size(48, 48);
			this.windowPicker.TabIndex = 34;
			this.windowPicker.TabStop = false;
			// 
			// StartupPositionControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.windowPicker);
			this.Controls.Add(this.checkBoxEnable);
			this.Controls.Add(this.comboBoxWinType);
			this.Controls.Add(this.label12);
			this.Controls.Add(this.textBoxCY);
			this.Controls.Add(this.label11);
			this.Controls.Add(this.textBoxCX);
			this.Controls.Add(this.label10);
			this.Controls.Add(this.textBoxY);
			this.Controls.Add(this.label9);
			this.Controls.Add(this.textBoxX);
			this.Controls.Add(this.label8);
			this.Name = "StartupPositionControl";
			this.Size = new System.Drawing.Size(667, 87);
			((System.ComponentModel.ISupportInitialize)(this.windowPicker)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.CheckBox checkBoxEnable;
		private System.Windows.Forms.ComboBox comboBoxWinType;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.TextBox textBoxCY;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.TextBox textBoxCX;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.TextBox textBoxY;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.TextBox textBoxX;
		private System.Windows.Forms.Label label8;
		private Library.GuiUtils.WindowPicker windowPicker;
	}
}
