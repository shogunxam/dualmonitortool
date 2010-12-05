namespace DualLauncher
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
			this.components = new System.ComponentModel.Container();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabPageMagicWords = new System.Windows.Forms.TabPage();
			this.buttonResetCounts = new System.Windows.Forms.Button();
			this.buttonDelete = new System.Windows.Forms.Button();
			this.buttonEdit = new System.Windows.Forms.Button();
			this.buttonAdd = new System.Windows.Forms.Button();
			this.dataGridView = new System.Windows.Forms.DataGridView();
			this.magicWordsBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
			this.tabPageKeys = new System.Windows.Forms.TabPage();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.label1 = new System.Windows.Forms.Label();
			this.buttonActivate = new System.Windows.Forms.Button();
			this.labelActivate = new System.Windows.Forms.Label();
			this.tabPageGeneral = new System.Windows.Forms.TabPage();
			this.checkBoxAutoStart = new System.Windows.Forms.CheckBox();
			this.tabPageImport = new System.Windows.Forms.TabPage();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.label2 = new System.Windows.Forms.Label();
			this.buttonImportQrs = new System.Windows.Forms.Button();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.buttonImportXml = new System.Windows.Forms.Button();
			this.buttonExportXml = new System.Windows.Forms.Button();
			this.buttonDeleteAll = new System.Windows.Forms.Button();
			this.buttonClose = new System.Windows.Forms.Button();
			this.magicWordsBindingSource = new System.Windows.Forms.BindingSource(this.components);
			this.aliasDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.filenameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Parameters = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Comment = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.UseCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.LastUsed = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.tabControl1.SuspendLayout();
			this.tabPageMagicWords.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.magicWordsBindingSource1)).BeginInit();
			this.tabPageKeys.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.tabPageGeneral.SuspendLayout();
			this.tabPageImport.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.groupBox2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.magicWordsBindingSource)).BeginInit();
			this.SuspendLayout();
			// 
			// tabControl1
			// 
			this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.tabControl1.Controls.Add(this.tabPageMagicWords);
			this.tabControl1.Controls.Add(this.tabPageKeys);
			this.tabControl1.Controls.Add(this.tabPageGeneral);
			this.tabControl1.Controls.Add(this.tabPageImport);
			this.tabControl1.Location = new System.Drawing.Point(12, 12);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(554, 320);
			this.tabControl1.TabIndex = 0;
			// 
			// tabPageMagicWords
			// 
			this.tabPageMagicWords.Controls.Add(this.buttonResetCounts);
			this.tabPageMagicWords.Controls.Add(this.buttonDelete);
			this.tabPageMagicWords.Controls.Add(this.buttonEdit);
			this.tabPageMagicWords.Controls.Add(this.buttonAdd);
			this.tabPageMagicWords.Controls.Add(this.dataGridView);
			this.tabPageMagicWords.Location = new System.Drawing.Point(4, 22);
			this.tabPageMagicWords.Name = "tabPageMagicWords";
			this.tabPageMagicWords.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageMagicWords.Size = new System.Drawing.Size(546, 294);
			this.tabPageMagicWords.TabIndex = 0;
			this.tabPageMagicWords.Text = "Magic Words";
			this.tabPageMagicWords.UseVisualStyleBackColor = true;
			// 
			// buttonResetCounts
			// 
			this.buttonResetCounts.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonResetCounts.Location = new System.Drawing.Point(416, 256);
			this.buttonResetCounts.Name = "buttonResetCounts";
			this.buttonResetCounts.Size = new System.Drawing.Size(124, 23);
			this.buttonResetCounts.TabIndex = 4;
			this.buttonResetCounts.Text = "Reset Usage Counts";
			this.buttonResetCounts.UseVisualStyleBackColor = true;
			this.buttonResetCounts.Click += new System.EventHandler(this.buttonResetCounts_Click);
			// 
			// buttonDelete
			// 
			this.buttonDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.buttonDelete.Location = new System.Drawing.Point(171, 256);
			this.buttonDelete.Name = "buttonDelete";
			this.buttonDelete.Size = new System.Drawing.Size(75, 23);
			this.buttonDelete.TabIndex = 3;
			this.buttonDelete.Text = "Delete";
			this.buttonDelete.UseVisualStyleBackColor = true;
			this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
			// 
			// buttonEdit
			// 
			this.buttonEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.buttonEdit.Location = new System.Drawing.Point(89, 256);
			this.buttonEdit.Name = "buttonEdit";
			this.buttonEdit.Size = new System.Drawing.Size(75, 23);
			this.buttonEdit.TabIndex = 2;
			this.buttonEdit.Text = "Edit";
			this.buttonEdit.UseVisualStyleBackColor = true;
			this.buttonEdit.Click += new System.EventHandler(this.buttonEdit_Click);
			// 
			// buttonAdd
			// 
			this.buttonAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.buttonAdd.Location = new System.Drawing.Point(7, 256);
			this.buttonAdd.Name = "buttonAdd";
			this.buttonAdd.Size = new System.Drawing.Size(75, 23);
			this.buttonAdd.TabIndex = 1;
			this.buttonAdd.Text = "Add";
			this.buttonAdd.UseVisualStyleBackColor = true;
			this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
			// 
			// dataGridView
			// 
			this.dataGridView.AllowUserToAddRows = false;
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
			this.dataGridView.DataSource = this.magicWordsBindingSource1;
			this.dataGridView.Location = new System.Drawing.Point(6, 6);
			this.dataGridView.Name = "dataGridView";
			this.dataGridView.RowHeadersVisible = false;
			this.dataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dataGridView.Size = new System.Drawing.Size(534, 243);
			this.dataGridView.TabIndex = 0;
			this.dataGridView.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_CellDoubleClick);
			this.dataGridView.SelectionChanged += new System.EventHandler(this.dataGridView_SelectionChanged);
			// 
			// magicWordsBindingSource1
			// 
			this.magicWordsBindingSource1.DataSource = typeof(DualLauncher.MagicWords);
			// 
			// tabPageKeys
			// 
			this.tabPageKeys.Controls.Add(this.groupBox1);
			this.tabPageKeys.Location = new System.Drawing.Point(4, 22);
			this.tabPageKeys.Name = "tabPageKeys";
			this.tabPageKeys.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageKeys.Size = new System.Drawing.Size(546, 294);
			this.tabPageKeys.TabIndex = 1;
			this.tabPageKeys.Text = "Keys";
			this.tabPageKeys.UseVisualStyleBackColor = true;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.buttonActivate);
			this.groupBox1.Controls.Add(this.labelActivate);
			this.groupBox1.Location = new System.Drawing.Point(16, 16);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(496, 127);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "HotKeys";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(6, 16);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(224, 13);
			this.label1.TabIndex = 8;
			this.label1.Text = "Activate Magic Word input";
			// 
			// buttonActivate
			// 
			this.buttonActivate.Location = new System.Drawing.Point(395, 11);
			this.buttonActivate.Name = "buttonActivate";
			this.buttonActivate.Size = new System.Drawing.Size(75, 23);
			this.buttonActivate.TabIndex = 7;
			this.buttonActivate.Text = "Change...";
			this.buttonActivate.UseVisualStyleBackColor = true;
			this.buttonActivate.Click += new System.EventHandler(this.buttonActivate_Click);
			// 
			// labelActivate
			// 
			this.labelActivate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
			this.labelActivate.Location = new System.Drawing.Point(236, 16);
			this.labelActivate.Name = "labelActivate";
			this.labelActivate.Size = new System.Drawing.Size(153, 13);
			this.labelActivate.TabIndex = 9;
			this.labelActivate.Text = "labelActivate";
			// 
			// tabPageGeneral
			// 
			this.tabPageGeneral.Controls.Add(this.checkBoxAutoStart);
			this.tabPageGeneral.Location = new System.Drawing.Point(4, 22);
			this.tabPageGeneral.Name = "tabPageGeneral";
			this.tabPageGeneral.Size = new System.Drawing.Size(546, 294);
			this.tabPageGeneral.TabIndex = 2;
			this.tabPageGeneral.Text = "General";
			this.tabPageGeneral.UseVisualStyleBackColor = true;
			// 
			// checkBoxAutoStart
			// 
			this.checkBoxAutoStart.AutoSize = true;
			this.checkBoxAutoStart.Location = new System.Drawing.Point(14, 14);
			this.checkBoxAutoStart.Name = "checkBoxAutoStart";
			this.checkBoxAutoStart.Size = new System.Drawing.Size(152, 17);
			this.checkBoxAutoStart.TabIndex = 0;
			this.checkBoxAutoStart.Text = "Start when Windows starts";
			this.checkBoxAutoStart.UseVisualStyleBackColor = true;
			// 
			// tabPageImport
			// 
			this.tabPageImport.Controls.Add(this.groupBox3);
			this.tabPageImport.Controls.Add(this.groupBox2);
			this.tabPageImport.Controls.Add(this.buttonDeleteAll);
			this.tabPageImport.Location = new System.Drawing.Point(4, 22);
			this.tabPageImport.Name = "tabPageImport";
			this.tabPageImport.Size = new System.Drawing.Size(546, 294);
			this.tabPageImport.TabIndex = 3;
			this.tabPageImport.Text = "Import / Export";
			this.tabPageImport.UseVisualStyleBackColor = true;
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.label2);
			this.groupBox3.Controls.Add(this.buttonImportQrs);
			this.groupBox3.Location = new System.Drawing.Point(13, 131);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(507, 139);
			this.groupBox3.TabIndex = 5;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Other Magic Word style file formats";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(193, 24);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(111, 13);
			this.label2.TabIndex = 1;
			this.label2.Text = "(As used by SlickRun)";
			// 
			// buttonImportQrs
			// 
			this.buttonImportQrs.Location = new System.Drawing.Point(13, 19);
			this.buttonImportQrs.Name = "buttonImportQrs";
			this.buttonImportQrs.Size = new System.Drawing.Size(148, 23);
			this.buttonImportQrs.TabIndex = 0;
			this.buttonImportQrs.Text = "Import QRS...";
			this.buttonImportQrs.UseVisualStyleBackColor = true;
			this.buttonImportQrs.Click += new System.EventHandler(this.buttonImportQrs_Click);
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.buttonImportXml);
			this.groupBox2.Controls.Add(this.buttonExportXml);
			this.groupBox2.Location = new System.Drawing.Point(13, 60);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(507, 60);
			this.groupBox2.TabIndex = 4;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "DualLauncher native file format";
			// 
			// buttonImportXml
			// 
			this.buttonImportXml.Location = new System.Drawing.Point(13, 19);
			this.buttonImportXml.Name = "buttonImportXml";
			this.buttonImportXml.Size = new System.Drawing.Size(148, 23);
			this.buttonImportXml.TabIndex = 1;
			this.buttonImportXml.Text = "Import XML...";
			this.buttonImportXml.UseVisualStyleBackColor = true;
			this.buttonImportXml.Click += new System.EventHandler(this.buttonImportXml_Click);
			// 
			// buttonExportXml
			// 
			this.buttonExportXml.Location = new System.Drawing.Point(257, 19);
			this.buttonExportXml.Name = "buttonExportXml";
			this.buttonExportXml.Size = new System.Drawing.Size(148, 23);
			this.buttonExportXml.TabIndex = 2;
			this.buttonExportXml.Text = "Export XML...";
			this.buttonExportXml.UseVisualStyleBackColor = true;
			this.buttonExportXml.Click += new System.EventHandler(this.buttonExportXml_Click);
			// 
			// buttonDeleteAll
			// 
			this.buttonDeleteAll.Location = new System.Drawing.Point(26, 19);
			this.buttonDeleteAll.Name = "buttonDeleteAll";
			this.buttonDeleteAll.Size = new System.Drawing.Size(148, 23);
			this.buttonDeleteAll.TabIndex = 3;
			this.buttonDeleteAll.Text = "Delete All Magic Words...";
			this.buttonDeleteAll.UseVisualStyleBackColor = true;
			this.buttonDeleteAll.Click += new System.EventHandler(this.buttonDeleteAll_Click);
			// 
			// buttonClose
			// 
			this.buttonClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.buttonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonClose.Location = new System.Drawing.Point(257, 340);
			this.buttonClose.Name = "buttonClose";
			this.buttonClose.Size = new System.Drawing.Size(75, 23);
			this.buttonClose.TabIndex = 1;
			this.buttonClose.Text = "Close";
			this.buttonClose.UseVisualStyleBackColor = true;
			// 
			// magicWordsBindingSource
			// 
			this.magicWordsBindingSource.DataSource = typeof(DualLauncher.MagicWords);
			// 
			// aliasDataGridViewTextBoxColumn
			// 
			this.aliasDataGridViewTextBoxColumn.DataPropertyName = "Alias";
			this.aliasDataGridViewTextBoxColumn.Frozen = true;
			this.aliasDataGridViewTextBoxColumn.HeaderText = "Alias";
			this.aliasDataGridViewTextBoxColumn.Name = "aliasDataGridViewTextBoxColumn";
			this.aliasDataGridViewTextBoxColumn.ReadOnly = true;
			this.aliasDataGridViewTextBoxColumn.Width = 54;
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
			this.LastUsed.Width = 77;
			// 
			// OptionsForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.buttonClose;
			this.ClientSize = new System.Drawing.Size(578, 375);
			this.Controls.Add(this.buttonClose);
			this.Controls.Add(this.tabControl1);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "OptionsForm";
			this.ShowInTaskbar = false;
			this.Text = "Options";
			this.Load += new System.EventHandler(this.OptionsForm_Load);
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OptionsForm_FormClosing);
			this.tabControl1.ResumeLayout(false);
			this.tabPageMagicWords.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.magicWordsBindingSource1)).EndInit();
			this.tabPageKeys.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			this.tabPageGeneral.ResumeLayout(false);
			this.tabPageGeneral.PerformLayout();
			this.tabPageImport.ResumeLayout(false);
			this.groupBox3.ResumeLayout(false);
			this.groupBox3.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.magicWordsBindingSource)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabPageMagicWords;
		private System.Windows.Forms.TabPage tabPageKeys;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.TabPage tabPageGeneral;
		private System.Windows.Forms.CheckBox checkBoxAutoStart;
		private System.Windows.Forms.Button buttonClose;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button buttonActivate;
		private System.Windows.Forms.Label labelActivate;
		private System.Windows.Forms.DataGridView dataGridView;
		private System.Windows.Forms.BindingSource magicWordsBindingSource;
		private System.Windows.Forms.Button buttonDelete;
		private System.Windows.Forms.Button buttonEdit;
		private System.Windows.Forms.Button buttonAdd;
		private System.Windows.Forms.BindingSource magicWordsBindingSource1;
		private System.Windows.Forms.TabPage tabPageImport;
		private System.Windows.Forms.Button buttonImportQrs;
		private System.Windows.Forms.Button buttonExportXml;
		private System.Windows.Forms.Button buttonImportXml;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Button buttonDeleteAll;
		private System.Windows.Forms.Button buttonResetCounts;
		private System.Windows.Forms.DataGridViewTextBoxColumn aliasDataGridViewTextBoxColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn filenameDataGridViewTextBoxColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn Parameters;
		private System.Windows.Forms.DataGridViewTextBoxColumn Comment;
		private System.Windows.Forms.DataGridViewTextBoxColumn UseCount;
		private System.Windows.Forms.DataGridViewTextBoxColumn LastUsed;
	}
}