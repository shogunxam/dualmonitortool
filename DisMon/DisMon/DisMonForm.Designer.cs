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
			this.buttonDisable = new System.Windows.Forms.Button();
			this.buttonEnable = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// buttonDisable
			// 
			this.buttonDisable.Location = new System.Drawing.Point(12, 12);
			this.buttonDisable.Name = "buttonDisable";
			this.buttonDisable.Size = new System.Drawing.Size(92, 92);
			this.buttonDisable.TabIndex = 0;
			this.buttonDisable.Text = "Disable Secondary Monitors";
			this.buttonDisable.UseVisualStyleBackColor = true;
			this.buttonDisable.Click += new System.EventHandler(this.buttonDisable_Click);
			// 
			// buttonEnable
			// 
			this.buttonEnable.Location = new System.Drawing.Point(110, 12);
			this.buttonEnable.Name = "buttonEnable";
			this.buttonEnable.Size = new System.Drawing.Size(92, 92);
			this.buttonEnable.TabIndex = 1;
			this.buttonEnable.Text = "Re-enable disabled Monitors";
			this.buttonEnable.UseVisualStyleBackColor = true;
			this.buttonEnable.Click += new System.EventHandler(this.buttonEnable_Click);
			// 
			// DisMonForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(216, 116);
			this.Controls.Add(this.buttonEnable);
			this.Controls.Add(this.buttonDisable);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "DisMonForm";
			this.Text = "DisMon";
			this.Shown += new System.EventHandler(this.DisMonForm_Shown);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button buttonDisable;
		private System.Windows.Forms.Button buttonEnable;
	}
}

