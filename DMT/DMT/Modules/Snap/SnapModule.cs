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

namespace DMT.Modules.Snap
{
	using System;
	using System.Collections.Generic;
	using System.Drawing;
	using System.Drawing.Imaging;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;
	using System.Windows.Forms;

	using DMT.Library;
	using DMT.Library.GuiUtils;
	using DMT.Library.HotKeys;
	using DMT.Library.Logging;
	using DMT.Library.PInvoke;
	using DMT.Library.Settings;
	using DMT.Resources;

	/// <summary>
	/// Snap module
	/// </summary>
	class SnapModule : Module
	{
		const int DefaultMaxSnaps = 8;
		const bool DefaultAutoShowSnap = true;
		const bool DefaultExpandSnap = false;
		const bool DefaultShrinkSnap = false;
		const bool DefaultMaintainAspectRatio = true;

		ISettingsService _settingsService;
		ILogger _logger;
		AppForm _appForm;
		SnapForm _snapForm = null;
		ToolStripMenuItem _showSnapToolStripMenuItem;

		/// <summary>
		/// Initialises a new instance of the <see cref="SnapModule" /> class.
		/// </summary>
		/// <param name="settingsService">Settings repository</param>
		/// <param name="hotKeyService">Service for registering hot keys</param>
		/// <param name="logger">Application logger</param>
		/// <param name="appForm">Application (hidden) window</param>
		public SnapModule(ISettingsService settingsService, IHotKeyService hotKeyService, ILogger logger, AppForm appForm)
			: base(hotKeyService)
		{
			_settingsService = settingsService;
			_logger = logger;
			_appForm = appForm;

			ModuleName = "Snap";
		}

		/// <summary>
		/// Gets the snap history
		/// </summary>
		public SnapHistory SnapHistory { get; private set; }

		/// <summary>
		/// Gets the hot key controller for the 'Take screen snap' hot key
		/// </summary>
		public HotKeyController TakeScreenSnapHotKeyController { get; private set; }

		/// <summary>
		/// Gets the hot key controller for the 'Take window snap' hot key
		/// </summary>
		public HotKeyController TakeWindowSnapHotKeyController { get; private set; }

		/// <summary>
		/// Gets the hot key controller for the 'Show snap window' hot key
		/// </summary>
		public HotKeyController ShowSnapHotKeyController { get; private set; }

		/// <summary>
		/// Gets or sets the maximum number of snaps to remember
		/// </summary>
		public int MaxSnaps
		{
			get { return MaxSnapsSetting.Value; }
			set { MaxSnapsSetting.Value = value; }
		}

		/// <summary>
		/// Gets or sets a value indicating whether to automatically show a snap when taken
		/// </summary>
		public bool AutoShowSnap
		{
			get { return AutoShowSnapSetting.Value; }
			set { AutoShowSnapSetting.Value = value; }
		}

		/// <summary>
		/// Gets or sets a value indicating whether to expand snap to fit screen if possible
		/// </summary>
		public bool ExpandSnap
		{
			get { return ExpandSnapSetting.Value; }
			set { ExpandSnapSetting.Value = value; }
		}

		/// <summary>
		/// Gets or sets a value indicating whether to shrink snap to fir screen if possible
		/// </summary>
		public bool ShrinkSnap
		{
			get { return ShrinkSnapSetting.Value; }
			set { ShrinkSnapSetting.Value = value; }
		}

		/// <summary>
		/// Gets or sets a value indicating whether to main aspect ratio of the snap
		/// </summary>
		public bool MaintainAspectRatio
		{
			get { return MaintainAspectRatioSetting.Value; }
			set { MaintainAspectRatioSetting.Value = value; }
		}

		// settings
		IntSetting MaxSnapsSetting { get; set; }

		BoolSetting AutoShowSnapSetting { get; set; }

		// ExpandSnap, ShrinkSnap and MaintainAspectRatio are the initial values used
		// when we startup.
		// Afer the window is created, it will use its own values which are distinct from these.
		BoolSetting ExpandSnapSetting { get; set; }

		BoolSetting ShrinkSnapSetting { get; set; }

		BoolSetting MaintainAspectRatioSetting { get; set; }

		/// <summary>
		/// Start the snap module
		/// </summary>
		public override void Start()
		{
			// setup hot keys & commands for magic words
			TakeScreenSnapHotKeyController = AddCommand("TakeSnap", SnapStrings.TakeSnapDescription, string.Empty, TakePrimaryScreenSnap);
			TakeWindowSnapHotKeyController = AddCommand("TakeWinSnap", SnapStrings.TakeWinSnapDescription, string.Empty, TakeActiveWindowSnap);
			ShowSnapHotKeyController = AddCommand("ShowSnap", SnapStrings.ShowSnapDescription, string.Empty, ToggleShowSnap);

			// settings
			MaxSnapsSetting = new IntSetting(_settingsService, ModuleName, "MaxSnaps", DefaultMaxSnaps);
			AutoShowSnapSetting = new BoolSetting(_settingsService, ModuleName, "AutoShowSnap", DefaultAutoShowSnap);
			ExpandSnapSetting = new BoolSetting(_settingsService, ModuleName, "ExpandSnap", DefaultExpandSnap);
			ShrinkSnapSetting = new BoolSetting(_settingsService, ModuleName, "ShrinkSnap", DefaultShrinkSnap);
			MaintainAspectRatioSetting = new BoolSetting(_settingsService, ModuleName, "MaintainAspectRatio", DefaultMaintainAspectRatio);

			// history of snaps taken
			SnapHistory = new SnapHistory(MaxSnaps);

			GetSnapForm();

			// add our menu items to the notifcation icon
			_appForm.AddMenuItem(SnapStrings.SnapNow, null, snapNowToolStripMenuItem_Click);
			_showSnapToolStripMenuItem = _appForm.AddMenuItem(SnapStrings.ShowSnap, null, showSnapToolStripMenuItem_Click);
			_appForm.AddMenuItem("-", null, null);
		}

