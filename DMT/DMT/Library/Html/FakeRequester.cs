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
	/// Fake HttpRequester which retrieves HTML pages from the MockData directory.
	/// Allows testing out screen scraping with pre-cached copy of pages
	/// without the need to do real HTTP requests.
	/// </summary>
	public class FakeRequester : IHttpRequester
	{
		/// <summary>
		/// Gets or sets the last Uri that responded
		/// </summary>
		public Uri LastResponseUri { get; protected set; }

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

			string testPath = @"..\..\..\MockData\" + testPage + ".htm";
			pageData = File.ReadAllText(testPath, Encoding.UTF8);
			LastResponseUri = uri;
			repliedConnection = null;

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

			image = Image.FromFile(@"..\..\..\MockData\image.jpg");
			LastResponseUri = uri;

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
			data = File.ReadAllBytes(@"..\..\..\MockData\data.tst");
			LastResponseUri = uri;

			return HttpStatusCode.OK;
		}
	}
}
