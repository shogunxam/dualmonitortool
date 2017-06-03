namespace DMT.Modules.SwapScreen
{
	partial class SwapScreenUdaOptionsPanel
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
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.buttonResetUDA = new System.Windows.Forms.Button();
			this.scrollableUdasPanel = new DMT.Modules.SwapScreen.ScrollableUdasPanel();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.scrollableUdasPanel);
			this.groupBox1.Controls.Add(this.buttonResetUDA);
			this.groupBox1.Location = new System.Drawing.Point(0, 0);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(500, 351);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "User Defined Areas and associated HotKeys";
			// 
			// buttonResetUDA
			// 
			this.buttonResetUDA.Location = new System.Drawing.Point(6, 322);
			this.buttonResetUDA.Name = "buttonResetUDA";
			this.buttonResetUDA.Size = new System.Drawing.Size(465, 23);
			this.buttonResetUDA.TabIndex = 10;
			this.buttonResetUDA.Text = "Reset areas and descriptions to match current monitors";
			this.buttonResetUDA.UseVisualStyleBackColor = true;
			this.buttonResetUDA.Click += new System.EventHandler(this.buttonResetUDA_Click);
			// 
			// scrollableUdasPanel
			// 
			this.scrollableUdasPanel.Location = new System.Drawing.Point(6, 19);
			this.scrollableUdasPanel.Name = "scrollableUdasPanel";
			this.scrollableUdasPanel.Size = new System.Drawing.Size(488, 286);
			this.scrollableUdasPanel.TabIndex = 11;
			// 
			// SwapScreenUdaOptionsPanel
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.groupBox1);
			this.Name = "SwapScreenUdaOptionsPanel";
			this.Size = new System.Drawing.Size(510, 362);
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Button buttonResetUDA;
		private ScrollableUdasPanel scrollableUdasPanel;

	}
}
