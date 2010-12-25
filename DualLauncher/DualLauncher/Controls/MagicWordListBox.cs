using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace DualLauncher
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
			this.View = Properties.Settings.Default.IconView;
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
