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

namespace DMT
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;
	using System.Windows.Forms;

	using DMT.Library.Command;
	using DMT.Library.Environment;
	using DMT.Library.HotKeys;
	using DMT.Library.Logging;
	using DMT.Library.Settings;
	using DMT.Modules;

	using Microsoft.Win32;

	/// <summary>
	/// Controls the modules within DMT
	/// </summary>
	class Controller
	{
		AppForm _appForm;
		ISettingsService _settingsService;
		IHotKeyService _hotKeyService;
		ILocalEnvironment _localEnvironment;
		IModuleService _moduleService;
		ICommandRunner _commandRunner;
		ILogger _logger;

		BoolSetting _firstRunSetting;

		/// <summary>
		/// Initialises a new instance of the <see cref="Controller" /> class.
		/// </summary>
		/// <param name="appForm">hidden DMT notification window</param>
		public Controller(AppForm appForm)
		{
			_appForm = appForm;
		}

		/// <summary>
		/// Gets the application logger
		/// </summary>
		public ILogger Logger 
		{ 
			get { return _logger; } 
		}

		/// <summary>
		/// Starts DMT and its modules.
		/// </summary>
		public void Start()
		{
			_settingsService = new SettingsRepository();
			_hotKeyService = new HotKeyRepository(_appForm, _settingsService);
			_localEnvironment = new LocalEnvironment();

			// ModuleRepository provides both IModuleService and ICommandRunner
			ModuleRepository moduleRepository = new ModuleRepository();
			_moduleService = moduleRepository;
			_commandRunner = moduleRepository;
			_logger = new Logger();
			_logger.LogInfo("Controller", "DMT Starting");

			// temp code
			System.OperatingSystem operatingSystem = System.Environment.OSVersion;
			_logger.LogInfo("Controller", "O/S Version Major:{0}, Minor:{1}", operatingSystem.Version.Major, operatingSystem.Version.Minor);

			// now add the modules
			_moduleService.AddModule(new DMT.Modules.General.GeneralModule(_settingsService, _hotKeyService, _logger, _appForm));
			_moduleService.AddModule(new DMT.Modules.Cursor.CursorModule(_settingsService, _hotKeyService, _logger));
			_moduleService.AddModule(new DMT.Modules.Launcher.LauncherModule(_settingsService, _hotKeyService, _localEnvironment, _logger, _appForm, _commandRunner));
			_moduleService.AddModule(new DMT.Modules.Snap.SnapModule(_settingsService, _hotKeyService, _logger, _appForm));
			_moduleService.AddModule(new DMT.Modules.SwapScreen.SwapScreenModule(_settingsService, _hotKeyService, _localEnvironment, _logger));
			_moduleService.AddModule(new DMT.Modules.WallpaperChanger.WallpaperChangerModule(_settingsService, _hotKeyService, _localEnvironment, _logger, _appForm));

			_moduleService.StartAllModules();

			_moduleService.StartUpComplete();

			_firstRunSetting = new BoolSetting(_settingsService, "_", "FirstRun", true);
			if (_firstRunSetting.Value)
			{
				FirstRunProcessing();
			}
		}

		/// <summary>
		/// Called when the display resolution has changed
		/// </summary>
		public void DisplayResolutionChanged()
		{
			if (_moduleService != null)
			{
				_moduleService.DisplayResolutionChanged();
			}
		}

		/// <summary>
		/// Make sure that all settings have been written to disk.
		/// </summary>
		public void Flush()
		{
			_moduleService.FlushAllModules();
		}

		/// <summary>
		/// Stops DMT and it's modules.
		/// </summary>
		public void Stop()
		{
			Flush();

			// stop each module in turn
			_moduleService.TerminateAllModules();

			// release all hotkeys in one go
			_hotKeyService.Stop();
		}

		/// <summary>
		/// Creates the Options dialog.
		/// </summary>
		/// <returns>The options dialog</returns>
		public OptionsForm CreateOptionsForm()
		{
			return new OptionsForm(_moduleService);
		}

		/// <summary>
		/// Runs an internal command
		/// </summary>
		/// <param name="command">The command to run</param>
		/// <param name="parameters">The parameters needed by the command</param>
		/// <returns>True if successful</returns>
		public bool RunInternalCommand(string command, string parameters)
		{
			return _commandRunner.RunInternalCommand(command, parameters);
		}

		bool ProcessCommand(string moduleName, string actionName)
		{
			return false;
		}

		void FirstRunProcessing()
		{
			_appForm.ShowFirstRun();
			_firstRunSetting.Value = false;
		}
	}
}
