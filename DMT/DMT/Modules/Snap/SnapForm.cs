using DMT.Library.Transform;
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
using System.Windows;
using System.Windows.Forms;

namespace DMT.Modules.Snap
{
	partial class SnapForm : Form
	{
		SnapModule _snapModule;
		bool _terminate = false;
		bool _expandSnap;
		bool _shrinkSnap;
		bool _maintainAspectRatio;
		Point _lastMousePosn;	// used for detecting drag movements on the snap


		public SnapForm(SnapModule snapModule)
		{
			_snapModule = snapModule;

			// copy initial settings from the module settings
			_expandSnap = _snapModule.ExpandSnap;
			_shrinkSnap = _snapModule.ShrinkSnap;
			_maintainAspectRatio = _snapModule.MaintainAspectRatio;

			InitializeComponent();

			InitContextMenu();

			UpdateScaleMenuItems();
		}

		public void Terminate()
		{
			_terminate = true;
			Close();
		}

		public void ShowImage(Image image)
		{
			pictureBox.Image = image;

			//pictureBox.Location = new Point(0, 0);
			//pictureBox.Size = image.Size;
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

			ReScalePictureBox();
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
			_snapModule.TakePrimaryScreenSnap();
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

		private void expandToFillScreenToolStripMenuItem_Click(object sender, EventArgs e)
		{
			_expandSnap = expandToFillScreenToolStripMenuItem.Checked;
			ReScalePictureBox();
		}

		private void shrinkToFitScreenToolStripMenuItem_Click(object sender, EventArgs e)
		{
			_shrinkSnap = shrinkToFitScreenToolStripMenuItem.Checked;
			ReScalePictureBox();
		}

		private void maintainAspectRatioToolStripMenuItem_Click(object sender, EventArgs e)
		{
			_maintainAspectRatio = maintainAspectRatioToolStripMenuItem.Checked;
			ReScalePictureBox();
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

		// make sure correct scaling menu items are checked
		void UpdateScaleMenuItems()
		{
			expandToFillScreenToolStripMenuItem.Checked = _expandSnap;
			shrinkToFitScreenToolStripMenuItem.Checked = _shrinkSnap;
			maintainAspectRatioToolStripMenuItem.Checked = _maintainAspectRatio;
		}

		void ReScalePictureBox()
		{
			// The default is for the picture box size to be the same as the source image
			// so it would get displayed without any scaling
			Size targetSize = pictureBox.Image.Size;

			Size windowSize = this.Size;

			// 
			if (_expandSnap)
			{
				if (_maintainAspectRatio)
				{
					if (targetSize.Width < windowSize.Width && targetSize.Height < windowSize.Height)
					{
						// increase size while still maintaining aspect ratio
						// to maximum the image size
						targetSize = ScaleHelper.UnderScale(targetSize, windowSize);
					}
				}
				else
				{
					if (targetSize.Width < windowSize.Width)
					{
						targetSize.Width = windowSize.Width;
					}
					if (targetSize.Height < windowSize.Height)
					{
						targetSize.Height = windowSize.Height;
					}
				}
			}

			//
			if (_shrinkSnap)
			{
				if (_maintainAspectRatio)
				{
					if (targetSize.Width > windowSize.Width || targetSize.Height > windowSize.Height)
					{
						// need to decrease size while still maintaining aspect ratio
						// so that all of the image will be visible
						targetSize = ScaleHelper.UnderScale(targetSize, windowSize);
					}
				}
				else
				{
					if (targetSize.Width > windowSize.Width)
					{
						targetSize.Width = windowSize.Width;
					}
					if (targetSize.Height > windowSize.Height)
					{
						targetSize.Height = windowSize.Height;
					}
				}

			}

			pictureBox.Location = new Point(0, 0);
			pictureBox.Size = targetSize;

			if (CanScrollSnap())
			{
				pictureBox.Cursor = Cursors.NoMove2D;
			}
			else
			{
				pictureBox.Cursor = Cursors.Default;
			}
		}

		private void pictureBox_MouseDown(object sender, MouseEventArgs e)
		{
			base.OnMouseDown(e);

			_lastMousePosn = pictureBox.PointToScreen(e.Location);
			// no need/advantage to capture mouse?
		}

		private void pictureBox_MouseMove(object sender, MouseEventArgs e)
		{
			base.OnMouseMove(e);

			if (e.Button == MouseButtons.Left)
			{
				if (CanScrollSnap())
				{
					// calculate delta movement
					Point newMousePosn = pictureBox.PointToScreen(e.Location);
					int deltaX = newMousePosn.X - _lastMousePosn.X;
					int deltaY = newMousePosn.Y - _lastMousePosn.Y;
					_lastMousePosn = newMousePosn;

					int newX = pictureBox.Location.X;
					int newY = pictureBox.Location.Y;
					// move the origin of the image (wrt the window)
					newX += deltaX;
					newY += deltaY;

					// make sure the new origin is within bounds
					if (newX > 0)
					{
						newX = 0;
					}
					else if (newX < this.Size.Width - pictureBox.Width)
					{
						newX = this.Size.Width - pictureBox.Width;
					}
					if (newY > 0)
					{
						newY = 0;
					}
					else if (newY < this.Size.Height - pictureBox.Height)
					{
						newY = this.Size.Height - pictureBox.Height;
					}

					pictureBox.Location = new Point(newX, newY);
				}
			}
		}

		bool CanScrollSnap()
		{
			Size windowSize = this.Size;
			Size picBoxSize = pictureBox.Size;

			return (picBoxSize.Width > windowSize.Width || picBoxSize.Height > windowSize.Height);
		}

	}
}
