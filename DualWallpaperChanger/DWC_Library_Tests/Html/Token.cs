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
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DualMonitorTools.DualWallpaperChanger.Html;

namespace DWC_Library_Tests.Html
{
	public class Token
	{
		public HtmlNodeType NodeType { get; set; }
		public string Value { get; set; }

		public Token(HtmlNodeType nodeType, string value)
		{
			NodeType = nodeType;
			Value = value;
		}

		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}

			Token token = obj as Token;
			if (token == null)
			{
				return false;
			}
			return (NodeType == token.NodeType && Value == token.Value);
		}

		public override int GetHashCode()
		{
			// see http://stackoverflow.com/questions/263400/what-is-the-best-algorithm-for-an-overridden-system-object-gethashcode
			return new  { NodeType, Value }.GetHashCode();
		}

		public bool Equals(Token token)
		{
			if (token == null)
			{
				return false;
			}
			return (NodeType == token.NodeType && Value == token.Value);
		}

		public override string ToString()
		{
			return string.Format("{0}:{1}", NodeType, Value);
		}
	}
}
