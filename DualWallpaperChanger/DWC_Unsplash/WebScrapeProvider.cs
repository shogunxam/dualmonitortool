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
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DualMonitorTools.DualWallpaperChanger.Unsplash
{
	/// <summary>
	/// An instance of a provider from the unsplash plugin
	/// </summary>
	class WebScrapeProvider : IImageProvider
	{
		WebScrapeConfig _config;

		// these relate to the provider type
		public string ProviderName { get { return WebScrapePlugin.PluginName; } }
		public Image ProviderImage { get { return WebScrapePlugin.PluginImage; } }
		public string Version { get { return WebScrapePlugin.PluginVersion; } }

		// these relate to the provider instance
		public string Description { get { return _config.Description; } }
		public int Weight { get { return _config.Weight; } }

		public Dictionary<string, string> Config { get { return _config.ToDictionary(); } }

		static Random _random = new Random();

		HttpConnectionManager _connectionManager = new HttpConnectionManager();

		HttpRequester _httpRequester;

		public WebScrapeProvider(Dictionary<string, string> config)
		{
			_config = new WebScrapeConfig(config);

			_httpRequester = new HttpRequester();
		}

		public Dictionary<string, string> ShowUserOptions()
		{
			WebScrapeForm dlg = new WebScrapeForm(_config);
			if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				_config = dlg.GetConfig();
				return _config.ToDictionary();
			}

			// return null to indicate options have not been updated
			return null;
		}

		public Image GetRandomImage(Size optimumSize)
		{
			Image image = null;

			string url = "https://unsplash.com/";
			HttpConnection homeConnection;
			string homePage = GetPage(url, null, "UnsplashHome", out homeConnection);

			string randomPageUrl = null;
			if (!_config.FirstPageOnly)
			{
				randomPageUrl = GetRandomPageUrl(homePage);
			}

			string randomPage;
			HttpConnection randomPageConnection;
			if (randomPageUrl == null)
			{
				// use the home page as our random page
				randomPageConnection = homeConnection;
				randomPage = homePage;
			}
			else
			{
				randomPage = GetPage(randomPageUrl, homeConnection, "UnsplashHome", out randomPageConnection);
			}

			IList<string> imageUrls;
			imageUrls = ParseImagesOnPage(randomPage);

			if (imageUrls.Count > 0)
			{
				// choose a random image
				int index = _random.Next(imageUrls.Count);
				string imageUrl =imageUrls[index];
				image = GetImage(imageUrl, randomPageConnection);
			}

			return image;
		}

		string GetRandomPageUrl(string homePage)
		{
			string url = null;

			// home page has a navigation bar
			int maxPages = ParseNumPagesOnPage(homePage);
			if (maxPages > 0)
			{
				int randomPage = _random.Next(1, maxPages + 1); // (inclusive, exclusive)
				// don't ask for the first page again
				if (randomPage > 1)
				{
					url = string.Format("/?page={0}", randomPage);
				}
			}

			return url;
		}

		IList<string> ParseImagesOnPage(string page)
		{
			List<string> images = new List<string>();

			string aHref = string.Empty;
			string aInnerText = string.Empty;

			HtmlReader htmlReader = new HtmlReader(page);
			while (htmlReader.Read())
			{
				if (htmlReader.NodeType == HtmlNodeType.Element)
				{
					HtmlElement element = new HtmlElement(htmlReader.Value);
					if (element.GetElementName() == "a")
					{
						aHref = element.GetAttribute("href");
					}
				}
				else if (htmlReader.NodeType == HtmlNodeType.EndElement)
				{
					HtmlElement element = new HtmlElement(htmlReader.Value);
					if (element.GetElementName() == "a")
					{
						if (string.Compare(aInnerText, "Download", true) == 0)
						{
							if (!string.IsNullOrEmpty(aHref))
							{
								images.Add(aHref);
							}
						}
						aInnerText = string.Empty;
						aHref = string.Empty;
					}
				}
				else if (htmlReader.NodeType == HtmlNodeType.Text)
				{
					aInnerText = htmlReader.Value;
				}
			}

			return images;
		}

		int ParseNumPagesOnPage(string page)
		{
			int maxPageNum = 0;

			string divClass = string.Empty;
			bool inAnchor = false;

			HtmlReader htmlReader = new HtmlReader(page);
			while (htmlReader.Read())
			{
				if (htmlReader.NodeType == HtmlNodeType.Element)
				{
					HtmlElement element = new HtmlElement(htmlReader.Value);
					if (element.GetElementName() == "div")
					{
						divClass = element.GetAttribute("class");
					}
					if (element.GetElementName() == "a")
					{
						inAnchor = true; ;
					}
				}
				else if (htmlReader.NodeType == HtmlNodeType.EndElement)
				{
					HtmlElement element = new HtmlElement(htmlReader.Value);
					if (element.GetElementName() == "div")
					{
						divClass = string.Empty;
					}
					if (element.GetElementName() == "a")
					{
						inAnchor = false;
					}
				}
				else if (htmlReader.NodeType == HtmlNodeType.Text)
				{
					if (string.Compare(divClass, "pagination", true) == 0)
					{
						if (inAnchor)
						{
							int pageNum;
							if (int.TryParse(htmlReader.Value, out pageNum))
							{
								if (pageNum > maxPageNum)
								{
									maxPageNum = pageNum;
								}
							}
						}
					}
				}
			}

			return maxPageNum;
		}

		string GetPage(string relativeUrl, HttpConnection parentConnection, string testPage, out HttpConnection connection)
		{
			Uri uri = GetFullUri(relativeUrl, parentConnection);
			connection = _connectionManager.GetConnection(uri);

			return _httpRequester.GetPage(connection, uri, testPage);
		}

		Image GetImage(string relativeUrl, HttpConnection parentConnection)
		{
			Uri uri = GetFullUri(relativeUrl, parentConnection);
			HttpConnection connection = _connectionManager.GetConnection(uri);

			return _httpRequester.GetImage(connection, uri);
		}

		Uri GetFullUri(string relativeUrl, HttpConnection parentConnection)
		{
			Uri uri;
			if (parentConnection != null)
			{
				uri = parentConnection.ExpandPath(relativeUrl);
			}
			else
			{
				uri = new Uri(relativeUrl);
			}

			return uri;
		}
	}
}
