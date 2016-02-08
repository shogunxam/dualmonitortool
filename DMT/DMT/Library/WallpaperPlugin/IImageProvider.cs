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

namespace DMT.Library.WallpaperPlugin
{
	using System;
	using System.Collections.Generic;
	using System.Drawing;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;

	/// <summary>
	/// Interface for an image provider
	/// </summary>
	public interface IImageProvider
	{
		/// <summary>
		/// Gets the version of the provider
		/// </summary>
		string Version { get; }		// Don't think this is needed?

		/// <summary>
		/// Gets the provider name
		/// </summary>
		string ProviderName { get; }

		/// <summary>
		/// Gets the image for the provider
		/// </summary>
		Image ProviderImage { get; }

		/// <summary>
		/// Gets the description for this provider
		/// </summary>
		string Description { get; }

		/// <summary>
		/// Gets the weight for this provider
		/// </summary>
		int Weight { get; }

		/// <summary>
		/// Gets the configuration for this provider
		/// </summary>
		Dictionary<string, string> Config { get; }

		/// <summary>
		/// Shows the configuration dialog and returns chosen options
		/// </summary>
		/// <returns>Chosen options</returns>
		Dictionary<string, string> ShowUserOptions();

		/// <summary>
		/// Gets a random image from the provider
		/// </summary>
		/// <param name="optimumSize">Optimum size of image</param>
		/// <param name="screenIndex">The screen index the image is for</param>
		/// <returns>Random image</returns>
		ProviderImage GetRandomImage(Size optimumSize, int screenIndex);
	}
}
