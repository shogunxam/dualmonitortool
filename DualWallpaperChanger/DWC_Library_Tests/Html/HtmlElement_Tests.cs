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
	public class HtmlElement_Tests
	{
		[Test]
		public void ParseNullContent()
		{
			// Arrange

			// Act
			HtmlElement element = new HtmlElement(null);

			// Assert
			Assert.AreEqual("", element.GetElementName());
			Assert.AreEqual(0, element.GetAllAttributes().Count);
			Assert.IsNull(element.GetAttribute("test"));
		}

		[Test]
		public void ParseEmptyContent()
		{
			// Arrange

			// Act
			HtmlElement element = new HtmlElement("");

			// Assert
			Assert.AreEqual("", element.GetElementName());
			Assert.AreEqual(0, element.GetAllAttributes().Count);
			Assert.IsNull(element.GetAttribute("test"));
		}


		[Test]
		public void ParseSimpleStartElement()
		{
			// Arrange

			// Act
			HtmlElement element = new HtmlElement("<html>");

			// Assert
			Assert.AreEqual("html", element.GetElementName());
			Assert.AreEqual(0, element.GetAllAttributes().Count);
			Assert.IsNull(element.GetAttribute("test"));
		}

		[Test]
		public void ParseSimpleCloseElement()
		{
			// Arrange

			// Act
			HtmlElement element = new HtmlElement("</html>");

			// Assert
			Assert.AreEqual("html", element.GetElementName());
			Assert.AreEqual(0, element.GetAllAttributes().Count);
			Assert.IsNull(element.GetAttribute("test"));
		}

		[Test]
		public void ParseSimpleOpenCloseElement()
		{
			// Arrange

			// Act
			HtmlElement element = new HtmlElement("<br/>");

			// Assert
			Assert.AreEqual("br", element.GetElementName());
			Assert.AreEqual(0, element.GetAllAttributes().Count);
			Assert.IsNull(element.GetAttribute("test"));
		}


		[Test]
		public void ParseNameWithoutValue()
		{
			// Arrange

			// Act
			HtmlElement element = new HtmlElement("<input checked>");

			// Assert
			Assert.AreEqual("input", element.GetElementName());
			Assert.AreEqual(1, element.GetAllAttributes().Count);
			Assert.AreEqual("", element.GetAttribute("checked"));
		}


		[Test]
		public void ParseNameAndValueNoQuotes()
		{
			// Arrange

			// Act
			HtmlElement element = new HtmlElement("<input type=text>");

			// Assert
			Assert.AreEqual("input", element.GetElementName());
			Assert.AreEqual(1, element.GetAllAttributes().Count);
			Assert.AreEqual("text", element.GetAttribute("type"));
		}

		[Test]
		public void ParseNameAndValueWithSingleQuotes()
		{
			// Arrange

			// Act
			HtmlElement element = new HtmlElement("<input type='text'>");

			// Assert
			Assert.AreEqual("input", element.GetElementName());
			Assert.AreEqual(1, element.GetAllAttributes().Count);
			Assert.AreEqual("text", element.GetAttribute("type"));
		}

		[Test]
		public void ParseNameAndValueWithDoubleQuotes()
		{
			// Arrange

			// Act
			HtmlElement element = new HtmlElement("<input type=\"text\">");

			// Assert
			Assert.AreEqual("input", element.GetElementName());
			Assert.AreEqual(1, element.GetAllAttributes().Count);
			Assert.AreEqual("text", element.GetAttribute("type"));
		}

		[Test]
		public void ParseMultipleAttributes()
		{
			// Arrange

			// Act
			HtmlElement element = new HtmlElement("<a href=\"www.domain.com/42\" target=_blank>");

			// Assert
			Assert.AreEqual("a", element.GetElementName());
			Assert.AreEqual(2, element.GetAllAttributes().Count);
			Assert.AreEqual("www.domain.com/42", element.GetAttribute("href"));
			Assert.AreEqual("_blank", element.GetAttribute("target"));
		}

		[Test]
		public void SkipsOverExcessSpaces()
		{
			// Arrange

			// Act
			HtmlElement element = new HtmlElement("<  input  type  =  'text'  >");

			// Assert
			Assert.AreEqual("input", element.GetElementName());
			Assert.AreEqual(1, element.GetAllAttributes().Count);
			Assert.AreEqual("text", element.GetAttribute("type"));
		}

		[Test]
		public void ConvertsNamesToLowerCase()
		{
			// Arrange

			// Act
			HtmlElement element = new HtmlElement("<INput TYpe=\"TExt\">");

			// Assert
			Assert.AreEqual("input", element.GetElementName());
			Assert.AreEqual(1, element.GetAllAttributes().Count);
			// values should not be mapped to lower case
			Assert.AreEqual("TExt", element.GetAttribute("type"));
		}

	}
}
