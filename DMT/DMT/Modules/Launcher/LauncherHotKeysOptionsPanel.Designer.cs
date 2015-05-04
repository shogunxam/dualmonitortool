namespace DMT.Modules.Launcher
{
	partial class LauncherHotKeysOptionsPanel
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
			this.hotKeyPanelActivate = new DMT.Library.HotKeys.HotKeyPanel();
			this.hotKeyPanelAddMagicWord = new DMT.Library.HotKeys.HotKeyPanel();
			this.SuspendLayout();
			// 
			// hotKeyPanelActivate
			// 
			this.hotKeyPanelActivate.Description = "Description";
			this.hotKeyPanelActivate.Location = new System.Drawing.Point(12, 3);
			this.hotKeyPanelActivate.Name = "hotKeyPanelActivate";
			this.hotKeyPanelActivate.Size = new System.Drawing.Size(465, 23);
			this.hotKeyPanelActivate.TabIndex = 0;
			// 
			// hotKeyPanelAddMagicWord
			// 
			this.hotKeyPanelAddMagicWord.Description = "Description";
			this.hotKeyPanelAddMagicWord.Location = new System.Drawing.Point(12, 32);
			this.hotKeyPanelAddMagicWord.Name = "hotKeyPanelAddMagicWord";
			this.hotKeyPanelAddMagicWord.Size = new System.Drawing.Size(465, 23);
			this.hotKeyPanelAddMagicWord.TabIndex = 1;
			// 
			// LauncherHotKeysOptionsPanel
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.hotKeyPanelAddMagicWord);
			this.Controls.Add(this.hotKeyPanelActivate);
			this.Name = "LauncherHotKeysOptionsPanel";
			this.Size = new System.Drawing.Size(695, 354);
			this.ResumeLayout(false);

		}

		#endregion

		private Library.HotKeys.HotKeyPanel hotKeyPanelActivate;
		private Library.HotKeys.HotKeyPanel hotKeyPanelAddMagicWord;
	}
}
