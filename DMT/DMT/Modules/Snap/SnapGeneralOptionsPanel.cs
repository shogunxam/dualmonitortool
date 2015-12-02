using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DMT.Library.HotKeys;

namespace DMT.Modules.Snap
{
	partial class SnapGeneralOptionsPanel : UserControl
	{
		SnapModule _snapModule;

		public SnapGeneralOptionsPanel(SnapModule snapModule)
		{
			_snapModule = snapModule;

			InitializeComponent();

			SetupHotKeys();
		}

		private void SnapGeneralOptionsPanel_Load(object sender, EventArgs e)
		{
			numericUpDownSnaps.Value = (decimal)_snapModule.MaxSnaps;
			checkBoxShowSnap.Checked = _snapModule.AutoShowSnap;
		}

		private void numericUpDownSnaps_ValueChanged(object sender, EventArgs e)
		{
			_snapModule.MaxSnaps = (int)_snapModule.MaxSnaps;
		}

		private void checkBoxShowSnap_CheckedChanged(object sender, EventArgs e)
		{
			_snapModule.AutoShowSnap = checkBoxShowSnap.Checked;
		}

		private void checkBoxExpandSnap_CheckedChanged(object sender, EventArgs e)
		{
			_snapModule.ExpandSnap = checkBoxExpandSnap.Checked;
		}

		private void checkBoxShrinkSnap_CheckedChanged(object sender, EventArgs e)
		{
			_snapModule.ShrinkSnap = checkBoxShrinkSnap.Checked;
		}

		private void checkBoxMaintainAspectRatio_CheckedChanged(object sender, EventArgs e)
		{
			_snapModule.MaintainAspectRatio = checkBoxMaintainAspectRatio.Checked;
		}

		void SetupHotKeys()
		{
			SetupHotKey(hotKeyPanelShowSnap, _snapModule.ShowSnapHotKeyController);
			SetupHotKey(hotKeyPanelTakeSnap, _snapModule.TakeScreenSnapHotKeyController);
			SetupHotKey(hotKeyPanelTakeWinSnap, _snapModule.TakeWindowSnapHotKeyController);
		}

		void SetupHotKey(HotKeyPanel hotKeyPanel, HotKeyController hotKeyController)
		{
			hotKeyPanel.SetHotKeyController(hotKeyController);
		}
	}
}
