#region copyright
// This file is part of Dual Monitor Tools which is a set of tools to assist
// users with multiple monitor setups.
// Copyright (C) 2009  Gerald Evans
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
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;
using System.Windows.Forms;
using DualSnap.Properties;


namespace DualSnap
{
	/// <summary>
	/// The main form of the application that when visible shows the current
	/// snapshot on the secondary monitor.
	/// It also contains the logic for handling the hotkey and context menu.
	/// </summary>
	public partial class SnapForm : Form
	{
		private bool shutDown = false;

		private const int ID_HOTKEY_DUALSNAP = 0x102;

		private HotKey dualSnapHotKey;

		private SnapHistory snapHistory = new SnapHistory(Properties.Settings.Default.MaxSnaps);

		/// <summary>
		/// Initialises the form, hot key and the context menu.
		/// </summary>
		public SnapForm()
		{
			InitializeComponent();

			this.Text = Program.MyTitle;

			InitHotKey();
			InitContextMenu();
		}

		private void InitHotKey()
		{
			KeyCombo defaultKeyCombo = new KeyCombo();
			defaultKeyCombo.FromPropertyValue(Properties.Settings.Default.HotKeyValue);

			dualSnapHotKey = new HotKey(this, ID_HOTKEY_DUALSNAP);
			dualSnapHotKey.RegisterHotKey(defaultKeyCombo);

			dualSnapHotKey.HotKeyPressed += new HotKey.HotKeyHandler(TakeSnap);
		}

		private void TermHotKey()
		{
			dualSnapHotKey.Dispose();
		}

		private void InitContextMenu()
		{
			snapsToolStripMenuItem.DropDown.AutoSize = false;
			snapsToolStripMenuItem.DropDown.Width = 128;
		}

		private void notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			TakeSnap();
		}

		private void TakeSnap()
		{
			Rectangle r = Screen.PrimaryScreen.Bounds;

			Bitmap primaryScreenImage = new Bitmap(r.Width, r.Height);
			using (Graphics g = Graphics.FromImage(primaryScreenImage))
			{
				g.CopyFromScreen(r.Location, new Point(0, 0), r.Size, CopyPixelOperation.SourceCopy);
			}
			pictureBox.Image = primaryScreenImage;

			Snap snap = new Snap(primaryScreenImage);
			snapHistory.Add(snap);

			if (Properties.Settings.Default.AutoShowSnap)
			{
				ShowLastSnap();
			}
		}

		private void ShowLastSnap()
		{
			// position window on second screen
			Screen secondaryScreen = ScreenHelper.NextScreen(Screen.PrimaryScreen);
			this.Location = secondaryScreen.Bounds.Location;
			this.Size = secondaryScreen.Bounds.Size;
			// we also maximize it, so if moved by SwapScreen it will still occupy the whole screen 
			// even if the monitor is a different size
			this.WindowState = FormWindowState.Maximized;
			this.TopMost = true;
			this.Visible = true;
			this.showLastSnapToolStripMenuItem.Checked = true;
		}

		private void HideLastSnap()
		{
			this.Visible = false;
			this.showLastSnapToolStripMenuItem.Checked = false;
		}

		private void SnapForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			// don't shutdown if the form is just being closed 
			if (shutDown || e.CloseReason != CloseReason.UserClosing)
			{
				TermHotKey();
			}
			else
			{
				// just hide the form and stop it from closing
				HideLastSnap();
				e.Cancel = true;
			}
		}

		#region context menu handlers
		private void contextMenuStrip_Opening(object sender, CancelEventArgs e)
		{
			// when the context menu opens we
			// enable/disable menu items as appropriate
			bool hasSnaps = snapHistory.Count > 0;

			showLastSnapToolStripMenuItem.Enabled = hasSnaps;
			snapsToolStripMenuItem.Enabled = hasSnaps;
			copyToolStripMenuItem.Enabled = hasSnaps;
			saveAsToolStripMenuItem.Enabled = hasSnaps;
		}

		// handle the "Snap" menu item click
		private void snapToolStripMenuItem_Click(object sender, EventArgs e)
		{
			TakeSnap();
		}

		// handle the "Show Snap" menu item click
		private void showSnapToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (this.Visible)
			{
				HideLastSnap();
			}
			else
			{
				ShowLastSnap();
			}
		}

		// This is called when the user attempts to open up the "Snaps" sub-menu
		// We dynamically build the Snaps menu here using snapHistory
		private void snapsToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
		{
			// first clear any existing menu
			snapsToolStripMenuItem.DropDownItems.Clear();

			ToolStripDropDown dropDown = snapsToolStripMenuItem.DropDown;
			dropDown.Items.Clear();

			// we'll calculate the size of the drop down ourself
			dropDown.AutoSize = false;
			dropDown.Height = 0;

			// now add each item from the history
			foreach (Snap snap in snapHistory)
			{
				SnapMenuItem snapMenuItem = new SnapMenuItem(snap);
				snapMenuItem.ToolTipText = Resources.SnapMenuItemTooltip;
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
			ShowLastSnap();
		}

		// Handle the "Copy" menu item click
		private void copyToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (pictureBox.Image != null)
			{
				// copy current snap to clipboard
				Clipboard.SetImage(pictureBox.Image);
			}
		}

		// Handle the "Save As" menu item click
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
						MessageBox.Show(Program.MyTitle, ex.Message);
					}
				}
			}
		}

		// Handle the "Options" menu item click
		private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			OptionsForm dlg = new OptionsForm();
			dlg.DualSnapHotKey = dualSnapHotKey.HotKeyCombo;
			dlg.AutoShowSnap = Properties.Settings.Default.AutoShowSnap;
			dlg.MaxSnaps = snapHistory.MaxSnaps;
			if (dlg.ShowDialog() == DialogResult.OK)
			{
				// use the new hotkey 
				dualSnapHotKey.RegisterHotKey(dlg.DualSnapHotKey);

				// and the maximum number of snaps we remember
				snapHistory.MaxSnaps = dlg.MaxSnaps;

				// save these settings for the next time we run
				Properties.Settings.Default.HotKeyValue = dlg.DualSnapHotKey.ToPropertyValue();
				Properties.Settings.Default.AutoShowSnap = dlg.AutoShowSnap;
				Properties.Settings.Default.MaxSnaps = dlg.MaxSnaps;
				Properties.Settings.Default.Save();
			}
		}

		// Handle the "About Dual Snap" menu item click
		private void aboutDualSnapToolStripMenuItem_Click(object sender, EventArgs e)
		{
			AboutForm dlg = new AboutForm();
			// TODO: why doesn't this appear to be modal?
			dlg.ShowDialog();
		}

		private void visitDualSnapWebsiteToolStripMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				System.Diagnostics.Process.Start("http://dualmonitortool.sourceforge.net/dualsnap.html");
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		// Handle the "Exit" menu item click
		private void exitToolStripMenuItem_Click(object sender, EventArgs e)
		{
			shutDown = true;
			this.Close();
			Application.Exit();
		}
		#endregion
	}
}