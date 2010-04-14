#region copyright
// This file is part of Dual Monitor Tools which is a set of tools to assist
// users with multiple monitor setups.
// Copyright (C) 2010  Gerald Evans
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
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

using Microsoft.Win32;

namespace DualWallpaper
{
	public partial class DualWallpaper : Form
	{
		private const int IDM_ABOUTBOX = 0x100;

		private Controller controller = new Controller();
		private List<int> selectedScreens = new List<int>();

		// This is the centered rectangle within the picture box
		// that we will use to display a preview of the wallpaper
		private Rectangle previewRect;

		private Image wallpaper = null;

		public DualWallpaper()
		{
			InitializeComponent();

			FillFitCombo();
			CalcPreviewRect();

			CreateWallpaper();

			// automatically select the first screen
			AddSelectedScreen(0);
		}

		private void FillFitCombo()
		{
			comboBoxFit.Items.Add(new Stretch(Stretch.Fit.StretchToFit));
			comboBoxFit.Items.Add(new Stretch(Stretch.Fit.OverStretch));
			comboBoxFit.Items.Add(new Stretch(Stretch.Fit.UnderStretch));
			comboBoxFit.SelectedIndex = 1;	// select OverStretch by default
		}

		private void buttonBrowse_Click(object sender, EventArgs e)
		{
			OpenFileDialog dlg = new OpenFileDialog();
			dlg.Filter = Properties.Resources.OpenImageFilter;
			//dlg.FilterIndex = 1;
			//dlg.Title=
			if (dlg.ShowDialog() == DialogResult.OK)
			{
				textBoxImage.Text = dlg.FileName;
				try
				{
					picSource.Image = Bitmap.FromFile(textBoxImage.Text);
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message, Program.MyTitle);
				}
			}
		}

