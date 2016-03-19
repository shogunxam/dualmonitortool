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

namespace DMT.Modules.WallpaperChanger
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel;
	using System.Data;
	using System.Diagnostics;
	using System.Drawing;
	using System.Linq;
	using System.Text;
	using System.Windows.Forms;

	using DMT.Library.Wallpaper;
	using DMT.Library.WallpaperPlugin;
	using DMT.Resources;

	/// <summary>
	/// Options panel for the wallpaper changers properties
	/// </summary>
	partial class WallpaperChangerPropertiesOptionsPanel : UserControl
	{
		WallpaperChangerModule _wallpaperChangerModule;

		int _selectedScreenIndex = 0;	// We show information for image on this zero based monitor

		// This is the centered rectangle within the picture box
		// that we will use to display a preview of the wallpaper
		Rectangle _previewRect;

		/// <summary>
		/// Initialises a new instance of the <see cref="WallpaperChangerPropertiesOptionsPanel" /> class.
		/// </summary>
		/// <param name="wallpaperChangerModule">The wallpaper changer module</param>
		public WallpaperChangerPropertiesOptionsPanel(WallpaperChangerModule wallpaperChangerModule)
		{
			_wallpaperChangerModule = wallpaperChangerModule;

			InitializeComponent();
		}

		/// <summary>
		/// This informs us that the time to the next change has changed
		/// </summary>
		/// <param name="nextChangeMsg">Message saying when next change is</param>
		/// <param name="foreColor">Foreground colour to use for message</param>
		public void ShowNextChange(string nextChangeMsg, Color foreColor)
		{
			////if (labelNextChange != null)
			////{
			////	labelNextChange.Text = nextChangeMsg;
			////	labelNextChange.ForeColor = foreColor;
			////}
		}

		/// <summary>
		/// Generate and show new wallpaper
		/// </summary>
		public void ShowNewWallpaper()
		{
			Image sourceImage = _wallpaperChangerModule.Desktop.CurrentWallpaperImage;
			IWallpaperCompositor compositor = _wallpaperChangerModule.Desktop.CurrentCompositor;
			if (sourceImage != null && compositor != null)
			{
				Size sourceSize = sourceImage.Size;
				Rectangle pictureBoxRect = new Rectangle(new Point(0, 0), picPreview.Size);
				_previewRect = WallpaperCompositor.UnderStretch(sourceSize, pictureBoxRect);

				Image preview = new Bitmap(_previewRect.Width, _previewRect.Height);

				using (Graphics g = Graphics.FromImage(preview))
				{
					g.DrawImage(sourceImage, 0, 0, preview.Width, preview.Height);

					// now indicate the positions of the monitors
					DisplayMonitors(compositor, g);
					ShowSourceImageInfo();

					// change label to indicate properties actually shown
					labelProperties.Text = WallpaperStrings.WallpaperPropertiesDescription;
				}

				// display preview
				if (picPreview.Image != null)
				{
					picPreview.Image.Dispose();
				}

				picPreview.Image = preview;
			}
		}

		void DisplayMonitors(IWallpaperCompositor compositor, Graphics g)
		{
			for (int screenIndex = 0; screenIndex < compositor.AllScreens.Count; screenIndex++)
			{
				string screenName = string.Format("{0}", screenIndex + 1);
				////if (compositor.AllScreens[screenIndex].Primary)
				////{
				////	screenName += "P";
				////}
				DisplayMonitor(compositor, g, compositor.AllScreens[screenIndex].ScreenRect, screenIndex, screenName);
			}
		}

		void DisplayMonitor(IWallpaperCompositor compositor, Graphics g, Rectangle screenRect, int screenIndex, string screenName)
		{
			////Rectangle previewRect = new Rectangle(new Point(0, 0), previewSize);

			// need to determine position of screen rect in the preview
			Rectangle picBoxRect = new Rectangle(new Point(0, 0), _previewRect.Size);
			Rectangle previewScreen = WallpaperCompositor.CalcDestRect(compositor.DesktopRect, picBoxRect, screenRect);

			// TODO: look into this!
			previewScreen = new Rectangle(previewScreen.Left, previewScreen.Top, previewScreen.Width - 1, previewScreen.Height - 1);

			// draw border around screen
			Pen borderPen1 = Pens.Black;
			Pen borderPen2 = Pens.White;
			Brush textBrush = Brushes.White;
			if (screenIndex == _selectedScreenIndex)
			{
				borderPen1 = Pens.Yellow;
				borderPen2 = Pens.Yellow;
				textBrush = Brushes.Yellow;
			}

			// leave outermost pixels of image visible
			previewScreen.Inflate(-1, -1);
			g.DrawRectangle(borderPen1, previewScreen);
			previewScreen.Inflate(-1, -1);
			g.DrawRectangle(borderPen2, previewScreen);

			// display the screen name centered in the screen
			using (Font font = new Font("Arial", 24, FontStyle.Bold, GraphicsUnit.Point))
			{
				g.DrawString(screenName, font, textBrush, previewScreen);
			}
		}

		void ShowSourceImageInfo()
		{
			ProviderImage providerImage = _wallpaperChangerModule.Desktop.GetProviderImage(_selectedScreenIndex);
			if (providerImage == null)
			{
				// remove any existing info
				ClearLink(linkLabelProvider);
				ClearLink(linkLabelSource);
				ClearLink(linkLabelPhotographer);
				ClearLink(linkLabelDetails);
			}
			else
			{
				ShowLink(linkLabelProvider, providerImage.Provider, providerImage.ProviderUrl);
				ShowLink(linkLabelSource, providerImage.Source, providerImage.SourceUrl);
				ShowLink(linkLabelPhotographer, providerImage.Photographer, providerImage.PhotographerUrl);
				ShowLink(linkLabelDetails, providerImage.MoreInfo, providerImage.MoreInfoUrl);
			}
		}

		void ShowLink(LinkLabel linkLabel, string text, string url)
		{
			linkLabel.Links.Clear();
			linkLabel.Text = text;
			if (!string.IsNullOrEmpty(url))
			{
				linkLabel.Links.Add(0, text.Length, url);
			}
		}

		void ClearLink(LinkLabel linkLabel)
		{
			linkLabel.Links.Clear();
			linkLabel.Text = string.Empty;
		}

		private void linkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			ProcessStartInfo startInfo = new ProcessStartInfo(e.Link.LinkData.ToString());
			Process.Start(startInfo);
		}

		private void WallpaperChangerPropertiesOptionsPanel_Load(object sender, EventArgs e)
		{
			ShowNewWallpaper();
		}

		private void picPreview_MouseClick(object sender, MouseEventArgs e)
		{
			// check if the area clicked belongs to one of the screens
			int screenIndex = PosnToScreen(e.X, e.Y);
			if (screenIndex >= 0)
			{
				if (screenIndex != _selectedScreenIndex)
				{
					_selectedScreenIndex = screenIndex;
					ShowNewWallpaper();
				}
			}
		}

		private int PosnToScreen(int x, int y)
		{
			int ret = -1;
			IWallpaperCompositor compositor = _wallpaperChangerModule.Desktop.CurrentCompositor;

			if (compositor != null)
			{
				if (_previewRect.Contains(x, y))
				{
					// now map this onto the virtual desktop
					Point desktopPoint = WallpaperCompositor.CalcDestPoint(_previewRect, compositor.DesktopRect, new Point(x, y));

					// now check each screen to see if it contains this point
					for (int screenIndex = 0; screenIndex < compositor.AllScreens.Count; screenIndex++)
					{
						if (compositor.AllScreens[screenIndex].ScreenRect.Contains(desktopPoint.X, desktopPoint.Y))
						{
							ret = screenIndex;
							break;
						}
					}
				}
			}

			return ret;
		}
	}
}
