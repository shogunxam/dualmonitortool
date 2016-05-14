#region copyright
// This file is part of Dual Monitor Tools which is a set of tools to assist
// users with multiple monitor setups.
// Copyright (C) 2009-2015  Gerald Evans
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

namespace DMT.Library.GuiUtils
{
	using System;
	using System.Collections.Generic;
	using System.Drawing;
	using System.Runtime.InteropServices;
	using System.Text;
	using System.Windows.Forms;

	using DMT.Library.PInvoke;

	/// <summary>
	/// Utility class to help in handling multiple screens.
	/// </summary>
	static class ScreenHelper
	{
		static IntPtr _lastSupersizeHwnd = (IntPtr)0;
		static Rectangle _lastSupersizeRect = new Rectangle();

		/// <summary>
		/// Attempts to minimize all visible application windows on
		/// the first screen.
		/// </summary>
		public static void ShowDesktop1()
		{
			ShowDesktop(0);
		}

		/// <summary>
		/// Attempts to minimize all visible application windows on
		/// the second screen.
		/// </summary>
		public static void ShowDesktop2()
		{
			ShowDesktop(1);
		}

		/// <summary>
		/// Attempts to minimize all visible application windows on
		/// the third screen.
		/// </summary>
		public static void ShowDesktop3()
		{
			ShowDesktop(2);
		}

		/// <summary>
		/// Attempts to minimize all visible application windows on
		/// the fourth screen.
		/// </summary>
		public static void ShowDesktop4()
		{
			ShowDesktop(3);
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
				NativeMethods.WINDOWPLACEMENT windowPlacement = new NativeMethods.WINDOWPLACEMENT();
				NativeMethods.GetWindowPlacement(hWnd, ref windowPlacement);
				Rectangle windowRectangle = RectToRectangle(ref windowPlacement.rcNormalPosition);
				if (windowRectangle.IntersectsWith(curScreen.Bounds))
				{
					// this window does exist (maybe partially) on this screen
					// so lets minimise it
					windowPlacement.showCmd = NativeMethods.SW_SHOWMINIMIZED;
					NativeMethods.SetWindowPlacement(hWnd, ref windowPlacement);
				}
			}
		}

