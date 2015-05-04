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
using System.Text;
using System.Drawing;
using System.ComponentModel;

namespace DMT.Modules.Launcher
{
	/// <summary>
	/// A magic word.
	/// This is the the program that is to be run togetehr with any environmantal
	/// details like parameters, start directory and info relating to positioning the application.
	/// It also includes an alias/mnemonic for the application.
	/// </summary>
	[Serializable]
	public class MagicWord : INotifyPropertyChanged
	{
		//public const int AlternativePositions = 4;
		//public enum Position { First = 0, Second = 1, Third = 2, Fourth = 3 }

		public event PropertyChangedEventHandler PropertyChanged;

		string _alias;
		/// <summary>
		/// An alias for the application.  
		/// This may contain spaces and punctuation characters etc. if desired.
		/// </summary>
		public string Alias
		{
			get { return _alias; }
			set 
			{ 
				if (value != _alias)
				{
					_alias = value;
					NotifyPropertyChanged("Alias");
				}
			}
		}

		string _filename;
		/// <summary>
		/// This can be:
		/// Full pathname to the application to run,
		/// or full pathname to the document to open
		/// or full pathname of an existing directory
		/// or a full URL which is understood by the users default browser.
		/// If it is a URL, it may contain escape sequences like %W% for dynamic input 
		/// from the user at runtime.
		/// </summary>
		public string Filename
		{
			get { return _filename; }
			set 
			{ 
				if (value != _filename)
				{
					_filename = value;
					NotifyPropertyChanged("Filename");
				}
			}
		}

		string _parameters;
		/// <summary>
		/// Where Filename specifies an application, any parameters can be specified in here.
		/// These may also include escape sequences of the form %I% (or %W% if url encoding is required)
		/// for dynamic input.
		/// </summary>
		public string Parameters
		{
			get { return _parameters; }
			set
			{
				if (value != _parameters)
				{
					_parameters = value;
					NotifyPropertyChanged("Parameters");
				}
			}
		}

		string _startDirectory;
		/// <summary>
		/// Directory to start the application in.
		/// </summary>
		public string StartDirectory
		{
			get { return _startDirectory; }
			set 
			{ 
				if (value != _startDirectory)
				{
					_startDirectory = value;
					NotifyPropertyChanged("StartDirectory");
				}
			}
		}

		string _comment;
		/// <summary>
		/// For compatibility with SlickRun, if dynamic input is used and a non-named parameter is used,
		/// then the comment is used as the prompt for input, but named parameters are recommended.
		/// Otherwise, the comment is free for whatver the user wishes.
		/// </summary>
		public string Comment
		{
			get { return _comment; }
			set
			{
				if (value != _comment)
				{
					_comment = value;
					NotifyPropertyChanged("Comment");
				}
			}
		}

		string _windowClass;
		/// <summary>
		/// This is used to help detect the correct main window of the application so that
		/// it can be positioned at its required location if required.
		/// </summary>
		public string WindowClass
		{
			get { return _windowClass; }
			set 
			{ 
				if (value != _windowClass)
				{
					_windowClass = value;
					NotifyPropertyChanged("WindowClass");
				}
			}
		}

		string _captionRegExpr;
		/// <summary>
		/// This is used to help detect the correct main window of the application so that
		/// it can be positioned at its required location if required.
		/// </summary>
		public string CaptionRegExpr
		{
			get { return _captionRegExpr; }
			set
			{
				if (value != _captionRegExpr)
				{
					_captionRegExpr = value;
					NotifyPropertyChanged("CaptionRegExpr");
				}
			}
		}

		int _useCount;
		/// <summary>
		/// Number of times this magic word has been run (since reset) via Dual Launcher.
		/// </summary>
		public int UseCount
		{
			get { return _useCount; }
			set 
			{
				if (value != _useCount)
				{
					_useCount = value;
					NotifyPropertyChanged("UseCount");
				}
			}
		}

		DateTime _lastUsed = DateTime.MinValue;
		/// <summary>
		/// The last time (unless reset) the magic word was run via Dual Launcher.
		/// </summary>
		public DateTime LastUsed
		{
			get { return _lastUsed; }
			set
			{
				if (value != _lastUsed)
				{
					_lastUsed = value;
					NotifyPropertyChanged("LastUsed");
				}
			}
		}
	
	
		// an array means passing index's around 
		//private StartupPosition[] positions = new StartupPosition[AlternativePositions];

		/// <summary>
		/// Gets the StartupPosition relating to the index (1 ... 4).
		/// </summary>
		/// <param name="index1">Index in range 1 to 4</param>
		/// <returns>The corresponding StartupPosition</returns>
		public StartupPosition GetStartupPosition(int index1)
		{
			switch (index1)
			{
				case 1:
					return _startupPosition1;
				case 2:
					return _startupPosition2;
				case 3:
					return _startupPosition3;
				case 4:
					return _startupPosition4;
				default:
					throw new ApplicationException(string.Format("MagicWord.GetStartupPosition - position: {0} invalid", index1));
			}
		}

		StartupPosition _startupPosition1;
		/// <summary>
		/// The first StartupPosition
		/// </summary>
		public StartupPosition StartupPosition1
		{
			get { return _startupPosition1; }
			set 
			{ 
				_startupPosition1 = value;
				NotifyPropertyChanged("StartupPosition1");
			}
		}

		StartupPosition _startupPosition2;
		/// <summary>
		/// The second StartupPosition
		/// </summary>
		public StartupPosition StartupPosition2
		{
			get { return _startupPosition2; }
			set
			{
				_startupPosition2 = value;
				NotifyPropertyChanged("StartupPosition2");
			}
		}

		StartupPosition _startupPosition3;
		/// <summary>
		/// The third StartupPosition
		/// </summary>
		public StartupPosition StartupPosition3
		{
			get { return _startupPosition3; }
			set
			{
				_startupPosition3 = value;
				NotifyPropertyChanged("StartupPosition3");
			}
		}

		StartupPosition _startupPosition4;
		/// <summary>
		/// The fourth StartupPosition
		/// </summary>
		public StartupPosition StartupPosition4
		{
			get { return _startupPosition4; }
			set
			{
				_startupPosition4 = value;
				NotifyPropertyChanged("StartupPosition4");
			}
		}
	
		/// <summary>
		/// defualt ctor
		/// </summary>
		public MagicWord()
		{
		}

		/// <summary>
		/// ctor with the minimum necessary to create a usable magic word
		/// </summary>
		/// <param name="alias">Alias to activate the magic word</param>
		/// <param name="filename">Application/document/directory/url associated with the magic word</param>
		public MagicWord(string alias, string filename)
		{
			this._alias = alias;
			this._filename = filename;
		}

		//public MagicWord Clone()
		//{
		//    MagicWord mw = (MagicWord)MemberwiseClone();

		//    // TODO

		//    return mw;
		//}

		//public StartupPosition StartPosition(Position position)
		//{
		//    return positions[(int)position];
		//}

		//public void SetPosition(Position position, StartupPosition startupPosition)
		//{
		//    positions[(int)position] = startupPosition;
		//}

		private void NotifyPropertyChanged(string property)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(property));
			}
		}
	}
}
