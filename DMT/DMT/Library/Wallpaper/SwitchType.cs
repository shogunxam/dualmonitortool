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

namespace DMT.Library.Wallpaper
{
	using System;
	using System.Collections.Generic;
	using System.Text;

	using DMT.Resources;

	/// <summary>
	/// Specifies how to handle multiple monitors when generating the wallpaper
	/// </summary>
	class SwitchType
	{
		/// <summary>
		/// Initialises a new instance of the <see cref="SwitchType" /> class.
		/// </summary>
		/// <param name="mapping">Image to monitor mapping type</param>
		public SwitchType(ImageToMonitorMapping mapping)
		{
			Mapping = mapping;
		}

		/// <summary>
		/// Mapping between the images and the monitors
		/// </summary>
		public enum ImageToMonitorMapping
		{
			/// <summary>
			/// same image spread over all monitors
			/// </summary>
			OneToOneBig = 0,

			/// <summary>
			/// same image on each monitor
			/// </summary>
			OneToMany = 1,

			/// <summary>
			/// different image on each monitor
			/// </summary>
			ManyToMany = 2,	

			/// <summary>
			/// different on all and done one at a time
			/// </summary>
			ManyToManyInSequence = 3
		}

		/// <summary>
		/// Gets or sets the mapping to use
		/// </summary>
		public ImageToMonitorMapping Mapping { get; set; }

		/// <summary>
		/// Gets a list of all mapping types
		/// </summary>
		/// <returns>List of all mapping types</returns>
		public static List<SwitchType> AllTypes()
		{
			List<SwitchType> allTypes = new List<SwitchType>();
			allTypes.Add(new SwitchType(ImageToMonitorMapping.OneToOneBig));
			allTypes.Add(new SwitchType(ImageToMonitorMapping.OneToMany));
			allTypes.Add(new SwitchType(ImageToMonitorMapping.ManyToMany));
			allTypes.Add(new SwitchType(ImageToMonitorMapping.ManyToManyInSequence));
			return allTypes;
		}

		/// <summary>
		/// Gets the mapping as a displayable string
		/// </summary>
		/// <returns>Displayable string</returns>
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
	}
}
