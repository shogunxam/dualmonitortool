namespace DualLauncher
{
	partial class MagicWordForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MagicWordForm));
			this.label1 = new System.Windows.Forms.Label();
			this.textBoxAlias = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.textBoxFilename = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.textBoxStartDirectory = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.textBoxParameters = new System.Windows.Forms.TextBox();
			this.buttonBrowse = new System.Windows.Forms.Button();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.textBoxCaption = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.textBoxWindowClass = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.textBoxComment = new System.Windows.Forms.TextBox();
			this.buttonOK = new System.Windows.Forms.Button();
			this.buttonCancel = new System.Windows.Forms.Button();
			this.tabControl = new System.Windows.Forms.TabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.tabPage3 = new System.Windows.Forms.TabPage();
			this.tabPage4 = new System.Windows.Forms.TabPage();
			this.buttonDirBrowse = new System.Windows.Forms.Button();
			this.pictureBoxIcon = new System.Windows.Forms.PictureBox();
			this.buttonTest = new System.Windows.Forms.Button();
			this.windowPicker = new DualLauncher.WindowPicker();
			this.startupPositionControl1 = new DualLauncher.StartupPositionControl();
			this.startupPositionControl2 = new DualLauncher.StartupPositionControl();
			this.startupPositionControl3 = new DualLauncher.StartupPositionControl();
			this.startupPositionControl4 = new DualLauncher.StartupPositionControl();
			this.toolTip = new System.Windows.Forms.ToolTip(this.components);
			this.groupBox1.SuspendLayout();
			this.tabControl.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.tabPage2.SuspendLayout();
			this.tabPage3.SuspendLayout();
			this.tabPage4.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxIcon)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.windowPicker)).BeginInit();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 18);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(68, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Magic Word:";
			// 
			// textBoxAlias
			// 
			this.textBoxAlias.Location = new System.Drawing.Point(108, 15);
			this.textBoxAlias.Name = "textBoxAlias";
			this.textBoxAlias.Size = new System.Drawing.Size(207, 20);
			this.textBoxAlias.TabIndex = 1;
			this.toolTip.SetToolTip(this.textBoxAlias, "This is the Word you type (or start typing) to run this program.");
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(12, 44);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(52, 13);
			this.label2.TabIndex = 2;
			this.label2.Text = "Filename:";
			// 
			// textBoxFilename
			// 
			this.textBoxFilename.Location = new System.Drawing.Point(108, 41);
			this.textBoxFilename.Name = "textBoxFilename";
			this.textBoxFilename.Size = new System.Drawing.Size(510, 20);
			this.textBoxFilename.TabIndex = 3;
			this.toolTip.SetToolTip(this.textBoxFilename, resources.GetString("textBoxFilename.ToolTip"));
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(12, 70);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(43, 13);
			this.label3.TabIndex = 4;
			this.label3.Text = "Start in:";
			// 
			// textBoxStartDirectory
			// 
			this.textBoxStartDirectory.Location = new System.Drawing.Point(108, 67);
			this.textBoxStartDirectory.Name = "textBoxStartDirectory";
			this.textBoxStartDirectory.Size = new System.Drawing.Size(510, 20);
			this.textBoxStartDirectory.TabIndex = 5;
			this.toolTip.SetToolTip(this.textBoxStartDirectory, "This is the working directory you wnat the application started in.\r\nThis will nor" +
					"mally be left blank.");
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(12, 96);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(63, 13);
			this.label4.TabIndex = 6;
			this.label4.Text = "Parameters:";
			// 
			// textBoxParameters
			// 
			this.textBoxParameters.Location = new System.Drawing.Point(108, 93);
			this.textBoxParameters.Name = "textBoxParameters";
			this.textBoxParameters.Size = new System.Drawing.Size(510, 20);
			this.textBoxParameters.TabIndex = 7;
			this.toolTip.SetToolTip(this.textBoxParameters, "Any parameters that need to be passed to the application.\r\nThis will normally be " +
					"left blank.");
			// 
			// buttonBrowse
			// 
			this.buttonBrowse.Location = new System.Drawing.Point(624, 39);
			this.buttonBrowse.Name = "buttonBrowse";
			this.buttonBrowse.Size = new System.Drawing.Size(75, 23);
			this.buttonBrowse.TabIndex = 8;
			this.buttonBrowse.Text = "Browse...";
			this.toolTip.SetToolTip(this.buttonBrowse, "Browse for the application or document.");
			this.buttonBrowse.UseVisualStyleBackColor = true;
			this.buttonBrowse.Click += new System.EventHandler(this.buttonBrowse_Click);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.textBoxCaption);
			this.groupBox1.Controls.Add(this.label6);
			this.groupBox1.Controls.Add(this.textBoxWindowClass);
			this.groupBox1.Controls.Add(this.label5);
			this.groupBox1.Location = new System.Drawing.Point(15, 145);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(684, 81);
			this.groupBox1.TabIndex = 9;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Application Window Identification";
			this.toolTip.SetToolTip(this.groupBox1, resources.GetString("groupBox1.ToolTip"));
			// 
			// textBoxCaption
			// 
			this.textBoxCaption.Location = new System.Drawing.Point(93, 45);
			this.textBoxCaption.Name = "textBoxCaption";
			this.textBoxCaption.Size = new System.Drawing.Size(510, 20);
			this.textBoxCaption.TabIndex = 3;
			this.toolTip.SetToolTip(this.textBoxCaption, "Regular expression to help identify the correct window.\r\nNormally you can get awa" +
					"y with just a substring from the windows caption.");
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(6, 48);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(46, 13);
			this.label6.TabIndex = 2;
			this.label6.Text = "Caption:";
			// 
			// textBoxWindowClass
			// 
			this.textBoxWindowClass.Location = new System.Drawing.Point(93, 19);
			this.textBoxWindowClass.Name = "textBoxWindowClass";
			this.textBoxWindowClass.Size = new System.Drawing.Size(510, 20);
			this.textBoxWindowClass.TabIndex = 1;
			this.toolTip.SetToolTip(this.textBoxWindowClass, "This is the name of the window class that the above program uses.\r\nMay be left bl" +
					"ank, but will be filled in if you use the crosshairs.");
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(6, 22);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(77, 13);
			this.label5.TabIndex = 0;
			this.label5.Text = "Window Class:";
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(12, 122);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(54, 13);
			this.label7.TabIndex = 10;
			this.label7.Text = "Comment:";
			// 
			// textBoxComment
			// 
			this.textBoxComment.Location = new System.Drawing.Point(108, 119);
			this.textBoxComment.Name = "textBoxComment";
			this.textBoxComment.Size = new System.Drawing.Size(510, 20);
			this.textBoxComment.TabIndex = 11;
			this.toolTip.SetToolTip(this.textBoxComment, "You can optionally leave a comment here if you wish.");
			// 
			// buttonOK
			// 
			this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.buttonOK.Location = new System.Drawing.Point(235, 357);
			this.buttonOK.Name = "buttonOK";
			this.buttonOK.Size = new System.Drawing.Size(75, 23);
			this.buttonOK.TabIndex = 12;
			this.buttonOK.Text = "OK";
			this.buttonOK.UseVisualStyleBackColor = true;
			this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
			// 
			// buttonCancel
			// 
			this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonCancel.Location = new System.Drawing.Point(392, 357);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new System.Drawing.Size(75, 23);
			this.buttonCancel.TabIndex = 13;
			this.buttonCancel.Text = "Cancel";
			this.buttonCancel.UseVisualStyleBackColor = true;
			// 
			// tabControl
			// 
			this.tabControl.Controls.Add(this.tabPage1);
			this.tabControl.Controls.Add(this.tabPage2);
			this.tabControl.Controls.Add(this.tabPage3);
			this.tabControl.Controls.Add(this.tabPage4);
			this.tabControl.Location = new System.Drawing.Point(15, 232);
			this.tabControl.Name = "tabControl";
			this.tabControl.SelectedIndex = 0;
			this.tabControl.Size = new System.Drawing.Size(684, 119);
			this.tabControl.TabIndex = 14;
			// 
			// tabPage1
			// 
			this.tabPage1.Controls.Add(this.startupPositionControl1);
			this.tabPage1.Location = new System.Drawing.Point(4, 22);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage1.Size = new System.Drawing.Size(676, 93);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "Start Position 1";
			this.tabPage1.UseVisualStyleBackColor = true;
			// 
			// tabPage2
			// 
			this.tabPage2.Controls.Add(this.startupPositionControl2);
			this.tabPage2.Location = new System.Drawing.Point(4, 22);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage2.Size = new System.Drawing.Size(676, 93);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "Start Position 2";
			this.tabPage2.UseVisualStyleBackColor = true;
			// 
			// tabPage3
			// 
			this.tabPage3.Controls.Add(this.startupPositionControl3);
			this.tabPage3.Location = new System.Drawing.Point(4, 22);
			this.tabPage3.Name = "tabPage3";
			this.tabPage3.Size = new System.Drawing.Size(676, 93);
			this.tabPage3.TabIndex = 2;
			this.tabPage3.Text = "Start Position3";
			this.tabPage3.UseVisualStyleBackColor = true;
			// 
			// tabPage4
			// 
			this.tabPage4.Controls.Add(this.startupPositionControl4);
			this.tabPage4.Location = new System.Drawing.Point(4, 22);
			this.tabPage4.Name = "tabPage4";
			this.tabPage4.Size = new System.Drawing.Size(676, 93);
			this.tabPage4.TabIndex = 3;
			this.tabPage4.Text = "Start Position 4";
			this.tabPage4.UseVisualStyleBackColor = true;
			// 
			// buttonDirBrowse
			// 
			this.buttonDirBrowse.Location = new System.Drawing.Point(624, 65);
			this.buttonDirBrowse.Name = "buttonDirBrowse";
			this.buttonDirBrowse.Size = new System.Drawing.Size(75, 23);
			this.buttonDirBrowse.TabIndex = 15;
			this.buttonDirBrowse.Text = "Browse...";
			this.toolTip.SetToolTip(this.buttonDirBrowse, "Brose for the starting directory.");
			this.buttonDirBrowse.UseVisualStyleBackColor = true;
			this.buttonDirBrowse.Click += new System.EventHandler(this.buttonDirBrowse_Click);
			// 
			// pictureBoxIcon
			// 
			this.pictureBoxIcon.Location = new System.Drawing.Point(328, 3);
			this.pictureBoxIcon.Name = "pictureBoxIcon";
			this.pictureBoxIcon.Size = new System.Drawing.Size(32, 32);
			this.pictureBoxIcon.TabIndex = 18;
			this.pictureBoxIcon.TabStop = false;
			// 
			// buttonTest
			// 
			this.buttonTest.Location = new System.Drawing.Point(379, 8);
			this.buttonTest.Name = "buttonTest";
			this.buttonTest.Size = new System.Drawing.Size(128, 23);
			this.buttonTest.TabIndex = 19;
			this.buttonTest.Text = "Test the Magic Word";
			this.buttonTest.UseVisualStyleBackColor = true;
			this.buttonTest.Click += new System.EventHandler(this.buttonTest_Click);
			// 
			// windowPicker
			// 
			this.windowPicker.Location = new System.Drawing.Point(637, 96);
			this.windowPicker.Name = "windowPicker";
			this.windowPicker.Size = new System.Drawing.Size(48, 48);
			this.windowPicker.TabIndex = 17;
			this.windowPicker.TabStop = false;
			this.toolTip.SetToolTip(this.windowPicker, "Drag the crosshairs over an existing running application to quickly fill in this " +
					"form.");
			// 
			// startupPositionControl1
			// 
			this.startupPositionControl1.Location = new System.Drawing.Point(6, 3);
			this.startupPositionControl1.Name = "startupPositionControl1";
			this.startupPositionControl1.Size = new System.Drawing.Size(667, 87);
			this.startupPositionControl1.TabIndex = 0;
			// 
			// startupPositionControl2
			// 
			this.startupPositionControl2.Location = new System.Drawing.Point(6, 3);
			this.startupPositionControl2.Name = "startupPositionControl2";
			this.startupPositionControl2.Size = new System.Drawing.Size(667, 87);
			this.startupPositionControl2.TabIndex = 0;
			// 
			// startupPositionControl3
			// 
			this.startupPositionControl3.Location = new System.Drawing.Point(6, 3);
			this.startupPositionControl3.Name = "startupPositionControl3";
			this.startupPositionControl3.Size = new System.Drawing.Size(667, 87);
			this.startupPositionControl3.TabIndex = 0;
			// 
			// startupPositionControl4
			// 
			this.startupPositionControl4.Location = new System.Drawing.Point(6, 3);
			this.startupPositionControl4.Name = "startupPositionControl4";
			this.startupPositionControl4.Size = new System.Drawing.Size(667, 87);
			this.startupPositionControl4.TabIndex = 0;
			// 
			// MagicWordForm
			// 
			this.AcceptButton = this.buttonOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.buttonCancel;
			this.ClientSize = new System.Drawing.Size(711, 387);
			this.Controls.Add(this.buttonTest);
			this.Controls.Add(this.pictureBoxIcon);
			this.Controls.Add(this.windowPicker);
			this.Controls.Add(this.buttonDirBrowse);
			this.Controls.Add(this.tabControl);
			this.Controls.Add(this.buttonCancel);
			this.Controls.Add(this.buttonOK);
			this.Controls.Add(this.textBoxComment);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.buttonBrowse);
			this.Controls.Add(this.textBoxParameters);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.textBoxStartDirectory);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.textBoxFilename);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.textBoxAlias);
			this.Controls.Add(this.label1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "MagicWordForm";
			this.ShowInTaskbar = false;
			this.Text = "MagicWordForm";
			this.Load += new System.EventHandler(this.MagicWordForm_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.tabControl.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.tabPage2.ResumeLayout(false);
			this.tabPage3.ResumeLayout(false);
			this.tabPage4.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxIcon)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.windowPicker)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox textBoxAlias;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox textBoxFilename;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox textBoxStartDirectory;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox textBoxParameters;
		private System.Windows.Forms.Button buttonBrowse;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox textBoxCaption;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox textBoxWindowClass;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.TextBox textBoxComment;
		private System.Windows.Forms.Button buttonOK;
		private System.Windows.Forms.Button buttonCancel;
		private System.Windows.Forms.TabControl tabControl;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.TabPage tabPage2;
		private System.Windows.Forms.TabPage tabPage3;
		private System.Windows.Forms.TabPage tabPage4;
		private System.Windows.Forms.Button buttonDirBrowse;
		private DualLauncher.StartupPositionControl startupPositionControl1;
		private DualLauncher.StartupPositionControl startupPositionControl2;
		private DualLauncher.StartupPositionControl startupPositionControl3;
		private DualLauncher.StartupPositionControl startupPositionControl4;
		private WindowPicker windowPicker;
		private System.Windows.Forms.PictureBox pictureBoxIcon;
		private System.Windows.Forms.Button buttonTest;
		private System.Windows.Forms.ToolTip toolTip;
	}
}