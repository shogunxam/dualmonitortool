using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace DualLauncher
{
	public class MagicWordListBox : ListView
	{
		//private List<MagicWord> magicWords;

		public void InitControl()
		{
			//this.SmallImageList.Images = new ImageList();
			//this.View = View.LargeIcon;
			//this.View = View.List;
			//this.View = View.Details;
			this.View = Properties.Settings.Default.IconView;
		}

		public void SetMagicWords(List<MagicWord> magicWords)
		{
			//this.magicWords = magicWords;

			this.BeginUpdate();
			this.Items.Clear();

			//this.SmallImageList.Images.Clear();
			ImageList imageList = new ImageList();
			imageList.ImageSize = new Size(32, 32);
			imageList.ColorDepth = ColorDepth.Depth32Bit;

			// first icon is used the generic icon we use when we can't find the
			// applications icon
			imageList.Images.Add(Properties.Resources.missingIcon);

			if (magicWords != null)
			{
				foreach (MagicWord mw in magicWords)
				{
					MagicWordExecutable executable = new MagicWordExecutable(mw);
					int imageIndex = 0;	// the default (missing icon)
					Icon fileIcon = executable.Icon;
					if (fileIcon != null)
					{
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
