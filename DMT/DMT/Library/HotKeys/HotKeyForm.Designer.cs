namespace DMT.Library.HotKeys
{
	partial class HotKeyForm
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
			this.lblNote = new System.Windows.Forms.RichTextBox();
			this.buttonCancel = new System.Windows.Forms.Button();
			this.buttonOK = new System.Windows.Forms.Button();
			this.checkBoxEnable = new System.Windows.Forms.CheckBox();
			this.labelDescription = new System.Windows.Forms.Label();
			this.keyComboPanel = new DMT.Library.HotKeys.KeyComboPanel();
			this.SuspendLayout();
			// 
			// lblNote
			// 
			this.lblNote.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
			this.lblNote.Location = new System.Drawing.Point(10, 189);
			this.lblNote.Name = "lblNote";
			this.lblNote.ReadOnly = true;
			this.lblNote.Size = new System.Drawing.Size(360, 78);
			this.lblNote.TabIndex = 10;
			this.lblNote.Text = "";
			// 
			// buttonCancel
			// 
			this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonCancel.Location = new System.Drawing.Point(244, 160);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new System.Drawing.Size(75, 23);
			this.buttonCancel.TabIndex = 9;
			this.buttonCancel.Text = "Cancel";
			this.buttonCancel.UseVisualStyleBackColor = true;
			this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
			// 
			// buttonOK
			// 
			this.buttonOK.Location = new System.Drawing.Point(58, 160);
			this.buttonOK.Name = "buttonOK";
			this.buttonOK.Size = new System.Drawing.Size(75, 23);
			this.buttonOK.TabIndex = 8;
			this.buttonOK.Text = "OK";
			this.buttonOK.UseVisualStyleBackColor = true;
			this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
			// 
			// checkBoxEnable
			// 
			this.checkBoxEnable.AutoSize = true;
			this.checkBoxEnable.Location = new System.Drawing.Point(15, 29);
			this.checkBoxEnable.Name = "checkBoxEnable";
			this.checkBoxEnable.Size = new System.Drawing.Size(115, 17);
			this.checkBoxEnable.TabIndex = 7;
			this.checkBoxEnable.Text = "Enable this Hotkey";
			this.checkBoxEnable.UseVisualStyleBackColor = true;
			this.checkBoxEnable.CheckedChanged += new System.EventHandler(this.checkBoxEnable_CheckedChanged);
			// 
			// labelDescription
			// 
			this.labelDescription.Location = new System.Drawing.Point(12, 9);
			this.labelDescription.Name = "labelDescription";
			this.labelDescription.Size = new System.Drawing.Size(358, 17);
			this.labelDescription.TabIndex = 6;
			this.labelDescription.Text = "Hotkey to ...";
			// 
			// keyComboPanel
			// 
			this.keyComboPanel.Location = new System.Drawing.Point(15, 52);
			this.keyComboPanel.Name = "keyComboPanel";
			this.keyComboPanel.Size = new System.Drawing.Size(242, 95);
			this.keyComboPanel.TabIndex = 11;
			// 
			// HotKeyForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(380, 275);
			this.Controls.Add(this.keyComboPanel);
			this.Controls.Add(this.lblNote);
			this.Controls.Add(this.buttonCancel);
			this.Controls.Add(this.buttonOK);
			this.Controls.Add(this.checkBoxEnable);
			this.Controls.Add(this.labelDescription);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "HotKeyForm";
			this.ShowInTaskbar = false;
			this.Text = "Change Hotkey";
			this.Load += new System.EventHandler(this.HotKeyForm_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.RichTextBox lblNote;
		private System.Windows.Forms.Button buttonCancel;
		private System.Windows.Forms.Button buttonOK;
		private System.Windows.Forms.CheckBox checkBoxEnable;
		private System.Windows.Forms.Label labelDescription;
		private KeyComboPanel keyComboPanel;
	}
}