using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DMT.Modules.General
{
	public partial class BrightnessForm : Form
	{
		DisplayDevices _displayDevices;
		int _monitorIndex;
		uint _originalBrightness;

		public BrightnessForm(DisplayDevices displayDevices, int monitorIndex, uint curBrightness, uint minBrightness, uint maxBrightness)
		{
			_displayDevices = displayDevices;
			_monitorIndex = monitorIndex;
			_originalBrightness = curBrightness;

			InitializeComponent();

			trackBarBrightness.Minimum = (int)minBrightness;
			trackBarBrightness.Maximum = (int)maxBrightness;
			trackBarBrightness.Value = (int)curBrightness;

			labelMinBrightness.Text = minBrightness.ToString();
			labelMaxBrightness.Text = maxBrightness.ToString();
			ShowCurBrightnessValue();
		}

		private void buttonOK_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.OK;
			Close();
		}

		private void buttonCancel_Click(object sender, EventArgs e)
		{
			// restore brightness to original value
			_displayDevices.ChangeMonitorBrightness(_monitorIndex, _originalBrightness);

			DialogResult = DialogResult.Cancel;
			Close();
		}

		private void trackBarBrightness_Scroll(object sender, EventArgs e)
		{
			_displayDevices.ChangeMonitorBrightness(_monitorIndex, (uint)trackBarBrightness.Value);
			ShowCurBrightnessValue();
		}

		void ShowCurBrightnessValue()
		{
			labelCurBrightness.Text = trackBarBrightness.Value.ToString();
		}
	}
}
