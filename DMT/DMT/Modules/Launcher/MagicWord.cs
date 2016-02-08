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

namespace DMT.Modules.Launcher
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel;
	using System.Drawing;
	using System.Text;

	/// <summary>
	/// A magic word.
	/// This is the the program that is to be run together with any environmental
	/// details like parameters, start directory and info relating to positioning the application.
	/// It also includes an alias/mnemonic for the application.
	/// </summary>
	[Serializable]
	public class MagicWord : INotifyPropertyChanged
	{
		string _alias;
		string _filename;
		string _parameters;
		string _startDirectory;
		string _comment;
		string _windowClass;
		string _captionRegExpr;
		int _useCount;
		DateTime _lastUsed = DateTime.MinValue;
		StartupPosition _startupPosition1;
		StartupPosition _startupPosition2;
		StartupPosition _startupPosition3;
		StartupPosition _startupPosition4;

		/// <summary>
		/// Initialises a new instance of the <see cref="MagicWord" /> class.
		/// </summary>
		public MagicWord()
		{
		}

		/// <summary>
		/// Initialises a new instance of the <see cref="MagicWord" /> class.
		/// </summary>
		/// <param name="alias">Alias to activate the magic word</param>
		/// <param name="filename">Application/document/directory/url associated with the magic word</param>
		public MagicWord(string alias, string filename)
		{
			this._alias = alias;
			this._filename = filename;
		}

		/// <summary>
		/// Event that fires when a magic word has changed 
		/// </summary>
		public event PropertyChangedEventHandler PropertyChanged;

		/// <summary>
		/// Gets or sets the alias for the application.  
		/// This may contain spaces and punctuation characters etc. if desired.
		/// </summary>
		public string Alias
		{
			get 
			{ 
				return _alias; 
			}

			set 
			{ 
				if (value != _alias)
				{
					_alias = value;
					NotifyPropertyChanged("Alias");
				}
			}
		}

		/// <summary>
		/// Gets or sets the filename.
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
			get 
			{ 
				return _filename; 
			}

			set 
			{ 
				if (value != _filename)
				{
					_filename = value;
					NotifyPropertyChanged("Filename");
				}
			}
		}

		/// <summary>
		/// Gets or sets the parameters.
		/// Where Filename specifies an application, any parameters can be specified in here.
		/// These may also include escape sequences of the form %I% (or %W% if url encoding is required)
		/// for dynamic input.
		/// </summary>
		public string Parameters
		{
			get 
			{ 
				return _parameters; 
			}

			set
			{
				if (value != _parameters)
				{
					_parameters = value;
					NotifyPropertyChanged("Parameters");
				}
			}
		}

		/// <summary>
		/// Gets or sets the directory to start the application in.
		/// </summary>
		public string StartDirectory
		{
			get 
			{ 
				return _startDirectory; 
			}

			set 
			{ 
				if (value != _startDirectory)
				{
					_startDirectory = value;
					NotifyPropertyChanged("StartDirectory");
				}
			}
		}

		/// <summary>
		/// Gets or sets the comment.
		/// For compatibility with SlickRun, if dynamic input is used and a non-named parameter is used,
		/// then the comment is used as the prompt for input, but named parameters are recommended.
		/// Otherwise, the comment is free for whatever the user wishes.
		/// </summary>
		public string Comment
		{
			get 
			{ 
				return _comment; 
			}

			set
			{
				if (value != _comment)
				{
					_comment = value;
					NotifyPropertyChanged("Comment");
				}
			}
		}

		/// <summary>
		/// Gets or sets the window class.
		/// This is used to help detect the correct main window of the application so that
		/// it can be positioned at its required location if required.
		/// </summary>
		public string WindowClass
		{
			get 
			{ 
				return _windowClass; 
			}

			set 
			{ 
				if (value != _windowClass)
				{
					_windowClass = value;
					NotifyPropertyChanged("WindowClass");
				}
			}
		}

		/// <summary>
		/// Gets or sets the regular expression for the window caption.
		/// This is used to help detect the correct main window of the application so that
		/// it can be positioned at its required location if required.
		/// </summary>
		public string CaptionRegExpr
		{
			get 
			{ 
				return _captionRegExpr; 
			}

			set
			{
				if (value != _captionRegExpr)
				{
					_captionRegExpr = value;
					NotifyPropertyChanged("CaptionRegExpr");
				}
			}
		}

		/// <summary>
		/// Gets or sets the number of times this magic word has been run (since reset) via Launcher.
		/// </summary>
		public int UseCount
		{
			get 
			{ 
				return _useCount; 
			}

			set 
			{
				if (value != _useCount)
				{
					_useCount = value;
					NotifyPropertyChanged("UseCount");
				}
			}
		}

		/// <summary>
		/// Gets or sets the last time (unless reset) the magic word was run via Launcher.
		/// </summary>
		public DateTime LastUsed
		{
			get 
			{ 
				return _lastUsed; 
			}

			set
			{
				if (value != _lastUsed)
				{
					_lastUsed = value;
					NotifyPropertyChanged("LastUsed");
				}
			}
		}

		/// <summary>
		/// Gets or sets the first start up position
		/// </summary>
		public StartupPosition StartupPosition1
		{
			get 
			{ 
				return _startupPosition1; 
			}

			set 
			{ 
				_startupPosition1 = value;
				NotifyPropertyChanged("StartupPosition1");
			}
		}

		/// <summary>
		/// Gets or sets the second start up position
		/// </summary>
		public StartupPosition StartupPosition2
		{
			get 
			{ 
				return _startupPosition2; 
			}

			set
			{
				_startupPosition2 = value;
				NotifyPropertyChanged("StartupPosition2");
			}
		}

		/// <summary>
		/// Gets or sets the third start up position
		/// </summary>
		public StartupPosition StartupPosition3
		{
			get 
			{ 
				return _startupPosition3; 
			}

			set
			{
				_startupPosition3 = value;
				NotifyPropertyChanged("StartupPosition3");
			}
		}

		/// <summary>
		/// Gets or sets the fourth start up position
		/// </summary>
		public StartupPosition StartupPosition4
		{
			get 
			{
				return _startupPosition4; 
			}

			set
			{
				_startupPosition4 = value;
				NotifyPropertyChanged("StartupPosition4");
			}
		}

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

		void NotifyPropertyChanged(string property)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(property));
			}
		}
	}
}
