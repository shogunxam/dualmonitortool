namespace DMT.Modules.SwapScreen
{
	partial class SwapScreenSdaOptionsPanel
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SwapScreenSdaOptionsPanel));
			this.checkBoxEnable = new System.Windows.Forms.CheckBox();
			this.label1 = new System.Windows.Forms.Label();
			this.listBoxModifiers = new System.Windows.Forms.ListBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.listBoxRegistrationErrors = new System.Windows.Forms.ListBox();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.label4 = new System.Windows.Forms.Label();
			this.buttonUp = new System.Windows.Forms.Button();
			this.buttonDown = new System.Windows.Forms.Button();
			this.checkBoxBorderOutside = new System.Windows.Forms.CheckBox();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.SuspendLayout();
			// 
			// checkBoxEnable
			// 
			this.checkBoxEnable.AutoSize = true;
			this.checkBoxEnable.Location = new System.Drawing.Point(6, 103);
			this.checkBoxEnable.Name = "checkBoxEnable";
			this.checkBoxEnable.Size = new System.Drawing.Size(166, 17);
			this.checkBoxEnable.TabIndex = 0;
			this.checkBoxEnable.Text = "Enable System Defined Areas";
			this.checkBoxEnable.UseVisualStyleBackColor = true;
			this.checkBoxEnable.CheckedChanged += new System.EventHandler(this.checkBoxEnable_CheckedChanged);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(3, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(111, 13);
			this.label1.TabIndex = 1;
			this.label1.Text = "System Defined Areas";
			// 
			// listBoxModifiers
			// 
			this.listBoxModifiers.AllowDrop = true;
			this.listBoxModifiers.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
			this.listBoxModifiers.FormattingEnabled = true;
			this.listBoxModifiers.Location = new System.Drawing.Point(6, 126);
			this.listBoxModifiers.Name = "listBoxModifiers";
			this.listBoxModifiers.Size = new System.Drawing.Size(193, 108);
			this.listBoxModifiers.TabIndex = 2;
			this.listBoxModifiers.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.listBoxModifiers_DrawItem);
			this.listBoxModifiers.SelectedIndexChanged += new System.EventHandler(this.listBoxModifiers_SelectedIndexChanged);
			this.listBoxModifiers.DragDrop += new System.Windows.Forms.DragEventHandler(this.listBoxModifiers_DragDrop);
			this.listBoxModifiers.DragOver += new System.Windows.Forms.DragEventHandler(this.listBoxModifiers_DragOver);
			this.listBoxModifiers.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listBoxModifiers_MouseDown);
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(256, 136);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(234, 105);
			this.label2.TabIndex = 3;
			this.label2.Text = resources.GetString("label2.Text");
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(3, 243);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(208, 13);
			this.label3.TabIndex = 4;
			this.label3.Text = "Following hot keys could not be registered:";
			// 
			// listBoxRegistrationErrors
			// 
			this.listBoxRegistrationErrors.FormattingEnabled = true;
			this.listBoxRegistrationErrors.Location = new System.Drawing.Point(6, 263);
			this.listBoxRegistrationErrors.Name = "listBoxRegistrationErrors";
			this.listBoxRegistrationErrors.Size = new System.Drawing.Size(484, 95);
			this.listBoxRegistrationErrors.TabIndex = 5;
			// 
			// pictureBox1
			// 
			this.pictureBox1.Image = global::DMT.Properties.Resources.NumPad1;
			this.pictureBox1.Location = new System.Drawing.Point(350, 12);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(109, 109);
			this.pictureBox1.TabIndex = 6;
			this.pictureBox1.TabStop = false;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(3, 23);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(317, 46);
			this.label4.TabIndex = 7;
			this.label4.Text = "By using one of the screen modifier keys below together with the numeric key pad," +
    " you can move the active window to any full screen, half screen or quarter scree" +
    "n.";
			// 
			// buttonUp
			// 
			this.buttonUp.Image = ((System.Drawing.Image)(resources.GetObject("buttonUp.Image")));
			this.buttonUp.Location = new System.Drawing.Point(205, 136);
			this.buttonUp.Name = "buttonUp";
			this.buttonUp.Size = new System.Drawing.Size(32, 32);
			this.buttonUp.TabIndex = 8;
			this.buttonUp.UseVisualStyleBackColor = true;
			this.buttonUp.Click += new System.EventHandler(this.buttonUp_Click);
			// 
			// buttonDown
			// 
			this.buttonDown.Image = ((System.Drawing.Image)(resources.GetObject("buttonDown.Image")));
			this.buttonDown.Location = new System.Drawing.Point(205, 193);
			this.buttonDown.Name = "buttonDown";
			this.buttonDown.Size = new System.Drawing.Size(32, 32);
			this.buttonDown.TabIndex = 9;
			this.buttonDown.UseVisualStyleBackColor = true;
			this.buttonDown.Click += new System.EventHandler(this.buttonDown_Click);
			// 
			// checkBoxBorderOutside
			// 
			this.checkBoxBorderOutside.AutoSize = true;
			this.checkBoxBorderOutside.Location = new System.Drawing.Point(6, 80);
			this.checkBoxBorderOutside.Name = "checkBoxBorderOutside";
			this.checkBoxBorderOutside.Size = new System.Drawing.Size(169, 17);
			this.checkBoxBorderOutside.TabIndex = 10;
			this.checkBoxBorderOutside.Text = "Put borders on outside of SDA";
			this.checkBoxBorderOutside.UseVisualStyleBackColor = true;
			this.checkBoxBorderOutside.CheckedChanged += new System.EventHandler(this.checkBoxBorderOutside_CheckedChanged);
			// 
			// SwapScreenSdaOptionsPanel
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.checkBoxBorderOutside);
			this.Controls.Add(this.buttonDown);
			this.Controls.Add(this.buttonUp);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.pictureBox1);
			this.Controls.Add(this.listBoxRegistrationErrors);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.listBoxModifiers);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.checkBoxEnable);
			this.Name = "SwapScreenSdaOptionsPanel";
			this.Size = new System.Drawing.Size(500, 360);
			this.Load += new System.EventHandler(this.SwapScreenSdaOptionsPanel_Load);
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.CheckBox checkBoxEnable;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ListBox listBoxModifiers;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.ListBox listBoxRegistrationErrors;
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Button buttonUp;
		private System.Windows.Forms.Button buttonDown;
		private System.Windows.Forms.CheckBox checkBoxBorderOutside;
	}
}
