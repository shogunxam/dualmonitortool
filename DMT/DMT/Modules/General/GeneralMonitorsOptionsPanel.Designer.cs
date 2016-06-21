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
			this.dataGridView = new System.Windows.Forms.DataGridView();
			this.checkBoxShowVirtual = new System.Windows.Forms.CheckBox();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
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
			// 
			// checkBoxShowVirtual
			// 
			this.checkBoxShowVirtual.AutoSize = true;
			this.checkBoxShowVirtual.Location = new System.Drawing.Point(3, 318);
			this.checkBoxShowVirtual.Name = "checkBoxShowVirtual";
			this.checkBoxShowVirtual.Size = new System.Drawing.Size(128, 17);
			this.checkBoxShowVirtual.TabIndex = 1;
			this.checkBoxShowVirtual.Text = "Show Virtual Monitors";
			this.checkBoxShowVirtual.UseVisualStyleBackColor = true;
			this.checkBoxShowVirtual.CheckedChanged += new System.EventHandler(this.checkBoxShowVirtual_CheckedChanged);
			// 
			// GeneralMonitorsOptionsPanel
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.checkBoxShowVirtual);
			this.Controls.Add(this.dataGridView);
			this.Name = "GeneralMonitorsOptionsPanel";
			this.Size = new System.Drawing.Size(500, 338);
			((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.DataGridView dataGridView;
		private System.Windows.Forms.CheckBox checkBoxShowVirtual;


	}
}
