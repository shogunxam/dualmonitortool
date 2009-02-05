using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace SwapScreen
{
	public class HotKey : IMessageFilter
	{
		public delegate void HotKeyHandler();
		public event HotKeyHandler HotKeyPressed;

		private KeyCombo hotKeyCombo;

		public KeyCombo HotKeyCombo
		{
			get { return hotKeyCombo; }
			set 
			{
				hotKeyCombo = value;
				if (isRegistered)
				{
					RegisterHotKey();
				}
			}
		}
	
		private Form form;
		private int id;
		private bool isRegistered;

		public HotKey(KeyCombo hotKeyCombo, Form form, int id)
		{
			this.hotKeyCombo = hotKeyCombo;
			this.form = form;
			this.id = id;

			// we need to monitor the messages so we know when the hotkey is pressed
			Application.AddMessageFilter(this);
		}

		public void CleanUp()
		{
			// make sure hot key is not registered
			UnRegisterHotKey();
			Application.RemoveMessageFilter(this);
		}

		public bool RegisterHotKey()
		{
			if (form == null)
			{
				throw new ApplicationException("HotKey must be associated with a form before registering");
			}

			if (isRegistered)
			{
				UnRegisterHotKey();
			}

			isRegistered = Win32.RegisterHotKey(form.Handle, id, 
			                                    hotKeyCombo.Win32Modifier, 
												hotKeyCombo.Win32KeyCode);
			return isRegistered;
		}

		public void UnRegisterHotKey()
		{
			if (isRegistered)
			{
				if (Win32.UnregisterHotKey(form.Handle, id))
				{
					isRegistered = false;
				}
			}
		}

		public bool PreFilterMessage(ref Message m)
		{
			if (m.Msg == Win32.WM_HOTKEY)
			{
				if ((int)m.WParam == id)
				{
					if (HotKeyPressed != null)
					{
						HotKeyPressed();
					}
				}
			}
			return false; // allow message to be dispatched
		}
	}
}
