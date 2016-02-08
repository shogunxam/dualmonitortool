#region copyright
// This file is part of Dual Monitor Tools which is a set of tools to assist
// users with multiple monitor setups.
// Copyright (C) 2010-2015  Gerald Evans
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

namespace DMT.Library.Binding
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel;
	using System.Reflection;
	using System.Text;

	/// <summary>
	/// Compares property values of 2 objects. 
	/// </summary>
	/// <typeparam name="T">The type of the collection being compared</typeparam>
	public class PropertyComparer<T> : IComparer<T>
	{
		PropertyInfo _propertyInfo;
		ListSortDirection _sortDirection;

		/// <summary>
		/// Initialises a new instance of the <see cref="{PropertyComparer&lt;T&gt;}" /> class.
		/// </summary>
		/// <param name="propertyName">The property to compare</param>
		/// <param name="sortDirection">The sort direction</param>
		public PropertyComparer(string propertyName, ListSortDirection sortDirection)
		{
			_propertyInfo = typeof(T).GetProperty(propertyName);
			_sortDirection = sortDirection;
		}

		/// <summary>
		/// implementation of IComparer.Compare()
		/// </summary>
		/// <param name="firstRecord">First record</param>
		/// <param name="secondRecord">Second record</param>
		/// <returns>-1, 0 or +1 depending on the result of the comparison</returns>
		public int Compare(T firstRecord, T secondRecord)
		{
			int ret = 0;

			// get the values for the required property for these records
			object firstRecordField = _propertyInfo.GetValue(firstRecord, null);
			object secondRecordField = _propertyInfo.GetValue(secondRecord, null);

			// if the field supports IComparable, then use this
			if (firstRecordField is IComparable)
			{
				ret = (firstRecordField as IComparable).CompareTo(secondRecordField);
			}
			else if (firstRecordField != null && secondRecordField != null)
			{
				// simple fallback
				ret = firstRecordField.ToString().CompareTo(secondRecordField.ToString());
			}
			else if (firstRecordField != null && secondRecordField == null)
			{
				ret = 1;
			}
			else if (firstRecordField == null && secondRecordField != null)
			{
				ret = -1;
			}

			if (_sortDirection == ListSortDirection.Descending)
			{
				// reverse result
				ret = -ret;
			}

			return ret;
		}
	}
}
