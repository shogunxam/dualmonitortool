namespace SwapScreen
{
	partial class KeyComboPanel
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
			this.comboKey = new System.Windows.Forms.ComboBox();
			this.label1 = new System.Windows.Forms.Label();
			this.chkAlt = new System.Windows.Forms.CheckBox();
			this.chkCtrl = new System.Windows.Forms.CheckBox();
			this.chkShift = new System.Windows.Forms.CheckBox();
			this.chkWin = new System.Windows.Forms.CheckBox();
			this.SuspendLayout();
			// 
			// comboKey
			// 
			this.comboKey.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboKey.FormattingEnabled = true;
			this.comboKey.Location = new System.Drawing.Point(119, 35);
			this.comboKey.Name = "comboKey";
			this.comboKey.Size = new System.Drawing.Size(116, 21);
			this.comboKey.TabIndex = 11;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(70, 39);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(13, 13);
			this.label1.TabIndex = 10;
			this.label1.Text = "+";
			// 
			// chkAlt
			// 
			this.chkAlt.AutoSize = true;
			this.chkAlt.Location = new System.Drawing.Point(3, 51);
			this.chkAlt.Name = "chkAlt";
			this.chkAlt.Size = new System.Drawing.Size(38, 17);
			this.chkAlt.TabIndex = 8;
			this.chkAlt.Text = "Alt";
			this.chkAlt.UseVisualStyleBackColor = true;
			// 
			// chkCtrl
			// 
			this.chkCtrl.AutoSize = true;
			this.chkCtrl.Location = new System.Drawing.Point(3, 27);
			this.chkCtrl.Name = "chkCtrl";
			this.chkCtrl.Size = new System.Drawing.Size(41, 17);
			this.chkCtrl.TabIndex = 7;
			this.chkCtrl.Text = "Ctrl";
			this.chkCtrl.UseVisualStyleBackColor = true;
			// 
			// chkShift
			// 
			this.chkShift.AutoSize = true;
			this.chkShift.Location = new System.Drawing.Point(2, 3);
			this.chkShift.Name = "chkShift";
			this.chkShift.Size = new System.Drawing.Size(47, 17);
			this.chkShift.TabIndex = 6;
			this.chkShift.Text = "Shift";
			this.chkShift.UseVisualStyleBackColor = true;
			// 
			// chkWin
			// 
			this.chkWin.AutoSize = true;
			this.chkWin.Location = new System.Drawing.Point(2, 75);
			this.chkWin.Name = "chkWin";
			this.chkWin.Size = new System.Drawing.Size(45, 17);
			this.chkWin.TabIndex = 12;
			this.chkWin.Text = "Win";
			this.chkWin.UseVisualStyleBackColor = true;
			// 
			// KeyComboPanel
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.chkWin);
			this.Controls.Add(this.comboKey);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.chkAlt);
			this.Controls.Add(this.chkCtrl);
			this.Controls.Add(this.chkShift);
			this.Name = "KeyComboPanel";
			this.Size = new System.Drawing.Size(238, 102);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ComboBox comboKey;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.CheckBox chkAlt;
		private System.Windows.Forms.CheckBox chkCtrl;
		private System.Windows.Forms.CheckBox chkShift;
		private System.Windows.Forms.CheckBox chkWin;
	}
}
