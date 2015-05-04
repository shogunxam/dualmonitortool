namespace DMT.Modules.Launcher
{
	partial class LauncherImportOptionsPanel
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
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.label2 = new System.Windows.Forms.Label();
			this.buttonImportQrs = new System.Windows.Forms.Button();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.buttonImportXml = new System.Windows.Forms.Button();
			this.buttonExportXml = new System.Windows.Forms.Button();
			this.buttonDeleteAll = new System.Windows.Forms.Button();
			this.groupBox3.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.label2);
			this.groupBox3.Controls.Add(this.buttonImportQrs);
			this.groupBox3.Location = new System.Drawing.Point(12, 122);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(507, 139);
			this.groupBox3.TabIndex = 8;
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
			this.groupBox2.Location = new System.Drawing.Point(12, 51);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(507, 60);
			this.groupBox2.TabIndex = 7;
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
			this.buttonDeleteAll.Location = new System.Drawing.Point(25, 10);
			this.buttonDeleteAll.Name = "buttonDeleteAll";
			this.buttonDeleteAll.Size = new System.Drawing.Size(148, 23);
			this.buttonDeleteAll.TabIndex = 6;
			this.buttonDeleteAll.Text = "Delete all Magic Words...";
			this.buttonDeleteAll.UseVisualStyleBackColor = true;
			this.buttonDeleteAll.Click += new System.EventHandler(this.buttonDeleteAll_Click);
			// 
			// LauncherImportOptionsPanel
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.groupBox3);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.buttonDeleteAll);
			this.Name = "LauncherImportOptionsPanel";
			this.Size = new System.Drawing.Size(546, 294);
			this.groupBox3.ResumeLayout(false);
			this.groupBox3.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button buttonImportQrs;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Button buttonImportXml;
		private System.Windows.Forms.Button buttonExportXml;
		private System.Windows.Forms.Button buttonDeleteAll;
	}
}
