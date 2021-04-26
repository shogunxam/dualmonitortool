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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CursorDeadSpaceOptionsPanel));
			this.label1 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.checkBoxDeadSpaceXJump = new System.Windows.Forms.CheckBox();
			this.checkBoxDeadSpaceYJump = new System.Windows.Forms.CheckBox();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
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
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(3, 178);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(494, 36);
			this.label3.TabIndex = 2;
			this.label3.Text = "These options allow you to move the cursor through such borders and the cursor wi" +
    "ll jump to the nearesr valid position on the adjoining mointor in the direction " +
    "that the cursor is being moved.";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(3, 214);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(455, 37);
			this.label4.TabIndex = 3;
			this.label4.Text = "Note: You would normally only want to select one of these options and then only i" +
    "f you have a non-rectangular secreen layout.";
			// 
			// checkBoxDeadSpaceXJump
			// 
			this.checkBoxDeadSpaceXJump.AutoSize = true;
			this.checkBoxDeadSpaceXJump.Location = new System.Drawing.Point(6, 263);
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
			this.checkBoxDeadSpaceYJump.Location = new System.Drawing.Point(6, 286);
			this.checkBoxDeadSpaceYJump.Name = "checkBoxDeadSpaceYJump";
			this.checkBoxDeadSpaceYJump.Size = new System.Drawing.Size(326, 17);
			this.checkBoxDeadSpaceYJump.TabIndex = 5;
			this.checkBoxDeadSpaceYJump.Text = "Allow cursor to jump across dead space when moving vertically.";
			this.checkBoxDeadSpaceYJump.UseVisualStyleBackColor = true;
			this.checkBoxDeadSpaceYJump.CheckedChanged += new System.EventHandler(this.checkBoxDeadSpaceYJump_CheckedChanged);
			// 
			// pictureBox1
			// 
			this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
			this.pictureBox1.Location = new System.Drawing.Point(8, 71);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(134, 90);
			this.pictureBox1.TabIndex = 6;
			this.pictureBox1.TabStop = false;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(3, 35);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(483, 33);
			this.label2.TabIndex = 7;
			this.label2.Text = "If you have a non-rectangular screen layout, then you will not be able to move th" +
    "e cursor across a border if there isn\'t a monitor on the other side. ";
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(166, 79);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(320, 77);
			this.label5.TabIndex = 8;
			this.label5.Text = resources.GetString("label5.Text");
			// 
			// CursorDeadSpaceOptionsPanel
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.label5);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.pictureBox1);
			this.Controls.Add(this.checkBoxDeadSpaceYJump);
			this.Controls.Add(this.checkBoxDeadSpaceXJump);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label1);
			this.Name = "CursorDeadSpaceOptionsPanel";
			this.Size = new System.Drawing.Size(500, 360);
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.CheckBox checkBoxDeadSpaceXJump;
		private System.Windows.Forms.CheckBox checkBoxDeadSpaceYJump;
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label5;
	}
}
