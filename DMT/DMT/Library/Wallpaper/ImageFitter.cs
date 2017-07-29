using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;

namespace DMT.Library.Wallpaper
{
	static public class ImageFitter
	{
		enum ScaleDimension {  WidthPrimary, HeightPrimary, Equal};

		public static StretchType.Fit UpgradeFit(StretchType.Fit oldFit)
		{
			if (oldFit.HasFlag(StretchType.Fit.NewFit))
			{
				// already new fit - no need to upgrade
				return oldFit;
			}
			else
			{
				if (oldFit == StretchType.Fit.UnderStretch)
				{
					return StretchType.Fit.NewFit | StretchType.Fit.MaintainAspectRatio | StretchType.Fit.AllowEnlarge | StretchType.Fit.AllowShrink;
				}
				else if (oldFit == StretchType.Fit.Center)
				{
					return StretchType.Fit.NewFit | StretchType.Fit.MaintainAspectRatio;
				}
				else if (oldFit == StretchType.Fit.StretchToFit)
				{
					return StretchType.Fit.NewFit | StretchType.Fit.AllowEnlarge | StretchType.Fit.AllowShrink;
				}
				else // if (oldFit == StretchType.Fit.OverStretch)
				{
					// overstretch and the default case
					return StretchType.Fit.NewFit | StretchType.Fit.MaintainAspectRatio | StretchType.Fit.AllowEnlarge | StretchType.Fit.AllowShrink | StretchType.Fit.ClipImage;
				}
			}
		}

		public static Rectangle OldFitImage(Size imageSize, StretchType.Fit fit, Rectangle targetRect)
		{
			Rectangle virtualDestRect = Rectangle.Empty;

			switch (fit)
			{
				case StretchType.Fit.Center:
					virtualDestRect = Center(imageSize, targetRect);
					break;

				case StretchType.Fit.StretchToFit:
					virtualDestRect = targetRect;
					break;

				case StretchType.Fit.UnderStretch:
					virtualDestRect = UnderStretch(imageSize, targetRect);
					break;

				case StretchType.Fit.OverStretch:
					virtualDestRect = OverStretch(imageSize, targetRect);
					break;

				default:
					//Debug.Fail("Unknown type: " + fit.ToString());
					//virtualDestRect = targetRect;
					break;
			}

			return virtualDestRect;
		}

		public static Rectangle FitImage(Size imageSize, StretchType.Fit fit, Rectangle targetRect)
		{
			Rectangle virtualDestRect = Rectangle.Empty;

			Size targetSize = targetRect.Size;
			bool clip = fit.HasFlag(StretchType.Fit.ClipImage);


			if (fit.HasFlag(StretchType.Fit.MaintainAspectRatio))
			{

				if (imageSize.Width == targetSize.Width && imageSize.Height == targetSize.Height)
				{
					// exact fit
					virtualDestRect = targetRect;
				}
				else if (imageSize.Width >= targetSize.Width && imageSize.Height >= targetSize.Height)
				{
					// image is larger
					if (fit.HasFlag(StretchType.Fit.AllowShrink))
					{
						virtualDestRect = Stretch(imageSize, clip, targetRect);
					}
					else
					{
						virtualDestRect = Center(imageSize, targetRect);
					}
				}
				else if (imageSize.Width <= targetSize.Width && imageSize.Height <= targetSize.Height)
				{
					//image is smaller
					if (fit.HasFlag(StretchType.Fit.AllowEnlarge))
					{
						virtualDestRect = Stretch(imageSize, clip, targetRect);
					}
					else
					{
						virtualDestRect = Center(imageSize, targetRect);
					}
				}
				else
				{
					// image must be smaller than target in one dimension, but larger in other dimension

					// default case if we can't scale
					virtualDestRect = Center(imageSize, targetRect);

					ScaleDimension scaleDimension = CompareScaleFactor(imageSize, targetRect);

					bool useWidth = (scaleDimension == ScaleDimension.WidthPrimary);
					if (!clip)
					{
						useWidth = !useWidth;
					}

					if (useWidth)
					{
						if (imageSize.Width < targetRect.Width && fit.HasFlag(StretchType.Fit.AllowEnlarge))
						{
							virtualDestRect = Stretch(imageSize, clip, targetRect);
						}
						else if (imageSize.Width > targetRect.Width && fit.HasFlag(StretchType.Fit.AllowShrink))
						{
							virtualDestRect = Stretch(imageSize, clip, targetRect);
						}
					}
					else
					{
						if (imageSize.Height < targetRect.Height && fit.HasFlag(StretchType.Fit.AllowEnlarge))
						{
							virtualDestRect = Stretch(imageSize, clip, targetRect);
						}
						else if (imageSize.Height > targetRect.Height && fit.HasFlag(StretchType.Fit.AllowShrink))
						{
							virtualDestRect = Stretch(imageSize, clip, targetRect);
						}
					}
				}
			}
			else
			{
				// TODO handle cases where we don't care about aspect ratio 
				// for now, just return target

				virtualDestRect = Center(imageSize, targetRect);

				int left = virtualDestRect.Left;
				int top = virtualDestRect.Top;
				int width = virtualDestRect.Width;
				int height = virtualDestRect.Height;

				if (fit.HasFlag(StretchType.Fit.AllowEnlarge))
				{
					if (targetRect.Width > imageSize.Width)
					{
						width = targetRect.Width;
						left = 0;
					}
					if (targetRect.Height > imageSize.Height)
					{
						height = targetRect.Height;
						top = 0;
					}
				}

				if (fit.HasFlag(StretchType.Fit.AllowShrink))
				{
					if (targetRect.Width < imageSize.Width)
					{
						width = targetRect.Width;
						left = 0;
					}
					if (targetRect.Height < imageSize.Height)
					{
						height = targetRect.Height;
						top = 0;
					}
				}

				virtualDestRect = new Rectangle(left, top, width, height);
			}

			return virtualDestRect;
		}

