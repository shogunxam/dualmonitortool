namespace DualSnap
{
	partial class OptionsForm
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
			this.label1 = new System.Windows.Forms.Label();
			this.numericUpDown = new System.Windows.Forms.NumericUpDown();
			this.button1 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.checkBoxShowSnap = new System.Windows.Forms.CheckBox();
			this.checkBoxAutoStart = new System.Windows.Forms.CheckBox();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.label2 = new System.Windows.Forms.Label();
			this.buttonTakeSnap = new System.Windows.Forms.Button();
			this.labelTakeSnap = new System.Windows.Forms.Label();
			this.buttonShowSnap = new System.Windows.Forms.Button();
			this.label3 = new System.Windows.Forms.Label();
			this.labelShowSnap = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown)).BeginInit();
			this.groupBox3.SuspendLayout();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(9, 104);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(119, 13);
			this.label1.TabIndex = 1;
			this.label1.Text = "Max remembered snaps";
			// 
			// numericUpDown
			// 
			this.numericUpDown.Location = new System.Drawing.Point(134, 102);
			this.numericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.numericUpDown.Name = "numericUpDown";
			this.numericUpDown.Size = new System.Drawing.Size(68, 20);
			this.numericUpDown.TabIndex = 2;
			this.numericUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			// 
			// button1
			// 
			this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.button1.Location = new System.Drawing.Point(132, 193);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(75, 23);
			this.button1.TabIndex = 3;
			this.button1.Text = "OK";
			this.button1.UseVisualStyleBackColor = true;
			// 
			// button2
			// 
			this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.button2.Location = new System.Drawing.Point(295, 193);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(75, 23);
			this.button2.TabIndex = 4;
			this.button2.Text = "Cancel";
			this.button2.UseVisualStyleBackColor = true;
			// 
			// checkBoxShowSnap
			// 
			this.checkBoxShowSnap.AutoSize = true;
			this.checkBoxShowSnap.Location = new System.Drawing.Point(12, 133);
			this.checkBoxShowSnap.Name = "checkBoxShowSnap";
			this.checkBoxShowSnap.Size = new System.Drawing.Size(226, 17);
			this.checkBoxShowSnap.TabIndex = 5;
			this.checkBoxShowSnap.Text = "Show snap on second screen when taken";
			this.checkBoxShowSnap.UseVisualStyleBackColor = true;
			// 
			// checkBoxAutoStart
			// 
			this.checkBoxAutoStart.AutoSize = true;
			this.checkBoxAutoStart.Location = new System.Drawing.Point(12, 156);
			this.checkBoxAutoStart.Name = "checkBoxAutoStart";
			this.checkBoxAutoStart.Size = new System.Drawing.Size(152, 17);
			this.checkBoxAutoStart.TabIndex = 6;
			this.checkBoxAutoStart.Text = "Start when Windows starts";
			this.checkBoxAutoStart.UseVisualStyleBackColor = true;
			this.checkBoxAutoStart.CheckedChanged += new System.EventHandler(this.checkBoxAutoStart_CheckedChanged);
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.label2);
			this.groupBox3.Controls.Add(this.buttonTakeSnap);
			this.groupBox3.Controls.Add(this.labelTakeSnap);
			this.groupBox3.Controls.Add(this.buttonShowSnap);
			this.groupBox3.Controls.Add(this.label3);
			this.groupBox3.Controls.Add(this.labelShowSnap);
			this.groupBox3.Location = new System.Drawing.Point(12, 12);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(481, 76);
			this.groupBox3.TabIndex = 8;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "HotKeys";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(6, 20);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(224, 13);
			this.label2.TabIndex = 11;
			this.label2.Text = "Take a snapshot of the primary screen";
			// 
			// buttonTakeSnap
			// 
			this.buttonTakeSnap.Location = new System.Drawing.Point(395, 15);
			this.buttonTakeSnap.Name = "buttonTakeSnap";
			this.buttonTakeSnap.Size = new System.Drawing.Size(75, 23);
			this.buttonTakeSnap.TabIndex = 10;
			this.buttonTakeSnap.Text = "Change...";
			this.buttonTakeSnap.UseVisualStyleBackColor = true;
			this.buttonTakeSnap.Click += new System.EventHandler(this.buttonTakeSnap_Click);
			// 
			// labelTakeSnap
			// 
			this.labelTakeSnap.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
			this.labelTakeSnap.Location = new System.Drawing.Point(236, 20);
			this.labelTakeSnap.Name = "labelTakeSnap";
			this.labelTakeSnap.Size = new System.Drawing.Size(153, 13);
			this.labelTakeSnap.TabIndex = 12;
			this.labelTakeSnap.Text = "labelTakeSnap";
			// 
			// buttonShowSnap
			// 
			this.buttonShowSnap.Location = new System.Drawing.Point(395, 40);
			this.buttonShowSnap.Name = "buttonShowSnap";
			this.buttonShowSnap.Size = new System.Drawing.Size(75, 23);
			this.buttonShowSnap.TabIndex = 15;
			this.buttonShowSnap.Text = "Change...";
			this.buttonShowSnap.UseVisualStyleBackColor = true;
			this.buttonShowSnap.Click += new System.EventHandler(this.buttonShowSnap_Click);
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(6, 45);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(224, 13);
			this.label3.TabIndex = 13;
			this.label3.Text = "Toggle display of current snapshot";
			// 
			// labelShowSnap
			// 
			this.labelShowSnap.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
			this.labelShowSnap.Location = new System.Drawing.Point(236, 45);
			this.labelShowSnap.Name = "labelShowSnap";
			this.labelShowSnap.Size = new System.Drawing.Size(153, 13);
			this.labelShowSnap.TabIndex = 14;
			this.labelShowSnap.Text = "labelShowSnap";
			// 
			// OptionsForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(505, 228);
			this.Controls.Add(this.groupBox3);
			this.Controls.Add(this.checkBoxAutoStart);
			this.Controls.Add(this.checkBoxShowSnap);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.numericUpDown);
			this.Controls.Add(this.label1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "OptionsForm";
			this.Text = "Dual Snap Options";
			this.Load += new System.EventHandler(this.OptionsForm_Load);
			this.HelpRequested += new System.Windows.Forms.HelpEventHandler(this.OptionsForm_HelpRequested);
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown)).EndInit();
			this.groupBox3.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.NumericUpDown numericUpDown;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.CheckBox checkBoxShowSnap;
		private System.Windows.Forms.CheckBox checkBoxAutoStart;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button buttonTakeSnap;
		private System.Windows.Forms.Label labelTakeSnap;
		private System.Windows.Forms.Button buttonShowSnap;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label labelShowSnap;
	}
}