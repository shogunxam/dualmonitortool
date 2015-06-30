using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DMT.Modules.WallpaperChanger.Plugins.Unsplash
{
	class PhotoDetails
	{
		public string DownloadUrl { get; set; }
		public string Photographer { get; set; }
		public string PhotographerUrl { get; set; }

		public void Clear()
		{
			DownloadUrl = "";
			Photographer = "";
			PhotographerUrl = "";
		}
	}
}
