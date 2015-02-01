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

namespace DualMonitorTools.DualWallpaperChanger.Html
{
	/// <summary>
	/// Represents a single connection to a web site
	/// </summary>
	public class HttpConnection
	{
		const string UserAgent = "Dual Wallpaper Changer/1.10";
		//const string contentTypeStr = "content-type";
		//const string charsetStr = "charset=";
		//const string unicodeStr = "Unicode";
		//const string iso8859Str = "iso-8859-1";

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

		public HttpWebRequest CreateGetRequest(Uri uri)
		{
			HttpWebRequest webRequest = CreateRequest(uri);
			webRequest.Method = "GET";

			return webRequest;
		}

		public HttpWebRequest CreatePostRequest(Uri uri, List<Tuple<string, string>> postParameters)
		{
			HttpWebRequest webRequest = CreateRequest(uri);
			webRequest.Method = "POST";
			webRequest.ContentType = "application/x-www-form-urlencoded";

			// encode the post parameters
			Encoding urlEncoding = Encoding.Default;
			string postParameterAsString = EncodePostParameters(postParameters, urlEncoding);
			byte[] postParamtersAsBytes = Encoding.ASCII.GetBytes(postParameterAsString);

			// add the encoded post parameters
			webRequest.ContentLength = postParamtersAsBytes.Length;
			Stream requestStream = webRequest.GetRequestStream();
			requestStream.Write(postParamtersAsBytes, 0, postParamtersAsBytes.Length);
			requestStream.Close();

			return webRequest;
		}

		public HttpResponseData GetResponse(HttpWebRequest webRequest)
		{
			HttpWebResponse webResponse = CatchGetResponse(webRequest);

			byte[] rawData = ReadResponseStream(webResponse);
			HttpResponseData responseData =  new HttpResponseData(webResponse.StatusCode, rawData, webResponse.ContentType);

			// save the uri of the page that actually reponded to us
			LastUri = webRequest.Address;

			// save any cookies returned to us
			// TODO: this is wrong as 302 could have sent us to a different domain 
			// and the cookies should be for there, but this will do for now
			MergeCookies(webResponse);
			webResponse.Close();

			return responseData;
		}


		HttpWebRequest CreateRequest(Uri uri)
		{
			HttpWebRequest webRequest = (HttpWebRequest)HttpWebRequest.Create(uri);
			if (LastUri != null)
			{
				webRequest.Referer = LastUri.AbsolutePath;
			}

			webRequest.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
			webRequest.Headers.Add("Accept-Language", "en-us,en;q=0.5");
			webRequest.Headers.Add("Accept-Charset", "ISO-8859-1,utf-8;q=0.7,*;q=0.7");
			webRequest.UserAgent = UserAgent;

			CookieContainer cookieContainer = new CookieContainer();
			cookieContainer.Add(Cookies);
			webRequest.CookieContainer = cookieContainer;


			return webRequest;
		}

		string EncodePostParameters(List<Tuple<string, string>> postParameters, Encoding urlEncoding)
		{
			StringBuilder sb = new StringBuilder();

			foreach (Tuple<string, string> postParameter in postParameters)
			{
				if (sb.Length > 0)
				{
					sb.Append("&");
				}
				sb.Append(HttpUtility.UrlEncode(postParameter.Item1, urlEncoding));
				if (postParameter.Item2 != null)
				{
					sb.Append("=");
					sb.Append(HttpUtility.UrlEncode(postParameter.Item2, urlEncoding));
				}
			}

			return sb.ToString();
		}

		HttpWebResponse CatchGetResponse(HttpWebRequest webRequest)
		{
			try
			{
				return (HttpWebResponse)webRequest.GetResponse();
			}
			catch (WebException ex)
			{
				if (ex.Response != null)
				{
					return (HttpWebResponse)ex.Response;
				}
				throw;
			}
		}

		byte[] ReadResponseStream(HttpWebResponse webResponse)
		{
			byte[] rawData = null;

			Stream responseStream = webResponse.GetResponseStream();

			int bytesRead;
			byte[] buffer = new byte[16384];

			while ((bytesRead = responseStream.Read(buffer, 0, buffer.Length)) != 0)
			{
				int offset = 0;
				if (rawData == null)
				{
					offset = 0;
					rawData = new byte[bytesRead];
				}
				else
				{
					byte[] previousData = rawData;
					offset = rawData.Length;
					rawData = new byte[offset + bytesRead];
					previousData.CopyTo(rawData, 0);
				}
				
				Array.Copy(buffer, 0, rawData, offset, bytesRead);
			}

			responseStream.Close();

			return rawData;
		}

		void MergeCookies(HttpWebResponse webResponse)
		{
			if (webResponse.Cookies != null)
			{
				foreach (Cookie newCookie in webResponse.Cookies)
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