		/// <summary>
		/// Terminates the module
		/// </summary>
		public override void Terminate()
		{
			if (_snapForm != null)
			{
				_snapForm.Terminate();
			}
		}

		/// <summary>
		/// Called to notify that the display resolution has changed
		/// </summary>
		public override void DisplayResolutionChanged()
		{
			// if resolution of secondary monitor changes, 
			// we need to re-position the window
			if (_snapForm != null && _snapForm.Visible)
			{
				ShowLastSnap();
			}
		}

		/// <summary>
		/// Gets the option nodes for the snap module
		/// </summary>
		/// <returns>Option nodes</returns>
		public override ModuleOptionNode GetOptionNodes()
		{
			Image image = new Bitmap(Properties.Resources.DualSnap_16_16);
			ModuleOptionNodeBranch options = new ModuleOptionNodeBranch("Snap", image, new SnapRootOptionsPanel());
			options.Nodes.Add(new ModuleOptionNodeLeaf("General", image, new SnapGeneralOptionsPanel(this)));

			return options;
		}

		/// <summary>
		/// Take a snap of the primary screen
		/// </summary>
		public void TakePrimaryScreenSnap()
		{
			Rectangle r = Screen.PrimaryScreen.Bounds;

			TakeSnap(r);
		}

		/// <summary>
		/// Take a snap of the active window
		/// </summary>
		public void TakeActiveWindowSnap()
		{
			IntPtr hWnd = NativeMethods.GetForegroundWindow();
			if (hWnd != null)
			{
				NativeMethods.RECT rect;
				if (NativeMethods.GetWindowRect(hWnd, out rect))
				{
					Rectangle r = ScreenHelper.RectToRectangle(ref rect);
					TakeSnap(r);
				}
			}
		}

		/// <summary>
		/// Toggle the visibility of the snap window
		/// </summary>
		public void ToggleShowSnap()
		{
			SnapForm snapForm = GetSnapForm();
			if (snapForm.Visible)
			{
				HideLastSnap();
			}
			else
			{
				ShowLastSnap();
			}
		}

		/// <summary>
		/// Show the last snap taken.
		/// Dos nothing if no snaps available.
		/// </summary>
		public void ShowLastSnap()
		{
			// if we have a snap, then show it
			if (SnapHistory.Count > 0)
			{
				// position window on second screen
				Screen secondaryScreen = ScreenHelper.NextScreen(Screen.PrimaryScreen);
				SnapForm snapForm = GetSnapForm();
				snapForm.ShowAt(secondaryScreen.Bounds);
				if (_showSnapToolStripMenuItem != null)
				{
					_showSnapToolStripMenuItem.Checked = true;
				}
			}
		}

		void TakeSnap(Rectangle sourceRectangle)
		{
			Bitmap snappedImage = new Bitmap(sourceRectangle.Width, sourceRectangle.Height, GetPixelFormat());
			using (Graphics g = Graphics.FromImage(snappedImage))
			{
				g.CopyFromScreen(sourceRectangle.Location, new Point(0, 0), sourceRectangle.Size, CopyPixelOperation.SourceCopy);
			}

			SnapForm snapForm = GetSnapForm();
			snapForm.ShowImage(snappedImage);

			// whenever we take a snap, we reset the snap zooming to match our settings
			// TODO: it can get a bit confusing have 2 copies of the settings.  Should they be permanently linked? 
			snapForm.SetScaling(ExpandSnap, ShrinkSnap, MaintainAspectRatio);

			Snap snap = new Snap(snappedImage);
			SnapHistory.Add(snap);

			if (AutoShowSnap || snapForm.Visible)
			{
				ShowLastSnap();
			}
		}

		PixelFormat GetPixelFormat()
		{
			switch (Screen.PrimaryScreen.BitsPerPixel)
			{
				case 8:
				case 16:
					return PixelFormat.Format16bppRgb555;

				case 24:
					return PixelFormat.Format24bppRgb;

				case 32:
					return PixelFormat.Format32bppRgb;

				default:
					return PixelFormat.Format32bppRgb;
			}
		}

		void HideLastSnap()
		{
			SnapForm snapForm = GetSnapForm();
			snapForm.HideSnap();
			if (_showSnapToolStripMenuItem != null)
			{
				_showSnapToolStripMenuItem.Checked = false;
			}
		}

		void snapNowToolStripMenuItem_Click(object sender, EventArgs e)
		{
			TakePrimaryScreenSnap();
		}

		void showSnapToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ToggleShowSnap();
		}

		SnapForm GetSnapForm()
		{
			if (_snapForm == null)
			{
				_snapForm = new SnapForm(this);
			}

			return _snapForm;
		}
	}
}
