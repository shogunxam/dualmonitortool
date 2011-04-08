#region copyright
// This file is part of Dual Monitor Tools which is a set of tools to assist
// users with multiple monitor setups.
// Copyright (C) 2011  Gerald Evans
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
using System.Text;
using System.Windows.Forms;

namespace SwapScreen
{
	public partial class UdaForm : Form
	{
		private UdaController udaController;

		public UdaForm(UdaController udaController)
		{
			this.udaController = udaController;

			InitializeComponent();

			InitWindowPicker();

			ShowCurrentValues();
		}

		private void InitWindowPicker()
		{
			// initialise the window picker
			this.windowPicker.InitControl(SwapScreen.Properties.Resources.TargetCursor,
				Properties.Resources.WinNoCrossHairs,
				Properties.Resources.WinCrossHairs);

			this.windowPicker.HoveredWindowChanged += new WindowPicker.HoverHandler(windowPicker_HoveredWindowChanged);
		}

		private void windowPicker_HoveredWindowChanged(IntPtr hWnd)
		{
			Win32.RECT rect;
			if (Win32.GetWindowRect(hWnd, out rect))
			{
				Rectangle rectangle = ScreenHelper.RectToRectangle(ref rect);
				SetWindowRect(rectangle);
			}
		}

		/// <summary>
		/// Changes the displayed rectangle
		/// </summary>
		/// <param name="rectangle"></param>
		public void SetWindowRect(Rectangle rectangle)
		{
			this.textBoxX.Text = rectangle.X.ToString();
			this.textBoxY.Text = rectangle.Y.ToString();
			this.textBoxWidth.Text = rectangle.Width.ToString();
			this.textBoxHeight.Text = rectangle.Height.ToString();
		}

		private void ShowCurrentValues()
		{
			textBoxName.Text = udaController.Description;
			Rectangle position = udaController.Position;
			textBoxX.Text = position.X.ToString();
			textBoxY.Text = position.Y.ToString();
			textBoxWidth.Text = position.Width.ToString();
			textBoxHeight.Text = position.Height.ToString();

			keyComboPanel.KeyCombo = udaController.HotKeyCombo;
			checkBoxEnable.Checked = udaController.HotKeyCombo.Enabled;

			UpdateEnableStatus();
		}

		private void checkBoxEnable_CheckedChanged(object sender, EventArgs e)
		{
			UpdateEnableStatus();
		}

		private void UpdateEnableStatus()
		{
			bool enable = checkBoxEnable.Checked;
			textBoxName.Enabled = enable;

			textBoxX.Enabled = enable;
			textBoxY.Enabled = enable;
			textBoxWidth.Enabled = enable;
			textBoxHeight.Enabled = enable;

			keyComboPanel.Enabled = enable;
		}

		private void buttonOK_Click(object sender, EventArgs e)
		{
			// get the hotkey from the panel
			KeyCombo keyCombo = keyComboPanel.KeyCombo;

			// and disable/enable it depending on the checkbox
			keyCombo.Enabled = checkBoxEnable.Checked;

			// validate the co-ordinates
			bool isValid = true;

			// TODO: we could perform further validation here
			int left = TextBoxToInt(textBoxX, ref isValid);
			int top = TextBoxToInt(textBoxY, ref isValid);
			int width = TextBoxToInt(textBoxWidth, ref isValid);
			int height = TextBoxToInt(textBoxHeight, ref isValid);

			// allow zero width/height so you can make window disapear?
			if (width < 0)
			{
				isValid = false;
			}
			if (height < 0)
			{
				isValid = false;
			}

			if (!isValid)
			{
				MessageBox.Show(Properties.Resources.InvalidPosition, Program.MyTitle);
				return;
			}
			Rectangle position = new Rectangle(left, top, width, height);

			if (keyCombo.Enabled)
			{
				if (keyCombo.KeyCode == (Keys)0)
				{
					MessageBox.Show(Properties.Resources.NoKey, Program.MyTitle);
					return;
				}
			}


			// set the values in the controller which will also attempt to register the hotkey
			// this returns false if the registration fails
			// TODO: need to tidy this up
			if (!udaController.SetValues(keyCombo.ComboValue, position, textBoxName.Text))
			{
				MessageBox.Show(Properties.Resources.RegisterFail, Program.MyTitle);
				return;
			}

			// all OK
			// tell controller to save the settings
			Controller.Instance.SaveUdaSettings();

			DialogResult = DialogResult.OK;
			Close();
		}


		private int TextBoxToInt(Control control, ref bool isValid)
		{
			// only set isValid to false
			int ret = 0;	// defalt value if not a number
			try
			{
				ret = Convert.ToInt32(control.Text);
			}
			catch (Exception)
			{
				// if this is the first control that is invalid, set focus to it
				if (isValid)
				{
					control.Focus();
				}
				isValid = false;
			}

			return ret;
		}

	}
}