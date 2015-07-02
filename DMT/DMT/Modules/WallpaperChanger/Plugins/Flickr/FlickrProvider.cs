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

using DMT.Library.Html;
using DMT.Library.Settings;
using DMT.Library.Utils;
using DMT.Library.WallpaperPlugin;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace DMT.Modules.WallpaperChanger.Plugins.Flickr
{
	class FlickrProvider : IImageProvider
	{
		FlickrConfig _config;
		StringSetting _apiKeySetting;

		public string ApiKey
		{
			get { return _apiKeySetting.Value; }
			set { _apiKeySetting.Value = value; }
		}

		const string FlickrHome = "www.flickr.com";

		// these relate to the provider type
		public string ProviderName { get { return FlickrPlugin.PluginName; } }
		public Image ProviderImage { get { return FlickrPlugin.PluginImage; } }
		public string Version { get { return FlickrPlugin.PluginVersion; } }

		// these relate to the provider instance
		public string Description { get { return _config.Description; } }
		public int Weight { get { return _config.Weight; } }

		public Dictionary<string, string> Config { get { return _config.ToDictionary(); } }

		static Random _random = new Random();

		HttpConnectionManager _connectionManager = new HttpConnectionManager();

		HttpRequester _httpRequester;

		public FlickrProvider(Dictionary<string, string> config, StringSetting apiKeySetting)
		{
			_config = new FlickrConfig(config);
			_apiKeySetting = apiKeySetting;

			_httpRequester = new HttpRequester();
		}

		public Dictionary<string, string> ShowUserOptions()
		{
			FlickrForm dlg = new FlickrForm(_config);
			dlg.textBoxApiKey.Text = _apiKeySetting.Value;
			if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				_config = dlg.GetConfig();
				_apiKeySetting.Value = dlg.textBoxApiKey.Text;
				return _config.ToDictionary();
			}

			// return null to indicate options have not been updated
			return null;
		}

		public ProviderImage GetRandomImage(Size optimumSize)
		{
			ProviderImage providerImage = null;

			FlickrQuery searchQuery = GetSearchQuery();

			string hitsPageData = GetPage(searchQuery, "flickrHits1");
			List<string> photoIds = ParseHitsPageForPhotos(hitsPageData);

			// choose a photoid at random
			if (photoIds.Count > 0)
			{
				int rand = _random.Next(0, photoIds.Count);  // (inclusive, exclusive)
				string photoId = photoIds[rand];

				// get available sizes
				FlickrQuery sizesQuery = GetSizesQuery(photoId);
				string sizesPageData = GetPage(sizesQuery, "flickrSizes");
				string url = ParseSizesForBest(sizesPageData, optimumSize);

				providerImage = new ProviderImage(GetImage(url));
				providerImage.Provider = ProviderName;
				providerImage.ProviderUrl = FlickrHome;

				// get details of the image
				FlickrQuery photoInfoQuery = GetPhotoInfoQuery(photoId);
				string photoInfoPageData = GetPage(photoInfoQuery, "flickrPhotoInfo");
				string photographerUserId;
				ParsePhotoInfo(photoInfoPageData, providerImage, out photographerUserId);

				// get details of the photographer - already have name, but want their page
				FlickrQuery peopleInfoQuery = GetPeopleInfoQuery(photographerUserId);
				string peopleInfoPageData = GetPage(peopleInfoQuery, "flickrPeopleInfo");
				ParsePeopleInfo(peopleInfoPageData, providerImage);
			}

			return providerImage;
		}

		string GetPage(FlickrQuery query, string testPage)
		{
			Uri uri = GetUri(query);
			HttpConnection connection = _connectionManager .GetConnection(uri);

			return _httpRequester.GetPage(connection, uri, testPage);
		}

		Image GetImage(string url)
		{
			Uri uri = new Uri(url);
			HttpConnection connection = _connectionManager.GetConnection(uri);
			return _httpRequester.GetImage(connection, uri);
		}

		Uri GetUri(FlickrQuery query)
		{
			UriBuilder uriBuilder = new UriBuilder("https://api.flickr.com/services/rest/");
			uriBuilder.Query = query.ToString();
			return uriBuilder.Uri;
		}

		FlickrQuery GetSearchQuery(int pageNum = -1)
		{
			// flickr wants us to use .getRecent for parameterless searches
			if (string.IsNullOrWhiteSpace(_config.UserId)
				&& string.IsNullOrWhiteSpace(_config.Tags)
				&& string.IsNullOrWhiteSpace(_config.Text)
				&& string.IsNullOrWhiteSpace(_config.GroupId))
			{
				return new FlickrQuery("flickr.photos.getRecent", ApiKey);
			}

			FlickrQuery query = new FlickrQuery("flickr.photos.search", ApiKey);

			if (!string.IsNullOrEmpty(_config.UserId))
			{
				query.Add("user_id", _config.UserId);
			}

			if (!string.IsNullOrEmpty(_config.Tags))
			{
				query.Add("tags", _config.Tags);
				if (_config.TagModeAll)
				{
					query.Add("tag_mode", "all");
				}
			}

			if (!string.IsNullOrEmpty(_config.Text))
			{
				query.Add("text", _config.Text);
			}

			if (!string.IsNullOrEmpty(_config.GroupId))
			{
				query.Add("group_id", _config.GroupId);
			}

			return query;
		}

		FlickrQuery GetSizesQuery(string photoId)
		{
			FlickrQuery query = new FlickrQuery("flickr.photos.getSizes", ApiKey);
			query.Add("photo_id", photoId);
			return query;
		}

		List<string> ParseHitsPageForPhotos(string hitsPageData)
		{
			// just collect the photoIds for now
			List<string> photoIds = new List<string>();

			XmlReader xmlReader = XmlReader.Create(new StringReader(hitsPageData));
			while (xmlReader.Read())
			{
				if (xmlReader.NodeType == XmlNodeType.Element)
				{
					if (xmlReader.LocalName == "photo")
					{
						string photoId = null;
						while (xmlReader.MoveToNextAttribute())
						{
							if (xmlReader.Name == "id")
							{
								photoId = xmlReader.Value;
							}
						}
						if (!string.IsNullOrEmpty(photoId))
						{
							photoIds.Add(photoId);
						}
					}
				}
			}

			return photoIds;
		}

		string ParseSizesForBest(string sizesPageData, Size optimumSize)
		{
			// want the smallest size that is at least optimimSize
			// or if they are all samller, want the biggest size

			int bestWidth = 0;
			int bestHeight = 0;
			string bestUrl = null;

			XmlReader xmlReader = XmlReader.Create(new StringReader(sizesPageData));
			while (xmlReader.Read())
			{
				if (xmlReader.NodeType == XmlNodeType.Element)
				{
					if (xmlReader.LocalName == "size")
					{
						string label = "";
						int width = 0;
						int height = 0;
						string source = null;
						while (xmlReader.MoveToNextAttribute())
						{
							if (xmlReader.Name == "label")
							{
								label = xmlReader.Value;
							}
							else if (xmlReader.Name == "width")
							{
								width = StringUtils.ToInt(xmlReader.Value);
							}
							else if (xmlReader.Name == "height")
							{
								height = StringUtils.ToInt(xmlReader.Value);
							}
							else if (xmlReader.Name == "source")
							{
								source = xmlReader.Value;
							}
						}
						// don't want original, as this doesn't reflect any possible rotation of the image
						if (string.Compare(label, "Original", true) != 0)
						{
							if (IsBetterSize(width, height, optimumSize, bestWidth, bestHeight))
							{
								bestWidth = width;
								bestHeight = height;
								bestUrl = source;
							}
						}
					}
				}
			}

			return bestUrl;
		}

		bool IsBetterSize(int width, int height, Size optimumSize, int oldWidth, int oldHeight)
		{
			if (IsAtLeastOptimum(oldWidth, oldHeight, optimumSize))
			{
				// already have a suitable size
				// choose new size only if is also larger than optimum, but uses less pixels than current
				if (IsAtLeastOptimum(width, height, optimumSize))
				{
					if (width * height < oldWidth * oldHeight)
					{
						return true;
					}
				}
			}
			else
			{
				// choose new size if it has more pixels than current
				if (width * height > oldWidth + oldHeight)
				{
					return true;
				}
			}

			return false;
		}

		bool IsAtLeastOptimum(int width, int height, Size optimumSize)
		{
			return width >= optimumSize.Width && height >= optimumSize.Height;
		}

		FlickrQuery GetPhotoInfoQuery(string photoId)
		{
			FlickrQuery query = new FlickrQuery("flickr.photos.getInfo", ApiKey);
			query.Add("photo_id", photoId);
			return query;
		}

		void ParsePhotoInfo(string photoInfoPageData, ProviderImage providerImage, out string userId)
		{
			userId = "";

			XmlReader xmlReader = XmlReader.Create(new StringReader(photoInfoPageData));
			while (xmlReader.Read())
			{
				if (xmlReader.NodeType == XmlNodeType.Element)
				{
					if (xmlReader.LocalName == "owner")
					{
						string userName = null;
						string realName = null;
						while (xmlReader.MoveToNextAttribute())
						{
							if (xmlReader.Name == "nsid")
							{
								userId = xmlReader.Value;
							}
							else if (xmlReader.Name == "username")
							{
								userName = xmlReader.Value;
							}
							else if (xmlReader.Name == "realname")
							{
								realName = xmlReader.Value;
							}
						}
						providerImage.Photographer = ChooseName(userName, realName);
					}
					else if (xmlReader.LocalName == "title")
					{
						providerImage.MoreInfo = ParseSimpleEntity(xmlReader);
					}
					else if (xmlReader.LocalName == "url")
					{
						string urlType = "";
						while (xmlReader.MoveToNextAttribute())
						{
							if (xmlReader.Name == "type")
							{
								urlType = xmlReader.Value;
							}
						}
						if (string.Compare(urlType, "photopage", true) == 0)
						{
							providerImage.SourceUrl = ParseSimpleEntity(xmlReader);
							providerImage.Source = providerImage.SourceUrl;
						}
					}
				}
			}
		}

		string ChooseName(string userName, string realName)
		{
			// let realName override userName
			string name = "";

			if (!string.IsNullOrEmpty(userName))
			{
				name = userName;
			}
			if (!string.IsNullOrEmpty(realName))
			{
				name = realName;
			}

			return name;
		}

		FlickrQuery GetPeopleInfoQuery(string userId)
		{
			FlickrQuery query = new FlickrQuery("flickr.people.getInfo", ApiKey);
			query.Add("user_id", userId);
			return query;
		}

		void ParsePeopleInfo(string peopleInfoPageData, ProviderImage providerImage)
		{
			XmlReader xmlReader = XmlReader.Create(new StringReader(peopleInfoPageData));
			while (xmlReader.Read())
			{
				if (xmlReader.NodeType == XmlNodeType.Element)
				{
					if (xmlReader.LocalName == "profileurl")
					{
						providerImage.PhotographerUrl = ParseSimpleEntity(xmlReader);
					}
				}
			}
		}

		string ParseSimpleEntity(XmlReader xmlReader)
		{
			string entityValue = "";

			if (!xmlReader.IsEmptyElement)
			{
				while (xmlReader.Read())
				{
					if (xmlReader.NodeType == XmlNodeType.Element)
					{
						SkipEntity(xmlReader, xmlReader.LocalName);
					}
					else if (xmlReader.NodeType == XmlNodeType.EndElement)
					{
						break;
					}
					else if (xmlReader.NodeType == XmlNodeType.Text)
					{
						entityValue += xmlReader.Value;
					}
				}
			}

			return entityValue;

		}

		void SkipEntity(XmlReader xmlReader, string entity)
		{
			if (!xmlReader.IsEmptyElement)
			{
				while (xmlReader.Read())
				{
					if (xmlReader.NodeType == XmlNodeType.Element)
					{
						SkipEntity(xmlReader, xmlReader.LocalName);
					}
					else if (xmlReader.NodeType == XmlNodeType.EndElement)
					{
						if (xmlReader.LocalName == entity)
						{
							break;
						}
					}
				}
			}
		}
	}
}
