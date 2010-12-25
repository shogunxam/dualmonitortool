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
using System.Text.RegularExpressions;
using System.IO;
using System.Collections.ObjectModel;

namespace DualLauncher
{
	/// <summary>
	/// Class to import qrs (SlickRun) files
	/// </summary>
	public class QrsImporter
	{
		private static Regex regexAlias = new Regex(@"\[(.+)\]");
		private static Regex regexAssign = new Regex(@"(.*?)=(.*)");

		/// <summary>
		/// Static method to read and return all magic words in a QRS file
		/// </summary>
		/// <param name="filename"></param>
		/// <returns></returns>
		public static Collection<MagicWord> Import(string filename)
		{
			Collection<MagicWord> magicWords = new Collection<MagicWord>();

			MagicWord curMagicWord = null;

			using (StreamReader streamReader = new StreamReader(filename))
			{
				while (! streamReader.EndOfStream)
				{
					string curLine = streamReader.ReadLine();

					// check for start of a new magic word
					Match match = regexAlias.Match(curLine);
					if (match.Success)
					{
						if (curMagicWord != null)
						{
							// save previous magic word
							magicWords.Add(curMagicWord);
						}
						curMagicWord = new MagicWord();
						curMagicWord.Alias = match.Groups[1].Value;
					}
					else
					{
						// check for the other lines
						match = regexAssign.Match(curLine);
						if (match.Success)
						{
							string field = match.Groups[1].Value;
							if (String.Compare(field, "Filename", true) == 0)
							{
								curMagicWord.Filename = FieldToString(match.Groups[2].Value);
								// SlickRun tends to use just "iexplore" for Internet explorer,
								// but we need the full pathname
								if (string.Compare(curMagicWord.Filename, "iexplore", true) == 0)
								{
									curMagicWord.Filename = MagicWordExecutable.GetAssociatedApp(".htm");
								}
							}
							else if (String.Compare(field, "Path", true) == 0)
							{
								curMagicWord.StartDirectory = FieldToString(match.Groups[2].Value);
							}
							else if (String.Compare(field, "Params", true) == 0)
							{
								curMagicWord.Parameters = FieldToString(match.Groups[2].Value);
							}
							else if (String.Compare(field, "Notes", true) == 0)
							{
								curMagicWord.Comment = FieldToString(match.Groups[2].Value);
							}
							else if (String.Compare(field, "StartMode", true) == 0)
							{
								int showCmd = Win32.SW_SHOW;
								try
								{
									showCmd = Convert.ToInt32(FieldToString(match.Groups[2].Value));
								}
								catch (Exception)
								{
								}
								curMagicWord.StartupPosition1 = new StartupPosition();
								curMagicWord.StartupPosition1.ShowCmd = showCmd;
							}
						}
					}
				}
				if (curMagicWord != null)
				{
					// flush out final word
					magicWords.Add(curMagicWord);
				}
			}

			return magicWords;
		}

		private static string FieldToString(string fieldValue)
		{
			string ret;
			if (fieldValue.Length >= 2 && fieldValue[0] == '"' && fieldValue[fieldValue.Length - 1] == '"')
			{
				// TODO: any need to unescape quotes?
				ret = fieldValue.Substring(1, fieldValue.Length - 2);
			}
			else
			{
				ret = fieldValue;
			}

			return ret;
		}
	}
}
