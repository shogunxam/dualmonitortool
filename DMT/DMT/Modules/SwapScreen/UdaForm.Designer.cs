namespace DMT.Modules.SwapScreen
{
	partial class UdaForm
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
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.keyComboPanel = new DMT.Library.HotKeys.KeyComboPanel();
			this.checkBoxEnable = new System.Windows.Forms.CheckBox();
			this.textBoxHeight = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.textBoxWidth = new System.Windows.Forms.TextBox();
			this.toolTip = new System.Windows.Forms.ToolTip(this.components);
			this.textBoxY = new System.Windows.Forms.TextBox();
			this.textBoxX = new System.Windows.Forms.TextBox();
			this.textBoxName = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.labelName = new System.Windows.Forms.Label();
			this.buttonCancel = new System.Windows.Forms.Button();
			this.buttonOK = new System.Windows.Forms.Button();
			this.windowPicker = new DMT.Library.GuiUtils.WindowPicker();
			this.groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.windowPicker)).BeginInit();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.keyComboPanel);
			this.groupBox1.Location = new System.Drawing.Point(31, 140);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(259, 130);
			this.groupBox1.TabIndex = 32;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Hotkey";
			// 
			// keyComboPanel
			// 
			this.keyComboPanel.Location = new System.Drawing.Point(11, 29);
			this.keyComboPanel.Name = "keyComboPanel";
			this.keyComboPanel.Size = new System.Drawing.Size(242, 95);
			this.keyComboPanel.TabIndex = 0;
			// 
			// checkBoxEnable
			// 
			this.checkBoxEnable.AutoSize = true;
			this.checkBoxEnable.Location = new System.Drawing.Point(12, 12);
			this.checkBoxEnable.Name = "checkBoxEnable";
			this.checkBoxEnable.Size = new System.Drawing.Size(226, 17);
			this.checkBoxEnable.TabIndex = 31;
			this.checkBoxEnable.Text = "Enable this User Defined Area and Hotkey";
			this.checkBoxEnable.UseVisualStyleBackColor = true;
			this.checkBoxEnable.CheckedChanged += new System.EventHandler(this.checkBoxEnable_CheckedChanged);
			// 
			// textBoxHeight
			// 
			this.textBoxHeight.Location = new System.Drawing.Point(167, 104);
			this.textBoxHeight.Name = "textBoxHeight";
			this.textBoxHeight.Size = new System.Drawing.Size(64, 20);
			this.textBoxHeight.TabIndex = 30;
			this.toolTip.SetToolTip(this.textBoxHeight, "Height of area");
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(151, 107);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(12, 13);
			this.label4.TabIndex = 29;
			this.label4.Text = "x";
			// 
			// textBoxWidth
			// 
			this.textBoxWidth.Location = new System.Drawing.Point(81, 104);
			this.textBoxWidth.Name = "textBoxWidth";
			this.textBoxWidth.Size = new System.Drawing.Size(64, 20);
			this.textBoxWidth.TabIndex = 28;
			this.toolTip.SetToolTip(this.textBoxWidth, "Width of area");
			// 
			// textBoxY
			// 
			this.textBoxY.Location = new System.Drawing.Point(167, 78);
			this.textBoxY.Name = "textBoxY";
			this.textBoxY.Size = new System.Drawing.Size(64, 20);
			this.textBoxY.TabIndex = 26;
			this.toolTip.SetToolTip(this.textBoxY, "Y co-ordinate for top left corner");
			// 
			// textBoxX
			// 
			this.textBoxX.Location = new System.Drawing.Point(81, 78);
			this.textBoxX.Name = "textBoxX";
			this.textBoxX.Size = new System.Drawing.Size(64, 20);
			this.textBoxX.TabIndex = 24;
			this.toolTip.SetToolTip(this.textBoxX, "X co-ordinate of top left corner");
			// 
			// textBoxName
			// 
			this.textBoxName.Location = new System.Drawing.Point(81, 44);
			this.textBoxName.Name = "textBoxName";
			this.textBoxName.Size = new System.Drawing.Size(213, 20);
			this.textBoxName.TabIndex = 22;
			this.toolTip.SetToolTip(this.textBoxName, "Your name for this area");
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(28, 107);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(30, 13);
			this.label3.TabIndex = 27;
			this.label3.Text = "Size:";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(151, 81);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(10, 13);
			this.label2.TabIndex = 25;
			this.label2.Text = ",";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(28, 81);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(47, 13);
			this.label1.TabIndex = 23;
			this.label1.Text = "Position:";
			// 
			// labelName
			// 
			this.labelName.AutoSize = true;
			this.labelName.Location = new System.Drawing.Point(28, 44);
			this.labelName.Name = "labelName";
			this.labelName.Size = new System.Drawing.Size(38, 13);
			this.labelName.TabIndex = 21;
			this.labelName.Text = "Name:";
			// 
			// buttonCancel
			// 
			this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonCancel.Location = new System.Drawing.Point(184, 276);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new System.Drawing.Size(75, 23);
			this.buttonCancel.TabIndex = 20;
			this.buttonCancel.Text = "Cancel";
			this.buttonCancel.UseVisualStyleBackColor = true;
			this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
			// 
			// buttonOK
			// 
			this.buttonOK.Location = new System.Drawing.Point(52, 276);
			this.buttonOK.Name = "buttonOK";
			this.buttonOK.Size = new System.Drawing.Size(75, 23);
			this.buttonOK.TabIndex = 19;
			this.buttonOK.Text = "OK";
			this.buttonOK.UseVisualStyleBackColor = true;
			this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
			// 
			// windowPicker
			// 
			this.windowPicker.Location = new System.Drawing.Point(243, 79);
			this.windowPicker.Name = "windowPicker";
			this.windowPicker.Size = new System.Drawing.Size(48, 48);
			this.windowPicker.TabIndex = 33;
			this.windowPicker.TabStop = false;
			// 
			// UdaForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(311, 313);
			this.Controls.Add(this.windowPicker);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.checkBoxEnable);
			this.Controls.Add(this.textBoxHeight);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.textBoxWidth);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.textBoxY);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.textBoxX);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.textBoxName);
			this.Controls.Add(this.labelName);
			this.Controls.Add(this.buttonCancel);
			this.Controls.Add(this.buttonOK);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "UdaForm";
			this.ShowInTaskbar = false;
			this.Text = "Change User Defined Area";
			this.groupBox1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.windowPicker)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.GroupBox groupBox1;
		private Library.HotKeys.KeyComboPanel keyComboPanel;
		private System.Windows.Forms.CheckBox checkBoxEnable;
		private System.Windows.Forms.TextBox textBoxHeight;
		private System.Windows.Forms.ToolTip toolTip;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox textBoxWidth;
		private System.Windows.Forms.TextBox textBoxY;
		private System.Windows.Forms.TextBox textBoxX;
		private System.Windows.Forms.TextBox textBoxName;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label labelName;
		private System.Windows.Forms.Button buttonCancel;
		private System.Windows.Forms.Button buttonOK;
		private Library.GuiUtils.WindowPicker windowPicker;
	}
}