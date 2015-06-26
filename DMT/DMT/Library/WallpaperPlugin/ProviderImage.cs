using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace DMT.Library.WallpaperPlugin
{
	public class ProviderImage : IDisposable
	{
		public Image Image { get; protected set; }
		public string Provider { get; set; }
		public string Source { get; set; }
		public string Photographer { get; set; }
		public string MoreInfo { get; set; }

		public ProviderImage(Image image)
		{
			Image = image;
		}

		~ProviderImage()
		{
			Dispose(false);
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (Image != null)
				{
					Image.Dispose();
				}
			}
		}
	}
}
