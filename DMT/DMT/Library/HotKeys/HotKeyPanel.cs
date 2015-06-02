using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DMT.Library.HotKeys
{
	partial class HotKeyPanel : UserControl
	{
		[Description("Description of hotkey"), Category("Data")]
		public string Description
		{
			get { return labelDescription.Text; }
			set { labelDescription.Text = value; }
		}

		//[Description("Description of hotkey"), Category("Data")]
		//public string KeyCombo
		//{
		//	get { return labelDescription.Text; }
		//	set { labelDescription.Text = value; }
		//}

		//KeyCombo _keyCombo;

		//HotKey _hotKey;
		//public HotKey HotKey { get; set; }

		HotKeyController _hotKeyController = null;

		public HotKeyPanel()
		{
			InitializeComponent();
		}

		public void SetHotKeyController(HotKeyController hotKeyController)
		{
			_hotKeyController = hotKeyController;

			labelDescription.Text = hotKeyController.Description;
			labelKeyCombo.Text = hotKeyController.ToString();
		}

		private void buttonChange_Click(object sender, EventArgs e)
		{
			// show edit box
			if (_hotKeyController != null)
			{
				if (_hotKeyController.Edit())
				{
					labelKeyCombo.Text = _hotKeyController.ToString();
				}
			}
		}
	}
}
