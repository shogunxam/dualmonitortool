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
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace DMT.Modules.Launcher
{
	/// <summary>
	/// Control to display a selection of MagicWords
	/// </summary>
	public class MagicWordListBox : ListView
	{
		/// <summary>
		/// Performs one time initialisation of the control.
		/// Should be called from the owners OnLoad()
		/// </summary>
		public void InitControl()
		{
			//this.View = View.LargeIcon;
			//this.View = View.List;
			//this.View = View.Details;
			//this.View = Properties.Settings.Default.IconView;
			this.View = System.Windows.Forms.View.LargeIcon;
		}

		/// <summary>
		/// Sets the list of MagicWords that are to be displayed in the control
		/// </summary>
		/// <param name="magicWords"></param>
		public void SetMagicWords(List<MagicWord> magicWords)
		{
			this.BeginUpdate();
			this.Items.Clear();

			ImageList imageList = new ImageList();
			imageList.ImageSize = new Size(32, 32);
			imageList.ColorDepth = ColorDepth.Depth32Bit;

			// first icon slot is used for the generic icon we use when we can't find the
			// applications icon
			imageList.Images.Add(Properties.Resources.missingIcon);

			if (magicWords != null)
			{
				// Could use null for the parameterMap
				ParameterMap map = new ParameterMap();
				foreach (MagicWord mw in magicWords)
				{
					MagicWordExecutable executable = new MagicWordExecutable(mw, map);
					int imageIndex = 0;	// the default (missing icon)
					Icon fileIcon = executable.Icon;
					if (fileIcon != null)
					{
						// found an icon for the application, so use it
						imageList.Images.Add(fileIcon);
						imageIndex = imageList.Images.Count - 1;
					}

					ListViewItem listViewItem = new ListViewItem(mw.Alias);
					listViewItem.ImageIndex = imageIndex;

					ListViewItem.ListViewSubItem subItemFilename = new ListViewItem.ListViewSubItem(listViewItem, mw.Filename);
					listViewItem.SubItems.Add(subItemFilename);

					//ListViewItem.ListViewSubItem subItemComment = new ListViewItem.ListViewSubItem(listViewItem, mw.Comment);
					//listViewItem.SubItems.Add(subItemComment);

					this.Items.Add(listViewItem);
				}
			}

			this.SmallImageList = imageList;
			this.LargeImageList = imageList;

			this.EndUpdate();
		}
	}
}
