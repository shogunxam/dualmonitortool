#region copyright
// This file is part of Dual Monitor Tools which is a set of tools to assist
// users with multiple monitor setups.
// Copyright (C) 2014  Gerald Evans
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
	/// The command line options.
	/// </summary>
	class Options
	{
		// Input filenames
		List<string> _inputFiles = new List<string>();

		/// <summary>
		/// Indicates if running in command line mode
		/// </summary>
		public bool CmdMode { get; protected set; }

		/// <summary>
		/// Show usage and exit
		/// </summary>
		public bool ShowUsage { get; protected set; }

		/// <summary>
		/// Show version and exit
		/// </summary>
		public bool ShowVersion { get; protected set; }

		public Stretch.Fit StretchType { get; protected set; }

		/// <summary>
		/// The command that needs to be run
		/// </summary>
		public string SourceFilename
		{
			get
			{
				if (_inputFiles != null && _inputFiles.Count >= 1)
				{
					return _inputFiles[0];
				}
				return null;
			}
		}

		/// <summary>
		/// Option specification errors
		/// </summary>
		public List<string> Errors { get; protected set; }


		/// <summary>
		/// Ctor that takes the list of command line arguments passed into us
		/// </summary>
		/// <param name="args">Command line arguments</param>
		public Options(string[] args)
		{
			int argIndex = 0;

			Errors = new List<string>();

			// set defaults
			//CmdMode = false;
			//ShowUsage = false;
			//ShowVersion = false;
			StretchType = Stretch.Fit.OverStretch;

			while (argIndex < args.Length)
			{
				// at the moment any option/argument will force it into command line mode,
				// but at some time in the future, we may want to allow options for gui mode?
				CmdMode = true;

				string arg = args[argIndex];
				if (arg.Length > 1 && arg[0] == '-')
				{
					if (arg.Substring(1) == "?")
					{
						ShowUsage = true;
					}
					else if (arg.Substring(1) == "v")
					{
						ShowVersion = true;
					}
					else if (arg.Substring(1) == "sf")
					{
						StretchType = Stretch.Fit.StretchToFit;
					}
					else if (arg.Substring(1) == "so")
					{
						StretchType = Stretch.Fit.OverStretch;
					}
					else if (arg.Substring(1) == "su")
					{
						StretchType = Stretch.Fit.UnderStretch;
					}
					else if (arg.Substring(1) == "sc")
					{
						StretchType = Stretch.Fit.Center;
					}
					else
					{
						Errors.Add(string.Format("Unrecognised option: \"{0}\"", arg));
					}
				}
				else
				{
					_inputFiles.Add(arg);
				}

				argIndex++;
			}

			// perform validation
			if (CmdMode && !ShowUsage && !ShowVersion)
			{
				if (_inputFiles.Count == 0)
				{
					Errors.Add("Must specify an input image file to use as the wallpaper");
				}
				else if (_inputFiles.Count > 1)
				{
					Errors.Add("Only a single image file can be specified");
				}
			}
		}
	}
}