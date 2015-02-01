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

using DualMonitorTools.DualWallpaperChanger.Html;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DWC_Library_Tests.Html
{
	[TestFixture]
	public class HtmlReader_Tests
	{
		[Test]
		public void ParseNullContent()
		{
			// Arrange
			string html = null;
			List<Token> expectedTokens = new List<Token>();

			// Act
			IList<Token> tokens = Parse(html);

			// Assert
			AssertTokensEqual(expectedTokens, tokens);
		}

		[Test]
		public void ParseEmptyString()
		{
			// Arrange
			string html = "";
			List<Token> expectedTokens = new List<Token>();

			// Act
			IList<Token> tokens = Parse(html);

			// Assert
			AssertTokensEqual(expectedTokens, tokens);
		}

		[Test]
		public void ParseStartElement()
		{
			// Arrange
			string html = "<apple>";
			List<Token> expectedTokens = new List<Token>();
			expectedTokens.Add(new Token(HtmlNodeType.Element, "<apple>"));

			// Act
			IList<Token> tokens = Parse(html);

			// Assert
			AssertTokensEqual(expectedTokens, tokens);
		}

		[Test]
		public void ParseEndElement()
		{
			// Arrange
			string html = "</apple>";
			List<Token> expectedTokens = new List<Token>();
			expectedTokens.Add(new Token(HtmlNodeType.EndElement, "</apple>"));

			// Act
			IList<Token> tokens = Parse(html);

			// Assert
			AssertTokensEqual(expectedTokens, tokens);
		}

		[Test]
		public void ParseClosingStartElement()
		{
			// Arrange
			string html = "<apple/>";
			List<Token> expectedTokens = new List<Token>();
			expectedTokens.Add(new Token(HtmlNodeType.Element, "<apple/>"));

			// Act
			IList<Token> tokens = Parse(html);

			// Assert
			AssertTokensEqual(expectedTokens, tokens);
		}

		[Test]
		public void ParseText()
		{
			// Arrange
			string html = "apple";
			List<Token> expectedTokens = new List<Token>();
			expectedTokens.Add(new Token(HtmlNodeType.Text, "apple"));

			// Act
			IList<Token> tokens = Parse(html);

			// Assert
			AssertTokensEqual(expectedTokens, tokens);
		}

		[Test]
		public void ParseComment()
		{
			// Arrange
			string html = "<!--- apple --->";
			List<Token> expectedTokens = new List<Token>();
			expectedTokens.Add(new Token(HtmlNodeType.Comment, "<!--- apple --->"));

			// Act
			IList<Token> tokens = Parse(html);

			// Assert
			AssertTokensEqual(expectedTokens, tokens);
		}

		[Test]
		public void ParseSimple()
		{
			// Arrange
			string html = "<a href=\"#\">link</a>";
			List<Token> expectedTokens = new List<Token>();
			expectedTokens.Add(new Token(HtmlNodeType.Element, "<a href=\"#\">"));
			expectedTokens.Add(new Token(HtmlNodeType.Text, "link"));
			expectedTokens.Add(new Token(HtmlNodeType.EndElement, "</a>"));

			// Act
			IList<Token> tokens = Parse(html);

			// Assert
			AssertTokensEqual(expectedTokens, tokens);
		}

		IList<Token> Parse(string html)
		{
			List<Token> tokens = new List<Token>();

			HtmlReader htmlReader = new HtmlReader(html);
			while (htmlReader.Read())
			{
				Token token = new Token(htmlReader.NodeType, htmlReader.Value);
				tokens.Add(token);
			}

			return tokens;
		}

		void AssertTokensEqual(IList<Token> expectedTokens, IList<Token> tokens)
		{
			int minCount = Math.Min(expectedTokens.Count, tokens.Count);
			for (int i = 0; i < minCount; i++)
			{
				Assert.AreEqual(expectedTokens[i], tokens[i]);
			}
			if (expectedTokens.Count > tokens.Count)
			{
				for (int i = tokens.Count; i < expectedTokens.Count; i++)
				{
					Assert.AreEqual(expectedTokens[i], null);
				}
			}
			else if (tokens.Count > expectedTokens.Count)
			{
				for (int i = expectedTokens.Count; i < tokens.Count; i++)
				{
					Assert.AreEqual(null, tokens[i]);
				}
			}
		}
	}
}
