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
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace DualLauncher
{
	public partial class EntryForm : Form
	{
		private bool shutDown = false;

		private bool lastKeyWasDel = false;

		private const int ID_HOTKEY_ACTIVATE = 0x501;

		//private HotKey dualLauncherHotKey;
		private HotKeyController activateHotKeyController;
		public HotKeyController ActivateHotKeyController
		{
			get { return activateHotKeyController; }
		}


		public EntryForm()
		{
			InitializeComponent();

			InitHotKey();
			string filename = Program.MyDbFnm;
			try
			{
				MagicWords.Instance.Load(filename);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, Program.MyTitle);
			}
		}

		private void EntryForm_Load(object sender, EventArgs e)
		{
			// initially position ourselves at the center of the primay screen
			Rectangle screenRect = Screen.PrimaryScreen.Bounds;
			this.Location = new Point((screenRect.Width - Size.Width) / 2, (screenRect.Height - Size.Height) / 2);

			// need to be notified whenever the magic words change
			MagicWords.Instance.ListChanged += new ListChangedEventHandler(OnMagicWordsChanged);

			magicWordListBox.InitControl();

			SetAutoComplete();
			HideEntryForm();

			Input.TextChanged += new EventHandler(Input_TextChanged);

			timer.Interval = 500;
			timer.Start();
		}

		void Input_TextChanged(object sender, EventArgs e)
		{
			//ShowAliasIcon();
			string alias = Input.Text;
			//if (alias.Length > 0)
			{
				//MagicWord magicWord = MagicWords.Instance.FindByAlias(alias);
				//if (magicWord != null)
				{
					ShowAliasIcon();
				}
				//if (!lastKeyWasDel)
				{
					DoAutoComplete();
				}
			}
		}

		private void DoAutoComplete()
		{

			string alias = Input.Text;
			int maxMostUsedSize = Properties.Settings.Default.MaxMostUsedSize;
			List<MagicWord> autoCompleteWords = null;

			// if user doesn't want most used icons when no text
			// then don't bother sorting the list of magic words
			if (alias.Length > 0 || maxMostUsedSize > 0)
			{
				// get magic words that match the current alias prefix
				autoCompleteWords = MagicWords.Instance.GetAutoCompleteWords(alias);
				SortMagicWords(autoCompleteWords);

				// autocomplete the textbox
				if (autoCompleteWords.Count > 0)
				{
					if (alias.Length > 0 && !lastKeyWasDel)
					{
						string firstWord = autoCompleteWords[0].Alias;
						Input.Text = firstWord;
						Input.SelectionStart = alias.Length;
						Input.SelectionLength = firstWord.Length - alias.Length;
					}
				}

				// if no input, then truncate the list of magic words
				if (alias.Length == 0)
				{
					if (autoCompleteWords.Count > maxMostUsedSize)
					{
						autoCompleteWords.RemoveRange(maxMostUsedSize, autoCompleteWords.Count - maxMostUsedSize);
					}
				}
			}

			// update the icons displayed
			magicWordListBox.SetMagicWords(autoCompleteWords);
		}

		private void SortMagicWords(List<MagicWord> magicWords)
		{
			if (Properties.Settings.Default.UseMru)
			{
				// want mru first
				magicWords.Sort(delegate(MagicWord mw1, MagicWord mw2) { return mw2.LastUsed.CompareTo(mw1.LastUsed); });
			}
			else
			{
				// want most used first
				magicWords.Sort(delegate(MagicWord mw1, MagicWord mw2) { return mw2.UseCount.CompareTo(mw1.UseCount); });
			}
		}

		private void ShowAliasIcon()
		{
			Icon fileIcon = null;
			string alias = Input.Text;
			if (alias.Length > 0)
			{
				// first find magic word for alias 
				MagicWord magicWord = MagicWords.Instance.FindByAlias(alias);
				if (magicWord != null)
				{
					MagicWordExecutable executable = new MagicWordExecutable(magicWord);
					fileIcon = executable.Icon;
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

		private void OnMagicWordsChanged(object o, ListChangedEventArgs args)
		{
			SetAutoComplete();
		}

		private void SetAutoComplete()
		{
		}

		private void EntryForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			// don't shutdown if the form is just being closed 
			if (shutDown || e.CloseReason != CloseReason.UserClosing)
			{
				CleanUp();
			}
			else
			{
				// just hide the form and stop it from closing
				HideEntryForm();
				e.Cancel = true;
			}
		}

		private void CleanUp()
		{
			// make sure magic word list is saved if needed
			try
			{
				MagicWords.Instance.SaveIfDirty(Program.MyDbFnm);
			}
			catch (Exception)
			{
				// TODO: is it too late to do anything about this?
			}
			TermHotKey();
		}

		private void InitHotKey()
		{
			activateHotKeyController = new HotKeyController(this, ID_HOTKEY_ACTIVATE,
				"ActivateHotKey",
				Properties.Resources.ActivateDescription,
				"",		// no Windows 7 key
				new HotKey.HotKeyHandler(ShowEntryForm));
		}

		private void TermHotKey()
		{
			//dualLauncherHotKey.Dispose();
			activateHotKeyController.Dispose();
		}

		private void ShowEntryForm()
		{
			this.Visible = true;
			this.Activate();
			this.Input.Focus();
			DoAutoComplete();
		}

		private void HideEntryForm()
		{
			this.Visible = false;
		}


		private void notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			ShowEntryForm();
		}

		#region context menu handlers

		private void enterMagicWordToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ShowEntryForm();
		}

		private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			HideEntryForm();
			OptionsForm dlg = new OptionsForm(this);
			dlg.ShowDialog();
		}

		private void aboutDualLauncherToolStripMenuItem_Click(object sender, EventArgs e)
		{
			HideEntryForm();
			AboutForm dlg = new AboutForm();
			// TODO: why doesn't this appear to be modal?
			dlg.ShowDialog();
		}

		private void exitToolStripMenuItem_Click(object sender, EventArgs e)
		{
			shutDown = true;
			this.Close();
			Application.Exit();
		}
		#endregion

		private void Input_KeyDown(object sender, KeyEventArgs e)
		{
			ProcessKeyDown(sender, e);
		}

		private void magicWordListBox_KeyDown(object sender, KeyEventArgs e)
		{
			ProcessKeyDown(sender, e);
		}

		private void ProcessKeyDown(object sender, KeyEventArgs e)
		{
			lastKeyWasDel = false;

			Trace.WriteLine(string.Format("KeyCode: {0} KeyValue: {1} KeyData: {2}",
				e.KeyCode, e.KeyValue, e.KeyData));
			if (e.KeyCode == Keys.Enter)
			{
				ProcessInput(1);
			}
			else if (e.KeyCode == Keys.F1)
			{
				ProcessInput(1);
			}
			else if (e.KeyCode == Keys.F2)
			{
				ProcessInput(2);
			}
			else if (e.KeyCode == Keys.F3)
			{
				ProcessInput(3);
			}
			else if (e.KeyCode == Keys.F4)
			{
				ProcessInput(4);
			}
			else if (e.KeyCode == Keys.Escape)
			{
				HideEntryForm();
			}
			else
			{
				//DoAutoComplete();
				if (e.KeyCode == Keys.Back || e.KeyCode == Keys.Delete)
				{
					lastKeyWasDel = true;
				}
			}
		}

		private void ProcessInput(int position)
		{
			ProcessInput(Input.Text, position);
		}

		private void ProcessInput(string alias, int position)
		{
			MagicWord magicWord = MagicWords.Instance.FindByAlias(alias);

			if (magicWord != null)
			{
				StartMagicWord(magicWord, position);
			}
		}

		private void StartMagicWord(MagicWord magicWord, int position)
		{
			if (magicWord != null)
			{
				magicWord.UseCount++;
				magicWord.LastUsed = DateTime.Now;
				StartupPosition startPosition;
				if (position == 2)
				{
					startPosition = magicWord.StartupPosition2;
				}
				else if (position == 3)
				{
					startPosition = magicWord.StartupPosition3;
				}
				else if (position == 4)
				{
					startPosition = magicWord.StartupPosition4;
				}
				else
				{
					startPosition = magicWord.StartupPosition1;
				}
				StartupController.Instance.Launch(magicWord, startPosition);
				Input.Text = "";
				HideEntryForm();
			}
		}

		private void timer_Tick(object sender, EventArgs e)
		{
			StartupController.Instance.Poll();
		}

		private void magicWordListBox_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
		{
			ListViewItem listViewItem = e.Item;
			if (listViewItem != null)
			{
				string alias = listViewItem.Text;
				//Input.Text = alias;
			}
		}


		private void magicWordListBox_DoubleClick(object sender, EventArgs e)
		{
		}

		private void magicWordListBox_MouseClick(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				System.Windows.Forms.ListView.SelectedListViewItemCollection selectedItems = magicWordListBox.SelectedItems;
				if (selectedItems.Count == 1)
				{
					string alias = selectedItems[0].Text;
					ProcessInput(alias, 1);
				}
			}

		}

		private void EntryForm_Deactivate(object sender, EventArgs e)
		{
			HideEntryForm();
		}

	}
}