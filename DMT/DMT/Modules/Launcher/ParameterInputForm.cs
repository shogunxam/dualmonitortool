#region copyright
// This file is part of Dual Monitor Tools which is a set of tools to assist
// users with multiple monitor setups.
// Copyright (C) 2009-2015 Gerald Evans
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

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DMT.Modules.Launcher
{
	public partial class ParameterInputForm : Form
	{
		//private string parameterPrompt;
		///// <summary>
		///// Prompt for the parameter
		///// </summary>
		//public string ParameterPrompt
		//{
		//	//get { return parameterPrompt; }
		//	set { parameterPrompt = value; }
		//}

		/// <summary>
		/// Prompt for the parameter
		/// </summary>
		public string ParameterPrompt { get; set; }

		//private string parameterValue;
		///// <summary>
		///// Value user entered for the parameter
		///// </summary>
		//public string ParameterValue
		//{
		//	get { return parameterValue; }
		//	//set { parameterValue = value; }
		//}
	

		/// <summary>
		/// Value user entered for the parameter
		/// </summary>
		public string ParameterValue { get; protected set; }

	
		public ParameterInputForm()
		{
			InitializeComponent();
		}

		private void ParameterInputForm_Load(object sender, EventArgs e)
		{
			labelPrompt.Text = ParameterPrompt + ":";
		}

		private void buttonOK_Click(object sender, EventArgs e)
		{
			ParameterValue = textBoxParameter.Text;
		}
	}
}
