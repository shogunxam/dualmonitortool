#region copyright
// This file is part of Dual Monitor Tools which is a set of tools to assist
// users with multiple monitor setups.
// Copyright (C) 2011  Gerald Evans
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
using System.Drawing;
using System.Windows.Forms;

namespace SwapScreen
{
	public class UdaController
	{
		private string description;
		public string Description
		{
			get { return description; }
			set { description = value; }
		}

		private Rectangle position;
		public Rectangle Position
		{
			get { return position; }
			set { position = value; }
		}

		// The HotKey does the real work
		private HotKey hotKey;

		/// <summary>
		/// The KeyCombo that we will be using as the hotkey.
		/// </summary>
		public KeyCombo HotKeyCombo
		{
			get { return hotKey.HotKeyCombo; }
		}

		public UdaController(Form form, int id, string propertyValue)
		{
			// create the hotkey
			hotKey = new HotKey(form, id);

			// register our handler
			hotKey.HotKeyPressed += HotKeyHandler;

			// restore state from serialised data
			InitFromProperty(propertyValue);
		}

		public UdaController(Form form, int id)
		{
			// create the hotkey
			hotKey = new HotKey(form, id);

			// register our handler
			hotKey.HotKeyPressed += HotKeyHandler;
		}

		~UdaController()
		{
			Dispose(false);
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		private void Dispose(bool disposing)
		{
			if (disposing)
			{
				hotKey.Dispose();
			}
		}

		public void InitFromProperty(string propertyValue)
		{
			// the propertyValue is of the form: "KeyCombo|X|Y|Width|Height|Description"
			// eg."655409|0|0|640|800|screen1 left"
			// note: description could have embedded |'s
			uint keyCode = 0;
			int x = 0;
			int y = 0;
			int width = 0;
			int height = 0;
			string description = "";

			string[] fields = propertyValue.Split("|".ToCharArray());
			keyCode = GetFieldAsUInt(fields, 0, KeyCombo.DisabledComboValue);
			x = GetFieldAsInt(fields, 1, 0);
			y = GetFieldAsInt(fields, 2, 0);
			width = GetFieldAsInt(fields, 3, 0);
			height = GetFieldAsInt(fields, 4, 0);
			//description = GetFieldsAsString(fields, 5, "");
			if (fields.Length > 5)
			{
				description = string.Join("|", fields, 5, fields.Length - 5);
			}

			// save values
			position = new Rectangle(x, y, width, height);
			SetValues(keyCode, position, description);
			//this.description = description;

			//// set the KeyCombo for the hotkey and register it
			//KeyCombo keyCombo = new KeyCombo();
			//keyCombo.FromPropertyValue(keyCode);
			//hotKey.RegisterHotKey(keyCombo);
		}

		public bool SetValues(uint keyCode, Rectangle position, string description)
		{
			KeyCombo keyCombo = new KeyCombo();
			keyCombo.ComboValue = keyCode;
			if (!hotKey.RegisterHotKey(keyCombo))
			{
				return false;
			}

			this.position = position;
			this.description = description;
			return true;
		}

		private int GetFieldAsInt(string[] fields, int fieldIndex, int defaultValue)
		{
			int ret = defaultValue;

			if (fields.Length > fieldIndex)
			{
				try
				{
					ret = Int32.Parse(fields[fieldIndex]);
				}
				catch (Exception)
				{
				}
			}
			return ret;
		}

		private uint GetFieldAsUInt(string[] fields, int fieldIndex, uint defaultValue)
		{
			uint ret = defaultValue;

			if (fields.Length > fieldIndex)
			{
				try
				{
					ret = UInt32.Parse(fields[fieldIndex]);
				}
				catch (Exception)
				{
				}
			}
			return ret;
		}

		//private string GetFieldAsString(string[] fields, int fieldIndex, string defaultValue)
		//{
		//    string ret = defaultValue;

		//    if (fields.Length > fieldIndex)
		//    {
		//        ret = fields[fieldIndex];
		//    }
		//    return ret;
		//}

		public string GetPropertyValue()
		{
			return ToPropertyValue(hotKey.HotKeyCombo.ToPropertyValue(), position, description);
		}

		public static string ToPropertyValue(uint keyCode, Rectangle rect, string description)
		{
			string ret = string.Format("{0}|{1}|{2}|{3}|{4}|{5}",
										keyCode,
										rect.Left, rect.Top, rect.Width, rect.Height,
										description);

			return ret;
		}

		public void HotKeyHandler()
		{
			ScreenHelper.MoveActiveToRectangle(position);
		}
	}
}
