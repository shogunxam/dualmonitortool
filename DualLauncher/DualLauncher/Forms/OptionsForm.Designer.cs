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
			this.magicWordsBindingSource = new System.Windows.Forms.BindingSource(this.components);
			this.tabPageKeys = new System.Windows.Forms.TabPage();
			this.groupBox4 = new System.Windows.Forms.GroupBox();
			this.labelPos4Prompt = new System.Windows.Forms.Label();
			this.buttonPos4 = new System.Windows.Forms.Button();
			this.labelPos4 = new System.Windows.Forms.Label();
			this.labelPos3Prompt = new System.Windows.Forms.Label();
			this.buttonPos3 = new System.Windows.Forms.Button();
			this.labelPos3 = new System.Windows.Forms.Label();
			this.labelPos2Prompt = new System.Windows.Forms.Label();
			this.buttonPos2 = new System.Windows.Forms.Button();
			this.labelPos2 = new System.Windows.Forms.Label();
			this.labelPos1Prompt = new System.Windows.Forms.Label();
			this.buttonPos1 = new System.Windows.Forms.Button();
			this.labelPos1 = new System.Windows.Forms.Label();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.label4 = new System.Windows.Forms.Label();
			this.buttonAddMagicWord = new System.Windows.Forms.Button();
			this.labelAddMagicWord = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.buttonActivate = new System.Windows.Forms.Button();
			this.labelActivate = new System.Windows.Forms.Label();
			this.tabPageGeneral = new System.Windows.Forms.TabPage();
			this.label6 = new System.Windows.Forms.Label();
			this.numericUpDownTimeout = new System.Windows.Forms.NumericUpDown();
			this.label5 = new System.Windows.Forms.Label();
			this.groupBox5 = new System.Windows.Forms.GroupBox();
			this.radioButtonIconDetails = new System.Windows.Forms.RadioButton();
			this.radioButtonIconList = new System.Windows.Forms.RadioButton();
			this.radioButtonIconLargeIcon = new System.Windows.Forms.RadioButton();
			this.label3 = new System.Windows.Forms.Label();
			this.numericUpDownIcons = new System.Windows.Forms.NumericUpDown();
			this.checkBoxMru = new System.Windows.Forms.CheckBox();
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
			this.toolTip = new System.Windows.Forms.ToolTip(this.components);
			this.aliasDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.filenameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Parameters = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Comment = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.UseCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.LastUsed = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.tabControl1.SuspendLayout();
			this.tabPageMagicWords.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.magicWordsBindingSource)).BeginInit();
			this.tabPageKeys.SuspendLayout();
			this.groupBox4.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.tabPageGeneral.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownTimeout)).BeginInit();
			this.groupBox5.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownIcons)).BeginInit();
			this.tabPageImport.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.groupBox2.SuspendLayout();
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
			this.toolTip.SetToolTip(this.tabControl1, "Deletes ALL Magic Words.\r\nUseful before an import if you want the imported list t" +
					"o replace all of your current Magic Words.");
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
			this.buttonResetCounts.Text = "Reset usage counts";
			this.toolTip.SetToolTip(this.buttonResetCounts, "Reset the number of times you have used each Magic Word.");
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
			this.toolTip.SetToolTip(this.buttonDelete, "Delete the selected Magic Word.");
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
			this.toolTip.SetToolTip(this.buttonEdit, "Edit the currently selected Magic Word.");
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
			this.toolTip.SetToolTip(this.buttonAdd, "Add a new Magic Word.");
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
			this.dataGridView.Location = new System.Drawing.Point(6, 6);
			this.dataGridView.Name = "dataGridView";
			this.dataGridView.ReadOnly = true;
			this.dataGridView.RowHeadersVisible = false;
			this.dataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dataGridView.Size = new System.Drawing.Size(534, 243);
			this.dataGridView.TabIndex = 0;
			this.toolTip.SetToolTip(this.dataGridView, "Click on the column headers to sort.\r\nDouble-click on a row to edit it.");
			this.dataGridView.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_CellDoubleClick);
			this.dataGridView.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dataGridView_KeyPress);
			this.dataGridView.SelectionChanged += new System.EventHandler(this.dataGridView_SelectionChanged);
			// 
			// magicWordsBindingSource
			// 
			this.magicWordsBindingSource.DataSource = typeof(DualLauncher.MagicWords);
			// 
			// tabPageKeys
			// 
			this.tabPageKeys.Controls.Add(this.groupBox4);
			this.tabPageKeys.Controls.Add(this.groupBox1);
			this.tabPageKeys.Location = new System.Drawing.Point(4, 22);
			this.tabPageKeys.Name = "tabPageKeys";
			this.tabPageKeys.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageKeys.Size = new System.Drawing.Size(546, 294);
			this.tabPageKeys.TabIndex = 1;
			this.tabPageKeys.Text = "Keys";
			this.tabPageKeys.UseVisualStyleBackColor = true;
			// 
			// groupBox4
			// 
			this.groupBox4.Controls.Add(this.labelPos4Prompt);
			this.groupBox4.Controls.Add(this.buttonPos4);
			this.groupBox4.Controls.Add(this.labelPos4);
			this.groupBox4.Controls.Add(this.labelPos3Prompt);
			this.groupBox4.Controls.Add(this.buttonPos3);
			this.groupBox4.Controls.Add(this.labelPos3);
			this.groupBox4.Controls.Add(this.labelPos2Prompt);
			this.groupBox4.Controls.Add(this.buttonPos2);
			this.groupBox4.Controls.Add(this.labelPos2);
			this.groupBox4.Controls.Add(this.labelPos1Prompt);
			this.groupBox4.Controls.Add(this.buttonPos1);
			this.groupBox4.Controls.Add(this.labelPos1);
			this.groupBox4.Location = new System.Drawing.Point(16, 120);
			this.groupBox4.Name = "groupBox4";
			this.groupBox4.Size = new System.Drawing.Size(496, 145);
			this.groupBox4.TabIndex = 1;
			this.groupBox4.TabStop = false;
			this.groupBox4.Text = "Magic Word accept keys  for the 4 different opening positions";
			// 
			// labelPos4Prompt
			// 
			this.labelPos4Prompt.Location = new System.Drawing.Point(6, 111);
			this.labelPos4Prompt.Name = "labelPos4Prompt";
			this.labelPos4Prompt.Size = new System.Drawing.Size(224, 13);
			this.labelPos4Prompt.TabIndex = 20;
			this.labelPos4Prompt.Text = "Open window in position 4";
			// 
			// buttonPos4
			// 
			this.buttonPos4.Location = new System.Drawing.Point(395, 106);
			this.buttonPos4.Name = "buttonPos4";
			this.buttonPos4.Size = new System.Drawing.Size(75, 23);
			this.buttonPos4.TabIndex = 19;
			this.buttonPos4.Text = "Change...";
			this.toolTip.SetToolTip(this.buttonPos4, "Click to change the accept key for position 4.");
			this.buttonPos4.UseVisualStyleBackColor = true;
			this.buttonPos4.Click += new System.EventHandler(this.buttonPos4_Click);
			// 
			// labelPos4
			// 
			this.labelPos4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
			this.labelPos4.Location = new System.Drawing.Point(236, 111);
			this.labelPos4.Name = "labelPos4";
			this.labelPos4.Size = new System.Drawing.Size(153, 13);
			this.labelPos4.TabIndex = 21;
			this.labelPos4.Text = "labelPos4";
			this.toolTip.SetToolTip(this.labelPos4, "Press this key combination in the Magic Word entry box\r\nto open the window in Sta" +
					"rt Position 4.\r\n");
			// 
			// labelPos3Prompt
			// 
			this.labelPos3Prompt.Location = new System.Drawing.Point(6, 82);
			this.labelPos3Prompt.Name = "labelPos3Prompt";
			this.labelPos3Prompt.Size = new System.Drawing.Size(224, 13);
			this.labelPos3Prompt.TabIndex = 17;
			this.labelPos3Prompt.Text = "Open window in position 3";
			// 
			// buttonPos3
			// 
			this.buttonPos3.Location = new System.Drawing.Point(395, 77);
			this.buttonPos3.Name = "buttonPos3";
			this.buttonPos3.Size = new System.Drawing.Size(75, 23);
			this.buttonPos3.TabIndex = 16;
			this.buttonPos3.Text = "Change...";
			this.toolTip.SetToolTip(this.buttonPos3, "Click to change the accept key for position 3.");
			this.buttonPos3.UseVisualStyleBackColor = true;
			this.buttonPos3.Click += new System.EventHandler(this.buttonPos3_Click);
			// 
			// labelPos3
			// 
			this.labelPos3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
			this.labelPos3.Location = new System.Drawing.Point(236, 82);
			this.labelPos3.Name = "labelPos3";
			this.labelPos3.Size = new System.Drawing.Size(153, 13);
			this.labelPos3.TabIndex = 18;
			this.labelPos3.Text = "labelPos3";
			this.toolTip.SetToolTip(this.labelPos3, "Press this key combination in the Magic Word entry box\r\nto open the window in Sta" +
					"rt Position 3.");
			// 
			// labelPos2Prompt
			// 
			this.labelPos2Prompt.Location = new System.Drawing.Point(6, 53);
			this.labelPos2Prompt.Name = "labelPos2Prompt";
			this.labelPos2Prompt.Size = new System.Drawing.Size(224, 13);
			this.labelPos2Prompt.TabIndex = 14;
			this.labelPos2Prompt.Text = "Open window in position 2";
			// 
			// buttonPos2
			// 
			this.buttonPos2.Location = new System.Drawing.Point(395, 48);
			this.buttonPos2.Name = "buttonPos2";
			this.buttonPos2.Size = new System.Drawing.Size(75, 23);
			this.buttonPos2.TabIndex = 13;
			this.buttonPos2.Text = "Change...";
			this.toolTip.SetToolTip(this.buttonPos2, "Click to change the accept key for position 2.");
			this.buttonPos2.UseVisualStyleBackColor = true;
			this.buttonPos2.Click += new System.EventHandler(this.buttonPos2_Click);
			// 
			// labelPos2
			// 
			this.labelPos2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
			this.labelPos2.Location = new System.Drawing.Point(236, 53);
			this.labelPos2.Name = "labelPos2";
			this.labelPos2.Size = new System.Drawing.Size(153, 13);
			this.labelPos2.TabIndex = 15;
			this.labelPos2.Text = "labelPos2";
			this.toolTip.SetToolTip(this.labelPos2, "Press this key combination in the Magic Word entry box\r\nto open the window in Sta" +
					"rt Position 2.");
			// 
			// labelPos1Prompt
			// 
			this.labelPos1Prompt.Location = new System.Drawing.Point(6, 24);
			this.labelPos1Prompt.Name = "labelPos1Prompt";
			this.labelPos1Prompt.Size = new System.Drawing.Size(224, 13);
			this.labelPos1Prompt.TabIndex = 11;
			this.labelPos1Prompt.Text = "Open window in position 1";
			// 
			// buttonPos1
			// 
			this.buttonPos1.Location = new System.Drawing.Point(395, 19);
			this.buttonPos1.Name = "buttonPos1";
			this.buttonPos1.Size = new System.Drawing.Size(75, 23);
			this.buttonPos1.TabIndex = 10;
			this.buttonPos1.Text = "Change...";
			this.toolTip.SetToolTip(this.buttonPos1, "Click to change the accept key for position 1.");
			this.buttonPos1.UseVisualStyleBackColor = true;
			this.buttonPos1.Click += new System.EventHandler(this.buttonPos1_Click);
			// 
			// labelPos1
			// 
			this.labelPos1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
			this.labelPos1.Location = new System.Drawing.Point(236, 24);
			this.labelPos1.Name = "labelPos1";
			this.labelPos1.Size = new System.Drawing.Size(153, 13);
			this.labelPos1.TabIndex = 12;
			this.labelPos1.Text = "labelPos1";
			this.toolTip.SetToolTip(this.labelPos1, "Press this key combination in the Magic Word entry box\r\nto open the window in Sta" +
					"rt Position 1.");
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.label4);
			this.groupBox1.Controls.Add(this.buttonAddMagicWord);
			this.groupBox1.Controls.Add(this.labelAddMagicWord);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.buttonActivate);
			this.groupBox1.Controls.Add(this.labelActivate);
			this.groupBox1.Location = new System.Drawing.Point(16, 16);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(496, 75);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "HotKeys";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(6, 45);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(224, 13);
			this.label4.TabIndex = 11;
			this.label4.Text = "Add Magic Word for current application";
			// 
			// buttonAddMagicWord
			// 
			this.buttonAddMagicWord.Location = new System.Drawing.Point(395, 40);
			this.buttonAddMagicWord.Name = "buttonAddMagicWord";
			this.buttonAddMagicWord.Size = new System.Drawing.Size(75, 23);
			this.buttonAddMagicWord.TabIndex = 10;
			this.buttonAddMagicWord.Text = "Change...";
			this.toolTip.SetToolTip(this.buttonAddMagicWord, "Click to change the HotKey.");
			this.buttonAddMagicWord.UseVisualStyleBackColor = true;
			this.buttonAddMagicWord.Click += new System.EventHandler(this.buttonAddMagicWord_Click);
			// 
			// labelAddMagicWord
			// 
			this.labelAddMagicWord.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
			this.labelAddMagicWord.Location = new System.Drawing.Point(236, 45);
			this.labelAddMagicWord.Name = "labelAddMagicWord";
			this.labelAddMagicWord.Size = new System.Drawing.Size(153, 13);
			this.labelAddMagicWord.TabIndex = 12;
			this.labelAddMagicWord.Text = "labelAddMagicWord";
			this.toolTip.SetToolTip(this.labelAddMagicWord, "Press this key combination to popup the Magic Word entry box.");
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
			this.toolTip.SetToolTip(this.buttonActivate, "Click to change the HotKey.");
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
			this.toolTip.SetToolTip(this.labelActivate, "Press this key combination to popup the Magic Word entry box.");
			// 
			// tabPageGeneral
			// 
			this.tabPageGeneral.Controls.Add(this.label6);
			this.tabPageGeneral.Controls.Add(this.numericUpDownTimeout);
			this.tabPageGeneral.Controls.Add(this.label5);
			this.tabPageGeneral.Controls.Add(this.groupBox5);
			this.tabPageGeneral.Controls.Add(this.label3);
			this.tabPageGeneral.Controls.Add(this.numericUpDownIcons);
			this.tabPageGeneral.Controls.Add(this.checkBoxMru);
			this.tabPageGeneral.Controls.Add(this.checkBoxAutoStart);
			this.tabPageGeneral.Location = new System.Drawing.Point(4, 22);
			this.tabPageGeneral.Name = "tabPageGeneral";
			this.tabPageGeneral.Size = new System.Drawing.Size(546, 294);
			this.tabPageGeneral.TabIndex = 2;
			this.tabPageGeneral.Text = "General";
			this.tabPageGeneral.UseVisualStyleBackColor = true;
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(284, 194);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(47, 13);
			this.label6.TabIndex = 7;
			this.label6.Text = "seconds";
			// 
			// numericUpDownTimeout
			// 
			this.numericUpDownTimeout.Location = new System.Drawing.Point(214, 192);
			this.numericUpDownTimeout.Maximum = new decimal(new int[] {
            3600,
            0,
            0,
            0});
			this.numericUpDownTimeout.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.numericUpDownTimeout.Name = "numericUpDownTimeout";
			this.numericUpDownTimeout.Size = new System.Drawing.Size(64, 20);
			this.numericUpDownTimeout.TabIndex = 6;
			this.numericUpDownTimeout.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.numericUpDownTimeout.ValueChanged += new System.EventHandler(this.numericUpDownTimeout_ValueChanged);
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(4, 194);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(204, 13);
			this.label5.TabIndex = 5;
			this.label5.Text = "Timeout for application window to appear:";
			// 
			// groupBox5
			// 
			this.groupBox5.Controls.Add(this.radioButtonIconDetails);
			this.groupBox5.Controls.Add(this.radioButtonIconList);
			this.groupBox5.Controls.Add(this.radioButtonIconLargeIcon);
			this.groupBox5.Location = new System.Drawing.Point(3, 87);
			this.groupBox5.Name = "groupBox5";
			this.groupBox5.Size = new System.Drawing.Size(163, 100);
			this.groupBox5.TabIndex = 4;
			this.groupBox5.TabStop = false;
			this.groupBox5.Text = "Display icons in:";
			// 
			// radioButtonIconDetails
			// 
			this.radioButtonIconDetails.AutoSize = true;
			this.radioButtonIconDetails.Location = new System.Drawing.Point(11, 67);
			this.radioButtonIconDetails.Name = "radioButtonIconDetails";
			this.radioButtonIconDetails.Size = new System.Drawing.Size(41, 17);
			this.radioButtonIconDetails.TabIndex = 2;
			this.radioButtonIconDetails.TabStop = true;
			this.radioButtonIconDetails.Text = "List";
			this.radioButtonIconDetails.UseVisualStyleBackColor = true;
			this.radioButtonIconDetails.CheckedChanged += new System.EventHandler(this.radioButton_CheckedChanged);
			// 
			// radioButtonIconList
			// 
			this.radioButtonIconList.AutoSize = true;
			this.radioButtonIconList.Location = new System.Drawing.Point(11, 44);
			this.radioButtonIconList.Name = "radioButtonIconList";
			this.radioButtonIconList.Size = new System.Drawing.Size(103, 17);
			this.radioButtonIconList.TabIndex = 1;
			this.radioButtonIconList.TabStop = true;
			this.radioButtonIconList.Text = "Multi-column List";
			this.radioButtonIconList.UseVisualStyleBackColor = true;
			this.radioButtonIconList.CheckedChanged += new System.EventHandler(this.radioButton_CheckedChanged);
			// 
			// radioButtonIconLargeIcon
			// 
			this.radioButtonIconLargeIcon.AutoSize = true;
			this.radioButtonIconLargeIcon.Location = new System.Drawing.Point(11, 20);
			this.radioButtonIconLargeIcon.Name = "radioButtonIconLargeIcon";
			this.radioButtonIconLargeIcon.Size = new System.Drawing.Size(44, 17);
			this.radioButtonIconLargeIcon.TabIndex = 0;
			this.radioButtonIconLargeIcon.TabStop = true;
			this.radioButtonIconLargeIcon.Text = "Grid";
			this.radioButtonIconLargeIcon.UseVisualStyleBackColor = true;
			this.radioButtonIconLargeIcon.CheckedChanged += new System.EventHandler(this.radioButton_CheckedChanged);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(11, 63);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(159, 13);
			this.label3.TabIndex = 3;
			this.label3.Text = "Initial number of icons to display:";
			this.toolTip.SetToolTip(this.label3, "This is the number of icons displayed below the entry box is empty.\r\nThese will b" +
					"e your most used (or most recently used) applications.");
			// 
			// numericUpDownIcons
			// 
			this.numericUpDownIcons.Location = new System.Drawing.Point(196, 61);
			this.numericUpDownIcons.Name = "numericUpDownIcons";
			this.numericUpDownIcons.Size = new System.Drawing.Size(54, 20);
			this.numericUpDownIcons.TabIndex = 2;
			this.numericUpDownIcons.ValueChanged += new System.EventHandler(this.numericUpDownIcons_ValueChanged);
			// 
			// checkBoxMru
			// 
			this.checkBoxMru.AutoSize = true;
			this.checkBoxMru.Location = new System.Drawing.Point(14, 38);
			this.checkBoxMru.Name = "checkBoxMru";
			this.checkBoxMru.Size = new System.Drawing.Size(331, 17);
			this.checkBoxMru.TabIndex = 1;
			this.checkBoxMru.Text = "Use most recently used in auto completion (instead of most used)";
			this.toolTip.SetToolTip(this.checkBoxMru, "If checked, then the most recently used applications will be used rather than the" +
					" most used applications for auto completion.");
			this.checkBoxMru.UseVisualStyleBackColor = true;
			this.checkBoxMru.CheckedChanged += new System.EventHandler(this.checkBoxMru_CheckedChanged);
			// 
			// checkBoxAutoStart
			// 
			this.checkBoxAutoStart.AutoSize = true;
			this.checkBoxAutoStart.Location = new System.Drawing.Point(14, 14);
			this.checkBoxAutoStart.Name = "checkBoxAutoStart";
			this.checkBoxAutoStart.Size = new System.Drawing.Size(152, 17);
			this.checkBoxAutoStart.TabIndex = 0;
			this.checkBoxAutoStart.Text = "Start when Windows starts";
			this.toolTip.SetToolTip(this.checkBoxAutoStart, "Check to start DualLauncher when Windows starts.");
			this.checkBoxAutoStart.UseVisualStyleBackColor = true;
			this.checkBoxAutoStart.CheckedChanged += new System.EventHandler(this.checkBoxAutoStart_CheckedChanged);
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
			this.toolTip.SetToolTip(this.buttonImportQrs, "Merge in the Magic Words from a QRS file saved by SlickRun.");
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
			this.toolTip.SetToolTip(this.buttonImportXml, "Merge in the Magic Words from an XML file.");
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
			this.toolTip.SetToolTip(this.buttonExportXml, "Saves the current Magic Words to an XML file.\r\nUseful for backup purposes and for" +
					" moving Magic Words between machines.");
			this.buttonExportXml.UseVisualStyleBackColor = true;
			this.buttonExportXml.Click += new System.EventHandler(this.buttonExportXml_Click);
			// 
			// buttonDeleteAll
			// 
			this.buttonDeleteAll.Location = new System.Drawing.Point(26, 19);
			this.buttonDeleteAll.Name = "buttonDeleteAll";
			this.buttonDeleteAll.Size = new System.Drawing.Size(148, 23);
			this.buttonDeleteAll.TabIndex = 3;
			this.buttonDeleteAll.Text = "Delete all Magic Words...";
			this.buttonDeleteAll.UseVisualStyleBackColor = true;
			this.buttonDeleteAll.Click += new System.EventHandler(this.buttonDeleteAll_Click);
			// 
			// buttonClose
			// 
			this.buttonClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonClose.Location = new System.Drawing.Point(487, 340);
			this.buttonClose.Name = "buttonClose";
			this.buttonClose.Size = new System.Drawing.Size(75, 23);
			this.buttonClose.TabIndex = 1;
			this.buttonClose.Text = "Close";
			this.buttonClose.UseVisualStyleBackColor = true;
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
			this.Text = "Dual Launcher - Options";
			this.Load += new System.EventHandler(this.OptionsForm_Load);
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OptionsForm_FormClosing);
			this.tabControl1.ResumeLayout(false);
			this.tabPageMagicWords.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.magicWordsBindingSource)).EndInit();
			this.tabPageKeys.ResumeLayout(false);
			this.groupBox4.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			this.tabPageGeneral.ResumeLayout(false);
			this.tabPageGeneral.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownTimeout)).EndInit();
			this.groupBox5.ResumeLayout(false);
			this.groupBox5.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownIcons)).EndInit();
			this.tabPageImport.ResumeLayout(false);
			this.groupBox3.ResumeLayout(false);
			this.groupBox3.PerformLayout();
			this.groupBox2.ResumeLayout(false);
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
		private System.Windows.Forms.TabPage tabPageImport;
		private System.Windows.Forms.Button buttonImportQrs;
		private System.Windows.Forms.Button buttonExportXml;
		private System.Windows.Forms.Button buttonImportXml;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Button buttonDeleteAll;
		private System.Windows.Forms.Button buttonResetCounts;
		private System.Windows.Forms.CheckBox checkBoxMru;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.NumericUpDown numericUpDownIcons;
		private System.Windows.Forms.ToolTip toolTip;
		private System.Windows.Forms.GroupBox groupBox4;
		private System.Windows.Forms.Label labelPos4Prompt;
		private System.Windows.Forms.Button buttonPos4;
		private System.Windows.Forms.Label labelPos4;
		private System.Windows.Forms.Label labelPos3Prompt;
		private System.Windows.Forms.Button buttonPos3;
		private System.Windows.Forms.Label labelPos3;
		private System.Windows.Forms.Label labelPos2Prompt;
		private System.Windows.Forms.Button buttonPos2;
		private System.Windows.Forms.Label labelPos2;
		private System.Windows.Forms.Label labelPos1Prompt;
		private System.Windows.Forms.Button buttonPos1;
		private System.Windows.Forms.Label labelPos1;
		private System.Windows.Forms.GroupBox groupBox5;
		private System.Windows.Forms.RadioButton radioButtonIconLargeIcon;
		private System.Windows.Forms.RadioButton radioButtonIconDetails;
		private System.Windows.Forms.RadioButton radioButtonIconList;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Button buttonAddMagicWord;
		private System.Windows.Forms.Label labelAddMagicWord;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.NumericUpDown numericUpDownTimeout;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.DataGridViewTextBoxColumn aliasDataGridViewTextBoxColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn filenameDataGridViewTextBoxColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn Parameters;
		private System.Windows.Forms.DataGridViewTextBoxColumn Comment;
		private System.Windows.Forms.DataGridViewTextBoxColumn UseCount;
		private System.Windows.Forms.DataGridViewTextBoxColumn LastUsed;
	}
}