		/// <summary>
		///  Minimise all but the active window
		/// </summary>
		public static void MinimiseAllButActive()
		{
			IntPtr hWndActive = NativeMethods.GetForegroundWindow();
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
		/// Minimise/restore the active window
		/// </summary>
		public static void MinimiseActive()
		{
			IntPtr hWnd = NativeMethods.GetForegroundWindow();
			if (!IsNullHandle(hWnd))
			{
				// toggle the minimised state of the window
				ToggleMinimiseWindow(hWnd);
			}
		}

		/// <summary>
		/// Minimise/restores the specified window
		/// </summary>
		/// <param name="hWnd">HWND of window to minimise</param>
		public static void ToggleMinimiseWindow(IntPtr hWnd)
		{
			int style = NativeMethods.GetWindowLong(hWnd, NativeMethods.GWL_STYLE);
			NativeMethods.WINDOWPLACEMENT windowPlacement = new NativeMethods.WINDOWPLACEMENT();
			NativeMethods.GetWindowPlacement(hWnd, ref windowPlacement);

			// check if the window is already minimised
			if ((style & NativeMethods.WS_MINIMIZE) == 0)
			{
				// not minimised - so minimise
				windowPlacement.showCmd = NativeMethods.SW_SHOWMINIMIZED;
			}
			else
			{
				// already minimised, so restore
				windowPlacement.showCmd = NativeMethods.SW_SHOWNORMAL;
			}

			NativeMethods.SetWindowPlacement(hWnd, ref windowPlacement);
		}

		/// <summary>
		/// Minimise the specified window
		/// </summary>
		/// <param name="hWnd">HWND of window to minimise</param>
		public static void MinimiseWindow(IntPtr hWnd)
		{
			NativeMethods.WINDOWPLACEMENT windowPlacement = new NativeMethods.WINDOWPLACEMENT();
			NativeMethods.GetWindowPlacement(hWnd, ref windowPlacement);
			windowPlacement.showCmd = NativeMethods.SW_SHOWMINIMIZED;
			NativeMethods.SetWindowPlacement(hWnd, ref windowPlacement);
		}

		/// <summary>
		/// Maximise the active window
		/// </summary>
		public static void MaximiseActive()
		{
			IntPtr hWnd = NativeMethods.GetForegroundWindow();
			if (!IsNullHandle(hWnd))
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
			int style = NativeMethods.GetWindowLong(hWnd, NativeMethods.GWL_STYLE);
			if ((style & NativeMethods.WS_MAXIMIZEBOX) != 0)
			{
				NativeMethods.WINDOWPLACEMENT windowPlacement = new NativeMethods.WINDOWPLACEMENT();
				NativeMethods.GetWindowPlacement(hWnd, ref windowPlacement);

				// check if the window is already maximised
				if ((style & NativeMethods.WS_MAXIMIZE) == 0)
				{
					// not maximised - so maximise
					windowPlacement.showCmd = NativeMethods.SW_SHOWMAXIMIZED;
				}
				else
				{
					// already maximised, so restore
					windowPlacement.showCmd = NativeMethods.SW_SHOWNORMAL;
				}

				NativeMethods.SetWindowPlacement(hWnd, ref windowPlacement);
			}
		}

		/// <summary>
		/// Supersizes the active window
		/// </summary>
		public static void SupersizeActive()
		{
			IntPtr hWnd = NativeMethods.GetForegroundWindow();
			if (!IsNullHandle(hWnd))
			{
				SupersizeWindow(hWnd);
			}
		}

		/// <summary>
		/// Supersizes the window
		/// </summary>
		/// <param name="hWnd">Windows to supersize</param>
		public static void SupersizeWindow(IntPtr hWnd)
		{
			// only supersize the window if it has a sizing border,
			// otherwise the user would not be able to restore its size
			int style = NativeMethods.GetWindowLong(hWnd, NativeMethods.GWL_STYLE);
			if ((style & NativeMethods.WS_THICKFRAME) != 0)
			{
				// This used to use the entire VirtualDesktop, but now uses the entire
				// WorkingDesktop as we need the area where we can place windows.
				// This only makes a difference when the taskbar is placed at the edge of
				// the total bounded area and stretches right across it. Eg. when the taskbar
				// is on the left side of the left most screen when screens positioned as left and right.
				Rectangle vitrualWorkingRect = GetVitrualWorkingRect();
				NativeMethods.WINDOWPLACEMENT windowPlacement = new NativeMethods.WINDOWPLACEMENT();
				NativeMethods.GetWindowPlacement(hWnd, ref windowPlacement);
				Rectangle curRect = RectToRectangle(ref windowPlacement.rcNormalPosition);

				if (hWnd == _lastSupersizeHwnd && curRect == vitrualWorkingRect)
				{
					// this window has already been supersized, 
					// so we need to return it to its previous (restored) size
					windowPlacement.rcNormalPosition = RectangleToRect(ref _lastSupersizeRect);
				}
				else
				{
					// supersize the window
					windowPlacement.rcNormalPosition = RectangleToRect(ref vitrualWorkingRect);

					// and remember it, so we can undo it
					_lastSupersizeHwnd = hWnd;
					_lastSupersizeRect = curRect;
				}

				windowPlacement.showCmd = NativeMethods.SW_SHOWNORMAL;
				NativeMethods.SetWindowPlacement(hWnd, ref windowPlacement);
			}
		}
		#endregion

		#region Individual Window movement
		/// <summary>
		/// Moves the active window to the next screen.
		/// </summary>
		public static void MoveActiveToNextScreen()
		{
			IntPtr hWnd = NativeMethods.GetForegroundWindow();
			if (!IsNullHandle(hWnd))
			{
				MoveWindowToNext(hWnd, +1);
			}
		}

		/// <summary>
		/// Moves the active window to the previous screen.
		/// </summary>
		public static void MoveActiveToPrevScreen()
		{
			IntPtr hWnd = NativeMethods.GetForegroundWindow();
			if (!IsNullHandle(hWnd))
			{
				MoveWindowToNext(hWnd, -1);
			}
		}

		/// <summary>
		/// Snap the active window left.
		/// </summary>
		public static void SnapActiveLeft()
		{
			IntPtr hWnd = NativeMethods.GetForegroundWindow();
			if (!IsNullHandle(hWnd))
			{
				SnapWindowLeftRight(hWnd, -1);
			}
		}

		/// <summary>
		/// Snap the active window right.
		/// </summary>
		public static void SnapActiveRight()
		{
			IntPtr hWnd = NativeMethods.GetForegroundWindow();
			if (!IsNullHandle(hWnd))
			{
				SnapWindowLeftRight(hWnd, 1);
			}
		}

		/// <summary>
		/// Snap the active window up.
		/// </summary>
		public static void SnapActiveUp()
		{
			IntPtr hWnd = NativeMethods.GetForegroundWindow();
			if (!IsNullHandle(hWnd))
			{
				SnapWindowUpDown(hWnd, -1);
			}
		}

		/// <summary>
		/// Snap the active window down.
		/// </summary>
		public static void SnapActiveDown()
		{
			IntPtr hWnd = NativeMethods.GetForegroundWindow();
			if (!IsNullHandle(hWnd))
			{
				SnapWindowUpDown(hWnd, 1);
			}
		}

		/// <summary>
		/// Moves the active window to the given rectangle
		/// </summary>
		/// <param name="rectangle">Location to move window to</param>
		public static void MoveActiveToRectangle(Rectangle rectangle)
		{
			IntPtr hWnd = NativeMethods.GetForegroundWindow();
			if (!IsNullHandle(hWnd))
			{
				MoveWindow(hWnd, rectangle);
			}
		}

		/// <summary>
		/// Swaps the size/position of the active window and the top level
		/// window immediately below it (in z-order). 
		/// </summary>
		public static void SwapTop2Windows()
		{
			IntPtr hWndTop = NativeMethods.GetForegroundWindow();
			if (!IsNullHandle(hWndTop))
			{
				////IntPtr hWndNext = hWndTop;
				////do
				////{
				////    hWndNext = Win32.GetWindow(hWndNext, Win32.GW_HWNDNEXT);
				////} while (hWndNext != null && !IsCandidateForMoving(hWndNext));

				// less efficient, but easier to read
				IntPtr hWndNext = NativeMethods.GetWindow(hWndTop, NativeMethods.GW_HWNDNEXT);
				while (!IsNullHandle(hWndNext) && !IsCandidateForMoving(hWndNext))
				{
					hWndNext = NativeMethods.GetWindow(hWndNext, NativeMethods.GW_HWNDNEXT);
				}

				if (!IsNullHandle(hWndNext))
				{
					SwapWindows(hWndTop, hWndNext);

					// now make the old next the active window
					NativeMethods.SetForegroundWindow(hWndNext);
				}
			}
		}
		#endregion

		#region Private Helpers
		/// <summary>
		/// Converts a Win32 RECT to a Rectangle.
		/// </summary>
		/// <param name="rect">Win 32 RECT</param>
		/// <returns>.NET Rectangle</returns>
		public static Rectangle RectToRectangle(ref NativeMethods.RECT rect)
		{
			Rectangle rectangle = new Rectangle(rect.left, rect.top, rect.right - rect.left, rect.bottom - rect.top);

			return rectangle;
		}

		/// <summary>
		/// Converts a Rectangle to a Win32 RECT
		/// </summary>
		/// <param name="rectangle">.NET Rectangle</param>
		/// <returns>Win32 RECT</returns>
		public static NativeMethods.RECT RectangleToRect(ref Rectangle rectangle)
		{
			NativeMethods.RECT rect = new NativeMethods.RECT(rectangle.Left, rectangle.Top, rectangle.Right, rectangle.Bottom);

			return rect;
		}

		/// <summary>
		/// Gets a list of all windows that we think should be allowed
		/// to be moved between screens.
		/// </summary>
		/// <returns>List of HWND's belonging to application windows.</returns>
		static List<IntPtr> GetVisibleApplicationWindows()
		{
			// TODO: rewrite using IsCandidateForMoving()
			List<IntPtr> hWndList = new List<IntPtr>();
			IntPtr hWndShell = NativeMethods.GetShellWindow();
			Rectangle vitrualDesktopRect = GetVitrualDesktopRect();

			// use anonymous method to simplify access to the windows list
			NativeMethods.EnumWindowsProc windowVisiter = delegate(IntPtr hWnd, uint lParam)
			{
				if (hWnd == hWndShell)
				{
					// ignore the shell (Program Manager) window
				}
				else if (!NativeMethods.IsWindowVisible(hWnd))
				{
					// ignore any windows without WS_VISIBLE
				}
				else
				{
					NativeMethods.WINDOWPLACEMENT windowPlacement = new NativeMethods.WINDOWPLACEMENT();
					NativeMethods.GetWindowPlacement(hWnd, ref windowPlacement);
					Rectangle windowRectangle = RectToRectangle(ref windowPlacement.rcNormalPosition);
					if (!windowRectangle.IntersectsWith(vitrualDesktopRect))
					{
						// window has been deliberately positioned offscreen, so leave alone
					}
					else
					{
						int exStyle = NativeMethods.GetWindowLong(hWnd, NativeMethods.GWL_EXSTYLE);
						if ((exStyle & NativeMethods.WS_EX_TOOLWINDOW) != 0)
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
			NativeMethods.EnumWindows(windowVisiter, 0);

			return hWndList;
		}

		static bool IsCandidateForMoving(IntPtr hWnd)
		{
			bool isCandidate = false;

			if (hWnd == NativeMethods.GetShellWindow())
			{
				// ignore the shell (Program Manager) window
			}
			else if (!NativeMethods.IsWindowVisible(hWnd))
			{
				// ignore any windows without WS_VISIBLE
			}
			else
			{
				NativeMethods.WINDOWPLACEMENT windowPlacement = new NativeMethods.WINDOWPLACEMENT();
				NativeMethods.GetWindowPlacement(hWnd, ref windowPlacement);
				Rectangle windowRectangle = RectToRectangle(ref windowPlacement.rcNormalPosition);
				Rectangle vitrualDesktopRect = GetVitrualDesktopRect();
				if (!windowRectangle.IntersectsWith(vitrualDesktopRect))
				{
					// window has been deliberately positioned offscreen, so leave alone
				}
				else
				{
					int exStyle = NativeMethods.GetWindowLong(hWnd, NativeMethods.GWL_EXSTYLE);
					if ((exStyle & NativeMethods.WS_EX_TOOLWINDOW) != 0)
					{
						// This is a tool window - leave alone
					}
					else
					{
						// we should be able to move this window without any ill effect
						isCandidate = true;
					}
				}
			}

			return isCandidate;
		}

		/// <summary>
		/// Get the bounding rectangle that covers the working area of all screens
		/// </summary>
		/// <returns>Bounding rectangle</returns>
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
		/// <returns>Virtual desktop rectangle</returns>
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
		/// Converts the co-ordinates for a rectangle on one screen
		/// to the same place on another screen.
		/// </summary>
		/// <param name="curRect">Rectangle to be moved</param>
		/// <param name="deltaScreenIndex">+1 for next screen -1 for previous screen</param>
		/// <returns>Rectangle on target screen</returns>
		static Rectangle TransfromRectToOtherScreen(ref Rectangle curRect, int deltaScreenIndex)
		{
			Rectangle otherRect = new Rectangle();
			otherRect = curRect;

			Screen curScreen = Screen.FromRectangle(curRect);
			int curScreenIndex = FindScreenIndex(curScreen);
			if (curScreenIndex >= 0)
			{
				int otherScreenIndex = DeltaScreenIndex(curScreenIndex, deltaScreenIndex);
				if (otherScreenIndex != curScreenIndex)
				{
					// keep TLHC in next screen same as current screen (relative to the working araea)
					Screen otherScreen = Screen.AllScreens[otherScreenIndex];
					// When positioning windows, (0, 0) is the TLHC of the working area (of primary)
					// even if the taskbar is on the left or top of the screen which is not necessarily
					// the same position as the true pixel position of (0,0) which is always TLHC
					// of primary monitor whether there is a task bar in that position or not.
					// This means we need to use Bounds and not WorkingArea as Windows will do it's
					// own adjustments.
					//otherRect.Offset(
					//	otherScreen.WorkingArea.Left - curScreen.WorkingArea.Left,
					//	otherScreen.WorkingArea.Top - curScreen.WorkingArea.Top);
					otherRect.Offset(
						otherScreen.Bounds.Left - curScreen.Bounds.Left,
						otherScreen.Bounds.Top - curScreen.Bounds.Top);
				}
			}

			return otherRect;
		}

		/// <summary>
		/// Calculate the screen index that is a displacement from specified screen index
		/// </summary>
		/// <param name="screenIndex">Starting screen index</param>
		/// <param name="deltaScreenIndex">Displacement, either positive or negative</param>
		/// <returns>Target screen index</returns>
		public static int DeltaScreenIndex(int screenIndex, int deltaScreenIndex)
		{
			int newScreenIndex = (screenIndex + deltaScreenIndex) % Screen.AllScreens.Length;
			if (newScreenIndex < 0)
			{
				newScreenIndex += Screen.AllScreens.Length;
			}

			return newScreenIndex;
		}

		/// <summary>
		/// Finds the next screen after the given screen
		/// </summary>
		/// <param name="curScreen">Current screen</param>
		/// <returns>Next screen after specified screen</returns>
		public static Screen NextScreen(Screen curScreen)
		{
			int curScreenIndex = FindScreenIndex(curScreen);
			if (curScreenIndex < 0)
			{
				// shouldn't happen
				return Screen.PrimaryScreen;
			}

			int nextScreenIndex = (curScreenIndex + 1) % Screen.AllScreens.Length;
			return Screen.AllScreens[nextScreenIndex];
		}

		/// <summary>
		/// Finds the index within Screen.AllScreens[] that the passed screen is on.
		/// </summary>
		/// <param name="screen">The screen whose index we are trying to find</param>
		/// <returns>Zero based screen index, or -1 if screen not found</returns>
		public static int FindScreenIndex(Screen screen)
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
		static void MoveWindowToNext(IntPtr hWnd, int deltaScreenIndex)
		{
			NativeMethods.WINDOWPLACEMENT windowPlacement = new NativeMethods.WINDOWPLACEMENT();
			NativeMethods.GetWindowPlacement(hWnd, ref windowPlacement);
			Rectangle curRect = RectToRectangle(ref windowPlacement.rcNormalPosition);
			Rectangle newRect = TransfromRectToOtherScreen(ref curRect, deltaScreenIndex);
			uint oldShowCmd = windowPlacement.showCmd;
			if (oldShowCmd == NativeMethods.SW_SHOWMINIMIZED || oldShowCmd == NativeMethods.SW_SHOWMAXIMIZED)
			{
				// need to restore window before moving it
				windowPlacement.showCmd = NativeMethods.SW_RESTORE;
				NativeMethods.SetWindowPlacement(hWnd, ref windowPlacement);
				windowPlacement.showCmd = NativeMethods.SW_SHOW;
				windowPlacement.rcNormalPosition = RectangleToRect(ref newRect);
				NativeMethods.SetWindowPlacement(hWnd, ref windowPlacement);

				// now minimise/maximise it
				windowPlacement.showCmd = oldShowCmd;
				NativeMethods.SetWindowPlacement(hWnd, ref windowPlacement);
			}
			else
			{
				// normal window - not minimised or maximised
				windowPlacement.rcNormalPosition = RectangleToRect(ref newRect);
				NativeMethods.SetWindowPlacement(hWnd, ref windowPlacement);
			}
		}

		/// <summary>
		/// Moves the window corresponding to the specified HWND
		/// to the left/right half screen.
		/// </summary>
		/// <param name="hWnd">HWND of window to move.</param>
		/// <param name="delta">Number of screens to move right.</param>
		static void SnapWindowLeftRight(IntPtr hWnd, int delta)
		{
			NativeMethods.WINDOWPLACEMENT windowPlacement = new NativeMethods.WINDOWPLACEMENT();
			NativeMethods.GetWindowPlacement(hWnd, ref windowPlacement);
			int style = NativeMethods.GetWindowLong(hWnd, NativeMethods.GWL_STYLE);
			Rectangle curRect = RectToRectangle(ref windowPlacement.rcNormalPosition);
			uint oldShowCmd = windowPlacement.showCmd;

			Screen curScreen = Screen.FromRectangle(curRect);
			int curScreenIndex = FindScreenIndex(curScreen);
			if (curScreenIndex >= 0)
			{
				int newHalf = AdvanceHalfScreen(curScreenIndex, curScreen.WorkingArea.Left, curScreen.WorkingArea.Right, curRect.Left, delta);
				int newScreenIndex = newHalf / 2;
				Rectangle screenRect = Screen.AllScreens[newScreenIndex].WorkingArea;
				Rectangle newRect;
				int newWidth = screenRect.Width / 2;
				int newHeight = screenRect.Height;
				if ((style & NativeMethods.WS_THICKFRAME) == 0)
				{
					// the window can't be resized, so keep its size the same
					newWidth = curRect.Width;
					newHeight = curRect.Height;
				}

				if ((newHalf % 2) == 0)
				{
					// left half
					newRect = new Rectangle(screenRect.Left, screenRect.Top, newWidth, newHeight);
				}
				else
				{
					// right half
					newRect = new Rectangle(screenRect.Left + screenRect.Width / 2, screenRect.Top, newWidth, newHeight);
				}

				if (oldShowCmd == NativeMethods.SW_SHOWMINIMIZED || oldShowCmd == NativeMethods.SW_SHOWMAXIMIZED)
				{
					// need to restore window before moving it
					windowPlacement.showCmd = NativeMethods.SW_RESTORE;
					NativeMethods.SetWindowPlacement(hWnd, ref windowPlacement);
				}

				windowPlacement.showCmd = NativeMethods.SW_SHOW;
				windowPlacement.rcNormalPosition = RectangleToRect(ref newRect);
				NativeMethods.SetWindowPlacement(hWnd, ref windowPlacement);
			}
		}

		/// <summary>
		/// Moves the window corresponding to the specified HWND
		/// to the top/bottom half screen.
		/// </summary>
		/// <param name="hWnd">HWND of window to move.</param>
		/// <param name="delta">Number of screens to move right.</param>
		static void SnapWindowUpDown(IntPtr hWnd, int delta)
		{
			NativeMethods.WINDOWPLACEMENT windowPlacement = new NativeMethods.WINDOWPLACEMENT();
			NativeMethods.GetWindowPlacement(hWnd, ref windowPlacement);
			int style = NativeMethods.GetWindowLong(hWnd, NativeMethods.GWL_STYLE);
			Rectangle curRect = RectToRectangle(ref windowPlacement.rcNormalPosition);
			uint oldShowCmd = windowPlacement.showCmd;

			Screen curScreen = Screen.FromRectangle(curRect);
			int curScreenIndex = FindScreenIndex(curScreen);
			if (curScreenIndex >= 0)
			{
				int newHalf = AdvanceHalfScreen(curScreenIndex, curScreen.WorkingArea.Top, curScreen.WorkingArea.Bottom, curRect.Top, delta);
				int newScreenIndex = newHalf / 2;
				Rectangle screenRect = Screen.AllScreens[newScreenIndex].WorkingArea;
				Rectangle newRect;
				int newWidth = screenRect.Width;
				int newHeight = screenRect.Height / 2;
				if ((style & NativeMethods.WS_THICKFRAME) == 0)
				{
					// the window can't be resized, so keep its size the same
					newWidth = curRect.Width;
					newHeight = curRect.Height;
				}

				if ((newHalf % 2) == 0)
				{
					// top half
					newRect = new Rectangle(screenRect.Left, screenRect.Top, newWidth, newHeight);
				}
				else
				{
					// bottom half
					newRect = new Rectangle(screenRect.Left, screenRect.Top + screenRect.Height / 2, newWidth, newHeight);
				}

				if (oldShowCmd == NativeMethods.SW_SHOWMINIMIZED || oldShowCmd == NativeMethods.SW_SHOWMAXIMIZED)
				{
					// need to restore window before moving it
					windowPlacement.showCmd = NativeMethods.SW_RESTORE;
					NativeMethods.SetWindowPlacement(hWnd, ref windowPlacement);
				}

				windowPlacement.showCmd = NativeMethods.SW_SHOW;
				windowPlacement.rcNormalPosition = RectangleToRect(ref newRect);
				NativeMethods.SetWindowPlacement(hWnd, ref windowPlacement);
			}
		}

		static int AdvanceHalfScreen(int curScreenIndex, int screenStart, int screenEnd, int winStart, int delta)
		{
			if (delta < 0)
			{
				if (winStart == screenStart)
				{
					// right half of previous screen
					return DeltaScreenIndex(curScreenIndex, -1) * 2 + 1;
				}
				else
				{
					// left half of current screen
					return curScreenIndex * 2;
				}
			}
			else
			{
				if (winStart == (screenStart + screenEnd) / 2)
				{
					// left half of next screen
					return DeltaScreenIndex(curScreenIndex, 1) * 2;
				}
				else
				{
					// right half of current screen
					return curScreenIndex * 2 + 1;
				}
			}
		}

		/// <summary>
		/// Moves the window corresponding to the specified HWND
		/// to the next screen.
		/// </summary>
		/// <param name="hWnd">HWND of window to move.</param>
		/// <param name="newRect">Rectangle the window is being moved too.</param>
		static void MoveWindow(IntPtr hWnd, Rectangle newRect)
		{
			NativeMethods.WINDOWPLACEMENT windowPlacement = new NativeMethods.WINDOWPLACEMENT();
			NativeMethods.GetWindowPlacement(hWnd, ref windowPlacement);
			uint oldShowCmd = windowPlacement.showCmd;
			if (oldShowCmd == NativeMethods.SW_SHOWMINIMIZED || oldShowCmd == NativeMethods.SW_SHOWMAXIMIZED)
			{
				// need to restore window before moving it
				windowPlacement.showCmd = NativeMethods.SW_RESTORE;
				NativeMethods.SetWindowPlacement(hWnd, ref windowPlacement);

				// make sure window will be shown normally
				windowPlacement.showCmd = NativeMethods.SW_SHOW;
			}

			int style = NativeMethods.GetWindowLong(hWnd, NativeMethods.GWL_STYLE);
			if ((style & NativeMethods.WS_THICKFRAME) == 0)
			{
				// the window can't be resized, so keep its size the same
				newRect.Width = windowPlacement.rcNormalPosition.right - windowPlacement.rcNormalPosition.left;
				newRect.Height = windowPlacement.rcNormalPosition.bottom - windowPlacement.rcNormalPosition.top;
			}

			windowPlacement.rcNormalPosition = RectangleToRect(ref newRect);
			NativeMethods.SetWindowPlacement(hWnd, ref windowPlacement);
		}

		static void SwapWindows(IntPtr hWndTop, IntPtr hWndNext)
		{
			// get the current window positions
			NativeMethods.WINDOWPLACEMENT topPlacement = new NativeMethods.WINDOWPLACEMENT();
			NativeMethods.GetWindowPlacement(hWndTop, ref topPlacement);
			NativeMethods.WINDOWPLACEMENT nextPlacement = new NativeMethods.WINDOWPLACEMENT();
			NativeMethods.GetWindowPlacement(hWndNext, ref nextPlacement);

			// swap the positions
			MoveWindowToPlacement(hWndTop, nextPlacement);
			MoveWindowToPlacement(hWndNext, topPlacement);
		}

		static void MoveWindowToPlacement(IntPtr hWnd, NativeMethods.WINDOWPLACEMENT windowPlacement)
		{
			NativeMethods.WINDOWPLACEMENT oldPlacement = new NativeMethods.WINDOWPLACEMENT();
			NativeMethods.GetWindowPlacement(hWnd, ref oldPlacement);
			uint oldShowCmd = oldPlacement.showCmd;
			if (oldShowCmd == NativeMethods.SW_SHOWMINIMIZED || oldShowCmd == NativeMethods.SW_SHOWMAXIMIZED)
			{
				// need to restore window before moving it
				oldPlacement.showCmd = NativeMethods.SW_RESTORE;
				NativeMethods.SetWindowPlacement(hWnd, ref oldPlacement);
			}

			// take copy of new windowPlacement (structure so copying values)
			NativeMethods.WINDOWPLACEMENT newPlacement = windowPlacement;

			// make sure window will initially be shown normally
			newPlacement.showCmd = NativeMethods.SW_SHOW;

			int style = NativeMethods.GetWindowLong(hWnd, NativeMethods.GWL_STYLE);
			if ((style & NativeMethods.WS_THICKFRAME) == 0)
			{
				// the window can't be resized, so keep its size the same
				int fixedWidth = oldPlacement.rcNormalPosition.right - oldPlacement.rcNormalPosition.left;
				int fixedHeight = oldPlacement.rcNormalPosition.bottom - oldPlacement.rcNormalPosition.top;
				newPlacement.rcNormalPosition.right = newPlacement.rcNormalPosition.left + fixedWidth;
				newPlacement.rcNormalPosition.bottom = newPlacement.rcNormalPosition.top + fixedHeight;
			}

			NativeMethods.SetWindowPlacement(hWnd, ref newPlacement);

			if (windowPlacement.showCmd == NativeMethods.SW_SHOWMINIMIZED || windowPlacement.showCmd == NativeMethods.SW_SHOWMAXIMIZED)
			{
				// set minimised or maximised as required
				newPlacement.showCmd = windowPlacement.showCmd;
				NativeMethods.SetWindowPlacement(hWnd, ref newPlacement);
			}
		}

		/// <summary>
		/// Tests for a NULL handle value in the WIN32 sense
		/// rather than a .NET null value
		/// </summary>
		/// <param name="hWnd">Window handle to test</param>
		/// <returns>True if it is a null handle</returns>
		static bool IsNullHandle(IntPtr hWnd)
		{
			return hWnd == IntPtr.Zero;
		}

		#endregion

		////#region Debugging helpers

		////// Not suitable for XP
		////[DllImport("user32.dll", SetLastError = true)]
		////static extern IntPtr OpenInputDesktop(uint dwFlags, bool fInherit, uint dwDesiredAccess);
		////const int DESKTOP_READOBJECTS = 1;

		////[DllImport("user32.dll", SetLastError = true)]
		////static extern bool GetUserObjectInformation(IntPtr hObj, int nIndex, StringBuilder lpString, int nLength, out int lpnLengthNeeded);
		////const int UOI_NAME = 2;

		////[DllImport("user32.dll", CharSet = CharSet.Auto)]
		////static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, IntPtr wParam, IntPtr lParam);
		////const uint WM_DISPLAYCHANGE = 0x7E;
		////private static IntPtr HWND_BROADCAST = new IntPtr(0xffff);

		////public static void DumpDesktopInfo(Form mainForm)
		////{
		////	List<string> log = new List<string>();

		////	IntPtr hDesk = OpenInputDesktop(0, false, DESKTOP_READOBJECTS);
		////	if (hDesk != IntPtr.Zero)
		////	{
		////		log.Add("Opened desktop");

		////		int stringLength = 0;
		////		StringBuilder lpString = new StringBuilder();
		////		GetUserObjectInformation(hDesk, UOI_NAME, lpString, stringLength, out stringLength);
		////		if (GetUserObjectInformation(hDesk, UOI_NAME, lpString, stringLength, out stringLength))
		////		{
		////			log.Add("Desktop name: " + lpString.ToString());
		////		}
		////		// CloseInputDesktop?
		////	}

		////	int monitorCount = System.Windows.Forms.SystemInformation.MonitorCount;
		////	log.Add(string.Format("Monitor count is {0}", monitorCount));

		////	for (int screenIndex = 0; screenIndex < Screen.AllScreens.Length; screenIndex++)
		////	{
		////		Rectangle rect = Screen.AllScreens[screenIndex].WorkingArea;
		////		log.Add(string.Format("screen {0}: ({1}, {2}) - ({3}, {4})", screenIndex, rect.Left, rect.Top, rect.Right, rect.Bottom));
		////	}

		////	//Form myForm = null;//Form.ActiveForm;
		////	//log.Add(string.Format("There are {0} open forms", Application.OpenForms.Count));
		////	//if (Application.OpenForms.Count > 0)
		////	//{
		////	//	myForm = Application.OpenForms[0];
		////	//}
		////	if (mainForm != null)
		////	{
		////		log.Add("Sending WM_DISPLAYCHANGE");
		////		log.Add(string.Format("to {0:x}", mainForm.Handle));
		////		SendMessage(mainForm.Handle, WM_DISPLAYCHANGE, IntPtr.Zero, IntPtr.Zero);
		////		//SendMessage(HWND_BROADCAST, WM_DISPLAYCHANGE, IntPtr.Zero, IntPtr.Zero);

		////		//System.Reflection.FieldInfo fieldInfo = typeof(Screen).GetField("AllScreens", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public);
		////		//fieldInfo.SetValue(null, null);

		////		System.Reflection.ConstructorInfo ctorInfo = typeof(Screen).GetConstructor(System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic, null, new Type[0], null);
		////		log.Add(string.Format("ctorInfo {0}", ctorInfo));
		////		ctorInfo.Invoke(null, null);
		////	}

		////	for (int screenIndex = 0; screenIndex < Screen.AllScreens.Length; screenIndex++)
		////	{
		////		Rectangle rect = Screen.AllScreens[screenIndex].WorkingArea;
		////		log.Add(string.Format("screen {0}: ({1}, {2}) - ({3}, {4})", screenIndex, rect.Left, rect.Top, rect.Right, rect.Bottom));
		////	}

		////	LogForm dlg = new LogForm(log);
		////	dlg.ShowDialog();
		////}

		////public static void DumpAllWindows()
		////{
		////	List<IntPtr> hWndList = GetVisibleApplicationWindows();

		////	List<string> log = new List<string>();

		////	// for each application window...
		////	foreach (IntPtr hWnd in hWndList)
		////	{
		////		log.Add(DumpWindowInfo(hWnd));
		////	}

		////	LogForm dlg = new LogForm(log);
		////	dlg.ShowDialog();
		////}

		////public static void DumpZWindows()
		////{
		////	int maxWindows = 50;
		////	List<string> log = new List<string>();

		////	IntPtr hWnd = Win32.GetForegroundWindow();
		////	//IntPtr hWnd = Win32.GetWindow((IntPtr)0, 0);
		////	if (hWnd != null)
		////	{
		////		do
		////		{
		////			log.Add(DumpWindowInfo(hWnd));

		////			hWnd = Win32.GetWindow(hWnd, Win32.GW_HWNDNEXT);
		////			//hWnd = Win32.GetWindow(hWnd, 3);
		////		} while (hWnd != null && maxWindows-- > 0);
		////	}

		////	LogForm dlg = new LogForm(log);
		////	dlg.ShowDialog();
		////}

		////public static string DumpWindowInfo(IntPtr hWnd)
		////{
		////	Win32.WINDOWPLACEMENT windowPlacement = new Win32.WINDOWPLACEMENT();
		////	Win32.GetWindowPlacement(hWnd, ref windowPlacement);

		////	string windowText = "";
		////	int textLen = Win32.GetWindowTextLength(hWnd);
		////	if (textLen > 0)
		////	{
		////		StringBuilder sb = new StringBuilder(textLen + 1);
		////		Win32.GetWindowText(hWnd, sb, sb.Capacity);
		////		windowText = sb.ToString();
		////	}

		////	int exStyle = Win32.GetWindowLong(hWnd, Win32.GWL_EXSTYLE);
		////	string style = DumpExStyle(exStyle);

		////	string visible = "";
		////	if (Win32.IsWindowVisible(hWnd))
		////	{
		////		visible = "VISIBLE";
		////	}

		////	return string.Format("hWnd:{0} {1} \"{2}\" {3} {4}", hWnd, DumpWindowPlacement(windowPlacement), windowText, style, visible);
		////}

		////public static string DumpWindowPlacement(Win32.WINDOWPLACEMENT windowPlacement)
		////{
		////	string ret;
		////	string flags = "";
		////	string showCmd = "";
		////	string minPos = "";
		////	string maxPos = "";
		////	string normalPos = "";

		////	if ((windowPlacement.flags & Win32.WPF_SETMINPOSITION) != 0)
		////	{
		////		flags += " WPF_SETMINPOSITION";
		////	}
		////	if ((windowPlacement.flags & Win32.WPF_RESTORETOMAXIMIZED) != 0)
		////	{
		////		flags += " WPF_RESTORETOMAXIMIZED";
		////	}

		////	switch (windowPlacement.showCmd)
		////	{
		////		case Win32.SW_HIDE:
		////			showCmd = "SW_HIDE";
		////			break;
		////		case Win32.SW_MINIMIZE:
		////			showCmd = "SW_MINIMIZE";
		////			break;
		////		case Win32.SW_RESTORE:
		////			showCmd = "SW_RESTORE";
		////			break;
		////		case Win32.SW_SHOW:
		////			showCmd = "SW_SHOW";
		////			break;
		////		case Win32.SW_SHOWMAXIMIZED:
		////			showCmd = "SW_SHOWMAXIMIZED";
		////			break;
		////		case Win32.SW_SHOWMINIMIZED:
		////			showCmd = "SW_SHOWMINIMIZED";
		////			break;
		////		case Win32.SW_SHOWMINNOACTIVE:
		////			showCmd = "SW_SHOWMINNOACTIVE";
		////			break;
		////		case Win32.SW_SHOWNA:
		////			showCmd = "SW_SHOWNA";
		////			break;
		////		case Win32.SW_SHOWNOACTIVATE:
		////			showCmd = "SW_SHOWNOACTIVATE";
		////			break;
		////		case Win32.SW_SHOWNORMAL:
		////			showCmd = "SW_SHOWNORMAL";
		////			break;
		////		default:
		////			showCmd = "???";
		////			break;
		////	}

		////	minPos = string.Format("({0}, {1})", windowPlacement.ptMinPosition.x, windowPlacement.ptMinPosition.y);
		////	maxPos = string.Format("({0}, {1})", windowPlacement.ptMaxPosition.x, windowPlacement.ptMaxPosition.y);
		////	normalPos = string.Format("({0}, {1}) - ({2}, {3})",
		////		windowPlacement.rcNormalPosition.left, windowPlacement.rcNormalPosition.top,
		////		windowPlacement.rcNormalPosition.right, windowPlacement.rcNormalPosition.bottom);

		////	ret = string.Format("flags:{0}, showCmd:{1}, minPos:{2}, maxPos:{3}, normalPos:{4}",
		////		flags, showCmd, minPos, maxPos, normalPos);

		////	return ret;
		////}

		////private static string DumpExStyle(int exStyle)
		////{
		////	string style = "";
		////	if ((exStyle & Win32.WS_EX_TOPMOST) != 0)
		////	{
		////		style += " WS_EX_TOPMOST";
		////	}
		////	if ((exStyle & Win32.WS_EX_TRANSPARENT) != 0)
		////	{
		////		style += " WS_EX_TRANSPARENT";
		////	}
		////	if ((exStyle & Win32.WS_EX_TOOLWINDOW) != 0)
		////	{
		////		style += " WS_EX_TOOLWINDOW";
		////	}
		////	if ((exStyle & Win32.WS_EX_APPWINDOW) != 0)
		////	{
		////		style += " WS_EX_APPWINDOW";
		////	}

		////	return style;
		////}
		////#endregion
	}
}
