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
using DMT.Library.HotKeys;
using DMT.Library.Logging;
using DMT.Library.Settings;
using DMT.Library.Wallpaper;
using DMT.Library.WallpaperPlugin;
using DMT.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DMT.Modules.WallpaperChanger
{
	class WallpaperChangerModule : Module
	{
		//const string _moduleName = "WallpaperChanger";
		const int _defaultIntervalHours = 1;
		const int _defaultIntervalMinutes = 0;

		ISettingsService _settingsService;
		//IHotKeyService _hotKeyService;
		ILogger _logger;
		AppForm _appForm;

		ILocalEnvironment _localEnvironment;
		IProviderPersistence _providerPersistence;
		IProviderFactory _providerFactory;
		IWallpaperCompositorFactory _wallpaperCompositorFactory;
		IImageRepository _imageRepository;
		Desktop _desktop;

		public Desktop Desktop { get { return _desktop; } }

		// for periodically updating the wallpaper
		System.Timers.Timer _timer;
		bool _paused = false;
		int _minutesSinceLastChange = 0;

		WallpaperChangerGeneralOptionsPanel _generalOptionsPanel;
		WallpaperChangerPropertiesOptionsPanel _propertiesOptionsPanel;

		ToolStripMenuItem _pauseToolStripMenuItem;

		// hotkey to change wallpaper now
		public HotKeyController ChangeWallpaperHotKeyController { get; protected set; }

		// settings
		IntSetting IntervalHoursSetting { get; set; }
		public int IntervalHours
		{
			get { return IntervalHoursSetting.Value; }
			set { IntervalHoursSetting.Value = value; }
		}

		IntSetting IntervalMinutesSetting { get; set; }
		public int IntervalMinutes
		{
			get { return IntervalMinutesSetting.Value; }
			set { IntervalMinutesSetting.Value = value; }
		}

		BoolSetting ChangeOnStartupSetting { get; set; }
		public bool ChangeOnStartup
		{
			get { return ChangeOnStartupSetting.Value; }
			set { ChangeOnStartupSetting.Value = value; }
		}

		BoolSetting ChangePeriodicallySetting { get; set; }
		public bool ChangePeriodically
		{
			get { return ChangePeriodicallySetting.Value; }
			set 
			{ 
				ChangePeriodicallySetting.Value = value;
				// will need to change time to next change
				UpdateTimeToChange();
			}
		}

		BoolSetting SmoothFadeSetting { get; set; }
		public bool SmoothFade
		{
			get { return SmoothFadeSetting.Value; }
			set { SmoothFadeSetting.Value = value; }
		}

		IntSetting BackgroundColourSetting { get; set; }
		public Color BackgroundColour
		{
			get { return Color.FromArgb(BackgroundColourSetting.Value); }
			set { BackgroundColourSetting.Value = value.ToArgb(); }
		}

		IntSetting FitSetting { get; set; }
		public StretchType.Fit Fit
		{
			get { return (StretchType.Fit)FitSetting.Value; }
			set { FitSetting.Value = (int)value; }
		}

		IntSetting MonitorMappingSetting { get; set; }
		public SwitchType.ImageToMonitorMapping MonitorMapping
		{
			get { return (SwitchType.ImageToMonitorMapping)MonitorMappingSetting.Value; }
			set { MonitorMappingSetting.Value = (int)value; }
		}

		public WallpaperChangerModule(ISettingsService settingsService, IHotKeyService hotKeyService, ILocalEnvironment localEnvironment, ILogger logger, AppForm appForm)
			: base(hotKeyService)
		{
			_settingsService = settingsService;
			//_hotKeyService = hotKeyService;
			_localEnvironment = localEnvironment;
			_logger = logger;
			_appForm = appForm;

			ModuleName = "WallpaperChanger";
			_providerFactory = new ProviderFactory(_settingsService);
			_providerPersistence = new ProviderPersistence(_providerFactory, _localEnvironment);
			_wallpaperCompositorFactory = new WallpaperCompositorFactory();
			_imageRepository = new ImageRepository(_providerPersistence, _logger);
			_desktop = new Desktop(this, _localEnvironment, _imageRepository, _wallpaperCompositorFactory);
		}


		public override void Start()
		{
			// hot keys
			ChangeWallpaperHotKeyController = AddCommand("ChangeWallpaper", WallpaperStrings.ChangeWallpaperDescription, "", UpdateWallpaper);
			// Pause available as a command only
			AddCommand("PauseWallpaper", WallpaperStrings.PauseWallpaperDescription, "", PauseWallpaper, false, true);

			//ChangeWallpaperHotKeyController = CreateHotKeyController("ChangeWallpaperHotKey", WallpaperStrings.ChangeWallpaperDescription, "", UpdateWallpaper);


			// settings
			IntervalHoursSetting = new IntSetting(_settingsService, ModuleName, "IntervalHours", _defaultIntervalHours);
			IntervalMinutesSetting = new IntSetting(_settingsService, ModuleName, "IntervalMinutes", _defaultIntervalMinutes);
			ChangeOnStartupSetting = new BoolSetting(_settingsService, ModuleName, "ChangeOnStartup", false);
			ChangePeriodicallySetting = new BoolSetting(_settingsService, ModuleName, "ChangePeriodically", false);
			SmoothFadeSetting = new BoolSetting(_settingsService, ModuleName, "SmoothFade", false);
			BackgroundColourSetting = new IntSetting(_settingsService, ModuleName, "BackgroundColour", Color.Black.ToArgb());
			FitSetting = new IntSetting(_settingsService, ModuleName, "Fit", (int)StretchType.Fit.OverStretch);
			MonitorMappingSetting = new IntSetting(_settingsService, ModuleName, "MonitorMapping", (int)SwitchType.ImageToMonitorMapping.OneToOneBig);

			if (ChangeOnStartup)
			{
				UpdateWallpaper();
			}

			// start a minute timer
			_timer = new System.Timers.Timer(60 * 1000);
			_timer.SynchronizingObject = _appForm;
			_timer.Elapsed += new System.Timers.ElapsedEventHandler(Timer_Tick);
			_timer.AutoReset = true;
			_timer.Enabled = true;
			UpdateTimeToChange();

			// add our menu items to the notifcation icon
			_appForm.AddMenuItem(WallpaperStrings.ChangeWallpaperNow, null, changeWallpaperNowToolStripMenuItem_Click);
			_pauseToolStripMenuItem = _appForm.AddMenuItem(WallpaperStrings.PauseWallpaperChanging, null, pauseWallpaperChangingToolStripMenuItem_Click);
			_appForm.AddMenuItem("-", null, null);
		}

		public override ModuleOptionNode GetOptionNodes()
		{
			Image image = new Bitmap(Properties.Resources.DualWallpaper_16_16);
			ModuleOptionNodeBranch options = new ModuleOptionNodeBranch("Wallpaper Changer", image, new WallpaperChangerRootOptionsPanel());
			_generalOptionsPanel = new WallpaperChangerGeneralOptionsPanel(this);
			options.Nodes.Add(new ModuleOptionNodeLeaf("General", image, _generalOptionsPanel));
			_propertiesOptionsPanel = new WallpaperChangerPropertiesOptionsPanel(this);
			options.Nodes.Add(new ModuleOptionNodeLeaf("Properties", image, _propertiesOptionsPanel));
			options.Nodes.Add(new ModuleOptionNodeLeaf("Providers", image, new WallpaperChangerProvidersOptionsPanel(this)));

			return options;
		}

		public override void Terminate()
		{
			//if (_entryForm != null)
			//{
			//	_entryForm.Terminate();
			//}

		}

		/// <summary>
		/// Returns a list of all available plugins
		/// </summary>
		/// <returns>All available plugins</returns>
		public List<IDWC_Plugin> GetPluginsDataSource()
		{
			return _providerFactory.GetAvailablePlugins();
		}

		/// <summary>
		/// Returns a list of all providers the user has currently configured
		/// ready for display in a DataGrid
		/// </summary>
		/// <returns>Users configured providers</returns>
		public BindingList<IImageProvider> GetProvidersDataSource()
		{
			return _imageRepository.DataSource;
		}

		/// <summary>
		/// Generates a new wallpaper for the desktop
		/// </summary>
		public void UpdateWallpaper()
		{
			_desktop.UpdateWallpaper();
			_minutesSinceLastChange = 0;
			UpdateTimeToChange();
			UpdateWallpaperPreview();
		}

		/// <summary>
		/// Allows the user to add a new provider.
		/// This will cause the providers config dialog to be displayed
		/// </summary>
		/// <param name="providerName">Name of provider</param>
		/// <returns>true iff provider added</returns>
		public bool AddProvider(string providerName)
		{
			Dictionary<string, string> configDictionary = new Dictionary<string, string>();
			IImageProvider provider = _providerFactory.CreateProvider(providerName, configDictionary);
			if (provider != null)
			{
				configDictionary = provider.ShowUserOptions();
				if (configDictionary != null)
				{
					_imageRepository.DataSource.Add(provider);
					_imageRepository.Save();
					UpdateTimeToChange();
					return true;
				}
			}
			return false;
		}

		/// <summary>
		/// Allows the user to edit a provider they have previously added
		/// This will cause the providers config dialog to be displayed
		/// </summary>
		/// <param name="rowIndex">Index of provider returned by GetProvidersDataSource()</param>
		/// <returns>true iff provider config changes saved</returns>
		public bool EditProvider(int rowIndex)
		{
			IImageProvider provider = _imageRepository.DataSource[rowIndex];
			Dictionary<string, string> configDictionary = provider.ShowUserOptions();
			if (configDictionary != null)
			{
				_imageRepository.Save();
				return true;
			}
			return false;
		}

		/// <summary>
		/// Deletes all indicated providers
		/// </summary>
		/// <param name="rowIndexes">List of indexes returned by GetProvidersDataSource() to delete</param>
		public void DeleteProviders(List<int> rowIndexes)
		{
			// TODO - check
			List<IImageProvider> providers = new List<IImageProvider>();
			foreach (int rowIndex in rowIndexes)
			{
				providers.Add(_imageRepository.DataSource[rowIndex]);
			}
			foreach (IImageProvider provider in providers)
			{
				_imageRepository.DataSource.Remove(provider);
			}
			_imageRepository.Save();
			UpdateTimeToChange();
		}

		void PauseWallpaper()
		{
			_paused = !_paused;
			if (_pauseToolStripMenuItem != null)
			{
				_pauseToolStripMenuItem.Checked = _paused;
			}
			UpdateTimeToChange();
		}

		//HotKeyController CreateHotKeyController(string settingName, string description, string win7Key, HotKey.HotKeyHandler handler)
		//{
		//	return _hotKeyService.CreateHotKeyController(ModuleName, settingName, description, win7Key, handler);
		//}

		void changeWallpaperNowToolStripMenuItem_Click(object sender, EventArgs e)
		{
			UpdateWallpaper();
		}

		void pauseWallpaperChangingToolStripMenuItem_Click(object sender, EventArgs e)
		{
			PauseWallpaper();
		}

		void Timer_Tick(object source, System.Timers.ElapsedEventArgs e)
		{
			if (!_paused)
			{
				_minutesSinceLastChange++;
				int period = GetMinutesBetweenChanges();
				if (ChangePeriodically && _minutesSinceLastChange >= period)
				{
					UpdateWallpaper();
				}
				else
				{
					// need to update time till next change
					UpdateTimeToChange();
				}
			}
		}

		public void UpdateTimeToChange()
		{
			string msgText;
			Color msgColor = SystemColors.ControlText;

			if (_imageRepository.DataSource.Count == 0)
			{
				msgText = WallpaperStrings.MsgNoProviders;
				msgColor = Color.Red;
			}
			else if (_paused)
			{
				msgText = WallpaperStrings.MsgIsPaused;
			}
			else if (ChangePeriodically)
			{
				int period = GetMinutesBetweenChanges();
				int timeLeft = period - _minutesSinceLastChange;
				// never want to report less than 0 minutes
				if (timeLeft < 1)
				{
					timeLeft = 1;
				}
				string formatString = (timeLeft <= 1) ? WallpaperStrings.MsgTimeToChange1 : WallpaperStrings.MsgTimeToChange2;
				msgText = string.Format(formatString, timeLeft);
			}
			else
			{
				msgText = WallpaperStrings.MsgNoChanging;
			}

			if (_generalOptionsPanel != null)
			{
				_generalOptionsPanel.ShowNextChange(msgText, msgColor);
			}


			if (_propertiesOptionsPanel != null)
			{
				_propertiesOptionsPanel.ShowNextChange(msgText, msgColor);
			}

		}

		void UpdateWallpaperPreview()
		{
			if (_propertiesOptionsPanel != null)
			{
				_propertiesOptionsPanel.ShowNewWallpaper();
			}
		}

		int GetMinutesBetweenChanges()
		{
			int ret = IntervalHours * 60 + IntervalMinutes;
			// Note: OK to return 0, as the timer only ticks once a minute,
			// so when on timer, won't change more than once per minute
			return ret;
		}

	}
}
