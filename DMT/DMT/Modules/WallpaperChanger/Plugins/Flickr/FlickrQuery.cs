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

namespace DMT.Modules.WallpaperChanger.Plugins.Flickr
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using System.Web;

	using DMT.Library.Html;
	
	/// <summary>
	/// Query to be sent to the Flickr API site
	/// </summary>
	class FlickrQuery
	{
		StringBuilder _query;

		/// <summary>
		/// Initialises a new instance of the <see cref="FlickrQuery" /> class.
		/// </summary>
		/// <param name="method">Flickr API method</param>
		/// <param name="apiKey">Flickr API key</param>
		public FlickrQuery(string method, string apiKey)
		{
			_query = new StringBuilder();
			Add("method", method);
			Add("api_key", apiKey);
		}

		/// <summary>
		/// Adds a parameter to the query
		/// </summary>
		/// <param name="name">Name of parameter to add</param>
		/// <param name="value">Value of parameter to add</param>
		public void Add(string name, string value)
		{
			if (_query.Length > 0)
			{
				_query.Append("&");
			}

			_query.Append(HttpHelper.UrlEncode(name));
			if (value != null)
			{
				_query.Append("=");
				_query.Append(HttpHelper.UrlEncode(value));
			}
		}

		/// <summary>
		/// String representation of query ready to send to Flickr
		/// </summary>
		/// <returns>Query as a string</returns>
		public override string ToString()
		{
			return _query.ToString();
		}
	}
}
