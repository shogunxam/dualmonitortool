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

namespace DMT.Modules.Launcher
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel;
	using System.Data;
	using System.Drawing;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;
	using System.Windows.Forms;

	using DMT.Library.Environment;
	using DMT.Library.HotKeys;

	/// <summary>
	/// Magic word entry dialog
	/// </summary>
	partial class EntryForm : Form
	{
		LauncherModule _launcherModule;
		ICommandRunner _commandRunner;
		ILocalEnvironment _localEnvironment;

		bool _terminate = false;
		bool _loaded = false;	// indicates if form has been loaded

		string _curBase = string.Empty;
		List<MagicWord> _curMagicWords = new List<MagicWord>();
		int _curIndex = 0;
		bool _doAutoComplete = false;
		bool _doDel = false;

		/// <summary>
		/// Initialises a new instance of the <see cref="EntryForm" /> class.
		/// </summary>
		/// <param name="launcherModule">The launcher module</param>
		/// <param name="commandRunner">Runs the command</param>
		/// <param name="localEnvironment">Local environment</param>
		public EntryForm(LauncherModule launcherModule, ICommandRunner commandRunner, ILocalEnvironment localEnvironment)
		{
			_launcherModule = launcherModule;
			_commandRunner = commandRunner;
			_localEnvironment = localEnvironment;

			InitializeComponent();
		}

		/// <summary>
		/// Terminates the use (permanently) of the entry form.
		/// </summary>
		public void Terminate()
		{
			_terminate = true;
			Close();
		}

		/// <summary>
		/// Show the magic word entry dialog
		/// </summary>
		public void ShowEntryForm()
		{
			Visible = true;
			Activate();
			textBoxInput.Focus();
			UpdateIconDisplayStyle();
			DoAutoComplete();
		}

		private void EntryForm_Load(object sender, EventArgs e)
		{
			SetStartupPosition();

			// TODO: check this is no longer needed
			//// need to be notified whenever the magic words change
			////_launcherModule.MagicWords.ListChanged += new ListChangedEventHandler(OnMagicWordsChanged);

			magicWordListBox.InitControl(_commandRunner);

			// TODO: check this is no longer needed
			////SetAutoComplete();
			HideEntryForm();

			textBoxInput.TextChanged += new EventHandler(Input_TextChanged);

			// indicate form has been loaded, so we know this function has been called
			// this is currently used to know when the form position has been set
			// as we don't want to save it on exit unless it has been set
			_loaded = true;
		}

		private void EntryForm_Deactivate(object sender, EventArgs e)
		{
			HideEntryForm();
		}

		private void EntryForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (_terminate || e.CloseReason != CloseReason.UserClosing)
			{
				// need to close
				if (_loaded)
				{
					// save our position
					_launcherModule.EntryFormPosition = new Rectangle(Location, Size);
				}
			}
			else
			{
				// just hide the form and stop it from closing
				HideEntryForm();
				e.Cancel = true;
			}
		}

		private void textBoxInput_KeyDown(object sender, KeyEventArgs e)
		{
			ProcessKeyDown(sender, e);
		}

		private void magicWordListBox_KeyDown(object sender, KeyEventArgs e)
		{
			ProcessKeyDown(sender, e);
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
		
		void Input_TextChanged(object sender, EventArgs e)
		{
			if (_doAutoComplete)
			{
				_doAutoComplete = false;
				DoAutoComplete();
			}
		}

		private void buttonOptions_Click(object sender, EventArgs e)
		{
			_launcherModule.ShowOptions();
		}

		// This is used to restore the entry forms position to its last known position
		void SetStartupPosition()
		{
			Rectangle rectangle = _launcherModule.EntryFormPosition;
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

		bool IsSensibleStartupPosition(ref Rectangle rectangle)
		{
			if (rectangle.Width <= 0 || rectangle.Height <= 0)
			{
				return false;
			}

			//// TODO: this is a bit over zealous as it doesn't allow the window to be a fractionaly
			//// outside of the working area or split over screens
			////foreach (Screen screen in Screen.AllScreens)
			////{
			////    if (screen.WorkingArea.Contains(rectangle))
			////    {
			////        return true;
			////    }
			////}
			////return false;

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

		void HideEntryForm()
		{
			Visible = false;
		}

		void UpdateIconDisplayStyle()
		{
			this.magicWordListBox.View = _launcherModule.IconView;
		}

		private void ProcessKeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
			{
				ProcessInput(1);
				e.SuppressKeyPress = true;	// stop the annoying beep when this gets passed to textbox
			}
			else if (CompareKeys(e, _launcherModule.Position1Key))
			{
				ProcessInput(1);
			}
			else if (CompareKeys(e, _launcherModule.Position2Key))
			{
				ProcessInput(2);
			}
			else if (CompareKeys(e, _launcherModule.Position3Key))
			{
				ProcessInput(3);
			}
			else if (CompareKeys(e, _launcherModule.Position4Key))
			{
				ProcessInput(4);
			}
			else if (e.KeyCode == Keys.Escape)
			{
				// clear text and hide
				textBoxInput.Text = string.Empty;
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

		void ProcessAutoCompleteKeyDown(object sender, KeyEventArgs e)
		{
			_doAutoComplete = true;
			_doDel = false;

			if (e.KeyCode == Keys.Back || e.KeyCode == Keys.Delete)
			{
				// TODO: check user hasn't changed selection
				if (_curBase.Length > 0)
				{
					_doDel = true;
				}
			}
			else if (e.KeyCode == Keys.Up)
			{
				if (_curMagicWords.Count > 1)
				{
					_curIndex = (_curIndex + _curMagicWords.Count - 1) % _curMagicWords.Count;
					_doAutoComplete = false;
					ShowAutoCompleteText();
					e.Handled = true;
				}
			}
			else if (e.KeyCode == Keys.Down)
			{
				if (_curMagicWords.Count > 1)
				{
					_curIndex = (_curIndex + 1) % _curMagicWords.Count;
					_doAutoComplete = false;
					ShowAutoCompleteText();
					e.Handled = true;
				}
			}
		}

		void ProcessInput(int position)
		{
			ProcessInput(textBoxInput.Text, position);
		}

		void ProcessInput(string alias, int position)
		{
			List<MagicWord> magicWords = _launcherModule.MagicWords.FindAllByAlias(alias);

			if (magicWords.Count > 0)
			{
				textBoxInput.Text = string.Empty;
				HideEntryForm();
				if (_localEnvironment.IsWin10OrLater())
				{
					// Prior to Win 10, setting the form visibility to false
					// was sufficient to stop it from being picked up by Win32.GetForegroundWindow(),
					// which is important if we are running an internal command to say,
					// move the active window to the next monitor.
					// However Win 10 still thinks that the window we have just hidden 
					// is still the foreground window.

					// a quick yield seems to fix the problem
					System.Threading.Thread.Sleep(0);
				}

				_launcherModule.StartMagicWords(magicWords, position);
			}
		}

		void DoAutoComplete()
		{
			if (_doDel && _curBase.Length > 0)
			{
				_doDel = false;
				_curBase = _curBase.Substring(0, _curBase.Length - 1);
			}
			else
			{
				_curBase = textBoxInput.Text;
			}

			int maxMostUsedSize = _launcherModule.MaxIcons;

			// if user doesn't want most used icons when no text
			// then don't bother sorting the list of magic words
			if (_curBase.Length > 0 || maxMostUsedSize > 0)
			{
				// get magic words that match the current alias prefix
				_curMagicWords = _launcherModule.MagicWords.GetAutoCompleteWords(_curBase);
				SortMagicWords(_curMagicWords);
				_curIndex = 0;

				// if no input, then truncate the list of magic words
				if (_curBase.Length == 0)
				{
					if (_curMagicWords.Count > maxMostUsedSize)
					{
						_curMagicWords.RemoveRange(maxMostUsedSize, _curMagicWords.Count - maxMostUsedSize);
					}
				}
			}

			ShowAutoCompleteText();
			ShowAutoCompleteList();
		}

		void SortMagicWords(List<MagicWord> magicWords)
		{
			if (_launcherModule.UseMru)
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

		void ShowAutoCompleteText()
		{
			if (_curBase.Length > 0 && _curMagicWords.Count > 0)
			{
				System.Diagnostics.Debug.Assert(_curIndex >= 0 && _curIndex < _curMagicWords.Count, "cur index is invalid");
				string curWord = _curMagicWords[_curIndex].Alias;
				textBoxInput.Text = curWord;
				textBoxInput.SelectionStart = _curBase.Length;
				textBoxInput.SelectionLength = curWord.Length - _curBase.Length;
				ShowAliasIcon();
			}
			else
			{
				textBoxInput.Text = _curBase;
				textBoxInput.SelectionStart = _curBase.Length;
				textBoxInput.SelectionLength = 0;
				ShowAliasIcon();
			}
		}

		void ShowAutoCompleteList()
		{
			// update the icons displayed
			magicWordListBox.SetMagicWords(_curMagicWords);
		}

		// shows icon corresponding to alias in text box
		void ShowAliasIcon()
		{
			Icon fileIcon = null;
			string alias = textBoxInput.Text;
			if (alias.Length > 0)
			{
				// first find magic word for alias 
				MagicWord magicWord = _launcherModule.MagicWords.FindByAlias(alias);
				if (magicWord != null)
				{
					ParameterMap map = new ParameterMap();
					MagicWordExecutable executable = new MagicWordExecutable(magicWord, _commandRunner, map);
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
	}
}
