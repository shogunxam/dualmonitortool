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

namespace DMT.Modules.Launcher
{
	using System;
	using System.Collections.Generic;
	using System.Drawing;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;
	using System.Windows.Forms;

	using DMT.Library.Command;
	using DMT.Library.Environment;
	using DMT.Library.HotKeys;
	using DMT.Library.Logging;
	using DMT.Library.PInvoke;
	using DMT.Library.Settings;
	using DMT.Library.Utils;
	using DMT.Resources;

	/// <summary>
	/// The launcher module
	/// </summary>
	class LauncherModule : Module
	{
		const int DefaultMaxIcons = 8;

		ISettingsService _settingsService;
		ILocalEnvironment _localEnvironment;
		ILogger _logger;
		AppForm _appForm;
		StartupHandler _startupHandler;
		MagicWords _magicWords = null;
		EntryForm _entryForm = null;

		/// <summary>
		/// Initialises a new instance of the <see cref="LauncherModule" /> class.
		/// </summary>
		/// <param name="settingsService">Settings repository</param>
		/// <param name="hotKeyService">Service to register hot keys</param>
		/// <param name="localEnvironment">Local environment</param>
		/// <param name="logger">Application logger</param>
		/// <param name="appForm">Application (hidden) window</param>
		/// <param name="commandRunner">Command running service</param>
		public LauncherModule(ISettingsService settingsService, IHotKeyService hotKeyService, ILocalEnvironment localEnvironment, ILogger logger, AppForm appForm, ICommandRunner commandRunner)
			: base(hotKeyService)
		{
			_settingsService = settingsService;
			_localEnvironment = localEnvironment;
			_logger = logger;
			_appForm = appForm;
			CommandRunner = commandRunner;

			ModuleName = "Launcher";
		}

		/// <summary>
		/// Gets the list of magic words
		/// </summary>
		public MagicWords MagicWords 
		{ 
			get 
			{ 
				return GetMagicWords(); 
			} 
		}

		/// <summary>
		/// Gets the command runner
		/// </summary>
		public ICommandRunner CommandRunner { get; private set; }

		/// <summary>
		/// Gets the controller for the 'Activate magic word entry' hot key
		/// </summary>
		public HotKeyController ActivateHotKeyController { get; private set; }

		/// <summary>
		/// Gets the controller for the 'Add magic word for active window' hot key
		/// </summary>
		public HotKeyController AddMagicWordHotKeyController { get; private set; }

