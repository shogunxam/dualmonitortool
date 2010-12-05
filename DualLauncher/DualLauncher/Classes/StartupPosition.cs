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
	[Serializable]
	public class StartupPosition
	{
		private bool enablePosition;
		public bool EnablePosition
		{
			get { return enablePosition; }
			set { enablePosition = value; }
		}

		private Rectangle position;
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

		public Point Location
		{
			get { return position.Location; }
			set { position.Location = value; }
		}

		public Size Size
		{
			get { return position.Size; }
			set { position.Size = value; }
		}
	
		//private bool hasLocation = false;
		//public bool HasLocation
		//{
		//    get { return hasLocation; }
		//    set { hasLocation = value; }
		//}

		//private Point location;
		//public Point Location
		//{
		//    get { return location; }
		//    set { location = value; }
		//}

		//private bool hasSize = false;
		//public bool HasSize
		//{
		//    get { return hasSize; }
		//    set { hasSize = value; }
		//}

		//private Size size;
		//public Size Size
		//{
		//    get { return size; }
		//    set { size = value; }
		//}

		public StartupPosition()
		{
		}

		//public StartupPosition(Point location)
		//{
		//    hasLocation = true;
		//    this.location = location;
		//}

		//public StartupPosition(Point location, Size size)
		//{
		//    enablePosition = true;
		//    //hasLocation = true;
		//    this.location = location;
		//    //hasSize = true;
		//    this.size = size;
		//}

		public StartupPosition(Rectangle position)
		{
			enablePosition = true;
			this.position = position;
		}

		// note: this is not ICloneable.Clone()
		public StartupPosition Clone()
		{
			return (StartupPosition)this.MemberwiseClone();
		}
	
	}
}
