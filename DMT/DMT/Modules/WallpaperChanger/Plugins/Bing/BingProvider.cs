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

namespace DMT.Modules.WallpaperChanger.Plugins.Bing
{

	using DMT.Library.Html;
	using DMT.Library.WallpaperPlugin;
	using System;
	using System.Collections.Generic;
	using System.Drawing;
	using System.IO;
	//using System.Net.Http;
	using System.Runtime.Serialization;
	using System.Runtime.Serialization.Json;
	using System.Text;
	using System.Threading.Tasks;

	/// <summary>
	/// An instance of a provider from the unsplash plugin
	/// </summary>
	class BingProvider : IImageProvider
	{
		const string BaseUrl = "https://www.bing.com";

		BingConfig _config;
		HttpConnectionManager _connectionManager;
		HttpRequester _httpRequester;

		// these relate to the provider type
		public string ProviderName { get { return BingPlugin.PluginName; } }
		public Image ProviderImage { get { return BingPlugin.PluginImage; } }
		public string Version { get { return BingPlugin.PluginVersion; } }

		// these relate to the provider instance
		public string Description { get { return _config.Description; } }
		public int Weight { get { return _config.Weight; } }

		public Dictionary<string, string> Config { get { return _config.ToDictionary(); } }


		public BingProvider(Dictionary<string, string> config)
		{
			_config = new BingConfig(config);

			_connectionManager = new HttpConnectionManager();
			_httpRequester = new HttpRequester(_connectionManager);
		}

		public Dictionary<string, string> ShowUserOptions()
		{
			BingForm dlg = new BingForm(_config);
			if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				_config = dlg.GetConfig();
				return _config.ToDictionary();
			}

			// return null to indicate options have not been updated
			return null;
		}

		public ProviderImage GetRandomImage(Size optimumSize, int screenIndex)
		{
			ProviderImage providerImage = null;
#if ORIGINAL_IMPLEMENTATION
			BingImage bingImg = GetImage().Result;
#else
			BingImage bingImg = GetImage();
#endif
			if (bingImg != null && bingImg.Img != null)
			{
				{
					providerImage = new ProviderImage(bingImg.Img);
					providerImage.Provider = ProviderName;
					providerImage.ProviderUrl = bingImg.UrlBase;

					// for image source, return url that responded in case of 302's
					providerImage.Source = bingImg.Url;
					providerImage.SourceUrl = bingImg.Url;
					char[] delimires = { '(', ')' };
					string[] values = bingImg.Copyright.Split(delimires);

					//Copyright and image details
					providerImage.MoreInfo = values[0];
					providerImage.Photographer = values[1];
					providerImage.MoreInfoUrl = bingImg.CopyrightLink;
				}
			}
			return providerImage;
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

#if ORIGINAL_IMPLEMENTATION
		private async Task<BingImage> GetImage()
		{
			string baseUri = "https://www.bing.com";
			using (var client = new HttpClient())
			{
				using (var jsonStream = await client.GetStreamAsync("http://www.bing.com/HPImageArchive.aspx?format=js&idx=0&n=1&mkt=" + _config.Market).ConfigureAwait(continueOnCapturedContext: false))
				{
					var ser = new DataContractJsonSerializer(typeof(Result));
					var res = (Result)ser.ReadObject(jsonStream);
					using (var imgStream = await client.GetStreamAsync(new Uri(baseUri + res.images[0].URL)))
					{
						return new BingImage(Image.FromStream(imgStream), res.images[0].Copyright, res.images[0].CopyrightLink, baseUri, baseUri + res.images[0].URL);
					}
				}
			}
		}
#else
		// implementation that only needs .NET 4.0 so can run on XP
		BingImage GetImage()
		{
			string url = "http://www.bing.com/HPImageArchive.aspx?format=js&idx=0&n=1&mkt=" + _config.Market;
			Uri uri = new Uri(url);
			HttpConnection repliedConnection;
			string jsonString = _httpRequester.GetPage(uri, "dummyResult", out repliedConnection);
			
			using (Stream jsonStream = new MemoryStream(Encoding.UTF8.GetBytes(jsonString)))
			{
				DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(Result));
				Result res = (Result)ser.ReadObject(jsonStream);
				if (res != null && res.images != null && res.images.Length > 0 && res.images[0] != null)
				{
					Uri imageUri = new Uri(new Uri(BaseUrl), res.images[0].URL);
					Image image = _httpRequester.GetImage(imageUri);
					return new BingImage(image, res.images[0].Copyright, res.images[0].CopyrightLink, BaseUrl, BaseUrl + res.images[0].URL);
				}
			}

			return null;
		}
#endif

		[DataContract]
		private class Result
		{
			[DataMember(Name = "images")]
			public ResultImage[] images { get; set; }
		}

		[DataContract]
		private class ResultImage
		{
			[DataMember(Name = "enddate")]
			public string EndDate { get; set; }
			[DataMember(Name = "url")]
			public string URL { get; set; }
			[DataMember(Name = "urlbase")]
			public string URLBase { get; set; }
			[DataMember(Name = "copyright")]
			public string Copyright { get; set; }
			[DataMember(Name = "copyrightlink")]
			public string CopyrightLink { get; set; }
		}
	}

	public class BingImage
	{
		public BingImage(Image img, string copyright, string copyrightLink, string urlBase = null, string url = null)
		{
			Img = img;
			Copyright = copyright;
			CopyrightLink = copyrightLink;
			Url = url;
			UrlBase = urlBase;
		}
		public Image Img { get; set; }
		public string Copyright { get; set; }
		public string CopyrightLink { get; set; }
		public string Url;
		public string UrlBase;
	}
}
