namespace DMT.Modules.Cursor
{
	partial class CursorDeadSpaceOptionsPanel
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
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.checkBoxDeadSpaceXJump = new System.Windows.Forms.CheckBox();
			this.checkBoxDeadSpaceYJump = new System.Windows.Forms.CheckBox();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(3, 10);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(139, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Dead space cursor jumping.";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(3, 23);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(297, 13);
			this.label2.TabIndex = 1;
			this.label2.Text = "This only applies if you have a non rectangular monitor layout.";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(3, 50);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(494, 36);
			this.label3.TabIndex = 2;
			this.label3.Text = "Normally, you can\'t move the cursor from a monitor if there isn\'t a monitor at th" +
    "e target location of the cursor.";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(3, 86);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(455, 13);
			this.label4.TabIndex = 3;
			this.label4.Text = "This option allows the cursor to leave the monitor and jump to a location on the " +
    "nearest monitor.";
			// 
			// checkBoxDeadSpaceXJump
			// 
			this.checkBoxDeadSpaceXJump.AutoSize = true;
			this.checkBoxDeadSpaceXJump.Location = new System.Drawing.Point(6, 111);
			this.checkBoxDeadSpaceXJump.Name = "checkBoxDeadSpaceXJump";
			this.checkBoxDeadSpaceXJump.Size = new System.Drawing.Size(337, 17);
			this.checkBoxDeadSpaceXJump.TabIndex = 4;
			this.checkBoxDeadSpaceXJump.Text = "Allow cursor to jump across dead space when moving horizontally.";
			this.checkBoxDeadSpaceXJump.UseVisualStyleBackColor = true;
			this.checkBoxDeadSpaceXJump.CheckedChanged += new System.EventHandler(this.checkBoxDeadSpaceXJump_CheckedChanged);
			// 
			// checkBoxDeadSpaceYJump
			// 
			this.checkBoxDeadSpaceYJump.AutoSize = true;
			this.checkBoxDeadSpaceYJump.Location = new System.Drawing.Point(6, 134);
			this.checkBoxDeadSpaceYJump.Name = "checkBoxDeadSpaceYJump";
			this.checkBoxDeadSpaceYJump.Size = new System.Drawing.Size(326, 17);
			this.checkBoxDeadSpaceYJump.TabIndex = 5;
			this.checkBoxDeadSpaceYJump.Text = "Allow cursor to jump across dead space when moving vertically.";
			this.checkBoxDeadSpaceYJump.UseVisualStyleBackColor = true;
			this.checkBoxDeadSpaceYJump.CheckedChanged += new System.EventHandler(this.checkBoxDeadSpaceYJump_CheckedChanged);
			// 
			// CursorDeadSpaceOptionsPanel
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.checkBoxDeadSpaceYJump);
			this.Controls.Add(this.checkBoxDeadSpaceXJump);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Name = "CursorDeadSpaceOptionsPanel";
			this.Size = new System.Drawing.Size(500, 360);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.CheckBox checkBoxDeadSpaceXJump;
		private System.Windows.Forms.CheckBox checkBoxDeadSpaceYJump;
	}
}
