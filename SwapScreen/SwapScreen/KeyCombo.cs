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
using System.Text;
using System.Windows.Forms;

namespace SwapScreen
{
	/// <summary>
	/// This is a key combination that can be used as a hotkey,
	/// i.e. a physical key on the keyboard and any modifiers like shift etc.
	/// System.Windows.Forms.Keys almost provides what we want, but it doesn't 
	/// have a modifier for the Windows key.
	/// </summary>
	public struct KeyCombo
	{
		/// <summary>
		/// The virtual keycode (excluding any modifier)
		/// only the lesat sig 16 bits of this will be used
		/// </summary>
		public Keys KeyCode;

		/// <summary>
		/// Flag indicating if the Alt key is pressed.
		/// </summary>
		public bool AltMod;

		/// <summary>
		/// Flag indicating if the Shift key is pressed.
		/// </summary>
		public bool ShiftMod;

		/// <summary>
		/// Flag indicating if the Control key is pressed.
		/// </summary>
		public bool ControlMod;

		/// <summary>
		/// Flag indicating if the Windows key is pressed.
		/// </summary>
		public bool WinMod;

		/// <summary>
		/// Returns the KeyCode as used by Win32.
		/// Win32 uses the same value for the keycodes as used
		/// by System.Windows.Forms.Keys.
		/// </summary>
		public uint Win32KeyCode
		{
			get
			{
				return (uint)(KeyCode & Keys.KeyCode);
			}
		}

		/// <summary>
		/// Returns the modifiers as used by Win32.
		/// Note the Win32 modifier values are different to that used by
		/// System.Windows.Forms.Keys which doesn't even support the 
		/// Windows key modifier.
		/// </summary>
		public uint Win32Modifier
		{
			get
			{
				uint modifier = 0;
				if (AltMod)
				{
					modifier |= Win32.MOD_ALT;
				}
				if (ControlMod)
				{
					modifier |= Win32.MOD_CONTROL;
				}
				if (ShiftMod)
				{
					modifier |= Win32.MOD_SHIFT;
				}
				if (WinMod)
				{
					modifier |= Win32.MOD_WIN;
				}

				return modifier;
			}
		}

		/// <summary>
		/// Converts the state of the KeyCombo to a single uint
		/// so that it can be saved as a property.
		/// </summary>
		/// <returns>uint that can be passed to FromPropertyValue() to restore the current state.</returns>
		public uint ToPropertyValue()
		{
			return Win32KeyCode | (Win32Modifier << 16);
		}

		/// <summary>
		/// Uses the property value to restore the state of the KeyCombo.
		/// </summary>
		/// <param name="config">uint value from a previous call to ToPropertyValue().</param>
		public void FromPropertyValue(uint config)
		{
			KeyCode = (Keys)(config & 0xFFFF);
			uint keyModifier = config >> 16;
			AltMod = (keyModifier & Win32.MOD_ALT) != 0;
			ControlMod = (keyModifier & Win32.MOD_CONTROL) != 0;
			ShiftMod = (keyModifier & Win32.MOD_SHIFT) != 0;
			WinMod = (keyModifier & Win32.MOD_WIN) != 0;
		}
	}
}
