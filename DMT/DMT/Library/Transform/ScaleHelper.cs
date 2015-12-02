using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace DMT.Library.Transform
{
	public static class ScaleHelper
	{
		/// <summary>
		/// Scales the src size so that it will be as large as possible
		/// and still fit within the target size
		/// while maintaining aspect ratio
		/// </summary>
		/// <param name="src"></param>
		/// <param name="target"></param>
		/// <returns></returns>
		public static Size UnderScale(Size src, Size target)
		{
			Size dest = target;

			// if one of the sizes is zero, then something has gone wrong?
			if (src.Width > 0 && src.Height > 0)
			{
				// The bigger a factor is, the more that dimension needs to be increased 
				// to hit its target dimension
				int widthFactor = target.Width * src.Height;
				int heightFactor = target.Height * src.Width;

				// need the minimum factor to keep everything within the target size
				if (widthFactor < heightFactor)
				{
					// width factor is minimum, so width will be full width, just scale the height
					dest.Height = (src.Height * dest.Width) / src.Width;
				}
				else 
				{
					// height factor is minimum, so height will be full height, just scale the width
					dest.Width = (src.Width * dest.Height) / src.Height;
				}
			}

			return dest;
		}
	}
}
