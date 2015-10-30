using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DMT.Modules.WallpaperChanger.Plugins.Unsplash
{
	class PhotoDetails
	{
		public string ImageUrl { get; set; }		// The actual image
		public string PhotoDetailsUrl { get; set; }	// unsplash page about the photo
		public string Photographer { get; set; }	// Photographers name
		public string PhotographerUrl { get; set; }	// unsplash page about the photographer

		public void Clear()
		{
			ImageUrl = "";
			PhotoDetailsUrl = "";
			Photographer = "";
			PhotographerUrl = "";
		}
	}
}
