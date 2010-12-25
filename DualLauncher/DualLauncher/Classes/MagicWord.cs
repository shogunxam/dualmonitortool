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

namespace DualLauncher
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

		private string alias;
		/// <summary>
		/// An alias for the application.  
		/// This may contain spaces and punctuation characters etc. if desired.
		/// </summary>
		public string Alias
		{
			get { return alias; }
			set 
			{ 
				if (value != alias)
				{
					alias = value;
					NotifyPropertyChanged("Alias");
				}
			}
		}

		private string filename;
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
			get { return filename; }
			set 
			{ 
				if (value != filename)
				{
					filename = value;
					NotifyPropertyChanged("Filename");
				}
			}
		}

		private string parameters;
		/// <summary>
		/// Where Filename specifies an application, any parameters can be specified in here.
		/// These may also include escape sequences of the form %I% (or %W% if url encoding is required)
		/// for dynamic input.
		/// </summary>
		public string Parameters
		{
			get { return parameters; }
			set
			{
				if (value != parameters)
				{
					parameters = value;
					NotifyPropertyChanged("Parameters");
				}
			}
		}

		private string startDirectory;
		/// <summary>
		/// Directory to start the application in.
		/// </summary>
		public string StartDirectory
		{
			get { return startDirectory; }
			set 
			{ 
				if (value != startDirectory)
				{
					startDirectory = value;
					NotifyPropertyChanged("StartDirectory");
				}
			}
		}

		private string comment;
		/// <summary>
		/// For compatibility with SlickRun, if dynamic input is used and a non-named parameter is used,
		/// then the comment is used as the prompt for input, but named parameters are recommended.
		/// Otherwise, the comment is free for whatver the user wishes.
		/// </summary>
		public string Comment
		{
			get { return comment; }
			set
			{
				if (value != comment)
				{
					comment = value;
					NotifyPropertyChanged("Comment");
				}
			}
		}

		private string windowClass;
		/// <summary>
		/// This is used to help detect the correct main window of the application so that
		/// it can be positioned at its required location if required.
		/// </summary>
		public string WindowClass
		{
			get { return windowClass; }
			set 
			{ 
				if (value != windowClass)
				{
					windowClass = value;
					NotifyPropertyChanged("WindowClass");
				}
			}
		}

		private string captionRegExpr;
		/// <summary>
		/// This is used to help detect the correct main window of the application so that
		/// it can be positioned at its required location if required.
		/// </summary>
		public string CaptionRegExpr
		{
			get { return captionRegExpr; }
			set
			{
				if (value != captionRegExpr)
				{
					captionRegExpr = value;
					NotifyPropertyChanged("CaptionRegExpr");
				}
			}
		}

		private int useCount;
		/// <summary>
		/// Number of times this magic word has been run (since reset) via Dual Launcher.
		/// </summary>
		public int UseCount
		{
			get { return useCount; }
			set 
			{
				if (value != useCount)
				{
					useCount = value;
					NotifyPropertyChanged("UseCount");
				}
			}
		}

		private DateTime lastUsed = DateTime.MinValue;
		/// <summary>
		/// The last time (unless reset) the magic word was run via Dual Launcher.
		/// </summary>
		public DateTime LastUsed
		{
			get { return lastUsed; }
			set
			{
				if (value != lastUsed)
				{
					lastUsed = value;
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
					return startupPosition1;
				case 2:
					return startupPosition2;
				case 3:
					return startupPosition3;
				case 4:
					return startupPosition4;
				default:
					throw new ApplicationException(string.Format("MagicWord.GetStartupPosition - position: {0} invalid", index1));
			}
		}

		private StartupPosition startupPosition1;
		/// <summary>
		/// The first StartupPosition
		/// </summary>
		public StartupPosition StartupPosition1
		{
			get { return startupPosition1; }
			set 
			{ 
				startupPosition1 = value;
				NotifyPropertyChanged("StartupPosition1");
			}
		}

		private StartupPosition startupPosition2;
		/// <summary>
		/// The second StartupPosition
		/// </summary>
		public StartupPosition StartupPosition2
		{
			get { return startupPosition2; }
			set
			{
				startupPosition2 = value;
				NotifyPropertyChanged("StartupPosition2");
			}
		}

		private StartupPosition startupPosition3;
		/// <summary>
		/// The third StartupPosition
		/// </summary>
		public StartupPosition StartupPosition3
		{
			get { return startupPosition3; }
			set
			{
				startupPosition3 = value;
				NotifyPropertyChanged("StartupPosition3");
			}
		}

		private StartupPosition startupPosition4;
		/// <summary>
		/// The fourth StartupPosition
		/// </summary>
		public StartupPosition StartupPosition4
		{
			get { return startupPosition4; }
			set
			{
				startupPosition4 = value;
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
			this.alias = alias;
			this.filename = filename;
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
