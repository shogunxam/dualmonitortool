#region copyright
// This file is part of Dual Monitor Tools which is a set of tools to assist
// users with multiple monitor setups.
// Copyright (C) 2015  Gerald Evans
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
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;
	using System.Windows.Forms;

	/// <summary>
	/// Represents a single key.
	/// This is used to map between a key code and
	/// a displayable name for the key code.
	/// </summary>
	class VirtualKey
	{
		static VirtualKey[] _virtualKeys = new VirtualKey[]
		{
			new VirtualKey(Keys.Back, "<Backspace>"),
			new VirtualKey(Keys.Delete, "<Delete>"),
			//new VirtualKey(Keys.Divide, "<Divide>"), - this is the <Num/>
			new VirtualKey(Keys.Down, "<Down>"),
			new VirtualKey(Keys.End, "<End>"),
			new VirtualKey(Keys.Enter, "<Enter>"),
			new VirtualKey(Keys.Escape, "<Esc>"),
			new VirtualKey(Keys.Home, "<Home>"),
			new VirtualKey(Keys.Insert, "<Insert>"),
			new VirtualKey(Keys.Left, "<Left>"),
			new VirtualKey(Keys.PageDown, "<PageDown>"),
			new VirtualKey(Keys.PageUp, "<PageUp>"),
			new VirtualKey(Keys.Pause, "<Pause>"),
			new VirtualKey(Keys.PrintScreen, "<PrintScrn>"),
			new VirtualKey(Keys.Right, "<Right>"),
			new VirtualKey(Keys.Scroll, "<ScrollLock>"),
			new VirtualKey(Keys.Space, "<Space>"),
			new VirtualKey(Keys.Tab, "<Tab>"),
			new VirtualKey(Keys.Up, "<Up>"),

			new VirtualKey(Keys.OemBackslash, "<OemBackslash>"),
			new VirtualKey(Keys.OemCloseBrackets, "<OemCloseBrackets>"),
			new VirtualKey(Keys.Oemcomma, "<OemComma>"),
			new VirtualKey(Keys.OemMinus, "<OemMinus>"),
			new VirtualKey(Keys.OemOpenBrackets, "<OemOpenBrackets>"),
			new VirtualKey(Keys.OemPeriod, "<OemPeriod>"),
			new VirtualKey(Keys.OemPipe, "<OemPipe>"),
			new VirtualKey(Keys.Oemplus, "<OemPlus>"),
			new VirtualKey(Keys.OemQuestion, "<OemQuestion>"),
			new VirtualKey(Keys.OemQuotes, "<OemQuotes>"),
			new VirtualKey(Keys.OemSemicolon, "<OemSemicolon>"),
			new VirtualKey(Keys.Oemtilde, "<OemTilde>"),
			
			new VirtualKey(Keys.F1, "<F1>"),
			new VirtualKey(Keys.F2, "<F2>"),
			new VirtualKey(Keys.F3, "<F3>"),
			new VirtualKey(Keys.F4, "<F4>"),
			new VirtualKey(Keys.F5, "<F5>"),
			new VirtualKey(Keys.F6, "<F6>"),
			new VirtualKey(Keys.F7, "<F7>"),
			new VirtualKey(Keys.F8, "<F8>"),
			new VirtualKey(Keys.F9, "<F9>"),
			new VirtualKey(Keys.F10, "<F10>"),
			new VirtualKey(Keys.F11, "<F11>"),
			new VirtualKey(Keys.F12, "<F12>"),

			// The multimedia keys (with and without moddifiers)
			// can't be used as hotkeys
			////new VirtualKey(Keys.MediaNextTrack, "<MediaNextTrack>" ),
			////new VirtualKey(Keys.MediaPlayPause, "<MediaPlayPause>" ),
			////new VirtualKey(Keys.MediaPreviousTrack, "<MediaPreviousTrack>" ),
			////new VirtualKey(Keys.MediaStop, "<MediaStop>" ),
			////new VirtualKey(Keys.VolumeDown, "<VolumeDown>" ),
			////new VirtualKey(Keys.VolumeDown, "<VolumeMute>" ),
			////new VirtualKey(Keys.VolumeUp, "<VolumeUp>" ),

			////new VirtualKey(Keys.BrowserBack, "<BrowserBack>" ),
			////new VirtualKey(Keys.BrowserFavorites, "<BrowserFavorites>" ),
			////new VirtualKey(Keys.BrowserForward, "<BrowserForward>" ),
			////new VirtualKey(Keys.BrowserHome, "<BrowserHome>" ),
			////new VirtualKey(Keys.BrowserRefresh, "<BrowserRefresh>" ),
			////new VirtualKey(Keys.BrowserSearch, "<BrowserSearch>" ),
			////new VirtualKey(Keys.BrowserStop, "<BrowserStop>" ),

			new VirtualKey(Keys.NumPad0, "<Num0>"),
			new VirtualKey(Keys.NumPad1, "<Num1>"),
			new VirtualKey(Keys.NumPad2, "<Num2>"),
			new VirtualKey(Keys.NumPad3, "<Num3>"),
			new VirtualKey(Keys.NumPad4, "<Num4>"),
			new VirtualKey(Keys.NumPad5, "<Num5>"),
			new VirtualKey(Keys.NumPad6, "<Num6>"),
			new VirtualKey(Keys.NumPad7, "<Num7>"),
			new VirtualKey(Keys.NumPad8, "<Num8>"),
			new VirtualKey(Keys.NumPad9, "<Num9>"),

			new VirtualKey(Keys.Multiply, "<Num*>"),
			new VirtualKey(Keys.Add, "<Num+>"),
			new VirtualKey(Keys.Subtract, "<Num->"),
			new VirtualKey(Keys.Decimal, "<Num.>"),
			new VirtualKey(Keys.Divide, "<Num/>"),

			new VirtualKey(Keys.D0, "0"),
			new VirtualKey(Keys.D1, "1"),
			new VirtualKey(Keys.D2, "2"),
			new VirtualKey(Keys.D3, "3"),
			new VirtualKey(Keys.D4, "4"),
			new VirtualKey(Keys.D5, "5"),
			new VirtualKey(Keys.D6, "6"),
			new VirtualKey(Keys.D7, "7"),
			new VirtualKey(Keys.D8, "8"),
			new VirtualKey(Keys.D9, "9"),

			new VirtualKey(Keys.A, "A"),
			new VirtualKey(Keys.B, "B"),
			new VirtualKey(Keys.C, "C"),
			new VirtualKey(Keys.D, "D"),
			new VirtualKey(Keys.E, "E"),
			new VirtualKey(Keys.F, "F"),
			new VirtualKey(Keys.G, "G"),
			new VirtualKey(Keys.H, "H"),
			new VirtualKey(Keys.I, "I"),
			new VirtualKey(Keys.J, "J"),
			new VirtualKey(Keys.K, "K"),
			new VirtualKey(Keys.L, "L"),
			new VirtualKey(Keys.M, "M"),
			new VirtualKey(Keys.N, "N"),
			new VirtualKey(Keys.O, "O"),
			new VirtualKey(Keys.P, "P"),
			new VirtualKey(Keys.Q, "Q"),
			new VirtualKey(Keys.R, "R"),
			new VirtualKey(Keys.S, "S"),
			new VirtualKey(Keys.T, "T"),
			new VirtualKey(Keys.U, "U"),
			new VirtualKey(Keys.V, "V"),
			new VirtualKey(Keys.W, "W"),
			new VirtualKey(Keys.X, "X"),
			new VirtualKey(Keys.Y, "Y"),
			new VirtualKey(Keys.Z, "Z")
		};

		/// <summary>
		/// Initialises a new instance of the <see cref="VirtualKey" /> class.
		/// </summary>
		/// <param name="code">Code for the key</param>
		/// <param name="name">Name of the key</param>
		public VirtualKey(Keys code, string name)
		{
			Code = code;
			Name = name;
		}

		/// <summary>
		/// Gets all virtual keys
		/// </summary>
		public static IEnumerable<VirtualKey> AllVirtualKeys
		{
			get
			{
				return _virtualKeys;
			}
		}

		/// <summary>
		/// Gets or sets the code for the key
		/// </summary>
		public Keys Code { get; protected set; }

		/// <summary>
		/// Gets or sets the name for the key
		/// </summary>
		public string Name { get; protected set; }

		/// <summary>
		/// Converts the key code to a displayable name
		/// </summary>
		/// <param name="keyCode">key code to convert</param>
		/// <returns>displayable name</returns>
		public static string CodeToName(Keys keyCode)
		{
			string keyName = "?";
			foreach (VirtualKey virtualKey in _virtualKeys)
			{
				if (virtualKey.Code == keyCode)
				{
					keyName = virtualKey.Name;
					break;
				}
			}

			return keyName;
		}

		/// <summary>
		/// Converts a name to a key code
		/// </summary>
		/// <param name="keyName">Name to convert</param>
		/// <returns>key code</returns>
		public static Keys NameToCode(string keyName)
		{
			Keys keyCode = 0;
			foreach (VirtualKey virtualKey in _virtualKeys)
			{
				if (virtualKey.Name == keyName)
				{
					keyCode = virtualKey.Code;
					break;
				}
			}

			return keyCode;
		}
	}
}
