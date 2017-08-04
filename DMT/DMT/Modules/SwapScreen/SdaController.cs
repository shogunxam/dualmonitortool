#region copyright
// This file is part of Dual Monitor Tools which is a set of tools to assist
// users with multiple monitor setups.
// Copyright (C) 2017  Gerald Evans
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

namespace DMT.Modules.SwapScreen
{
	using System;
	using System.Collections.Generic;
	using System.Drawing;
	using System.Linq;
	using System.Text;

	using DMT.Library.HotKeys;
	using DMT.Library.GuiUtils;

	/// <summary>
	/// Controller for single system defined area
	/// </summary>
	class SdaController
	{
		SwapScreenModule _swapScreenModule;
		Command _command;
		IHotKeyService _hotKeyService;

		public SdaController(SwapScreenModule swapScreenModule, Command command, IHotKeyService hotKeyService)
		{
			_swapScreenModule = swapScreenModule;
			_command = command;
			_hotKeyService = hotKeyService;

			// add our handler to the command
			_command.Handler = HotKeyHandler;

			HotKey = _hotKeyService.CreateHotKey(HotKeyHandler);
		}

		/// <summary>
		/// Gets or sets the location of the system defined area
		/// </summary>
		public Rectangle Position { get; protected set; }

		/// <summary>
		/// Gets or sets the hotkey that does the real work
		/// </summary>
		public HotKey HotKey { get; protected set; }

		/// <summary>
		/// Updates values for the system defined area
		/// </summary>
		/// <param name="keyCode"> key code used by the hot key</param>
		/// <param name="position">Location of the system defined area</param>
		/// <returns>True if managed to set up the system defined area</returns>
		public bool SetValues(uint keyCode, Rectangle position)
		{
			KeyCombo keyCombo = new KeyCombo();
			keyCombo.ComboValue = keyCode;
			if (!HotKey.RegisterHotKey(keyCombo))
			{
				return false;
			}

			Position = position;
			return true;
		}

		/// <summary>
		/// Hot key handler for the system defined area
		/// </summary>
		public void HotKeyHandler()
		{
			bool pushBordersOut = !_swapScreenModule.BorderInsideSda;
			// The SDA areas are based on system co-ords not workspace co-ords
			ScreenHelper.MoveActiveToAbsoluteRectangle(Position, pushBordersOut);
		}

		public void Disable()
		{
			HotKey.UnRegisterHotKey();
		}

		void Dispose(bool disposing)
		{
			if (disposing)
			{
				HotKey.Dispose();
			}
		}

	}
}
