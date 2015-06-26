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

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMT.Library.WallpaperPlugin
{
	/// <summary>
	/// Interface for a provider
	/// </summary>
	public interface IImageProvider
	{
		string Version { get; }		// Don't think this is needed?
		string ProviderName { get; }
		Image ProviderImage { get; }
		string Description { get; }
		int Weight { get; }
		Dictionary<string, string> Config { get; }

		Dictionary<string, string> ShowUserOptions();
		ProviderImage GetRandomImage(Size optimumSize);
	}
}
