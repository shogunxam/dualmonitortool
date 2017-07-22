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
			this.scrollableUdasPanel = new DMT.Modules.SwapScreen.ScrollableUdasPanel();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.scrollableUdasPanel);
			this.groupBox1.Location = new System.Drawing.Point(0, 0);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(500, 316);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "User Defined Areas and associated HotKeys";
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
			this.Size = new System.Drawing.Size(500, 360);
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox groupBox1;
		private ScrollableUdasPanel scrollableUdasPanel;

	}
}
