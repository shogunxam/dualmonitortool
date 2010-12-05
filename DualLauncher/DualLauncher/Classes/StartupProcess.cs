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
	public class StartupProcess
	{
		private StartupPosition startupPosition;
		public StartupPosition StartupPosition
		{
			get { return startupPosition; }
			set { startupPosition = value; }
		}

		private uint pid;
		public uint Pid
		{
			get { return pid; }
			set { pid = value; }
		}

		private string captionRegExpr;
		public string CaptionRegExpr
		{
			get { return captionRegExpr; }
			set { captionRegExpr = value; }
		}

		private string windowClass;
		public string WindowClass
		{
			get { return windowClass; }
			set { windowClass = value; }
		}
	
	

		public StartupProcess()
		{
		}

		public StartupProcess(uint pid, MagicWord magicWord, StartupPosition startPosition)
		{
			this.pid = pid;
			// want own copy of the position
			this.startupPosition = startPosition.Clone();
			this.captionRegExpr = magicWord.CaptionRegExpr;
			this.windowClass = magicWord.WindowClass;
		}

		//public StartupProcess(uint pid, StartupPosition startupPosition)
		//{
		//    this.pid = pid;
		//    this.startupPosition = startupPosition;
		//}
	}
}
