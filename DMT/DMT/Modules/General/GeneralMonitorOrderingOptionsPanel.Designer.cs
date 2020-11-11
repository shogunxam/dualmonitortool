namespace DMT.Modules.General
{
	partial class GeneralMonitorOrderingOptionsPanel
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
			this.picPreview = new System.Windows.Forms.PictureBox();
			this.groupBoxOrder = new System.Windows.Forms.GroupBox();
			this.radioButtonDotNet = new System.Windows.Forms.RadioButton();
			this.radioButtonLeftRight = new System.Windows.Forms.RadioButton();
			this.label1 = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.picPreview)).BeginInit();
			this.groupBoxOrder.SuspendLayout();
			this.SuspendLayout();
			// 
			// picPreview
			// 
			this.picPreview.Location = new System.Drawing.Point(3, 3);
			this.picPreview.Name = "picPreview";
			this.picPreview.Size = new System.Drawing.Size(494, 161);
			this.picPreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
			this.picPreview.TabIndex = 1;
			this.picPreview.TabStop = false;
			// 
			// groupBoxOrder
			// 
			this.groupBoxOrder.Controls.Add(this.radioButtonDotNet);
			this.groupBoxOrder.Controls.Add(this.radioButtonLeftRight);
			this.groupBoxOrder.Location = new System.Drawing.Point(3, 170);
			this.groupBoxOrder.Name = "groupBoxOrder";
			this.groupBoxOrder.Size = new System.Drawing.Size(274, 79);
			this.groupBoxOrder.TabIndex = 2;
			this.groupBoxOrder.TabStop = false;
			this.groupBoxOrder.Text = "Order by";
			// 
			// radioButtonDotNet
			// 
			this.radioButtonDotNet.AutoSize = true;
			this.radioButtonDotNet.Location = new System.Drawing.Point(6, 42);
			this.radioButtonDotNet.Name = "radioButtonDotNet";
			this.radioButtonDotNet.Size = new System.Drawing.Size(246, 17);
			this.radioButtonDotNet.TabIndex = 1;
			this.radioButtonDotNet.TabStop = true;
			this.radioButtonDotNet.Text = "DotNet order, as used by DMT v2.7 and earlier";
			this.radioButtonDotNet.UseVisualStyleBackColor = true;
			this.radioButtonDotNet.CheckedChanged += new System.EventHandler(this.radioButtonDotNet_CheckedChanged);
			// 
			// radioButtonLeftRight
			// 
			this.radioButtonLeftRight.AutoSize = true;
			this.radioButtonLeftRight.Location = new System.Drawing.Point(6, 19);
			this.radioButtonLeftRight.Name = "radioButtonLeftRight";
			this.radioButtonLeftRight.Size = new System.Drawing.Size(128, 17);
			this.radioButtonLeftRight.TabIndex = 0;
			this.radioButtonLeftRight.TabStop = true;
			this.radioButtonLeftRight.Text = "Left - right, top - down";
			this.radioButtonLeftRight.UseVisualStyleBackColor = true;
			this.radioButtonLeftRight.CheckedChanged += new System.EventHandler(this.radioButtonLeftRight_CheckedChanged);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(6, 268);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(491, 34);
			this.label1.TabIndex = 3;
			this.label1.Text = "Note: This monitor ordering is only used by DMT itself for the Monitors grid, mov" +
    "ing windows between monitors and wallpaper generation.";
			// 
			// GeneralMonitorOrderingOptionsPanel
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.label1);
			this.Controls.Add(this.groupBoxOrder);
			this.Controls.Add(this.picPreview);
			this.Name = "GeneralMonitorOrderingOptionsPanel";
			this.Size = new System.Drawing.Size(500, 360);
			((System.ComponentModel.ISupportInitialize)(this.picPreview)).EndInit();
			this.groupBoxOrder.ResumeLayout(false);
			this.groupBoxOrder.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.PictureBox picPreview;
		private System.Windows.Forms.GroupBox groupBoxOrder;
		private System.Windows.Forms.RadioButton radioButtonDotNet;
		private System.Windows.Forms.RadioButton radioButtonLeftRight;
		private System.Windows.Forms.Label label1;
	}
}
