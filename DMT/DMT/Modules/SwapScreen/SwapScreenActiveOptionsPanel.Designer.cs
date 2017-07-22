namespace DMT.Modules.SwapScreen
{
	partial class SwapScreenActiveOptionsPanel
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
			this.scrollableHotKeysPanel = new DMT.Library.HotKeys.ScrollableHotKeysPanel();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.scrollableHotKeysPanel);
			this.groupBox1.Location = new System.Drawing.Point(0, 0);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(500, 316);
			this.groupBox1.TabIndex = 3;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Hotkeys for Active Window";
			// 
			// scrollableHotKeysPanel
			// 
			this.scrollableHotKeysPanel.Location = new System.Drawing.Point(6, 19);
			this.scrollableHotKeysPanel.Name = "scrollableHotKeysPanel";
			this.scrollableHotKeysPanel.Size = new System.Drawing.Size(488, 286);
			this.scrollableHotKeysPanel.TabIndex = 11;
			// 
			// SwapScreenActiveOptionsPanel
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.groupBox1);
			this.Name = "SwapScreenActiveOptionsPanel";
			this.Size = new System.Drawing.Size(500, 360);
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox groupBox1;
		private Library.HotKeys.ScrollableHotKeysPanel scrollableHotKeysPanel;
	}
}
