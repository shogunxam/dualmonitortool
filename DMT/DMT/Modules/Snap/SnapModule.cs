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

		ISettingsService _settingsService;
		//IHotKeyService _hotKeyService;
		ILogger _logger;
		AppForm _appForm;

		SnapForm _snapForm = null;
		public SnapHistory SnapHistory { get; protected set; }

		ToolStripMenuItem _showSnapToolStripMenuItem;

		// hot keys
		public HotKeyController TakeSnapHotKeyController { get; protected set; }
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

		public SnapModule(ISettingsService settingsService, IHotKeyService hotKeyService, ILogger logger, AppForm appForm)
			: base(hotKeyService)
		{
			_settingsService = settingsService;
			//_hotKeyService = hotKeyService;
			_logger = logger;
			_appForm = appForm;

			ModuleName = "Snap";

			Start();
		}

		public override void Terminate()
		{
			if (_snapForm != null)
			{
				_snapForm.Terminate();
			}

		}

		void Start()
		{
			// setup hot keys & commands for magic words
			TakeSnapHotKeyController = AddCommand("TakeSnap", SnapStrings.TakeSnapDescription, "", TakeSnap);
			ShowSnapHotKeyController = AddCommand("ShowSnap", SnapStrings.ShowSnapDescription, "", ToggleShowSnap);

			// hot keys
			//TakeSnapHotKeyController = CreateHotKeyController("TakeSnapHotKey", SnapStrings.TakeSnapDescription, "", TakeSnap);
			//ShowSnapHotKeyController = CreateHotKeyController("ShowSnapHotKey", SnapStrings.ShowSnapDescription, "", ToggleShowSnap);
			//base.RegisterHotKeys();

			// settings
			MaxSnapsSetting = new IntSetting(_settingsService, ModuleName, "MaxSnaps", _defaultMaxSnaps);
			AutoShowSnapSetting = new BoolSetting(_settingsService, ModuleName, "AutoShowSnap", _defaultAutoShowSnap);

			// history of snaps taken
			SnapHistory = new SnapHistory(MaxSnaps);

			GetSnapForm();

			// add our menu items to the notifcation icon
			_appForm.AddMenuItem(SnapStrings.SnapNow, null, snapNowToolStripMenuItem_Click);
			_showSnapToolStripMenuItem = _appForm.AddMenuItem(SnapStrings.ShowSnap, null, showSnapToolStripMenuItem_Click);
			_appForm.AddMenuItem("-", null, null);
		}


		public override ModuleOptionNode GetOptionNodes()
		{
			ModuleOptionNodeBranch options = new ModuleOptionNodeBranch("Snap", new SnapRootOptionsPanel());
			options.Nodes.Add(new ModuleOptionNodeLeaf("General", new SnapGeneralOptionsPanel(this)));

			return options;
		}

		//HotKeyController CreateHotKeyController(string settingName, string description, string win7Key, HotKey.HotKeyHandler handler)
		//{
		//	return _hotKeyService.CreateHotKeyController(ModuleName, settingName, description, win7Key, handler);
		//}

		public void TakeSnap()
		{
			Rectangle r = Screen.PrimaryScreen.Bounds;

			Bitmap primaryScreenImage = new Bitmap(r.Width, r.Height, GetPixelFormat());
			using (Graphics g = Graphics.FromImage(primaryScreenImage))
			{
				g.CopyFromScreen(r.Location, new Point(0, 0), r.Size, CopyPixelOperation.SourceCopy);
			}
			SnapForm snapForm = GetSnapForm();
			snapForm.ShowImage(primaryScreenImage);

			Snap snap = new Snap(primaryScreenImage);
			SnapHistory.Add(snap);

			if (AutoShowSnap)
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
			TakeSnap();
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
