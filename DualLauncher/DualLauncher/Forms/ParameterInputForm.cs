using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DualLauncher
{
	public partial class ParameterInputForm : Form
	{
		private string parameterPrompt;
		public string ParameterPrompt
		{
			//get { return parameterPrompt; }
			set { parameterPrompt = value; }
		}

		private string parameterValue;

		public string ParameterValue
		{
			get { return parameterValue; }
			//set { parameterValue = value; }
		}
	
	
		public ParameterInputForm()
		{
			InitializeComponent();
		}

		private void ParameterInputForm_Load(object sender, EventArgs e)
		{
			labelPrompt.Text = parameterPrompt + ":";
		}

		private void buttonOK_Click(object sender, EventArgs e)
		{
			parameterValue = textBoxParameter.Text;
		}
	}
}