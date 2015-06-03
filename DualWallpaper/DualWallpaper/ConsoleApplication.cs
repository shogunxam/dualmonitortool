using DMT.Library.Environment;
using DMT.Library.Wallpaper;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace DualWallpaper
{
	class ConsoleApplication
	{
		Options _options;

		public ConsoleApplication(Options options)
		{
			_options = options;
		}

		public void Run()
		{
			if (_options.Errors.Count > 0)
			{
				foreach (string error in _options.Errors)
				{
					Console.WriteLine(error);
				}
				ShowUsage();
			}
			else if (_options.ShowUsage)
			{
				ShowUsage();
			}
			else if (_options.ShowVersion)
			{
				ShowVersion();
			}
			else
			{
				// thanks to CKolumbus who supplied the initial version of this code
				Controller controller = new Controller();
				List<int> selectedScreens = new List<int>();
				string sourceFilename = _options.SourceFilename;

				// This is the current wallpaper laid out the same way that
				// the screens are laid out
				Image wallpaper = null;

				List<int> screenIndexes = new List<int>();

				int c = controller.AllScreens.Count;
				for (int i = 0; i < c; i++) screenIndexes.Add(i);

				Image image;
				try
				{
					image = Bitmap.FromFile(sourceFilename);
				}
				catch (System.IO.FileNotFoundException)
				{
					Console.WriteLine(string.Format("File \"{0}\" not found", sourceFilename));
					return;
				}
				catch (Exception ex)
				{
					Console.WriteLine(ex.Message);
					return;
				}
				Stretch stretchType = new Stretch(_options.StretchType);

				//controller.SetAllScreensActive();
				controller.SetActiveScreens(screenIndexes);
				controller.AddImage(image, stretchType.Type);

				wallpaper = controller.CreateWallpaperImage();
				ILocalEnvironment localEnvironment = new LocalEnvironment();
				WindowsWallpaper windowsWallpaper = new WindowsWallpaper(localEnvironment, wallpaper, controller.DesktopRect);
				windowsWallpaper.SetWallpaper();
			}
		}

		void ShowUsage()
		{
			Console.WriteLine("Usage: DualWallpaper [options] <input image>");
			Console.WriteLine("Sets the wallpaper using the input image");
			Console.WriteLine("Options:");
			Console.WriteLine("  -?   show usage");
			Console.WriteLine("  -v   show version");
			Console.WriteLine("       At most only one of the following shold be specified:");
			Console.WriteLine("  -so  over stretch image to fit, maintaining aspect ratio (default)");
			Console.WriteLine("  -su  under stretch image to fit, maintaining aspect ratio");
			Console.WriteLine("  -sc  center image with no stretching");
			Console.WriteLine("  -sf  stretch to fit in both directions");
		}

		void ShowVersion()
		{
			System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
			System.Diagnostics.FileVersionInfo fileVersionInfo = System.Diagnostics.FileVersionInfo.GetVersionInfo(assembly.Location);
			string version = fileVersionInfo.ProductVersion;
			Console.WriteLine(string.Format("DualWallpaper {0}", version));
		}
	}

}
