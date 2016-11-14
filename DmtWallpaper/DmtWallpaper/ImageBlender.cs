using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DmtWallpaper
{
	public partial class ImageBlender : Control
	{
		float _blend;
		Image _oldImage = null;
		Image _newImage = null;

		public float Blend 
		{ 
			get
			{
				return _blend;
			} 
			set
			{
				_blend = value;
				Invalidate();
			}
		}


		public ImageBlender()
		{
			InitializeComponent();

			SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer, true);
		}

		public void SetImages(Image oldImage, Image newImage, float initialBlend)
		{
			_oldImage = oldImage;
			_newImage = newImage;
			_blend = initialBlend;
			System.Diagnostics.Debug.WriteLine("{0} Initial _blend: {1}", DateTime.Now, _blend);

			Invalidate();
		}

		protected override void OnPaint(PaintEventArgs pe)
		{
			Rectangle rect = new Rectangle(0, 0, Width, Height);
			ColorMatrix colorMatrix = new ColorMatrix();
			ImageAttributes imageAttributes = new ImageAttributes();

			//System.Diagnostics.Debug.WriteLine("{0} _blend: {1}", DateTime.Now, _blend);
			System.Diagnostics.Stopwatch stopwatch = System.Diagnostics.Stopwatch.StartNew();

			if (_blend < 1.0F && _oldImage != null)
			{
				colorMatrix.Matrix33 = 1.0F - _blend;
				imageAttributes.SetColorMatrix(colorMatrix);
				pe.Graphics.DrawImage(_oldImage, rect, 0, 0, _oldImage.Width, _oldImage.Height, GraphicsUnit.Pixel, imageAttributes);
			}

			if (_blend > 0.0F && _newImage != null)
			{
				colorMatrix.Matrix33 = _blend;
				imageAttributes.SetColorMatrix(colorMatrix);
				pe.Graphics.DrawImage(_newImage, rect, 0, 0, _newImage.Width, _newImage.Height, GraphicsUnit.Pixel, imageAttributes);
			}

			System.Diagnostics.Debug.WriteLine("Paint took {0}ms", stopwatch.ElapsedMilliseconds);
			base.OnPaint(pe);
		}

		//void PaintImage(PaintEventArgs pe, Image image, Rectangle destRect, ImageAttributes imageAttributes)
		//{
		//	pe.Graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, imageAttributes);
		//}
	}
}
