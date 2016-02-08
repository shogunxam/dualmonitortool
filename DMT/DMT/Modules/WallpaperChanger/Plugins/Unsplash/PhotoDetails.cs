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

namespace DMT.Modules.WallpaperChanger.Plugins.Unsplash
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;

	/// <summary>
	/// Details of a single photo
	/// </summary>
	class PhotoDetails
	{
		/// <summary>
		/// Gets or sets the URL of the source of the photo
		/// </summary>
		public string ImageUrl { get; set; }

		/// <summary>
		/// Gets or sets the URL of the page containing details of the photo
		/// </summary>
		public string PhotoDetailsUrl { get; set; }

		/// <summary>
		/// Gets or sets the photographers name
		/// </summary>
		public string Photographer { get; set; }

		/// <summary>
		/// Gets or sets the URL of the page containing details about the photographer
		/// </summary>
		public string PhotographerUrl { get; set; }

		/// <summary>
		/// Clears all photo details
		/// </summary>
		public void Clear()
		{
			ImageUrl = string.Empty;
			PhotoDetailsUrl = string.Empty;
			Photographer = string.Empty;
			PhotographerUrl = string.Empty;
		}
	}
}
