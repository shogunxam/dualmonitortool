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

namespace DMT.Library.Html
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;

	/// <summary>
	/// Type of node
	/// </summary>
	public enum HtmlNodeType 
	{ 
		/// <summary>
		/// No node / default value
		/// </summary>
		None = 0, 

		/// <summary>
		/// Start element node
		/// </summary>
		Element = 1, 

		/// <summary>
		/// End element node
		/// </summary>
		EndElement = 2, 

		/// <summary>
		/// Text node
		/// </summary>
		Text = 3, 

		/// <summary>
		/// Comment node
		/// </summary>
		Comment = 4, 

		/// <summary>
		/// Instruction node
		/// </summary>
		Instruction = 5 
	}

	/// <summary>
	/// A (very) simple parser to parse HTML.
	/// </summary>
	public class HtmlReader
	{
		const string StartComment = "<!--";
		const string EndComment = "-->";

		string _html;
		int _curIndex;

		/// <summary>
		/// Initialises a new instance of the <see cref="HtmlReader" /> class.
		/// </summary>
		/// <param name="html">HTML to read</param>
		public HtmlReader(string html)
		{
			_html = html;

			// handle null without the need to continually test for it in Read()
			if (_html == null)
			{
				_html = string.Empty;
			}
		}

		/// <summary>
		/// Gets the node type
		/// </summary>
		public HtmlNodeType NodeType { get; private set; }

		/// <summary>
		/// Gets the node value
		/// </summary>
		public string Value { get; private set; }

		/// <summary>
		/// Reads the next node from the input
		/// </summary>
		/// <returns>True if a node has been read</returns>
		public bool Read()
		{
			NodeType = HtmlNodeType.None;
			Value = string.Empty;

			if (CharAvailable(0))
			{
				char ch = PeekChar(0);
				if (ch == '<')
				{
					if (CharAvailable(1))
					{
						char ch2 = PeekChar(1);
						if (ch2 == '!')
						{
							if (TakeComment())
							{
							}
							else
							{
								// assume doctype instruction
								TakeElement();
							}
						}
						else
						{
							TakeElement();
						}
					}
				}
				else
				{
					// text 
					TakeText();
				}

				return true;
			}

			return false;
		}

		bool TakeComment()
		{
			if (TakeString(StartComment))
			{
				StringBuilder sb = new StringBuilder(StartComment);

				Value = StartComment;
				int endOffset = PeekScan(EndComment);
				int length;
				if (endOffset >= 0)
				{
					length = endOffset + EndComment.Length;
				}
				else
				{
					length = RemainingLength();
				}

				Value += _html.Substring(_curIndex, length);
				_curIndex += length;

				NodeType = HtmlNodeType.Comment;
				return true;
			}

			return false;
		}

		bool TakeElement()
		{
			// take everything between '<' and '>' (inclusive)
			if (CharAvailable(2) && TakeString("<"))
			{
				StringBuilder nodeValue = new StringBuilder();
				nodeValue.Append("<");
				char ch = PeekChar(0);
				if (ch == '/')
				{
					NodeType = HtmlNodeType.EndElement;
				}
				else if (ch == '!')
				{
					NodeType = HtmlNodeType.Instruction;
				}
				else
				{
					NodeType = HtmlNodeType.Element;
				}

				char curQuoteChar = '\0';
				while (TakeAnyChar(out ch))
				{
					nodeValue.Append(ch);
					if (curQuoteChar != '\0')
					{
						// within quotes -looking for matching end quote
						if (ch == curQuoteChar)
						{
							// found matching quote
							curQuoteChar = '\0';
						}
					}
					else
					{
						// not within quotes
						if (ch == '\'' || ch == '"')
						{
							curQuoteChar = ch;
						}
						else if (ch == '>')
						{
							break;
						}
					}
				}

				Value = nodeValue.ToString();

				return true;
			}

			return false;
		}

		bool TakeText()
		{
			StringBuilder sb = new StringBuilder();
			NodeType = HtmlNodeType.Text;
			while (_curIndex < _html.Length)
			{
				char ch = PeekChar(0);
				if (ch == '<')
				{
					break;
				}
				else
				{
					sb.Append(ch);
					_curIndex++;
				}
			}

			Value = sb.ToString();
			return Value.Length > 0;
		}

		bool TakeAnyChar(out char ch)
		{
			if (_curIndex < _html.Length)
			{
				ch = _html[_curIndex];
				_curIndex++;
				return true;
			}

			ch = '\0';
			return false;
		}

		bool TakeString(string match)
		{
			if (PeekMatches(match))
			{
				_curIndex += match.Length;
				return true;
			}

			return false;
		}

		bool PeekMatches(string match)
		{
			if (!CharAvailable(match.Length))
			{
				return false;
			}

			return PeekMatches(0, match);
		}

		bool PeekMatches(int offset, string match)
		{
			// it's the callers responsibility to make sure not past end of string
			for (int matchIndex = 0; matchIndex < match.Length; matchIndex++)
			{
				if (PeekChar(offset + matchIndex) != match[matchIndex])
				{
					return false;
				}
			}

			return true;
		}

		int PeekScan(string match)
		{
			for (int offset = 0; CharAvailable(offset + match.Length); offset++)
			{
				if (PeekMatches(offset, match))
				{
					return offset;
				}
			}

			return -1;
		}

		bool CharAvailable(int offset)
		{
			return _curIndex + offset < _html.Length;
		}

		int RemainingLength()
		{
			return _html.Length - _curIndex;
		}

		char PeekChar(int offset)
		{
			// it's the callers responsibility to make sure not past end of string
			return _html[_curIndex + offset];
		}
	}
}
