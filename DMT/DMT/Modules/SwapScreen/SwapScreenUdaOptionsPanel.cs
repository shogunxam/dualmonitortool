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

namespace DMT.Modules.SwapScreen
{
	partial class SwapScreenUdaOptionsPanel : UserControl
	{
		SwapScreenModule _swapScreenModule;

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

	}
}
