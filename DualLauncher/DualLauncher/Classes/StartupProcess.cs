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

namespace DualLauncher
{
	/// <summary>
	/// This is a process that is being started up.
	/// This object holds information to allow us to detect the main window
	/// of an application and then to move it to its desired position.
	/// </summary>
	public class StartupProcess
	{
		private StartupPosition startupPosition;
		/// <summary>
		/// The position we want the applications main window to start at
		/// </summary>
		public StartupPosition StartupPosition
		{
			get { return startupPosition; }
			set { startupPosition = value; }
		}

		private uint pid;
		/// <summary>
		/// The PID of the process
		/// </summary>
		public uint Pid
		{
			get { return pid; }
			set { pid = value; }
		}

		private string captionRegExpr;
		/// <summary>
		/// Regular expression to detect if the caption of a window is the window we are after
		/// </summary>
		public string CaptionRegExpr
		{
			get { return captionRegExpr; }
			set { captionRegExpr = value; }
		}

		private string windowClass;
		/// <summary>
		/// Window class of the window that we are after
		/// </summary>
		public string WindowClass
		{
			get { return windowClass; }
			set { windowClass = value; }
		}

		private DateTime expiryTime;
		/// <summary>
		/// This is the time after which this object is deemed to be of no further use
		/// </summary>
		public DateTime ExpiryTime
		{
			get { return expiryTime; }
			set { expiryTime = value; }
		}
	
		/// <summary>
		/// Ctor
		/// </summary>
		/// <param name="pid"></param>
		/// <param name="magicWord"></param>
		/// <param name="startPosition"></param>
		public StartupProcess(uint pid, MagicWord magicWord, StartupPosition startPosition)
		{
			this.pid = pid;
			// want own copy of the position
			this.startupPosition = startPosition.Clone();
			this.captionRegExpr = magicWord.CaptionRegExpr;
			this.windowClass = magicWord.WindowClass;

			this.expiryTime = DateTime.Now.AddSeconds((double)Properties.Settings.Default.StartupTimeout);
		}
	}
}
