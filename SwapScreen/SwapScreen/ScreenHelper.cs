using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Text;

namespace SwapScreen
{
	static class ScreenHelper
	{
		public static void ShowDestktop(int screenIndex)
		{
			if (screenIndex >= Screen.AllScreens.Length)
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

		// rotates screens right
		public static void SwapScreens()
		{
			List<IntPtr> hWndList = GetVisibleApplicationWindows();

			// for each visible application window...
			foreach (IntPtr hWnd in hWndList)
			{
				MoveWindowToNext(hWnd);
			}
		}

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

		public static Rectangle RectToRectangle(ref Win32.RECT rect)
		{
			Rectangle rectangle = new Rectangle(rect.left, rect.top, rect.right - rect.left, rect.bottom - rect.top);

			return rectangle;
		}

		#region Individual Window movement
		public static void MoveActiveWindow()
		{
			IntPtr hWnd = Win32.GetForegroundWindow();
			if (hWnd != null)
			{
				MoveWindowToNext(hWnd);
			}
		}

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

		private static Rectangle TransfromRectToNextScreen(ref Rectangle curRect)
		{
			Rectangle nextRec = new Rectangle();
			nextRec = curRect;

			Screen curScreen = Screen.FromRectangle(curRect);
			int curScreenIndex = -1;
			for (int i = 0; i < Screen.AllScreens.Length; i++)
			{
				if (curScreen.DeviceName == Screen.AllScreens[i].DeviceName)
				{
					curScreenIndex = i;
					break;
				}
			}
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