		static Rectangle Stretch(Size imageSize, bool clip, Rectangle targetRect)
		{
			if (clip)
			{
				return OverStretch(imageSize, targetRect);
			}
			else
			{
				return UnderStretch(imageSize, targetRect);
			}
		}


		/// <summary>
		/// Gets the rectangle when a rectangle of a given size in centered in another rectangle
		/// </summary>
		/// <param name="sourceSize">Size of rectangle you want centered</param>
		/// <param name="targetRect">Destination area that you want it centered in</param>
		/// <returns>The centered rectangle</returns>
		public static Rectangle Center(Size sourceSize, Rectangle targetRect)
		{
			Rectangle rect;

			// center of image gets mapped to center of destination
			// so work out the movement involved in doing this
			// remember image is at (0, 0)
			int shiftX = targetRect.Left + targetRect.Width / 2 - sourceSize.Width / 2;
			int shiftY = targetRect.Top + targetRect.Height / 2 - sourceSize.Height / 2;

			rect = new Rectangle(shiftX, shiftY, sourceSize.Width, sourceSize.Height);

			return rect;
		}


		/// <summary>
		/// Determines the destination rectangle to use to maintain the source aspect ratio
		/// and to fill the destination as much as possible without clipping.
		/// This may result in the need to add bars top and bottom, or left and right
		/// to keep the aspect ratio constant.
		/// </summary>
		/// <param name="sourceSize">Size of source image</param>
		/// <param name="targetRect">Area we have available to display the image in</param>
		/// <returns>rectangle for the under stretched image</returns>
		public static Rectangle UnderStretch(Size sourceSize, Rectangle targetRect)
		{
			Rectangle rect;

			// check if we need to add either vertical or horizontal bars 
			// either side of the image to keep the source aspect ratio
			int widthFactor = targetRect.Width * sourceSize.Height;
			int heightFactor = targetRect.Height * sourceSize.Width;
			if (widthFactor > heightFactor)
			{
				// need to add vertical bars
				int newWidth = (sourceSize.Width * targetRect.Height) / sourceSize.Height;
				int barSize = (targetRect.Width - newWidth) / 2;
				rect = new Rectangle(targetRect.Left + barSize, targetRect.Top, newWidth, targetRect.Height);
			}
			else if (heightFactor > widthFactor)
			{
				// need to add horizontal bars
				int newHeight = (sourceSize.Height * targetRect.Width) / sourceSize.Width;
				int barSize = (targetRect.Height - newHeight) / 2;
				rect = new Rectangle(targetRect.Left, targetRect.Top + barSize, targetRect.Width, newHeight);
			}
			else
			{
				// perfect type with no need to add bars
				rect = targetRect;
			}

			return rect;
		}
		/// <summary>
		/// Determines the destination rectangle to use to maintain the source aspect ratio
		/// and to fill the destination entirely, but keeping the clipping to a minimum.
		/// </summary>
		/// <param name="sourceSize">Size of source image</param>
		/// <param name="targetRect">Area we have available to display the image in</param>
		/// <returns>rectangle for the over stretched image</returns>
		public static Rectangle OverStretch(Size sourceSize, Rectangle targetRect)
		{
			Rectangle rect;

			// check which sides we need to clip 
			// to keep the source aspect ratio
			int widthFactor = targetRect.Width * sourceSize.Height;
			int heightFactor = targetRect.Height * sourceSize.Width;
			if (widthFactor > heightFactor)
			{
				// need to clip top and bottom
				int newHeight = (sourceSize.Height * targetRect.Width) / sourceSize.Width;
				int clipSize = (newHeight - targetRect.Height) / 2;
				rect = new Rectangle(targetRect.Left, targetRect.Top - clipSize, targetRect.Width, newHeight);
			}
			else if (heightFactor > widthFactor)
			{
				// need to clip srcLeft and srcRight
				int newWidth = (sourceSize.Width * targetRect.Height) / sourceSize.Height;
				int clipSize = (newWidth - targetRect.Width) / 2;
				rect = new Rectangle(targetRect.Left - clipSize, targetRect.Top, newWidth, targetRect.Height);
			}
			else
			{
				// perfect type with no need to add bars
				rect = targetRect;
			}

			return rect;
		}

		//  -1 width factor is largest, +1 => height factor is largest
		static ScaleDimension CompareScaleFactor(Size sourceSize, Rectangle targetRect)
		{
			int widthFactor = targetRect.Width * sourceSize.Height;
			int heightFactor = targetRect.Height * sourceSize.Width;
			if (widthFactor > heightFactor)
			{
				return ScaleDimension.WidthPrimary;
			}
			else if (heightFactor > widthFactor)
			{
				return ScaleDimension.HeightPrimary;
			}
			else
			{
				return ScaleDimension.Equal;
			}
		}
	}
}
