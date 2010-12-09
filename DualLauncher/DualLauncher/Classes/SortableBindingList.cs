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
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Reflection;

using System.Diagnostics;

// based on http://msdn.microsoft.com/en-us/library/aa480736.aspx
// and http://msdn.microsoft.com/en-us/library/ms993236.aspx

namespace DualLauncher
{
	public class SortableBindingList<T> : BindingList<T>
	{

		protected override bool SupportsSortingCore
		{
			get { return true; }
		}

		private bool isSortedCore;
		protected override bool IsSortedCore
		{
			get { return isSortedCore; }
		}

		ListSortDirection sortDirectionCore;
		protected override ListSortDirection SortDirectionCore
		{
			get { return sortDirectionCore; }
		}

		PropertyDescriptor sortPropertyCore;
		protected override PropertyDescriptor SortPropertyCore
		{
			get { return sortPropertyCore; }
		}

		public void Sort(PropertyDescriptor property, ListSortDirection sortDirection)
		{
			ApplySortCore(property, ListSortDirection.Ascending);
		}

		protected override void ApplySortCore(PropertyDescriptor property, ListSortDirection sortDirection)
		{
			sortPropertyCore = property;
			sortDirectionCore = sortDirection;

			if (property != null)
			{
				PropertyComparer<T> comparer = new PropertyComparer<T>(property.Name, sortDirection);

				//
				List<T> listItems = this.Items as List<T>;
				listItems.Sort(comparer);

				isSortedCore = true;

				// Raise the ListChanged event so bound controls refresh their
				// values.
				OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
			}
		}

		public void Insert(T record)
		{
			// insert the record anywhere and then sort
			// TODO: alternatively we could find the correct insertion point
			this.Insert(0, record);
			ReSort();
		}

		private void ReSort()
		{
			if (isSortedCore)
			{
				ApplySortCore(sortPropertyCore, sortDirectionCore);
			}
		}

		protected override void RemoveSortCore()
		{
			// don't think we need to support this
		}

		//public void RemoveSort()
		//{
		//    RemoveSortCore();
		//}

		protected override int FindCore(PropertyDescriptor property, object key)
		{
			//if (key != null)
			//{
			//    T item;
			//    PropertyInfo propertyInfo = typeof(T).GetProperty(property.Name);
			//    for (int i = 0; i < Count; i++)
			//    {
			//        item = Items[i] as T;
			//        if (propertyInfo.GetValue(item, null).Equals(key))
			//        {
			//            return i;
			//        }
			//    }
			//}
			return -1;
		}

	}
}
