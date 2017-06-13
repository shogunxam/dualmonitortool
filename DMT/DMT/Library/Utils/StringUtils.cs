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

namespace DMT.Library.Utils
{
	using System;
	using System.Collections.Generic;
	using System.Drawing;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;

	/// <summary>
	/// Utility class to support string conversions 
	/// </summary>
	static class StringUtils
	{
		/// <summary>
		/// Converts a string to a boolean
		/// </summary>
		/// <param name="text">Input string to convert</param>
		/// <param name="defaultValue">Default value if input string not valid</param>
		/// <returns>Value as a boolean</returns>
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

		/// <summary>
		/// Converts a string to an integer
		/// </summary>
		/// <param name="text">Input string to convert</param>
		/// <param name="defaultValue">Default value if input string not valid</param>
		/// <returns>Value as an integer</returns>
		public static int ToInt(string text, int defaultValue = 0)
		{
			int result = 0;

			if (int.TryParse(text, out result))
			{
				return result;
			}

			return defaultValue;
		}

		/// <summary>
		/// Converts a string to an unsigned integer
		/// </summary>
		/// <param name="text">Input string to convert</param>
		/// <param name="defaultValue">Default value if input string not valid</param>
		/// <returns>Value as an unsigned integer</returns>
		public static uint ToUInt(string text, uint defaultValue = 0)
		{
			uint result = 0;

			if (uint.TryParse(text, out result))
			{
				return result;
			}

			return defaultValue;
		}

		/// <summary>
		/// Converts a string to a rectangle.
		/// input expected to be in format: "x,y,width,height"
		/// </summary>
		/// <param name="text">Input string to convert</param>
		/// <returns>Value as a rectangle</returns>
		public static Rectangle ToRectangle(string text)
		{
			string[] fields = text.Split(",".ToCharArray());
			int x = GetFieldAsInt(fields, 0, 0);
			int y = GetFieldAsInt(fields, 1, 0);
			int width = GetFieldAsInt(fields, 2, 0);
			int height = GetFieldAsInt(fields, 3, 0);

			return new Rectangle(x, y, width, height);
		}

		/// <summary>
		/// Serialises a rectangle to a string
		/// </summary>
		/// <param name="r">Rectangle to serialise</param>
		/// <returns>Serialised rectangle</returns>
		public static string FromRectangle(Rectangle r)
		{
			return string.Format("{0},{1},{2},{3}", r.X, r.Y, r.Width, r.Height);
		}

		public static uint GetFieldAsUInt(string[] fields, int fieldIndex, uint defaultValue)
		{
			uint ret = defaultValue;

			if (fields.Length > fieldIndex)
			{
				ret = StringUtils.ToUInt(fields[fieldIndex]);
			}

			return ret;
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
