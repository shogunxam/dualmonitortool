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
	/// <summary>
	/// Main form for Dual Launcher.
	/// This is the form that popups when the hotkey is pressed
	/// </summary>
	public partial class EntryForm : Form
	{
		private bool shutDown = false;
		private bool loaded = false;	// indicates if form has been loaded

		private const int ID_HOTKEY_ACTIVATE = 0x501;
		private const int ID_HOTKEY_ADD_MAGIC_WORD = 0x502;

		private HotKeyController activateHotKeyController;
		/// <summary>
		/// The hotkey to activate the entry form
		/// </summary>
		public HotKeyController ActivateHotKeyController
		{
			get { return activateHotKeyController; }
		}

		private HotKeyController addMagicWordHotKeyController;
		/// <summary>
		/// The hotkey which will popup the 'Add MagicWord' form
		/// prefilled with information taken from the active application.
		/// </summary>
		public HotKeyController AddMagicWordHotKeyController
		{
			get { return addMagicWordHotKeyController; }
		}

		public EntryForm()
		{
			InitializeComponent();

			InitHotKeys();
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
			SetStartupPosition();

			// need to be notified whenever the magic words change
			MagicWords.Instance.ListChanged += new ListChangedEventHandler(OnMagicWordsChanged);

			magicWordListBox.InitControl();

			SetAutoComplete();
			HideEntryForm();

			Input.TextChanged += new EventHandler(Input_TextChanged);

			timer.Interval = 500;
			timer.Start();

			// indicate form has been loaded, so we know this function has been called
			// this is currently used to know whenthe form position has been set
			// as we don't want to save it on exit unless it has been set
			loaded = true;
		}

		// This is used to restore the entry forms position to its last known position
		private void SetStartupPosition()
		{
			Rectangle rectangle = Properties.Settings.Default.LastPosition;
			if (IsSensibleStartupPosition(ref rectangle))
			{
				this.Location = rectangle.Location;
				this.Size = rectangle.Size;
			}
			else
			{
				// just position ourselves at the center of the primary screen
				Rectangle screenRect = Screen.PrimaryScreen.Bounds;
				this.Location = new Point((screenRect.Width - Size.Width) / 2, (screenRect.Height - Size.Height) / 2);
			}
		}

		private bool IsSensibleStartupPosition(ref Rectangle rectangle)
		{
			if (rectangle.Width <= 0 || rectangle.Height <= 0)
			{
				return false;
			}

			//// TODO: this is a bit over zealous as it doesn't allow the window to be a fractionaly
			//// outside of the working area or split over screens
			//foreach (Screen screen in Screen.AllScreens)
			//{
			//    if (screen.WorkingArea.Contains(rectangle))
			//    {
			//        return true;
			//    }
			//}
			//return false;

			// find closest screen
			Screen screen = Screen.FromRectangle(rectangle);
			if (!screen.WorkingArea.IntersectsWith(rectangle))
			{
				return false;
			}
			// TODO: push the rectangle onto the screen?

			// should at least be able to see some of the window, so allow
			return true;
		}

		// shows icon corresponding to alias in text box
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
					ParameterMap map = new ParameterMap();
					MagicWordExecutable executable = new MagicWordExecutable(magicWord, map);
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
				// only save the position if the form has been loaded,
				// else we would end up save the std default windows position
				if (loaded)
				{
					// save our position
					// note we never should be minimised or maximised
					Properties.Settings.Default.LastPosition = new Rectangle(Location, Size);
					Properties.Settings.Default.Save();
				}

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
			TermHotKeys();
		}

		private void InitHotKeys()
		{
			activateHotKeyController = new HotKeyController(this, ID_HOTKEY_ACTIVATE,
				"ActivateHotKey",
				Properties.Resources.ActivateDescription,
				"",		// no Windows 7 key
				new HotKey.HotKeyHandler(ShowEntryForm));

			addMagicWordHotKeyController = new HotKeyController(this, ID_HOTKEY_ADD_MAGIC_WORD,
				"AddMagicWordHotKey",
				Properties.Resources.AddMagicWordDescription,
				"",		// no Windows 7 key
				new HotKey.HotKeyHandler(AddNewMagicWordForActiveWindow));
		}

		private void TermHotKeys()
		{
			addMagicWordHotKeyController.Dispose();
			activateHotKeyController.Dispose();
		}

		private void AddNewMagicWordForActiveWindow()
		{
			MagicWord newMagicWord = new MagicWord();
			// get the active window
			IntPtr hWnd = Win32.GetForegroundWindow();

			if (hWnd != IntPtr.Zero)
			{
				// use details from the active window to prefill a new MagicWord
				MagicWordForm.GetWindowDetails(hWnd, newMagicWord);
			}
			AddNewMagicWord(newMagicWord);
		}

		private void AddNewMagicWord()
		{
			AddNewMagicWord(new MagicWord());
		}

		private void AddNewMagicWord(MagicWord newMagicWord)
		{
			// let the user edit the details

			// need to activate, or the MagicWordForm will be hidden under whatever was the active window
			this.Activate();	
			MagicWordForm dlg = new MagicWordForm(newMagicWord);
			if (dlg.ShowDialog() == DialogResult.OK)
			{
				// user wants this word, so insert it
				MagicWords.Instance.Insert(newMagicWord);
			}
		}

		private void ShowEntryForm()
		{
			this.Visible = true;
			this.Activate();
			this.Input.Focus();
			UpdateIconDisplayStyle();
			DoAutoComplete();
		}

		private void HideEntryForm()
		{
			this.Visible = false;
		}

		private void UpdateIconDisplayStyle()
		{
			this.magicWordListBox.View = Properties.Settings.Default.IconView;
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

		private void addNewMagicWordToolStripMenuItem_Click(object sender, EventArgs e)
		{
			AddNewMagicWord();
		}

		private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ShowOptions();
		}

		private void buttonOptions_Click(object sender, EventArgs e)
		{
			ShowOptions();
		}

		private void ShowOptions()
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

		private void visitDualLauncherWebsiteToolStripMenuItem_Click(object sender, EventArgs e)
		{
			VisitDualLauncherWebsite();
		}

		private void VisitDualLauncherWebsite()
		{
			try
			{
				System.Diagnostics.Process.Start("http://dualmonitortool.sourceforge.net/duallauncher.html");
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, Program.MyTitle);
			}
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
			if (e.KeyCode == Keys.Enter)
			{
				ProcessInput(1);
				e.SuppressKeyPress = true;	// stop the annoying beep when this gets passed to textbox
			}
			else if (CompareKeys(e, Properties.Settings.Default.Position1Key)) //Keys.F1)
			{
				ProcessInput(1);
			}
			else if (CompareKeys(e, Properties.Settings.Default.Position2Key)) //Keys.F2)
			{
				ProcessInput(2);
			}
			else if (CompareKeys(e, Properties.Settings.Default.Position3Key)) //Keys.F3)
			{
				ProcessInput(3);
			}
			else if (CompareKeys(e, Properties.Settings.Default.Position4Key)) //Keys.F4)
			{
				ProcessInput(4);
			}
			else if (e.KeyCode == Keys.Escape)
			{
				// clear text and hide
				Input.Text = "";
				HideEntryForm();
				e.SuppressKeyPress = true;	// stop the annoying beep when this gets passed to textbox
			}
			else
			{
				ProcessAutoCompleteKeyDown(sender, e);
			}
		}

		private bool CompareKeys(KeyEventArgs e, uint propertyValue)
		{
			bool ret = false;
			KeyCombo keyCombo = new KeyCombo();
			keyCombo.FromPropertyValue(propertyValue);

			if (e.KeyCode == keyCombo.KeyCode)
			{
				// check the modifier keys
				// Note: the Win modifier is not available in KeyEventArgs
				if (e.Alt == keyCombo.AltMod && e.Control == keyCombo.ControlMod && e.Shift == keyCombo.ShiftMod)
				{
					ret = true;
				}
			}

			return ret;
		}

		private void ProcessInput(int position)
		{
			ProcessInput(Input.Text, position);
		}

		private void ProcessInput(string alias, int position)
		{
			List<MagicWord> magicWords = MagicWords.Instance.FindAllByAlias(alias);

			if (magicWords.Count > 0)
			{
				ParameterMap map = new ParameterMap();
				foreach (MagicWord magicWord in magicWords)
				{
					StartMagicWord(magicWord, position, map);
				}
				Input.Text = "";
				HideEntryForm();
			}
		}

		private void StartMagicWord(MagicWord magicWord, int positionIndex1, ParameterMap map)
		{
			if (magicWord != null)
			{
				magicWord.UseCount++;
				magicWord.LastUsed = DateTime.Now;
				StartupPosition startPosition = magicWord.GetStartupPosition(positionIndex1);
				StartupController.Instance.Launch(magicWord, startPosition, map);
				//Input.Text = "";
				//HideEntryForm();
			}
		}

		private void timer_Tick(object sender, EventArgs e)
		{
			StartupController.Instance.Poll();
		}

		private void magicWordListBox_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
		{
			//ListViewItem listViewItem = e.Item;
			//if (listViewItem != null)
			//{
			//    string alias = listViewItem.Text;
			//    //Input.Text = alias;
			//}
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

		#region AutoComplete

		// TODO: this needs moving into its own class

		private string curBase = "";
		private List<MagicWord> curMagicWords = new List<MagicWord>();
		private int curIndex = 0;
		private bool doAutoComplete = false;
		bool doDel = false;

		void Input_TextChanged(object sender, EventArgs e)
		{
			if (doAutoComplete)
			{
				doAutoComplete = false;
				//DoAutoComplete(Input.Text);
				DoAutoComplete();
			}
		}

		private void DoAutoComplete()
		{
			if (doDel && curBase.Length > 0)
			{
				Trace.WriteLine(string.Format("DoAutoComplete: deleting char"));
				doDel = false;
				curBase = curBase.Substring(0, curBase.Length - 1);
			}
			else
			{
				curBase = Input.Text;
			}

			Trace.WriteLine(string.Format("DoAutoComplete: curBase: {0}", curBase));
			int maxMostUsedSize = Properties.Settings.Default.MaxMostUsedSize;

			// if user doesn't want most used icons when no text
			// then don't bother sorting the list of magic words
			if (curBase.Length > 0 || maxMostUsedSize > 0)
			{
				// get magic words that match the current alias prefix
				curMagicWords = MagicWords.Instance.GetAutoCompleteWords(curBase);
				SortMagicWords(curMagicWords);
				curIndex = 0;

				// if no input, then truncate the list of magic words
				if (curBase.Length == 0)
				{
					if (curMagicWords.Count > maxMostUsedSize)
					{
						curMagicWords.RemoveRange(maxMostUsedSize, curMagicWords.Count - maxMostUsedSize);
					}
				}
			}
			ShowAutoCompleteText();
			ShowAutoCompleteList();
		}

		private void ShowAutoCompleteText()
		{
			Trace.WriteLine(string.Format("ShowAutoCompleteText: curBase: {0}", curBase));
			if (curBase.Length > 0 && curMagicWords.Count > 0)
			{
				Debug.Assert(curIndex >= 0 && curIndex < curMagicWords.Count);
				string curWord = curMagicWords[curIndex].Alias;
				Trace.WriteLine(string.Format("ShowAutoCompleteText: curWord: {0}", curWord));
				Input.Text = curWord;
				Input.SelectionStart = curBase.Length;
				Input.SelectionLength = curWord.Length - curBase.Length;
				ShowAliasIcon();
			}
			else
			{
				Input.Text = curBase;
				Input.SelectionStart = curBase.Length;
				Input.SelectionLength = 0;
				ShowAliasIcon();
			}
		}

		private void ShowAutoCompleteList()
		{
			// update the icons displayed
			magicWordListBox.SetMagicWords(curMagicWords);
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

		private void ProcessAutoCompleteKeyDown(object sender, KeyEventArgs e)
		{
			doAutoComplete = true;
			doDel = false;

			Trace.WriteLine(string.Format("ProcessAutoCompleteKeyDown KeyCode: {0} KeyValue: {1} KeyData: {2}",
				e.KeyCode, e.KeyValue, e.KeyData));
			if (e.KeyCode == Keys.Back || e.KeyCode == Keys.Delete)
			{
				// TODO: check user hasn't changed selection
				if (curBase.Length > 0)
				{
					doDel = true;
				}
			}
			else if (e.KeyCode == Keys.Up)
			{
				if (curMagicWords.Count > 1)
				{
					curIndex = (curIndex + curMagicWords.Count - 1) % curMagicWords.Count;
					doAutoComplete = false;
					ShowAutoCompleteText();
					e.Handled = true;
				}
			}
			else if (e.KeyCode == Keys.Down)
			{
				if (curMagicWords.Count > 1)
				{
					curIndex = (curIndex + 1) % curMagicWords.Count;
					doAutoComplete = false;
					ShowAutoCompleteText();
					e.Handled = true;
				}
			}
		}

		#endregion

	}
}
