namespace SwapScreen
{
	partial class UdaPanel
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
			this.labelDescription = new System.Windows.Forms.Label();
			this.buttonChange = new System.Windows.Forms.Button();
			this.labelKeyCombo = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// labelDescription
			// 
			this.labelDescription.Location = new System.Drawing.Point(0, 5);
			this.labelDescription.Name = "labelDescription";
			this.labelDescription.Size = new System.Drawing.Size(224, 13);
			this.labelDescription.TabIndex = 8;
			this.labelDescription.Text = "Description";
			// 
			// buttonChange
			// 
			this.buttonChange.Location = new System.Drawing.Point(389, 0);
			this.buttonChange.Name = "buttonChange";
			this.buttonChange.Size = new System.Drawing.Size(75, 23);
			this.buttonChange.TabIndex = 7;
			this.buttonChange.Text = "Change...";
			this.buttonChange.UseVisualStyleBackColor = true;
			this.buttonChange.Click += new System.EventHandler(this.buttonChange_Click);
			// 
			// labelKeyCombo
			// 
			this.labelKeyCombo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
			this.labelKeyCombo.Location = new System.Drawing.Point(230, 5);
			this.labelKeyCombo.Name = "labelKeyCombo";
			this.labelKeyCombo.Size = new System.Drawing.Size(153, 13);
			this.labelKeyCombo.TabIndex = 9;
			this.labelKeyCombo.Text = "KeyCombo";
			// 
			// UdaPanel
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.labelDescription);
			this.Controls.Add(this.buttonChange);
			this.Controls.Add(this.labelKeyCombo);
			this.Name = "UdaPanel";
			this.Size = new System.Drawing.Size(465, 23);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Label labelDescription;
		private System.Windows.Forms.Button buttonChange;
		private System.Windows.Forms.Label labelKeyCombo;
	}
}
