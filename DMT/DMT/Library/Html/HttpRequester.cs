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
	using System.Drawing;
	using System.IO;
	using System.Linq;
	using System.Net;
	using System.Text;
	using System.Threading.Tasks;

	/// <summary>
	/// Performs HTTP requests
	/// </summary>
	public class HttpRequester : IHttpRequester
	{
		const string UserAgent = "Dual Monitor Tools/2.1";

		// max redirects for a single request, before we give up
		const int MaxRedirects = 5;

		IHttpConnectionManager _connectionManager;

		/// <summary>
		/// Initialises a new instance of the <see cref="HttpRequester" /> class.
		/// </summary>
		/// <param name="connectionManager">Connection manager to use</param>
		public HttpRequester(IHttpConnectionManager connectionManager)
		{
			_connectionManager = connectionManager;
		}

		/// <summary>
		/// Gets the last Uri that responded
		/// </summary>
		public Uri LastResponseUri { get; private set; }

		/// <summary>
		/// Gets the page
		/// </summary>
		/// <param name="uri">Location of page</param>
		/// <param name="testPage">Page name - used for fake requests only</param>
		/// <param name="repliedConnection">The connection that the response came in on</param>
		/// <returns>The contents of the page</returns>
		public string GetPage(Uri uri, string testPage, out HttpConnection repliedConnection)
		{
			string pageData = string.Empty;

			HttpConnection connection = _connectionManager.GetConnection(uri);
			HttpWebRequest webRequest = CreateGetRequest(connection, uri);
			HttpResponseData responseData = GetResponse(connection, webRequest, out repliedConnection);
			if (responseData.StatusCode == HttpStatusCode.OK)
			{
				pageData = responseData.EncodeAsString();
				LastResponseUri = webRequest.Address;
			}

			return pageData;
		}

		/// <summary>
		/// Gets the image
		/// </summary>
		/// <param name="uri">Location of image</param>
		/// <returns>The image</returns>
		public Image GetImage(Uri uri)
		{
			Image image = null;

			HttpConnection connection = _connectionManager.GetConnection(uri);
			HttpWebRequest webRequest = CreateGetRequest(connection, uri);
			HttpConnection repliedConnection;
			HttpResponseData responseData = GetResponse(connection, webRequest, out repliedConnection);
#if DEBUG
			SaveImageToDisk(responseData);
#endif
			if (responseData.StatusCode == HttpStatusCode.OK)
			{
				image = responseData.EncodeAsImage();
				LastResponseUri = webRequest.Address;
			}

			return image;
		}

		/// <summary>
		/// Gets the binary data
		/// </summary>
		/// <param name="uri">Location of data</param>
		/// <param name="data">Returned data</param>
		/// <returns>HTTP status code</returns>
		public HttpStatusCode GetData(Uri uri, ref byte[] data)
		{
			data = null;
			HttpConnection connection = _connectionManager.GetConnection(uri);
			HttpWebRequest webRequest = CreateGetRequest(connection, uri);
			HttpConnection repliedConnection;
			HttpResponseData responseData = GetResponse(connection, webRequest, out repliedConnection);
			if (responseData.StatusCode == HttpStatusCode.OK)
			{
				data = responseData.RawData;
				LastResponseUri = webRequest.Address;
			}

			return responseData.StatusCode;
		}

		#region Request creation

		HttpWebRequest CreateGetRequest(HttpConnection connection, Uri uri)
		{
			HttpWebRequest webRequest = CreateRequest(connection, uri);
			webRequest.Method = "GET";

			return webRequest;
		}

		HttpWebRequest CreatePostRequest(HttpConnection connection, Uri uri, List<Tuple<string, string>> postParameters)
		{
			HttpWebRequest webRequest = CreateRequest(connection, uri);
			webRequest.Method = "POST";
			webRequest.ContentType = "application/x-www-form-urlencoded";

			// we will handle the redirection
			webRequest.AllowAutoRedirect = false;

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

		HttpWebRequest CreateRequest(HttpConnection connection, Uri uri)
		{
			HttpWebRequest webRequest = (HttpWebRequest)HttpWebRequest.Create(uri);
			if (connection.LastUri != null)
			{
				webRequest.Referer = connection.LastUri.AbsolutePath;
			}

			webRequest.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
			webRequest.Headers.Add("Accept-Language", "en-us,en;q=0.5");
			webRequest.Headers.Add("Accept-Charset", "ISO-8859-1,utf-8;q=0.7,*;q=0.7");
			webRequest.UserAgent = UserAgent;

			CookieContainer cookieContainer = new CookieContainer();
			cookieContainer.Add(connection.Cookies);
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

				sb.Append(HttpHelper.UrlEncode(postParameter.Item1, urlEncoding));
				if (postParameter.Item2 != null)
				{
					sb.Append("=");
					sb.Append(HttpHelper.UrlEncode(postParameter.Item2, urlEncoding));
				}
			}

			return sb.ToString();
		}
		#endregion

		#region Response handling
		HttpResponseData GetResponse(HttpConnection connection, HttpWebRequest webRequest, out HttpConnection repliedConnection)
		{
			HttpResponseData responseData = null;
			HttpConnection curConnection = connection;
			HttpWebResponse webResponse = null;

			webResponse = CatchGetResponse(webRequest);

			int redirectNum = 0;
			if (IsARedirect(webResponse.StatusCode) && redirectNum < MaxRedirects)
			{
				redirectNum++;

				// save the cookies
				curConnection.MergeCookies(webResponse.Cookies);

				// save the uri of the page that actually reponded to us
				curConnection.LastUri = webRequest.Address;

				// create a new request to the redirected page
				Uri uri = GetFullUri(webResponse.GetResponseHeader("Location"), curConnection);

				// this may be on another host, so make sure we have the correct connection
				curConnection = _connectionManager.GetConnection(uri);
				HttpWebRequest redirectedWebRequest = CreateGetRequest(curConnection, uri);

				// close existing response
				webResponse.Close();

				// now start new request
				webResponse = CatchGetResponse(webRequest);
			}

			// save the cookies
			curConnection.MergeCookies(webResponse.Cookies);

			// save the uri of the page that actually reponded to us
			curConnection.LastUri = webRequest.Address;

			byte[] rawData = ReadResponseStream(webResponse);
			responseData = new HttpResponseData(webResponse.StatusCode, rawData, webResponse.ContentType);

			webResponse.Close();

			repliedConnection = curConnection;
			return responseData;
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

		bool IsARedirect(HttpStatusCode statusCode)
		{
			// for now, just support 302, 301
			if (statusCode == HttpStatusCode.Redirect || statusCode == HttpStatusCode.MovedPermanently)
			{
				return true;
			}

			return false;
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

		Uri GetFullUri(string relativeUrl, HttpConnection parentConnection)
		{
			Uri uri;
			if (parentConnection != null)
			{
				uri = parentConnection.ExpandPath(relativeUrl);
			}
			else
			{
				uri = new Uri(relativeUrl);
			}

			return uri;
		}

		#endregion

#if DEBUG
		void SaveImageToDisk(HttpResponseData responseData)
		{
			// create a unique filename
			string filename = @"G:\Wallpaper\";
			filename += Guid.NewGuid().ToString();
			filename += ".jpg";
			File.WriteAllBytes(filename, responseData.RawData);
		}
#endif
	}
}
