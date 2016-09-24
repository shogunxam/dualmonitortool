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

namespace DMT.Modules.WallpaperChanger.Plugins.Unsplash
{
	using System;
	using System.Collections.Generic;
	using System.Drawing;
	using System.Drawing.Drawing2D;
	using System.IO;
	using System.Linq;
	using System.Net;
	using System.Text;
	using System.Threading.Tasks;

	using DMT.Library.Html;
	using DMT.Library.Utils;
	using DMT.Library.WallpaperPlugin;

	/// <summary>
	/// An instance of a provider from the unsplash plugin
	/// </summary>
	class WebScrapeProvider : IImageProvider
	{
		WebScrapeConfig _config;
		HttpConnectionManager _connectionManager;
		HttpRequester _httpRequester;

		/// <summary>
		/// Initialises a new instance of the <see cref="WebScrapeProvider" /> class.
		/// </summary>
		/// <param name="config">Configuration for unsplash scraper</param>
		public WebScrapeProvider(Dictionary<string, string> config)
		{
			_config = new WebScrapeConfig(config);

			_connectionManager = new HttpConnectionManager();
			_httpRequester = new HttpRequester(_connectionManager);
		}

		/// <summary>
		/// Gets the provider name - same for all instances of this class
		/// </summary>
		public string ProviderName 
		{ 
			get 
			{ 
				return WebScrapePlugin.PluginName; 
			} 
		}

		/// <summary>
		/// Gets the provider image - same for all instances of this class
		/// </summary>
		public Image ProviderImage 
		{ 
			get 
			{ 
				return WebScrapePlugin.PluginImage; 
			} 
		}

		/// <summary>
		/// Gets the provider version - same for all instances of this class
		/// </summary>
		public string Version 
		{ 
			get 
			{ 
				return WebScrapePlugin.PluginVersion; 
			} 
		}

		/// <summary>
		/// Gets the description for this instance of the provider
		/// </summary>
		public string Description 
		{ 
			get 
			{ 
				return _config.Description; 
			} 
		}

		/// <summary>
		/// Gets the weight for this instance of the provider
		/// </summary>
		public int Weight 
		{ 
			get 
			{ 
				return _config.Weight; 
			} 
		}

		/// <summary>
		/// Gets the configuration 
		/// </summary>
		public Dictionary<string, string> Config 
		{ 
			get 
			{ 
				return _config.ToDictionary(); 
			} 
		}

		/// <summary>
		/// Allows the user to update the configuration 
		/// </summary>
		/// <returns>New configuration, or null if no changes</returns>
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

		// Unsplash has changed the interface again
		// There is now an API, but (without further investigation) it doesn't look suitable for an open source application
		// There is a very simple interface at https://source.unsplash.com/ which we can use for the time being
#if OLD_CODE
		/// <summary>
		/// Returns a random image
		/// </summary>
		/// <param name="optimumSize">Optimum image size</param>
		/// <param name="screenIndex">Screen index image is for</param>
		/// <returns>Random image</returns>
		public ProviderImage GetRandomImage(Size optimumSize, int screenIndex)
		{
			ProviderImage providerImage = null;

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

			IList<PhotoDetails> imageUrls;
			imageUrls = ParseImagesOnPage(randomPage);

			if (imageUrls.Count > 0)
			{
				// choose a random image
				int index = RNG.Next(imageUrls.Count);
				PhotoDetails photoDetails = imageUrls[index];

				// expand the links before performing any requests
				string photographerUrl = GetFullUrl(photoDetails.PhotographerUrl, randomPageConnection);
				string photoUrl = GetFullUrl(photoDetails.PhotoDetailsUrl, randomPageConnection);

				string imageUrl = imageUrls[index].ImageUrl;
				imageUrl = CleanImageUrl(imageUrl, optimumSize);
				Image image = GetImage(imageUrl, randomPageConnection);
				if (image != null)
				{
					providerImage = new ProviderImage(image);
					providerImage.Provider = ProviderName;
					providerImage.ProviderUrl = "www.unsplash.com";

					// for image source, return url that responded in case of 302's
					Uri uri = _httpRequester.LastResponseUri;
					providerImage.Source = uri.ToString();
					providerImage.SourceUrl = photoUrl;

					providerImage.Photographer = photoDetails.Photographer;
					providerImage.PhotographerUrl = photographerUrl;
				}
			}

			return providerImage;
		}

		string CleanImageUrl(string imageUrl, Size optimumSize)
		{
			// The url is HTML encoded, so first undo this
			string newUrl = imageUrl.Replace("&amp;", "&");

			// default width is 1080
			// change this to match required width
			string requiredWidth = string.Format("w={0}", optimumSize.Width);
			newUrl = newUrl.Replace("w=1080", requiredWidth);

			return newUrl;
		}

