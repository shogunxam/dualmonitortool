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
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMT.Library.Html
{
	/// <summary>
	/// Handles re-use of connections
	/// </summary>
	public class HttpConnectionManager
	{
		List<HttpConnection> _connections = new List<HttpConnection>();

		public HttpConnection GetConnection(Uri uri)
		{
			foreach (HttpConnection connection in _connections)
			{
				if (connection.CanHandle(uri))
				{
					return connection;
				}
			}

			HttpConnection newConnection = new HttpConnection(uri);
			_connections.Add(newConnection);
			return newConnection;
		}
	}
}
