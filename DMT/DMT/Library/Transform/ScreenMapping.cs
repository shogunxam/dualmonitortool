#region copyright
// This file is part of Dual Monitor Tools which is a set of tools to assist
// users with multiple monitor setups.
// Copyright (C) 2010-2015  Gerald Evans
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
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace DMT.Library.Transform
{
	/// <summary>
	/// Provides a mapping between a rectangle within a source image and a rectangle on a single screen.
	/// By adjusting this mapping it is possible to show different parts of the source image.
	/// </summary>
	public class ScreenMapping
	{
		private Image sourceImage;
		/// <summary>
		/// The source image that we want to display
		/// </summary>
		public Image SourceImage
		{
			get { return sourceImage; }
			set { sourceImage = value; }
		}

		private Rectangle sourceRect;
		/// <summary>
		/// The rectangle within the source image that we will be displaying
		/// </summary>
		public Rectangle SourceRect
		{
			get { return sourceRect; }
			set { sourceRect = value; }
		}

		private Rectangle destRect;
		/// <summary>
		/// The rectangle on the desktop that will display the SourceRect region of the source image
		/// </summary>
		public Rectangle DestRect
		{
			get { return destRect; }
			set { destRect = value; }
		}

		private Rectangle screenRect;
		/// <summary>
		/// The bounding rectangle for the screen
		/// </summary>
		public Rectangle ScreenRect
		{
			get { return screenRect; }
			set { screenRect = value; }
		}

		private bool primary;
		/// <summary>
		/// Indicates if this is the primary monitor
		/// </summary>
		public bool Primary
		{
			get { return primary; }
			set { primary = value; }
		}

		private Scaler scaleX = null;
		private Scaler scaleY = null;
	
		/// <summary>
		/// Constructs an empty screen mapping for a particular screen
		/// </summary>
		/// <param name="screenRect">Bounding rectangle of the screen</param>
		/// <param name="primary">Indicates if this is the primary monitor</param>
		public ScreenMapping(Rectangle screenRect, bool primary)
		{
			this.sourceImage = null;
			this.sourceRect = Rectangle.Empty;
			this.destRect = Rectangle.Empty;
			this.ScreenRect = screenRect;
			this.primary = primary;
		}

		/// <summary>
		/// Generates an initial mapping which would display the whole of the image 
		/// on the specified virtual rectangle.
		/// </summary>
		/// <param name="image">Image to display</param>
		/// <param name="virtualDestRect">The rectangle that the whole of the image maps to</param>
		public void GenerateMapping(Image image, Rectangle virtualDestRect)
		{
			this.sourceImage = image;

			Rectangle imageRect = new Rectangle(new Point(0, 0), image.Size);

			// if we are adding bars then we need to reduce screenRect accordingly
			destRect = Rectangle.Intersect(screenRect, virtualDestRect);

			// imageRect maps to virtualDestRect,
			GenerateScalers(imageRect, virtualDestRect);

			// so we need to workout what would map to screenRect
			CalcSourceRect();
		}

		/// <summary>
		/// Displace the image on the screen by the specified number of pixels on the X axis
		/// </summary>
		/// <param name="delta">Displacement, a positive value means move the image to the right</param>
		public void DisplaceX(int delta)
		{
			if (scaleX != null)
			{
				scaleX.Displace(delta);

				// now need to recalculate sourceRect and destRect
				RecalcSrcAndDest();
			}
		}

		/// <summary>
		/// Displace the image on the screen by the specified number of pixels on the Y axis
		/// </summary>
		/// <param name="delta">Displacement, a positive value means move the image down</param>
		public void DisplaceY(int delta)
		{
			if (scaleY != null)
			{
				scaleY.Displace(delta);

				// now need to recalculate sourceRect and destRect
				RecalcSrcAndDest();
			}
		}

		public void Zoom(Point center, double factor)
		{
			if (scaleX != null && scaleY != null)
			{
				// adjust the scaling
				scaleX.Zoom(center.X, factor);
				scaleY.Zoom(center.Y, factor);

				// now update the mapping based on the new scaling
				RecalcSrcAndDest();
			}
		}

		private void RecalcSrcAndDest()
		{
			// we start with the destination which is the screen rect
			int destLeft = screenRect.Left;
			int destRight = screenRect.Right;
			int destTop = screenRect.Top;
			int destBottom = screenRect.Bottom;

			// we now determine what src rect is required to fill the dest rect
			int srcLeft = scaleX.SrcFromDest(destLeft);
			int srcRight = scaleX.SrcFromDest(destRight);
			int srcTop = scaleY.SrcFromDest(destTop);
			int srcBottom = scaleY.SrcFromDest(destBottom);

			// we now need to clip the src rect to make sure it is within the image
			// if any of the src image edges are clipped, then we must make sure we
			// also adjust the corresponding dest edge
			if (srcLeft < 0)
			{
				srcLeft = 0;
				destLeft = scaleX.DestFromSrc(srcLeft);
			}
			if (srcRight > ImageRect.Width)
			{
				srcRight = ImageRect.Width;
				destRight = scaleX.DestFromSrc(srcRight);
			}
			if (srcTop < 0)
			{
				srcTop = 0;
				destTop = scaleY.DestFromSrc(srcTop);
			}
			if (srcBottom > ImageRect.Width)
			{
				srcBottom = ImageRect.Height;
				destBottom = scaleY.DestFromSrc(srcBottom);
			}

			// Note: this is ok if width or height <= 0
			sourceRect = new Rectangle(srcLeft, srcTop, srcRight - srcLeft, srcBottom - srcTop);
			destRect = new Rectangle(destLeft, destTop, destRight - destLeft, destBottom - destTop);
		}

		private void GenerateScalers(Rectangle imageRect, Rectangle virtualDestRect)
		{
			scaleX = new Scaler(imageRect.Left, imageRect.Right, virtualDestRect.Left, virtualDestRect.Right);
			scaleY = new Scaler(imageRect.Top, imageRect.Bottom, virtualDestRect.Top, virtualDestRect.Bottom);
		}

		private void CalcSourceRect()
		{
			int left = scaleX.SrcFromDest(destRect.Left);
			int right = scaleX.SrcFromDest(destRect.Right);
			int top = scaleY.SrcFromDest(destRect.Top);
			int bottom = scaleY.SrcFromDest(destRect.Bottom);

			sourceRect = new Rectangle(left, top, right - left, bottom - top);
		}

		private Rectangle ImageRect
		{
			get { return new Rectangle(new Point(0, 0), sourceImage.Size); }
		}
	}
}