		private void buttonAdd_Click(object sender, EventArgs e)
		{
			if (textBoxImage.Text.Length > 0)
			{
				// load image file
				try
				{
					Image image = Bitmap.FromFile(textBoxImage.Text);
					Stretch stretchType = comboBoxFit.SelectedItem as Stretch;
					Debug.Assert(stretchType != null);
					controller.AddImage(image, stretchType.Type);

					CreateWallpaper();
					UpdatePreview();
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message, Program.MyTitle);
				}
			}
		}

		private void CreateWallpaper()
		{
			if (wallpaper != null)
			{
				wallpaper.Dispose();
			}
			wallpaper = controller.CreateImage();
		}

		private void UpdatePreview()
		{
			DisplaySelectedScreens();
			Image preview = new Bitmap(previewRect.Width, previewRect.Height);

			using (Graphics g = Graphics.FromImage(preview))
			{
				g.DrawImage(wallpaper, 0, 0, preview.Width, preview.Height);

				// now indicate the positions of the monitors
				DisplayMonitors(g, preview.Size);
				            
			}

			// display preview
			picPreview.Image = preview;
		}

		private void DisplayMonitors(Graphics g, Size previewSize)
		{
			for (int screenIndex = 0; screenIndex < Screen.AllScreens.Length; screenIndex++)
			{
				string screenName = string.Format("{0}", screenIndex + 1);
				if (Screen.AllScreens[screenIndex].Primary)
				{
					screenName += "P";
				}
				DisplayMonitor(g, previewSize, Screen.AllScreens[screenIndex].Bounds, screenIndex, screenName);
			}
		}

		private void DisplayMonitor(Graphics g, Size previewSize, Rectangle screenRect, int screenIndex, string screenName)
		{
			Rectangle previewRect = new Rectangle(new Point(0,0), previewSize);

			// need to determine position of screen rect in the preview
			Rectangle previewScreen = Controller.CalcDestRect(controller.DesktopRect, previewRect, screenRect);

			// TODO: look into this!
			previewScreen = new Rectangle(previewScreen.Left, previewScreen.Top, previewScreen.Width - 1, previewScreen.Height - 1);

			// draw border around screen
			Pen borderPen1 = Pens.Black;
			Pen borderPen2 = Pens.White;
			Brush textBrush = Brushes.White;
			if (IsScreenSelected(screenIndex))
			{
				borderPen1 = Pens.Yellow;
				borderPen2 = Pens.Yellow;
				textBrush = Brushes.Yellow;
			}
			previewScreen.Inflate(-1, -1);
			g.DrawRectangle(borderPen1, previewScreen);
			previewScreen.Inflate(-1, -1);
//			g.DrawRectangle(Pens.Black, previewScreen);
//			previewScreen.Inflate(-1, -1);
			g.DrawRectangle(borderPen2, previewScreen);

			// display the screen name centered in the screen
			using (Font font = new Font("Arial", 24, FontStyle.Bold, GraphicsUnit.Point))
			{
				g.DrawString(screenName, font, textBrush, previewScreen);
			}
		}
		private void CalcPreviewRect()
		{
			Size sourceSize = controller.DesktopRect.Size;
			Rectangle pictureBoxRect = new Rectangle(new Point(0, 0), picPreview.Size);
			// reduce preview size if needed to maintain aspect ratio
			previewRect = Controller.UnderStretch(sourceSize, pictureBoxRect);
		}

		#region screen selection
		private bool IsScreenSelected(int screenIndex)
		{
			bool selected = false;
			foreach (int screen in selectedScreens)
			{
				if (screen == screenIndex)
				{
					selected = true;
					break;
				}
			}

			return selected;
		}

		private void picPreview_MouseClick(object sender, MouseEventArgs e)
		{
			// check if the area clicked belongs to one of the screens
			int screenIndex = PosnToScreen(e.X, e.Y);
			if (screenIndex >= 0)
			{
				if ((Control.ModifierKeys & Keys.Control) != 0)
				{
					// control pressed down - add screen to current list
					if (IsScreenSelected(screenIndex))
					{
						// already selected - so remove (unless it is the only selected screen)
						RemoveSelectedScreen(screenIndex);
					}
					else
					{
						AddSelectedScreen(screenIndex);
					}
				}
				else
				{
					// replace current screen list with screen just clicked
					selectedScreens.Clear();
					AddSelectedScreen(screenIndex);
				}
			}
		}

		private void AddSelectedScreen(int screenIndex)
		{
			if (!IsScreenSelected(screenIndex))
			{
				selectedScreens.Add(screenIndex);
				OnSeclectedScreensChanged();
			}
		}

		private void RemoveSelectedScreen(int screenIndex)
		{
			// always want at least one screen selected
			if (selectedScreens.Count > 1)
			{
				selectedScreens.Remove(screenIndex);
				OnSeclectedScreensChanged();
			}
		}

		private void OnSeclectedScreensChanged()
		{
			controller.SetActiveScreens(selectedScreens);
			UpdatePreview();
		}

		private void DisplaySelectedScreens()
		{
			string screenText = "";
			foreach (int screen in selectedScreens)
			{
				if (screenText.Length > 0)
				{
					screenText += ", ";
				}
				screenText += String.Format("{0}", screen + 1);
			}

			labelScreensSelected.Text = screenText;
		}
		#endregion

		private int PosnToScreen(int x, int y)
		{
			int ret = -1;

			if (previewRect.Contains(x, y))
			{
				// now map this onto the virtual desktop
				Point desktopPoint = Controller.CalcDestPoint(previewRect, controller.DesktopRect, new Point(x, y));

				// now check each screen to see if it contains this point
				for (int screenIndex = 0; screenIndex < controller.AllScreens.Count; screenIndex++)
				{
					if (controller.AllScreens[screenIndex].ScreenRect.Contains(desktopPoint.X, desktopPoint.Y))
					{
						ret = screenIndex;
						break;
					}
				}
			}
			return ret;
		}

		private void buttonSave_Click(object sender, EventArgs e)
		{
			SaveFileDialog dlg = new SaveFileDialog();
			dlg.Filter = Properties.Resources.SaveImageFilter;
			if (dlg.ShowDialog() == DialogResult.OK)
			{
				try
				{
					wallpaper.Save(dlg.FileName, System.Drawing.Imaging.ImageFormat.Bmp);
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message, Program.MyTitle);
				}
			}
		}

		private void buttonSetWallpaper_Click(object sender, EventArgs e)
		{
			string dir = Program.MyAppDataDir;
			string path = Path.Combine(dir, "DualWallpaper.bmp");
			wallpaper.Save(path, System.Drawing.Imaging.ImageFormat.Bmp);

			// make sure image is tiled
			using (RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Control Panel\Desktop", true))
			{
				key.SetValue("TileWallpaper", "1");
				key.SetValue("WallpaperStyle", "0");
			}

			// now set the wallpaper
			Win32.SystemParametersInfo(Win32.SPI_SETDESKWALLPAPER, 0, path, Win32.SPIF_UPDATEINIFILE | Win32.SPIF_SENDWININICHANGE);

		}

		#region image movement
		// movement indicates where you want to look,
		// which is the reverse of what happens to onjects in the picture
		private void buttonMoveUp_Click(object sender, EventArgs e)
		{
			MoveSelection(0, 1);
		}

		private void buttonMoveRight_Click(object sender, EventArgs e)
		{
			MoveSelection(-1, 0);
		}

		private void buttonMoveDown_Click(object sender, EventArgs e)
		{
			MoveSelection(0, -1);
		}

		private void buttonMoveLeft_Click(object sender, EventArgs e)
		{
			MoveSelection(1, 0);
		}

		private void MoveSelection(int deltaX, int deltaY)
		{
			// get multiplier for movement
			int factor = GetMovementFactor();
			// and multiply the movement by this
			deltaX *= factor;
			deltaY *= factor;

			controller.MoveActiveScreens(deltaX, deltaY);
			CreateWallpaper();
			UpdatePreview();
		}

		private int GetMovementFactor()
		{
			int factor = 1;
			if (radioButtonMove1.Checked)
			{
				factor = 1;
			}
			else if (radioButtonMove10.Checked)
			{
				factor = 10;
			}
			else if (radioButtonMove100.Checked)
			{
				factor = 100;
			}

			return factor;
		}

		#endregion

		private void moveImage_CheckedChanged(object sender, EventArgs e)
		{
			//
		}

		private void DualWallpaper_Shown(object sender, EventArgs e)
		{
			SystemMenuHelper.AppendSeparator(this);
			SystemMenuHelper.Append(this, IDM_ABOUTBOX, Properties.Resources.AboutMenuItem);
		}

		protected override void WndProc(ref Message m)
		{
			if (m.Msg == Win32.WM_SYSCOMMAND)
			{
				if (m.WParam.ToInt32() == IDM_ABOUTBOX)
				{
					AboutForm dlg = new AboutForm();
					dlg.ShowDialog();
				}
			}

			base.WndProc(ref m);
		}

	}
}