namespace DualMonitorTools.DualWallpaperChanger
{
	partial class ProviderForm
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
			this.label1 = new System.Windows.Forms.Label();
			this.buttonOK = new System.Windows.Forms.Button();
			this.buttonCancel = new System.Windows.Forms.Button();
			this.dataGridView = new System.Windows.Forms.DataGridView();
			this.ProviderImage = new System.Windows.Forms.DataGridViewImageColumn();
			this.ProviderName = new System.Windows.Forms.DataGridViewTextBoxColumn();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(150, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Select required image provider";
			// 
			// buttonOK
			// 
			this.buttonOK.Location = new System.Drawing.Point(201, 266);
			this.buttonOK.Name = "buttonOK";
			this.buttonOK.Size = new System.Drawing.Size(75, 23);
			this.buttonOK.TabIndex = 2;
			this.buttonOK.Text = "OK";
			this.buttonOK.UseVisualStyleBackColor = true;
			this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
			// 
			// buttonCancel
			// 
			this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonCancel.Location = new System.Drawing.Point(282, 266);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new System.Drawing.Size(75, 23);
			this.buttonCancel.TabIndex = 3;
			this.buttonCancel.Text = "Cancel";
			this.buttonCancel.UseVisualStyleBackColor = true;
			// 
			// dataGridView
			// 
			this.dataGridView.AllowUserToAddRows = false;
			this.dataGridView.AllowUserToDeleteRows = false;
			this.dataGridView.AllowUserToResizeColumns = false;
			this.dataGridView.AllowUserToResizeRows = false;
			this.dataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.dataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
			this.dataGridView.BackgroundColor = System.Drawing.SystemColors.Window;
			this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ProviderImage,
            this.ProviderName});
			this.dataGridView.Location = new System.Drawing.Point(15, 25);
			this.dataGridView.MultiSelect = false;
			this.dataGridView.Name = "dataGridView";
			this.dataGridView.ReadOnly = true;
			this.dataGridView.RowHeadersVisible = false;
			this.dataGridView.RowTemplate.Height = 56;
			this.dataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dataGridView.Size = new System.Drawing.Size(565, 226);
			this.dataGridView.TabIndex = 4;
			this.dataGridView.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_CellDoubleClick);
			// 
			// ProviderImage
			// 
			this.ProviderImage.DataPropertyName = "Image";
			this.ProviderImage.HeaderText = "Provider";
			this.ProviderImage.Name = "ProviderImage";
			this.ProviderImage.ReadOnly = true;
			this.ProviderImage.Width = 52;
			// 
			// ProviderName
			// 
			this.ProviderName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.ProviderName.DataPropertyName = "Name";
			this.ProviderName.HeaderText = "Name";
			this.ProviderName.Name = "ProviderName";
			this.ProviderName.ReadOnly = true;
			// 
			// ProviderForm
			// 
			this.AcceptButton = this.buttonOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.buttonCancel;
			this.ClientSize = new System.Drawing.Size(592, 301);
			this.Controls.Add(this.dataGridView);
			this.Controls.Add(this.buttonCancel);
			this.Controls.Add(this.buttonOK);
			this.Controls.Add(this.label1);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ProviderForm";
			this.ShowInTaskbar = false;
			this.Text = "Choose Provider";
			((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button buttonOK;
		private System.Windows.Forms.Button buttonCancel;
		private System.Windows.Forms.DataGridView dataGridView;
		private System.Windows.Forms.DataGridViewImageColumn ProviderImage;
		private System.Windows.Forms.DataGridViewTextBoxColumn ProviderName;
	}
}