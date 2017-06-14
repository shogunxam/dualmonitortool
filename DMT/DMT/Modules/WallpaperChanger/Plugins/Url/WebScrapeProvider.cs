#region copyright
// This file is part of Dual Monitor Tools which is a set of tools to assist
// users with multiple monitor setups.
// Copyright (C) 2015-2016  Gerald Evans
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

namespace DMT.Modules.WallpaperChanger.Plugins.Url
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
	/// An instance of a provider from the Url plugin
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
		/// Gets the enabled state for this instance of the provider
		/// </summary>
		public bool Enabled
		{
			get
			{
				return _config.Enabled;
			}
			set
			{
				_config.Enabled = value;
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

		/// <summary>
		/// Returns a random image
		/// </summary>
		/// <param name="optimumSize">Optimum image size</param>
		/// <param name="screenIndex">Screen index image is for</param>
		/// <returns>Random image</returns>
		public ProviderImage GetRandomImage(Size optimumSize, int screenIndex)
		{
			ProviderImage providerImage = null;

			string url = _config.Url;

			if (_config.AllowEscapes)
			{
				url = UnEscapeUrl(url, optimumSize);
			}

			Image image = GetImage(url, null);
			if (image != null)
			{
				providerImage = new ProviderImage(image);
				providerImage.Provider = ProviderName;
				providerImage.ProviderUrl = url;

				// for image source, return url that responded in case of 302's
				Uri uri = _httpRequester.LastResponseUri;
				providerImage.Source = uri.ToString();
				providerImage.SourceUrl = uri.ToString();
			}

			return providerImage;
		}

		string UnEscapeUrl(string url, Size optimumSize)
		{
			const char EscapeChar = '%';
			StringBuilder sb = new StringBuilder();

			bool escaped = false;
			foreach (char ch in url)
			{
				if (escaped)
				{
					switch (ch)
					{
						case EscapeChar:
							// double escape char puts out single escape char
							sb.Append(EscapeChar);
							break;

						case 'W':
							sb.Append(optimumSize.Width.ToString());
							break;

						case 'H':
							sb.Append(optimumSize.Height.ToString());
							break;

						default:
							// escape not recognised - add original text to output
							sb.Append(EscapeChar);
							sb.Append(ch);
							break;
					}

					escaped = false;
				}
				else
				{
					if (ch == EscapeChar)
					{
						escaped = true;
					}
					else
					{
						sb.Append(ch);
					}
				}
			}
			return sb.ToString();
		}

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
