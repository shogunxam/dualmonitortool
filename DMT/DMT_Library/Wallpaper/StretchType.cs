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

using DMT.Library.Resources;
using System;
using System.Collections.Generic;
using System.Text;

namespace DMT.Library.Wallpaper
{
	/// <summary>
	/// Specifies the stretch type to fit an image onto a sreen (or screens)
	/// </summary>
	public class StretchType
	{
		/// <summary>
		/// Enum specifying how to fit an image where the aspect ratio 
		/// of the source and destionation may be different
		/// </summary>
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
			/// cobering all screens.
			/// Bars will be added using chosen background colour if needed.
			/// ('fit' in Win 8 terminology)
			/// </summary>
			UnderStretch = 1,
			/// <summary>
			/// center the image with each pixel on the dispaly corresponding to a pixel in the image,
			/// so it isn't stretched or shrunk
			/// </summary>
			Center = 2,
			/// <summary>
			/// stretch both X and Y to fit exactly, 
			/// which may resut in the aspect ratio changing
			/// </summary>
			StretchToFit = 3
		};

		private Fit type;
		/// <summary>
		/// The type of fit
		/// </summary>
		public Fit Type
		{
			get { return type; }
			set { type = value; }
		}

		/// <summary>
		/// ctor 
		/// </summary>
		/// <param name="type">initial stretch type</param>
		public StretchType(Fit type)
		{
			this.type = type;
		}

		/// <summary>
		/// Converts the stretch type to a displayable string
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			string ret;

			switch (type)
			{
				case Fit.OverStretch:
					ret = LibraryStrings.OverStretch;
					break;

				case Fit.UnderStretch:
					ret = LibraryStrings.UnderStretch;
					break;

				case Fit.Center:
					ret = LibraryStrings.Center;
					break;

				case Fit.StretchToFit:
					ret = LibraryStrings.Stretch;
					break;

				default:
					ret = "?";
					break;
			}

			return ret;
		}

		public static List<StretchType> AllTypes()
		{
			List<StretchType> allTypes = new List<StretchType>();
			allTypes.Add(new StretchType(StretchType.Fit.OverStretch));
			allTypes.Add(new StretchType(StretchType.Fit.UnderStretch));
			allTypes.Add(new StretchType(StretchType.Fit.Center));
			allTypes.Add(new StretchType(StretchType.Fit.StretchToFit));
			return allTypes;
		}
	}
}
