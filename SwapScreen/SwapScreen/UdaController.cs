using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace SwapScreen
{
	class UdaController
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

		public UdaController(Form form, int id, string propertyValue)
		{
			// create the hotkey
			hotKey = new HotKey(form, id);

			// register our handler
			hotKey.HotKeyPressed += HotKeyHandler;

			// restore state from serialised data
			InitFromProperty(propertyValue);
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
			this.description = description;

			// set the KeyCombo for the hotkey and register it
			KeyCombo keyCombo = new KeyCombo();
			keyCombo.FromPropertyValue(keyCode);
			hotKey.RegisterHotKey(keyCombo);
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
