#region copyright
// This file is part of Dual Monitor Tools which is a set of tools to assist
// users with multiple monitor setups.
// Copyright (C) 2010-2015  Gerald Evans
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

namespace DMT.Modules.Launcher
{
	using System;
	using System.Collections.Generic;
	using System.Text;

	/// <summary>
	/// This is a process that is being started up.
	/// This object holds information to allow us to detect the main window
	/// of an application and then to move it to its desired position.
	/// </summary>
	public class StartupProcess
	{
		/// <summary>
		/// Initialises a new instance of the <see cref="StartupProcess" /> class.
		/// </summary>
		/// <param name="pid">Process id of process started</param>
		/// <param name="magicWord">Magic word that launched process</param>
		/// <param name="startPosition">The required starting location for the main window of the process</param>
		/// <param name="startupTimeout">Timeout to wait for process to show it's window</param>
		public StartupProcess(uint pid, MagicWord magicWord, StartupPosition startPosition, int startupTimeout)
		{
			Pid = pid;

			// want own copy of the position
			StartupPosition = startPosition.Clone();
			CaptionRegExpr = magicWord.CaptionRegExpr;
			WindowClass = magicWord.WindowClass;

			ExpiryTime = DateTime.Now.AddSeconds((double)startupTimeout);
		}

		/// <summary>
		/// Gets the position we want the applications main window to start at
		/// </summary>
		public StartupPosition StartupPosition { get; private set; }

		/// <summary>
		/// Gets the PID of the process
		/// </summary>
		public uint Pid { get; private set; }

		/// <summary>
		/// Gets the regular expression to detect if the caption of a window is the window we are after
		/// </summary>
		public string CaptionRegExpr { get; private set; }

		/// <summary>
		/// Gets the window class of the window that we are after
		/// </summary>
		public string WindowClass { get; private set; }

		/// <summary>
		/// Gets the time after which this object is deemed to be of no further use
		/// </summary>
		public DateTime ExpiryTime { get; private set; }
	}
}
