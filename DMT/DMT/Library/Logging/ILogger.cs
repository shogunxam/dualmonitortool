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

namespace DMT.Library.Logging
{
	using System;
	using System.Collections.Generic;
	using System.Text;

	/// <summary>
	/// Interface to logger
	/// </summary>
	public interface ILogger
	{
		/// <summary>
		/// Log an exception
		/// </summary>
		/// <param name="source">Source or module name requesting the log</param>
		/// <param name="ex">Exception to log</param>
		void LogException(string source, Exception ex);

		/// <summary>
		/// Log an information message
		/// </summary>
		/// <param name="source">Source or module name requesting the log</param>
		/// <param name="format">Format string</param>
		/// <param name="formatParams">Parameters for format string</param>
		void LogInfo(string source, string format, params object[] formatParams);

		/// <summary>
		/// Log an error message
		/// </summary>
		/// <param name="source">Source or module name requesting the log</param>
		/// <param name="format">Format string</param>
		/// <param name="formatParams">Parameters for format string</param>
		void LogError(string source, string format, params object[] formatParams);
	}
}
