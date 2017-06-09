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

namespace DMT.Library.HotKeys
{
	using System;
	using System.Collections.Generic;
	using System.Text;
	using System.Windows.Forms;

	using DMT.Library.PInvoke;
	using DMT.Resources;

	/// <summary>
	/// This is a key combination that can be used as a hotkey,
	/// i.e. a physical key on the keyboard and any modifiers like shift etc.
	/// System.Windows.Forms.Keys almost provides what we want, but it doesn't 
	/// have a modifier for the Windows key.
	/// </summary>
	public struct KeyCombo
	{
		/// <summary>
		/// The key combo value for hotkeys that are disabled
		/// </summary>
		public const uint DisabledComboValue = 0;

		// The value of the KeyCombo is held in an uint,
		// where the least sig 16 bits are used to hold the key code,
		// and the top 16 bits to hold various flags:
		// 0x00010000 indicates if Alt is pressed,
		// 0x00020000 indicates if Control is pressed,
		// 0x00040000 indicates if Shift is pressed,
		// 0x00080000 indicates if Win is pressed,
		// 0x01000000 indicates the key combo is disabled
		public const uint FlagAlt = 0x00010000;        // Win32.MOD_ALT << 16
		public const uint FlagControl = 0x00020000;    // Win32.MOD_CONTROL << 16
		public const uint FlagShift = 0x00040000;      // Win32.MOD_CONTROL << 16
		public const uint FlagWin = 0x00080000;       // Win32.MOD_WIN << 16

		// Unlike SwapScreen, as we don't need to worry about backwards compatibility,
		// we set a flag to indicate the key is enabled, 
		// rather than setting a flag to indicate disabled as SwapScreen did
		const uint FlagEnabled = 0x01000000;

		/// <summary>
		/// Gets or sets the unsigned integer value used to fully represent this key combination
		/// </summary>
		public uint ComboValue { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether the key combination is enabled
		/// </summary>
		public bool Enabled
		{
			get { return IsSet(ComboValue, FlagEnabled); }
			set { SetBit(value, FlagEnabled); }
		}

		/// <summary>
		/// Gets or sets the virtual key code (excluding any modifier)
		/// only the least sig 16 bits of this will be used
		/// </summary>
		public Keys KeyCode
		{
			get { return (Keys)(ComboValue & 0xFFFF); }
			set { ComboValue = (ComboValue & 0xFFFF0000) | ((uint)value & 0xFFFF); }
		}

		/// <summary>
		/// Gets or sets a value indicating whether the Alt key is pressed.
		/// </summary>
		public bool AltMod
		{
			get { return IsSet(ComboValue, FlagAlt); }
			set { SetBit(value, FlagAlt); }
		}

		/// <summary>
		/// Gets or sets a value indicating whether the Shift key is pressed.
		/// </summary>
		public bool ShiftMod
		{
			get { return IsSet(ComboValue, FlagShift); }
			set { SetBit(value, FlagShift); }
		}

		/// <summary>
		/// Gets or sets a value indicating whether the Control key is pressed.
		/// </summary>
		public bool ControlMod
		{
			get { return IsSet(ComboValue, FlagControl); }
			set { SetBit(value, FlagControl); }
		}

		/// <summary>
		/// Gets or sets a value indicating whether the Windows key is pressed.
		/// </summary>
		public bool WinMod
		{
			get { return IsSet(ComboValue, FlagWin); }
			set { SetBit(value, FlagWin); }
		}

		/// <summary>
		/// Gets the KeyCode as used by Win32.
		/// Win32 uses the same value for the key codes as used
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
		/// Gets the modifiers as used by Win32.
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
					modifier |= NativeMethods.MOD_ALT;
				}

				if (ControlMod)
				{
					modifier |= NativeMethods.MOD_CONTROL;
				}

				if (ShiftMod)
				{
					modifier |= NativeMethods.MOD_SHIFT;
				}

				if (WinMod)
				{
					modifier |= NativeMethods.MOD_WIN;
				}

				return modifier;
			}
		}

		/// <summary>
		/// Converts the state of the KeyCombo to a single unsigned integer
		/// so that it can be saved as a property.
		/// </summary>
		/// <returns>Unsigned integer that can be passed to FromPropertyValue() to restore the current state.</returns>
		public uint ToPropertyValue()
		{
			return ComboValue;
		}

		/// <summary>
		/// Uses the property value to restore the state of the KeyCombo.
		/// </summary>
		/// <param name="config">Unsigned integer value from a previous call to ToPropertyValue().</param>
		public void FromPropertyValue(uint config)
		{
			ComboValue = config;
		}

		/// <summary>
		/// Converts the key combination into a displayable string
		/// </summary>
		/// <returns>A displayable string representation of the hotkey</returns>
		public override string ToString()
		{
			string ret;

			if (Enabled)
			{
				ret = string.Empty;
				if (WinMod)
				{
					ret += CommonStrings.WinModifierKey;	// "Win";
				}

				if (ControlMod)
				{
					if (ret.Length > 0)
					{
						ret += "+";
					}

					ret += CommonStrings.CtrlModifierKey;	// "Ctrl";
				}

				if (ShiftMod)
				{
					if (ret.Length > 0)
					{
						ret += "+";
					}

					ret += CommonStrings.ShiftModifierKey;	// "Shift";
				}

				if (AltMod)
				{
					if (ret.Length > 0)
					{
						ret += "+";
					}

					ret += CommonStrings.AltModifierKey;	// "Alt";
				}

				if (ret.Length > 0)
				{
					ret += "+";
				}

				ret += VirtualKey.CodeToName(KeyCode);
			}
			else
			{
				ret = CommonStrings.DisabledHotKey;	// "--DISABLED--";
			}

			return ret;
		}

		// checks if the given flag is set in the value
		bool IsSet(uint value, uint flag)
		{
			return (value & flag) != 0;
		}

		// sets or unsets a given flag mask in the value
		void SetBit(bool set, uint flag)
		{
			if (set)
			{
				ComboValue |= flag;
			}
			else
			{
				ComboValue &= ~flag;
			}
		}
	}
}
