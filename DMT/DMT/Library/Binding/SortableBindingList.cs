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
	using System.Collections;
	using System.Collections.Generic;
	using System.ComponentModel;
	using System.Diagnostics;
	using System.Reflection;
	using System.Text;

	/// <summary>
	/// This is a sortable implementation of a BindingList
	/// It is based on <see href="http://msdn.microsoft.com/en-us/library/aa480736.aspx" />
	/// and <see href="http://msdn.microsoft.com/en-us/library/ms993236.aspx" />
	/// </summary>
	/// <typeparam name="T">Type of item in collection</typeparam>
	public class SortableBindingList<T> : BindingList<T>
	{
		bool _isSortedCore;
		ListSortDirection _sortDirectionCore;
		PropertyDescriptor _sortPropertyCore;

		/// <summary>
		/// Gets a value indicating it sorting is supported
		/// </summary>
		protected override bool SupportsSortingCore
		{
			get { return true; }
		}

		/// <summary>
		/// Gets a value indicating if the list is currently sorted
		/// </summary>
		protected override bool IsSortedCore
		{
			get { return _isSortedCore; }
		}

		/// <summary>
		/// Gets the current direction of the sort
		/// </summary>
		protected override ListSortDirection SortDirectionCore
		{
			get { return _sortDirectionCore; }
		}

		/// <summary>
		/// Gets the property that we are sorting on
		/// </summary>
		protected override PropertyDescriptor SortPropertyCore
		{
			get { return _sortPropertyCore; }
		}

		/// <summary>
		/// Sort on the given property and direction
		/// </summary>
		/// <param name="property">Name of property to sort on</param>
		/// <param name="sortDirection">Sort direction</param>
		public void Sort(PropertyDescriptor property, ListSortDirection sortDirection)
		{
			ApplySortCore(property, ListSortDirection.Ascending);
		}

		/// <summary>
		/// Inserts the records into the correct sorted position
		/// </summary>
		/// <param name="record">Record to insert</param>
		public void Insert(T record)
		{
			// insert the record anywhere and then sort
			// TODO: alternatively we could find the correct insertion point
			this.Insert(0, record);
			ReSort();
		}

		/// <summary>
		/// Makes sure the list is sorted 
		/// </summary>
		public void ReSort()
		{
			if (_isSortedCore)
			{
				ApplySortCore(_sortPropertyCore, _sortDirectionCore);
			}
		}

		/// <summary>
		/// Performs the actual sorting
		/// </summary>
		/// <param name="property">Name of property to sort on</param>
		/// <param name="sortDirection">Sort direction</param>
		protected override void ApplySortCore(PropertyDescriptor property, ListSortDirection sortDirection)
		{
			_sortPropertyCore = property;
			_sortDirectionCore = sortDirection;

			if (property != null)
			{
				PropertyComparer<T> comparer = new PropertyComparer<T>(property.Name, sortDirection);

				List<T> listItems = this.Items as List<T>;
				listItems.Sort(comparer);

				_isSortedCore = true;

				// Raise the ListChanged event so bound controls refresh their
				// values.
				OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
			}
		}

		/// <summary>
		/// TODO: can this be removed?
		/// </summary>
		protected override void RemoveSortCore()
		{
			// don't think we need to support this
		}

		/// <summary>
		/// TODO: can this be removed?
		/// </summary>
		/// <param name="property">Property name</param>
		/// <param name="key">Key to find</param>
		/// <returns>Index of found key, or -1 if not found</returns>
		protected override int FindCore(PropertyDescriptor property, object key)
		{
			// don't think we need to support this

			////if (key != null)
			////{
			////    T item;
			////    PropertyInfo propertyInfo = typeof(T).GetProperty(property.Name);
			////    for (int i = 0; i < Count; i++)
			////    {
			////        item = Items[i] as T;
			////        if (propertyInfo.GetValue(item, null).Equals(key))
			////        {
			////            return i;
			////        }
			////    }
			////}
			return -1;
		}
	}
}
