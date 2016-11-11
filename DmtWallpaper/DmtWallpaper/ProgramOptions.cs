#region copyright
// This file is part of Dual Monitor Tools which is a set of tools to assist
// users with multiple monitor setups.
// Copyright (C) 2016 Gerald Evans
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
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DmtWallpaper
{
	public class ProgramOptions
	{
		public enum Mode { FullScreen, Preview, Configuration };

		public Mode RunMode { get; set; }
		public IntPtr HWnd { get; set; }

		public ProgramOptions(string[] args)
		{
			// default to Configuration mode
			RunMode = ProgramOptions.Mode.Configuration;
			HWnd = IntPtr.Zero;

			if (args.Length > 0)
			{
				string arg = args[0].ToLower().Trim();

				if (arg.Length > 1 && arg[0] == '/')
				{
					switch (arg[1])
					{
						case 's':
							RunMode = Mode.FullScreen;
							break;

						case 'p':
							RunMode = Mode.Preview;
							ScanWindowHandle(arg.Substring(2));
							break;

						case 'c':
							RunMode= Mode.Configuration;
							ScanWindowHandle(arg.Substring(2));
							break;
					}
				}
			}
		}

		void ScanWindowHandle(string arg)
		{
			arg = arg.TrimStart(new char[] { ' ', ':' });
			int hWnd;
			if (int.TryParse(arg, out hWnd))
			{
				HWnd = (IntPtr)hWnd;
			}
		}
	}
}
