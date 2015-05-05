namespace DMT
{
	partial class OptionsForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OptionsForm));
			this.treeViewOptions = new System.Windows.Forms.TreeView();
			this.panelPlaceholder = new System.Windows.Forms.Panel();
			this.buttonClose = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// treeViewOptions
			// 
			this.treeViewOptions.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.treeViewOptions.HideSelection = false;
			this.treeViewOptions.Location = new System.Drawing.Point(2, 0);
			this.treeViewOptions.Name = "treeViewOptions";
			this.treeViewOptions.Size = new System.Drawing.Size(241, 367);
			this.treeViewOptions.TabIndex = 0;
			this.treeViewOptions.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeViewOptions_AfterSelect);
			// 
			// panelPlaceholder
			// 
			this.panelPlaceholder.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.panelPlaceholder.Location = new System.Drawing.Point(260, 10);
			this.panelPlaceholder.Name = "panelPlaceholder";
			this.panelPlaceholder.Size = new System.Drawing.Size(516, 357);
			this.panelPlaceholder.TabIndex = 1;
			this.panelPlaceholder.Visible = false;
			// 
			// buttonClose
			// 
			this.buttonClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonClose.Location = new System.Drawing.Point(688, 377);
			this.buttonClose.Name = "buttonClose";
			this.buttonClose.Size = new System.Drawing.Size(75, 23);
			this.buttonClose.TabIndex = 2;
			this.buttonClose.Text = "Close";
			this.buttonClose.UseVisualStyleBackColor = true;
			this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
			// 
			// OptionsForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(775, 412);
			this.Controls.Add(this.buttonClose);
			this.Controls.Add(this.panelPlaceholder);
			this.Controls.Add(this.treeViewOptions);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "OptionsForm";
			this.Text = "Options for Dual Monitor Tools";
			this.Load += new System.EventHandler(this.OptionsForm_Load);
			this.SizeChanged += new System.EventHandler(this.OptionsForm_SizeChanged);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TreeView treeViewOptions;
		private System.Windows.Forms.Panel panelPlaceholder;
		private System.Windows.Forms.Button buttonClose;
	}
}