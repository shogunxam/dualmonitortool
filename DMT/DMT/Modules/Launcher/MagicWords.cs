#region copyright
// This file is part of Dual Monitor Tools which is a set of tools to assist
// users with multiple monitor setups.
// Copyright (C) 2010-2015  Gerald Evans
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
using DMT.Library;
using DMT.Library.Binding;

namespace DMT.Modules.Launcher
{
	/// <summary>
	/// List of MagicWords
	/// </summary>
	public class MagicWords : SortableBindingList<MagicWord>
	{
		//#region Singleton framework
		//// the single instance of the controller object
		//static private readonly MagicWords instance = new MagicWords();

		//// Explicit static constructor to tell C# compiler
		//// not to mark type as beforefieldinit
		//static MagicWords()
		//{
		//}

		//private MagicWords()
		//{
		//	// this is the default
		//	this.ListChanged += new ListChangedEventHandler(MagicWords_ListChanged);
		//}

		//private void MagicWords_ListChanged(object sender, ListChangedEventArgs e)
		//{
		//	isDirty = true;
		//}

		//public static MagicWords Instance
		//{
		//	get
		//	{
		//		return instance;
		//	}
		//}
		//#endregion

		public MagicWords()
		{
			// this is the default
			this.ListChanged += new ListChangedEventHandler(MagicWords_ListChanged);
		}

		void MagicWords_ListChanged(object sender, ListChangedEventArgs e)
		{
			_isDirty = true;
		}


		public delegate void WordsUpdatedHandler();

		private BindingList<MagicWord> magicWords
		{
			get { return this; }
		}

		/// <summary>
		/// DataSource for use with data bound controls
		/// </summary>
		public BindingList<MagicWord> DataSource
		{
			get { return this; }
		}

		bool _isDirty;

		/// <summary>
		/// Loads the magic words from a xml file.
		/// If the file doesn't exist, then a default set of magic words is set.
		/// (TODO: This is not really the correct place to do this.)
		/// </summary>
		/// <param name="filename">Xml file to load magic words from</param>
		/// <returns>true (always)</returns>
		public bool Load(string filename)
		{
			bool ret = false;

			magicWords.Clear();

			if (File.Exists(filename))
			{
				Merge(XmlImporter.Import(filename));
				ret = true;
			}

			_isDirty = false;

			return ret;
		}
		//public bool Load(string filename)
		//{
		//	bool ret = true;

		//	magicWords.Clear();

		//	if (File.Exists(filename))
		//	{
		//		Merge(XmlImporter.Import(filename));
		//	}
		//	else
		//	{
		//		// if file doesn't exist, just add a single magic word for help
		//		MagicWord mw = new MagicWord("help", "http://dualmonitortool.sourceforge.net/duallauncher.html");
		//		magicWords.Add(mw);
		//	}

		//	_isDirty = false;

		//	return ret;
		//}

		/// <summary>
		/// Saves the magic words if they have changed.
		/// </summary>
		/// <param name="filename"></param>
		public void SaveIfDirty(string filename)
		{
			if (_isDirty)
			{
				Save(filename);
			}
		}

		/// <summary>
		/// Saves the magic words
		/// </summary>
		/// <param name="filename"></param>
		/// <returns></returns>
		bool Save(string filename)
		{
			Trace.WriteLine("Saving...");
			//XmlImporter.Export(MagicWords.Instance, filename);
			XmlImporter.Export(this, filename);
			_isDirty = false;
			return true;
		}

		//private void FireWordsUpdated()
		//{
		//    if (WordsUpdated != null)
		//    {
		//        WordsUpdated();
		//    }
		//}

		//public AutoCompleteStringCollection GetAutoCompleteWords()
		//{
		//    AutoCompleteStringCollection collection = new AutoCompleteStringCollection();

		//    foreach (MagicWord mw in magicWords)
		//    {
		//        collection.Add(mw.Alias);
		//    }
		//    return collection;
		//}

		/// <summary>
		/// Gets the list of magic words whose alias starts with the given prefix
		/// </summary>
		/// <param name="prefix"></param>
		/// <returns></returns>
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

		/// <summary>
		/// Finds the first magic word that has the given alias
		/// </summary>
		/// <param name="alias"></param>
		/// <returns></returns>
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

		/// <summary>
		/// Finds all magic words with the given alias
		/// </summary>
		/// <param name="alias"></param>
		/// <returns></returns>
		public List<MagicWord> FindAllByAlias(string alias)
		{
			List<MagicWord> mws = new List<MagicWord>();
			foreach (MagicWord mw in magicWords)
			{
				if (String.Compare(alias, mw.Alias, true) == 0)
				{
					mws.Add(mw);
				}
			}

			return mws;
		}

		/// <summary>
		/// Merges the given magic words into the current list of magic words.
		/// </summary>
		/// <param name="importedWords"></param>
		public void Merge(Collection<MagicWord> importedWords)
		{
			// we have 2 passes so that we can correctly support
			// multiple applications all using the same alias

			// First we remove all existing instances of the words
			// we are about to import
			foreach (MagicWord importedWord in importedWords)
			{
				MagicWord existingWord;
				while ((existingWord = FindByAlias(importedWord.Alias)) != null)
				{
					Remove(existingWord);
				}
			}

			// now we can add the imported words
			foreach (MagicWord importedWord in importedWords)
			{
				Add(importedWord);
			}

			// make sure list is correctly sorted
			ReSort();
		}
	}
}
