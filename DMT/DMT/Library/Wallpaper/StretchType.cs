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

namespace DMT.Library.Wallpaper
{
	using System;
	using System.Collections.Generic;
	using System.Text;

	using DMT.Resources;

	/// <summary>
	/// Specifies the stretch type to fit an image onto a screen (or screens)
	/// </summary>
	public class StretchType
	{
		/// <summary>
		/// Initialises a new instance of the <see cref="StretchType" /> class.
		/// </summary>
		/// <param name="type">initial stretch type</param>
		public StretchType(Fit type)
		{
			Type = type;
		}

		/// <summary>
		/// Enumeration specifying how to fit an image where the aspect ratio 
		/// of the source and destination may be different
		/// </summary>
		[Flags]
		public enum Fit
		{
			/// <summary>
			/// Stretch maintaining aspect ratio, so that the image covers all screens.
			/// Image will be clipped if needed.
			/// ('fill' in Win 8 terminology)
			/// </summary>
			OverStretch = 0,

			/// <summary>
			/// Stretch maintaining aspect ratio, so that the image fits within the virtual rectangle
			/// covering all screens.
			/// Bars will be added using chosen background colour if needed.
			/// ('fit' in Win 8 terminology)
			/// </summary>
			UnderStretch = 1,

			/// <summary>
			/// Centre the image with each pixel on the display corresponding to a pixel in the image,
			/// so it isn't stretched or shrunk
			/// </summary>
			Center = 2,

			/// <summary>
			/// stretch both X and Y to fit exactly, 
			/// which may result in the aspect ratio changing
			/// </summary>
			StretchToFit = 3,

			NewFit = 0x08,
			MaintainAspectRatio = 0x10,
			AllowEnlarge = 0x20,
			AllowShrink = 0x40,
			ClipImage = 0x80
		}

		/// <summary>
		/// Gets or sets the type of fit
		/// </summary>
		public Fit Type { get; set; }

		/// <summary>
		/// Gets all available fit types
		/// </summary>
		/// <returns>List of all fit types</returns>
		public static List<StretchType> AllTypes()
		{
			List<StretchType> allTypes = new List<StretchType>();
			allTypes.Add(new StretchType(StretchType.Fit.OverStretch));
			allTypes.Add(new StretchType(StretchType.Fit.UnderStretch));
			allTypes.Add(new StretchType(StretchType.Fit.Center));
			allTypes.Add(new StretchType(StretchType.Fit.StretchToFit));
			return allTypes;
		}

		/// <summary>
		/// Converts the stretch type to a displayable string
		/// </summary>
		/// <returns>Displayable string</returns>
		public override string ToString()
		{
			string ret;

			switch (Type)
			{
				case Fit.OverStretch:
					ret = WallpaperStrings.OverStretch;
					break;

				case Fit.UnderStretch:
					ret = WallpaperStrings.UnderStretch;
					break;

				case Fit.Center:
					ret = WallpaperStrings.Center;
					break;

				case Fit.StretchToFit:
					ret = WallpaperStrings.Stretch;
					break;

				default:
					ret = "?";
					break;
			}

			return ret;
		}
	}
}
