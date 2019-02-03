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

namespace DMT.Modules.SwapScreen
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel;
	using System.Data;
	using System.Drawing;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;
	using System.Windows.Forms;

	using DMT.Library;
	using DMT.Library.GuiUtils;
	using DMT.Library.HotKeys;
	using DMT.Library.PInvoke;
	using DMT.Resources;

	/// <summary>
	/// Dialog for editing an user defined area
	/// </summary>
	partial class UdaForm : Form
	{
		UdaController _udaController;

		/// <summary>
		/// Initialises a new instance of the <see cref="UdaForm" /> class.
		/// </summary>
		/// <param name="udaController">Controller for the user defined area</param>
		public UdaForm(UdaController udaController)
		{
			_udaController = udaController;

			InitializeComponent();

			InitWindowPicker();

			ShowCurrentValues();
		}

		void InitWindowPicker()
		{
			// initialise the window picker
			windowPicker.InitControl(
				Properties.Resources.TargetCursor,
				Properties.Resources.WinNoCrossHairs,
				Properties.Resources.WinCrossHairs);

			windowPicker.HoveredWindowChanged += new WindowPicker.HoverHandler(windowPicker_HoveredWindowChanged);
		}

		private void windowPicker_HoveredWindowChanged(IntPtr hWnd)
		{
			NativeMethods.RECT rect;
			if (NativeMethods.GetWindowRect(hWnd, out rect))
			{
				Rectangle rectangle = ScreenHelper.RectToRectangle(ref rect);
				SetWindowRect(rectangle);
			}
		}

		/// <summary>
		/// Changes the displayed rectangle
		/// </summary>
		/// <param name="rectangle">Updates panel to use this rectangle</param>
		void SetWindowRect(Rectangle rectangle)
		{
			textBoxX.Text = rectangle.X.ToString();
			textBoxY.Text = rectangle.Y.ToString();
			textBoxWidth.Text = rectangle.Width.ToString();
			textBoxHeight.Text = rectangle.Height.ToString();
		}

		void ShowCurrentValues()
		{
			textBoxName.Text = _udaController.Description;
			Rectangle position = _udaController.Position;
			textBoxX.Text = position.X.ToString();
			textBoxY.Text = position.Y.ToString();
			textBoxWidth.Text = position.Width.ToString();
			textBoxHeight.Text = position.Height.ToString();

			keyComboPanel.KeyCombo = _udaController.HotKey.HotKeyCombo;
			checkBoxEnable.Checked = _udaController.HotKey.HotKeyCombo.Enabled;
			UpdateEnableStatus();
		}

		void UpdateEnableStatus()
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
				MsgDlg.UserError(SwapScreenStrings.InvalidPosition);
				return;
			}

			if (keyCombo.Enabled)
			{
				if (keyCombo.KeyCode == (Keys)0)
				{
					MsgDlg.UserError(CommonStrings.NoKey);
					return;
				}
			}

			Rectangle position = new Rectangle(left, top, width, height);

			if (keyCombo.Enabled)
			{
				if (keyCombo.KeyCode == (Keys)0)
				{
					MsgDlg.UserError(CommonStrings.NoKey);
					return;
				}
			}

			// set the values in the controller which will also attempt to register the hotkey
			// this returns false if the registration fails
			// TODO: need to tidy this up
			if (!_udaController.SetValues(keyCombo.ComboValue, position, textBoxName.Text))
			{
				MsgDlg.UserError(CommonStrings.RegisterFail);
				return;
			}

			// hotkey is OK
			DialogResult = DialogResult.OK;
			Close();
		}

		private void buttonCancel_Click(object sender, EventArgs e)
		{
		}

		private void checkBoxEnable_CheckedChanged(object sender, EventArgs e)
		{
			UpdateEnableStatus();
		}

		int TextBoxToInt(Control control, ref bool isValid)
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
