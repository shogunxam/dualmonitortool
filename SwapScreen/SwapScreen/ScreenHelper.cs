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

namespace SwapScreen
{
	/// <summary>
	/// Utility class to help in handling multiple screens.
	/// </summary>
	static class ScreenHelper
	{
		public static void ShowDesktop1()
		{
			ShowDesktop(0);
		}

		public static void ShowDesktop2()
		{
			ShowDesktop(1);
		}

		/// <summary>
		/// Attempts to minimize all visible application windows on
		/// the given screen.
		/// </summary>
		/// <param name="screenIndex">Zero based index of screen to show.</param>
		public static void ShowDesktop(int screenIndex)
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
		///  Minimise all but the active window
		/// </summary>
		public static void MinimiseAllButActive()
		{
			IntPtr hWndActive = Win32.GetForegroundWindow();
			List<IntPtr> hWndList = GetVisibleApplicationWindows();

			// for each visible application window...
			foreach (IntPtr hWnd in hWndList)
			{
				if (hWnd != hWndActive)
				{
					// not the active window, so lets minimise it
					MinimiseWindow(hWnd);
				}
			}
		}

		/// <summary>
		/// Each visible application window is moved to the next screen.
		/// </summary>
		public static void RotateScreensNext()
		{
			RotateScreens(+1);
		}

		/// <summary>
		/// Each visible application window is moved to the previous screen.
		/// </summary>
		public static void RotateScreensPrev()
		{
			RotateScreens(-1);
		}

		private static void RotateScreens(int delta)
		{
			List<IntPtr> hWndList = GetVisibleApplicationWindows();

			// for each visible application window...
			foreach (IntPtr hWnd in hWndList)
			{
				MoveWindowToNext(hWnd, delta);
			}
		}

		#region Individual Window manipulation
		/// <summary>
		/// Minimise the active window
		/// </summary>
		public static void MinimiseActive()
		{
			IntPtr hWnd = Win32.GetForegroundWindow();
			if (hWnd != null)
			{
				MinimiseWindow(hWnd);
			}
		}

		/// <summary>
		/// Minimise the specified window
		/// </summary>
		/// <param name="hWnd">HWND of window to minimise</param>
		public static void MinimiseWindow(IntPtr hWnd)
		{
			Win32.WINDOWPLACEMENT windowPlacement = new Win32.WINDOWPLACEMENT();
			Win32.GetWindowPlacement(hWnd, ref windowPlacement);
			windowPlacement.showCmd = Win32.SW_SHOWMINIMIZED;
			Win32.SetWindowPlacement(hWnd, ref windowPlacement);
		}

		/// <summary>
		/// Maximise the active window
		/// </summary>
		public static void MaximiseActive()
		{
			IntPtr hWnd = Win32.GetForegroundWindow();
			if (hWnd != null)
			{
				MaximiseWindow(hWnd);
			}
		}

		/// <summary>
		/// Maximise the specified window, or restore it if it's already maximised
		/// </summary>
		/// <param name="hWnd">HWND of window to maximise</param>
		public static void MaximiseWindow(IntPtr hWnd)
		{
			// only allow the window to be maximised if it has a maximise box
			int style = Win32.GetWindowLong(hWnd, Win32.GWL_STYLE);
			if ((style & Win32.WS_MAXIMIZEBOX) != 0)
			{
				Win32.WINDOWPLACEMENT windowPlacement = new Win32.WINDOWPLACEMENT();
				Win32.GetWindowPlacement(hWnd, ref windowPlacement);
				// check if the window is already maximised
				if ((style & Win32.WS_MAXIMIZE) == 0)
				{
					// not maximised - so maximise
					windowPlacement.showCmd = Win32.SW_SHOWMAXIMIZED;
				}
				else
				{
					// already maximised, so restore
					windowPlacement.showCmd = Win32.SW_SHOWNORMAL;
				}
				Win32.SetWindowPlacement(hWnd, ref windowPlacement);
			}
		}

		public static void SupersizeActive()
		{
			IntPtr hWnd = Win32.GetForegroundWindow();
			if (hWnd != null)
			{
				SupersizeWindow(hWnd);
			}
		}

		private static IntPtr lastSupersizeHwnd = (IntPtr)0;
		private static Rectangle lastSupersizeRect = new Rectangle();

		public static void SupersizeWindow(IntPtr hWnd)
		{
			// only supersize the window if it has a sizing border,
			// otherwise the user would not be able to restore its size
			int style = Win32.GetWindowLong(hWnd, Win32.GWL_STYLE);
			if ((style & Win32.WS_THICKFRAME) != 0)
			{
				Rectangle vitrualDesktopRect = GetVitrualDesktopRect();
				Win32.WINDOWPLACEMENT windowPlacement = new Win32.WINDOWPLACEMENT();
				Win32.GetWindowPlacement(hWnd, ref windowPlacement);
				Rectangle curRect = RectToRectangle(ref windowPlacement.rcNormalPosition);

				if (hWnd == lastSupersizeHwnd && curRect == vitrualDesktopRect)
				{
					// this window has already been supersized, 
					// so we need to return it to its previous (restored) size
					windowPlacement.rcNormalPosition = RectangleToRect(ref lastSupersizeRect);
				}
				else
				{
					// supersize the window
					windowPlacement.rcNormalPosition = RectangleToRect(ref vitrualDesktopRect);

					// and remember it, so we can undo it
					lastSupersizeHwnd = hWnd;
					lastSupersizeRect = curRect;
				}
				windowPlacement.showCmd = Win32.SW_SHOWNORMAL;
				Win32.SetWindowPlacement(hWnd, ref windowPlacement);
			}
		}
		#endregion


