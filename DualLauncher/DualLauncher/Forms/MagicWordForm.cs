#region copyright
// This file is part of Dual Monitor Tools which is a set of tools to assist
// users with multiple monitor setups.
// Copyright (C) 2010  Gerald Evans
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
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace DualLauncher
{
	public partial class MagicWordForm : Form
	{
		private MagicWord magicWord;

		public MagicWordForm(MagicWord magicWord)
		{
			this.magicWord = magicWord;
			InitializeComponent();
		}

		private void MagicWordForm_Load(object sender, EventArgs e)
		{
			// could use binding, but sometimes it's simpler to do it the old way!

			textBoxFilename.TextChanged += new EventHandler(textBoxFilename_TextChanged);

			textBoxAlias.Text = magicWord.Alias;
			textBoxFilename.Text = magicWord.Filename;
			textBoxParameters.Text = magicWord.Parameters;
			textBoxStartDirectory.Text = magicWord.StartDirectory;
			textBoxComment.Text = magicWord.Comment;
			textBoxWindowClass.Text = magicWord.WindowClass;
			textBoxCaption.Text = magicWord.CaptionRegExpr;

			this.windowPicker.InitControl(DualLauncher.Properties.Resources.TargetCursor,
				Properties.Resources.WinNoCrossHairs,
				Properties.Resources.WinCrossHairs);

			this.windowPicker.HoveredWindowChanged += new WindowPicker.HoverHandler(windowPicker_HoveredWindowChanged);

			this.startupPositionControl1.InitControl(magicWord.StartupPosition1);
			this.startupPositionControl2.InitControl(magicWord.StartupPosition2);
			this.startupPositionControl3.InitControl(magicWord.StartupPosition3);
			this.startupPositionControl4.InitControl(magicWord.StartupPosition4);
		}

		void textBoxFilename_TextChanged(object sender, EventArgs e)
		{
			ShowAliasIcon();
		}

		void windowPicker_HoveredWindowChanged(IntPtr hWnd)
		{
			StringBuilder sb = new StringBuilder(128);
			if (Win32.GetClassName(hWnd, sb, sb.Capacity) > 0)
			{
				this.textBoxWindowClass.Text = sb.ToString();
			}

			Win32.RECT rect;
			if (Win32.GetWindowRect(hWnd, out rect))
			{
				Rectangle rectangle = ScreenHelper.RectToRectangle(ref rect);
				this.startupPositionControl1.SetWindowRect(rectangle);
			}

			uint pid = 0;
			Win32.GetWindowThreadProcessId(hWnd, out pid);
			Process p = Process.GetProcessById((int)pid);
			this.textBoxFilename.Text = p.MainModule.FileName;
		}

		private void buttonBrowse_Click(object sender, EventArgs e)
		{
			OpenFileDialog dlg = new OpenFileDialog();
			dlg.Filter = Properties.Resources.BrowseExeFilter;
			string fileName = textBoxFilename.Text;
			dlg.FileName = fileName;
			if (fileName.Length > 0)
			{
				try
				{
					string dir = Path.GetDirectoryName(textBoxFilename.Text);
					dlg.InitialDirectory = dir;
					dlg.FileName = Path.GetFileName(textBoxFilename.Text);
				}
				catch (Exception)
				{
				}
			}
			if (dlg.ShowDialog() == DialogResult.OK)
			{
				// if this is directly put into the control then it gets lost
				textBoxFilename.Text = dlg.FileName;
			}
		}

		private void buttonDirBrowse_Click(object sender, EventArgs e)
		{
			FolderBrowserDialog dlg = new FolderBrowserDialog();
			dlg.SelectedPath = textBoxStartDirectory.Text;
			if (dlg.ShowDialog() == DialogResult.OK)
			{
				// if this is directly put into the control then it gets lost
				textBoxStartDirectory.Text = dlg.SelectedPath;
			}
		}

		private void ShowAliasIcon()
		{
			Icon fileIcon = null;
			string filename = textBoxFilename.Text;
			if (filename.Length > 0)
			{
				try
				{
					fileIcon = Icon.ExtractAssociatedIcon(filename);
				}
				catch (Exception)
				{
				}
			}
			if (fileIcon != null)
			{
				pictureBoxIcon.Image = fileIcon.ToBitmap();
			}
			else
			{
				pictureBoxIcon.Image = null;
			}
		}

		private void buttonOK_Click(object sender, EventArgs e)
		{
			bool isValid;

			StartupPosition position1 = new StartupPosition();
			isValid = this.startupPositionControl1.GetPosition(position1);

			StartupPosition position2 = new StartupPosition();
			isValid = this.startupPositionControl2.GetPosition(position2);

			StartupPosition position3 = new StartupPosition();
			isValid = this.startupPositionControl3.GetPosition(position3);

			StartupPosition position4 = new StartupPosition();
			isValid = this.startupPositionControl4.GetPosition(position4);

			if (!isValid)
			{
				this.DialogResult = DialogResult.None;
				MessageBox.Show("TODO");
				return;
			}

			magicWord.Alias = textBoxAlias.Text;
			magicWord.Filename = textBoxFilename.Text;
			magicWord.Parameters = textBoxParameters.Text;
			magicWord.StartDirectory = textBoxStartDirectory.Text;
			magicWord.Comment = textBoxComment.Text;
			magicWord.WindowClass = textBoxWindowClass.Text;
			magicWord.CaptionRegExpr = textBoxCaption.Text;

			magicWord.StartupPosition1 = position1;
			magicWord.StartupPosition2 = position2;
			magicWord.StartupPosition3 = position3;
			magicWord.StartupPosition4 = position4;
		}
	}
}