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

namespace DMT.Library.Transform
{
	using System;
	using System.Collections.Generic;
	using System.Drawing;
	using System.Text;
	using System.Windows.Forms;

	/// <summary>
	/// Provides a mapping between a rectangle within a source image and a rectangle on a single screen.
	/// By adjusting this mapping it is possible to show different parts of the source image.
	/// </summary>
	public class ScreenMapping
	{
		Scaler scaleX = null;
		Scaler scaleY = null;

		/// <summary>
		/// Initialises a new instance of the <see cref="ScreenMapping" /> class.
		/// This is an empty screen mapping for a particular screen.
		/// </summary>
		/// <param name="screenRect">Bounding rectangle of the screen</param>
		/// <param name="primary">Indicates if this is the primary monitor</param>
		public ScreenMapping(Rectangle screenRect, bool primary)
		{
			SourceImage = null;
			SourceRect = Rectangle.Empty;
			DestRect = Rectangle.Empty;
			ScreenRect = screenRect;
			Primary = primary;
		}

		/// <summary>
		/// Gets or sets the source image that we want to display
		/// </summary>
		public Image SourceImage { get; set; }

		/// <summary>
		/// Gets or sets the rectangle within the source image that we will be displaying
		/// </summary>
		public Rectangle SourceRect { get; set; }

		/// <summary>
		/// Gets or sets the rectangle on the desktop that will display the source rectangle region of the source image
		/// </summary>
		public Rectangle DestRect { get; set; }

		/// <summary>
		/// Gets or sets the bounding rectangle for the screen
		/// </summary>
		public Rectangle ScreenRect { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this is the primary monitor
		/// </summary>
		public bool Primary { get; set; }

		Rectangle ImageRect
		{
			get 
			{ 
				return new Rectangle(new Point(0, 0), SourceImage.Size); 
			}
		}

		/// <summary>
		/// Generates an initial mapping which would display the whole of the image 
		/// on the specified virtual rectangle.
		/// </summary>
		/// <param name="image">Image to display</param>
		/// <param name="virtualDestRect">The rectangle that the whole of the image maps to</param>
		public void GenerateMapping(Image image, Rectangle virtualDestRect)
		{
			SourceImage = image;

			Rectangle imageRect = new Rectangle(new Point(0, 0), image.Size);

			// if we are adding bars then we need to reduce screenRect accordingly
			DestRect = Rectangle.Intersect(ScreenRect, virtualDestRect);

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

		/// <summary>
		/// Zoom the mapping
		/// </summary>
		/// <param name="center">Point to zoom around</param>
		/// <param name="factor">Zoom factor with 1.0 being a no op</param>
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

		void RecalcSrcAndDest()
		{
			// we start with the destination which is the screen rect
			int destLeft = ScreenRect.Left;
			int destRight = ScreenRect.Right;
			int destTop = ScreenRect.Top;
			int destBottom = ScreenRect.Bottom;

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
			SourceRect = new Rectangle(srcLeft, srcTop, srcRight - srcLeft, srcBottom - srcTop);
			DestRect = new Rectangle(destLeft, destTop, destRight - destLeft, destBottom - destTop);
		}

		void GenerateScalers(Rectangle imageRect, Rectangle virtualDestRect)
		{
			scaleX = new Scaler(imageRect.Left, imageRect.Right, virtualDestRect.Left, virtualDestRect.Right);
			scaleY = new Scaler(imageRect.Top, imageRect.Bottom, virtualDestRect.Top, virtualDestRect.Bottom);
		}

		void CalcSourceRect()
		{
			int left = scaleX.SrcFromDest(DestRect.Left);
			int right = scaleX.SrcFromDest(DestRect.Right);
			int top = scaleY.SrcFromDest(DestRect.Top);
			int bottom = scaleY.SrcFromDest(DestRect.Bottom);

			SourceRect = new Rectangle(left, top, right - left, bottom - top);
		}
	}
}
