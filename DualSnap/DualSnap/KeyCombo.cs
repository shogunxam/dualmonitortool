#region copyright
// This file is part of Dual Monitor Tools which is a set of tools to assist
// users with multiple monitor setups.
// Copyright (C) 2009-2011  Gerald Evans
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

namespace DualSnap
{
	/// <summary>
	/// This is a key combination that can be used as a hotkey,
	/// i.e. a physical key on the keyboard and any modifiers like shift etc.
	/// System.Windows.Forms.Keys almost provides what we want, but it doesn't 
	/// have a modifier for the Windows key.
	/// </summary>
	public struct KeyCombo
	{
		// The value of the KeyCombo is held in an uint,
		// where the least 16 bits are used to hold the key code,
		// the bitmask 0x00010000 indicates if Alt is pressed,
		// 0x00020000 indicates if Control is pressed,
		// 0x00040000 indicates if Shift is pressed,
		// 0x00080000 indicates if Win is pressed,
		// and we have a flag at 0x8000 which if set indicates that 
		// the KeyCombo is disabled
		private const uint FLAG_ALT = 0x00010000;		// Win32.MOD_ALT << 16
		private const uint FLAG_CONTROL = 0x00020000;	// Win32.MOD_CONTROL << 16
		private const uint FLAG_SHIFT = 0x00040000;		// Win32.MOD_SHIFT << 16
		private const uint FLAG_WIN = 0x00080000;		// Win32.MOD_WIN << 16

		// Note we set the bit to indicate disabled (rather than enabled)
		// to maintain backward compatibility with previous versions of this
		// program which didn't have the disable ability
		private const uint FLAG_DISABLED = 0x01000000;

		/// <summary>
		/// The key combo value for hotkeys that are disabled
		/// </summary>
		public const uint DisabledComboValue = FLAG_DISABLED;

		private uint comboValue;
		/// <summary>
		/// A uint used to fully represent this key combination
		/// </summary>
		public uint ComboValue
		{
			get { return comboValue; }
			set { comboValue = value; }
		}

		/// <summary>
		/// Indicates if the key combination is enabled
		/// </summary>
		public bool Enabled
		{
			get { return !IsSet(comboValue, FLAG_DISABLED); }
			set { SetBit(!value, FLAG_DISABLED); }
		}

		/// <summary>
		/// The virtual keycode (excluding any modifier)
		/// only the lesat sig 16 bits of this will be used
		/// </summary>
		public Keys KeyCode
		{
			get { return (Keys)(comboValue & 0xFFFF); }
			set { comboValue = (comboValue & 0xFFFF0000) | ((uint)value & 0xFFFF); }
		}

		/// <summary>
		/// Flag indicating if the Alt key is pressed.
		/// </summary>
		public bool AltMod
		{
			get { return IsSet(comboValue, FLAG_ALT); }
			set { SetBit(value, FLAG_ALT); }
		}

		/// <summary>
		/// Flag indicating if the Shift key is pressed.
		/// </summary>
		public bool ShiftMod
		{
			get { return IsSet(comboValue, FLAG_SHIFT); }
			set { SetBit(value, FLAG_SHIFT); }
		}

		/// <summary>
		/// Flag indicating if the Control key is pressed.
		/// </summary>
		public bool ControlMod
		{
			get { return IsSet(comboValue, FLAG_CONTROL); }
			set { SetBit(value, FLAG_CONTROL); }
		}

		/// <summary>
		/// Flag indicating if the Windows key is pressed.
		/// </summary>
		public bool WinMod
		{
			get { return IsSet(comboValue, FLAG_WIN); }
			set { SetBit(value, FLAG_WIN); }
		}

		// checks if the given flag is set in the value
		private bool IsSet(uint value, uint flag)
		{
			return (value & flag) != 0;
		}

		// sets or unsets a given flag mask in the value
		private void SetBit(bool set, uint flag)
		{
			if (set)
			{
				comboValue |= flag;
			}
			else
			{
				comboValue &= ~flag;
			}
		}

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
			return comboValue;
		}

		/// <summary>
		/// Uses the property value to restore the state of the KeyCombo.
		/// </summary>
		/// <param name="config">uint value from a previous call to ToPropertyValue().</param>
		public void FromPropertyValue(uint config)
		{
			comboValue = config;
		}

		/// <summary>
		/// Converts the key combination into a displayable string
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			string ret;

			if (Enabled)
			{
				ret = "";
				if (WinMod)
				{
					ret += "Win";
				}
				if (ControlMod)
				{
					if (ret.Length > 0)
					{
						ret += "+";
					}
					ret += "Ctrl";
				}
				if (ShiftMod)
				{
					if (ret.Length > 0)
					{
						ret += "+";
					}
					ret += "Shift";
				}
				if (AltMod)
				{
					if (ret.Length > 0)
					{
						ret += "+";
					}
					ret += "Alt";
				}
				if (ret.Length > 0)
				{
					ret += "+";
				}
				ret += KeyComboPanel.KeyCodeToName(KeyCode);
			}
			else
			{
				ret = "--DISABLED--";
			}

			return ret;
		}
	}
}
