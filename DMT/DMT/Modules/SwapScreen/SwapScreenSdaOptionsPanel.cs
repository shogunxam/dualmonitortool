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

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DMT.Library.HotKeys;

namespace DMT.Modules.SwapScreen
{
	partial class SwapScreenSdaOptionsPanel : UserControl
	{
		class ModifierItem
		{
			public uint Modifier { get; private set; }

			public ModifierItem(uint modifier)
			{
				Modifier = modifier;
			}

			public override string ToString()
			{
				KeyCombo keyCombo = new KeyCombo(Modifier);
				return keyCombo.GetModifierName();
			}
		}

		SwapScreenModule _swapScreenModule;
		bool _loading = false;

		uint[] defaultSdaModifiers = 
		{
			KeyCombo.FlagControl,
			KeyCombo.FlagWin,
			KeyCombo.FlagAlt,
			KeyCombo.FlagShift,

			// add all 2 modifier combination
			KeyCombo.FlagShift | KeyCombo.FlagControl,
			KeyCombo.FlagShift | KeyCombo.FlagWin,
			KeyCombo.FlagShift | KeyCombo.FlagAlt,

			KeyCombo.FlagWin | KeyCombo.FlagControl,
			KeyCombo.FlagWin | KeyCombo.FlagAlt,

			KeyCombo.FlagControl | KeyCombo.FlagAlt

			// could add 3 modifier combinations, but these are tricky to press

		};

		/// <summary>
		/// Initialises a new instance of the <see cref="SwapScreenUdaOptionsPanel" /> class.
		/// </summary>
		/// <param name="swapScreenModule">Swap screen module</param>
		public SwapScreenSdaOptionsPanel(SwapScreenModule swapScreenModule)
		{
			_swapScreenModule = swapScreenModule;

			InitializeComponent();

			//checkBoxEnable.Checked = _swapScreenModule.EnableSdas;
		}

		public void ShowErrors()
		{
			listBoxRegistrationErrors.BeginUpdate();
			listBoxRegistrationErrors.Items.Clear();
			foreach (string error in _swapScreenModule.SdaHotKeyErrors)
			{
				listBoxRegistrationErrors.Items.Add(error);
			}
			listBoxRegistrationErrors.EndUpdate();
		}

		private void SwapScreenSdaOptionsPanel_Load(object sender, EventArgs e)
		{
			_loading = true;
			checkBoxEnable.Checked = _swapScreenModule.EnableSdas;
			FillModifiersList();
			ShowErrors();
			UpdateButtonStates();
			_loading = false;
		}

		private void checkBoxEnable_CheckedChanged(object sender, EventArgs e)
		{
			// prevent processing if we are just loading the form
			if (!_loading)
			{
				_swapScreenModule.EnableSdas = checkBoxEnable.Checked;
				RegenerateSDAs();
			}
		}

		void FillModifiersList()
		{
			uint[] savedSdaModifiers = _swapScreenModule.SdaModifiers;
			if (savedSdaModifiers.Length < defaultSdaModifiers.Length)
			{
				// extend the savedList with missing values from the default list
				// TODO: being lazy for now and just replacing it
				savedSdaModifiers = defaultSdaModifiers;
				_swapScreenModule.SdaModifiers = savedSdaModifiers;
			}
			
			// populate the listbox
			listBoxModifiers.BeginUpdate();
			listBoxModifiers.Items.Clear();
			foreach (uint modifier in savedSdaModifiers)
			{
				ModifierItem item = new ModifierItem(modifier);
				listBoxModifiers.Items.Add(item);
			}
			listBoxModifiers.EndUpdate();
		}

		void RegenerateSDAs()
		{
			//listBoxRegistrationErrors.Items.Clear();
			_swapScreenModule.GenerateSDAs();
			//ShowErrors(); - this will be called automatically by above
		}

