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
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace DisMon
{
	/// <summary>
	/// Singleton that allows disabling and re-eanbling of minitors
	/// 
	/// No support for Windows 7 yet.
	/// </summary>
	sealed class DisMon
	{
		static readonly DisMon instance = new DisMon();

		private IDisMon disMonImplementation;

		// Explicit static constructor to tell C# compiler
		// not to mark type as beforefieldinit
		static DisMon()
		{
		}

		// private ctor
		DisMon()
		{
			if (OsHelper.IsWin7OrLater())
			{
				disMonImplementation = new DisMon7();
			}
			else
			{
				disMonImplementation = new DisMon6();
			}
		}

		/// <summary>
		/// Static property to get access to the single instance of this class.
		/// </summary>
		public static DisMon Instance
		{
			get
			{
				return instance;
			}
		}

		/// <summary>
		/// The number of monitors we know about.
		/// </summary>
		public int Count
		{
			get
			{
				return disMonImplementation.Count();
			}
		}

		public void Reset()
		{
			disMonImplementation.Reset();
		}

		/// <summary>
		/// Mark the specified monitor as being the primary monitor.
		/// 
		/// WARNING: if this monitor is currently disabled, then it will be
		/// re-enabled with immediate effect.
		/// </summary>
		/// <param name="newPrimaryIndex">Zero based index of monitor.</param>
		public void MarkAsPrimary(int newPrimaryIndex)
		{
			disMonImplementation.MarkAsPrimary(newPrimaryIndex);
		}

		/// <summary>
		/// Disables all secondary monitors
		/// </summary>
		/// <returns>true if one or more monitors were disabled</returns>
		public void MarkAllSecondaryAsDisabled()
		{
			disMonImplementation.MarkAllSecondaryAsDisabled();
		}

		/// <summary>
		/// Mark the specified monitor as disabled.
		/// </summary>
		/// <param name="monitorIndex">Zero based index of monitor.</param>
		public void MarkAsDisabled(int monitorIndex)
		{
			disMonImplementation.MarkAsDisabled(monitorIndex);
		}

		///// <summary>
		///// Mark the specified monitor as enabled.
		///// </summary>
		///// <param name="enableIndex">Zero based index of monitor.</param>
		//public void MarkAsEnabled(int enableIndex)
		//{
		//    disMonImplementation.MarkAsEnabled(enableIndex);
		//}

		/// <summary>
		/// Updates all of the monitors with any pending changes.
		/// </summary>
		/// <returns>true if any change has been made to any monitor.</returns>
		public bool ApplyChanges()
		{
			return disMonImplementation.ApplyChanges();
		}

		/// <summary>
		/// Restore all monitors to the state that they were in at construction.
		/// </summary>
		public void Restore()
		{
			disMonImplementation.Restore();
		}
	}
}
