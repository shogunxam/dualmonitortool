namespace DMT.Modules.General
{
	partial class GeneralMonitorsOptionsPanel
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
			this.components = new System.ComponentModel.Container();
			this.dataGridView = new System.Windows.Forms.DataGridView();
			this.contextMenuStripChangePrimary = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.toolStripMenuItemChangePrimary = new System.Windows.Forms.ToolStripMenuItem();
			this.checkBoxShowAllMonitors = new System.Windows.Forms.CheckBox();
			this.labelErrorMsg = new System.Windows.Forms.Label();
			this.contextMenuStripChangeBrightness = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.toolStripMenuItemChangeBrightness = new System.Windows.Forms.ToolStripMenuItem();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
			this.contextMenuStripChangePrimary.SuspendLayout();
			this.contextMenuStripChangeBrightness.SuspendLayout();
			this.SuspendLayout();
			// 
			// dataGridView
			// 
			this.dataGridView.AllowUserToAddRows = false;
			this.dataGridView.AllowUserToDeleteRows = false;
			this.dataGridView.AllowUserToOrderColumns = true;
			this.dataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridView.Location = new System.Drawing.Point(3, 3);
			this.dataGridView.Name = "dataGridView";
			this.dataGridView.ReadOnly = true;
			this.dataGridView.RowHeadersWidth = 128;
			this.dataGridView.Size = new System.Drawing.Size(494, 296);
			this.dataGridView.TabIndex = 0;
			this.dataGridView.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_CellDoubleClick);
			this.dataGridView.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView_CellMouseDown);
			this.dataGridView.CellMouseEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_CellMouseEnter);
			this.dataGridView.CellMouseLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_CellMouseLeave);
			// 
			// contextMenuStripChangePrimary
			// 
			this.contextMenuStripChangePrimary.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemChangePrimary});
			this.contextMenuStripChangePrimary.Name = "dgvContextMenuStrip";
			this.contextMenuStripChangePrimary.Size = new System.Drawing.Size(236, 26);
			// 
			// toolStripMenuItemChangePrimary
			// 
			this.toolStripMenuItemChangePrimary.Name = "toolStripMenuItemChangePrimary";
			this.toolStripMenuItemChangePrimary.Size = new System.Drawing.Size(235, 22);
			this.toolStripMenuItemChangePrimary.Text = "Make this the primary monitor";
			this.toolStripMenuItemChangePrimary.Click += new System.EventHandler(this.toolStripMenuItemChangePrimary_Click);
			// 
			// checkBoxShowAllMonitors
			// 
			this.checkBoxShowAllMonitors.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.checkBoxShowAllMonitors.AutoSize = true;
			this.checkBoxShowAllMonitors.Location = new System.Drawing.Point(3, 318);
			this.checkBoxShowAllMonitors.Name = "checkBoxShowAllMonitors";
			this.checkBoxShowAllMonitors.Size = new System.Drawing.Size(110, 17);
			this.checkBoxShowAllMonitors.TabIndex = 1;
			this.checkBoxShowAllMonitors.Text = "Show All Monitors";
			this.checkBoxShowAllMonitors.UseVisualStyleBackColor = true;
			this.checkBoxShowAllMonitors.CheckedChanged += new System.EventHandler(this.checkBoxShowAll_CheckedChanged);
			// 
			// labelErrorMsg
			// 
			this.labelErrorMsg.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.labelErrorMsg.ForeColor = System.Drawing.Color.Red;
			this.labelErrorMsg.Location = new System.Drawing.Point(148, 319);
			this.labelErrorMsg.Name = "labelErrorMsg";
			this.labelErrorMsg.Size = new System.Drawing.Size(349, 13);
			this.labelErrorMsg.TabIndex = 2;
			// 
			// contextMenuStripChangeBrightness
			// 
			this.contextMenuStripChangeBrightness.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemChangeBrightness});
			this.contextMenuStripChangeBrightness.Name = "contextMenuStripChangeBrightness";
			this.contextMenuStripChangeBrightness.Size = new System.Drawing.Size(220, 48);
			// 
			// toolStripMenuItemChangeBrightness
			// 
			this.toolStripMenuItemChangeBrightness.Name = "toolStripMenuItemChangeBrightness";
			this.toolStripMenuItemChangeBrightness.Size = new System.Drawing.Size(219, 22);
			this.toolStripMenuItemChangeBrightness.Text = "Change monitor brightness";
			this.toolStripMenuItemChangeBrightness.Click += new System.EventHandler(this.toolStripMenuItemChangeBrightness_Click);
			// 
			// GeneralMonitorsOptionsPanel
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.labelErrorMsg);
			this.Controls.Add(this.checkBoxShowAllMonitors);
			this.Controls.Add(this.dataGridView);
			this.Name = "GeneralMonitorsOptionsPanel";
			this.Size = new System.Drawing.Size(500, 338);
			((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
			this.contextMenuStripChangePrimary.ResumeLayout(false);
			this.contextMenuStripChangeBrightness.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.DataGridView dataGridView;
		private System.Windows.Forms.CheckBox checkBoxShowAllMonitors;
		private System.Windows.Forms.Label labelErrorMsg;
		private System.Windows.Forms.ContextMenuStrip contextMenuStripChangePrimary;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemChangePrimary;
		private System.Windows.Forms.ContextMenuStrip contextMenuStripChangeBrightness;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemChangeBrightness;


	}
}
