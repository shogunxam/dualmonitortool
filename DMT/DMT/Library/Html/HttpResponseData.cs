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
	/// Response received from a HTTP request
	/// </summary>
	public class HttpResponseData
	{
		/// <summary>
		/// Initialises a new instance of the <see cref="HttpResponseData" /> class.
		/// </summary>
		/// <param name="statusCode">The response status code</param>
		/// <param name="rawData">The response raw data</param>
		/// <param name="contentType">The response content type</param>
		public HttpResponseData(HttpStatusCode statusCode, byte[] rawData, string contentType)
		{
			StatusCode = statusCode;
			RawData = rawData;
			ContentType = contentType;
		}

		/// <summary>
		/// Gets or sets the response HTTP status code
		/// </summary>
		public HttpStatusCode StatusCode { get; set; }

		/// <summary>
		/// Gets the raw data received in the response
		/// </summary>
		public byte[] RawData { get; private set; }

		/// <summary>
		/// Gets the content type of the response
		/// </summary>
		public string ContentType { get; private set; }

		/// <summary>
		/// Returns the response data as a string
		/// </summary>
		/// <returns>The returned content</returns>
		public string EncodeAsString()
		{
			// assume response is always UTF-8 for now
			Encoding encoding = Encoding.UTF8;

			string pageData = null;
			using (MemoryStream rawDataStream = new MemoryStream(RawData))
			{
				StreamReader streamReader = new StreamReader(rawDataStream, encoding);
				pageData = streamReader.ReadToEnd();
			}

			return pageData;
		}

		/// <summary>
		/// Returns the response data as an image
		/// </summary>
		/// <returns>The returned content</returns>
		public Image EncodeAsImage()
		{
			Image image = null;

			using (MemoryStream rawDataStream = new MemoryStream(RawData))
			{
				image = Image.FromStream(rawDataStream);
			}

			return image;
		}
	}
}
