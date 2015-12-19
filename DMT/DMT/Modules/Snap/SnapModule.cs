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

using DMT.Library;
using DMT.Library.GuiUtils;
using DMT.Library.HotKeys;
using DMT.Library.Logging;
using DMT.Library.PInvoke;
using DMT.Library.Settings;
using DMT.Resources;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DMT.Modules.Snap
{
	class SnapModule : Module
	{
		//const string _moduleName = "Snap";

		const int _defaultMaxSnaps = 8;
		const bool _defaultAutoShowSnap = true;
		const bool _defaultExpandSnap = false;
		const bool _defaultShrinkSnap = false;
		const bool _defaultMaintainAspectRatio = true;

		ISettingsService _settingsService;
		//IHotKeyService _hotKeyService;
		ILogger _logger;
		AppForm _appForm;

		SnapForm _snapForm = null;
		public SnapHistory SnapHistory { get; protected set; }

		ToolStripMenuItem _showSnapToolStripMenuItem;

		// hot keys
		public HotKeyController TakeScreenSnapHotKeyController { get; protected set; }
		public HotKeyController TakeWindowSnapHotKeyController { get; protected set; }
		public HotKeyController ShowSnapHotKeyController { get; protected set; }

		// settings
		IntSetting MaxSnapsSetting { get; set; }
		public int MaxSnaps
		{
			get { return MaxSnapsSetting.Value; }
			set { MaxSnapsSetting.Value = value; }
		}

		BoolSetting AutoShowSnapSetting { get; set; }
		public bool AutoShowSnap
		{
			get { return AutoShowSnapSetting.Value; }
			set { AutoShowSnapSetting.Value = value; }
		}

		// ExpandSnap, ShrinkSnap and MaintainAspectRatio are the initial values used
		// when we startup.
		// Afer the window is created, it will use its own values which are distinct from these.
		BoolSetting ExpandSnapSetting { get; set; }
		public bool ExpandSnap
		{
			get { return ExpandSnapSetting.Value; }
			set { ExpandSnapSetting.Value = value; }
		}

		BoolSetting ShrinkSnapSetting { get; set; }
		public bool ShrinkSnap
		{
			get { return ShrinkSnapSetting.Value; }
			set { ShrinkSnapSetting.Value = value; }
		}

		BoolSetting MaintainAspectRatioSetting { get; set; }
		public bool MaintainAspectRatio
		{
			get { return MaintainAspectRatioSetting.Value; }
			set { MaintainAspectRatioSetting.Value = value; }
		}

		public SnapModule(ISettingsService settingsService, IHotKeyService hotKeyService, ILogger logger, AppForm appForm)
			: base(hotKeyService)
		{
			_settingsService = settingsService;
			//_hotKeyService = hotKeyService;
			_logger = logger;
			_appForm = appForm;

			ModuleName = "Snap";
		}

		public override void Start()
		{
			// setup hot keys & commands for magic words
			TakeScreenSnapHotKeyController = AddCommand("TakeSnap", SnapStrings.TakeSnapDescription, "", TakePrimaryScreenSnap);
			TakeWindowSnapHotKeyController = AddCommand("TakeWinSnap", SnapStrings.TakeWinSnapDescription, "", TakeActiveWindowSnap);
			ShowSnapHotKeyController = AddCommand("ShowSnap", SnapStrings.ShowSnapDescription, "", ToggleShowSnap);

			// settings
			MaxSnapsSetting = new IntSetting(_settingsService, ModuleName, "MaxSnaps", _defaultMaxSnaps);
			AutoShowSnapSetting = new BoolSetting(_settingsService, ModuleName, "AutoShowSnap", _defaultAutoShowSnap);
			ExpandSnapSetting = new BoolSetting(_settingsService, ModuleName, "ExpandSnap", _defaultExpandSnap);
			ShrinkSnapSetting = new BoolSetting(_settingsService, ModuleName, "ShrinkSnap", _defaultShrinkSnap);
			MaintainAspectRatioSetting = new BoolSetting(_settingsService, ModuleName, "MaintainAspectRatio", _defaultMaintainAspectRatio);

			// history of snaps taken
			SnapHistory = new SnapHistory(MaxSnaps);

			GetSnapForm();

			// add our menu items to the notifcation icon
			_appForm.AddMenuItem(SnapStrings.SnapNow, null, snapNowToolStripMenuItem_Click);
			_showSnapToolStripMenuItem = _appForm.AddMenuItem(SnapStrings.ShowSnap, null, showSnapToolStripMenuItem_Click);
			_appForm.AddMenuItem("-", null, null);
		}

		public override void Terminate()
		{
			if (_snapForm != null)
			{
				_snapForm.Terminate();
			}
		}

		public override void DisplayResolutionChanged()
		{
			// if resolution of secondary monitor changes, 
			// we need to re-position the window
			if (_snapForm != null && _snapForm.Visible)
			{
				ShowLastSnap();
			}
		}

		public override ModuleOptionNode GetOptionNodes()
		{
			Image image = new Bitmap(Properties.Resources.DualSnap_16_16);
			ModuleOptionNodeBranch options = new ModuleOptionNodeBranch("Snap", image, new SnapRootOptionsPanel());
			options.Nodes.Add(new ModuleOptionNodeLeaf("General", image, new SnapGeneralOptionsPanel(this)));

			return options;
		}

		//HotKeyController CreateHotKeyController(string settingName, string description, string win7Key, HotKey.HotKeyHandler handler)
		//{
		//	return _hotKeyService.CreateHotKeyController(ModuleName, settingName, description, win7Key, handler);
		//}

		public void TakePrimaryScreenSnap()
		{
			Rectangle r = Screen.PrimaryScreen.Bounds;

			TakeSnap(r);

			//Bitmap primaryScreenImage = new Bitmap(r.Width, r.Height, GetPixelFormat());
			//using (Graphics g = Graphics.FromImage(primaryScreenImage))
			//{
			//	g.CopyFromScreen(r.Location, new Point(0, 0), r.Size, CopyPixelOperation.SourceCopy);
			//}
			//SnapForm snapForm = GetSnapForm();
			//snapForm.ShowImage(primaryScreenImage);

			//Snap snap = new Snap(primaryScreenImage);
			//SnapHistory.Add(snap);

			//if (AutoShowSnap)
			//{
			//	ShowLastSnap();
			//}
		}

		public void TakeActiveWindowSnap()
		{
			IntPtr hWnd = Win32.GetForegroundWindow();
			if (hWnd != null)
			{
				Win32.RECT rect;
				if (Win32.GetWindowRect(hWnd, out rect))
				{
					Rectangle r = ScreenHelper.RectToRectangle(ref rect);
					TakeSnap(r);
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
