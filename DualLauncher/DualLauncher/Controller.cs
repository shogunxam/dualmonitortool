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
using System.Text;
using System.Windows.Forms;

namespace DualLauncher
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
		private const int ID_HOTKEY_ACTIVATE = 0x501;

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

		private HotKeyController activateHotKeyController;
		/// <summary>
		/// HotKey to move active the input window
		/// </summary>
		public HotKeyController ActivateHotKeyController
		{
			get { return activateHotKeyController; }
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
			InitHotKeys(form);
		}

		/// <summary>
		/// Terminate the use of the controller.
		/// We could use Dispose() instead of this, but instead we will
		/// go with an explicit termination that matches the initialisation.
		/// Note: if needed Init() can be called again after Term().
		/// </summary>
		public void Term()
		{
			TermHotKeys();
		}

		// fully initialise all of the hotkeys
		private void InitHotKeys(Form form)
		{
			activateHotKeyController = new HotKeyController(form, ID_HOTKEY_ACTIVATE,
				"ActivateHotKey",
				Properties.Resources.ActivateDescription,
				"",		// no Windows 7 key
				new HotKey.HotKeyHandler(ScreenHelper.MoveActiveToNextScreen));
		}

		// terminates all of the hotkeys
		private void TermHotKeys()
		{
			activateHotKeyController.Dispose();
		}

	}
}
