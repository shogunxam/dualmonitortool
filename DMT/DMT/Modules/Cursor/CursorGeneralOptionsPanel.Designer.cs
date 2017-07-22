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
			this.comboBoxFreeMovementKey = new System.Windows.Forms.ComboBox();
			this.checkBoxPrimaryReturnUnhindered = new System.Windows.Forms.CheckBox();
			this.comboBoxCursorMode = new System.Windows.Forms.ComboBox();
			this.label15 = new System.Windows.Forms.Label();
			this.checkBoxControlUnhindersCursor = new System.Windows.Forms.CheckBox();
			this.groupBox4 = new System.Windows.Forms.GroupBox();
			this.label13 = new System.Windows.Forms.Label();
			this.label11 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.scrollBarSticky = new System.Windows.Forms.HScrollBar();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.hotKeyPanelCursorToPrimaryScreen = new DMT.Library.HotKeys.HotKeyPanel();
			this.hotKeyPanelCursorPrevScreen = new DMT.Library.HotKeys.HotKeyPanel();
			this.hotKeyPanelCursorNextScreen = new DMT.Library.HotKeys.HotKeyPanel();
			this.hotKeyPanelLockCursor = new DMT.Library.HotKeys.HotKeyPanel();
			this.hotKeyPanelStickyCursor = new DMT.Library.HotKeys.HotKeyPanel();
			this.hotKeyPanelFreeCursor = new DMT.Library.HotKeys.HotKeyPanel();
			this.checkBoxAllowButton = new System.Windows.Forms.CheckBox();
			this.comboBoxFreeMovementButton = new System.Windows.Forms.ComboBox();
			this.groupBox4.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.SuspendLayout();
			// 
			// comboBoxFreeMovementKey
			// 
			this.comboBoxFreeMovementKey.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxFreeMovementKey.FormattingEnabled = true;
			this.comboBoxFreeMovementKey.Location = new System.Drawing.Point(279, 251);
			this.comboBoxFreeMovementKey.Name = "comboBoxFreeMovementKey";
			this.comboBoxFreeMovementKey.Size = new System.Drawing.Size(194, 21);
			this.comboBoxFreeMovementKey.TabIndex = 52;
			this.comboBoxFreeMovementKey.SelectedIndexChanged += new System.EventHandler(this.comboBoxFreeMovementKey_SelectedIndexChanged);
			// 
			// checkBoxPrimaryReturnUnhindered
			// 
			this.checkBoxPrimaryReturnUnhindered.AutoSize = true;
			this.checkBoxPrimaryReturnUnhindered.Location = new System.Drawing.Point(10, 301);
			this.checkBoxPrimaryReturnUnhindered.Name = "checkBoxPrimaryReturnUnhindered";
			this.checkBoxPrimaryReturnUnhindered.Size = new System.Drawing.Size(254, 17);
			this.checkBoxPrimaryReturnUnhindered.TabIndex = 51;
			this.checkBoxPrimaryReturnUnhindered.Text = "Allow cursor to return freely to the primary screen";
			this.checkBoxPrimaryReturnUnhindered.UseVisualStyleBackColor = true;
			this.checkBoxPrimaryReturnUnhindered.CheckedChanged += new System.EventHandler(this.checkBoxPrimaryReturnUnhindered_CheckedChanged);
			// 
			// comboBoxCursorMode
			// 
			this.comboBoxCursorMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxCursorMode.FormattingEnabled = true;
			this.comboBoxCursorMode.Location = new System.Drawing.Point(174, 318);
			this.comboBoxCursorMode.Name = "comboBoxCursorMode";
			this.comboBoxCursorMode.Size = new System.Drawing.Size(300, 21);
			this.comboBoxCursorMode.TabIndex = 50;
			this.comboBoxCursorMode.SelectedIndexChanged += new System.EventHandler(this.comboBoxCursorMode_SelectedIndexChanged);
			// 
			// label15
			// 
			this.label15.AutoSize = true;
			this.label15.Location = new System.Drawing.Point(10, 321);
			this.label15.Name = "label15";
			this.label15.Size = new System.Drawing.Size(155, 13);
			this.label15.TabIndex = 49;
			this.label15.Text = "Default cursor mode on startup:";
			// 
			// checkBoxControlUnhindersCursor
			// 
			this.checkBoxControlUnhindersCursor.AutoSize = true;
			this.checkBoxControlUnhindersCursor.Location = new System.Drawing.Point(11, 255);
			this.checkBoxControlUnhindersCursor.Name = "checkBoxControlUnhindersCursor";
			this.checkBoxControlUnhindersCursor.Size = new System.Drawing.Size(249, 17);
			this.checkBoxControlUnhindersCursor.TabIndex = 48;
			this.checkBoxControlUnhindersCursor.Text = "Allow cursor to move freely if this key is pressed";
			this.checkBoxControlUnhindersCursor.UseVisualStyleBackColor = true;
			this.checkBoxControlUnhindersCursor.CheckedChanged += new System.EventHandler(this.checkBoxControlUnhindersCursor_CheckedChanged);
			// 
			// groupBox4
			// 
			this.groupBox4.Controls.Add(this.label13);
			this.groupBox4.Controls.Add(this.label11);
			this.groupBox4.Controls.Add(this.label5);
			this.groupBox4.Controls.Add(this.scrollBarSticky);
			this.groupBox4.Location = new System.Drawing.Point(3, 206);
			this.groupBox4.Name = "groupBox4";
			this.groupBox4.Size = new System.Drawing.Size(477, 43);
			this.groupBox4.TabIndex = 47;
			this.groupBox4.TabStop = false;
			this.groupBox4.Text = "Sticky cursor options";
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
			// scrollBarSticky
			// 
			this.scrollBarSticky.Location = new System.Drawing.Point(264, 14);
			this.scrollBarSticky.Maximum = 3000;
			this.scrollBarSticky.Name = "scrollBarSticky";
			this.scrollBarSticky.Size = new System.Drawing.Size(177, 17);
			this.scrollBarSticky.SmallChange = 10;
			this.scrollBarSticky.TabIndex = 38;
			this.scrollBarSticky.Value = 100;
			this.scrollBarSticky.ValueChanged += new System.EventHandler(this.scrollBarSticky_ValueChanged);
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.hotKeyPanelCursorToPrimaryScreen);
			this.groupBox3.Controls.Add(this.hotKeyPanelCursorPrevScreen);
			this.groupBox3.Controls.Add(this.hotKeyPanelCursorNextScreen);
			this.groupBox3.Controls.Add(this.hotKeyPanelLockCursor);
			this.groupBox3.Controls.Add(this.hotKeyPanelStickyCursor);
			this.groupBox3.Controls.Add(this.hotKeyPanelFreeCursor);
			this.groupBox3.Location = new System.Drawing.Point(3, 3);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(477, 194);
			this.groupBox3.TabIndex = 46;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Cursor movement HotKeys";
			// 
			// hotKeyPanelCursorToPrimaryScreen
			// 
			this.hotKeyPanelCursorToPrimaryScreen.Description = "Description";
			this.hotKeyPanelCursorToPrimaryScreen.Location = new System.Drawing.Point(8, 164);
			this.hotKeyPanelCursorToPrimaryScreen.Name = "hotKeyPanelCursorToPrimaryScreen";
			this.hotKeyPanelCursorToPrimaryScreen.Size = new System.Drawing.Size(465, 23);
			this.hotKeyPanelCursorToPrimaryScreen.TabIndex = 5;
			// 
			// hotKeyPanelCursorPrevScreen
			// 
			this.hotKeyPanelCursorPrevScreen.Description = "Description";
			this.hotKeyPanelCursorPrevScreen.Location = new System.Drawing.Point(8, 135);
			this.hotKeyPanelCursorPrevScreen.Name = "hotKeyPanelCursorPrevScreen";
			this.hotKeyPanelCursorPrevScreen.Size = new System.Drawing.Size(465, 23);
			this.hotKeyPanelCursorPrevScreen.TabIndex = 4;
			// 
			// hotKeyPanelCursorNextScreen
			// 
			this.hotKeyPanelCursorNextScreen.Description = "Description";
			this.hotKeyPanelCursorNextScreen.Location = new System.Drawing.Point(8, 106);
			this.hotKeyPanelCursorNextScreen.Name = "hotKeyPanelCursorNextScreen";
			this.hotKeyPanelCursorNextScreen.Size = new System.Drawing.Size(465, 23);
			this.hotKeyPanelCursorNextScreen.TabIndex = 3;
			// 
			// hotKeyPanelLockCursor
			// 
			this.hotKeyPanelLockCursor.Description = "Description";
			this.hotKeyPanelLockCursor.Location = new System.Drawing.Point(8, 77);
			this.hotKeyPanelLockCursor.Name = "hotKeyPanelLockCursor";
			this.hotKeyPanelLockCursor.Size = new System.Drawing.Size(465, 23);
			this.hotKeyPanelLockCursor.TabIndex = 2;
			// 
			// hotKeyPanelStickyCursor
			// 
			this.hotKeyPanelStickyCursor.Description = "Description";
			this.hotKeyPanelStickyCursor.Location = new System.Drawing.Point(8, 48);
			this.hotKeyPanelStickyCursor.Name = "hotKeyPanelStickyCursor";
			this.hotKeyPanelStickyCursor.Size = new System.Drawing.Size(465, 23);
			this.hotKeyPanelStickyCursor.TabIndex = 1;
			// 
			// hotKeyPanelFreeCursor
			// 
			this.hotKeyPanelFreeCursor.Description = "Description";
			this.hotKeyPanelFreeCursor.Location = new System.Drawing.Point(8, 19);
			this.hotKeyPanelFreeCursor.Name = "hotKeyPanelFreeCursor";
			this.hotKeyPanelFreeCursor.Size = new System.Drawing.Size(465, 23);
			this.hotKeyPanelFreeCursor.TabIndex = 0;
			// 
			// checkBoxAllowButton
			// 
			this.checkBoxAllowButton.AutoSize = true;
			this.checkBoxAllowButton.Location = new System.Drawing.Point(11, 278);
			this.checkBoxAllowButton.Name = "checkBoxAllowButton";
			this.checkBoxAllowButton.Size = new System.Drawing.Size(262, 17);
			this.checkBoxAllowButton.TabIndex = 53;
			this.checkBoxAllowButton.Text = "Allow cursor to move freely if this button is pressed";
			this.checkBoxAllowButton.UseVisualStyleBackColor = true;
			this.checkBoxAllowButton.CheckedChanged += new System.EventHandler(this.checkBoxAllowButton_CheckedChanged);
			// 
			// comboBoxFreeMovementButton
			// 
			this.comboBoxFreeMovementButton.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxFreeMovementButton.FormattingEnabled = true;
			this.comboBoxFreeMovementButton.Location = new System.Drawing.Point(279, 276);
			this.comboBoxFreeMovementButton.Name = "comboBoxFreeMovementButton";
			this.comboBoxFreeMovementButton.Size = new System.Drawing.Size(194, 21);
			this.comboBoxFreeMovementButton.TabIndex = 54;
			this.comboBoxFreeMovementButton.SelectedIndexChanged += new System.EventHandler(this.comboBoxFreeMovementButton_SelectedIndexChanged);
			// 
			// CursorGeneralOptionsPanel
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.comboBoxFreeMovementButton);
			this.Controls.Add(this.checkBoxAllowButton);
			this.Controls.Add(this.comboBoxFreeMovementKey);
			this.Controls.Add(this.checkBoxPrimaryReturnUnhindered);
			this.Controls.Add(this.comboBoxCursorMode);
			this.Controls.Add(this.label15);
			this.Controls.Add(this.checkBoxControlUnhindersCursor);
			this.Controls.Add(this.groupBox4);
			this.Controls.Add(this.groupBox3);
			this.Name = "CursorGeneralOptionsPanel";
			this.Size = new System.Drawing.Size(500, 360);
			this.groupBox4.ResumeLayout(false);
			this.groupBox4.PerformLayout();
			this.groupBox3.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ComboBox comboBoxFreeMovementKey;
		private System.Windows.Forms.CheckBox checkBoxPrimaryReturnUnhindered;
		private System.Windows.Forms.ComboBox comboBoxCursorMode;
		private System.Windows.Forms.Label label15;
		private System.Windows.Forms.CheckBox checkBoxControlUnhindersCursor;
		private System.Windows.Forms.GroupBox groupBox4;
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.HScrollBar scrollBarSticky;
		private System.Windows.Forms.GroupBox groupBox3;
		private Library.HotKeys.HotKeyPanel hotKeyPanelCursorPrevScreen;
		private Library.HotKeys.HotKeyPanel hotKeyPanelCursorNextScreen;
		private Library.HotKeys.HotKeyPanel hotKeyPanelLockCursor;
		private Library.HotKeys.HotKeyPanel hotKeyPanelStickyCursor;
		private Library.HotKeys.HotKeyPanel hotKeyPanelFreeCursor;
		private System.Windows.Forms.CheckBox checkBoxAllowButton;
		private System.Windows.Forms.ComboBox comboBoxFreeMovementButton;
		private Library.HotKeys.HotKeyPanel hotKeyPanelCursorToPrimaryScreen;

	}
}
