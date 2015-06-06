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

using DMT.Library;
using DMT.Library.Command;
using DMT.Library.GuiUtils;
using DMT.Library.PInvoke;
using DMT.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DMT.Modules.Launcher
{
	partial class MagicWordForm : Form
	{
		LauncherModule _launcherModule;
		MagicWord _magicWord;
		//ICommandRunner _commandRunner;
		bool _statsReset = false;
		int _timesUsed;
		DateTime _lastUsed;

		public MagicWordForm(LauncherModule launcherModule, MagicWord magicWord)
		{
			_launcherModule = launcherModule;
			_magicWord = magicWord;
			//_commandRunner = commandRunner;
			InitializeComponent();
		}

		private void MagicWordForm_Load(object sender, EventArgs e)
		{
			// could use binding, but sometimes it's simpler to do it the old way!

			textBoxFilename.TextChanged += new EventHandler(textBoxFilename_TextChanged);

			textBoxAlias.Text = _magicWord.Alias;
			textBoxFilename.Text = _magicWord.Filename;
			textBoxParameters.Text = _magicWord.Parameters;
			textBoxStartDirectory.Text = _magicWord.StartDirectory;
			textBoxComment.Text = _magicWord.Comment;
			textBoxWindowClass.Text = _magicWord.WindowClass;
			textBoxCaption.Text = _magicWord.CaptionRegExpr;

			this.windowPicker.InitControl(Properties.Resources.TargetCursor,
				Properties.Resources.WinNoCrossHairs,
				Properties.Resources.WinCrossHairs);

			this.windowPicker.HoveredWindowChanged += new WindowPicker.HoverHandler(windowPicker_HoveredWindowChanged);

			this.startupPositionControl1.InitControl(_magicWord.StartupPosition1);
			this.startupPositionControl2.InitControl(_magicWord.StartupPosition2);
			this.startupPositionControl3.InitControl(_magicWord.StartupPosition3);
			this.startupPositionControl4.InitControl(_magicWord.StartupPosition4);

			_lastUsed = _magicWord.LastUsed;
			_timesUsed = _magicWord.UseCount;
			ShowStats();
		}

		void textBoxFilename_TextChanged(object sender, EventArgs e)
		{
			ShowAliasIcon();
		}

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
		/// <summary>
		/// Fills the MagicWord with details extracted from the given window
		/// </summary>
		/// <param name="hWnd"></param>
		/// <param name="magicWord"></param>
		/// <returns></returns>
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
			dlg.Filter = LauncherStrings.BrowseExeFilter;
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
					if (_launcherModule.CommandRunner.IsInternalCommand(filename))
					{
						fileIcon = _launcherModule.CommandRunner.GetInternalCommandIcon(filename);
					}
					else
					{
						fileIcon = Icon.ExtractAssociatedIcon(filename);
					}
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
				_launcherModule.Launch(testMagicWord, startPosition, map);
			}
		}


		private void buttonInternalCommand_Click(object sender, EventArgs e)
		{
			InternalCommandForm dlg = new InternalCommandForm(_launcherModule.CommandRunner, textBoxFilename.Text);
			if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				textBoxFilename.Text = dlg.SelectedCommand;

				if (textBoxAlias.Text.Length == 0)
				{
					// if user has not entered an alias, use the action name
					string moduleName;
					string actionName;
					MagicCommand.SplitMagicCommand(dlg.SelectedCommand, out moduleName, out actionName);
					textBoxAlias.Text = actionName;
				}
			}
		}


		//private void textBoxAlias_Validating(object sender, CancelEventArgs e)
		//{
		//    if (textBoxAlias.Text.Length == 0)
		//    {
		//        MessageBox.Show(Properties.Resources.NoAlias, Program.MyTitle);
		//        e.Cancel = true;
		//    }
		//}

		//private void textBoxFilename_Validating(object sender, CancelEventArgs e)
		//{
		//    if (textBoxFilename.Text.Length == 0)
		//    {
		//        MessageBox.Show(Properties.Resources.NoFilename, Program.MyTitle);
		//        e.Cancel = true;
		//    }
		//}

		private void buttonOK_Click(object sender, EventArgs e)
		{
			bool isValid = FillMagicWord(this._magicWord);

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
				MessageBox.Show(LauncherStrings.InvalidCoOrd, CommonStrings.MyTitle);
				return false;
			}
			if (textBoxAlias.Text.Length == 0)
			{
				this.DialogResult = DialogResult.None;
				textBoxAlias.Focus();
				MessageBox.Show(LauncherStrings.NoAlias, CommonStrings.MyTitle);
				return false;
			}
			if (textBoxFilename.Text.Length == 0)
			{
				this.DialogResult = DialogResult.None;
				textBoxFilename.Focus();
				MessageBox.Show(LauncherStrings.NoFilename, CommonStrings.MyTitle);
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
			if (_statsReset)
			{
				changedMagicWord.LastUsed = _lastUsed;
				changedMagicWord.UseCount = _timesUsed;
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
			_lastUsed = new DateTime();
			_statsReset = true;
			ShowStats();
		}

		private void buttonResetTimesUsed_Click(object sender, EventArgs e)
		{
			_timesUsed = 0;
			_statsReset = true;
			ShowStats();
		}

		private void ShowStats()
		{
			if (_lastUsed != DateTime.MinValue)
			{
				labelLastUsed.Text = _lastUsed.ToString();
			}
			else
			{
				labelLastUsed.Text = "";
			}
			labelTimesUsed.Text = _timesUsed.ToString();
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