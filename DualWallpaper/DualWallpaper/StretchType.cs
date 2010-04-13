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
	class Stretch
	{
		public enum Fit
		{
			StretchToFit,	// stretches both X and Y to type exactly
			OverStretch,	// stretch maintaining aspect ratio, clipping if needed
			UnderStretch	// stretch maintaining aspect ratio, adding bars if needed
		};

		private Fit type;

		public Fit Type
		{
			get { return type; }
			set { type = value; }
		}

		public Stretch(Fit type)
		{
			this.type = type;
		}

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

				default:
					ret = "?";
					break;
			}

			return ret;
		}
	}
}