		/// <summary>
		/// Gets or sets the maximum number of icons to display in the magic word entry dialog
		/// </summary>
		public int MaxIcons 
		{
			get 
			{ 
				return MaxIconsSetting.Value; 
			}

			set 
			{ 
				MaxIconsSetting.Value = value; 
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether to sort icons by most recently used.
		/// If false, sorts by last used
		/// </summary>
		public bool UseMru
		{
			get 
			{ 
				return UseMruSetting.Value; 
			}

			set 
			{ 
				UseMruSetting.Value = value; 
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether magic words should be loaded as soon as we start
		/// </summary>
		public bool LoadWordsOnStartup
		{
			get 
			{ 
				return LoadWordsOnStartupSetting.Value; 
			}

			set 
			{ 
				LoadWordsOnStartupSetting.Value = value; 
			}
		}

		/// <summary>
		/// Gets or sets the type of view to use for the magic words entry dialog
		/// </summary>
		public View IconView
		{
			get 
			{ 
				return (View)IconViewSetting.Value; 
			}

			set 
			{ 
				IconViewSetting.Value = (int)value; 
			}
		}

		/// <summary>
		/// Gets or sets the timeout in seconds to wait for an applications
		/// main window appears in order that we can move it to the desired location
		/// </summary>
		public int StartupTimeout
		{
			get 
			{ 
				return StartupTimeoutSetting.Value; 
			}

			set 
			{ 
				StartupTimeoutSetting.Value = value; 
			}
		}

		/// <summary>
		/// Gets or sets the key to indicate that the user wants the application to appear at position 1
		/// </summary>
		public uint Position1Key
		{
			get 
			{ 
				return Position1KeySetting.Value; 
			}

			set 
			{ 
				Position1KeySetting.Value = value; 
			}
		}

		/// <summary>
		/// Gets or sets the key to indicate that the user wants the application to appear at position 2
		/// </summary>
		public uint Position2Key
		{
			get 
			{ 
				return Position2KeySetting.Value; 
			}

			set 
			{ 
				Position2KeySetting.Value = value; 
			}
		}

		/// <summary>
		/// Gets or sets the key to indicate that the user wants the application to appear at position 3
		/// </summary>
		public uint Position3Key
		{
			get 
			{ 
				return Position3KeySetting.Value; 
			}

			set 
			{ 
				Position3KeySetting.Value = value; 
			}
		}

		/// <summary>
		/// Gets or sets the key to indicate that the user wants the application to appear at position 4
		/// </summary>
		public uint Position4Key
		{
			get 
			{ 
				return Position4KeySetting.Value; 
			}

			set 
			{ 
				Position4KeySetting.Value = value; 
			}
		}

		/// <summary>
		/// Gets or sets the location for the magic word entry dialog
		/// </summary>
		public Rectangle EntryFormPosition
		{
			get
			{
				Rectangle rect = new Rectangle(0, 0, 0, 0);
				if (_settingsService.SettingExists(ModuleName, "EntryFormPosition"))
				{
					rect = StringUtils.ToRectangle(_settingsService.GetSetting(ModuleName, "EntryFormPosition"));
				}

				return rect;
			}

			set
			{
				string settingString = StringUtils.FromRectangle(value);
				_settingsService.SetSetting(ModuleName, "EntryFormPosition", settingString);
				_settingsService.SaveSettings();
			}
		}

		IntSetting MaxIconsSetting { get; set; }

		BoolSetting UseMruSetting { get; set; }

		BoolSetting LoadWordsOnStartupSetting { get; set; }

		IntSetting IconViewSetting { get; set; }

		IntSetting StartupTimeoutSetting { get; set; }

		UIntSetting Position1KeySetting { get; set; }

		UIntSetting Position2KeySetting { get; set; }

		UIntSetting Position3KeySetting { get; set; }

		UIntSetting Position4KeySetting { get; set; }

		/// <summary>
		/// Starts the module up
		/// </summary>
		public override void Start()
		{
			// this handles the actual starting up of applications and moving their windows
			_startupHandler = new StartupHandler(_appForm, CommandRunner);

			// hot keys
			//// don't want a magic word to show the magic word entry form!
			ActivateHotKeyController = AddCommand("Activate", LauncherStrings.ActivateDescription, string.Empty, ShowEntryForm, ActivateMagicWord);
			AddMagicWordHotKeyController = AddCommand("AddMagicWord", LauncherStrings.AddMagicWordDescription, string.Empty, AddNewMagicWordForActiveWindow);

			// settings
			MaxIconsSetting = new IntSetting(_settingsService, ModuleName, "MaxIcons", DefaultMaxIcons);
			UseMruSetting = new BoolSetting(_settingsService, ModuleName, "UseMru");
			LoadWordsOnStartupSetting = new BoolSetting(_settingsService, ModuleName, "LoadWordsOnStartup");
			IconViewSetting = new IntSetting(_settingsService, ModuleName, "IconView", (int)View.LargeIcon);
			StartupTimeoutSetting = new IntSetting(_settingsService, ModuleName, "StartupTimeout", 60);

			Position1KeySetting = new UIntSetting(_settingsService, ModuleName, "Position1Key", (uint)Keys.F1);
			Position2KeySetting = new UIntSetting(_settingsService, ModuleName, "Position2Key", (uint)Keys.F2);
			Position3KeySetting = new UIntSetting(_settingsService, ModuleName, "Position3Key", (uint)Keys.F3);
			Position4KeySetting = new UIntSetting(_settingsService, ModuleName, "Position4Key", (uint)Keys.F4);

			// add our menu items to the notifcation icon
			_appForm.AddMenuItem(LauncherStrings.EnterMagicWord, null, enterMagicWordToolStripMenuItem_Click);
			_appForm.AddMenuItem(LauncherStrings.AddMagicWord, null, addMagicWordToolStripMenuItem_Click);
			_appForm.AddMenuItem("-", null, null);
		}

		/// <summary>
		/// Indicates all modules have started, and allows
		/// individual modules to perform any post start up processing
		/// that depends on other modules
		/// </summary>
		public override void StartUpComplete()
		{
			if (LoadWordsOnStartup)
			{
				// load magic words now
				// (otherwise they will be loaded when first needed)
				GetMagicWords();
			}
		}

		/// <summary>
		/// Gives the module a chance to flush any data out to disk
		/// Will be called if the app is closing or system about to shutdown
		/// </summary>
		public override void Flush()
		{
			SaveMagicWords();
		}

		/// <summary>
		/// Terminates the module
		/// </summary>
		public override void Terminate()
		{
			if (_entryForm != null)
			{
				_entryForm.Terminate();
			}
		}

		/// <summary>
		/// Gets the option nodes for this module
		/// </summary>
		/// <returns>The root node</returns>
		public override ModuleOptionNode GetOptionNodes()
		{
			Image image = new Bitmap(Properties.Resources.DualLauncher_16_16);
			ModuleOptionNodeBranch options = new ModuleOptionNodeBranch("Launcher", image, new LauncherRootOptionsPanel());
			options.Nodes.Add(new ModuleOptionNodeLeaf("Magic Words", image, new LauncherMagicWordsOptionsPanel(this)));
			options.Nodes.Add(new ModuleOptionNodeLeaf("HotKeys", image, new LauncherHotKeysOptionsPanel(this)));
			options.Nodes.Add(new ModuleOptionNodeLeaf("General", image, new LauncheGeneralOptionsPanel(this)));
			options.Nodes.Add(new ModuleOptionNodeLeaf("Import / Export", image, new LauncherImportOptionsPanel(this)));

			return options;
		}

		/// <summary>
		/// Shows the options for this module
		/// </summary>
		public void ShowOptions()
		{
			// TODO: would be nicer if it opened on the launcher root page
			_appForm.ShowOptions();
		}

		/// <summary>
		/// Makes sure the magic words have been saved
		/// </summary>
		public void SaveMagicWords()
		{
			if (_magicWords != null)
			{
				string filename = FileLocations.Instance.MagicWordsFilename;
				_magicWords.SaveIfDirty(filename);
			}
		}

		/// <summary>
		/// Launch the specified magic words
		/// </summary>
		/// <param name="magicWords">Magic words to start</param>
		/// <param name="position">Which of the 4 positions to use</param>
		public void StartMagicWords(List<MagicWord> magicWords, int position)
		{
			ParameterMap map = new ParameterMap();
			foreach (MagicWord magicWord in magicWords)
			{
				StartMagicWord(magicWord, position, map);
			}
		}

		/// <summary>
		/// Starts a new process
		/// </summary>
		/// <param name="magicWord">The magic word being started</param>
		/// <param name="startPosition">The Start up position to use</param>
		/// <param name="map">A ParameterMap to use for any dynamic input</param>
		/// <returns>true if application started</returns>
		public bool Launch(MagicWord magicWord, StartupPosition startPosition, ParameterMap map)
		{
			return _startupHandler.Launch(magicWord, startPosition, map, StartupTimeout);
		}

		void StartMagicWord(MagicWord magicWord, int positionIndex1, ParameterMap map)
		{
			if (magicWord != null)
			{
				magicWord.UseCount++;
				magicWord.LastUsed = DateTime.Now;
				StartupPosition startPosition = magicWord.GetStartupPosition(positionIndex1);
				Launch(magicWord, startPosition, map);
			}
		}

		void enterMagicWordToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ShowEntryForm();
		}

		void addMagicWordToolStripMenuItem_Click(object sender, EventArgs e)
		{
			AddNewMagicWord(new MagicWord());
		}

		void AddNewMagicWordForActiveWindow()
		{
			MagicWord newMagicWord = new MagicWord();

			// get the active window
			IntPtr hWnd = NativeMethods.GetForegroundWindow();

			if (hWnd != IntPtr.Zero)
			{
				// use details from the active window to prefill a new MagicWord
				MagicWordForm.GetWindowDetails(hWnd, newMagicWord);
			}

			AddNewMagicWord(newMagicWord);
		}

		void AddNewMagicWord(MagicWord newMagicWord)
		{
			// let the user edit the details

			// need to activate, or the MagicWordForm will be hidden under whatever was the active window
			GetEntryForm().Activate();
			MagicWordForm dlg = new MagicWordForm(this, newMagicWord);
			if (dlg.ShowDialog() == DialogResult.OK)
			{
				// user wants this word, so insert it
				MagicWords.Insert(newMagicWord);
				SaveMagicWords();
			}
		}

		/// <summary>
		/// Handler for Activate - when no parameters
		/// Will show the entry form
		/// </summary>
		void ShowEntryForm()
		{
			GetEntryForm().ShowEntryForm();
		}

		/// <summary>
		/// Handler for Activate - when there are parameters
		/// Will treat the parameter as a magic word and try and run it
		/// </summary>
		/// <param name="parameters">Parameters for magic word</param>
		void ActivateMagicWord(string parameters)
		{
			List<MagicWord> magicWords = MagicWords.FindAllByAlias(parameters);
			int position = 1;	// could parse from parameters, but just use default position for now
			StartMagicWords(magicWords, position);
		}

		EntryForm GetEntryForm()
		{
			if (_entryForm == null)
			{
				_entryForm = new EntryForm(this, CommandRunner, _localEnvironment);
			}

			return _entryForm;
		}

		MagicWords GetMagicWords()
		{
			if (_magicWords == null)
			{
				_magicWords = new MagicWords(_logger);
				string filename = FileLocations.Instance.MagicWordsFilename;
				if (!_magicWords.Load(filename))
				{
					// magic word file doesn't exist
					// generate an initial set
					GenerateInitialMagicWords();
				}
			}

			return _magicWords;
		}

		void GenerateInitialMagicWords()
		{
			MagicWord mw = new MagicWord("Help", "http://dualmonitortool.sourceforge.net");
			MagicWords.Add(mw);

			AddAllInternalCommands();
			SaveMagicWords();
		}

		void AddAllInternalCommands()
		{
			MagicWord mw;
			IEnumerable<string> moduleNames = CommandRunner.GetModuleNames();
			foreach (string moduleName in moduleNames)
			{
				IEnumerable<string> actionNames = CommandRunner.GetModuleCommandNames(moduleName);
				foreach (string actionName in actionNames)
				{
					// make sure this alias hasn't already been defined
					if (MagicWords.FindByAlias(actionName) == null)
					{
						string description = CommandRunner.GetModuleActionDescription(moduleName, actionName);
						string magicCommand = MagicCommand.JoinMagicCommand(moduleName, actionName);
						mw = new MagicWord(actionName, magicCommand);
						mw.Comment = description;
						MagicWords.Add(mw);
					}
				}
			}
		}
	}
}
