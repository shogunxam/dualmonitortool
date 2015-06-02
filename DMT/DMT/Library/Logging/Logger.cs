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
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMT.Library.Logging
{
	public class Logger : ILogger
	{
		public void LogException(string source, Exception ex)
		{
			AddToLog(source, "Exception", ex.Message);
		}

		public void LogInfo(string source, string format, params object[] formatParams)
		{
			AddToLog(source, "Info", string.Format(format, formatParams));
		}

		public void LogError(string source, string format, params object[] formatParams)
		{
			AddToLog(source, "Error", string.Format(format, formatParams));
		}

		void AddToLog(string source, string type, string message)
		{
			string logLine = string.Format("{0}|{1}|{2}|{3}", DateTime.Now.ToString("HH:mm:ss"), source, type, message);
			Trace.TraceError(logLine);
		}
	}
}
