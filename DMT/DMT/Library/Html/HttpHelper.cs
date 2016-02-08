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

namespace DMT.Library.Html
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;

	/// <summary>
	/// Utility class to help with HTML requests
	/// </summary>
	public static class HttpHelper
	{
		/// <summary>
		/// Encode the given string  so it can be used as a valid url request
		/// </summary>
		/// <param name="s">string to encode</param>
		/// <param name="urlEncoding">Not currently used</param>
		/// <returns>String encoded as a valid url</returns>
		public static string UrlEncode(string s, Encoding urlEncoding)
		{
			return UrlEncode(s);
		}

		/// <summary>
		/// Does the same job as HttpUtility.UrlEncode()
		///  - which we don't have access to as it is not part of the .NET 4.0 client framework
		///  and the same as WebUtility.UrlEncode()
		///  - but this function is only available in .NET 4.5 or later
		/// </summary>
		/// <param name="s">string to encode</param>
		/// <returns>String encoded as a valid url</returns>
		public static string UrlEncode(string s)
		{
			// could make use of Uri.EscapeDataString(), but lets do it the hard way
			StringBuilder sb = new StringBuilder();
			foreach (char ch in s)
			{
				if ((ch >= 'a' && ch <= 'z')
				 || (ch >= 'A' && ch <= 'Z')
				 || (ch >= '0' && ch <= '9')
				 || ch == '$'
				 || ch == '-'
				 || ch == '_'
				 || ch == '@'
				 || ch == '.'
				 || ch == '!'
				 || ch == '*'
				 || ch == '('
				 || ch == ')'
				 || ch == ',')
				{
					sb.Append(ch);
				}
				else if (ch == ' ')
				{
					sb.Append("+"); // want "+" rather than "%20";
				}
				else
				{
					// TODO: what about chars that don't fit in 8 bits?
					int n = (int)ch;
					sb.Append(string.Format("%{0:x2}", (int)ch));
				}
			}

			return sb.ToString();
		}
	}
}
