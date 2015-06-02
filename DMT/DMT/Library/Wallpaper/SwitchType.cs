#region copyright
// This file is part of Dual Monitor Tools which is a set of tools to assist
// users with multiple monitor setups.
// Copyright (C) 2015  Gerald Evans
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

using DMT.Resources;
using System;
using System.Collections.Generic;
using System.Text;

namespace DMT.Library.Wallpaper
{
	/// <summary>
	/// Specifies how to handle multiple monitors when generating the wallpaper
	/// </summary>
	class SwitchType
	{
		/// <summary>
		/// Mapping between the images and the monitors
		/// </summary>
		public enum ImageToMonitorMapping
		{
			OneToOneBig = 0,			// same image spread over all monitors
			OneToMany = 1,				// same image on each monitor
			ManyToMany = 2,				// different image on each monitor
			ManyToManyInSequence = 3	// different on all and done one at a time
		}


		public ImageToMonitorMapping Mapping { get; set; }

		public SwitchType(ImageToMonitorMapping mapping)
		{
			Mapping = mapping;
		}

		public override string ToString()
		{
			string ret;

			switch (Mapping)
			{
				case ImageToMonitorMapping.OneToOneBig:
					ret = WallpaperStrings.OneToOneBig;
					break;

				case ImageToMonitorMapping.OneToMany:
					ret = WallpaperStrings.OneToMany;
					break;

				case ImageToMonitorMapping.ManyToMany:
					ret = WallpaperStrings.ManyToMany;
					break;

				case ImageToMonitorMapping.ManyToManyInSequence:
					ret = WallpaperStrings.ManyToManyInSequence;
					break;

				default:
					ret = "?";
					break;
			}

			return ret;
		}

		public static List<SwitchType> AllTypes()
		{
			List<SwitchType> allTypes = new List<SwitchType>();
			allTypes.Add(new SwitchType(ImageToMonitorMapping.OneToOneBig));
			allTypes.Add(new SwitchType(ImageToMonitorMapping.OneToMany));
			allTypes.Add(new SwitchType(ImageToMonitorMapping.ManyToMany));
			allTypes.Add(new SwitchType(ImageToMonitorMapping.ManyToManyInSequence));
			return allTypes;
		}

	}
}
