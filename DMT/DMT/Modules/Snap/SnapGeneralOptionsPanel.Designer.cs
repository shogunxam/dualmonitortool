namespace DMT.Modules.Snap
{
	partial class SnapGeneralOptionsPanel
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
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.hotKeyPanelTakeWinSnap = new DMT.Library.HotKeys.HotKeyPanel();
			this.hotKeyPanelShowSnap = new DMT.Library.HotKeys.HotKeyPanel();
			this.hotKeyPanelTakeSnap = new DMT.Library.HotKeys.HotKeyPanel();
			this.checkBoxShowSnap = new System.Windows.Forms.CheckBox();
			this.numericUpDownSnaps = new System.Windows.Forms.NumericUpDown();
			this.label1 = new System.Windows.Forms.Label();
			this.checkBoxExpandSnap = new System.Windows.Forms.CheckBox();
			this.checkBoxShrinkSnap = new System.Windows.Forms.CheckBox();
			this.checkBoxMaintainAspectRatio = new System.Windows.Forms.CheckBox();
			this.groupBox3.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownSnaps)).BeginInit();
			this.SuspendLayout();
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.hotKeyPanelTakeWinSnap);
			this.groupBox3.Controls.Add(this.hotKeyPanelShowSnap);
			this.groupBox3.Controls.Add(this.hotKeyPanelTakeSnap);
			this.groupBox3.Location = new System.Drawing.Point(3, 3);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(481, 112);
			this.groupBox3.TabIndex = 12;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "HotKeys";
			// 
			// hotKeyPanelTakeWinSnap
			// 
			this.hotKeyPanelTakeWinSnap.Description = "Description";
			this.hotKeyPanelTakeWinSnap.Location = new System.Drawing.Point(6, 48);
			this.hotKeyPanelTakeWinSnap.Name = "hotKeyPanelTakeWinSnap";
			this.hotKeyPanelTakeWinSnap.Size = new System.Drawing.Size(465, 23);
			this.hotKeyPanelTakeWinSnap.TabIndex = 2;
			// 
			// hotKeyPanelShowSnap
			// 
			this.hotKeyPanelShowSnap.Description = "Description";
			this.hotKeyPanelShowSnap.Location = new System.Drawing.Point(6, 77);
			this.hotKeyPanelShowSnap.Name = "hotKeyPanelShowSnap";
			this.hotKeyPanelShowSnap.Size = new System.Drawing.Size(465, 23);
			this.hotKeyPanelShowSnap.TabIndex = 1;
			// 
			// hotKeyPanelTakeSnap
			// 
			this.hotKeyPanelTakeSnap.Description = "Description";
			this.hotKeyPanelTakeSnap.Location = new System.Drawing.Point(6, 19);
			this.hotKeyPanelTakeSnap.Name = "hotKeyPanelTakeSnap";
			this.hotKeyPanelTakeSnap.Size = new System.Drawing.Size(465, 23);
			this.hotKeyPanelTakeSnap.TabIndex = 0;
			// 
			// checkBoxShowSnap
			// 
			this.checkBoxShowSnap.AutoSize = true;
			this.checkBoxShowSnap.Location = new System.Drawing.Point(3, 173);
			this.checkBoxShowSnap.Name = "checkBoxShowSnap";
			this.checkBoxShowSnap.Size = new System.Drawing.Size(226, 17);
			this.checkBoxShowSnap.TabIndex = 11;
			this.checkBoxShowSnap.Text = "Show snap on second screen when taken";
			this.checkBoxShowSnap.UseVisualStyleBackColor = true;
			this.checkBoxShowSnap.CheckedChanged += new System.EventHandler(this.checkBoxShowSnap_CheckedChanged);
			// 
			// numericUpDownSnaps
			// 
			this.numericUpDownSnaps.Location = new System.Drawing.Point(125, 142);
			this.numericUpDownSnaps.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.numericUpDownSnaps.Name = "numericUpDownSnaps";
			this.numericUpDownSnaps.Size = new System.Drawing.Size(68, 20);
			this.numericUpDownSnaps.TabIndex = 10;
			this.numericUpDownSnaps.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.numericUpDownSnaps.ValueChanged += new System.EventHandler(this.numericUpDownSnaps_ValueChanged);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(0, 144);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(119, 13);
			this.label1.TabIndex = 9;
			this.label1.Text = "Max remembered snaps";
			// 
			// checkBoxExpandSnap
			// 
			this.checkBoxExpandSnap.AutoSize = true;
			this.checkBoxExpandSnap.Location = new System.Drawing.Point(3, 196);
			this.checkBoxExpandSnap.Name = "checkBoxExpandSnap";
			this.checkBoxExpandSnap.Size = new System.Drawing.Size(147, 17);
			this.checkBoxExpandSnap.TabIndex = 13;
			this.checkBoxExpandSnap.Text = "Expand snap to fill screen";
			this.checkBoxExpandSnap.UseVisualStyleBackColor = true;
			this.checkBoxExpandSnap.CheckedChanged += new System.EventHandler(this.checkBoxExpandSnap_CheckedChanged);
			// 
			// checkBoxShrinkSnap
			// 
			this.checkBoxShrinkSnap.AutoSize = true;
			this.checkBoxShrinkSnap.Location = new System.Drawing.Point(3, 219);
			this.checkBoxShrinkSnap.Name = "checkBoxShrinkSnap";
			this.checkBoxShrinkSnap.Size = new System.Drawing.Size(140, 17);
			this.checkBoxShrinkSnap.TabIndex = 14;
			this.checkBoxShrinkSnap.Text = "Shrink snap to fit screen";
			this.checkBoxShrinkSnap.UseVisualStyleBackColor = true;
			this.checkBoxShrinkSnap.CheckedChanged += new System.EventHandler(this.checkBoxShrinkSnap_CheckedChanged);
			// 
			// checkBoxMaintainAspectRatio
			// 
			this.checkBoxMaintainAspectRatio.AutoSize = true;
			this.checkBoxMaintainAspectRatio.Location = new System.Drawing.Point(3, 242);
			this.checkBoxMaintainAspectRatio.Name = "checkBoxMaintainAspectRatio";
			this.checkBoxMaintainAspectRatio.Size = new System.Drawing.Size(124, 17);
			this.checkBoxMaintainAspectRatio.TabIndex = 15;
			this.checkBoxMaintainAspectRatio.Text = "Maintain aspect ratio";
			this.checkBoxMaintainAspectRatio.UseVisualStyleBackColor = true;
			this.checkBoxMaintainAspectRatio.CheckedChanged += new System.EventHandler(this.checkBoxMaintainAspectRatio_CheckedChanged);
			// 
			// SnapGeneralOptionsPanel
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.checkBoxMaintainAspectRatio);
			this.Controls.Add(this.checkBoxShrinkSnap);
			this.Controls.Add(this.checkBoxExpandSnap);
			this.Controls.Add(this.groupBox3);
			this.Controls.Add(this.checkBoxShowSnap);
			this.Controls.Add(this.numericUpDownSnaps);
			this.Controls.Add(this.label1);
			this.Name = "SnapGeneralOptionsPanel";
			this.Size = new System.Drawing.Size(500, 360);
			this.Load += new System.EventHandler(this.SnapGeneralOptionsPanel_Load);
			this.groupBox3.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownSnaps)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.CheckBox checkBoxShowSnap;
		private System.Windows.Forms.NumericUpDown numericUpDownSnaps;
		private System.Windows.Forms.Label label1;
		private Library.HotKeys.HotKeyPanel hotKeyPanelTakeSnap;
		private Library.HotKeys.HotKeyPanel hotKeyPanelShowSnap;
		private System.Windows.Forms.CheckBox checkBoxExpandSnap;
		private System.Windows.Forms.CheckBox checkBoxShrinkSnap;
		private System.Windows.Forms.CheckBox checkBoxMaintainAspectRatio;
		private Library.HotKeys.HotKeyPanel hotKeyPanelTakeWinSnap;
	}
}
