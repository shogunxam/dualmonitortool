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
using System.ComponentModel;
using System.Reflection;

namespace DualLauncher
{
	public class PropertyComparer<T> : IComparer<T>
	{
		private PropertyInfo propertyInfo;
		private ListSortDirection sortDirection;

		public PropertyComparer(string propertyName, ListSortDirection sortDirection)
		{
			this.propertyInfo = typeof(T).GetProperty(propertyName);
			this.sortDirection = sortDirection;
		}

		public int Compare(T xRecord, T yRecord)
		{
			int ret = 0;

			// get the values for the required property for these records
			object xField = propertyInfo.GetValue(xRecord, null);
			object yField = propertyInfo.GetValue(yRecord, null);

			// make sure the field supports IComparable
			if (xField is IComparable)
			{
				ret = (xField as IComparable).CompareTo(yField);
			}
			else if (xField != null && yField != null)
			{
				// simple fallback
				ret = xField.ToString().CompareTo(yField.ToString());
			}
			else if (xField != null && yField == null)
			{
				ret = 1;
			}
			else if (xField == null && yField != null)
			{
				ret = -1;
			}
			if (sortDirection == ListSortDirection.Descending)
			{
				// reverse result
				ret = -ret;
			}

			return ret;
		}
	}
}
