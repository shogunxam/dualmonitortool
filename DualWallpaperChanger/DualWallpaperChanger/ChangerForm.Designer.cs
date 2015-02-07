namespace DualMonitorTools.DualWallpaperChanger
{
	partial class ChangerForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChangerForm));
			this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
			this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.nextToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.pauseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.aboutDualWallpaperChangerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.visitDualWallpaperChangerWebsiteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.timer = new System.Windows.Forms.Timer(this.components);
			this.tabControl = new System.Windows.Forms.TabControl();
			this.tabPageGeneral = new System.Windows.Forms.TabPage();
			this.labelNextChange = new System.Windows.Forms.Label();
			this.buttonChangeWallpaper = new System.Windows.Forms.Button();
			this.pictureBoxBackgroundColour = new System.Windows.Forms.PictureBox();
			this.comboBoxMultiMonitor = new System.Windows.Forms.ComboBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.comboBoxFit = new System.Windows.Forms.ComboBox();
			this.label1 = new System.Windows.Forms.Label();
			this.checkBoxChangePeriodically = new System.Windows.Forms.CheckBox();
			this.labelPeriod3 = new System.Windows.Forms.Label();
			this.numericUpDownMinutes = new System.Windows.Forms.NumericUpDown();
			this.labelPeriod2 = new System.Windows.Forms.Label();
			this.numericUpDownHours = new System.Windows.Forms.NumericUpDown();
			this.labelPeriod1 = new System.Windows.Forms.Label();
			this.checkBoxChangeOnStart = new System.Windows.Forms.CheckBox();
			this.checkBoxAutoStart = new System.Windows.Forms.CheckBox();
			this.tabPageProviders = new System.Windows.Forms.TabPage();
			this.buttonDelete = new System.Windows.Forms.Button();
			this.buttonEdit = new System.Windows.Forms.Button();
			this.buttonAdd = new System.Windows.Forms.Button();
			this.dataGridView = new System.Windows.Forms.DataGridView();
			this.Weight = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ProviderImage = new System.Windows.Forms.DataGridViewImageColumn();
			this.ProviderName = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Description = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.tabPageErrors = new System.Windows.Forms.TabPage();
			this.buttonCopyErrors = new System.Windows.Forms.Button();
			this.buttonClearErrors = new System.Windows.Forms.Button();
			this.listBoxErrors = new System.Windows.Forms.ListBox();
			this.buttonClose = new System.Windows.Forms.Button();
			this.colorDialog = new System.Windows.Forms.ColorDialog();
			this.bindingSource = new System.Windows.Forms.BindingSource(this.components);
			this.contextMenuStrip.SuspendLayout();
			this.tabControl.SuspendLayout();
			this.tabPageGeneral.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxBackgroundColour)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownMinutes)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownHours)).BeginInit();
			this.tabPageProviders.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
			this.tabPageErrors.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.bindingSource)).BeginInit();
			this.SuspendLayout();
			// 
			// notifyIcon
			// 
			this.notifyIcon.ContextMenuStrip = this.contextMenuStrip;
			this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
			this.notifyIcon.Text = "Dual Wallpaper Switcher";
			this.notifyIcon.Visible = true;
			this.notifyIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon_MouseDoubleClick);
			// 
			// contextMenuStrip
			// 
			this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.optionsToolStripMenuItem,
            this.nextToolStripMenuItem,
            this.pauseToolStripMenuItem,
            this.toolStripSeparator2,
            this.aboutDualWallpaperChangerToolStripMenuItem,
            this.visitDualWallpaperChangerWebsiteToolStripMenuItem,
            this.toolStripSeparator1,
            this.exitToolStripMenuItem});
			this.contextMenuStrip.Name = "contextMenuStrip";
			this.contextMenuStrip.Size = new System.Drawing.Size(273, 148);
			// 
			// optionsToolStripMenuItem
			// 
			this.optionsToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
			this.optionsToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("optionsToolStripMenuItem.Image")));
			this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
			this.optionsToolStripMenuItem.Size = new System.Drawing.Size(272, 22);
			this.optionsToolStripMenuItem.Text = "Options";
			this.optionsToolStripMenuItem.Click += new System.EventHandler(this.optionsToolStripMenuItem_Click);
			// 
			// nextToolStripMenuItem
			// 
			this.nextToolStripMenuItem.Name = "nextToolStripMenuItem";
			this.nextToolStripMenuItem.Size = new System.Drawing.Size(272, 22);
			this.nextToolStripMenuItem.Text = "Change Wallpaper Now";
			this.nextToolStripMenuItem.Click += new System.EventHandler(this.nextToolStripMenuItem_Click);
			// 
			// pauseToolStripMenuItem
			// 
			this.pauseToolStripMenuItem.Name = "pauseToolStripMenuItem";
			this.pauseToolStripMenuItem.Size = new System.Drawing.Size(272, 22);
			this.pauseToolStripMenuItem.Text = "Pause Wallpaper Changing";
			this.pauseToolStripMenuItem.Click += new System.EventHandler(this.pauseToolStripMenuItem_Click);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(269, 6);
			// 
			// aboutDualWallpaperChangerToolStripMenuItem
			// 
			this.aboutDualWallpaperChangerToolStripMenuItem.Name = "aboutDualWallpaperChangerToolStripMenuItem";
			this.aboutDualWallpaperChangerToolStripMenuItem.Size = new System.Drawing.Size(272, 22);
			this.aboutDualWallpaperChangerToolStripMenuItem.Text = "About Dual Wallpaper Changer";
			this.aboutDualWallpaperChangerToolStripMenuItem.Click += new System.EventHandler(this.aboutDualWallpaperChangerToolStripMenuItem_Click);
			// 
			// visitDualWallpaperChangerWebsiteToolStripMenuItem
			// 
			this.visitDualWallpaperChangerWebsiteToolStripMenuItem.Name = "visitDualWallpaperChangerWebsiteToolStripMenuItem";
			this.visitDualWallpaperChangerWebsiteToolStripMenuItem.Size = new System.Drawing.Size(272, 22);
			this.visitDualWallpaperChangerWebsiteToolStripMenuItem.Text = "Visit Dual Wallpaper Changer Website";
			this.visitDualWallpaperChangerWebsiteToolStripMenuItem.Click += new System.EventHandler(this.visitDualWallpaperChangerWebsiteToolStripMenuItem_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(269, 6);
			// 
			// exitToolStripMenuItem
			// 
			this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
			this.exitToolStripMenuItem.Size = new System.Drawing.Size(272, 22);
			this.exitToolStripMenuItem.Text = "Exit";
			this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
			// 
			// tabControl
			// 
			this.tabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tabControl.Controls.Add(this.tabPageGeneral);
			this.tabControl.Controls.Add(this.tabPageProviders);
			this.tabControl.Controls.Add(this.tabPageErrors);
			this.tabControl.Location = new System.Drawing.Point(12, 12);
			this.tabControl.Name = "tabControl";
			this.tabControl.SelectedIndex = 0;
			this.tabControl.Size = new System.Drawing.Size(576, 290);
			this.tabControl.TabIndex = 1;
			// 
			// tabPageGeneral
			// 
			this.tabPageGeneral.Controls.Add(this.labelNextChange);
			this.tabPageGeneral.Controls.Add(this.buttonChangeWallpaper);
			this.tabPageGeneral.Controls.Add(this.pictureBoxBackgroundColour);
			this.tabPageGeneral.Controls.Add(this.comboBoxMultiMonitor);
			this.tabPageGeneral.Controls.Add(this.label3);
			this.tabPageGeneral.Controls.Add(this.label2);
			this.tabPageGeneral.Controls.Add(this.comboBoxFit);
			this.tabPageGeneral.Controls.Add(this.label1);
			this.tabPageGeneral.Controls.Add(this.checkBoxChangePeriodically);
			this.tabPageGeneral.Controls.Add(this.labelPeriod3);
			this.tabPageGeneral.Controls.Add(this.numericUpDownMinutes);
			this.tabPageGeneral.Controls.Add(this.labelPeriod2);
			this.tabPageGeneral.Controls.Add(this.numericUpDownHours);
			this.tabPageGeneral.Controls.Add(this.labelPeriod1);
			this.tabPageGeneral.Controls.Add(this.checkBoxChangeOnStart);
			this.tabPageGeneral.Controls.Add(this.checkBoxAutoStart);
			this.tabPageGeneral.Location = new System.Drawing.Point(4, 22);
			this.tabPageGeneral.Name = "tabPageGeneral";
			this.tabPageGeneral.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageGeneral.Size = new System.Drawing.Size(568, 264);
			this.tabPageGeneral.TabIndex = 0;
			this.tabPageGeneral.Text = "General";
			this.tabPageGeneral.UseVisualStyleBackColor = true;
			// 
			// labelNextChange
			// 
			this.labelNextChange.Location = new System.Drawing.Point(164, 230);
			this.labelNextChange.Name = "labelNextChange";
			this.labelNextChange.Size = new System.Drawing.Size(398, 13);
			this.labelNextChange.TabIndex = 14;
			this.labelNextChange.Text = "Next change in";
			// 
			// buttonChangeWallpaper
			// 
			this.buttonChangeWallpaper.Location = new System.Drawing.Point(13, 225);
			this.buttonChangeWallpaper.Name = "buttonChangeWallpaper";
			this.buttonChangeWallpaper.Size = new System.Drawing.Size(145, 23);
			this.buttonChangeWallpaper.TabIndex = 13;
			this.buttonChangeWallpaper.Text = "Change Wallpaper Now";
			this.buttonChangeWallpaper.UseVisualStyleBackColor = true;
			this.buttonChangeWallpaper.Click += new System.EventHandler(this.buttonChangeWallpaper_Click);
			// 
			// pictureBoxBackgroundColour
			// 
			this.pictureBoxBackgroundColour.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pictureBoxBackgroundColour.Location = new System.Drawing.Point(117, 166);
			this.pictureBoxBackgroundColour.Name = "pictureBoxBackgroundColour";
			this.pictureBoxBackgroundColour.Size = new System.Drawing.Size(445, 21);
			this.pictureBoxBackgroundColour.TabIndex = 13;
			this.pictureBoxBackgroundColour.TabStop = false;
			this.pictureBoxBackgroundColour.Click += new System.EventHandler(this.pictureBoxBackgroundColour_Click);
			// 
			// comboBoxMultiMonitor
			// 
			this.comboBoxMultiMonitor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxMultiMonitor.FormattingEnabled = true;
			this.comboBoxMultiMonitor.Location = new System.Drawing.Point(117, 112);
			this.comboBoxMultiMonitor.Name = "comboBoxMultiMonitor";
			this.comboBoxMultiMonitor.Size = new System.Drawing.Size(445, 21);
			this.comboBoxMultiMonitor.TabIndex = 9;
			this.comboBoxMultiMonitor.SelectedIndexChanged += new System.EventHandler(this.comboBoxMultiMonitor_SelectedIndexChanged);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(10, 115);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(70, 13);
			this.label3.TabIndex = 8;
			this.label3.Text = "Multi Monitor:";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(10, 170);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(101, 13);
			this.label2.TabIndex = 12;
			this.label2.Text = "Background Colour:";
			// 
			// comboBoxFit
			// 
			this.comboBoxFit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxFit.FormattingEnabled = true;
			this.comboBoxFit.Location = new System.Drawing.Point(117, 139);
			this.comboBoxFit.Name = "comboBoxFit";
			this.comboBoxFit.Size = new System.Drawing.Size(445, 21);
			this.comboBoxFit.TabIndex = 11;
			this.comboBoxFit.SelectedIndexChanged += new System.EventHandler(this.comboBoxFit_SelectedIndexChanged);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(10, 142);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(21, 13);
			this.label1.TabIndex = 10;
			this.label1.Text = "Fit:";
			// 
			// checkBoxChangePeriodically
			// 
			this.checkBoxChangePeriodically.AutoSize = true;
			this.checkBoxChangePeriodically.Location = new System.Drawing.Point(6, 52);
			this.checkBoxChangePeriodically.Name = "checkBoxChangePeriodically";
			this.checkBoxChangePeriodically.Size = new System.Drawing.Size(166, 17);
			this.checkBoxChangePeriodically.TabIndex = 2;
			this.checkBoxChangePeriodically.Text = "Change wallpaper periodically";
			this.checkBoxChangePeriodically.UseVisualStyleBackColor = true;
			this.checkBoxChangePeriodically.CheckedChanged += new System.EventHandler(this.checkBoxChangePeriodically_CheckedChanged);
			// 
			// labelPeriod3
			// 
			this.labelPeriod3.AutoSize = true;
			this.labelPeriod3.Location = new System.Drawing.Point(313, 79);
			this.labelPeriod3.Name = "labelPeriod3";
			this.labelPeriod3.Size = new System.Drawing.Size(44, 13);
			this.labelPeriod3.TabIndex = 7;
			this.labelPeriod3.Text = "Minutes";
			// 
			// numericUpDownMinutes
			// 
			this.numericUpDownMinutes.Location = new System.Drawing.Point(246, 77);
			this.numericUpDownMinutes.Maximum = new decimal(new int[] {
            59,
            0,
            0,
            0});
			this.numericUpDownMinutes.Name = "numericUpDownMinutes";
			this.numericUpDownMinutes.Size = new System.Drawing.Size(61, 20);
			this.numericUpDownMinutes.TabIndex = 6;
			this.numericUpDownMinutes.ValueChanged += new System.EventHandler(this.numericUpDownMinutes_ValueChanged);
			// 
			// labelPeriod2
			// 
			this.labelPeriod2.AutoSize = true;
			this.labelPeriod2.Location = new System.Drawing.Point(191, 79);
			this.labelPeriod2.Name = "labelPeriod2";
			this.labelPeriod2.Size = new System.Drawing.Size(35, 13);
			this.labelPeriod2.TabIndex = 5;
			this.labelPeriod2.Text = "Hours";
			// 
			// numericUpDownHours
			// 
			this.numericUpDownHours.Location = new System.Drawing.Point(132, 77);
			this.numericUpDownHours.Maximum = new decimal(new int[] {
            24,
            0,
            0,
            0});
			this.numericUpDownHours.Name = "numericUpDownHours";
			this.numericUpDownHours.Size = new System.Drawing.Size(53, 20);
			this.numericUpDownHours.TabIndex = 4;
			this.numericUpDownHours.ValueChanged += new System.EventHandler(this.numericUpDownHours_ValueChanged);
			// 
			// labelPeriod1
			// 
			this.labelPeriod1.AutoSize = true;
			this.labelPeriod1.Location = new System.Drawing.Point(5, 79);
			this.labelPeriod1.Name = "labelPeriod1";
			this.labelPeriod1.Size = new System.Drawing.Size(121, 13);
			this.labelPeriod1.TabIndex = 3;
			this.labelPeriod1.Text = "Time between changes:";
			// 
			// checkBoxChangeOnStart
			// 
			this.checkBoxChangeOnStart.AutoSize = true;
			this.checkBoxChangeOnStart.Location = new System.Drawing.Point(6, 29);
			this.checkBoxChangeOnStart.Name = "checkBoxChangeOnStart";
			this.checkBoxChangeOnStart.Size = new System.Drawing.Size(161, 17);
			this.checkBoxChangeOnStart.TabIndex = 1;
			this.checkBoxChangeOnStart.Text = "Change wallpaper on startup";
			this.checkBoxChangeOnStart.UseVisualStyleBackColor = true;
			this.checkBoxChangeOnStart.CheckedChanged += new System.EventHandler(this.checkBoxChangeOnStart_CheckedChanged);
			// 
			// checkBoxAutoStart
			// 
			this.checkBoxAutoStart.AutoSize = true;
			this.checkBoxAutoStart.Location = new System.Drawing.Point(6, 6);
			this.checkBoxAutoStart.Name = "checkBoxAutoStart";
			this.checkBoxAutoStart.Size = new System.Drawing.Size(152, 17);
			this.checkBoxAutoStart.TabIndex = 0;
			this.checkBoxAutoStart.Text = "Start when Windows starts";
			this.checkBoxAutoStart.UseVisualStyleBackColor = true;
			this.checkBoxAutoStart.CheckedChanged += new System.EventHandler(this.checkBoxAutoStart_CheckedChanged);
			// 
			// tabPageProviders
			// 
			this.tabPageProviders.Controls.Add(this.buttonDelete);
			this.tabPageProviders.Controls.Add(this.buttonEdit);
			this.tabPageProviders.Controls.Add(this.buttonAdd);
			this.tabPageProviders.Controls.Add(this.dataGridView);
			this.tabPageProviders.Location = new System.Drawing.Point(4, 22);
			this.tabPageProviders.Name = "tabPageProviders";
			this.tabPageProviders.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageProviders.Size = new System.Drawing.Size(568, 264);
			this.tabPageProviders.TabIndex = 1;
			this.tabPageProviders.Text = "Providers";
			this.tabPageProviders.UseVisualStyleBackColor = true;
			// 
			// buttonDelete
			// 
			this.buttonDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.buttonDelete.Location = new System.Drawing.Point(168, 235);
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
			this.buttonEdit.Location = new System.Drawing.Point(87, 235);
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
			this.buttonAdd.Location = new System.Drawing.Point(6, 235);
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
            this.Weight,
            this.ProviderImage,
            this.ProviderName,
            this.Description});
			this.dataGridView.Location = new System.Drawing.Point(0, 3);
			this.dataGridView.MultiSelect = false;
			this.dataGridView.Name = "dataGridView";
			this.dataGridView.ReadOnly = true;
			this.dataGridView.RowHeadersVisible = false;
			this.dataGridView.RowTemplate.Height = 56;
			this.dataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dataGridView.Size = new System.Drawing.Size(565, 226);
			this.dataGridView.TabIndex = 0;
			this.dataGridView.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_CellDoubleClick);
			this.dataGridView.SelectionChanged += new System.EventHandler(this.dataGridView_SelectionChanged);
			// 
			// Weight
			// 
			this.Weight.DataPropertyName = "Weight";
			this.Weight.HeaderText = "Weight";
			this.Weight.Name = "Weight";
			this.Weight.ReadOnly = true;
			this.Weight.Width = 66;
			// 
			// ProviderImage
			// 
			this.ProviderImage.DataPropertyName = "ProviderImage";
			this.ProviderImage.HeaderText = "Provider";
			this.ProviderImage.Name = "ProviderImage";
			this.ProviderImage.ReadOnly = true;
			this.ProviderImage.Width = 52;
			// 
			// ProviderName
			// 
			this.ProviderName.DataPropertyName = "ProviderName";
			this.ProviderName.HeaderText = "Name";
			this.ProviderName.Name = "ProviderName";
			this.ProviderName.ReadOnly = true;
			this.ProviderName.Width = 60;
			// 
			// Description
			// 
			this.Description.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.Description.DataPropertyName = "Description";
			this.Description.HeaderText = "Description";
			this.Description.Name = "Description";
			this.Description.ReadOnly = true;
			// 
			// tabPageErrors
			// 
			this.tabPageErrors.Controls.Add(this.buttonCopyErrors);
			this.tabPageErrors.Controls.Add(this.buttonClearErrors);
			this.tabPageErrors.Controls.Add(this.listBoxErrors);
			this.tabPageErrors.Location = new System.Drawing.Point(4, 22);
			this.tabPageErrors.Name = "tabPageErrors";
			this.tabPageErrors.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageErrors.Size = new System.Drawing.Size(568, 264);
			this.tabPageErrors.TabIndex = 2;
			this.tabPageErrors.Text = "Errors";
			this.tabPageErrors.UseVisualStyleBackColor = true;
			// 
			// buttonCopyErrors
			// 
			this.buttonCopyErrors.Location = new System.Drawing.Point(6, 235);
			this.buttonCopyErrors.Name = "buttonCopyErrors";
			this.buttonCopyErrors.Size = new System.Drawing.Size(148, 23);
			this.buttonCopyErrors.TabIndex = 3;
			this.buttonCopyErrors.Text = "Copy Errors to Clipboard";
			this.buttonCopyErrors.UseVisualStyleBackColor = true;
			this.buttonCopyErrors.Click += new System.EventHandler(this.buttonCopyErrors_Click);
			// 
			// buttonClearErrors
			// 
			this.buttonClearErrors.Location = new System.Drawing.Point(160, 235);
			this.buttonClearErrors.Name = "buttonClearErrors";
			this.buttonClearErrors.Size = new System.Drawing.Size(75, 23);
			this.buttonClearErrors.TabIndex = 2;
			this.buttonClearErrors.Text = "Clear Errors";
			this.buttonClearErrors.UseVisualStyleBackColor = true;
			this.buttonClearErrors.Click += new System.EventHandler(this.buttonClearErrors_Click);
			// 
			// listBoxErrors
			// 
			this.listBoxErrors.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.listBoxErrors.FormattingEnabled = true;
			this.listBoxErrors.Location = new System.Drawing.Point(6, 6);
			this.listBoxErrors.Name = "listBoxErrors";
			this.listBoxErrors.Size = new System.Drawing.Size(556, 212);
			this.listBoxErrors.TabIndex = 0;
			// 
			// buttonClose
			// 
			this.buttonClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.buttonClose.Location = new System.Drawing.Point(262, 331);
			this.buttonClose.Name = "buttonClose";
			this.buttonClose.Size = new System.Drawing.Size(75, 23);
			this.buttonClose.TabIndex = 2;
			this.buttonClose.Text = "Close";
			this.buttonClose.UseVisualStyleBackColor = true;
			this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
			// 
			// ChangerForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(600, 366);
			this.Controls.Add(this.buttonClose);
			this.Controls.Add(this.tabControl);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ChangerForm";
			this.Text = "Dual Wallpaper Changer";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SwitcherForm_FormClosing);
			this.contextMenuStrip.ResumeLayout(false);
			this.tabControl.ResumeLayout(false);
			this.tabPageGeneral.ResumeLayout(false);
			this.tabPageGeneral.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxBackgroundColour)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownMinutes)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownHours)).EndInit();
			this.tabPageProviders.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
			this.tabPageErrors.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.bindingSource)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.NotifyIcon notifyIcon;
		private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
		private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
		private System.Windows.Forms.Timer timer;
		private System.Windows.Forms.ToolStripMenuItem nextToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem pauseToolStripMenuItem;
		private System.Windows.Forms.TabControl tabControl;
		private System.Windows.Forms.TabPage tabPageGeneral;
		private System.Windows.Forms.CheckBox checkBoxAutoStart;
		private System.Windows.Forms.TabPage tabPageProviders;
		private System.Windows.Forms.Button buttonClose;
		private System.Windows.Forms.CheckBox checkBoxChangeOnStart;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ComboBox comboBoxFit;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.CheckBox checkBoxChangePeriodically;
		private System.Windows.Forms.Label labelPeriod3;
		private System.Windows.Forms.NumericUpDown numericUpDownMinutes;
		private System.Windows.Forms.Label labelPeriod2;
		private System.Windows.Forms.NumericUpDown numericUpDownHours;
		private System.Windows.Forms.Label labelPeriod1;
		private System.Windows.Forms.ComboBox comboBoxMultiMonitor;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.PictureBox pictureBoxBackgroundColour;
		private System.Windows.Forms.ColorDialog colorDialog;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.Button buttonDelete;
		private System.Windows.Forms.Button buttonEdit;
		private System.Windows.Forms.Button buttonAdd;
		private System.Windows.Forms.DataGridView dataGridView;
		private System.Windows.Forms.BindingSource bindingSource;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripMenuItem aboutDualWallpaperChangerToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem visitDualWallpaperChangerWebsiteToolStripMenuItem;
		private System.Windows.Forms.Button buttonChangeWallpaper;
		private System.Windows.Forms.Label labelNextChange;
		private System.Windows.Forms.DataGridViewTextBoxColumn Weight;
		private System.Windows.Forms.DataGridViewImageColumn ProviderImage;
		private System.Windows.Forms.DataGridViewTextBoxColumn ProviderName;
		private System.Windows.Forms.DataGridViewTextBoxColumn Description;
		private System.Windows.Forms.TabPage tabPageErrors;
		private System.Windows.Forms.ListBox listBoxErrors;
		private System.Windows.Forms.Button buttonClearErrors;
		private System.Windows.Forms.Button buttonCopyErrors;
	}
}

