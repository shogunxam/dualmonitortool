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

namespace DMT.Modules.WallpaperChanger
{
	using System;
	using System.Collections.Generic;
	using System.Text;

	using DMT.Library.Wallpaper;
	using DMT.Library.WallpaperPlugin;

	/// <summary>
	/// Interface to an image provider factory
	/// </summary>
	public interface IProviderFactory
	{
		/// <summary>
		/// Gets the list of available plugins
		/// </summary>
		/// <returns>List of plugins</returns>
		List<IDWC_Plugin> GetAvailablePlugins();

		/// <summary>
		/// Creates a provider
		/// </summary>
		/// <param name="name">Name of provider</param>
		/// <param name="dictionaryConfig">Configuration to use for provider</param>
		/// <returns>Created provider</returns>
		IImageProvider CreateProvider(string name, Dictionary<string, string> dictionaryConfig);
	}
}
