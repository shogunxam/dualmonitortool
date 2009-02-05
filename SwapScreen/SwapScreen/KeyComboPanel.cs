using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SwapScreen
{
	public partial class KeyComboPanel : UserControl
	{
		#region Virtual Key Codes
		private struct VirtualKey
		{
			public Keys virtKey;

			private string keyName;
			public string KeyName
			{
				get { return keyName; }
			}

			public VirtualKey(Keys virtKey, string keyName)
			{
				this.virtKey = virtKey;
				this.keyName = keyName;
			}
		}
		private static VirtualKey[] virtualKeys = new VirtualKey[]
		{
			new VirtualKey(Keys.Back, "<Backspace>" ),
			new VirtualKey(Keys.Tab, "<Tab>" ),
			new VirtualKey(Keys.Enter, "<Enter>" ),
			new VirtualKey(Keys.Pause, "<Pause>" ),
			new VirtualKey(Keys.Escape, "<Esc>" ),
			new VirtualKey(Keys.Space, "<Space>" ),
			new VirtualKey(Keys.PageUp, "<PageUp>" ),
			new VirtualKey(Keys.PageDown, "<PageDown>" ),
			new VirtualKey(Keys.End, "<End>" ),
			new VirtualKey(Keys.Home, "<Home>" ),
			new VirtualKey(Keys.Left, "<Left>" ),
			new VirtualKey(Keys.Up, "<Up>" ),
			new VirtualKey(Keys.Right, "<Right>" ),
			new VirtualKey(Keys.Down, "<Down>" ),
			new VirtualKey(Keys.Insert, "<Insert>" ),
			new VirtualKey(Keys.Delete, "<Delete>" ),

			new VirtualKey(Keys.D0, "0" ),
			new VirtualKey(Keys.D1, "1" ),
			new VirtualKey(Keys.D2, "2" ),
			new VirtualKey(Keys.D3, "3" ),
			new VirtualKey(Keys.D4, "4" ),
			new VirtualKey(Keys.D5, "5" ),
			new VirtualKey(Keys.D6, "6" ),
			new VirtualKey(Keys.D7, "7" ),
			new VirtualKey(Keys.D8, "8" ),
			new VirtualKey(Keys.D9, "9" ),

			new VirtualKey(Keys.A, "A" ),
			new VirtualKey(Keys.B, "B" ),
			new VirtualKey(Keys.C, "C" ),
			new VirtualKey(Keys.D, "D" ),
			new VirtualKey(Keys.E, "E" ),
			new VirtualKey(Keys.F, "F" ),
			new VirtualKey(Keys.G, "G" ),
			new VirtualKey(Keys.H, "H" ),
			new VirtualKey(Keys.I, "I" ),
			new VirtualKey(Keys.J, "J" ),
			new VirtualKey(Keys.K, "K" ),
			new VirtualKey(Keys.L, "L" ),
			new VirtualKey(Keys.M, "M" ),
			new VirtualKey(Keys.N, "N" ),
			new VirtualKey(Keys.O, "O" ),
			new VirtualKey(Keys.P, "P" ),
			new VirtualKey(Keys.Q, "Q" ),
			new VirtualKey(Keys.R, "R" ),
			new VirtualKey(Keys.S, "S" ),
			new VirtualKey(Keys.T, "T" ),
			new VirtualKey(Keys.U, "U" ),
			new VirtualKey(Keys.V, "V" ),
			new VirtualKey(Keys.W, "W" ),
			new VirtualKey(Keys.X, "X" ),
			new VirtualKey(Keys.Y, "Y" ),
			new VirtualKey(Keys.Z, "Z" ),

			new VirtualKey(Keys.NumPad0, "<Num0>" ),
			new VirtualKey(Keys.NumPad1, "<Num1>" ),
			new VirtualKey(Keys.NumPad2, "<Num2>" ),
			new VirtualKey(Keys.NumPad3, "<Num3>" ),
			new VirtualKey(Keys.NumPad4, "<Num4>" ),
			new VirtualKey(Keys.NumPad5, "<Num5>" ),
			new VirtualKey(Keys.NumPad6, "<Num6>" ),
			new VirtualKey(Keys.NumPad7, "<Num7>" ),
			new VirtualKey(Keys.NumPad8, "<Num8>" ),
			new VirtualKey(Keys.NumPad9, "<Num9>" ),

			new VirtualKey(Keys.Multiply, "<Num*>" ),
			new VirtualKey(Keys.Add, "<Num+>" ),
			new VirtualKey(Keys.Subtract, "<Num->" ),
			new VirtualKey(Keys.Decimal, "<Num.>" ),
			new VirtualKey(Keys.Divide, "<Num/>" ),

			new VirtualKey(Keys.F1, "<F1>" ),
			new VirtualKey(Keys.F2, "<F2>" ),
			new VirtualKey(Keys.F3, "<F3>" ),
			new VirtualKey(Keys.F4, "<F4>" ),
			new VirtualKey(Keys.F5, "<F5>" ),
			new VirtualKey(Keys.F6, "<F6>" ),
			new VirtualKey(Keys.F7, "<F7>" ),
			new VirtualKey(Keys.F8, "<F8>" ),
			new VirtualKey(Keys.F9, "<F9>" ),
			new VirtualKey(Keys.F10, "<F10>" ),
			new VirtualKey(Keys.F11, "<F11>" ),
			new VirtualKey(Keys.F12, "<F12>" ),

			new VirtualKey(Keys.Scroll, "<ScrollLock>" )

		};
		#endregion

		private KeyCombo keyCombo;
		public KeyCombo KeyCombo
		{
			get { return keyCombo; }
			set 
			{ 
				keyCombo = value;
				ShowKeyCombo();
			}
		}

		public KeyComboPanel()
		{
			InitializeComponent();
			FillKeysCombo();
		}

		public void ShowKeyCombo()
		{
			chkAlt.Checked = keyCombo.AltMod;
			chkCtrl.Checked = keyCombo.ControlMod;
			chkShift.Checked = keyCombo.ShiftMod;
			chkWin.Checked = keyCombo.WinMod;

			string keyName = KeyCodeToName(keyCombo.KeyCode);
			comboKey.SelectedItem = keyName;
		}

		public void SaveKeyCombo()
		{
			if (comboKey.SelectedItem != null)
			{
				keyCombo.KeyCode = NameToKeyCode(this.comboKey.SelectedItem.ToString());
				keyCombo.AltMod = chkAlt.Checked;
				keyCombo.ControlMod = chkCtrl.Checked;
				keyCombo.ShiftMod = chkShift.Checked;
				keyCombo.WinMod = chkWin.Checked;
			}
		}

		private void FillKeysCombo()
		{
			comboKey.BeginUpdate();
			comboKey.Items.Clear();
			foreach (VirtualKey virtualKey in virtualKeys)
			{
				comboKey.Items.Add(virtualKey.KeyName);
			}
			comboKey.EndUpdate();
		}

		private static string KeyCodeToName(Keys keyCode)
		{
			string keyName = "?";
			foreach (VirtualKey virtualKey in virtualKeys)
			{
				if (virtualKey.virtKey == keyCode)
				{
					keyName = virtualKey.KeyName;
					break;
				}
			}
			return keyName;
		}

		private static Keys NameToKeyCode(string keyName)
		{
			Keys keyCode = 0;
			foreach (VirtualKey virtualKey in virtualKeys)
			{
				if (virtualKey.KeyName == keyName)
				{
					keyCode = virtualKey.virtKey;
					break;
				}
			}
			return keyCode;
		}
	}
}
