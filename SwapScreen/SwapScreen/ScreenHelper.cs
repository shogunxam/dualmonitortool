#region copyright
// This file is part of Dual Monitor Tools which is a set of tools to assist
// users with multiple monitor setups.
// Copyright (C) 2009  Gerald Evans
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

namespace SwapScreen
{
	/// <summary>
	/// Utility class to help in handling multiple screens.
	/// </summary>
	static class ScreenHelper
	{
		/// <summary>
		/// Attempts to minimize all visible application windows on
		/// the given screen.
		/// </summary>
		/// <param name="screenIndex">Zero based index of screen to show.</param>
		public static void ShowDestktop(int screenIndex)
		{
			if (screenIndex < 0 || screenIndex >= Screen.AllScreens.Length)
			{
				return;
			}

			Screen curScreen = Screen.AllScreens[screenIndex];
			List<IntPtr> hWndList = GetVisibleApplicationWindows();

			// for each visible application window...
			foreach (IntPtr hWnd in hWndList)
			{
				Win32.WINDOWPLACEMENT windowPlacement = new Win32.WINDOWPLACEMENT();
				Win32.GetWindowPlacement(hWnd, ref windowPlacement);
				Rectangle windowRectangle = RectToRectangle(ref windowPlacement.rcNormalPosition);
				if (windowRectangle.IntersectsWith(curScreen.Bounds))
				{
					// this window does exist (maybe partially) on this screen
					// so lets minimise it
					windowPlacement.showCmd = Win32.SW_SHOWMINIMIZED;
					Win32.SetWindowPlacement(hWnd, ref windowPlacement);
				}
			}
		}

		/// <summary>
		/// Each visible application window is moved to the next screen.
		/// </summary>
		public static void SwapScreens()
		{
			List<IntPtr> hWndList = GetVisibleApplicationWindows();

			// for each visible application window...
			foreach (IntPtr hWnd in hWndList)
			{
				MoveWindowToNext(hWnd);
			}
		}

		/// <summary>
		/// Gets a list of all windows that we think should be allowed
		/// to be moved between screens.
		/// </summary>
		/// <returns>List of HWND's belonging to application windows.</returns>
		public static List<IntPtr> GetVisibleApplicationWindows()
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
		/// Get the bounding rectangle that covers all screens
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

		#region Individual Window movement
		/// <summary>
		/// Moves the active window to the next screen.
		/// </summary>
		public static void MoveActiveWindow()
		{
			IntPtr hWnd = Win32.GetForegroundWindow();
			if (hWnd != null)
			{
				MoveWindowToNext(hWnd);
			}
		}

		/// <summary>
		/// Moves the window corresponding to the specified HWND
		/// to the next screen.
		/// </summary>
		/// <param name="hWnd">HWND of window to move.</param>
		public static void MoveWindowToNext(IntPtr hWnd)
		{
			Win32.WINDOWPLACEMENT windowPlacement = new Win32.WINDOWPLACEMENT();
			Win32.GetWindowPlacement(hWnd, ref windowPlacement);
			Rectangle curRect = new Rectangle(windowPlacement.rcNormalPosition.left,
										   windowPlacement.rcNormalPosition.top,
										   windowPlacement.rcNormalPosition.right - windowPlacement.rcNormalPosition.left,
										   windowPlacement.rcNormalPosition.bottom - windowPlacement.rcNormalPosition.top);
			Rectangle newRect = TransfromRectToNextScreen(ref curRect);
			uint oldShowCmd = windowPlacement.showCmd;
			if (oldShowCmd == Win32.SW_SHOWMINIMIZED || oldShowCmd == Win32.SW_SHOWMAXIMIZED)
			{
				windowPlacement.showCmd = Win32.SW_RESTORE;
				Win32.SetWindowPlacement(hWnd, ref windowPlacement);
				windowPlacement.showCmd = Win32.SW_SHOW;
				windowPlacement.rcNormalPosition.left = newRect.Left;
				windowPlacement.rcNormalPosition.top = newRect.Top;
				windowPlacement.rcNormalPosition.right = newRect.Right;
				windowPlacement.rcNormalPosition.bottom = newRect.Bottom;
				Win32.SetWindowPlacement(hWnd, ref windowPlacement);
				windowPlacement.showCmd = oldShowCmd;
				Win32.SetWindowPlacement(hWnd, ref windowPlacement);
			}
			else
			{
				windowPlacement.rcNormalPosition.left = newRect.Left;
				windowPlacement.rcNormalPosition.top = newRect.Top;
				windowPlacement.rcNormalPosition.right = newRect.Right;
				windowPlacement.rcNormalPosition.bottom = newRect.Bottom;
				Win32.SetWindowPlacement(hWnd, ref windowPlacement);
			}
		}

		/// <summary>
		/// Converts the co-ordinates for a rectangle on one screen
		/// to the smae place on the next screen.
		/// </summary>
		/// <param name="curRect"></param>
		/// <returns></returns>
		private static Rectangle TransfromRectToNextScreen(ref Rectangle curRect)
		{
			Rectangle nextRec = new Rectangle();
			nextRec = curRect;

			Screen curScreen = Screen.FromRectangle(curRect);
			int curScreenIndex = FindScreenIndex(curScreen);
			if (curScreenIndex >= 0)
			{
				int nextScreenIndex = (curScreenIndex + 1) % Screen.AllScreens.Length;
				if (nextScreenIndex != curScreenIndex)
				{
					// keep TLHC in next screen same as current screen (relative to the working araea
					Screen nextScreen = Screen.AllScreens[nextScreenIndex];
					nextRec.Offset(nextScreen.WorkingArea.Left - curScreen.WorkingArea.Left,
								   nextScreen.WorkingArea.Top - curScreen.WorkingArea.Top);
				}
			}

			return nextRec;
		}

