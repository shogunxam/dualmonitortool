#region copyright
// This file is part of Dual Monitor Tools which is a set of tools to assist
// users with multiple monitor setups.
// Copyright (C) 2016  Gerald Evans
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

namespace DMT.Library.HotKeys
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel;
	using System.Drawing;
	using System.Data;
	using System.Linq;
	using System.Text;
	using System.Windows.Forms;

	public partial class ScrollableHotKeysPanel : UserControl
	{
		const int MaxPanelsDisplayed = 10;	// as in the [Design]
		IList<HotKeyController> _hotKeyControllers;

		HotKeyPanel[] _hotKeyPanels;

		int _curTop;	// zero based index of top panel displayed into controller array
		int _maxTop;	// max index for top panel displayed into controller array
		int _numPanelsDisplayed;


		//public ScrollableHotKeysPanel(HotKeyController[] hotKeyControllers)
		//{
		//	InitializeComponent();
		//	_hotKeyControllers = hotKeyControllers;
		//	_hotKeyPanels = new HotKeyPanel[] { hotKeyPanel0, hotKeyPanel1, hotKeyPanel2, hotKeyPanel3, hotKeyPanel4,
		//		hotKeyPanel5, hotKeyPanel6, hotKeyPanel7, hotKeyPanel8, hotKeyPanel9 };
		//	InitScrolling();
		//	MapControllersToPanels();
		//}

		/// <summary>
		/// Default ctor so that the panel can be used in the VS designer GUI
		/// Make sure you call Init() afterwards passing in the controllers
		/// </summary>
		public ScrollableHotKeysPanel()
		{
			InitializeComponent();
		}

		public void Init(IList<HotKeyController> hotKeyControllers)
		{
			_hotKeyControllers = hotKeyControllers;

			_hotKeyPanels = new HotKeyPanel[] { hotKeyPanel0, hotKeyPanel1, hotKeyPanel2, hotKeyPanel3, hotKeyPanel4,
				hotKeyPanel5, hotKeyPanel6, hotKeyPanel7, hotKeyPanel8, hotKeyPanel9 };

			InitScrolling();

			MapControllersToPanels();
		}

		void InitScrolling()
		{
			_curTop = 0;	// always start at the top

			if (_hotKeyControllers.Count <= MaxPanelsDisplayed)
			{
				// we can display all of the panels without any scrolling
				_maxTop = 0;
				_numPanelsDisplayed = _hotKeyControllers.Count;

				// we need to hide any un-needed panels
				for (int n = _hotKeyControllers.Count; n < MaxPanelsDisplayed; n++)
				{
					_hotKeyPanels[n].Hide();
				}

				// also need to hide the scroll bar
				vScrollBar.Hide();
			}
			else
			{
				// need to enable scrolling
				_maxTop = _hotKeyControllers.Count - MaxPanelsDisplayed;
				_numPanelsDisplayed = MaxPanelsDisplayed;

				// init scroll bar
				vScrollBar.Minimum = 0;
				//vScrollBar.Maximum = _maxTop + vScrollBar.LargeChange;
				vScrollBar.LargeChange = MaxPanelsDisplayed;
				vScrollBar.Maximum = _hotKeyControllers.Count - 1;
				vScrollBar.Value = _curTop;
			}
		}

		void MapControllersToPanels()
		{
			for (int panelIndex = 0; panelIndex < _numPanelsDisplayed; panelIndex++)
			{
				int controllerIndex = panelIndex + _curTop;
				_hotKeyPanels[panelIndex].SetHotKeyController(_hotKeyControllers[controllerIndex]);
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
