using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace SwapScreen
{
	public struct KeyCombo
	{
		// the virtual keycode (excluding any modifier)
		public Keys KeyCode;
		// the modifiers
		public bool AltMod;
		public bool ShiftMod;
		public bool ControlMod;
		public bool WinMod;

		public uint Win32KeyCode
		{
			get
			{
				return (uint)KeyCode;
			}
		}

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

		public uint ToPropertyValue()
		{
			return Win32KeyCode | (Win32Modifier << 16);
		}

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
