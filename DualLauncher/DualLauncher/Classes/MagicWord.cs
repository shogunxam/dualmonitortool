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
	[Serializable]
	public class MagicWord : INotifyPropertyChanged
	{
		public const int AlternativePositions = 4;
		public enum Position { First = 0, Second = 1, Third = 2, Fourth = 3 }

		public event PropertyChangedEventHandler PropertyChanged;

		private string alias;
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
	
	
		// an array means passing index's arround 
		//private StartupPosition[] positions = new StartupPosition[AlternativePositions];

		private StartupPosition startupPosition1;
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
		public StartupPosition StartupPosition4
		{
			get { return startupPosition4; }
			set
			{
				startupPosition4 = value;
				NotifyPropertyChanged("StartupPosition4");
			}
		}
	

		public MagicWord()
		{
		}

		public MagicWord(string alias, string filename)
		{
			this.alias = alias;
			this.filename = filename;
		}

		public MagicWord Clone()
		{
			MagicWord mw = (MagicWord)MemberwiseClone();

			// TODO

			return mw;
		}

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
