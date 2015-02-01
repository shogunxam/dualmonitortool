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
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DualMonitorTools.DualWallpaperChanger.Html
{
	/// <summary>
	/// Performs HTTP requests
	/// </summary>
	public class HttpRequester : IHttpRequester
	{
		public string GetPage(HttpConnection connection, Uri uri, string testPage)
		{
			string pageData = string.Empty;

			HttpWebRequest webRequest = connection.CreateGetRequest(uri);
			HttpResponseData responseData = connection.GetResponse(webRequest);
			if (responseData.StatusCode == HttpStatusCode.OK)
			{
				pageData = responseData.EncodeAsString();
			}

			return pageData;
		}

		public Image GetImage(HttpConnection connection, Uri uri)
		{
			Image image = null;

			HttpWebRequest webRequest = connection.CreateGetRequest(uri);
			HttpResponseData responseData = connection.GetResponse(webRequest);
#if DEBUG
			SaveImageToDisk(responseData);
#endif
			if (responseData.StatusCode == HttpStatusCode.OK)
			{
				image = responseData.EncodeAsImage();
			}

			return image;
		}

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
