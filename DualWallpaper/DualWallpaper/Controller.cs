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
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DualWallpaper
{
	class Controller
	{
		//public enum Fit
		//{
		//    StretchToFit,	// stretches both X and Y to type exactly
		//    OverStretch,	// stretch maintaining aspect ratio, clipping if needed
		//    UnderStretch	// stretch maintaining aspect ratio, adding bars if needed
		//};

		private List<ScreenMapping> allScreens = new List<ScreenMapping>();
		public List<ScreenMapping> AllScreens
		{
			get { return allScreens; }
		}

		private List<ScreenMapping> activeScreens = new List<ScreenMapping>();

		private Rectangle desktopRect;
		public Rectangle DesktopRect
		{
			get { return desktopRect; }
		}
	

		public Controller()
		{
			Init();
		}

		public void SetActiveScreens(List<int> screenIndexes)
		{
			activeScreens.Clear();
			foreach (int screenIndex in screenIndexes)
			{
				Debug.Assert(screenIndex >= 0 && screenIndex < allScreens.Count);
				activeScreens.Add(allScreens[screenIndex]);
			}
		}

		public void AddImage(Image image, Stretch.Fit fit)
		{
			Debug.Assert(activeScreens.Count > 0);
			GenerateMappings(image, fit);
		}

		public Image CreateImage()
		{
			Bitmap image = new Bitmap(desktopRect.Width, desktopRect.Height);

			using (Graphics g = Graphics.FromImage(image))
			{
				g.Clear(Color.Black);

				foreach (ScreenMapping screenMapping in allScreens)
				{
					if (screenMapping.SourceImage != null)
					{
						g.DrawImage(screenMapping.SourceImage,
									screenMapping.DestRect,
									screenMapping.SourceRect,
									GraphicsUnit.Pixel);
					}
				}
			}

			return image;
		}

		public void MoveActiveScreens(int deltaX, int deltaY)
		{
			foreach (ScreenMapping screenMapping in activeScreens)
			{
				if (deltaX != 0)
				{
					screenMapping.DisplaceX(deltaX);
				}
				if (deltaY != 0)
				{
					screenMapping.DisplaceY(deltaY);
				}
			}
		}

		private void GenerateMappings(Image image, Stretch.Fit fit)
		{
			Debug.Assert(activeScreens.Count > 0);
			Rectangle boundingRect = GetBoundingRect(activeScreens);

			Rectangle imageRect = new Rectangle(new Point(0, 0), image.Size);
			Rectangle virtualDestRect = GetvirtualDestRect(image, fit, boundingRect);

			// imageRect gets mapped to virtualDestRect
			// now need to work out the mappings for the individual screens
			foreach (ScreenMapping screenMapping in activeScreens)
			{
				//GenerateMapping(screenMapping, imageRect, virtualDestRect);
				//screenMapping.SourceImage = image;
				screenMapping.GenerateMapping(image, virtualDestRect);
			}
		}

		public static Rectangle CalcDestRect(Rectangle src1, Rectangle dest1, Rectangle src2)
		{
			// TODO: use CalcDestPoint
			int left = ScaleDest(src1.Left, src1.Right, dest1.Left, dest1.Right, src2.Left);
			int right = ScaleDest(src1.Left, src1.Right, dest1.Left, dest1.Right, src2.Right);
			int top = ScaleDest(src1.Top, src1.Bottom, dest1.Top, dest1.Bottom, src2.Top);
			int bottom = ScaleDest(src1.Top, src1.Bottom, dest1.Top, dest1.Bottom, src2.Bottom);

			Rectangle dest2 = new Rectangle(left, top, right - left, bottom - top);

			return dest2;
		}

		public static Point CalcDestPoint(Rectangle src1, Rectangle dest1, Point src2)
		{
			int x = ScaleDest(src1.Left, src1.Right, dest1.Left, dest1.Right, src2.X);
			int y = ScaleDest(src1.Top, src1.Bottom, dest1.Top, dest1.Bottom, src2.Y);

			return new Point(x, y);
		}

		private static int ScaleDest(int s1, int s2, int d1, int d2, int s3)
		{
			int srcDelta = s2 - s1;

			// + destDelta / 2 to minimise rounding errors
			int d3 = d1 + ((s3 - s1) * (d2 - d1) + srcDelta / 2) / srcDelta;

			return d3;
		}

		private Rectangle GetvirtualDestRect(Image image, Stretch.Fit fit, Rectangle boundingRect)
		{
			Rectangle virtualDestRect = Rectangle.Empty;

			switch (fit)
			{
				case Stretch.Fit.StretchToFit:
					virtualDestRect = boundingRect;
					break;

				case Stretch.Fit.UnderStretch:
					virtualDestRect = UnderStretch(image.Size, boundingRect);
					break;

				case Stretch.Fit.OverStretch:
					virtualDestRect = OverStretch(image.Size, boundingRect);
					break;

				default:
					Debug.Fail("Unknown type: " + fit.ToString());
					break;
			}


			return virtualDestRect;
		}

		public static Rectangle UnderStretch(Size sourceSize, Rectangle destRect)
		{
			Rectangle rect;

			// check if we need to add either vertical or horizontal bars 
			// either side of the image to keep the source aspect ratio
			int widthFactor = destRect.Width * sourceSize.Height;
			int heightFactor = destRect.Height * sourceSize.Width;
			if (widthFactor > heightFactor)
			{
				// need to add vertical bars
				int newWidth = (sourceSize.Width * destRect.Height) / sourceSize.Height;
				int barSize = (destRect.Width - newWidth) / 2;
				rect = new Rectangle(destRect.Left + barSize,
									 destRect.Top,
									 newWidth,
									 destRect.Height);
			}
			else if (heightFactor > widthFactor)
			{
				// need to add horizontal bars
				int newHeight = (sourceSize.Height * destRect.Width) / sourceSize.Width;
				int barSize = (destRect.Height - newHeight) / 2;
				rect = new Rectangle(destRect.Left,
									 destRect.Top + barSize,
									 destRect.Width,
									 newHeight);
			}
			else
			{
				// perfect type with no need to add bars
				rect = destRect;
			}

			return rect;
		}

		public static Rectangle OverStretch(Size sourceSize, Rectangle destRect)
		{
			Rectangle rect;

			// check which sides we need to clip 
			// to keep the source aspect ratio
			int widthFactor = destRect.Width * sourceSize.Height;
			int heightFactor = destRect.Height * sourceSize.Width;
			if (widthFactor > heightFactor)
			{
				// need to clip top and bottom
				int newHeight = (sourceSize.Height * destRect.Width) / sourceSize.Width;
				int clipSize = (newHeight - destRect.Height) / 2;
				rect = new Rectangle(destRect.Left,
									 destRect.Top - clipSize,
									 destRect.Width,
									 newHeight);
			}
			else if (heightFactor > widthFactor)
			{
				// need to clip srcLeft and srcRight
				int newWidth = (sourceSize.Width * destRect.Height) / sourceSize.Height;
				int clipSize = (newWidth - destRect.Width) / 2;
				rect = new Rectangle(destRect.Left - clipSize,
									 destRect.Top,
									 newWidth,
									 destRect.Height);
			}
			else
			{
				// perfect type with no need to add bars
				rect = destRect;
			}

			return rect;
		}

		private void Init()
		{
			foreach (Screen screen in Screen.AllScreens)
			{
				ScreenMapping screenMapping = new ScreenMapping(screen.Bounds, screen.Primary);
				allScreens.Add(screenMapping);
			}

			desktopRect = GetBoundingRect(allScreens);
		}

		private Rectangle GetBoundingRect(List<ScreenMapping> screenMappingList)
		{
			Rectangle boundingRect = Rectangle.Empty;

			for (int screenIndex = 0; screenIndex < screenMappingList.Count; screenIndex++)
			{
				if (screenIndex == 0)
				{
					boundingRect = screenMappingList[screenIndex].ScreenRect;
				}
				else
				{
					boundingRect = Rectangle.Union(boundingRect, screenMappingList[screenIndex].ScreenRect);
				}
			}

			return boundingRect;
		}
	}
}
