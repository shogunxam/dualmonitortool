namespace SwapScreen
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
			this.buttonCancel = new System.Windows.Forms.Button();
			this.buttonOK = new System.Windows.Forms.Button();
			this.labelName = new System.Windows.Forms.Label();
			this.textBoxName = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.textBoxX = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.textBoxY = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.textBoxWidth = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.textBoxHeight = new System.Windows.Forms.TextBox();
			this.checkBoxEnable = new System.Windows.Forms.CheckBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.keyComboPanel = new SwapScreen.KeyComboPanel();
			this.toolTip = new System.Windows.Forms.ToolTip(this.components);
			this.windowPicker = new SwapScreen.WindowPicker();
			this.groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.windowPicker)).BeginInit();
			this.SuspendLayout();
			// 
			// buttonCancel
			// 
			this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonCancel.Location = new System.Drawing.Point(185, 277);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new System.Drawing.Size(75, 23);
			this.buttonCancel.TabIndex = 6;
			this.buttonCancel.Text = "Cancel";
			this.buttonCancel.UseVisualStyleBackColor = true;
			// 
			// buttonOK
			// 
			this.buttonOK.Location = new System.Drawing.Point(53, 277);
			this.buttonOK.Name = "buttonOK";
			this.buttonOK.Size = new System.Drawing.Size(75, 23);
			this.buttonOK.TabIndex = 5;
			this.buttonOK.Text = "OK";
			this.buttonOK.UseVisualStyleBackColor = true;
			this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
			// 
			// labelName
			// 
			this.labelName.AutoSize = true;
			this.labelName.Location = new System.Drawing.Point(29, 45);
			this.labelName.Name = "labelName";
			this.labelName.Size = new System.Drawing.Size(38, 13);
			this.labelName.TabIndex = 7;
			this.labelName.Text = "Name:";
			// 
			// textBoxName
			// 
			this.textBoxName.Location = new System.Drawing.Point(82, 45);
			this.textBoxName.Name = "textBoxName";
			this.textBoxName.Size = new System.Drawing.Size(213, 20);
			this.textBoxName.TabIndex = 8;
			this.toolTip.SetToolTip(this.textBoxName, "Your name for this area");
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(29, 82);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(47, 13);
			this.label1.TabIndex = 9;
			this.label1.Text = "Position:";
			// 
			// textBoxX
			// 
			this.textBoxX.Location = new System.Drawing.Point(82, 79);
			this.textBoxX.Name = "textBoxX";
			this.textBoxX.Size = new System.Drawing.Size(64, 20);
			this.textBoxX.TabIndex = 10;
			this.toolTip.SetToolTip(this.textBoxX, "X co-ordinate of top left corner");
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(152, 82);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(10, 13);
			this.label2.TabIndex = 11;
			this.label2.Text = ",";
			// 
			// textBoxY
			// 
			this.textBoxY.Location = new System.Drawing.Point(168, 79);
			this.textBoxY.Name = "textBoxY";
			this.textBoxY.Size = new System.Drawing.Size(64, 20);
			this.textBoxY.TabIndex = 12;
			this.toolTip.SetToolTip(this.textBoxY, "Y co-ordinate for top left corner");
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(29, 108);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(30, 13);
			this.label3.TabIndex = 13;
			this.label3.Text = "Size:";
			// 
			// textBoxWidth
			// 
			this.textBoxWidth.Location = new System.Drawing.Point(82, 105);
			this.textBoxWidth.Name = "textBoxWidth";
			this.textBoxWidth.Size = new System.Drawing.Size(64, 20);
			this.textBoxWidth.TabIndex = 14;
			this.toolTip.SetToolTip(this.textBoxWidth, "Width of area");
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(152, 108);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(12, 13);
			this.label4.TabIndex = 15;
			this.label4.Text = "x";
			// 
			// textBoxHeight
			// 
			this.textBoxHeight.Location = new System.Drawing.Point(168, 105);
			this.textBoxHeight.Name = "textBoxHeight";
			this.textBoxHeight.Size = new System.Drawing.Size(64, 20);
			this.textBoxHeight.TabIndex = 16;
			this.toolTip.SetToolTip(this.textBoxHeight, "Height of area");
			// 
			// checkBoxEnable
			// 
			this.checkBoxEnable.AutoSize = true;
			this.checkBoxEnable.Location = new System.Drawing.Point(13, 13);
			this.checkBoxEnable.Name = "checkBoxEnable";
			this.checkBoxEnable.Size = new System.Drawing.Size(226, 17);
			this.checkBoxEnable.TabIndex = 17;
			this.checkBoxEnable.Text = "Enable this User Defined Area and Hotkey";
			this.checkBoxEnable.UseVisualStyleBackColor = true;
			this.checkBoxEnable.CheckedChanged += new System.EventHandler(this.checkBoxEnable_CheckedChanged);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.keyComboPanel);
			this.groupBox1.Location = new System.Drawing.Point(32, 141);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(259, 130);
			this.groupBox1.TabIndex = 18;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Hotkey";
			// 
			// keyComboPanel
			// 
			this.keyComboPanel.Location = new System.Drawing.Point(6, 19);
			this.keyComboPanel.Name = "keyComboPanel";
			this.keyComboPanel.Size = new System.Drawing.Size(238, 102);
			this.keyComboPanel.TabIndex = 0;
			// 
			// windowPicker
			// 
			this.windowPicker.Location = new System.Drawing.Point(243, 79);
			this.windowPicker.Name = "windowPicker";
			this.windowPicker.Size = new System.Drawing.Size(48, 48);
			this.windowPicker.TabIndex = 19;
			this.windowPicker.TabStop = false;
			this.toolTip.SetToolTip(this.windowPicker, "Drag and drop over a window to copy its position and size");
			// 
			// UdaForm
			// 
			this.AcceptButton = this.buttonOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.buttonCancel;
			this.ClientSize = new System.Drawing.Size(320, 318);
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
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
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

		private System.Windows.Forms.Button buttonCancel;
		private System.Windows.Forms.Button buttonOK;
		private System.Windows.Forms.Label labelName;
		private System.Windows.Forms.TextBox textBoxName;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox textBoxX;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox textBoxY;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox textBoxWidth;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox textBoxHeight;
		private System.Windows.Forms.CheckBox checkBoxEnable;
		private System.Windows.Forms.GroupBox groupBox1;
		private KeyComboPanel keyComboPanel;
		private WindowPicker windowPicker;
		private System.Windows.Forms.ToolTip toolTip;
	}
}