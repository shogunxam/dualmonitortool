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
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace DualWallpaper
{
	class ScreenMapping
	{
		private Image sourceImage;
		public Image SourceImage
		{
			get { return sourceImage; }
			set { sourceImage = value; }
		}

		private Rectangle sourceRect;
		public Rectangle SourceRect
		{
			get { return sourceRect; }
			set { sourceRect = value; }
		}

		private Rectangle destRect;
		public Rectangle DestRect
		{
			get { return destRect; }
			set { destRect = value; }
		}

		private Rectangle screenRect;
		public Rectangle ScreenRect
		{
			get { return screenRect; }
			set { screenRect = value; }
		}

		private bool primary;
		public bool Primary
		{
			get { return primary; }
			set { primary = value; }
		}

		private Scaler scaleX = null;
		private Scaler scaleY = null;
	

		public ScreenMapping(Rectangle screenRect, bool primary)
		{
			this.sourceImage = null;
			this.sourceRect = Rectangle.Empty;
			this.destRect = Rectangle.Empty;
			this.ScreenRect = screenRect;
			this.primary = primary;
		}

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

		public void DisplaceX(int delta)
		{
			if (scaleX != null)
			{
				scaleX.Displace(delta);

				// now need to recalculate sourceRect and destRect

				// we start with the screen rect and work out the dest rect for that with the new displacement
				int srcLeft = scaleX.SrcFromDest(screenRect.Left);
				int srcRight = scaleX.SrcFromDest(screenRect.Right);

				Rectangle newSrcRect = new Rectangle(srcLeft, sourceRect.Top, srcRight - srcLeft, sourceRect.Height);
				// clip to make sure it is within the image
				//sourceRect = Rectangle.Intersect(newSrcRect, ImageRect);
				sourceRect = IntersectX(newSrcRect, ImageRect);

				// now translate this back to the dest rect
				int destLeft = scaleX.DestFromSrc(sourceRect.Left);
				int destRight = scaleX.DestFromSrc(sourceRect.Right);

				Rectangle newDestRect = new Rectangle(destLeft, destRect.Top, destRight - destLeft, destRect.Height);
				destRect = newDestRect;
			}
		}

		private Rectangle IntersectX(Rectangle rect1, Rectangle rect2)
		{
			// intersects the 2 rectangles in the x direction 
			// but leave the y co-ords of rect1
			Rectangle rect = Rectangle.Intersect(rect1, rect2);
			return new Rectangle(rect.Left, rect1.Top, rect.Width, rect1.Height);
		}

		public void DisplaceY(int delta)
		{
			if (scaleY != null)
			{
				scaleY.Displace(delta);

				// now need to recalculate sourceRect and destRect

				// we start with the screen rect and work out the dest rect for that with the new displacement
				int srcTop = scaleY.SrcFromDest(screenRect.Top);
				int srcBottom = scaleY.SrcFromDest(screenRect.Bottom);

				Rectangle newSrcRect = new Rectangle(sourceRect.Left, srcTop, sourceRect.Width, srcBottom - srcTop);
				// clip to make sure it is within the image
				//sourceRect = Rectangle.Intersect(newSrcRect, ImageRect);
				sourceRect = IntersectY(newSrcRect, ImageRect);

				// now translate this back to the dest rect
				int destTop = scaleY.DestFromSrc(sourceRect.Top);
				int destBottom = scaleY.DestFromSrc(sourceRect.Bottom);

				Rectangle newDestRect = new Rectangle(destRect.Left, destTop, destRect.Width, destBottom - destTop);
				destRect = newDestRect;
			}
		}

		private Rectangle IntersectY(Rectangle rect1, Rectangle rect2)
		{
			// intersects the 2 rectangles in the y direction 
			// but leave the x co-ords of rect1
			Rectangle rect = Rectangle.Intersect(rect1, rect2);
			return new Rectangle(rect1.Left, rect.Top, rect1.Width, rect.Height);
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
