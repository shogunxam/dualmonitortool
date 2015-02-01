﻿namespace DualMonitorTools.DualWallpaperChanger.RandomShapes
{
	partial class RandomShapesForm
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
			this.checkBoxUseGradients = new System.Windows.Forms.CheckBox();
			this.textBoxDescription = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.numericUpDownWeight = new System.Windows.Forms.NumericUpDown();
			this.label1 = new System.Windows.Forms.Label();
			this.buttonCancel = new System.Windows.Forms.Button();
			this.buttonOK = new System.Windows.Forms.Button();
			this.label3 = new System.Windows.Forms.Label();
			this.numericUpDownRectangles = new System.Windows.Forms.NumericUpDown();
			this.checkBoxUseAlpha = new System.Windows.Forms.CheckBox();
			this.checkBoxRectangles = new System.Windows.Forms.CheckBox();
			this.checkBoxEllipses = new System.Windows.Forms.CheckBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.checkBoxRandomBackground = new System.Windows.Forms.CheckBox();
			this.label4 = new System.Windows.Forms.Label();
			this.buttonAbout = new System.Windows.Forms.Button();
			this.pictureBox = new System.Windows.Forms.PictureBox();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownWeight)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownRectangles)).BeginInit();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
			this.SuspendLayout();
			// 
			// checkBoxUseGradients
			// 
			this.checkBoxUseGradients.AutoSize = true;
			this.checkBoxUseGradients.Location = new System.Drawing.Point(15, 28);
			this.checkBoxUseGradients.Name = "checkBoxUseGradients";
			this.checkBoxUseGradients.Size = new System.Drawing.Size(143, 17);
			this.checkBoxUseGradients.TabIndex = 16;
			this.checkBoxUseGradients.Text = "Fill shapes with gradients";
			this.checkBoxUseGradients.UseVisualStyleBackColor = true;
			// 
			// textBoxDescription
			// 
			this.textBoxDescription.Location = new System.Drawing.Point(109, 102);
			this.textBoxDescription.Name = "textBoxDescription";
			this.textBoxDescription.Size = new System.Drawing.Size(470, 20);
			this.textBoxDescription.TabIndex = 12;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(8, 105);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(63, 13);
			this.label2.TabIndex = 11;
			this.label2.Text = "Description:";
			// 
			// numericUpDownWeight
			// 
			this.numericUpDownWeight.Location = new System.Drawing.Point(109, 76);
			this.numericUpDownWeight.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
			this.numericUpDownWeight.Name = "numericUpDownWeight";
			this.numericUpDownWeight.Size = new System.Drawing.Size(120, 20);
			this.numericUpDownWeight.TabIndex = 10;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(8, 78);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(44, 13);
			this.label1.TabIndex = 9;
			this.label1.Text = "Weight:";
			// 
			// buttonCancel
			// 
			this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonCancel.Location = new System.Drawing.Point(298, 310);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new System.Drawing.Size(75, 23);
			this.buttonCancel.TabIndex = 18;
			this.buttonCancel.Text = "Cancel";
			this.buttonCancel.UseVisualStyleBackColor = true;
			this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
			// 
			// buttonOK
			// 
			this.buttonOK.Location = new System.Drawing.Point(217, 310);
			this.buttonOK.Name = "buttonOK";
			this.buttonOK.Size = new System.Drawing.Size(75, 23);
			this.buttonOK.TabIndex = 17;
			this.buttonOK.Text = "OK";
			this.buttonOK.UseVisualStyleBackColor = true;
			this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(8, 139);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(46, 13);
			this.label3.TabIndex = 19;
			this.label3.Text = "Shapes:";
			// 
			// numericUpDownRectangles
			// 
			this.numericUpDownRectangles.Location = new System.Drawing.Point(109, 132);
			this.numericUpDownRectangles.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
			this.numericUpDownRectangles.Name = "numericUpDownRectangles";
			this.numericUpDownRectangles.Size = new System.Drawing.Size(120, 20);
			this.numericUpDownRectangles.TabIndex = 20;
			// 
			// checkBoxUseAlpha
			// 
			this.checkBoxUseAlpha.AutoSize = true;
			this.checkBoxUseAlpha.Location = new System.Drawing.Point(15, 51);
			this.checkBoxUseAlpha.Name = "checkBoxUseAlpha";
			this.checkBoxUseAlpha.Size = new System.Drawing.Size(129, 17);
			this.checkBoxUseAlpha.TabIndex = 21;
			this.checkBoxUseAlpha.Text = "Make shapes opaque";
			this.checkBoxUseAlpha.UseVisualStyleBackColor = true;
			// 
			// checkBoxRectangles
			// 
			this.checkBoxRectangles.AutoSize = true;
			this.checkBoxRectangles.Location = new System.Drawing.Point(17, 28);
			this.checkBoxRectangles.Name = "checkBoxRectangles";
			this.checkBoxRectangles.Size = new System.Drawing.Size(82, 17);
			this.checkBoxRectangles.TabIndex = 22;
			this.checkBoxRectangles.Text = "Rectamgles";
			this.checkBoxRectangles.UseVisualStyleBackColor = true;
			// 
			// checkBoxEllipses
			// 
			this.checkBoxEllipses.AutoSize = true;
			this.checkBoxEllipses.Location = new System.Drawing.Point(17, 51);
			this.checkBoxEllipses.Name = "checkBoxEllipses";
			this.checkBoxEllipses.Size = new System.Drawing.Size(61, 17);
			this.checkBoxEllipses.TabIndex = 23;
			this.checkBoxEllipses.Text = "Ellipses";
			this.checkBoxEllipses.UseVisualStyleBackColor = true;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.checkBoxRectangles);
			this.groupBox1.Controls.Add(this.checkBoxEllipses);
			this.groupBox1.Location = new System.Drawing.Point(11, 203);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(200, 83);
			this.groupBox1.TabIndex = 24;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Use following shapes";
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.checkBoxUseGradients);
			this.groupBox2.Controls.Add(this.checkBoxUseAlpha);
			this.groupBox2.Location = new System.Drawing.Point(326, 203);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(200, 83);
			this.groupBox2.TabIndex = 25;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Paint style";
			// 
			// checkBoxRandomBackground
			// 
			this.checkBoxRandomBackground.AutoSize = true;
			this.checkBoxRandomBackground.Location = new System.Drawing.Point(11, 168);
			this.checkBoxRandomBackground.Name = "checkBoxRandomBackground";
			this.checkBoxRandomBackground.Size = new System.Drawing.Size(158, 17);
			this.checkBoxRandomBackground.TabIndex = 26;
			this.checkBoxRandomBackground.Text = "Random background colour";
			this.checkBoxRandomBackground.UseVisualStyleBackColor = true;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(106, 32);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(384, 13);
			this.label4.TabIndex = 27;
			this.label4.Text = "Plugin for DualWallpaperChanger to create images composed of random shapes";
			// 
			// buttonAbout
			// 
			this.buttonAbout.Location = new System.Drawing.Point(14, 310);
			this.buttonAbout.Name = "buttonAbout";
			this.buttonAbout.Size = new System.Drawing.Size(75, 23);
			this.buttonAbout.TabIndex = 28;
			this.buttonAbout.Text = "About";
			this.buttonAbout.UseVisualStyleBackColor = true;
			this.buttonAbout.Click += new System.EventHandler(this.buttonAbout_Click);
			// 
			// pictureBox
			// 
			this.pictureBox.Image = global::DualMonitorTools.DualWallpaperChanger.RandomShapes.Properties.Resources.PluginImage;
			this.pictureBox.Location = new System.Drawing.Point(11, 12);
			this.pictureBox.Name = "pictureBox";
			this.pictureBox.Size = new System.Drawing.Size(48, 48);
			this.pictureBox.TabIndex = 29;
			this.pictureBox.TabStop = false;
			// 
			// RandomShapesForm
			// 
			this.AcceptButton = this.buttonOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.buttonCancel;
			this.ClientSize = new System.Drawing.Size(644, 351);
			this.Controls.Add(this.pictureBox);
			this.Controls.Add(this.buttonAbout);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.checkBoxRandomBackground);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.numericUpDownRectangles);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.buttonCancel);
			this.Controls.Add(this.buttonOK);
			this.Controls.Add(this.textBoxDescription);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.numericUpDownWeight);
			this.Controls.Add(this.label1);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "RandomShapesForm";
			this.Text = "Random Shapes";
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownWeight)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownRectangles)).EndInit();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.CheckBox checkBoxUseGradients;
		private System.Windows.Forms.TextBox textBoxDescription;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.NumericUpDown numericUpDownWeight;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button buttonCancel;
		private System.Windows.Forms.Button buttonOK;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.NumericUpDown numericUpDownRectangles;
		private System.Windows.Forms.CheckBox checkBoxUseAlpha;
		private System.Windows.Forms.CheckBox checkBoxRectangles;
		private System.Windows.Forms.CheckBox checkBoxEllipses;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.CheckBox checkBoxRandomBackground;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Button buttonAbout;
		private System.Windows.Forms.PictureBox pictureBox;
	}
}