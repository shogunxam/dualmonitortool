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
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.IO;
using System.Diagnostics;

namespace DualLauncher
{
	//public class MagicWords : BindingList<MagicWord>
	public class MagicWords : SortableBindingList<MagicWord>
	//public class MagicWords
	{
		#region Singleton framework
		// the single instance of the controller object
		static readonly MagicWords instance = new MagicWords();

		// Explicit static constructor to tell C# compiler
		// not to mark type as beforefieldinit
		static MagicWords()
		{
		}

		MagicWords()
		{
			// this is the default
			//this.RaiseListChangedEvents = true;
			this.ListChanged += new ListChangedEventHandler(MagicWords_ListChanged);
		}

		void MagicWords_ListChanged(object sender, ListChangedEventArgs e)
		{
			isDirty = true;
		}

		public static MagicWords Instance
		{
			get
			{
				return instance;
			}
		}
		#endregion

		public delegate void WordsUpdatedHandler();

		//public event WordsUpdatedHandler WordsUpdated;

		//List<MagicWord> magicWords = new List<MagicWord>();
		BindingList<MagicWord> magicWords
		{
			get { return this; }
		}

		public BindingList<MagicWord> DataSource
		{
			get { return this; }
		}

		private bool isDirty;
		public bool IsDirty
		{
			get { return isDirty; }
		}

		public bool Load(string filename)
		{
			bool ret = true;

			magicWords.Clear();

			if (File.Exists(filename))
			{
				Merge(XmlImporter.Import(filename));
			}


			//// test code
			//MagicWord mw = new MagicWord("notepad", @"C:\Windows\notepad.exe");
			//AddTestPosns(mw);

			//mw = new MagicWord("test", @"T:\Test.exe");
			//AddTestPosns(mw);

			//mw = new MagicWord("explorer", @"C:\Windows\explorer.exe");
			//AddTestPosns(mw);

			//mw = new MagicWord("iexplore", @"C:\Program Files (x86)\Internet Explorer\iexplore.exe");
			//AddTestPosns(mw);

			//mw = new MagicWord("firefox", @"G:\Mozilla Firefox\firefox.exe");
			//AddTestPosns(mw);

			//mw = new MagicWord("vim", @"G:\Vim\vim72\gvim.exe");
			//AddTestPosns(mw);

			//mw = new MagicWord("cmd", @"C:\Windows\System32\cmd.exe");
			//AddTestPosns(mw);

			//mw = new MagicWord("paint", @"G:\Paint.NET\PaintDotNet.exe");
			//mw.CaptionRegExpr = "Paint.NET";
			//AddTestPosns(mw);

			//mw = new MagicWord("vnc", @"G:\VNC4\vncviewer.exe");
			//AddTestPosns(mw);

			//mw = new MagicWord("dmt", "http://dualmonitortool.sourceforge.net");
			//AddTestPosns(mw);

			//mw = new MagicWord("vinotepad", @"C:\Windows\notepad.exe");
			//AddTestPosns(mw);

			//FireWordsUpdated();

			isDirty = false;

			return ret;
		}

		//private void AddTestPosns(MagicWord mw)
		//{
		//    mw.StartupPosition1 = new StartupPosition(new Rectangle(0, 0, 400, 400));
		//    mw.StartupPosition2 =  new StartupPosition(new Rectangle(1280, 0, 400, 400));
		//    mw.StartupPosition3 =  new StartupPosition(new Rectangle(880, 624, 400, 400));
		//    mw.StartupPosition4 =  new StartupPosition(new Rectangle(2160, 624, 400, 400));
		//    magicWords.Add(mw);
		//}

		public void SaveIfDirty(string filename)
		{
			if (IsDirty)
			{
				Save(filename);
			}
		}

		public bool Save(string filename)
		{
			Trace.WriteLine("Saving...");
			XmlImporter.Export(MagicWords.Instance, filename);
			isDirty = false;
			return true;
		}

		//private void FireWordsUpdated()
		//{
		//    if (WordsUpdated != null)
		//    {
		//        WordsUpdated();
		//    }
		//}

		public AutoCompleteStringCollection GetAutoCompleteWords()
		{
			AutoCompleteStringCollection collection = new AutoCompleteStringCollection();

			foreach (MagicWord mw in magicWords)
			{
				collection.Add(mw.Alias);
			}
			return collection;
		}

		public List<MagicWord> GetAutoCompleteWords(string prefix)
		{
			List<MagicWord> autoCompleteWords = new List<MagicWord>();

			foreach (MagicWord mw in magicWords)
			{
				if (mw.Alias.StartsWith(prefix, true, null))
				{
					autoCompleteWords.Add(mw);
				}
			}

			return autoCompleteWords;
		}

		public MagicWord FindByAlias(string alias)
		{
			foreach (MagicWord mw in magicWords)
			{
				if (String.Compare(alias, mw.Alias, true) == 0)
				{
					return mw;
				}
			}

			return null;
		}

		public void Merge(Collection<MagicWord> importedWords)
		{
			// imported words have precedence over existing words
			foreach (MagicWord importedWord in importedWords)
			{
				MagicWord existingWord = FindByAlias(importedWord.Alias);
				if (existingWord != null)
				{
					// word already exists
					Remove(existingWord);
				}
				Add(importedWord);
			}
		}
	}
}