		string GetRandomPageUrl(string homePage)
		{
			string url = null;

			// home page has a navigation bar
			int maxPages = ParseNumPagesOnPage(homePage);
			if (maxPages > 0)
			{
				int randomPage = RNG.Next(1, maxPages + 1); // (inclusive, exclusive)

				// make sure we don't ask for the first page again
				if (randomPage > 1)
				{
					url = string.Format("/?page={0}", randomPage);
				}
			}

			return url;
		}


		IList<PhotoDetails> ParseImagesOnPage(string page)
		{
			List<PhotoDetails> images = new List<PhotoDetails>();

			string aHref = string.Empty;
			string aInnerText = string.Empty;
			string photoDetailsUrl = string.Empty;
			string photoUrl = string.Empty;
			bool inH2 = false;
			bool inPhotoA = false;	// within <a title="View the photo..."> </a>

			HtmlReader htmlReader = new HtmlReader(page);
			while (htmlReader.Read())
			{
				if (htmlReader.NodeType == HtmlNodeType.Element)
				{
					HtmlElement element = new HtmlElement(htmlReader.Value);
					if (element.GetElementName() == "h2")
					{
						inH2 = true;
					}
					else if (element.GetElementName() == "a")
					{
						aHref = element.GetAttribute("href");
						string title = element.GetAttribute("title");
						if (title != null && title.StartsWith("View the photo By"))
						{
							photoDetailsUrl = aHref;
							inPhotoA = true;
						}
					}
					else if (element.GetElementName() == "img")
					{
						if (inPhotoA)
						{
							photoUrl = element.GetAttribute("src");
						}
					}
				}
				else if (htmlReader.NodeType == HtmlNodeType.EndElement)
				{
					HtmlElement element = new HtmlElement(htmlReader.Value);
					if (element.GetElementName() == "a")
					{
						if (inH2)
						{
							// h2 now only used around author
							if (!string.IsNullOrEmpty(photoDetailsUrl))
							{
								// should now have all the details we need
								PhotoDetails photoDetails = new PhotoDetails();
								photoDetails.Photographer = aInnerText;
								if (!string.IsNullOrEmpty(aHref))
								{
									photoDetails.PhotographerUrl = aHref;
								}

								photoDetails.PhotoDetailsUrl = photoDetailsUrl;
								photoDetails.ImageUrl = photoUrl;
								images.Add(photoDetails);
								photoDetailsUrl = string.Empty;
							}
						}

						aInnerText = string.Empty;
						aHref = string.Empty;
						inPhotoA = false;
					}
					else if (element.GetElementName() == "h2")
					{
						inH2 = false;
					}
				}
				else if (htmlReader.NodeType == HtmlNodeType.Text)
				{
					// NOTE: only remembers last text string
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
						inAnchor = true;
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
#else
		/// <summary>
		/// Returns a random image
		/// </summary>
		/// <param name="optimumSize">Optimum image size</param>
		/// <param name="screenIndex">Screen index image is for</param>
		/// <returns>Random image</returns>
		public ProviderImage GetRandomImage(Size optimumSize, int screenIndex)
		{
			ProviderImage providerImage = null;

			string url = "https://source.unsplash.com/";

			url += "random";

			// we can also request images by category etc.
			// TODO: add this in the next release

			// we can also add required size
			// TODO: again this can wait for the next release

			Image image = GetImage(url, null);
			if (image != null)
			{
				providerImage = new ProviderImage(image);
				providerImage.Provider = ProviderName;
				providerImage.ProviderUrl = "www.unsplash.com";

				// for image source, return url that responded in case of 302's
				Uri uri = _httpRequester.LastResponseUri;
				providerImage.Source = uri.ToString();
				providerImage.SourceUrl = uri.ToString();

				// we don't get any photographer details using source.unsplash
				//providerImage.Photographer = photoDetails.Photographer;
				//providerImage.PhotographerUrl = photographerUrl;
			}

			return providerImage;
		}

#endif

		string GetPage(string relativeUrl, HttpConnection parentConnection, string testPage, out HttpConnection repliedConnection)
		{
			Uri uri = GetFullUri(relativeUrl, parentConnection);

			return _httpRequester.GetPage(uri, testPage, out repliedConnection);
		}

		Image GetImage(string relativeUrl, HttpConnection parentConnection)
		{
			Uri uri = GetFullUri(relativeUrl, parentConnection);

			return _httpRequester.GetImage(uri);
		}

		string GetFullUrl(string relativeUrl, HttpConnection parentConnection)
		{
			if (string.IsNullOrEmpty(relativeUrl))
			{
				return string.Empty;
			}

			Uri uri = GetFullUri(relativeUrl, parentConnection);
			return uri.ToString();
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
