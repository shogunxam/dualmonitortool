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

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DMT.Modules.Launcher
{
	partial class LauncheGeneralOptionsPanel : UserControl
	{
		LauncherModule _launcherModule;

		public LauncheGeneralOptionsPanel(LauncherModule launcherModule)
		{
			_launcherModule = launcherModule;

			InitializeComponent();
		}

		private void LauncherImportOptionsPanel_Load(object sender, EventArgs e)
		{
			checkBoxMru.Checked = _launcherModule.UseMru;
			checkBoxLoadWordsOnStartup.Checked = _launcherModule.LoadWordsOnStartup;
			numericUpDownIcons.Value = (decimal)_launcherModule.MaxIcons;

			if (_launcherModule.IconView == View.Details)
			{
				radioButtonIconDetails.Checked = true;
			}
			else if (_launcherModule.IconView == View.List)
			{
				radioButtonIconList.Checked = true;
			}
			else
			{
				radioButtonIconLargeIcon.Checked = true;
			}

			numericUpDownTimeout.Value = (decimal)_launcherModule.StartupTimeout;
		}

		private void checkBoxMru_CheckedChanged(object sender, EventArgs e)
		{
			_launcherModule.UseMru = checkBoxMru.Checked;
		}

		private void checkBoxLoadWordsOnStartup_CheckedChanged(object sender, EventArgs e)
		{
			_launcherModule.LoadWordsOnStartup = checkBoxLoadWordsOnStartup.Checked;
		}

		private void numericUpDownIcons_ValueChanged(object sender, EventArgs e)
		{
			_launcherModule.MaxIcons = (int)numericUpDownIcons.Value;
		}

		private void radioButton_CheckedChanged(object sender, EventArgs e)
		{
			View iconView = View.LargeIcon;	// the default
			if (radioButtonIconDetails.Checked)
			{
				iconView = View.Details;
			}
			else if (radioButtonIconList.Checked)
			{
				iconView = View.List;
			}

			if (iconView != _launcherModule.IconView)
			{
				_launcherModule.IconView = iconView;
			}
		}

		private void numericUpDownTimeout_ValueChanged(object sender, EventArgs e)
		{
			_launcherModule.StartupTimeout = (int)numericUpDownTimeout.Value;
		}
	}
}
