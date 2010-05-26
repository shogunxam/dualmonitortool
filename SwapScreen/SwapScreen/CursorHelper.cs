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
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace SwapScreen
{
	class CursorHelper
	{
		// temp test code
		public static void ToggleLockCursorToScreen()
		{
			if (llMouseHook == IntPtr.Zero)
			{
				StickyCursor();
			}
			else
			{
				UnLockCursor();
			}
		}

		public static void FreeCursor()
		{
			UnLockCursor();
		}

		public static void StickyCursor()
		{
			minForce = 1000; //Properties.Settings.Default.MinStickyForce;
			LockCursorToScreen();
		}

		public static void LockCursor()
		{
			minForce = Int32.MaxValue;
			LockCursorToScreen();
		}

		public static void CursorToNextScreen()
		{
			CursorToDeltaScreen(1);
		}

		public static void CursorToPrevScreen()
		{
			CursorToDeltaScreen(-1);
		}


		private static CursorBarrierLower leftBarrier;
		private static CursorBarrierUpper rightBarrier;
		private static CursorBarrierLower topBarrier;
		private static CursorBarrierUpper bottomBarrier;
		private static int minForce;
		
		private static Win32.HookProc llMouseProc = llMouseHookCallback;
		private static IntPtr llMouseHook = IntPtr.Zero;

		private static bool CursorLocked
		{
			get { return llMouseHook != IntPtr.Zero; }
		}

		private static int llMouseHookCallback(int nCode, IntPtr wParam, IntPtr lParam)
		{
			if (nCode >= 0)
			{
				Win32.MSLLHOOKSTRUCT msllHookStruct = (Win32.MSLLHOOKSTRUCT)Marshal.PtrToStructure(lParam, typeof(Win32.MSLLHOOKSTRUCT));

				int x = msllHookStruct.pt.x;
				int y = msllHookStruct.pt.y;
				bool brokenThrough = false;

				if (leftBarrier.BrokenThrough(ref x))
				{
					brokenThrough = true;
				}
				if (rightBarrier.BrokenThrough(ref x))
				{
					brokenThrough = true;
				}
				if (topBarrier.BrokenThrough(ref y))
				{
					brokenThrough = true;
				}
				if (bottomBarrier.BrokenThrough(ref y))
				{
					brokenThrough = true;
				}

				if (brokenThrough)
				{
					ReBuildBarriers(new Point(x, y));
				}
				if (x != msllHookStruct.pt.x || y != msllHookStruct.pt.y)
				{
					Cursor.Position = new Point(x, y);
					return 1;
				}
			}
			return Win32.CallNextHookEx(llMouseHook, nCode, wParam, lParam);
		}

		private static void LockCursorToScreen()
		{
			ReBuildBarriers(Cursor.Position);

			if (llMouseHook == IntPtr.Zero)
			{
				using (Process curProcess = Process.GetCurrentProcess())
				{
					using (ProcessModule curModule = curProcess.MainModule)
					{
						IntPtr hModule = Win32.GetModuleHandle(curModule.ModuleName);
						llMouseHook = Win32.SetWindowsHookEx(Win32.WH_MOUSE_LL, llMouseProc, hModule, 0);
					}
				}
			}
		}

		private static void UnLockCursor()
		{
			if (llMouseHook != IntPtr.Zero)
			{
				Win32.UnhookWindowsHookEx(llMouseHook);
				llMouseHook = IntPtr.Zero;
			}
		}

		private static void CursorToDeltaScreen(int deltaScreenIndex)
		{
			bool wasLocked = CursorLocked;

			Point oldCursorPosition = Cursor.Position;
			Screen curScreen = Screen.FromPoint(oldCursorPosition);
			int curScreenIndex = ScreenHelper.FindScreenIndex(curScreen);
			int newScreenIndex = ScreenHelper.DeltaScreenIndex(curScreenIndex, deltaScreenIndex);
			if (newScreenIndex != curScreenIndex)
			{
				// want to position the cursor on this new screen in a position
				// that is relative to the position it was on the old screen.
				Debug.Assert(newScreenIndex >= 0 && newScreenIndex < Screen.AllScreens.Length);
				Screen newScreen = Screen.AllScreens[newScreenIndex];

				Scaler scaleX = new Scaler(curScreen.Bounds.Left, curScreen.Bounds.Right,
				                           newScreen.Bounds.Left, newScreen.Bounds.Right);
				Scaler scaleY = new Scaler(curScreen.Bounds.Top, curScreen.Bounds.Bottom,
										   newScreen.Bounds.Top, newScreen.Bounds.Bottom);
				Point newCursorPosition = new Point(scaleX.DestFromSrc(oldCursorPosition.X),
													scaleY.DestFromSrc(oldCursorPosition.Y));
				Debug.Assert(newScreen.Bounds.Contains(newCursorPosition));
				if (wasLocked)
				{
					UnLockCursor();
				}
				Cursor.Position = newCursorPosition;
				if (wasLocked)
				{
					LockCursorToScreen();
				}
			}
		}

		private static void ReBuildBarriers(Point pt)
		{
			Screen curScreen = Screen.FromPoint(pt);
			// We use the virtualDesktopRect to determine if it is
			// possible for the mouse to move over each of the borders
			// of the current screen
			Rectangle vitrualDesktopRect = ScreenHelper.GetVitrualDesktopRect();

			// left of current screen
			if (curScreen.Bounds.Left > vitrualDesktopRect.Left)
			{
				leftBarrier = new CursorBarrierLower(true, curScreen.Bounds.Left, minForce);
			}
			else
			{
				leftBarrier = new CursorBarrierLower(false, 0, 0);
			}

			// right of current screen
			if (curScreen.Bounds.Right < vitrualDesktopRect.Right)
			{
				rightBarrier = new CursorBarrierUpper(true, curScreen.Bounds.Right - 1, minForce);
			}
			else
			{
				rightBarrier = new CursorBarrierUpper(false, 0, 0);
			}

			// top of current screen
			if (curScreen.Bounds.Top > vitrualDesktopRect.Top)
			{
				topBarrier = new CursorBarrierLower(true, curScreen.Bounds.Top, minForce);
			}
			else
			{
				topBarrier = new CursorBarrierLower(false, 0, 0);
			}

			// bottom of current screen
			if (curScreen.Bounds.Bottom < vitrualDesktopRect.Bottom)
			{
				bottomBarrier = new CursorBarrierUpper(true, curScreen.Bounds.Right - 1, minForce);
			}
			else
			{
				bottomBarrier = new CursorBarrierUpper(false, 0, 0);
			}
		}
	
	}
}
