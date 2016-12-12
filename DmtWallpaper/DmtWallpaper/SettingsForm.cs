using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace DmtWallpaper
{
	public partial class SettingsForm : Form
	{
		public SettingsForm()
		{
			InitializeComponent();

			labelProductName.Text = string.Format("{0}  -  Version {1}", AssemblyTitle, AssemblyVersion);
			labelCopyright.Text = AssemblyCopyright;
			labelLicense.Text = DmtWallpaper.Properties.Resources.License;

			string wallpaperFilename = WallpaperFilenameHelper.GetWallpaperFilename(Handle);

			if (wallpaperFilename == null)
			{
				textBoxFilename.Text = "DMT is either not running or not generating wallpaper.";
			}
			else
			{
				textBoxFilename.Text = wallpaperFilename;
			}
		}

		/// <summary>
		/// Gets our name ("DMT Wallpaper Screen Saver")
		/// </summary>
		public string AssemblyTitle
		{
			get
			{
				return  DmtWallpaper.Properties.Resources.MyTitle;
			}
		}

		/// <summary>
		/// Gets our current version as a a string
		/// </summary>
		public string AssemblyVersion
		{
			get
			{
				return Assembly.GetExecutingAssembly().GetName().Version.ToString();
			}
		}

		/// <summary>
		/// Gets the copyright notice
		/// </summary>
		public string AssemblyCopyright
		{
			get
			{
				// Get all Copyright attributes on this assembly
				object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);

				// If there aren't any Copyright attributes, return an empty string
				if (attributes.Length == 0)
				{
					return string.Empty;
				}

				// If there is a Copyright attribute, return its value
				return ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
			}
		}

#if QUERY_DMT_USING_COPYDATA
		protected override void WndProc(ref Message m)
		{
			if (m.Msg == NativeMethods.WM_COPYDATA)
			{
				NativeMethods.COPYDATASTRUCT cds = (NativeMethods.COPYDATASTRUCT)m.GetLParam(typeof(NativeMethods.COPYDATASTRUCT));
				WallpaperFilenameHelper.HandleCopyData(cds);
			}
			else
			{
				base.WndProc(ref m);
			}
		}
#endif

		private void buttonClose_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.OK;
			Close();
		}
	}
}
