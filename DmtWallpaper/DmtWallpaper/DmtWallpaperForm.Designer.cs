#region copyright
// This file is part of Dual Monitor Tools which is a set of tools to assist
// users with multiple monitor setups.
// Copyright (C) 2016 Gerald Evans
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

namespace DmtWallpaper
{
	partial class DmtWallpaperForm
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
			this.blendTimer = new System.Windows.Forms.Timer(this.components);
			this.imageBlender = new DmtWallpaper.ImageBlender();
			this.SuspendLayout();
			// 
			// blendTimer
			// 
			this.blendTimer.Tick += new System.EventHandler(this.blendTimer_Tick);
			// 
			// imageBlender
			// 
			this.imageBlender.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.imageBlender.Blend = 0F;
			this.imageBlender.Location = new System.Drawing.Point(0, 0);
			this.imageBlender.Name = "imageBlender";
			this.imageBlender.Size = new System.Drawing.Size(200, 200);
			this.imageBlender.TabIndex = 0;
			this.imageBlender.Text = "imageBlender";
			this.imageBlender.KeyDown += new System.Windows.Forms.KeyEventHandler(this.imageBlender_KeyDown);
			this.imageBlender.MouseClick += new System.Windows.Forms.MouseEventHandler(this.imageBlender_MouseClick);
			this.imageBlender.MouseMove += new System.Windows.Forms.MouseEventHandler(this.imageBlender_MouseMove);
			// 
			// DmtWallpaperForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(200, 200);
			this.Controls.Add(this.imageBlender);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "DmtWallpaperForm";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "DMT Wallpaper Screen Saver";
			this.Load += new System.EventHandler(this.DmtWallpaperForm_Load);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DmtWallpaperForm_KeyDown);
			this.ResumeLayout(false);

		}

		#endregion

		private ImageBlender imageBlender;
		private System.Windows.Forms.Timer blendTimer;

	}
}

