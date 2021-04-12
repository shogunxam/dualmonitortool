namespace DMT.Modules.Cursor
{
	partial class CursorGeneralOptionsPanel
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
			this.label13 = new System.Windows.Forms.Label();
			this.label11 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.hotKeyPanelCursorToPrimaryScreen = new DMT.Library.HotKeys.HotKeyPanel();
			this.hotKeyPanelCursorPrevScreen = new DMT.Library.HotKeys.HotKeyPanel();
			this.hotKeyPanelCursorNextScreen = new DMT.Library.HotKeys.HotKeyPanel();
			this.groupBox3.SuspendLayout();
			this.SuspendLayout();
			// 
			// label13
			// 
			this.label13.AutoSize = true;
			this.label13.Location = new System.Drawing.Point(444, 16);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(27, 13);
			this.label13.TabIndex = 41;
			this.label13.Text = "Max";
			// 
			// label11
			// 
			this.label11.AutoSize = true;
			this.label11.Location = new System.Drawing.Point(237, 16);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(24, 13);
			this.label11.TabIndex = 40;
			this.label11.Text = "Min";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(7, 16);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(208, 13);
			this.label5.TabIndex = 39;
			this.label5.Text = "Resistance to movement between screens";
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.hotKeyPanelCursorToPrimaryScreen);
			this.groupBox3.Controls.Add(this.hotKeyPanelCursorPrevScreen);
			this.groupBox3.Controls.Add(this.hotKeyPanelCursorNextScreen);
			this.groupBox3.Location = new System.Drawing.Point(3, 3);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(477, 112);
			this.groupBox3.TabIndex = 46;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Cursor HotKeys";
			// 
			// hotKeyPanelCursorToPrimaryScreen
			// 
			this.hotKeyPanelCursorToPrimaryScreen.Description = "Description";
			this.hotKeyPanelCursorToPrimaryScreen.Location = new System.Drawing.Point(8, 79);
			this.hotKeyPanelCursorToPrimaryScreen.Name = "hotKeyPanelCursorToPrimaryScreen";
			this.hotKeyPanelCursorToPrimaryScreen.Size = new System.Drawing.Size(465, 23);
			this.hotKeyPanelCursorToPrimaryScreen.TabIndex = 5;
			// 
			// hotKeyPanelCursorPrevScreen
			// 
			this.hotKeyPanelCursorPrevScreen.Description = "Description";
			this.hotKeyPanelCursorPrevScreen.Location = new System.Drawing.Point(8, 50);
			this.hotKeyPanelCursorPrevScreen.Name = "hotKeyPanelCursorPrevScreen";
			this.hotKeyPanelCursorPrevScreen.Size = new System.Drawing.Size(465, 23);
			this.hotKeyPanelCursorPrevScreen.TabIndex = 4;
			// 
			// hotKeyPanelCursorNextScreen
			// 
			this.hotKeyPanelCursorNextScreen.Description = "Description";
			this.hotKeyPanelCursorNextScreen.Location = new System.Drawing.Point(8, 21);
			this.hotKeyPanelCursorNextScreen.Name = "hotKeyPanelCursorNextScreen";
			this.hotKeyPanelCursorNextScreen.Size = new System.Drawing.Size(465, 23);
			this.hotKeyPanelCursorNextScreen.TabIndex = 3;
			// 
			// CursorGeneralOptionsPanel
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.groupBox3);
			this.Name = "CursorGeneralOptionsPanel";
			this.Size = new System.Drawing.Size(500, 360);
			this.groupBox3.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.GroupBox groupBox3;
		private Library.HotKeys.HotKeyPanel hotKeyPanelCursorPrevScreen;
		private Library.HotKeys.HotKeyPanel hotKeyPanelCursorNextScreen;
		private Library.HotKeys.HotKeyPanel hotKeyPanelCursorToPrimaryScreen;

	}
}
