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

namespace DualWallpaper
{
	/// <summary>
	/// Specifies the stretch type to maintain aspect ratio
	/// </summary>
	class Stretch
	{
		/// <summary>
		/// Enum specifying how to fit an image where the aspect ratio 
		/// of the source and destionation may be different
		/// </summary>
		public enum Fit
		{
			/// <summary>
			/// stretch both X and Y to fit exactly, 
			/// which may resut in the aspect ratio changing
			/// </summary>
			StretchToFit,
			/// <summary>
			/// stretch maintaining aspect ratio, clipping if needed
			/// </summary>
			OverStretch,
			/// <summary>
			/// stretch maintaining aspect ratio, adding bars if needed
			/// </summary>
			UnderStretch,
			/// <summary>
			/// center the image with each pixel on the dispaly corresponding to a pixel in the image,
			/// soit isn't stretched or shrunk
			/// </summary>
			Center
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
		public Stretch(Fit type)
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
				case Fit.StretchToFit:
					ret = Properties.Resources.Stretch;
					break;

				case Fit.OverStretch:
					ret = Properties.Resources.OverStretch;
					break;

				case Fit.UnderStretch:
					ret = Properties.Resources.UnderStretch;
					break;

				case Fit.Center:
					ret = Properties.Resources.Center;
					break;

				default:
					ret = "?";
					break;
			}

			return ret;
		}
	}
}
