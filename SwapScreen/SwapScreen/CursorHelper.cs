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
	/// <summary>
	/// Utility class to help with cursor manipulation.
	/// 
	/// TODO: this class contains some static state variables, so may want
	/// to look at splitting this class up and makeing use of a singleton
	/// at some point in the future.
	/// </summary>
	public class CursorHelper
	{
		//[Serializable]
		public enum CursorType { Free = 0, Sticky, Lock };

		public static void Init(CursorType initCursorType)
		{
			// put everything in a fixed state (with a free cursor)
			curCursorType = CursorType.Free;
			minForce = Properties.Settings.Default.MinStickyForce;
			enableDisableLocking = Properties.Settings.Default.ControlUnhindersCursor;

			// now set the initial cursor mode
			if (initCursorType == CursorType.Sticky)
			{
				StickyCursor();
			}
			else if (initCursorType == CursorType.Lock)
			{
				LockCursor();
			}
			else
			{
				// leave in free mode
			}
		}

		public static void Term()
		{
			// This will release the hooks if they are hooked
			UnLockCursor();
			curCursorType = CursorType.Free;
		}

		// we remember the current cursor type so that
		// we can perform a toggle operation if required
		private static CursorType curCursorType = CursorType.Free;

		/// <summary>
		/// Set the cursor so that its movement is unhindered by the screen edges
		/// </summary>
		public static void FreeCursor()
		{
			UnLockCursor();
			curCursorType = CursorType.Free;
		}

		/// <summary>
		/// Make the transition between the screens sticky.
		/// 
		/// Note: If the current cursor state is already sticky and a hotkey has not been defined to free the cursor
		/// then we toggle the sticky state off.  This is mainly for safety rather than functionality.
		/// </summary>
		public static void StickyCursor()
		{
			if (curCursorType == CursorType.Sticky && !Controller.Instance.FreeCursorHotKeyController.IsEnabled())
			{
				// force operation to toggle
				FreeCursor();
			}
			else
			{
				minForce = Properties.Settings.Default.MinStickyForce;
				LockCursorToScreen();
				curCursorType = CursorType.Sticky;
			}
		}

		/// <summary>
		/// Lock the cursor to the current screen.
		/// 
		/// Note: If the current cursor state is already locked and a hotkey has not been defined to free the cursor
		/// then we toggle the locked state off.  This is mainly for safety rather than functionality.
		/// </summary>
		public static void LockCursor()
		{
			if (curCursorType == CursorType.Lock && !Controller.Instance.FreeCursorHotKeyController.IsEnabled())
			{
				// force operation to toggle
				FreeCursor();
			}
			else
			{
				minForce = Int32.MaxValue;
				LockCursorToScreen();
				curCursorType = CursorType.Lock;
			}
		}

		/// <summary>
		/// Move the cursor to the next screen.
		/// The cursors position relative to the edges of the screen it is on
		/// is maintaied after it has been moved.
		/// </summary>
		public static void CursorToNextScreen()
		{
			CursorToDeltaScreen(1);
		}

		/// <summary>
		/// Move the cursor to the previous screen.
		/// The cursors position relative to the edges of the screen it is on
		/// is maintaied after it has been moved.
		/// </summary>
		public static void CursorToPrevScreen()
		{
			CursorToDeltaScreen(-1);
		}

		/// <summary>
		/// Called when display settings have changed.
		/// We need to capture this as the screen co-ords may have
		/// changed so we must rebuild the barriers.
		/// </summary>
		public static void DisplaySettingsChanged()
		{
			ReBuildBarriers();
		}

		// Barriers which constrain the cursor movement
		private static CursorBarrierLower leftBarrier;
		private static CursorBarrierUpper rightBarrier;
		private static CursorBarrierLower topBarrier;
		private static CursorBarrierUpper bottomBarrier;
		// minimum amount of force to break through the barrier
		private static int minForce;
		public static int MinForce
		{
			set 
			{ 
				minForce = value;
				// minForce is used within the barriers, so must update these as well
				ReBuildBarriers();
			}
		}

		// if true, allows free movement of the cursor
		// this is set when the control key is pressed
		private static bool disableLocking;

		// enables the disabling of locking!
		// i.e. allows locking to be disabled if the control key is pressed
		private static bool enableDisableLocking;
		public static bool EnableDisableLocking
		{
			set
			{
				enableDisableLocking = value;
				if (!enableDisableLocking)
				{
					disableLocking = false;
				}
			}
		}
		
		// Win32 low level mouse hook
		private static Win32.HookProc llMouseProc = llMouseHookCallback;
		private static IntPtr llMouseHook = IntPtr.Zero;

		// Win32 low level keyboard hook
		private static Win32.HookProc llKeyboardProc = llKeyboardHookCallback;
		private static IntPtr llKeyboardHook = IntPtr.Zero;

		// inidicates if cursor movement is restricted (sticky or locked)
		private static bool CursorLocked
		{
			get { return llMouseHook != IntPtr.Zero; }
		}

		// This is the low level Mouse hook callback
		// Processing in here should be efficient as possible
		// as it can be called very frequently.
		private static int llMouseHookCallback(int nCode, IntPtr wParam, IntPtr lParam)
		{
			if (nCode >= 0 && !disableLocking)
			{
				// TODO: we only want pt, so we don't have to marshal the entire structure
				//Win32.MSLLHOOKSTRUCT msllHookStruct = (Win32.MSLLHOOKSTRUCT)Marshal.PtrToStructure(lParam, typeof(Win32.MSLLHOOKSTRUCT));

				// lParam is a pointer to a MSLLHOOKSTRUCT, but we only want the cursor position
				// from this which are the first 2 ints, so instead of marshalling the entire structure
				// we just marshal the first 2 ints to minimise any performance hit 
				int originalX = Marshal.ReadInt32(lParam);
				int originalY = Marshal.ReadInt32(lParam, 4);
				int x = originalX;
				int y = originalY;

				bool brokenThrough = leftBarrier.BrokenThrough(ref x);
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
				if (x != originalX || y != originalY)
				{
					// override the position that Windows wants to place the cursor
					Cursor.Position = new Point(x, y);
					return 1;
				}
			}
			return Win32.CallNextHookEx(llMouseHook, nCode, wParam, lParam);
		}

		// This is the low level Keyboard hook callback
		// Processing in here should be efficient as possible
		// as it can be called very frequently.
		private static int llKeyboardHookCallback(int nCode, IntPtr wParam, IntPtr lParam)
		{
			if (nCode >= 0)
			{

				// lParam is a pointer to a KBDLLHOOKSTRUCT, but we only want the virtual key code
				// from this which is the first int, so instead of marshalling the entire structure
				// we just marshal the first int to minimise any performance hit 
				uint vkCode = (uint)Marshal.ReadInt32(lParam);
				Keys key = (Keys)vkCode;

				if (key == Keys.LControlKey)
				{
					if (enableDisableLocking)
					{
						int msg = (int)wParam;
						if (msg == Win32.WM_KEYDOWN)
						{
							disableLocking = true;
						}
						else if (msg == Win32.WM_KEYUP)
						{
							disableLocking = false;
							// must also rebuild the barriers as the cursor may now be on a different screen
							ReBuildBarriers();
						}
					}
				}
			}
			return Win32.CallNextHookEx(llKeyboardHook, nCode, wParam, lParam);
		}

		// The cursor should be locked (possibly just sticky) to the screen it is currently on.
		private static void LockCursorToScreen()
		{
			ReBuildBarriers();

			if (llMouseHook == IntPtr.Zero)
			{
				using (Process curProcess = Process.GetCurrentProcess())
				{
					using (ProcessModule curModule = curProcess.MainModule)
					{
						IntPtr hModule = Win32.GetModuleHandle(curModule.ModuleName);
						llMouseHook = Win32.SetWindowsHookEx(Win32.WH_MOUSE_LL, llMouseProc, hModule, 0);
						if (llMouseHook != IntPtr.Zero)
						{
							// mouse & keyboard should be hooked together so no need
							// to move this out into its own 'if (llKeyboardHook == IntPtr.Zero)' test
							llKeyboardHook = Win32.SetWindowsHookEx(Win32.WH_KEYBOARD_LL, llKeyboardProc, hModule, 0);
						}
					}
				}
			}
		}

		// The cursor's movement should not be hindered by screen edges
		private static void UnLockCursor()
		{
			// make sure the low level keyboard hook is unhooked
			if (llKeyboardHook != IntPtr.Zero)
			{
				// unhook our callback to make sure there is no performance degredation
				Win32.UnhookWindowsHookEx(llKeyboardHook);
				llKeyboardHook = IntPtr.Zero;
			}

			// make sure the low level mouse hook is unhooked
			if (llMouseHook != IntPtr.Zero)
			{
				// unhook our callback to make sure there is no performance degredation
				Win32.UnhookWindowsHookEx(llMouseHook);
				llMouseHook = IntPtr.Zero;
			}
		}

		// Move the cursor to another screen
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

		// rebuild the barriers based on the current cursor position
		private static void ReBuildBarriers()
		{
			ReBuildBarriers(Cursor.Position);
		}

		// rebuild the barriers to restrict movement of the cursor
		// to the screen that it is currently on.
		// This can be called by the low level mouse hook callback,
		// so needs to be reasonably efficient.
		private static void ReBuildBarriers(Point pt)
		{
			Screen curScreen = Screen.FromPoint(pt);
			// We use the virtualDesktopRect to determine if it is
			// possible for the mouse to move over each of the borders
			// of the current screen.
			Rectangle vitrualDesktopRect = ScreenHelper.GetVitrualDesktopRect();

			// left of current screen
			if (curScreen.Bounds.Left > vitrualDesktopRect.Left)
			{
				leftBarrier = new CursorBarrierLower(true, curScreen.Bounds.Left, minForce);
			}
			else
			{
				// not possible for mouse to move here, so fully disable barrier to improve efficiency
				leftBarrier = new CursorBarrierLower(false, 0, 0);
			}

			// right of current screen
			if (curScreen.Bounds.Right < vitrualDesktopRect.Right)
			{
				rightBarrier = new CursorBarrierUpper(true, curScreen.Bounds.Right - 1, minForce);
			}
			else
			{
				// not possible for mouse to move here, so fully disable barrier to improve efficiency
				rightBarrier = new CursorBarrierUpper(false, 0, 0);
			}

			// top of current screen
			if (curScreen.Bounds.Top > vitrualDesktopRect.Top)
			{
				topBarrier = new CursorBarrierLower(true, curScreen.Bounds.Top, minForce);
			}
			else
			{
				// not possible for mouse to move here, so fully disable barrier to improve efficiency
				topBarrier = new CursorBarrierLower(false, 0, 0);
			}

			// bottom of current screen
			if (curScreen.Bounds.Bottom < vitrualDesktopRect.Bottom)
			{
				bottomBarrier = new CursorBarrierUpper(true, curScreen.Bounds.Bottom - 1, minForce);
			}
			else
			{
				// not possible for mouse to move here, so fully disable barrier to improve efficiency
				bottomBarrier = new CursorBarrierUpper(false, 0, 0);
			}
		}
	}
}
