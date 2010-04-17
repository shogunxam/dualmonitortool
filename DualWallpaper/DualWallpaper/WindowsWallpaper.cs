using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;

using Microsoft.Win32;


namespace DualWallpaper
{
	/// <summary>
	/// This handles Windows specific aspects of wallpaper.
	/// This includes hanling the case where you have a monitor to the left
	/// or above the primary monitor, as Windows requires that (0,0) in
	/// the wallpaper corresponds to (0,0) on your primary monitor.
	/// </summary>
	class WindowsWallpaper
	{
		private Image srcImage;
		private Rectangle virtualDesktop;

		/// <summary>
		/// ctor which takes the virtual desktop rectangle and
		/// an image which is laid out corresponding to the virtual desktop,
		/// so the TLHC of the image corresponds to the TLHC of the virtual desktop
		/// which may not be the same as the TLHC of the primary monitor.
		/// </summary>
		/// <param name="srcImage">image</param>
		/// <param name="virtualDesktop">virtual desktop rectangle</param>
		public WindowsWallpaper(Image srcImage, Rectangle virtualDesktop)
		{
			Debug.Assert(srcImage.Size == virtualDesktop.Size);
			this.srcImage = srcImage;
			this.virtualDesktop = virtualDesktop;
		}

		/// <summary>
		/// Sets the Windows wallpaper.
		/// This will create a new image if the primary monitor
		/// is not both the leftmost and topmost monitor.
		/// </summary>
		public void SetWallpaper()
		{
			bool wrapped;
			Image image = WrapImage(out wrapped);

			SetWallpaper(image);

			if (wrapped)
			{
				image.Dispose();
			}
		}

		/// <summary>
		/// Saves the wallpaper to a file in a format usable by most? 
		/// automatic screen changers.
		/// This will create a new image if the primary monitor
		/// is not both the leftmost and topmost monitor.
		/// <param name="fnm">Filename to save the wallpaper too</param>
		public void SaveWallpaper(string fnm)
		{
			bool wrapped;
			Image image = WrapImage(out wrapped);

			SaveWallpaper(image, fnm);

			if (wrapped)
			{
				image.Dispose();
			}
		}

		private void SetWallpaper(Image wallpaper)
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

		private void SaveWallpaper(Image wallpaper, string fnm)
		{
			wallpaper.Save(fnm, System.Drawing.Imaging.ImageFormat.Bmp);
		}

		private Image WrapImage(out bool wrapped)
		{
			if (virtualDesktop.Left < 0 || virtualDesktop.Top < 0)
			{
				// must wrap image
				// so that the four quadrants
				// ab
				// cd
				// where d would be the primary monitor to
				// dc
				// ba
				wrapped = true;
				Image image = new Bitmap(srcImage.Width, srcImage.Height);
				int xWrap = -virtualDesktop.Left;
				int xNotWrap = srcImage.Width - xWrap;
				int yWrap = -virtualDesktop.Top;
				int yNotWrap = srcImage.Height - yWrap;

				using (Graphics g = Graphics.FromImage(image))
				{

					// quadrant a
					if (xWrap > 0 && yWrap > 0)
					{
						g.DrawImage(srcImage,
									new Rectangle(xNotWrap, yNotWrap, xWrap, yWrap),
									new Rectangle(0, 0, xWrap, yWrap),
									GraphicsUnit.Pixel);
					}
					// quadrant b
					if (yWrap > 0 && xNotWrap > 0)
					{
						g.DrawImage(srcImage,
									new Rectangle(0, yNotWrap, xNotWrap, yWrap),
									new Rectangle(xWrap, 0, xNotWrap, yWrap),
									GraphicsUnit.Pixel);
					}
					// quadrant c
					if (xWrap > 0 && yNotWrap > 0)
					{
						g.DrawImage(srcImage,
									new Rectangle(xNotWrap, 0, xWrap, yNotWrap),
									new Rectangle(0, yWrap, xWrap, yNotWrap),
									GraphicsUnit.Pixel);
					}
					// quadrant d
					Debug.Assert(xNotWrap > 0 && yNotWrap > 0);
					if (xNotWrap > 0 && yNotWrap > 0)
					{
						g.DrawImage(srcImage,
									new Rectangle(0, 0, xNotWrap, yNotWrap),
									new Rectangle(xWrap, yWrap, xNotWrap, yNotWrap),
									GraphicsUnit.Pixel);
					}
				}
				wrapped = true;
				return image;
			}
			else
			{
				// can use original src image
				wrapped = false;
				return srcImage;
			}
		}
	}
}
