using DMT.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DMT.Modules.Snap
{
	partial class SnapForm : Form
	{
		SnapModule _snapModule;
		bool _terminate = false;

		public SnapForm(SnapModule snapModule)
		{
			_snapModule = snapModule;

			InitializeComponent();

			InitContextMenu();
		}

		public void Terminate()
		{
			_terminate = true;
			Close();
		}

		public void ShowImage(Image image)
		{
			pictureBox.Image = image;
		}

		public void ShowAt(Rectangle rectangle)
		{
			this.WindowState = FormWindowState.Normal;	// necessary, or Location won't work
			this.StartPosition = FormStartPosition.Manual; // if we don't do this, on first display Windows will decide
			this.Location = rectangle.Location;
			this.Size = rectangle.Size;
			// we also maximize it, so if moved by SwapScreen it will still occupy the whole screen 
			// even if the monitor is a different size
			this.WindowState = FormWindowState.Maximized;
			this.TopMost = true;
			this.Visible = true;
			this.showSnapToolStripMenuItem.Checked = true;
		}

		public void HideSnap()
		{
			this.Visible = false;
			this.showSnapToolStripMenuItem.Checked = false;
		}

		private void SnapForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			// don't shutdown if the form is just being closed 
			if (!_terminate)
			{
				// just hide the form and stop it from closing
				HideSnap();
				e.Cancel = true;
			}
		}

		private void contextMenuStrip_Opening(object sender, CancelEventArgs e)
		{
			// when the context menu opens we
			// enable/disable menu items as appropriate
			bool hasSnaps = _snapModule.SnapHistory.Count > 0;

			showSnapToolStripMenuItem.Enabled = hasSnaps;
			snapsToolStripMenuItem.Enabled = hasSnaps;
			copyToolStripMenuItem.Enabled = hasSnaps;
			saveAsToolStripMenuItem.Enabled = hasSnaps;
			deleteCurrentSnapToolStripMenuItem.Enabled = hasSnaps;
			deleteAllSnapsToolStripMenuItem.Enabled = hasSnaps;
		}

		private void snapToolStripMenuItem_Click(object sender, EventArgs e)
		{
			_snapModule.TakeSnap();
		}

		private void showSnapToolStripMenuItem_Click(object sender, EventArgs e)
		{
			_snapModule.ToggleShowSnap();
		}

		private void snapsToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
		{
			// first clear any existing menu
			snapsToolStripMenuItem.DropDownItems.Clear();

			ToolStripDropDown dropDown = snapsToolStripMenuItem.DropDown;
			dropDown.Items.Clear();

			// we'll calculate the size of the drop down ourself
			dropDown.AutoSize = false;
			// TODO: we need a minimum height for the border?
			// If this is zero, and there is one item in the menu,
			// then .NET will display a down arrow which if clicked
			// will result in an exception.
			dropDown.Height = 4;	// TODO: 

			// now add each item from the history
			foreach (Snap snap in _snapModule.SnapHistory)
			{
				SnapMenuItem snapMenuItem = new SnapMenuItem(snap);
				snapMenuItem.ToolTipText = SnapStrings.SnapMenuItemTooltip;
				// insert items at begining, so topmost displayed item is latest snap
				//snapsToolStripMenuItem.DropDownItems.Add(snapMenuItem);
				dropDown.Items.Insert(0, snapMenuItem);
				dropDown.Width = snapMenuItem.Width;	// All items are same width
				dropDown.Height += snapMenuItem.Height;
				snapMenuItem.Click += new EventHandler(snapMenuItem_Click);
			}
		}

		// handles the "Snaps" sub-menu items click
		private void snapMenuItem_Click(object sender, EventArgs e)
		{
			SnapMenuItem snapMenuItem = sender as SnapMenuItem;
			Snap snap = snapMenuItem.Snap;

			pictureBox.Image = snap.Image;
			_snapModule.ShowLastSnap();
		}

		private void copyToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (pictureBox.Image != null)
			{
				// copy current snap to clipboard
				Clipboard.SetImage(pictureBox.Image);
			}
		}

		private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (pictureBox.Image != null)
			{
				// request where to save file
				SaveFileDialog dlg = new SaveFileDialog();
				// TODO: allow other file formats?
				dlg.Filter = "png files (*.png)|*.png";
				if (dlg.ShowDialog() == DialogResult.OK)
				{
					try
					{
						pictureBox.Image.Save(dlg.FileName, ImageFormat.Png);
					}
					catch (Exception ex)
					{
						MessageBox.Show(ex.Message, CommonStrings.MyTitle);
					}
				}
			}
		}

		private void deleteCurrentSnapToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (_snapModule.SnapHistory.Count > 0)
			{
				// we do not know our position in the list,
				// but we know the current image as we are displaying it
				if (pictureBox.Image != null)
				{
					if (_snapModule.SnapHistory.Delete(pictureBox.Image))
					{
						// we have deleted this snap
						if (_snapModule.SnapHistory.Count > 0)
						{
							pictureBox.Image = _snapModule.SnapHistory.LastSnap().Image;
						}
						else
						{
							// we have deleted the last snap
							pictureBox.Image = null;
							HideSnap();
						}
					}
				}
			}
		}

		private void deleteAllSnapsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (_snapModule.SnapHistory.Count > 0)
			{
				// if deleting multiple snaps, get confirmation first
				if (_snapModule.SnapHistory.Count > 1)
				{
					string msg = string.Format(SnapStrings.DeleteConfirm, _snapModule.SnapHistory.Count);
					if (MessageBox.Show(msg,
						CommonStrings.MyTitle,
						MessageBoxButtons.YesNo,
						MessageBoxIcon.Question,
						MessageBoxDefaultButton.Button2) != DialogResult.Yes)
					{
						// no, user doesn't want to go ahead
						return;
					}
				}

				// delete all snaps
				_snapModule.SnapHistory.DeleteAll();

				// make sure the current image isn't referenced, so it can be freed
				pictureBox.Image = null;

				// we could ask the garbage collection to run here,
				// but it is usually advised to let the GC run when it thinks is best
				//GC.Collect();

				// no point in having the snap window visible
				HideSnap();
			}
		}

		void InitContextMenu()
		{
			snapsToolStripMenuItem.DropDown.AutoSize = false;
			snapsToolStripMenuItem.DropDown.Width = 128;
		}

	}
}
