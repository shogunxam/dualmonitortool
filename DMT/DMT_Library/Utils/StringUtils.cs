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

namespace DMT.Library.Utils
{
	public static class StringUtils
	{
		public static bool ToBool(string text, bool defaultValue = false)
		{
			if (string.Compare(text, "true", true) == 0
			 || string.Compare(text, "1") == 0)
			{
				return true;
			}
			else if (string.Compare(text, "false", true) == 0
			 || string.Compare(text, "0") == 0)
			{
				return false;
			}

			return defaultValue;
		}

		public static int ToInt(string text, int defaultValue = 0)
		{
			int result = 0;

			if (Int32.TryParse(text, out result))
			{
				return result;
			}
			return defaultValue;
		}

		public static uint ToUInt(string text, uint defaultValue = 0)
		{
			uint result = 0;

			if (UInt32.TryParse(text, out result))
			{
				return result;
			}
			return defaultValue;
		}

		public static Rectangle ToRectangle(string text)
		{
			string[] fields = text.Split(",".ToCharArray());
			int x = GetFieldAsInt(fields, 0, 0);
			int y = GetFieldAsInt(fields, 1, 0);
			int width = GetFieldAsInt(fields, 2, 0);
			int height = GetFieldAsInt(fields, 3, 0);

			return new Rectangle(x, y, width, height);
		}

		public static string FromRectangle(Rectangle r)
		{
			return string.Format("{0},{1},{2},{3}", r.X, r.Y, r.Width, r.Height);
		}

		static int GetFieldAsInt(string[] fields, int fieldIndex, int defaultValue)
		{
			int ret = defaultValue;

			if (fields.Length > fieldIndex)
			{
				ret = StringUtils.ToInt(fields[fieldIndex]);
			}
			return ret;
		}

	}
}
