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

		private bool statsReset = false;
		private int timesUsed;
		private DateTime lastUsed;

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

			lastUsed = magicWord.LastUsed;
			timesUsed = magicWord.UseCount;
			ShowStats();
			//string lastUsed = "";
			//labelLastUsed.Text = magicWord.LastUsed.ToString();
			//labelTimesUsed.Text = magicWord.UseCount.ToString();
		}

		void textBoxFilename_TextChanged(object sender, EventArgs e)
		{
			ShowAliasIcon();
		}

		//void windowPicker_HoveredWindowChanged(IntPtr hWnd)
		//{
		//    StringBuilder sb = new StringBuilder(128);
		//    if (Win32.GetClassName(hWnd, sb, sb.Capacity) > 0)
		//    {
		//        this.textBoxWindowClass.Text = sb.ToString();
		//    }

		//    Win32.RECT rect;
		//    if (Win32.GetWindowRect(hWnd, out rect))
		//    {
		//        Rectangle rectangle = ScreenHelper.RectToRectangle(ref rect);
		//        this.startupPositionControl1.SetWindowRect(rectangle);
		//    }

		//    uint pid = 0;
		//    Win32.GetWindowThreadProcessId(hWnd, out pid);
		//    Process p = Process.GetProcessById((int)pid);
		//    this.textBoxFilename.Text = p.MainModule.FileName;

		//    // use the name without path or extension as the default for the alias
		//    this.textBoxAlias.Text = Path.GetFileNameWithoutExtension(p.MainModule.FileName);
		//}

		void windowPicker_HoveredWindowChanged(IntPtr hWnd)
		{
			MagicWord tempMagicWord = new MagicWord();
			if (GetWindowDetails(hWnd, tempMagicWord))
			{
				textBoxWindowClass.Text = tempMagicWord.WindowClass;
				if (tempMagicWord.StartupPosition1 != null)
				{
					startupPositionControl1.SetWindowRect(tempMagicWord.StartupPosition1.Position);
				}
				textBoxFilename.Text = tempMagicWord.Filename;
				textBoxAlias.Text = tempMagicWord.Alias;
			}
		}

		// TODO: probably not the best place for this function to live
		public static bool GetWindowDetails(IntPtr hWnd, MagicWord magicWord)
		{
			bool ret = true;

			StringBuilder sb = new StringBuilder(128);
			if (Win32.GetClassName(hWnd, sb, sb.Capacity) > 0)
			{
				magicWord.WindowClass = sb.ToString();
			}

			Win32.RECT rect;
			if (Win32.GetWindowRect(hWnd, out rect))
			{
				Rectangle rectangle = ScreenHelper.RectToRectangle(ref rect);
				if (magicWord.StartupPosition1 == null)
				{
					magicWord.StartupPosition1 = new StartupPosition();
				}
				magicWord.StartupPosition1.Position = rectangle;
			}

			uint pid = 0;
			if (Win32.GetWindowThreadProcessId(hWnd, out pid) != 0)
			{
				try
				{
					Process p = Process.GetProcessById((int)pid);
					magicWord.Filename = p.MainModule.FileName;

					// use the name without path or extension as the default for the alias
					magicWord.Alias = Path.GetFileNameWithoutExtension(p.MainModule.FileName);
				}
				catch (Exception)
				{
					// the process could have just died
				}
			}

			return ret;
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

		private void buttonTest_Click(object sender, EventArgs e)
		{
			MagicWord testMagicWord = new MagicWord();
			if (FillMagicWord(testMagicWord))
			{
				// magic word is valid

				// check which tab is currently selected to determine corresponding position
				int positionIndex1 = this.tabControl.SelectedIndex + 1;
				StartupPosition startPosition = testMagicWord.GetStartupPosition(positionIndex1);
				// and try and run it
				ParameterMap map = new ParameterMap();
				bool ok = StartupController.Instance.Launch(testMagicWord, startPosition, map);
			}
		}

		private void buttonOK_Click(object sender, EventArgs e)
		{
			bool isValid = FillMagicWord(this.magicWord);

			if (!isValid)
			{
				this.DialogResult = DialogResult.None;
			}
		}

		private bool FillMagicWord(MagicWord changedMagicWord)
		{
			bool isValid = true;

			StartupPosition position1 = new StartupPosition();
			StartupPosition position2 = new StartupPosition();
			StartupPosition position3 = new StartupPosition();
			StartupPosition position4 = new StartupPosition();

			GetPosition(0, startupPositionControl1, position1, ref isValid);
			GetPosition(1, startupPositionControl2, position2, ref isValid);
			GetPosition(2, startupPositionControl3, position3, ref isValid);
			GetPosition(3, startupPositionControl4, position4, ref isValid);

			if (!isValid)
			{
				this.DialogResult = DialogResult.None;
				MessageBox.Show(Properties.Resources.InvalidCoOrd, Program.MyTitle);
				return false;
			}

			changedMagicWord.Alias = textBoxAlias.Text;
			changedMagicWord.Filename = textBoxFilename.Text;
			changedMagicWord.Parameters = textBoxParameters.Text;
			changedMagicWord.StartDirectory = textBoxStartDirectory.Text;
			changedMagicWord.Comment = textBoxComment.Text;
			changedMagicWord.WindowClass = textBoxWindowClass.Text;
			changedMagicWord.CaptionRegExpr = textBoxCaption.Text;

			changedMagicWord.StartupPosition1 = position1;
			changedMagicWord.StartupPosition2 = position2;
			changedMagicWord.StartupPosition3 = position3;
			changedMagicWord.StartupPosition4 = position4;

			// TODO: stats could have changed since the dialog opened
			if (statsReset)
			{
				changedMagicWord.LastUsed = lastUsed;
				changedMagicWord.UseCount = timesUsed;
			}


			return true;
		}

		private void GetPosition(int tabIndex, StartupPositionControl startupPositionControl, StartupPosition position, ref bool isValid)
		{
			if (isValid)
			{
				isValid = startupPositionControl.GetPosition(position);
				if (!isValid)
				{
					// make sure this tab is selected so user can see error
					// TODO
					this.tabControl.SelectedIndex = tabIndex;
				}
			}
		}

		private void buttonResetLastUsed_Click(object sender, EventArgs e)
		{
			lastUsed = new DateTime();
			statsReset = true;
			ShowStats();
		}

		private void buttonResetTimesUsed_Click(object sender, EventArgs e)
		{
			timesUsed = 0;
			statsReset = true;
			ShowStats();
		}

		private void ShowStats()
		{
			if (lastUsed != DateTime.MinValue)
			{
				labelLastUsed.Text = lastUsed.ToString();
			}
			else
			{
				labelLastUsed.Text = "";
			}
			labelTimesUsed.Text = timesUsed.ToString();
		}

		#region Drag and Drop
		private void MagicWordForm_DragEnter(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent(DataFormats.FileDrop))
			{
				e.Effect = DragDropEffects.Copy;
			}
			else
			{
				e.Effect = DragDropEffects.None;
			}
		}

		private void MagicWordForm_DragDrop(object sender, DragEventArgs e)
		{
			string[] filenames = e.Data.GetData(DataFormats.FileDrop) as string[];
			if (filenames != null)
			{
				if (filenames.Length > 0)
				{
					textBoxFilename.Text = filenames[0];
					textBoxAlias.Text = Path.GetFileNameWithoutExtension(filenames[0]);
				}
			}
		}
		#endregion

	}
}