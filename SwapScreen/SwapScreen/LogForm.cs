using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SwapScreen
{
	public partial class LogForm : Form
	{
		private List<string> log;

		public LogForm(List<string> log)
		{
			this.log = log;
			InitializeComponent();
		}

		private void LogForm_Load(object sender, EventArgs e)
		{
			ShowLog();
		}

		private void ShowLog()
		{
			listBox.BeginUpdate();
			listBox.Items.Clear();
			foreach (string s in log)
			{
				listBox.Items.Add(s);
			}
			listBox.EndUpdate();
		}
	}
}