#region copyright
// This file is part of Dual Monitor Tools which is a set of tools to assist
// users with multiple monitor setups.
// Copyright (C) 2018  Gerald Evans
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

namespace DMT.Library.Html
{
	static class NetHelper
	{
		public static void InitServicePointManager()
		{
			// We need to explicitly support all SSL protocols to maximise the number of servers we can communicate with

			// .Net 4.0 does not have defines for TLS 1.1 and 1.2 so we define them here
			System.Net.SecurityProtocolType Tls11 = (System.Net.SecurityProtocolType)768;	// .NET 4.5 can use System.Net.SecurityProtocolType.Tls11
			System.Net.SecurityProtocolType Tls12 = (System.Net.SecurityProtocolType)3072;	// .NET 4.5 can use System.Net.SecurityProtocolType.Tls12

			// We need .Net 4.6.2+ to have TLS 1.2 used by default, (.NET 4.0 only has SSL3 and TLS 1.0 defined by default)
			// so we need to add it in here together with TLS 1.1
			System.Net.SecurityProtocolType protocolType = System.Net.ServicePointManager.SecurityProtocol;
			protocolType |= Tls11;
			protocolType |= Tls12;
			System.Net.ServicePointManager.SecurityProtocol = protocolType;
		}
	}
}
