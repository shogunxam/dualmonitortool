namespace DMT.Modules.Launcher
{
	partial class EntryForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EntryForm));
			this.pictureBoxIcon = new System.Windows.Forms.PictureBox();
			this.textBoxInput = new System.Windows.Forms.TextBox();
			this.buttonOptions = new System.Windows.Forms.Button();
			this.magicWordListBox = new DMT.Modules.Launcher.MagicWordListBox();
			this.columnHeaderAlias = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeaderFilename = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxIcon)).BeginInit();
			this.SuspendLayout();
			// 
			// pictureBoxIcon
			// 
			this.pictureBoxIcon.Location = new System.Drawing.Point(12, 12);
			this.pictureBoxIcon.Name = "pictureBoxIcon";
			this.pictureBoxIcon.Size = new System.Drawing.Size(32, 32);
			this.pictureBoxIcon.TabIndex = 2;
			this.pictureBoxIcon.TabStop = false;
			// 
			// textBoxInput
			// 
			this.textBoxInput.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxInput.Location = new System.Drawing.Point(63, 19);
			this.textBoxInput.Name = "textBoxInput";
			this.textBoxInput.Size = new System.Drawing.Size(310, 20);
			this.textBoxInput.TabIndex = 3;
			this.textBoxInput.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxInput_KeyDown);
			// 
			// buttonOptions
			// 
			this.buttonOptions.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonOptions.Image = ((System.Drawing.Image)(resources.GetObject("buttonOptions.Image")));
			this.buttonOptions.Location = new System.Drawing.Point(379, 19);
			this.buttonOptions.Name = "buttonOptions";
			this.buttonOptions.Size = new System.Drawing.Size(24, 24);
			this.buttonOptions.TabIndex = 4;
			this.buttonOptions.UseVisualStyleBackColor = true;
			// 
			// magicWordListBox
			// 
			this.magicWordListBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.magicWordListBox.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderAlias,
            this.columnHeaderFilename});
			this.magicWordListBox.Location = new System.Drawing.Point(12, 50);
			this.magicWordListBox.Name = "magicWordListBox";
			this.magicWordListBox.Size = new System.Drawing.Size(391, 169);
			this.magicWordListBox.TabIndex = 5;
			this.magicWordListBox.UseCompatibleStateImageBehavior = false;
			this.magicWordListBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.magicWordListBox_KeyDown);
			this.magicWordListBox.MouseClick += new System.Windows.Forms.MouseEventHandler(this.magicWordListBox_MouseClick);
			// 
			// columnHeaderAlias
			// 
			this.columnHeaderAlias.Text = "Magic Word";
			this.columnHeaderAlias.Width = 120;
			// 
			// columnHeaderFilename
			// 
			this.columnHeaderFilename.Text = "Filename";
			this.columnHeaderFilename.Width = 200;
			// 
			// EntryForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(415, 231);
			this.Controls.Add(this.magicWordListBox);
			this.Controls.Add(this.buttonOptions);
			this.Controls.Add(this.textBoxInput);
			this.Controls.Add(this.pictureBoxIcon);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "EntryForm";
			this.ShowInTaskbar = false;
			this.Text = "DMT - Launcher";
			this.Deactivate += new System.EventHandler(this.EntryForm_Deactivate);
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.EntryForm_FormClosing);
			this.Load += new System.EventHandler(this.EntryForm_Load);
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxIcon)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ColumnHeader columnHeaderAlias;
		private System.Windows.Forms.ColumnHeader columnHeaderFilename;
		private System.Windows.Forms.PictureBox pictureBoxIcon;
		private System.Windows.Forms.TextBox textBoxInput;
		private System.Windows.Forms.Button buttonOptions;
		private MagicWordListBox magicWordListBox;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
	}
}