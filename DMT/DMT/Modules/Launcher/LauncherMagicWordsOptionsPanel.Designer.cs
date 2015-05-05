namespace DMT.Modules.Launcher
{
	partial class LauncherMagicWordsOptionsPanel
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
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
			this.buttonResetCounts = new System.Windows.Forms.Button();
			this.buttonDelete = new System.Windows.Forms.Button();
			this.buttonEdit = new System.Windows.Forms.Button();
			this.buttonAdd = new System.Windows.Forms.Button();
			this.dataGridView = new System.Windows.Forms.DataGridView();
			this.magicWordsBindingSource = new System.Windows.Forms.BindingSource(this.components);
			this.aliasDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.filenameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Parameters = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Comment = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.UseCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.LastUsed = new System.Windows.Forms.DataGridViewTextBoxColumn();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.magicWordsBindingSource)).BeginInit();
			this.SuspendLayout();
			// 
			// buttonResetCounts
			// 
			this.buttonResetCounts.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonResetCounts.Location = new System.Drawing.Point(366, 295);
			this.buttonResetCounts.Name = "buttonResetCounts";
			this.buttonResetCounts.Size = new System.Drawing.Size(124, 23);
			this.buttonResetCounts.TabIndex = 9;
			this.buttonResetCounts.Text = "Reset usage counts";
			this.buttonResetCounts.UseVisualStyleBackColor = true;
			this.buttonResetCounts.Click += new System.EventHandler(this.buttonResetCounts_Click);
			// 
			// buttonDelete
			// 
			this.buttonDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.buttonDelete.Location = new System.Drawing.Point(168, 295);
			this.buttonDelete.Name = "buttonDelete";
			this.buttonDelete.Size = new System.Drawing.Size(75, 23);
			this.buttonDelete.TabIndex = 8;
			this.buttonDelete.Text = "Delete";
			this.buttonDelete.UseVisualStyleBackColor = true;
			this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
			// 
			// buttonEdit
			// 
			this.buttonEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.buttonEdit.Location = new System.Drawing.Point(86, 295);
			this.buttonEdit.Name = "buttonEdit";
			this.buttonEdit.Size = new System.Drawing.Size(75, 23);
			this.buttonEdit.TabIndex = 7;
			this.buttonEdit.Text = "Edit";
			this.buttonEdit.UseVisualStyleBackColor = true;
			this.buttonEdit.Click += new System.EventHandler(this.buttonEdit_Click);
			// 
			// buttonAdd
			// 
			this.buttonAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.buttonAdd.Location = new System.Drawing.Point(4, 295);
			this.buttonAdd.Name = "buttonAdd";
			this.buttonAdd.Size = new System.Drawing.Size(75, 23);
			this.buttonAdd.TabIndex = 6;
			this.buttonAdd.Text = "Add";
			this.buttonAdd.UseVisualStyleBackColor = true;
			this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
			// 
			// dataGridView
			// 
			this.dataGridView.AllowUserToAddRows = false;
			this.dataGridView.AllowUserToDeleteRows = false;
			this.dataGridView.AllowUserToResizeColumns = false;
			this.dataGridView.AllowUserToResizeRows = false;
			dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
			this.dataGridView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
			this.dataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.dataGridView.AutoGenerateColumns = false;
			this.dataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
			this.dataGridView.BackgroundColor = System.Drawing.SystemColors.Window;
			this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.aliasDataGridViewTextBoxColumn,
            this.filenameDataGridViewTextBoxColumn,
            this.Parameters,
            this.Comment,
            this.UseCount,
            this.LastUsed});
			this.dataGridView.DataSource = this.magicWordsBindingSource;
			this.dataGridView.Location = new System.Drawing.Point(6, 0);
			this.dataGridView.Name = "dataGridView";
			this.dataGridView.ReadOnly = true;
			this.dataGridView.RowHeadersVisible = false;
			this.dataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dataGridView.Size = new System.Drawing.Size(483, 286);
			this.dataGridView.TabIndex = 5;
			this.dataGridView.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_CellDoubleClick);
			this.dataGridView.SelectionChanged += new System.EventHandler(this.dataGridView_SelectionChanged);
			this.dataGridView.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dataGridView_KeyPress);
			// 
			// magicWordsBindingSource
			// 
			this.magicWordsBindingSource.DataSource = typeof(DMT.Modules.Launcher.MagicWords);
			// 
			// aliasDataGridViewTextBoxColumn
			// 
			this.aliasDataGridViewTextBoxColumn.DataPropertyName = "Alias";
			this.aliasDataGridViewTextBoxColumn.Frozen = true;
			this.aliasDataGridViewTextBoxColumn.HeaderText = "Magic Word";
			this.aliasDataGridViewTextBoxColumn.Name = "aliasDataGridViewTextBoxColumn";
			this.aliasDataGridViewTextBoxColumn.ReadOnly = true;
			this.aliasDataGridViewTextBoxColumn.Width = 90;
			// 
			// filenameDataGridViewTextBoxColumn
			// 
			this.filenameDataGridViewTextBoxColumn.DataPropertyName = "Filename";
			this.filenameDataGridViewTextBoxColumn.HeaderText = "Filename";
			this.filenameDataGridViewTextBoxColumn.Name = "filenameDataGridViewTextBoxColumn";
			this.filenameDataGridViewTextBoxColumn.ReadOnly = true;
			this.filenameDataGridViewTextBoxColumn.Width = 74;
			// 
			// Parameters
			// 
			this.Parameters.DataPropertyName = "Parameters";
			this.Parameters.HeaderText = "Parameters";
			this.Parameters.Name = "Parameters";
			this.Parameters.ReadOnly = true;
			this.Parameters.Width = 85;
			// 
			// Comment
			// 
			this.Comment.DataPropertyName = "Comment";
			this.Comment.HeaderText = "Comment";
			this.Comment.Name = "Comment";
			this.Comment.ReadOnly = true;
			this.Comment.Width = 76;
			// 
			// UseCount
			// 
			this.UseCount.DataPropertyName = "UseCount";
			this.UseCount.HeaderText = "UseCount";
			this.UseCount.Name = "UseCount";
			this.UseCount.ReadOnly = true;
			this.UseCount.Width = 79;
			// 
			// LastUsed
			// 
			this.LastUsed.DataPropertyName = "LastUsed";
			this.LastUsed.HeaderText = "LastUsed";
			this.LastUsed.Name = "LastUsed";
			this.LastUsed.ReadOnly = true;
			this.LastUsed.Width = 77;
			// 
			// LauncherMagicWordsOptionsPanel
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.buttonResetCounts);
			this.Controls.Add(this.buttonDelete);
			this.Controls.Add(this.buttonEdit);
			this.Controls.Add(this.buttonAdd);
			this.Controls.Add(this.dataGridView);
			this.Name = "LauncherMagicWordsOptionsPanel";
			this.Size = new System.Drawing.Size(493, 338);
			this.Load += new System.EventHandler(this.LauncherMagicWordsOptionsPanel_Load);
			((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.magicWordsBindingSource)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button buttonResetCounts;
		private System.Windows.Forms.Button buttonDelete;
		private System.Windows.Forms.Button buttonEdit;
		private System.Windows.Forms.Button buttonAdd;
		private System.Windows.Forms.DataGridView dataGridView;
		private System.Windows.Forms.BindingSource magicWordsBindingSource;
		private System.Windows.Forms.DataGridViewTextBoxColumn aliasDataGridViewTextBoxColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn filenameDataGridViewTextBoxColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn Parameters;
		private System.Windows.Forms.DataGridViewTextBoxColumn Comment;
		private System.Windows.Forms.DataGridViewTextBoxColumn UseCount;
		private System.Windows.Forms.DataGridViewTextBoxColumn LastUsed;
	}
}
