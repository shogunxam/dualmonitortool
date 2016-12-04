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

namespace DMT.Modules.SwapScreen
{
	using DMT.Resources;
	using System;
	using System.Collections.Generic;
	using System.ComponentModel;
	using System.Data;
	using System.Drawing;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;
	using System.Windows.Forms;

	/// <summary>
	/// Options panel for swap screen user defined areas
	/// </summary>
	partial class SwapScreenUdaOptionsPanel : UserControl
	{
		SwapScreenModule _swapScreenModule;

		/// <summary>
		/// Initialises a new instance of the <see cref="SwapScreenUdaOptionsPanel" /> class.
		/// </summary>
		/// <param name="swapScreenModule">Swap screen module</param>
		public SwapScreenUdaOptionsPanel(SwapScreenModule swapScreenModule)
		{
			_swapScreenModule = swapScreenModule;

			InitializeComponent();

			SetupUdas();
		}

		void SetupUdas()
		{
			SetupUda(udaPanel1, 0);
			SetupUda(udaPanel2, 1);
			SetupUda(udaPanel3, 2);
			SetupUda(udaPanel4, 3);
			SetupUda(udaPanel5, 4);
			SetupUda(udaPanel6, 5);
			SetupUda(udaPanel7, 6);
			SetupUda(udaPanel8, 7);
			SetupUda(udaPanel9, 8);
			SetupUda(udaPanel10, 9);
		}

		void SetupUda(UdaPanel udaPanel, int idx)
		{
			UdaController udaController = GetUdaController(idx);
			if (udaController != null)
			{
				udaPanel.SetUdaController(udaController);
			}
			else
			{
				// TODO: what if udaController is null
				throw new ApplicationException("Not enough UdaControllers");
			}
		}

		UdaController GetUdaController(int idx)
		{
			if (idx < _swapScreenModule.UdaControllers.Count)
			{
				return _swapScreenModule.UdaControllers[idx];
			}

			return null;
		}

		private void buttonResetUDA_Click(object sender, EventArgs e)
		{
			// get confirmation from user before updating their UDAs
			if (MessageBox.Show(SwapScreenStrings.ConfirmResetUdas, CommonStrings.MyTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
			{
				_swapScreenModule.ResetUdas();
				// Yes, this is horrible
				// TODO: should this be done when generating the UDAs?
				udaPanel1.UpdateDisplay();
				udaPanel2.UpdateDisplay();
				udaPanel3.UpdateDisplay();
				udaPanel4.UpdateDisplay();
				udaPanel5.UpdateDisplay();
				udaPanel6.UpdateDisplay();
				udaPanel7.UpdateDisplay();
				udaPanel8.UpdateDisplay();
				udaPanel9.UpdateDisplay();
				udaPanel10.UpdateDisplay();
			}
		}
	}
}
