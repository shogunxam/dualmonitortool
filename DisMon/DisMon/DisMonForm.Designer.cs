namespace DisMon
{
	partial class DisMonForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DisMonForm));
			this.buttonPS = new System.Windows.Forms.Button();
			this.buttonSP = new System.Windows.Forms.Button();
			this.buttonPX = new System.Windows.Forms.Button();
			this.buttonXP = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// buttonPS
			// 
			this.buttonPS.Image = ((System.Drawing.Image)(resources.GetObject("buttonPS.Image")));
			this.buttonPS.Location = new System.Drawing.Point(12, 12);
			this.buttonPS.Name = "buttonPS";
			this.buttonPS.Size = new System.Drawing.Size(106, 58);
			this.buttonPS.TabIndex = 2;
			this.buttonPS.UseVisualStyleBackColor = true;
			this.buttonPS.Click += new System.EventHandler(this.buttonPS_Click);
			// 
			// buttonSP
			// 
			this.buttonSP.Image = ((System.Drawing.Image)(resources.GetObject("buttonSP.Image")));
			this.buttonSP.Location = new System.Drawing.Point(124, 12);
			this.buttonSP.Name = "buttonSP";
			this.buttonSP.Size = new System.Drawing.Size(106, 58);
			this.buttonSP.TabIndex = 3;
			this.buttonSP.UseVisualStyleBackColor = true;
			this.buttonSP.Click += new System.EventHandler(this.buttonSP_Click);
			// 
			// buttonPX
			// 
			this.buttonPX.Image = ((System.Drawing.Image)(resources.GetObject("buttonPX.Image")));
			this.buttonPX.Location = new System.Drawing.Point(12, 76);
			this.buttonPX.Name = "buttonPX";
			this.buttonPX.Size = new System.Drawing.Size(106, 58);
			this.buttonPX.TabIndex = 4;
			this.buttonPX.UseVisualStyleBackColor = true;
			this.buttonPX.Click += new System.EventHandler(this.buttonPX_Click);
			// 
			// buttonXP
			// 
			this.buttonXP.Image = ((System.Drawing.Image)(resources.GetObject("buttonXP.Image")));
			this.buttonXP.Location = new System.Drawing.Point(124, 76);
			this.buttonXP.Name = "buttonXP";
			this.buttonXP.Size = new System.Drawing.Size(106, 58);
			this.buttonXP.TabIndex = 5;
			this.buttonXP.UseVisualStyleBackColor = true;
			this.buttonXP.Click += new System.EventHandler(this.buttonXP_Click);
			// 
			// DisMonForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(243, 148);
			this.Controls.Add(this.buttonXP);
			this.Controls.Add(this.buttonPX);
			this.Controls.Add(this.buttonSP);
			this.Controls.Add(this.buttonPS);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "DisMonForm";
			this.Text = "DisMon";
			this.Shown += new System.EventHandler(this.DisMonForm_Shown);
			this.HelpRequested += new System.Windows.Forms.HelpEventHandler(this.DisMonForm_HelpRequested);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button buttonPS;
		private System.Windows.Forms.Button buttonSP;
		private System.Windows.Forms.Button buttonPX;
		private System.Windows.Forms.Button buttonXP;
	}
}

