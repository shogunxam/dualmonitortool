#region copyright
// This file is part of Dual Monitor Tools which is a set of tools to assist
// users with multiple monitor setups.
// Copyright (C) 2017  Gerald Evans
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
