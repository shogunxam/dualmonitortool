namespace DualWallpaper
{
	partial class DualWallpaper
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DualWallpaper));
			this.picPreview = new System.Windows.Forms.PictureBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.picSource = new System.Windows.Forms.PictureBox();
			this.buttonAdd = new System.Windows.Forms.Button();
			this.comboBoxFit = new System.Windows.Forms.ComboBox();
			this.label3 = new System.Windows.Forms.Label();
			this.buttonBrowse = new System.Windows.Forms.Button();
			this.textBoxImage = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.labelScreensSelected = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.buttonSave = new System.Windows.Forms.Button();
			this.buttonSetWallpaper = new System.Windows.Forms.Button();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.buttonMoveLeft = new System.Windows.Forms.Button();
			this.buttonMoveDown = new System.Windows.Forms.Button();
			this.buttonMoveRight = new System.Windows.Forms.Button();
			this.buttonMoveUp = new System.Windows.Forms.Button();
			this.radioButtonMove100 = new System.Windows.Forms.RadioButton();
			this.radioButtonMove10 = new System.Windows.Forms.RadioButton();
			this.radioButtonMove1 = new System.Windows.Forms.RadioButton();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.buttonZoomOut = new System.Windows.Forms.Button();
			this.buttonZoomIn = new System.Windows.Forms.Button();
			this.radioButtonZoom20 = new System.Windows.Forms.RadioButton();
			this.radioButtonZoom5 = new System.Windows.Forms.RadioButton();
			this.radioButtonZoom1 = new System.Windows.Forms.RadioButton();
			((System.ComponentModel.ISupportInitialize)(this.picPreview)).BeginInit();
			this.groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.picSource)).BeginInit();
			this.groupBox2.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.SuspendLayout();
			// 
			// picPreview
			// 
			this.picPreview.Location = new System.Drawing.Point(9, 12);
			this.picPreview.Name = "picPreview";
			this.picPreview.Size = new System.Drawing.Size(768, 216);
			this.picPreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
			this.picPreview.TabIndex = 0;
			this.picPreview.TabStop = false;
			this.picPreview.MouseClick += new System.Windows.Forms.MouseEventHandler(this.picPreview_MouseClick);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.picSource);
			this.groupBox1.Controls.Add(this.buttonAdd);
			this.groupBox1.Controls.Add(this.comboBoxFit);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.buttonBrowse);
			this.groupBox1.Controls.Add(this.textBoxImage);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Location = new System.Drawing.Point(15, 273);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(540, 189);
			this.groupBox1.TabIndex = 1;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Add Image";
			// 
			// picSource
			// 
			this.picSource.BackColor = System.Drawing.Color.Black;
			this.picSource.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.picSource.Location = new System.Drawing.Point(173, 40);
			this.picSource.Name = "picSource";
			this.picSource.Size = new System.Drawing.Size(197, 110);
			this.picSource.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.picSource.TabIndex = 8;
			this.picSource.TabStop = false;
			// 
			// buttonAdd
			// 
			this.buttonAdd.Location = new System.Drawing.Point(458, 154);
			this.buttonAdd.Name = "buttonAdd";
			this.buttonAdd.Size = new System.Drawing.Size(75, 23);
			this.buttonAdd.TabIndex = 7;
			this.buttonAdd.Text = "Add Image";
			this.buttonAdd.UseVisualStyleBackColor = true;
			this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
			// 
			// comboBoxFit
			// 
			this.comboBoxFit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxFit.FormattingEnabled = true;
			this.comboBoxFit.Location = new System.Drawing.Point(51, 156);
			this.comboBoxFit.Name = "comboBoxFit";
			this.comboBoxFit.Size = new System.Drawing.Size(401, 21);
			this.comboBoxFit.TabIndex = 6;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(6, 159);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(21, 13);
			this.label3.TabIndex = 5;
			this.label3.Text = "Fit:";
			// 
			// buttonBrowse
			// 
			this.buttonBrowse.Location = new System.Drawing.Point(458, 12);
			this.buttonBrowse.Name = "buttonBrowse";
			this.buttonBrowse.Size = new System.Drawing.Size(75, 23);
			this.buttonBrowse.TabIndex = 4;
			this.buttonBrowse.Text = "Browse...";
			this.buttonBrowse.UseVisualStyleBackColor = true;
			this.buttonBrowse.Click += new System.EventHandler(this.buttonBrowse_Click);
			// 
			// textBoxImage
			// 
			this.textBoxImage.Location = new System.Drawing.Point(51, 14);
			this.textBoxImage.Name = "textBoxImage";
			this.textBoxImage.Size = new System.Drawing.Size(401, 20);
			this.textBoxImage.TabIndex = 3;
			this.textBoxImage.TextChanged += new System.EventHandler(this.textBoxImage_TextChanged);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(6, 16);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(39, 13);
			this.label2.TabIndex = 2;
			this.label2.Text = "Image:";
			// 
			// labelScreensSelected
			// 
			this.labelScreensSelected.Location = new System.Drawing.Point(110, 257);
			this.labelScreensSelected.Name = "labelScreensSelected";
			this.labelScreensSelected.Size = new System.Drawing.Size(295, 13);
			this.labelScreensSelected.TabIndex = 1;
			this.labelScreensSelected.Text = "labelScreensSelected";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 257);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(92, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Screens selected:";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(12, 231);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(445, 13);
			this.label4.TabIndex = 2;
			this.label4.Text = "Select 1 or more screens above (use ctrl+Left click to select multiple) and then " +
				"add an image.";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(12, 244);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(168, 13);
			this.label5.TabIndex = 3;
			this.label5.Text = "Repeat for any remaining screens.";
			// 
			// buttonSave
			// 
			this.buttonSave.Location = new System.Drawing.Point(275, 468);
			this.buttonSave.Name = "buttonSave";
			this.buttonSave.Size = new System.Drawing.Size(110, 23);
			this.buttonSave.TabIndex = 4;
			this.buttonSave.Text = "Save Image";
			this.buttonSave.UseVisualStyleBackColor = true;
			this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
			// 
			// buttonSetWallpaper
			// 
			this.buttonSetWallpaper.Location = new System.Drawing.Point(427, 468);
			this.buttonSetWallpaper.Name = "buttonSetWallpaper";
			this.buttonSetWallpaper.Size = new System.Drawing.Size(110, 23);
			this.buttonSetWallpaper.TabIndex = 5;
			this.buttonSetWallpaper.Text = "Set Wallpaper";
			this.buttonSetWallpaper.UseVisualStyleBackColor = true;
			this.buttonSetWallpaper.Click += new System.EventHandler(this.buttonSetWallpaper_Click);
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.buttonMoveLeft);
			this.groupBox2.Controls.Add(this.buttonMoveDown);
			this.groupBox2.Controls.Add(this.buttonMoveRight);
			this.groupBox2.Controls.Add(this.buttonMoveUp);
			this.groupBox2.Controls.Add(this.radioButtonMove100);
			this.groupBox2.Controls.Add(this.radioButtonMove10);
			this.groupBox2.Controls.Add(this.radioButtonMove1);
			this.groupBox2.Location = new System.Drawing.Point(561, 234);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(216, 116);
			this.groupBox2.TabIndex = 6;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Move Image";
			// 
			// buttonMoveLeft
			// 
			this.buttonMoveLeft.Image = ((System.Drawing.Image)(resources.GetObject("buttonMoveLeft.Image")));
			this.buttonMoveLeft.Location = new System.Drawing.Point(98, 42);
			this.buttonMoveLeft.Name = "buttonMoveLeft";
			this.buttonMoveLeft.Size = new System.Drawing.Size(32, 32);
			this.buttonMoveLeft.TabIndex = 6;
			this.buttonMoveLeft.UseVisualStyleBackColor = true;
			this.buttonMoveLeft.Click += new System.EventHandler(this.buttonMoveLeft_Click);
			// 
			// buttonMoveDown
			// 
			this.buttonMoveDown.Image = ((System.Drawing.Image)(resources.GetObject("buttonMoveDown.Image")));
			this.buttonMoveDown.Location = new System.Drawing.Point(130, 74);
			this.buttonMoveDown.Name = "buttonMoveDown";
			this.buttonMoveDown.Size = new System.Drawing.Size(32, 32);
			this.buttonMoveDown.TabIndex = 5;
			this.buttonMoveDown.UseVisualStyleBackColor = true;
			this.buttonMoveDown.Click += new System.EventHandler(this.buttonMoveDown_Click);
			// 
			// buttonMoveRight
			// 
			this.buttonMoveRight.Image = ((System.Drawing.Image)(resources.GetObject("buttonMoveRight.Image")));
			this.buttonMoveRight.Location = new System.Drawing.Point(162, 42);
			this.buttonMoveRight.Name = "buttonMoveRight";
			this.buttonMoveRight.Size = new System.Drawing.Size(32, 32);
			this.buttonMoveRight.TabIndex = 4;
			this.buttonMoveRight.UseVisualStyleBackColor = true;
			this.buttonMoveRight.Click += new System.EventHandler(this.buttonMoveRight_Click);
			// 
			// buttonMoveUp
			// 
			this.buttonMoveUp.Image = ((System.Drawing.Image)(resources.GetObject("buttonMoveUp.Image")));
			this.buttonMoveUp.Location = new System.Drawing.Point(130, 10);
			this.buttonMoveUp.Name = "buttonMoveUp";
			this.buttonMoveUp.Size = new System.Drawing.Size(32, 32);
			this.buttonMoveUp.TabIndex = 3;
			this.buttonMoveUp.UseVisualStyleBackColor = true;
			this.buttonMoveUp.Click += new System.EventHandler(this.buttonMoveUp_Click);
			// 
			// radioButtonMove100
			// 
			this.radioButtonMove100.AutoSize = true;
			this.radioButtonMove100.Checked = true;
			this.radioButtonMove100.Location = new System.Drawing.Point(9, 65);
			this.radioButtonMove100.Name = "radioButtonMove100";
			this.radioButtonMove100.Size = new System.Drawing.Size(73, 17);
			this.radioButtonMove100.TabIndex = 2;
			this.radioButtonMove100.TabStop = true;
			this.radioButtonMove100.Text = "100 Pixels";
			this.radioButtonMove100.UseVisualStyleBackColor = true;
			this.radioButtonMove100.CheckedChanged += new System.EventHandler(this.moveImage_CheckedChanged);
			// 
			// radioButtonMove10
			// 
			this.radioButtonMove10.AutoSize = true;
			this.radioButtonMove10.Location = new System.Drawing.Point(9, 42);
			this.radioButtonMove10.Name = "radioButtonMove10";
			this.radioButtonMove10.Size = new System.Drawing.Size(67, 17);
			this.radioButtonMove10.TabIndex = 1;
			this.radioButtonMove10.Text = "10 Pixels";
			this.radioButtonMove10.UseVisualStyleBackColor = true;
			this.radioButtonMove10.CheckedChanged += new System.EventHandler(this.moveImage_CheckedChanged);
			// 
			// radioButtonMove1
			// 
			this.radioButtonMove1.AutoSize = true;
			this.radioButtonMove1.Location = new System.Drawing.Point(9, 19);
			this.radioButtonMove1.Name = "radioButtonMove1";
			this.radioButtonMove1.Size = new System.Drawing.Size(56, 17);
			this.radioButtonMove1.TabIndex = 0;
			this.radioButtonMove1.Text = "1 Pixel";
			this.radioButtonMove1.UseVisualStyleBackColor = true;
			this.radioButtonMove1.CheckedChanged += new System.EventHandler(this.moveImage_CheckedChanged);
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.buttonZoomOut);
			this.groupBox3.Controls.Add(this.buttonZoomIn);
			this.groupBox3.Controls.Add(this.radioButtonZoom20);
			this.groupBox3.Controls.Add(this.radioButtonZoom5);
			this.groupBox3.Controls.Add(this.radioButtonZoom1);
			this.groupBox3.Location = new System.Drawing.Point(561, 356);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(216, 106);
			this.groupBox3.TabIndex = 7;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Zoom Image";
			// 
			// buttonZoomOut
			// 
			this.buttonZoomOut.Image = ((System.Drawing.Image)(resources.GetObject("buttonZoomOut.Image")));
			this.buttonZoomOut.Location = new System.Drawing.Point(130, 49);
			this.buttonZoomOut.Name = "buttonZoomOut";
			this.buttonZoomOut.Size = new System.Drawing.Size(32, 32);
			this.buttonZoomOut.TabIndex = 4;
			this.buttonZoomOut.UseVisualStyleBackColor = true;
			this.buttonZoomOut.Click += new System.EventHandler(this.buttonZoomOut_Click);
			// 
			// buttonZoomIn
			// 
			this.buttonZoomIn.Image = ((System.Drawing.Image)(resources.GetObject("buttonZoomIn.Image")));
			this.buttonZoomIn.Location = new System.Drawing.Point(130, 10);
			this.buttonZoomIn.Name = "buttonZoomIn";
			this.buttonZoomIn.Size = new System.Drawing.Size(32, 32);
			this.buttonZoomIn.TabIndex = 3;
			this.buttonZoomIn.UseVisualStyleBackColor = true;
			this.buttonZoomIn.Click += new System.EventHandler(this.buttonZoomIn_Click);
			// 
			// radioButtonZoom20
			// 
			this.radioButtonZoom20.AutoSize = true;
			this.radioButtonZoom20.Checked = true;
			this.radioButtonZoom20.Location = new System.Drawing.Point(9, 64);
			this.radioButtonZoom20.Name = "radioButtonZoom20";
			this.radioButtonZoom20.Size = new System.Drawing.Size(45, 17);
			this.radioButtonZoom20.TabIndex = 2;
			this.radioButtonZoom20.TabStop = true;
			this.radioButtonZoom20.Text = "20%";
			this.radioButtonZoom20.UseVisualStyleBackColor = true;
			// 
			// radioButtonZoom5
			// 
			this.radioButtonZoom5.AutoSize = true;
			this.radioButtonZoom5.Location = new System.Drawing.Point(9, 41);
			this.radioButtonZoom5.Name = "radioButtonZoom5";
			this.radioButtonZoom5.Size = new System.Drawing.Size(39, 17);
			this.radioButtonZoom5.TabIndex = 1;
			this.radioButtonZoom5.TabStop = true;
			this.radioButtonZoom5.Text = "5%";
			this.radioButtonZoom5.UseVisualStyleBackColor = true;
			// 
			// radioButtonZoom1
			// 
			this.radioButtonZoom1.AutoSize = true;
			this.radioButtonZoom1.Location = new System.Drawing.Point(9, 18);
			this.radioButtonZoom1.Name = "radioButtonZoom1";
			this.radioButtonZoom1.Size = new System.Drawing.Size(39, 17);
			this.radioButtonZoom1.TabIndex = 0;
			this.radioButtonZoom1.TabStop = true;
			this.radioButtonZoom1.Text = "1%";
			this.radioButtonZoom1.UseVisualStyleBackColor = true;
			// 
			// DualWallpaper
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(789, 503);
			this.Controls.Add(this.groupBox3);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.buttonSetWallpaper);
			this.Controls.Add(this.buttonSave);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.picPreview);
			this.Controls.Add(this.labelScreensSelected);
			this.Controls.Add(this.label1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.Name = "DualWallpaper";
			this.Text = "Dual Wallpaper";
			this.Shown += new System.EventHandler(this.DualWallpaper_Shown);
			this.HelpRequested += new System.Windows.Forms.HelpEventHandler(this.DualWallpaper_HelpRequested);
			((System.ComponentModel.ISupportInitialize)(this.picPreview)).EndInit();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.picSource)).EndInit();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.groupBox3.ResumeLayout(false);
			this.groupBox3.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.PictureBox picPreview;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.TextBox textBoxImage;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label labelScreensSelected;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button buttonAdd;
		private System.Windows.Forms.ComboBox comboBoxFit;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Button buttonBrowse;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Button buttonSave;
		private System.Windows.Forms.Button buttonSetWallpaper;
		private System.Windows.Forms.PictureBox picSource;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.RadioButton radioButtonMove100;
		private System.Windows.Forms.RadioButton radioButtonMove10;
		private System.Windows.Forms.RadioButton radioButtonMove1;
		private System.Windows.Forms.Button buttonMoveUp;
		private System.Windows.Forms.Button buttonMoveLeft;
		private System.Windows.Forms.Button buttonMoveDown;
		private System.Windows.Forms.Button buttonMoveRight;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.Button buttonZoomOut;
		private System.Windows.Forms.Button buttonZoomIn;
		private System.Windows.Forms.RadioButton radioButtonZoom20;
		private System.Windows.Forms.RadioButton radioButtonZoom5;
		private System.Windows.Forms.RadioButton radioButtonZoom1;
	}
}

