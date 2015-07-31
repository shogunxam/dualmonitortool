#region copyright
// This file is part of Dual Monitor Tools which is a set of tools to assist
// users with multiple monitor setups.
// Copyright (C) 2015  Gerald Evans
// 
// Dual Monitor Tools is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.
#endregion

namespace DMT.Modules.WallpaperChanger.Plugins.Flickr
{
	partial class FlickrForm
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
			this.linkLabel = new System.Windows.Forms.LinkLabel();
			this.checkBoxRandomPage = new System.Windows.Forms.CheckBox();
			this.buttonCancel = new System.Windows.Forms.Button();
			this.buttonOK = new System.Windows.Forms.Button();
			this.textBoxDescription = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.numericUpDownWeight = new System.Windows.Forms.NumericUpDown();
			this.label4 = new System.Windows.Forms.Label();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.label1 = new System.Windows.Forms.Label();
			this.textBoxTags = new System.Windows.Forms.TextBox();
			this.checkBoxTagMode = new System.Windows.Forms.CheckBox();
			this.label3 = new System.Windows.Forms.Label();
			this.textBoxText = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.textBoxUserId = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.textBoxGroupId = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.textBoxApiKey = new System.Windows.Forms.TextBox();
			this.buttonTest = new System.Windows.Forms.Button();
			this.toolTip = new System.Windows.Forms.ToolTip(this.components);
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownWeight)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.SuspendLayout();
			// 
			// linkLabel
			// 
			this.linkLabel.Location = new System.Drawing.Point(109, 31);
			this.linkLabel.Name = "linkLabel";
			this.linkLabel.Size = new System.Drawing.Size(473, 13);
			this.linkLabel.TabIndex = 25;
			this.linkLabel.TabStop = true;
			this.linkLabel.Text = "Image provider to get images from ";
			this.linkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel_LinkClicked);
			// 
			// checkBoxRandomPage
			// 
			this.checkBoxRandomPage.AutoSize = true;
			this.checkBoxRandomPage.Location = new System.Drawing.Point(112, 258);
			this.checkBoxRandomPage.Name = "checkBoxRandomPage";
			this.checkBoxRandomPage.Size = new System.Drawing.Size(405, 17);
			this.checkBoxRandomPage.TabIndex = 30;
			this.checkBoxRandomPage.Text = "Take image from random page (rather than page containing latest added photos)";
			this.checkBoxRandomPage.UseVisualStyleBackColor = true;
			// 
			// buttonCancel
			// 
			this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonCancel.Location = new System.Drawing.Point(352, 350);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new System.Drawing.Size(75, 23);
			this.buttonCancel.TabIndex = 32;
			this.buttonCancel.Text = "Cancel";
			this.buttonCancel.UseVisualStyleBackColor = true;
			// 
			// buttonOK
			// 
			this.buttonOK.Location = new System.Drawing.Point(206, 350);
			this.buttonOK.Name = "buttonOK";
			this.buttonOK.Size = new System.Drawing.Size(75, 23);
			this.buttonOK.TabIndex = 31;
			this.buttonOK.Text = "OK";
			this.buttonOK.UseVisualStyleBackColor = true;
			this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
			// 
			// textBoxDescription
			// 
			this.textBoxDescription.Location = new System.Drawing.Point(112, 105);
			this.textBoxDescription.Name = "textBoxDescription";
			this.textBoxDescription.Size = new System.Drawing.Size(470, 20);
			this.textBoxDescription.TabIndex = 29;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(11, 108);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(63, 13);
			this.label2.TabIndex = 28;
			this.label2.Text = "Description:";
			// 
			// numericUpDownWeight
			// 
			this.numericUpDownWeight.Location = new System.Drawing.Point(112, 79);
			this.numericUpDownWeight.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
			this.numericUpDownWeight.Name = "numericUpDownWeight";
			this.numericUpDownWeight.Size = new System.Drawing.Size(120, 20);
			this.numericUpDownWeight.TabIndex = 27;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(11, 81);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(44, 13);
			this.label4.TabIndex = 26;
			this.label4.Text = "Weight:";
			// 
			// pictureBox1
			// 
			this.pictureBox1.Image = global::DMT.Properties.Resources.FlickrPlugin;
			this.pictureBox1.Location = new System.Drawing.Point(12, 12);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(48, 48);
			this.pictureBox1.TabIndex = 33;
			this.pictureBox1.TabStop = false;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(11, 134);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(34, 13);
			this.label1.TabIndex = 34;
			this.label1.Text = "Tags:";
			// 
			// textBoxTags
			// 
			this.textBoxTags.Location = new System.Drawing.Point(112, 131);
			this.textBoxTags.Name = "textBoxTags";
			this.textBoxTags.Size = new System.Drawing.Size(470, 20);
			this.textBoxTags.TabIndex = 35;
			this.toolTip.SetToolTip(this.textBoxTags, "Comma separated list of tags");
			// 
			// checkBoxTagMode
			// 
			this.checkBoxTagMode.AutoSize = true;
			this.checkBoxTagMode.Location = new System.Drawing.Point(112, 157);
			this.checkBoxTagMode.Name = "checkBoxTagMode";
			this.checkBoxTagMode.Size = new System.Drawing.Size(210, 17);
			this.checkBoxTagMode.TabIndex = 36;
			this.checkBoxTagMode.Text = "Photos must have all or the above tags";
			this.checkBoxTagMode.UseVisualStyleBackColor = true;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(11, 183);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(31, 13);
			this.label3.TabIndex = 37;
			this.label3.Text = "Text:";
			// 
			// textBoxText
			// 
			this.textBoxText.Location = new System.Drawing.Point(112, 180);
			this.textBoxText.Name = "textBoxText";
			this.textBoxText.Size = new System.Drawing.Size(470, 20);
			this.textBoxText.TabIndex = 38;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(11, 209);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(44, 13);
			this.label5.TabIndex = 39;
			this.label5.Text = "User Id:";
			// 
			// textBoxUserId
			// 
			this.textBoxUserId.Location = new System.Drawing.Point(112, 206);
			this.textBoxUserId.Name = "textBoxUserId";
			this.textBoxUserId.Size = new System.Drawing.Size(470, 20);
			this.textBoxUserId.TabIndex = 40;
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(11, 235);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(51, 13);
			this.label6.TabIndex = 41;
			this.label6.Text = "Group Id:";
			// 
			// textBoxGroupId
			// 
			this.textBoxGroupId.Location = new System.Drawing.Point(112, 232);
			this.textBoxGroupId.Name = "textBoxGroupId";
			this.textBoxGroupId.Size = new System.Drawing.Size(470, 20);
			this.textBoxGroupId.TabIndex = 42;
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(11, 284);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(48, 13);
			this.label7.TabIndex = 43;
			this.label7.Text = "API Key:";
			// 
			// textBoxApiKey
			// 
			this.textBoxApiKey.Location = new System.Drawing.Point(112, 281);
			this.textBoxApiKey.Name = "textBoxApiKey";
			this.textBoxApiKey.Size = new System.Drawing.Size(470, 20);
			this.textBoxApiKey.TabIndex = 44;
			this.toolTip.SetToolTip(this.textBoxApiKey, "This needs to be obtained from flickr");
			// 
			// buttonTest
			// 
			this.buttonTest.Location = new System.Drawing.Point(12, 350);
			this.buttonTest.Name = "buttonTest";
			this.buttonTest.Size = new System.Drawing.Size(128, 23);
			this.buttonTest.TabIndex = 45;
			this.buttonTest.Text = "Test Search Criteria";
			this.buttonTest.UseVisualStyleBackColor = true;
			this.buttonTest.Click += new System.EventHandler(this.buttonTest_Click);
			// 
			// FlickrForm
			// 
			this.AcceptButton = this.buttonOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.buttonCancel;
			this.ClientSize = new System.Drawing.Size(622, 385);
			this.Controls.Add(this.buttonTest);
			this.Controls.Add(this.textBoxApiKey);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.textBoxGroupId);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.textBoxUserId);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.textBoxText);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.checkBoxTagMode);
			this.Controls.Add(this.textBoxTags);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.linkLabel);
			this.Controls.Add(this.checkBoxRandomPage);
			this.Controls.Add(this.buttonCancel);
			this.Controls.Add(this.buttonOK);
			this.Controls.Add(this.textBoxDescription);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.numericUpDownWeight);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.pictureBox1);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FlickrForm";
			this.ShowInTaskbar = false;
			this.Text = "flickr";
			this.Load += new System.EventHandler(this.FlickrForm_Load);
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownWeight)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.LinkLabel linkLabel;
		private System.Windows.Forms.CheckBox checkBoxRandomPage;
		private System.Windows.Forms.Button buttonCancel;
		private System.Windows.Forms.Button buttonOK;
		private System.Windows.Forms.TextBox textBoxDescription;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.NumericUpDown numericUpDownWeight;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox textBoxTags;
		private System.Windows.Forms.CheckBox checkBoxTagMode;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox textBoxText;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox textBoxUserId;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox textBoxGroupId;
		private System.Windows.Forms.Label label7;
		public System.Windows.Forms.TextBox textBoxApiKey;
		private System.Windows.Forms.Button buttonTest;
		private System.Windows.Forms.ToolTip toolTip;
	}
}