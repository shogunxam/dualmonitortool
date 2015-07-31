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
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace DMT.Library.Html
{
	/// <summary>
	/// Represents a single connection to a single web site
	/// </summary>
	public class HttpConnection
	{
		public string Host { get; private set; }
		public int Port { get; private set; }
		public CookieCollection Cookies { get; private set; }
		public Uri LastUri { get; set; }

		public HttpConnection(Uri uri)
		{
			Host = uri.Host;
			Port = uri.Port;
			LastUri = null;
			Cookies = new CookieCollection();
			Cookies.Add(new Cookie("DNT", "1", "/", Host));
		}

		public bool CanHandle(Uri uri)
		{
			if (string.Compare(uri.Host, Host, true) == 0 && uri.Port == Port)
			{
				return true;
			}

			return false;
		}

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
