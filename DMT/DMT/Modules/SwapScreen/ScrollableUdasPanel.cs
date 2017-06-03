#region copyright
// This file is part of Dual Monitor Tools which is a set of tools to assist
// users with multiple monitor setups.
// Copyright (C) 2017  Gerald Evans
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
	using System;
	using System.Collections.Generic;
	using System.ComponentModel;
	using System.Drawing;
	using System.Data;
	using System.Linq;
	using System.Text;
	using System.Windows.Forms;

	// TODO: merge with ScrollableHotKeysPanel
	// or at least have a base class to remove duplication of code

	partial class ScrollableUdasPanel : UserControl
	{
		const int MaxPanelsDisplayed = 10;	// as in the [Design]
		IList<UdaController> _udaControllers;

		UdaPanel[] _udaPanels;

		int _curTop;	// zero based index of top panel displayed into controller array
		int _maxTop;	// max index for top panel displayed into controller array
		int _numPanelsDisplayed;


		public ScrollableUdasPanel()
		{
			InitializeComponent();
		}

		public void Init(IList<UdaController> udaControllers)
		{
			_udaControllers = udaControllers;

			_udaPanels = new UdaPanel[] { udaPanel0, udaPanel1, udaPanel2, udaPanel3, udaPanel4,
				udaPanel5, udaPanel6, udaPanel7, udaPanel8, udaPanel9 };

			InitScrolling();

			MapControllersToPanels();
		}

		public void UpdateDisplay()
		{
			// called when one or more panels need to be forced to refresh

			foreach (UdaPanel udaPanel in _udaPanels)
			{
				udaPanel.UpdateDisplay();
			}
		}

		void InitScrolling()
		{
			_curTop = 0;	// always start at the top

			if (_udaControllers.Count <= MaxPanelsDisplayed)
			{
				// we can display all of the panels without any scrolling
				// shouldn't be needed for UDAs, but we support it jic
				_maxTop = 0;
				_numPanelsDisplayed = _udaControllers.Count;

				// we need to hide any un-needed panels
				for (int n = _udaControllers.Count; n < MaxPanelsDisplayed; n++)
				{
					_udaPanels[n].Hide();
				}

				// also need to hide the scroll bar
				vScrollBar.Hide();
			}
			else
			{
				// need to enable scrolling
				_maxTop = _udaControllers.Count - MaxPanelsDisplayed;
				_numPanelsDisplayed = MaxPanelsDisplayed;

				// init scroll bar
				vScrollBar.Minimum = 0;
				//vScrollBar.Maximum = _maxTop + vScrollBar.LargeChange;
				vScrollBar.LargeChange = MaxPanelsDisplayed;
				vScrollBar.Maximum = _udaControllers.Count - 1;
				vScrollBar.Value = _curTop;
			}
		}

		void MapControllersToPanels()
		{
			for (int panelIndex = 0; panelIndex < _numPanelsDisplayed; panelIndex++)
			{
				int controllerIndex = panelIndex + _curTop;
				_udaPanels[panelIndex].SetUdaController(_udaControllers[controllerIndex]);
			}
		}

		private void vScrollBar_Scroll(object sender, ScrollEventArgs e)
		{
			SetNewScrollTop(e.NewValue);
		}

		void SetNewScrollTop(int newTop)
		{
			if (newTop < 0)
			{
				newTop = 0;
			}

			if (newTop > _maxTop)
			{
				newTop = _maxTop;
			}

			if (newTop != _curTop)
			{
				_curTop = newTop;
				MapControllersToPanels();
			}
		}
	}
}
