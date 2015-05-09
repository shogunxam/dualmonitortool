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

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DMT.Modules.WallpaperChanger.Plugins.RandomShapes
{
	public partial class RandomShapesForm : Form
	{
		public RandomShapesForm(RandomShapesConfig config)
		{
			InitializeComponent();

			numericUpDownWeight.Value = config.Weight;
			textBoxDescription.Text = config.Description;
			numericUpDownRectangles.Value = config.ShapeCount;
			checkBoxRandomBackground.Checked = config.RandomBackground;
			checkBoxRectangles.Checked = config.UseRectangles;
			checkBoxEllipses.Checked = config.UseEllipses;
			checkBoxUseGradients.Checked = config.UseGradients;
			checkBoxUseAlpha.Checked = config.UseAlpha;
		}

		private void RandomShapesForm_Load(object sender, EventArgs e)
		{

		}

		private void buttonOK_Click(object sender, EventArgs e)
		{
			// TODO: validation
			if (!checkBoxRectangles.Checked && !checkBoxEllipses.Checked)
			{
				MessageBox.Show("At least one of 'Rectangles' or 'Ellipses' must be selected", this.Text);
				return;
			}

			DialogResult = DialogResult.OK;
			Close();
		}


		public RandomShapesConfig GetConfig()
		{
			// ALT: could save original config and update it directly
			RandomShapesConfig config = new RandomShapesConfig();
			config.Weight = (int)numericUpDownWeight.Value;
			config.Description = textBoxDescription.Text;
			config.ShapeCount = (int)numericUpDownRectangles.Value;
			config.RandomBackground = checkBoxRandomBackground.Checked;
			config.UseRectangles = checkBoxRectangles.Checked;
			config.UseEllipses = checkBoxEllipses.Checked;
			config.UseGradients = checkBoxUseGradients.Checked;
			config.UseAlpha = checkBoxUseAlpha.Checked;

			return config;
		}
	}
}
