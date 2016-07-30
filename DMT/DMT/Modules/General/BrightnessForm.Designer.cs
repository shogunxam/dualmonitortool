namespace DMT.Modules.General
{
	partial class BrightnessForm
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
			this.buttonOK = new System.Windows.Forms.Button();
			this.buttonCancel = new System.Windows.Forms.Button();
			this.trackBarBrightness = new System.Windows.Forms.TrackBar();
			this.labelCurBrightness = new System.Windows.Forms.Label();
			this.labelMinBrightness = new System.Windows.Forms.Label();
			this.labelMaxBrightness = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.trackBarBrightness)).BeginInit();
			this.SuspendLayout();
			// 
			// buttonOK
			// 
			this.buttonOK.Location = new System.Drawing.Point(186, 95);
			this.buttonOK.Name = "buttonOK";
			this.buttonOK.Size = new System.Drawing.Size(75, 23);
			this.buttonOK.TabIndex = 0;
			this.buttonOK.Text = "OK";
			this.buttonOK.UseVisualStyleBackColor = true;
			this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
			// 
			// buttonCancel
			// 
			this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonCancel.Location = new System.Drawing.Point(292, 95);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new System.Drawing.Size(75, 23);
			this.buttonCancel.TabIndex = 1;
			this.buttonCancel.Text = "Cancel";
			this.buttonCancel.UseVisualStyleBackColor = true;
			this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
			// 
			// trackBarBrightness
			// 
			this.trackBarBrightness.Location = new System.Drawing.Point(63, 26);
			this.trackBarBrightness.Name = "trackBarBrightness";
			this.trackBarBrightness.Size = new System.Drawing.Size(425, 45);
			this.trackBarBrightness.TabIndex = 2;
			this.trackBarBrightness.Scroll += new System.EventHandler(this.trackBarBrightness_Scroll);
			// 
			// labelCurBrightness
			// 
			this.labelCurBrightness.Location = new System.Drawing.Point(250, 58);
			this.labelCurBrightness.Name = "labelCurBrightness";
			this.labelCurBrightness.Size = new System.Drawing.Size(48, 13);
			this.labelCurBrightness.TabIndex = 3;
			this.labelCurBrightness.Text = "cur";
			this.labelCurBrightness.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// labelMinBrightness
			// 
			this.labelMinBrightness.Location = new System.Drawing.Point(12, 26);
			this.labelMinBrightness.Name = "labelMinBrightness";
			this.labelMinBrightness.Size = new System.Drawing.Size(48, 13);
			this.labelMinBrightness.TabIndex = 4;
			this.labelMinBrightness.Text = "min";
			this.labelMinBrightness.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// labelMaxBrightness
			// 
			this.labelMaxBrightness.Location = new System.Drawing.Point(494, 26);
			this.labelMaxBrightness.Name = "labelMaxBrightness";
			this.labelMaxBrightness.Size = new System.Drawing.Size(49, 13);
			this.labelMaxBrightness.TabIndex = 5;
			this.labelMaxBrightness.Text = "max";
			this.labelMaxBrightness.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// BrightnessForm
			// 
			this.AcceptButton = this.buttonOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.buttonCancel;
			this.ClientSize = new System.Drawing.Size(568, 135);
			this.Controls.Add(this.labelMaxBrightness);
			this.Controls.Add(this.labelMinBrightness);
			this.Controls.Add(this.labelCurBrightness);
			this.Controls.Add(this.trackBarBrightness);
			this.Controls.Add(this.buttonCancel);
			this.Controls.Add(this.buttonOK);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "BrightnessForm";
			this.ShowInTaskbar = false;
			this.Text = "Adjust Monitor Brightness";
			((System.ComponentModel.ISupportInitialize)(this.trackBarBrightness)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button buttonOK;
		private System.Windows.Forms.Button buttonCancel;
		private System.Windows.Forms.TrackBar trackBarBrightness;
		private System.Windows.Forms.Label labelCurBrightness;
		private System.Windows.Forms.Label labelMinBrightness;
		private System.Windows.Forms.Label labelMaxBrightness;
	}
}