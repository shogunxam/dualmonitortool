using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;

namespace DMT.Library.Utils
{
	public static class ImageExifExtensions
	{
		const int exifType_AsciiString = 2;

		const int exif_ImageDescription = 0x10E;
		const int exif_Orientation = 0x112;
		const int exif_Artist = 0x13B;
		const int exif_Copyright = 0x8298;

		public static int GetExifOrientation(this Image image)
		{
			if (Array.IndexOf(image.PropertyIdList, exif_Orientation) >= 0)
			{
				return (int)image.GetPropertyItem(exif_Orientation).Value[0];
			}

			return -1;
		}

		public static string GetExifPhotographer(this Image image)
		{
			string ret = GetPropertyAsString(image, exif_Artist);
			if (ret != null)
			{
				return ret;
			}

			ret = GetPropertyAsString(image, exif_Copyright);

			return ret;
		}

		public static string GetExifDescription(this Image image)
		{
			string ret = GetPropertyAsString(image, exif_ImageDescription);

			return ret;
		}

		public static void RemoveExifOrientation(this Image image)
		{
			image.RemovePropertyItem(exif_Orientation);
		}

		static string GetPropertyAsString(Image image, int exifTag)
		{
			if (Array.IndexOf(image.PropertyIdList, exifTag) >= 0)
			{
				PropertyItem pi = image.GetPropertyItem(exifTag);
				if (pi != null && pi.Type == exifType_AsciiString)
				{
					string ret = Encoding.ASCII.GetString(pi.Value);
					if (!string.IsNullOrWhiteSpace(ret))
					{
						return ret;
					}
				}
			}

			return null;
		}
	}


}