		#region Individual Window movement
		/// <summary>
		/// Moves the active window to the next screen.
		/// </summary>
		public static void MoveActiveToNextScreen()
		{
			IntPtr hWnd = Win32.GetForegroundWindow();
			if (hWnd != null)
			{
				MoveWindowToNext(hWnd, +1);
			}
		}

		/// <summary>
		/// Moves the active window to the previous screen.
		/// </summary>
		public static void MoveActiveToPrevScreen()
		{
			IntPtr hWnd = Win32.GetForegroundWindow();
			if (hWnd != null)
			{
				MoveWindowToNext(hWnd, -1);
			}
		}
		#endregion

		#region Private Helpers
		/// <summary>
		/// Converts a Win32 RECT to a Rectangle.
		/// </summary>
		/// <param name="rect">Win 32 RECT</param>
		/// <returns>.NET Rectangle</returns>
		private static Rectangle RectToRectangle(ref Win32.RECT rect)
		{
			Rectangle rectangle = new Rectangle(rect.left, rect.top, rect.right - rect.left, rect.bottom - rect.top);

			return rectangle;
		}

		/// <summary>
		/// Converts a Rectangle to a Win32 RECT
		/// </summary>
		/// <param name="rectangle">.NET Rectabgle</param>
		/// <returns>Win32 RECT</returns>
		private static Win32.RECT RectangleToRect(ref Rectangle rectangle)
		{
			Win32.RECT rect = new Win32.RECT(rectangle.Left, rectangle.Top, rectangle.Right, rectangle.Bottom);

			return rect;
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
		/// Get the bounding rectangle that covers all screens
		/// </summary>
		/// <returns></returns>
		private static Rectangle GetVitrualDesktopRect()
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
		/// Converts the co-ordinates for a rectangle on one screen
		/// to the same place on another screen.
		/// </summary>
		/// <param name="curRect">Rectangle to be moved</param>
		/// <param name="deltaScreenIndex">+1 for next screen -1 for previous screen</param>
		/// <returns></returns>
		private static Rectangle TransfromRectToOtherScreen(ref Rectangle curRect, int deltaScreenIndex)
		{
			Rectangle otherRect = new Rectangle();
			otherRect = curRect;

			Screen curScreen = Screen.FromRectangle(curRect);
			int curScreenIndex = FindScreenIndex(curScreen);
			if (curScreenIndex >= 0)
			{
				int otherScreenIndex = (curScreenIndex + deltaScreenIndex) % Screen.AllScreens.Length;
				if (otherScreenIndex < 0)
				{
					otherScreenIndex += Screen.AllScreens.Length;
				}
				if (otherScreenIndex != curScreenIndex)
				{
					// keep TLHC in next screen same as current screen (relative to the working araea)
					Screen otherScreen = Screen.AllScreens[otherScreenIndex];
					otherRect.Offset(otherScreen.WorkingArea.Left - curScreen.WorkingArea.Left,
									 otherScreen.WorkingArea.Top - curScreen.WorkingArea.Top);
				}
			}

			return otherRect;
		}

		/// <summary>
		/// Finds the index within Screen.AllScreens[] that the passed screen is on.
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

		/// <summary>
		/// Moves the window corresponding to the specified HWND
		/// to the next screen.
		/// </summary>
		/// <param name="hWnd">HWND of window to move.</param>
		/// <param name="deltaScreenIndex">Number of screens to move right.</param>
		private static void MoveWindowToNext(IntPtr hWnd, int deltaScreenIndex)
		{
			Win32.WINDOWPLACEMENT windowPlacement = new Win32.WINDOWPLACEMENT();
			Win32.GetWindowPlacement(hWnd, ref windowPlacement);
			//Rectangle curRect = new Rectangle(windowPlacement.rcNormalPosition.left,
			//                               windowPlacement.rcNormalPosition.top,
			//                               windowPlacement.rcNormalPosition.right - windowPlacement.rcNormalPosition.left,
			//                               windowPlacement.rcNormalPosition.bottom - windowPlacement.rcNormalPosition.top);
			Rectangle curRect = RectToRectangle(ref windowPlacement.rcNormalPosition);
			Rectangle newRect = TransfromRectToOtherScreen(ref curRect, deltaScreenIndex);
			uint oldShowCmd = windowPlacement.showCmd;
			if (oldShowCmd == Win32.SW_SHOWMINIMIZED || oldShowCmd == Win32.SW_SHOWMAXIMIZED)
			{
				// need to restore window before moving it
				windowPlacement.showCmd = Win32.SW_RESTORE;
				Win32.SetWindowPlacement(hWnd, ref windowPlacement);
				windowPlacement.showCmd = Win32.SW_SHOW;
				//windowPlacement.rcNormalPosition.left = newRect.Left;
				//windowPlacement.rcNormalPosition.top = newRect.Top;
				//windowPlacement.rcNormalPosition.right = newRect.Right;
				//windowPlacement.rcNormalPosition.bottom = newRect.Bottom;
				windowPlacement.rcNormalPosition = RectangleToRect(ref newRect);
				Win32.SetWindowPlacement(hWnd, ref windowPlacement);
				// now minimise/maximise it
				windowPlacement.showCmd = oldShowCmd;
				Win32.SetWindowPlacement(hWnd, ref windowPlacement);
			}
			else
			{
				// normal window - not minimised or maximised
				//windowPlacement.rcNormalPosition.left = newRect.Left;
				//windowPlacement.rcNormalPosition.top = newRect.Top;
				//windowPlacement.rcNormalPosition.right = newRect.Right;
				//windowPlacement.rcNormalPosition.bottom = newRect.Bottom;
				windowPlacement.rcNormalPosition = RectangleToRect(ref newRect);
				Win32.SetWindowPlacement(hWnd, ref windowPlacement);
			}
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
