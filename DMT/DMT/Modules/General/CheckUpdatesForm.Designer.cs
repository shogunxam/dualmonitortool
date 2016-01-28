namespace DMT.Modules.General
{
	partial class CheckUpdatesForm
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
			this.labelStatus = new System.Windows.Forms.Label();
			this.buttonOpenDownload = new System.Windows.Forms.Button();
			this.buttonInstall = new System.Windows.Forms.Button();
			this.buttonCancel = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// labelStatus
			// 
			this.labelStatus.Location = new System.Drawing.Point(12, 9);
			this.labelStatus.Name = "labelStatus";
			this.labelStatus.Size = new System.Drawing.Size(413, 66);
			this.labelStatus.TabIndex = 0;
			this.labelStatus.Text = "Checking for updates...";
			// 
			// buttonOpenDownload
			// 
			this.buttonOpenDownload.Location = new System.Drawing.Point(12, 78);
			this.buttonOpenDownload.Name = "buttonOpenDownload";
			this.buttonOpenDownload.Size = new System.Drawing.Size(132, 23);
			this.buttonOpenDownload.TabIndex = 1;
			this.buttonOpenDownload.Text = "Open download page";
			this.buttonOpenDownload.UseVisualStyleBackColor = true;
			this.buttonOpenDownload.Click += new System.EventHandler(this.buttonOpenDownload_Click);
			// 
			// buttonInstall
			// 
			this.buttonInstall.Location = new System.Drawing.Point(150, 78);
			this.buttonInstall.Name = "buttonInstall";
			this.buttonInstall.Size = new System.Drawing.Size(194, 23);
			this.buttonInstall.TabIndex = 2;
			this.buttonInstall.Text = "Download and install new version";
			this.buttonInstall.UseVisualStyleBackColor = true;
			this.buttonInstall.Click += new System.EventHandler(this.buttonInstall_Click);
			// 
			// buttonCancel
			// 
			this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonCancel.Location = new System.Drawing.Point(350, 78);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new System.Drawing.Size(75, 23);
			this.buttonCancel.TabIndex = 3;
			this.buttonCancel.Text = "Cancel";
			this.buttonCancel.UseVisualStyleBackColor = true;
			this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
			// 
			// CheckUpdatesForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.buttonCancel;
			this.ClientSize = new System.Drawing.Size(439, 117);
			this.Controls.Add(this.buttonCancel);
			this.Controls.Add(this.buttonInstall);
			this.Controls.Add(this.buttonOpenDownload);
			this.Controls.Add(this.labelStatus);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "CheckUpdatesForm";
			this.ShowInTaskbar = false;
			this.Text = "Updates for Dual Monitor Tools";
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Label labelStatus;
		private System.Windows.Forms.Button buttonOpenDownload;
		private System.Windows.Forms.Button buttonInstall;
		private System.Windows.Forms.Button buttonCancel;
	}
}