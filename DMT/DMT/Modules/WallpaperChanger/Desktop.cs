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

using DMT.Library.Environment;
using DMT.Library.Wallpaper;
using DMT.Library.WallpaperPlugin;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace DMT.Modules.WallpaperChanger
{
	/// <summary>
	/// Represents the physical desktop.
	/// Responsible for the top level of wallpaper generation.
	/// Also needs to remember the set of images used for the last wallpaper
	/// so that it can just change the image for a single monitor.
	/// </summary>
	class Desktop
	{
		int _lastScreenUpdated = -1;
		List<ProviderImage> _currentImages = null;
		IWallpaperCompositor _compositor = null;
		Image _currentWallpaperImage = null;

		public Image CurrentWallpaperImage { get { return _currentWallpaperImage; } }
		public IWallpaperCompositor CurrentCompositor { get { return _compositor; } }

		WallpaperChangerModule _wallpaperChangerModule;
		ILocalEnvironment _localEnvironment;
		IImageRepository _imageRepository;
		IWallpaperCompositorFactory _compositorFactory;

		public Desktop(WallpaperChangerModule wallpaperChangerModule, ILocalEnvironment monitorEnvironment, IImageRepository imageRepository, IWallpaperCompositorFactory compositorFactory)
		{
			_wallpaperChangerModule = wallpaperChangerModule;
			_localEnvironment = monitorEnvironment;
			_imageRepository = imageRepository;
			_compositorFactory = compositorFactory;
		}

		/// <summary>
		/// Generate a new wallpaper
		/// </summary>
		public void UpdateWallpaper()
		{
			// Need to create a new compositor each time as the screens (count/sizes) may have changed
			_compositor = _compositorFactory.Create(_localEnvironment.Monitors);
			_compositor.DesktopRectBackColor = _wallpaperChangerModule.BackgroundColour;

			SwitchType.ImageToMonitorMapping monitorMapping = _wallpaperChangerModule.MonitorMapping;

			if (monitorMapping == SwitchType.ImageToMonitorMapping.ManyToManyInSequence)
			{
				UpdatePartialWallpaper(monitorMapping, _compositor);
			}
			else
			{
				UpdateFullWallpaper(monitorMapping, _compositor);
			}
		}

		public ProviderImage GetProviderImage(int screenIndex)
		{
			return GetRememberedImage(screenIndex);
		}

		void UpdateFullWallpaper(SwitchType.ImageToMonitorMapping monitorMapping, IWallpaperCompositor compositor)
		{
			//List<ProviderImage> generatedImages = new List<ProviderImage>();
			List<int> selectedScreens = new List<int>();
			StretchType stretchType = new StretchType(_wallpaperChangerModule.Fit);

			// will be replacing all existing images, so dispose of any remembered from before
			ForgetRememberedImages();

			if (monitorMapping == SwitchType.ImageToMonitorMapping.ManyToMany)
			{
				for (int i = 0; i < compositor.AllScreens.Count; i++)
				{
					// different image on each screen
					ProviderImage sourceImage = GetRandomImageForScreen(compositor, i);
					if (sourceImage != null && sourceImage.Image != null)
					{
						compositor.AddImage(sourceImage.Image, ScreenToList(i), stretchType.Type);
						//generatedImages.Add(sourceImage);
						RememberImage(sourceImage, i);
					}
				}
			}
			else if (monitorMapping == SwitchType.ImageToMonitorMapping.OneToMany)
			{
				// same image repeated on each monitor
				// find maximum width, height over all monitors
				Size optimumSize = new Size(0, 0);
				for (int i = 0; i < compositor.AllScreens.Count; i++)
				{
					if (compositor.AllScreens[i].ScreenRect.Width > optimumSize.Width)
					{
						optimumSize.Width = compositor.AllScreens[i].ScreenRect.Width;
					}
					if (compositor.AllScreens[i].ScreenRect.Height > optimumSize.Height)
					{
						optimumSize.Height = compositor.AllScreens[i].ScreenRect.Height;
					}
				}
				ProviderImage sourceImage = GetRandomImage(optimumSize);
				if (sourceImage != null && sourceImage.Image != null)
				{
					// add image to each monitor
					for (int i = 0; i < compositor.AllScreens.Count; i++)
					{
						compositor.AddImage(sourceImage.Image, ScreenToList(i), stretchType.Type);
					}
					//generatedImages.Add(sourceImage);
					RememberImage(sourceImage, 0);
				}
			}
			else
			{
				// default: single image covers all monitors
				Size optimumSize = compositor.DesktopRect.Size;
				ProviderImage sourceImage = GetRandomImage(optimumSize);
				if (sourceImage != null && sourceImage.Image != null)
				{
					selectedScreens = GetAllScreenIndexes(compositor);
					compositor.AddImage(sourceImage.Image, selectedScreens, stretchType.Type); 
					//generatedImages.Add(sourceImage);
					RememberImage(sourceImage, 0);
				}
			}

			//using (Image wallpaper = compositor.CreateWallpaperImage())
			//{
			//	WindowsWallpaper windowsWallpaper = new WindowsWallpaper(_localEnvironment, wallpaper, compositor.DesktopRect);
			//	windowsWallpaper.SetWallpaper(_wallpaperChangerModule.SmoothFade);
			//}
			CreateWallpaperImage();
			WindowsWallpaper windowsWallpaper = new WindowsWallpaper(_localEnvironment, _currentWallpaperImage, _compositor.DesktopRect);
			windowsWallpaper.SetWallpaper(_wallpaperChangerModule.SmoothFade);

			//// must dispose of the images
			//foreach (ProviderImage providerImage in generatedImages)
			//{
			//	providerImage.Dispose();
			//}
		}

		void UpdatePartialWallpaper(SwitchType.ImageToMonitorMapping monitorMapping, IWallpaperCompositor compositor)
		{
			StretchType stretchType = new StretchType(_wallpaperChangerModule.Fit);

			if (monitorMapping == SwitchType.ImageToMonitorMapping.ManyToManyInSequence)
			{
				int numScreens = compositor.AllScreens.Count;
				if (_lastScreenUpdated < 0 || !HaveRememberedAllImages(numScreens))
				{
					// first time through or just switched to this mode, 
					// so redo all screens
					for (int i = 0; i < numScreens; i++)
					{
						ProviderImage sourceImage = GetRandomImageForScreen(compositor, i);
						if (sourceImage != null)
						{
							RememberImage(sourceImage, i);
						}
					}
					// set so, the next screen we update will be the first
					_lastScreenUpdated = compositor.AllScreens.Count - 1;
				}
				else
				{
					// just need the one image

					_lastScreenUpdated++;
					if (numScreens > 0)
					{
						_lastScreenUpdated %= numScreens;
					}

					ProviderImage sourceImage = GetRandomImageForScreen(compositor, _lastScreenUpdated);
					if (sourceImage != null)
					{
						RememberImage(sourceImage, _lastScreenUpdated);
					}
				}

				// now add the required image for each screen to the compositor
				for (int i = 0; i < numScreens; i++)
				{
					//compositor.AddImage(_previousImages[i], ScreenToList(i), stretchType.Type);
					ProviderImage providerImage = GetRememberedImage(i);
					// Should always have an image, but jic
					if (providerImage != null)
					{
						compositor.AddImage(providerImage.Image, ScreenToList(i), stretchType.Type);
					}
				}

			}

			//using (Image wallpaper = compositor.CreateWallpaperImage())
			//{
			//	WindowsWallpaper windowsWallpaper = new WindowsWallpaper(_localEnvironment, wallpaper, compositor.DesktopRect);
			//	windowsWallpaper.SetWallpaper(_wallpaperChangerModule.SmoothFade);
			//}
			CreateWallpaperImage();
			WindowsWallpaper windowsWallpaper = new WindowsWallpaper(_localEnvironment, _currentWallpaperImage, _compositor.DesktopRect);
			windowsWallpaper.SetWallpaper(_wallpaperChangerModule.SmoothFade);
		}

		void CreateWallpaperImage()
		{
			Image wallpaper = _compositor.CreateWallpaperImage();

			if (_currentWallpaperImage != null)
			{
				_currentWallpaperImage.Dispose();
			}
			_currentWallpaperImage = wallpaper;
		}

		ProviderImage GetRandomImageForScreen(IWallpaperCompositor compositor, int screenIndex)
		{
			Size optimumSize = compositor.AllScreens[screenIndex].ScreenRect.Size;
			return GetRandomImage(optimumSize);
		}

		ProviderImage GetRandomImage(Size optimumSize)
		{
			//return ImageRepository.Instance.GetRandomImage();
			return _imageRepository.GetRandomImage(optimumSize);
		}


		List<int> ScreenToList(int screenIndex)
		{
			List<int> screenIndexes = new List<int>();
			screenIndexes.Add(screenIndex);
			return screenIndexes;
		}

		List<int> GetAllScreenIndexes(IWallpaperCompositor compositor)
		{
			List<int> allScreens = new List<int>();

			

			for (int i = 0; i < compositor.AllScreens.Count; i++)
			{
				allScreens.Add(i);
			}

			return allScreens;
		}

		void RememberImage(ProviderImage providerImage, int screenIndex)
		{
			// controller does this for us
			if (_currentImages == null)
			{
				_currentImages = new List<ProviderImage>();
			}

			// grow the array if needed
			while (_currentImages.Count <= screenIndex)
			{
				_currentImages.Add(null);
			}

			if (_currentImages[screenIndex] != null)
			{
				_currentImages[screenIndex].Dispose();
			}
			_currentImages[screenIndex] = providerImage;
		}

		void ForgetRememberedImages()
		{
			if (_currentImages != null)
			{
				foreach (ProviderImage providerImage in _currentImages)
				{
					if (providerImage != null)
					{
						providerImage.Dispose();
					}
				}
				_currentImages = null;
			}
		}

		ProviderImage GetRememberedImage(int screenIndex)
		{
			ProviderImage ret = null;

			if (_currentImages != null)
			{
				if (screenIndex < _currentImages.Count)
				{
					ret = _currentImages[screenIndex];
				}
			}

			return ret;
		}

		bool HaveRememberedAllImages(int numMonitors)
		{
			if (_currentImages != null)
			{
				// make sure we have a slot for each active monitor
				if (_currentImages.Count < numMonitors)
				{
					return false;
				}
				// check none of these are empty slots
				for (int n = 0; n < numMonitors; n++)
				{
					if (_currentImages[n] == null)
					{
						return false;
					}
				}
				// have an active slot for each active monitor
				return true;
			}

			return false;
		}
	}
}
