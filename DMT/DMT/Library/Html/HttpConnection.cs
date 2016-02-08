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
	using System.IO;
	using System.Linq;
	using System.Net;
	using System.Text;
	using System.Threading.Tasks;
	using System.Web;

	/// <summary>
	/// Represents a single connection to a single web site
	/// </summary>
	public class HttpConnection
	{
		/// <summary>
		/// Initialises a new instance of the <see cref="HttpConnection" /> class.
		/// </summary>
		/// <param name="uri">The location to connect to</param>
		public HttpConnection(Uri uri)
		{
			Host = uri.Host;
			Port = uri.Port;
			LastUri = null;
			Cookies = new CookieCollection();
			Cookies.Add(new Cookie("DNT", "1", "/", Host));
		}

		/// <summary>
		/// Gets the host name
		/// </summary>
		public string Host { get; private set; }

		/// <summary>
		/// Gets the port number
		/// </summary>
		public int Port { get; private set; }

		/// <summary>
		/// Gets the cookies
		/// </summary>
		public CookieCollection Cookies { get; private set; }

		/// <summary>
		/// Gets or sets the last uri requested on the connection
		/// </summary>
		public Uri LastUri { get; set; }

		/// <summary>
		/// Checks if the requested uri can be retrieved on this connection
		/// </summary>
		/// <param name="uri">Uri we need to request</param>
		/// <returns>True if connection is suitable</returns>
		public bool CanHandle(Uri uri)
		{
			if (string.Compare(uri.Host, Host, true) == 0 && uri.Port == Port)
			{
				return true;
			}

			return false;
		}

		/// <summary>
		/// Converts relative path to full path
		/// </summary>
		/// <param name="relativeUrl">Relative path</param>
		/// <returns>Full path</returns>
		public Uri ExpandPath(string relativeUrl)
		{
			Uri uri;
			if (LastUri != null)
			{
				uri = new Uri(LastUri, relativeUrl);
			}
			else
			{
				// have to hope/assume it is a full URL
				uri = new Uri(relativeUrl);
			}

			return uri;
		}

		/// <summary>
		/// Merges cookies into those already held for this connection
		/// </summary>
		/// <param name="returnedCookies">Cookies to merge in</param>
		public void MergeCookies(CookieCollection returnedCookies)
		{
			if (returnedCookies != null)
			{
				foreach (Cookie newCookie in returnedCookies)
				{
					if (!newCookie.Expired)
					{
						bool alreadyExists = false;
						foreach (Cookie existingCookie in Cookies)
						{
							if (existingCookie.Name == newCookie.Name)
							{
								// just update assuming Path and Domain unchanged
								existingCookie.Value = newCookie.Value;
								existingCookie.Expires = newCookie.Expires;
								alreadyExists = true;
								break;
							}
						}

						if (!alreadyExists)
						{
							// TODO: may need to do some cleaning up of the cookie first due to deficiencies in .NET
							Cookies.Add(newCookie);
						}
					}
				}
			}
		}
	}
}
