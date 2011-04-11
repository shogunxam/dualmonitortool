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
using System.Collections.Specialized;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace SwapScreen
{
	/// <summary>
	/// Singleton to act as a controller for all the hotkeys
	/// The class is referenced using Controller.Instance.
	/// Init() should be called first, and Term() should
	/// be called before the application exits
	/// </summary>
	sealed class Controller
	{
		// ID's used by the hotkeys
		private const int ID_HOTKEY_NEXTSCREEN = 0x201;
		private const int ID_HOTKEY_PREVSCREEN = 0x202;
		private const int ID_HOTKEY_MINIMISE = 0x203;
		private const int ID_HOTKEY_MINIMISE_ALL_BUT = 0x204;
		private const int ID_HOTKEY_MAXIMISE = 0x205;
		private const int ID_HOTKEY_SUPERSIZE = 0x206;
		private const int ID_HOTKEY_ROTATENEXT = 0x207;
		private const int ID_HOTKEY_ROTATEPREV = 0x208;
		private const int ID_HOTKEY_SHOWDESKTOP1 = 0x209;
		private const int ID_HOTKEY_SHOWDESKTOP2 = 0x20A;
		private const int ID_HOTKEY_FREECURSOR = 0x20B;
		private const int ID_HOTKEY_STICKYCURSOR = 0x20C;
		private const int ID_HOTKEY_LOCKCURSOR = 0x20D;
		private const int ID_HOTKEY_CURSORNEXTSCREEN = 0x20E;
		private const int ID_HOTKEY_CURSORPREVSCREEN = 0x20F;

		private const int ID_HOTKEY_UDA_START = 0x1000;
		// area above this reserved for dynamic (user) user hotkeys

		// the single instance of the controller object
		static readonly Controller instance = new Controller();

		// Explicit static constructor to tell C# compiler
		// not to mark type as beforefieldinit
		static Controller()
		{
		}

		Controller()
		{
			// need separate call to Init() to initialise
		}

		public static Controller Instance
		{
			get
			{
				return instance;
			}
		}

		private HotKeyController nextScreenHotKeyController;
		/// <summary>
		/// HotKey to move active window to next screen
		/// </summary>
		public HotKeyController NextScreenHotKeyController
		{
			get { return nextScreenHotKeyController; }
		}

		private HotKeyController prevScreenHotKeyController;
		/// <summary>
		/// HotKey to move active window to previous screen
		/// </summary>
		public HotKeyController PrevScreenHotKeyController
		{
			get { return prevScreenHotKeyController; }
		}

		private HotKeyController minimiseHotKeyController;
		/// <summary>
		/// HotKey to minimise active window
		/// </summary>
		public HotKeyController MinimiseHotKeyController
		{
			get { return minimiseHotKeyController; }
		}

		private HotKeyController minimiseAllButHotKeyController;
		/// HotKey to minimise all windows apart from the active window
		public HotKeyController MinimiseAllButHotKeyController
		{
			get { return minimiseAllButHotKeyController; }
		}

		private HotKeyController maximiseHotKeyController;
		/// <summary>
		///HotKey to maximise active window
		/// </summary>
		public HotKeyController MaximiseHotKeyController
		{
			get { return maximiseHotKeyController; }
		}

		private HotKeyController supersizeHotKeyController;
		/// <summary>
		/// HotKey to supersize the active window
		/// </summary>
		public HotKeyController SupersizeHotKeyController
		{
			get { return supersizeHotKeyController; }
		}

		private HotKeyController rotateNextHotKeyController;
		/// <summary>
		/// HotKey to rotate screens forwards
		/// </summary>
		public HotKeyController RotateNextHotKeyController
		{
			get { return rotateNextHotKeyController; }
		}

		private HotKeyController rotatePrevHotKeyController;
		/// <summary>
		/// HotKey to rotate screen backwards
		/// </summary>
		public HotKeyController RotatePrevHotKeyController
		{
			get { return rotatePrevHotKeyController; }
		}

		private HotKeyController showDesktop1HotKeyController;
		/// <summary>
		/// HotKey to show the desktop on screen 1 (1 Based)
		/// </summary>
		public HotKeyController ShowDesktop1HotKeyController
		{
			get { return showDesktop1HotKeyController; }
		}

		private HotKeyController showDesktop2HotKeyController;
		/// <summary>
		/// HotKey to show the desktop on screen 2 (1 Based)
		/// </summary>
		public HotKeyController ShowDesktop2HotKeyController
		{
			get { return showDesktop2HotKeyController; }
		}

		private HotKeyController freeCursorHotKeyController;
		/// <summary>
		/// HotKey to allow cursor to move freely between screens
		/// </summary>
		public HotKeyController FreeCursorHotKeyController
		{
			get { return freeCursorHotKeyController; }
		}

		private HotKeyController stickyCursorHotKeyController;
		/// <summary>
		/// HotKey to make cursor movement sticky between screens
		/// </summary>
		public HotKeyController StickyCursorHotKeyController
		{
			get { return stickyCursorHotKeyController; }
		}

		private HotKeyController lockCursorHotKeyController;
		/// <summary>
		/// HotKey to lock cursor onto current screen
		/// </summary>
		public HotKeyController LockCursorHotKeyController
		{
			get { return lockCursorHotKeyController; }
		}

		private HotKeyController cursorNextScreenHotKeyController;
		/// <summary>
		/// Hotkey to move cursor to next screen
		/// </summary>
		public HotKeyController CursorNextScreenHotKeyController
		{
			get { return cursorNextScreenHotKeyController; }
		}

		private HotKeyController cursorPrevScreenHotKeyController;
		/// <summary>
		/// Hotkey to move cursor to previous screen
		/// </summary>
		public HotKeyController CursorPrevScreenHotKeyController
		{
			get { return cursorPrevScreenHotKeyController; }
		}

		/// <summary>
		/// Initialise the Controller.
		/// This could be done implicitly from the ctor() 
		/// which will be called the first time the instance is accessed,
		/// but we will stick to explicit initialisation for now as it
		/// makes it easier to pass parameters into the initialisation.
		/// 
		/// This will initialise all of the hotkeys and setup hooks for those 
		/// that are not disabled.
		/// </summary>
		public void Init(Form form)
		{
			CursorHelper.Init((CursorHelper.CursorType)Properties.Settings.Default.DefaultCursorType);

			InitHotKeys(form);
			InitUdaHotKeys(form);
		}

		/// <summary>
		/// Terminate the use of the controller.
		/// We could use Dispose() instead of this, but instead we will
		/// go with an explicit termination that matches the initialisation.
		/// Note: if needed Init() can be called again after Term().
		/// </summary>
		public void Term()
		{
			TermUdaHotKeys();
			TermHotKeys();

			CursorHelper.Term();
		}

		// fully initialise all of the hotkeys
		private void InitHotKeys(Form form)
		{
			nextScreenHotKeyController = new HotKeyController(form, ID_HOTKEY_NEXTSCREEN,
				"HotKeyValue",
				Properties.Resources.NextScreenDescription,
				Properties.Resources.NextScreenWin7,
				new HotKey.HotKeyHandler(ScreenHelper.MoveActiveToNextScreen));

			prevScreenHotKeyController = new HotKeyController(form, ID_HOTKEY_PREVSCREEN,
				"PrevScreenHotKey",
				Properties.Resources.PrevScreenDescription,
				Properties.Resources.PrevScreenWin7,
				new HotKey.HotKeyHandler(ScreenHelper.MoveActiveToPrevScreen));

			minimiseHotKeyController = new HotKeyController(form, ID_HOTKEY_MINIMISE,
				"MinimiseHotKey",
				Properties.Resources.MinimiseDescription,
				Properties.Resources.MinimiseWin7,
				new HotKey.HotKeyHandler(ScreenHelper.MinimiseActive));

			minimiseAllButHotKeyController = new HotKeyController(form, ID_HOTKEY_MINIMISE_ALL_BUT,
				"MinimiseAllButHotKey",
				Properties.Resources.MinimiseAllButDescription,
				Properties.Resources.MinimiseAllButWin7,
				new HotKey.HotKeyHandler(ScreenHelper.MinimiseAllButActive));

			maximiseHotKeyController = new HotKeyController(form, ID_HOTKEY_MAXIMISE,
				"MaximiseHotKey",
				Properties.Resources.MaximiseDescription,
				Properties.Resources.MaximiseWin7,
				new HotKey.HotKeyHandler(ScreenHelper.MaximiseActive));

			supersizeHotKeyController = new HotKeyController(form, ID_HOTKEY_SUPERSIZE,
				"SupersizeHotKey",
				Properties.Resources.SupersizeDescription,
				Properties.Resources.SupersizeWin7,
				new HotKey.HotKeyHandler(ScreenHelper.SupersizeActive));

			rotateNextHotKeyController = new HotKeyController(form, ID_HOTKEY_ROTATENEXT,
				"RotateNextHotKey",
				Properties.Resources.RotateNextDescription,
				Properties.Resources.RotateNextWin7,
				new HotKey.HotKeyHandler(ScreenHelper.RotateScreensNext));

			rotatePrevHotKeyController = new HotKeyController(form, ID_HOTKEY_ROTATEPREV,
				"RotatePrevHotKey",
				Properties.Resources.RotatePrevDescription,
				Properties.Resources.RotatePrevWin7,
				new HotKey.HotKeyHandler(ScreenHelper.RotateScreensPrev));

			showDesktop1HotKeyController = new HotKeyController(form, ID_HOTKEY_SHOWDESKTOP1,
				"ShowDesktop1HotKey",
				Properties.Resources.ShowDesktop1Description,
				Properties.Resources.ShowDesktop1Win7,
				new HotKey.HotKeyHandler(ScreenHelper.ShowDesktop1));

			showDesktop2HotKeyController = new HotKeyController(form, ID_HOTKEY_SHOWDESKTOP2,
				"ShowDesktop2HotKey",
				Properties.Resources.ShowDesktop2Description,
				Properties.Resources.ShowDesktop2Win7,
				new HotKey.HotKeyHandler(ScreenHelper.ShowDesktop2));

			freeCursorHotKeyController = new HotKeyController(form, ID_HOTKEY_FREECURSOR,
				"FreeCursorHotKey",
				Properties.Resources.FreeCursorDescription,
				Properties.Resources.FreeCursorWin7,
				new HotKey.HotKeyHandler(CursorHelper.FreeCursor));

			stickyCursorHotKeyController = new HotKeyController(form, ID_HOTKEY_STICKYCURSOR,
				"StickyCursorHotKey",
				Properties.Resources.StickyCursorDescription,
				Properties.Resources.StickyCursorWin7,
				new HotKey.HotKeyHandler(CursorHelper.StickyCursor));

			lockCursorHotKeyController = new HotKeyController(form, ID_HOTKEY_LOCKCURSOR,
				"LockCursorHotKey",
				Properties.Resources.LockCursorDescription,
				Properties.Resources.LockCursorWin7,
				new HotKey.HotKeyHandler(CursorHelper.LockCursor));

			cursorNextScreenHotKeyController = new HotKeyController(form, ID_HOTKEY_CURSORNEXTSCREEN,
				"CursorNextScreenHotKey",
				Properties.Resources.CursorNextScreenDescription,
				Properties.Resources.CursorNextScreenWin7,
				new HotKey.HotKeyHandler(CursorHelper.CursorToNextScreen));

			cursorPrevScreenHotKeyController = new HotKeyController(form, ID_HOTKEY_CURSORPREVSCREEN,
				"CursorPrevScreenHotKey",
				Properties.Resources.CursorPrevScreenDescription,
				Properties.Resources.CursorPrevScreenWin7,
				new HotKey.HotKeyHandler(CursorHelper.CursorToPrevScreen));
		}

		// terminates all of the hotkeys
		private void TermHotKeys()
		{
			cursorPrevScreenHotKeyController.Dispose();
			cursorNextScreenHotKeyController.Dispose();
			lockCursorHotKeyController.Dispose();
			stickyCursorHotKeyController.Dispose();
			freeCursorHotKeyController.Dispose();
			showDesktop2HotKeyController.Dispose();
			showDesktop1HotKeyController.Dispose();
			rotatePrevHotKeyController.Dispose();
			rotateNextHotKeyController.Dispose();
			supersizeHotKeyController.Dispose();
			maximiseHotKeyController.Dispose();
			minimiseAllButHotKeyController.Dispose();
			minimiseHotKeyController.Dispose();
			prevScreenHotKeyController.Dispose();
			nextScreenHotKeyController.Dispose();
		}


		#region UDA HotKeys

		// In this version we have a fixed number of UDA controllers
		// but in future the plan is for this to be dynamic 
		private const int numUdaControllers = 10;

		private List<UdaController> UdaControllers = new List<UdaController>();

		// fully initialise all of the hotkeys
		private void InitUdaHotKeys(Form form)
		{
			int id = ID_HOTKEY_UDA_START;

			// add a fxed number of controllers (10)
			for (int idx = 0; idx < numUdaControllers; idx++)
			{
				UdaController controller = new UdaController(form, id);
				UdaControllers.Add(controller);
				id++;
			}

			// get the saved settings
			StringCollection udaProperties = Properties.Settings.Default.UdaHotKeys;
			if (udaProperties != null && udaProperties.Count > 0)
			{
				int controllerIndex = 0;
				foreach (string udaProperty in udaProperties)
				{
					if (controllerIndex < UdaControllers.Count)
					{
						UdaController udaController = UdaControllers[controllerIndex];
						udaController.InitFromProperty(udaProperty);
					}
					controllerIndex++;
				}
			}
			else
			{
				// no settings - so generate an initial list
				GenerateDefaultUdas();
			}
		}

		public void SaveUdaSettings()
		{
			StringCollection udaProperties = new StringCollection();
			foreach (UdaController udaController in UdaControllers)
			{
				string propertyValue = udaController.GetPropertyValue();
				udaProperties.Add(propertyValue);
			}

			Properties.Settings.Default.UdaHotKeys = udaProperties;
			Properties.Settings.Default.Save();
		}

		private void GenerateDefaultUdas()
		{
			int screens = Screen.AllScreens.Length;
			int idx = 0;

			// add full screens
			for (int screen = 0; screen < Screen.AllScreens.Length; screen++)
			{
				Rectangle rect = Screen.AllScreens[screen].WorkingArea;

				string description = string.Format("Screen {0}", screen + 1);
				SetDefaultUda(idx, rect.Left, rect.Top, rect.Width, rect.Height, description);
				idx++;
			}

			// add half screens
			for (int screen = 0; screen < Screen.AllScreens.Length; screen++)
			{
				Rectangle rect = Screen.AllScreens[screen].WorkingArea;

				string description = string.Format("Screen {0} - left half", screen + 1);
				SetDefaultUda(idx, rect.Left, rect.Top, rect.Width / 2, rect.Height, description);
				idx++;

				description = string.Format("Screen {0} - right half", screen + 1);
				SetDefaultUda(idx, rect.Left + rect.Width / 2, rect.Top, rect.Width / 2, rect.Height, description);
				idx++;
			}

			// add quadrants
			for (int screen = 0; screen < Screen.AllScreens.Length; screen++)
			{
				Rectangle rect = Screen.AllScreens[screen].WorkingArea;

				string description = string.Format("Screen {0} - top left quadrant", screen + 1);
				SetDefaultUda(idx, rect.Left, rect.Top, rect.Width / 2, rect.Height / 2, description);
				idx++;

				description = string.Format("Screen {0} - top right quadrant", screen + 1);
				SetDefaultUda(idx, rect.Left + rect.Width / 2, rect.Top, rect.Width / 2, rect.Height / 2, description);
				idx++;

				description = string.Format("Screen {0} - bottom left quadrant", screen + 1);
				SetDefaultUda(idx, rect.Left, rect.Top + rect.Height / 2, rect.Width / 2, rect.Height / 2, description);
				idx++;

				description = string.Format("Screen {0} - bottom right quadrant", screen + 1);
				SetDefaultUda(idx, rect.Left + rect.Width / 2, rect.Top + rect.Height / 2, rect.Width / 2, rect.Height / 2, description);
				idx++;
			}
		}

		private void SetDefaultUda(int idx, int left, int right, int width, int height, string description)
		{
			if (idx >= 0 && idx < UdaControllers.Count)
			{
				UdaController udaController = UdaControllers[idx];

				//uint keyCode = 655409;	// Ctrl+Win+1
				uint keyCode = 0x10A0031;	// Ctrl+Win+1 - disabled
				keyCode += (uint)idx;
				if (idx == 9)
				{
					keyCode = 0x10A0030;	// Ctrl+Win+0 - disabled
				}

				Rectangle rectangle = new Rectangle(left, right, width, height);
				udaController.InitFromProperty(UdaController.ToPropertyValue(keyCode, rectangle, description));
			}
		}

		public UdaController GetUdaController(int index)
		{
			if (index >= 0 && index < UdaControllers.Count)
			{
				return UdaControllers[index];
			}
			else
			{
				return null;
			}
		}

		// terminates all of the hotkeys
		private void TermUdaHotKeys()
		{
			foreach (UdaController controller in UdaControllers)
			{
				controller.Dispose();
			}
		}

		#endregion
	}
}