#region copyright
// This file is part of Dual Monitor Tools which is a set of tools to assist
// users with multiple monitor setups.
// Copyright (C) 2020  Gerald Evans
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

namespace DMT.Modules.General
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel;
	using System.Drawing;
	using System.Data;
	using System.Linq;
	using System.Text;
	using System.Windows.Forms;
	using DMT.Library.Environment;
	using DMT.Library.Wallpaper;

	partial class GeneralMonitorOrderingOptionsPanel : UserControl
	{
		GeneralModule _generalModule;

		public GeneralMonitorOrderingOptionsPanel(GeneralModule generalModule)
		{
			_generalModule = generalModule;

			InitializeComponent();

			UpdateOrderingButtons();

			ShowCurrentLayout();
		}

		public void ShowCurrentLayout()
		{
			ShowMonitorLayout();
		}


		void UpdateOrderingButtons()
		{
			switch (_generalModule.MonitorOrder) //(Monitor.MonitorOrder)
			{
				case Monitor.EMonitorOrder.DotNet:
					radioButtonDotNet.Checked = true;
					break;

				case Monitor.EMonitorOrder.LeftRight: // default case
				default:
					radioButtonLeftRight.Checked = true;
					break;
			}
		}

		// TODO: some of this is duplicated from WallpaperChangerOptionsPanel,
		// so may want to see if common code can be extracted out
		void ShowMonitorLayout()
		{
			Monitors monitors = Monitor.AllMonitors;

			// get rectangle within the picture box where the monitoes will be displayed
			Rectangle pictureBoxRect = new Rectangle(new Point(0, 0), picPreview.Size);
			Rectangle previewRect = ImageFitter.UnderStretch(monitors.Bounds.Size, pictureBoxRect);

			Image preview = new Bitmap(previewRect.Width, previewRect.Height);

			using (Graphics g = Graphics.FromImage(preview))
			{
				for (int monitorIndex = 0; monitorIndex < monitors.Count; monitorIndex++)
				{
					string monitorName = string.Format("{0}", monitorIndex + 1);
					if (monitors[monitorIndex].Primary)
					{
						monitorName += "P";
					}
					ShowMonitor(monitors, monitorIndex, g, previewRect, monitorName);
				}
			}

			// display preview
			if (picPreview.Image != null)
			{
				picPreview.Image.Dispose();
			}

			picPreview.Image = preview;

		}

		void ShowMonitor(Monitors monitors, int monitorIndex, Graphics g, Rectangle previewRect, string screenName)
		{
			// need to determine position of screen rect in the preview
			Rectangle screenRect = monitors[monitorIndex].Bounds; // position in desktop
			Rectangle picBoxRect = new Rectangle(new Point(0, 0), previewRect.Size); // target rectangle for entire dektop
			Rectangle previewScreen = WallpaperCompositor.CalcDestRect(monitors.Bounds, picBoxRect, screenRect);

			// TODO: look into this!
			previewScreen = new Rectangle(previewScreen.Left, previewScreen.Top, previewScreen.Width - 1, previewScreen.Height - 1);

			// draw border around screen
			Pen borderPen1 = Pens.Black;
			Pen borderPen2 = Pens.Black; // Pens.White;
			Brush textBrush = Brushes.Black; //  Brushes.White;
			//if (screenIndex == _selectedScreenIndex)
			//{
			//	borderPen1 = Pens.Yellow;
			//	borderPen2 = Pens.Yellow;
			//	textBrush = Brushes.Yellow;
			//}

			// leave outermost pixels of image visible
			previewScreen.Inflate(-1, -1);
			g.DrawRectangle(borderPen1, previewScreen);
			previewScreen.Inflate(-1, -1);
			g.DrawRectangle(borderPen2, previewScreen);

			// display the screen name centered in the screen
			using (Font font = new Font("Arial", 24, FontStyle.Bold, GraphicsUnit.Point))
			{
				StringFormat stringFormat = new StringFormat();
				stringFormat.Alignment = StringAlignment.Center;
				stringFormat.LineAlignment = StringAlignment.Center;
				g.DrawString(screenName, font, textBrush, previewScreen, stringFormat);
			}

		}

		private void radioButtonLeftRight_CheckedChanged(object sender, EventArgs e)
		{
			UpdateOrdering(sender);
		}

		private void radioButtonDotNet_CheckedChanged(object sender, EventArgs e)
		{
			UpdateOrdering(sender);
		}

		void UpdateOrdering(object sender)
		{
			RadioButton rb = sender as RadioButton;
			if (rb != null)
			{
				if (rb.Checked)
				{
					UpdateOrdering();
				}
			}
		}

		// call to update monitor ordering
		void UpdateOrdering()
		{
			if (radioButtonDotNet.Checked)
			{
				_generalModule.MonitorOrder = Monitor.EMonitorOrder.DotNet;
			}
			else //if (radioButtonLeftRight.Checked)
			{
				// anything else
				_generalModule.MonitorOrder = Monitor.EMonitorOrder.LeftRight;
			}
		}
	}
}
