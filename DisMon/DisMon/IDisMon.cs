#region copyright
// This file is part of Dual Monitor Tools which is a set of tools to assist
// users with multiple monitor setups.
// Copyright (C) 2010  Gerald Evans
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
using System.Text;

namespace DisMon
{
	/// <summary>
	/// Interface required to implement O/S specific monitor changes
	/// </summary>
	interface IDisMon
	{
		//public void Init();

		int Count();
		//void Revert();
		void Reset();
		void MarkAsPrimary(int newPrimaryIndex);
		void MarkAllSecondaryAsDisabled();
		//bool IsDisabled(int monitorIndex);
		void MarkAsDisabled(int monitorIndex);
		//void MarkAsEnabled(int enableIndex);
		bool ApplyChanges();
		void Restore();
	}
}
