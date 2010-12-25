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
using System.Drawing;
using System.Text;

namespace DualLauncher
{
	/// <summary>
	/// Specifies a startup position for an application
	/// </summary>
	[Serializable]
	public class StartupPosition
	{
		private bool enablePosition;
		/// <summary>
		/// Indicates if this StartupPosition is to be used
		/// </summary>
		public bool EnablePosition
		{
			get { return enablePosition; }
			set { enablePosition = value; }
		}

		private Rectangle position;
		/// <summary>
		/// Position to open the window at.
		/// </summary>
		// don't serialize the rectangle directly as it leads to duplication
		// in the XML due to it having multiple public properties to access
		// the same info.
		// Instead we use out own Location and Size for serialization
		[System.Xml.Serialization.XmlIgnore]
		public Rectangle Position
		{
			get { return position; }
			set { position = value; }
		}

		/// <summary>
		/// Added for serialisation of location instead of using Position
		/// </summary>
		public Point Location
		{
			get { return position.Location; }
			set { position.Location = value; }
		}

		/// <summary>
		/// Added for serialisation of size instead of using Position
		/// </summary>
		public Size Size
		{
			get { return position.Size; }
			set { position.Size = value; }
		}

		private int showCmd;
		/// <summary>
		/// Indicates if the window should be shown minimised, maximised or normal.
		/// This uses the save values as used by Win32.ShowWindow() and Win32.WINDOWPLACEMENT
		/// </summary>
		public int ShowCmd
		{
			get { return showCmd; }
			set { showCmd = value; }
		}

		public StartupPosition()
		{
		}

		/// <summary>
		/// Deep clone the StartupPosition.
		/// (No actual need for deep clone with current implementation)
		/// note: this is not ICloneable.Clone() - but could be if needed
		/// </summary>
		/// <returns></returns>
		public StartupPosition Clone()
		{
			return (StartupPosition)this.MemberwiseClone();
		}
	
	}
}
