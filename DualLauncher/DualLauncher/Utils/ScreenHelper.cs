#region copyright
// This file is part of Dual Monitor Tools which is a set of tools to assist
// users with multiple monitor setups.
// Copyright (C) 2009-2010  Gerald Evans
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
using System.Windows.Forms;
using System.Text;

namespace DualLauncher
{
	/// <summary>
	/// Utility class to help in handling multiple screens.
	/// </summary>
	static class ScreenHelper
	{


		/// <summary>
		/// Converts a Win32 RECT to a Rectangle.
		/// </summary>
		/// <param name="rect">Win 32 RECT</param>
		/// <returns>.NET Rectangle</returns>
		public static Rectangle RectToRectangle(ref Win32.RECT rect)
		{
			Rectangle rectangle = new Rectangle(rect.left, rect.top, rect.right - rect.left, rect.bottom - rect.top);

			return rectangle;
		}

		/// <summary>
		/// Gets a list of all windows that we think should be allowed
		/// to be moved between screens.
		/// </summary>
		/// <returns>List of HWND's belonging to application windows.</returns>
		private static List<IntPtr> GetVisibleApplicationWindows()
		{
			List<IntPtr> hWndList = new List<IntPtr>();
			IntPtr hWndShell = Win32.GetShellWindow();
			Rectangle vitrualDesktopRect = GetVitrualDesktopRect();

			// use anonymous method to simplify access to the windows list
			Win32.EnumWindowsProc windowVisiter = delegate(IntPtr hWnd, uint lParam)
			{
				if (hWnd == hWndShell)
				{
					// ignore the shell (Program Manager) window
				}
				else if (!Win32.IsWindowVisible(hWnd))
				{
					// ignore any windows without WS_VISIBLE
				}
				else
				{
					Win32.WINDOWPLACEMENT windowPlacement = new Win32.WINDOWPLACEMENT();
					Win32.GetWindowPlacement(hWnd, ref windowPlacement);
					Rectangle windowRectangle = RectToRectangle(ref windowPlacement.rcNormalPosition);
					if (!windowRectangle.IntersectsWith(vitrualDesktopRect))
					{
						// window has been deliberately positioned offscreen, so leave alone
					}
					else
					{
						int exStyle = Win32.GetWindowLong(hWnd, Win32.GWL_EXSTYLE);
						if ((exStyle & Win32.WS_EX_TOOLWINDOW) != 0)
						{
							// This is a tool window - leave alone
						}
						else
						{
							// we should be able to move this window without any ill effect
							hWndList.Add(hWnd);
						}
					}
				}
				return true;
			};
			Win32.EnumWindows(windowVisiter, 0);

			return hWndList;
		}

		/// <summary>
		/// Get the bounding rectangle that covers the working area of all screens
		/// </summary>
		/// <returns></returns>
		public static Rectangle GetVitrualWorkingRect()
		{
			Rectangle boundingRect = new Rectangle();
			for (int screenIndex = 0; screenIndex < Screen.AllScreens.Length; screenIndex++)
			{
				if (screenIndex == 0)
				{
					boundingRect = Screen.AllScreens[screenIndex].WorkingArea;
				}
				else
				{
					boundingRect = Rectangle.Union(boundingRect, Screen.AllScreens[screenIndex].WorkingArea);
				}
			}

			return boundingRect;
		}

		/// <summary>
		/// Get the bounding rectangle that covers all areas of all screens
		/// </summary>
		/// <returns></returns>
		public static Rectangle GetVitrualDesktopRect()
		{
			Rectangle boundingRect = new Rectangle();
			for (int screenIndex = 0; screenIndex < Screen.AllScreens.Length; screenIndex++)
			{
				if (screenIndex == 0)
				{
					boundingRect = Screen.AllScreens[screenIndex].Bounds;
				}
				else
				{
					boundingRect = Rectangle.Union(boundingRect, Screen.AllScreens[screenIndex].Bounds);
				}
			}

			return boundingRect;
		}
	}
}
