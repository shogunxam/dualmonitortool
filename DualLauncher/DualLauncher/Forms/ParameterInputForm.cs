using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DualLauncher
{
	// Form for the input of dynamic data when launching an application
	public partial class ParameterInputForm : Form
	{
		private string parameterPrompt;
		/// <summary>
		/// Prompt for the parameter
		/// </summary>
		public string ParameterPrompt
		{
			//get { return parameterPrompt; }
			set { parameterPrompt = value; }
		}

		private string parameterValue;
		/// <summary>
		/// Value user entered for the parameter
		/// </summary>
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