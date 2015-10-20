using DMT.Library.GuiUtils;
using DMT.Library.HotKeys;
using DMT.Library.Logging;
using DMT.Library.PInvoke;
using DMT.Library.Settings;
using DMT.Library.Transform;
using DMT.Resources;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DMT.Modules.Cursor
{
	class CursorModule : Module
	{
		ISettingsService _settingsService;
		ILogger _logger;

		public HotKeyController FreeCursorHotKeyController { get; protected set; }
		public HotKeyController StickyCursorHotKeyController { get; protected set; }
		public HotKeyController LockCursorHotKeyController { get; protected set; }
		public HotKeyController CursorNextScreenHotKeyController { get; protected set; }
		public HotKeyController CursorPrevScreenHotKeyController { get; protected set; }
		public HotKeyController CursorToPrimaryScreenHotKeyController { get; protected set; }

		public enum CursorType { Free = 0, Sticky = 1, Lock = 2 };

		//public enum MouseButton { None = 0, Left = 1, Middle = 2, Right = 3, X1 = 4, X2 = 5 };

		CursorType _curCursorType = CursorType.Free;

		// Barriers which constrain the cursor movement
		CursorBarrierLower leftBarrier = new CursorBarrierLower(false, 0, 0);
		CursorBarrierUpper rightBarrier = new CursorBarrierUpper(false, 0, 0);
		CursorBarrierLower topBarrier = new CursorBarrierLower(false, 0, 0);
		CursorBarrierUpper bottomBarrier = new CursorBarrierUpper(false, 0, 0);

		int _minForce;
		bool _freeMovementKeyPressed;
		bool _freeMovementButtonPressed;

		// Win32 low level mouse hook
		Win32.HookProc llMouseProc; // = llMouseHookCallback;
		IntPtr llMouseHook = IntPtr.Zero;

		// Win32 low level keyboard hook
		Win32.HookProc llKeyboardProc; // = llKeyboardHookCallback;
		IntPtr llKeyboardHook = IntPtr.Zero;

		// inidicates if cursor movement is restricted (sticky or locked)
		bool CursorLocked
		{
			get { return llMouseHook != IntPtr.Zero; }
		}

		IntSetting MinStickyForceSetting { get; set; }
		public int MinStickyForce
		{
			get { return MinStickyForceSetting.Value; }
			set 
			{ 
				MinStickyForceSetting.Value = value;
				if (_curCursorType == CursorType.Sticky)
				{
					// also need to update the min force that is currently in use
					_minForce = value;
					// minForce is used within the barriers, so must update these as well
					ReBuildBarriers();
				}
			}
		}


		BoolSetting AllowFreeMovementKeySetting { get; set; }
		public bool AllowFreeMovementKey
		{
			get { return AllowFreeMovementKeySetting.Value; }
			set{ AllowFreeMovementKeySetting.Value = value; }
		}

		BoolSetting AllowFreeMovementButtonSetting { get; set; }
		public bool AllowFreeMovementButton
		{
			get { return AllowFreeMovementButtonSetting.Value; }
			set { AllowFreeMovementButtonSetting.Value = value; }
		}


		BoolSetting PrimaryReturnUnhinderedSetting { get; set; }
		public bool PrimaryReturnUnhindered
		{
			get { return PrimaryReturnUnhinderedSetting.Value; }
			set { PrimaryReturnUnhinderedSetting.Value = value; }
		}

		IntSetting FreeMovementKeySetting { get; set; }
		public Keys FreeMovementKey
		{
			get { return (Keys)FreeMovementKeySetting.Value; }
			set { FreeMovementKeySetting.Value = (int)value; }
		}

		UIntSetting FreeMovementButtonSetting { get; set; }
		public MouseButtons FreeMovementButton
		{
			get { return (MouseButtons)FreeMovementButtonSetting.Value; }
			set { FreeMovementButtonSetting.Value = (uint)value; }
		}

		IntSetting DefaultCursorModeSetting { get; set; }
		public CursorType DefaultCursorMode
		{
			get { return (CursorType)DefaultCursorModeSetting.Value; }
			set { DefaultCursorModeSetting.Value = (int)value; /* TODO */ }
		}


		public CursorModule(ISettingsService settingsService, IHotKeyService hotKeyService, ILogger logger)
			: base(hotKeyService)
		{
			_settingsService = settingsService;
			//_hotKeyService = hotKeyService;
			_logger = logger;

			ModuleName = "Cursor";

			llMouseProc = llMouseHookCallback;
			llKeyboardProc = llKeyboardHookCallback;
		}

		public override ModuleOptionNode GetOptionNodes()
		{
			Image image = new Bitmap(Properties.Resources.cursor_16_16);
			ModuleOptionNodeBranch options = new ModuleOptionNodeBranch("Cursor", image, new CursorRootOptionsPanel());
			options.Nodes.Add(new ModuleOptionNodeLeaf("General", image, new CursorGeneralOptionsPanel(this)));

			return options;
		}

		public override void Start()
		{
			// hot keys
			FreeCursorHotKeyController = AddCommand("FreeCursor", CursorStrings.FreeCursorDescription, CursorStrings.FreeCursorWin7, FreeCursor);
			StickyCursorHotKeyController = AddCommand("StickyCursor", CursorStrings.StickyCursorDescription, CursorStrings.StickyCursorWin7, StickyCursor);
			LockCursorHotKeyController = AddCommand("LockCursor", CursorStrings.LockCursorDescription, CursorStrings.LockCursorWin7, LockCursor);
			CursorNextScreenHotKeyController = AddCommand("CursorToNextScreen", CursorStrings.CursorNextScreenDescription, CursorStrings.CursorNextScreenWin7, CursorToNextScreen);
			CursorPrevScreenHotKeyController = AddCommand("CursorToPrevScreen", CursorStrings.CursorPrevScreenDescription, CursorStrings.CursorPrevScreenWin7, CursorToPrevScreen);
			CursorToPrimaryScreenHotKeyController = AddCommand("CursorToPrimaryScreen", CursorStrings.CursorToPrimaryScreenDescription, CursorStrings.CursorToPrimaryScreenWin7, CursorToPrimaryScreen);

			// init the other values from the settings

			MinStickyForceSetting = new IntSetting(_settingsService, ModuleName, "MinStickyForce");

			// The setting value for this should have been "AllowFreeMovement"
			AllowFreeMovementKeySetting = new BoolSetting(_settingsService, ModuleName, "ControlUnhindersCursor");
			FreeMovementKeySetting = new IntSetting(_settingsService, ModuleName, "FreeMovementKey");

			AllowFreeMovementButtonSetting = new BoolSetting(_settingsService, ModuleName, "AllowFreeMovementButton");
			FreeMovementButtonSetting = new UIntSetting(_settingsService, ModuleName, "FreeMovementButton");

			PrimaryReturnUnhinderedSetting = new BoolSetting(_settingsService, ModuleName, "PrimaryReturnUnhindered");

			DefaultCursorModeSetting = new IntSetting(_settingsService, ModuleName, "DefaultCursorMode");

			InitCursorMode(DefaultCursorMode);
		}

		public override void Terminate()
		{
			// This will release the hooks if they are hooked
			UnLockCursor();
			_curCursorType = CursorType.Free;
		}

		public override void DisplayResolutionChanged()
		{
			ReBuildBarriers();
		}

		void InitCursorMode(CursorType initCursorType)
		{
			// put everything in a fixed state (with a free cursor)
			_curCursorType = CursorType.Free;

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

		/// <summary>
		/// Set the cursor so that its movement is unhindered by the screen edges
		/// </summary>
		public void FreeCursor()
		{
			UnLockCursor();
			_curCursorType = CursorType.Free;
		}

		/// <summary>
		/// Make the transition between the screens sticky.
		/// 
		/// Note: If the current cursor state is already sticky and a hotkey has not been defined to free the cursor
		/// then we toggle the sticky state off.  This is mainly for safety rather than functionality.
		/// </summary>
		public void StickyCursor()
		{
			if (_curCursorType == CursorType.Sticky && !HaveFreeCursorHotKey())
			{
				// force operation to toggle
				FreeCursor();
			}
			else
			{
				_minForce = MinStickyForce;
				LockCursorToScreen();
				_curCursorType = CursorType.Sticky;
			}
		}

		/// <summary>
		/// Lock the cursor to the current screen.
		/// 
		/// Note: If the current cursor state is already locked and a hotkey has not been defined to free the cursor
		/// then we toggle the locked state off.  This is mainly for safety rather than functionality.
		/// </summary>
		public void LockCursor()
		{
			if (_curCursorType == CursorType.Lock && !HaveFreeCursorHotKey())
			{
				// force operation to toggle
				FreeCursor();
			}
			else
			{
				_minForce = Int32.MaxValue;
				LockCursorToScreen();
				_curCursorType = CursorType.Lock;
			}
		}

		bool HaveFreeCursorHotKey()
		{
			return FreeCursorHotKeyController.IsEnabled();
		}

		/// <summary>
		/// Move the cursor to the next screen.
		/// The cursors position relative to the edges of the screen it is on
		/// is maintaied after it has been moved.
		/// </summary>
		void CursorToNextScreen()
		{
			CursorToDeltaScreen(1);
		}

		/// <summary>
		/// Move the cursor to the previous screen.
		/// The cursors position relative to the edges of the screen it is on
		/// is maintaied after it has been moved.
		/// </summary>
		void CursorToPrevScreen()
		{
			CursorToDeltaScreen(-1);
		}

		/// <summary>
		/// Move the cursor to the primary screen and position it in the center.
		/// </summary>
		void CursorToPrimaryScreen()
		{
			bool wasLocked = CursorLocked;

			// find primary monitor
			Screen primaryScreen = Screen.PrimaryScreen;
			Point newCursorPosition = new Point(primaryScreen.Bounds.Width / 2, primaryScreen.Bounds.Height / 2);
			if (wasLocked)
			{
				UnLockCursor();
			}
			System.Windows.Forms.Cursor.Position = newCursorPosition;
			if (wasLocked)
			{
				LockCursorToScreen();
			}
		}

		/// <summary>
		/// Called when display settings have changed.
		/// We need to capture this as the screen co-ords may have
		/// changed so we must rebuild the barriers.
		/// </summary>
		void DisplaySettingsChanged()
		{
			ReBuildBarriers();
		}

		#region hooks
		// This is the low level Mouse hook callback
		// Processing in here should be efficient as possible
		// as it can be called very frequently.
		public int llMouseHookCallback(int nCode, IntPtr wParam, IntPtr lParam)
		{
			if (nCode >= 0)
			{
				// lParam is a pointer to a MSLLHOOKSTRUCT, 
				Win32.MSLLHOOKSTRUCT msllHookStruct = (Win32.MSLLHOOKSTRUCT)Marshal.PtrToStructure(lParam, typeof(Win32.MSLLHOOKSTRUCT));
				int originalX = msllHookStruct.pt.x;
				int originalY = msllHookStruct.pt.y;
				int x = originalX;
				int y = originalY;

				if (AllowFreeMovementButton)
				{
					MouseButtons mouseButton = MouseButtons.None;
					bool buttonDown = false;
					int msg = (int)wParam;
					switch(msg)
					{
						case Win32.WM_LBUTTONDOWN:
							mouseButton = MouseButtons.Left;
							buttonDown= true;
							break;

						case Win32.WM_LBUTTONUP:
							mouseButton = MouseButtons.Left;
							break;

						case Win32.WM_MBUTTONDOWN:
							mouseButton = MouseButtons.Middle;
							buttonDown= true;
							break;

						case Win32.WM_MBUTTONUP:
							mouseButton = MouseButtons.Middle;
							break;

						case Win32.WM_RBUTTONDOWN:
							mouseButton = MouseButtons.Right;
							buttonDown= true;
							break;

						case Win32.WM_RBUTTONUP:
							mouseButton = MouseButtons.Right;
							break;

						case Win32.WM_XBUTTONDOWN:
							mouseButton = GetXButton(msllHookStruct);
							buttonDown = true;
							break;

						case Win32.WM_XBUTTONUP:
							mouseButton = GetXButton(msllHookStruct);
							break;
					}

					if (mouseButton != MouseButtons.None && mouseButton == FreeMovementButton)
					{
						if (buttonDown)
						{
							_freeMovementButtonPressed = true;
						}
						else
						{
							_freeMovementButtonPressed = false;
							// must also rebuild the barriers as the cursor may now be on a different screen
							ReBuildBarriers();
						}
					}
				}

				if (!_freeMovementKeyPressed && !_freeMovementButtonPressed)
				{

					// If this message was generated by a touch event
					// then we want to allow the cursor to move un-hindered
					bool touchEvent = ((msllHookStruct.dwExtraInfo & Win32.MOUSEEVENTF_FROMTOUCH) == Win32.MOUSEEVENTF_FROMTOUCH);

					// check in case returning to (or is already on) primary screen and user wants this to happen freely
					bool freelyReturnToPrimary = PrimaryReturnUnhindered && Screen.PrimaryScreen.Bounds.Contains(x, y);

					if (touchEvent || freelyReturnToPrimary)
					{
						// allow cursor to move freely
						// still need to check if we have moved outside of the current screen
						// so that we can rebuild the barriers for the new screen
						bool outside = leftBarrier.Outside(x);
						if (rightBarrier.Outside(x))
						{
							outside = true;
						}
						if (topBarrier.Outside(y))
						{
							outside = true;
						}
						if (bottomBarrier.Outside(y))
						{
							outside = true;
						}

						if (outside)
						{
							ReBuildBarriers(new Point(x, y));
						}
					}
					else
					{
						// check if the cursor has moved from one screen to another
						// and if so add the required amount of stickiness to the cursor
						// restraining it to the current screen if necessary

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
							System.Windows.Forms.Cursor.Position = new Point(x, y);
							return 1;
						}
					}
				}
			}
			return Win32.CallNextHookEx(llMouseHook, nCode, wParam, lParam);
		}

		MouseButtons GetXButton(Win32.MSLLHOOKSTRUCT msllHookStruct)
		{
			uint mouseDataHiWord = msllHookStruct.mouseData >> 16;

			if (mouseDataHiWord == Win32.XBUTTON1)
			{
				return MouseButtons.XButton1;
			}
			else if (mouseDataHiWord == Win32.XBUTTON2)
			{
				return MouseButtons.XButton2;
			}
			else
			{
				return MouseButtons.None;
			}
		}

		// This is the low level Keyboard hook callback
		// Processing in here should be efficient as possible
		// as it can be called very frequently.
		private int llKeyboardHookCallback(int nCode, IntPtr wParam, IntPtr lParam)
		{
			if (nCode >= 0)
			{

				// lParam is a pointer to a KBDLLHOOKSTRUCT, but we only want the virtual key code
				// from this which is the first int, so instead of marshalling the entire structure
				// we just marshal the first int to minimise any performance hit 
				uint vkCode = (uint)Marshal.ReadInt32(lParam);
				Keys key = (Keys)vkCode;

				if (key == FreeMovementKey)
				{
					//if (enableDisableLocking)
					if (AllowFreeMovementKey)
					{
						int msg = (int)wParam;
						if (msg == Win32.WM_KEYDOWN)
						{
							_freeMovementKeyPressed = true;
						}
						else if (msg == Win32.WM_KEYUP)
						{
							_freeMovementKeyPressed = false;
							// must also rebuild the barriers as the cursor may now be on a different screen
							ReBuildBarriers();
						}
					}
				}
			}
			return Win32.CallNextHookEx(llKeyboardHook, nCode, wParam, lParam);
		}

		// The cursor should be locked (possibly just sticky) to the screen it is currently on.
		private void LockCursorToScreen()
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
		private void UnLockCursor()
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
		#endregion


		// Move the cursor to another screen
		private void CursorToDeltaScreen(int deltaScreenIndex)
		{
			bool wasLocked = CursorLocked;

			Point oldCursorPosition = System.Windows.Forms.Cursor.Position;
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
				System.Windows.Forms.Cursor.Position = newCursorPosition;
				if (wasLocked)
				{
					LockCursorToScreen();
				}
			}
		}

		// rebuild the barriers based on the current cursor position
		private void ReBuildBarriers()
		{
			ReBuildBarriers(System.Windows.Forms.Cursor.Position);
		}

		// rebuild the barriers to restrict movement of the cursor
		// to the screen that it is currently on.
		// This can be called by the low level mouse hook callback,
		// so needs to be reasonably efficient.
		//
		// Note: no locking is currently employed so need to be carefull of the order in which things are done
		private void ReBuildBarriers(Point pt)
		{
			Screen curScreen = Screen.FromPoint(pt);
			// We use the virtualDesktopRect to determine if it is
			// possible for the mouse to move over each of the borders
			// of the current screen.
			Rectangle vitrualDesktopRect = ScreenHelper.GetVitrualDesktopRect();

			// left of current screen
			if (curScreen.Bounds.Left > vitrualDesktopRect.Left)
			{
				leftBarrier.ChangeBarrier(true, curScreen.Bounds.Left, _minForce);
			}
			else
			{
				// not possible for mouse to move here, so fully disable barrier to improve efficiency
				leftBarrier.ChangeBarrier(false, 0, 0);
			}

			// right of current screen
			if (curScreen.Bounds.Right < vitrualDesktopRect.Right)
			{
				rightBarrier.ChangeBarrier(true, curScreen.Bounds.Right - 1, _minForce);
			}
			else
			{
				// not possible for mouse to move here, so fully disable barrier to improve efficiency
				rightBarrier.ChangeBarrier(false, 0, 0);
			}

			// top of current screen
			if (curScreen.Bounds.Top > vitrualDesktopRect.Top)
			{
				topBarrier.ChangeBarrier(true, curScreen.Bounds.Top, _minForce);
			}
			else
			{
				// not possible for mouse to move here, so fully disable barrier to improve efficiency
				topBarrier.ChangeBarrier(false, 0, 0);
			}

			// bottom of current screen
			if (curScreen.Bounds.Bottom < vitrualDesktopRect.Bottom)
			{
				bottomBarrier.ChangeBarrier(true, curScreen.Bounds.Bottom - 1, _minForce);
			}
			else
			{
				// not possible for mouse to move here, so fully disable barrier to improve efficiency
				bottomBarrier.ChangeBarrier(false, 0, 0);
			}
		}
	}
}
