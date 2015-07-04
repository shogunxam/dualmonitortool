using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DMT.Library.Html
{
	public static class HttpHelper
	{
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
		/// <param name="s"></param>
		/// <returns></returns>
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
					sb.Append(String.Format("%{0:x2}", (int)ch));
				}
			}

			return sb.ToString();
		}
	}
}