		/// <summary>
		/// Finds the index within Screen.AllScreens[] that the passed screen is .
		/// </summary>
		/// <param name="screen">The screen whoose index we are trying to find</param>
		/// <returns>Zero based screen index, or -1 if screen not found</returns>
		private static int FindScreenIndex(Screen screen)
		{
			int screenIndex = -1;
			for (int i = 0; i < Screen.AllScreens.Length; i++)
			{
				// We cannnot compare the screen objects as Screen.FromRectangle()
				// creates a new instance of the screen, rather than returning the
				// one in the AllScreens array.
				// Also comparing the DeviceName does not always seem to work,
				// as have seen corrupt names (on XP SP3 with Catalyst 9.1).
				// So we just compare the Bounds rectangle.
				if (screen.Bounds == Screen.AllScreens[i].Bounds)
				{
					screenIndex = i;
					break;
				}
			}

			return screenIndex;
		}

		#endregion

		#region Debugging helpers
		public static void DumpAllWindows()
		{
			List<IntPtr> hWndList = GetVisibleApplicationWindows();

			List<string> log = new List<string>();

			// for each application window...
			foreach (IntPtr hWnd in hWndList)
			{
				Win32.WINDOWPLACEMENT windowPlacement = new Win32.WINDOWPLACEMENT();
				Win32.GetWindowPlacement(hWnd, ref windowPlacement);

				string windowText = "";
				int textLen = Win32.GetWindowTextLength(hWnd);
				if (textLen > 0)
				{
					StringBuilder sb = new StringBuilder(textLen);
					Win32.GetWindowText(hWnd, sb, textLen + 1);
					windowText = sb.ToString();
				}

				int exStyle = Win32.GetWindowLong(hWnd, Win32.GWL_EXSTYLE);
				string style = DumpExStyle(exStyle);

				string visible = "";
				if (Win32.IsWindowVisible(hWnd))
				{
					visible = "VISIBLE";
				}

				log.Add(string.Format("hWnd:{0} {1} \"{2}\" {3} {4}", hWnd, DumpWindowPlacement(windowPlacement), windowText, style, visible));
			}

			LogForm dlg = new LogForm(log);
			dlg.ShowDialog();
		}

		public static string DumpWindowPlacement(Win32.WINDOWPLACEMENT windowPlacement)
		{
			string ret;
			string flags = "";
			string showCmd = "";
			string minPos = "";
			string maxPos = "";
			string normalPos = "";

			if ((windowPlacement.flags & Win32.WPF_SETMINPOSITION) != 0)
			{
				flags += " WPF_SETMINPOSITION";
			}
			if ((windowPlacement.flags & Win32.WPF_RESTORETOMAXIMIZED) != 0)
			{
				flags += " WPF_RESTORETOMAXIMIZED";
			}

			switch (windowPlacement.showCmd)
			{
				case Win32.SW_HIDE:
					showCmd = "SW_HIDE";
					break;
				case Win32.SW_MINIMIZE:
					showCmd = "SW_MINIMIZE";
					break;
				case Win32.SW_RESTORE:
					showCmd = "SW_RESTORE";
					break;
				case Win32.SW_SHOW:
					showCmd = "SW_SHOW";
					break;
				case Win32.SW_SHOWMAXIMIZED:
					showCmd = "SW_SHOWMAXIMIZED";
					break;
				case Win32.SW_SHOWMINIMIZED:
					showCmd = "SW_SHOWMINIMIZED";
					break;
				case Win32.SW_SHOWMINNOACTIVE:
					showCmd = "SW_SHOWMINNOACTIVE";
					break;
				case Win32.SW_SHOWNA:
					showCmd = "SW_SHOWNA";
					break;
				case Win32.SW_SHOWNOACTIVATE:
					showCmd = "SW_SHOWNOACTIVATE";
					break;
				case Win32.SW_SHOWNORMAL:
					showCmd = "SW_SHOWNORMAL";
					break;
				default:
					showCmd = "???";
					break;
			}

			minPos = string.Format("({0}, {1})", windowPlacement.ptMinPosition.x, windowPlacement.ptMinPosition.y);
			maxPos = string.Format("({0}, {1})", windowPlacement.ptMaxPosition.x, windowPlacement.ptMaxPosition.y);
			normalPos = string.Format("({0}, {1}) - ({2}, {3})",
				windowPlacement.rcNormalPosition.left, windowPlacement.rcNormalPosition.top,
				windowPlacement.rcNormalPosition.right, windowPlacement.rcNormalPosition.bottom);

			ret = string.Format("flags:{0}, showCmd:{1}, minPos:{2}, maxPos:{3}, normalPos:{4}",
				flags, showCmd, minPos, maxPos, normalPos);

			return ret;
		}

		private static string DumpExStyle(int exStyle)
		{
			string style = "";
			if ((exStyle & Win32.WS_EX_TOPMOST) != 0)
			{
				style += " WS_EX_TOPMOST";
			}
			if ((exStyle & Win32.WS_EX_TRANSPARENT) != 0)
			{
				style += " WS_EX_TRANSPARENT";
			}
			if ((exStyle & Win32.WS_EX_TOOLWINDOW) != 0)
			{
				style += " WS_EX_TOOLWINDOW";
			}
			if ((exStyle & Win32.WS_EX_APPWINDOW) != 0)
			{
				style += " WS_EX_APPWINDOW";
			}

			return style;
		}
		#endregion
	}
}