		private void listBoxModifiers_MouseDown(object sender, MouseEventArgs e)
		{
			object selectedItem = listBoxModifiers.SelectedItem;
			if (selectedItem != null)
			{
				listBoxModifiers.DoDragDrop(selectedItem, DragDropEffects.Move);
			}

			// SelectedIndexChanged doesn't seem to fire in this case
			// so we force a check for changed selected \vsindex
			UpdateButtonStates();
		}

		private void listBoxModifiers_DragOver(object sender, DragEventArgs e)
		{
			e.Effect = DragDropEffects.Move;
		}

		private void listBoxModifiers_DragDrop(object sender, DragEventArgs e)
		{
			Point point = listBoxModifiers.PointToClient(new Point(e.X, e.Y));
			int index = listBoxModifiers.IndexFromPoint(point);
			if (index < 0)
			{
				index = listBoxModifiers.Items.Count - 1;
			}
			object data = e.Data.GetData(typeof(ModifierItem));
			listBoxModifiers.Items.Remove(data);
			listBoxModifiers.Items.Insert(index, data);

			// select dropped item
			listBoxModifiers.SelectedIndex = index;
			UpdateButtonStates();

			UseNewModifierOrder();
		}

		private void listBoxModifiers_DrawItem(object sender, DrawItemEventArgs e)
		{
			e.DrawBackground();

			Brush myBrush = Brushes.Black;

			string text = "Screen " + (e.Index + 1);
			text += " -> " + listBoxModifiers.Items[e.Index].ToString();

			e.Graphics.DrawString(text, e.Font, myBrush, e.Bounds, StringFormat.GenericDefault);
			e.DrawFocusRectangle();
		}

		private void buttonUp_Click(object sender, EventArgs e)
		{
			int selectedIndex = listBoxModifiers.SelectedIndex;
			if (selectedIndex > 0)
			{
				// can move item up
				object current = listBoxModifiers.Items[selectedIndex];
				object previous = listBoxModifiers.Items[selectedIndex - 1];
				// now swap
				listBoxModifiers.Items[selectedIndex - 1] = current;
				listBoxModifiers.Items[selectedIndex] = previous;
				// and make sure moved item is selected
				listBoxModifiers.SelectedIndex = selectedIndex - 1;

				UseNewModifierOrder();
			}
		}

		private void buttonDown_Click(object sender, EventArgs e)
		{
			int selectedIndex = listBoxModifiers.SelectedIndex;
			if (selectedIndex >= 0 && selectedIndex < listBoxModifiers.Items.Count - 1)
			{
				// can move item down
				object current = listBoxModifiers.Items[selectedIndex];
				object next = listBoxModifiers.Items[selectedIndex + 1];
				// now swap
				listBoxModifiers.Items[selectedIndex + 1] = current;
				listBoxModifiers.Items[selectedIndex] = next;
				// and make sure moved item is selected
				listBoxModifiers.SelectedIndex = selectedIndex + 1;

				UseNewModifierOrder();
			}
		}

		private void listBoxModifiers_SelectedIndexChanged(object sender, EventArgs e)
		{
			UpdateButtonStates();
		}

		void UpdateButtonStates()
		{
			bool enableMoveUp = false;
			bool enableMoveDown = false;

			int selectedIndex = listBoxModifiers.SelectedIndex;

			if (selectedIndex >= 0)
			{
				if (selectedIndex > 0)
				{
					enableMoveUp = true;
				}

				if (selectedIndex < listBoxModifiers.Items.Count - 1)
				{
					enableMoveDown = true;
				}
			}

			buttonUp.Enabled = enableMoveUp;
			buttonDown.Enabled = enableMoveDown;
		}

		void UseNewModifierOrder()
		{
			// get the current order
			List<uint> newOrder = new List<uint>();
			for (int index = 0; index < listBoxModifiers.Items.Count; index++)
			{
				ModifierItem modifierItem = listBoxModifiers.Items[index] as ModifierItem;
				if (modifierItem != null)
				{
					newOrder.Add(modifierItem.Modifier);
				}
			}

			// save the new order
			_swapScreenModule.SdaModifiers = newOrder.ToArray();

			// and force regeneration of SDAs
			RegenerateSDAs();
		}
	}
}
