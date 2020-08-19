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

namespace DMT.Modules.SwapScreen
{
	using System;
	using System.Collections.Generic;
	using System.Drawing;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;

	using DMT.Library;
	using DMT.Library.Environment;
	using DMT.Library.GuiUtils;
	using DMT.Library.HotKeys;
	using DMT.Library.Logging;
	using DMT.Library.Settings;
	using DMT.Resources;
	using System.Windows.Forms;
	using DMT.Library.Utils;

	/// <summary>
	/// Module for Swap Screen
	/// </summary>
	class SwapScreenModule : Module
	{
		// Number of User Defined Areas
		const int NumUdaControllers = 100;	// 10;

		// Maximum number of screens we support for 'Show Desktop'
		const int MaxNumScreens = 16;

		ISettingsService _settingsService;
		ILocalEnvironment _localEnvironment;
		ILogger _logger;

		SwapScreenSdaOptionsPanel _sdaOptionsPanel;

		/// <summary>
		/// Initialises a new instance of the <see cref="SwapScreenModule" /> class.
		/// </summary>
		/// <param name="settingsService">Settings repository</param>
		/// <param name="hotKeyService">Service for registering hot keys</param>
		/// <param name="localEnvironment">Local environment</param>
		/// <param name="logger">Application logger</param>
		public SwapScreenModule(ISettingsService settingsService, IHotKeyService hotKeyService, ILocalEnvironment localEnvironment, ILogger logger)
			: base(hotKeyService)
		{
			_settingsService = settingsService;
			_localEnvironment = localEnvironment;
			_logger = logger;

			ModuleName = "SwapScreen";
		}

#region Active Window hot keys

		/// <summary>
		/// Gets the controller for the 'Next Screen' hot key
		/// </summary>
		public HotKeyController NextScreenHotKeyController { get; private set; }

		/// <summary>
		/// Gets the controller for the 'Previous Screen' hot key
		/// </summary>
		public HotKeyController PrevScreenHotKeyController { get; private set; }

		/// <summary>
		/// Gets the controller for the 'Minimise window' hot key
		/// </summary>
		public HotKeyController MinimiseHotKeyController { get; private set; }

		/// <summary>
		/// Gets the controller for the 'Maximise window' hot key
		/// </summary>
		public HotKeyController MaximiseHotKeyController { get; private set; }

		/// <summary>
		/// Gets the controller for the 'super size window' hot key
		/// </summary>
		public HotKeyController SupersizeHotKeyController { get; private set; }

		/// <summary>
		/// Gets the controller for the 'swop top 2 windows' hot key
		/// </summary>
		public HotKeyController SwapTop2HotKeyController { get; private set; }

		/// <summary>
		/// Gets the controller for the 'snap window left' hot key
		/// </summary>
		public HotKeyController SnapLeftHotKeyController { get; private set; }

		/// <summary>
		/// Gets the controller for the 'snap window right' hot key
		/// </summary>
		public HotKeyController SnapRightHotKeyController { get; private set; }

		/// <summary>
		/// Gets the controller for the 'snap window up' hot key
		/// </summary>
		public HotKeyController SnapUpHotKeyController { get; private set; }

		/// <summary>
		/// Gets the controller for the 'snap window down' hot key
		/// </summary>
		public HotKeyController SnapDownHotKeyController { get; private set; }
#endregion

#region User and system defined areas hot keys

		/// <summary>
		/// Gets the controllers for the user defined areas hot keys
		/// </summary>
		public List<UdaController> UdaControllers { get; private set; }

		/// <summary>
		/// Gets the controllers for the system defined areas hot keys
		/// </summary>
		public List<SdaController> SdaControllers { get; private set; }

		/// <summary>
		/// Gets the last set of errors encountered when trying to register the system defined areas hot keys
		/// </summary>
		public List<string> SdaHotKeyErrors { get; private set; }

		/// <summary>
		/// Gets or sets a value indicating whether to enable the SDAs
		/// </summary>
		public bool EnableSdas
		{
			get { return EnableSdasSetting.Value; }
			set { EnableSdasSetting.Value = value; }
		}

		public bool BorderOutsideSda
		{
			get { return BorderOutsideSdaSetting.Value; }
			set { BorderOutsideSdaSetting.Value = value; }
		}

		public uint[] SdaModifiers
		{
			get { return ToModifierList(SdaModifiersSetting.Value); }
			set { SdaModifiersSetting.Value = FromModifierList(value); }
		}

#endregion

#region Other Windows hot keys

		/// <summary>
		/// Gets the controller for the 'minimise all but the active window' hot key
		/// </summary>
		public HotKeyController MinimiseAllButHotKeyController { get; private set; }

		/// <summary>
		/// Gets the controller for the 'rotate screens forwards' hot key
		/// </summary>
		public HotKeyController RotateNextHotKeyController { get; private set; }

		/// <summary>
		/// Gets the controller for the 'rotate screens backwards' hot key
		/// </summary>
		public HotKeyController RotatePrevHotKeyController { get; private set; }

		/// <summary>
		/// Gets the controller for the 'show desktop that cursor is on' hot key
		/// </summary>
		public HotKeyController ShowCursorDesktopHotKeyController { get; private set; }

		/// <summary>
		/// Gets the maximum number of desktops that can be configured
		/// </summary>
		public int MaxConfigurableDesktops { get; private set; }

		/// <summary>
		/// Gets the controller array for the 'show desktop on screen n' hot keys
		/// </summary>
		public HotKeyController[] ShowDesktopHotKeyControllers { get; private set; }

#endregion

		BoolSetting EnableSdasSetting { get; set; }
		BoolSetting BorderOutsideSdaSetting { get; set; }
		StringSetting SdaModifiersSetting { get; set; }


		/// <summary>
		/// Starts the swap screen module
		/// </summary>
		public override void Start()
		{
			// Active Window
			NextScreenHotKeyController = AddCommand("NextScreen", SwapScreenStrings.NextScreenDescription, SwapScreenStrings.NextScreenWin7, ScreenHelper.MoveActiveToNextScreen);
			PrevScreenHotKeyController = AddCommand("PrevScreen", SwapScreenStrings.PrevScreenDescription, SwapScreenStrings.PrevScreenWin7, ScreenHelper.MoveActiveToPrevScreen);
			MinimiseHotKeyController = AddCommand("Minimise", SwapScreenStrings.MinimiseDescription, SwapScreenStrings.MinimiseWin7, ScreenHelper.MinimiseActive);
			MaximiseHotKeyController = AddCommand("Maximise", SwapScreenStrings.MaximiseDescription, SwapScreenStrings.MaximiseWin7, ScreenHelper.MaximiseActive);
			SupersizeHotKeyController = AddCommand("Supersize", SwapScreenStrings.SupersizeDescription, SwapScreenStrings.SupersizeWin7, ScreenHelper.SupersizeActive);
			SwapTop2HotKeyController = AddCommand("SwapTop2", SwapScreenStrings.SwapTop2Description, SwapScreenStrings.SwapTop2Win7, ScreenHelper.SwapTop2Windows);
			SnapLeftHotKeyController = AddCommand("SnapLeft", SwapScreenStrings.SnapLeftDescription, SwapScreenStrings.SnapLeftWin7, ScreenHelper.SnapActiveLeft);
			SnapRightHotKeyController = AddCommand("SnapRight", SwapScreenStrings.SnapRightDescription, SwapScreenStrings.SnapRightWin7, ScreenHelper.SnapActiveRight);
			SnapUpHotKeyController = AddCommand("SnapUp", SwapScreenStrings.SnapUpDescription, SwapScreenStrings.SnapUpWin7, ScreenHelper.SnapActiveUp);
			SnapDownHotKeyController = AddCommand("SnapDown", SwapScreenStrings.SnapDownDescription, SwapScreenStrings.SnapDownWin7, ScreenHelper.SnapActiveDown);

			// Other Windows
			MinimiseAllButHotKeyController = AddCommand("MinimiseAllBut", SwapScreenStrings.MinimiseAllButDescription, SwapScreenStrings.MinimiseAllButWin7, ScreenHelper.MinimiseAllButActive);
			RotateNextHotKeyController = AddCommand("RotateNext", SwapScreenStrings.RotateNextDescription, SwapScreenStrings.RotateNextWin7, ScreenHelper.RotateScreensNext);
			RotatePrevHotKeyController = AddCommand("RotatePrev", SwapScreenStrings.RotatePrevDescription, SwapScreenStrings.RotatePrevWin7, ScreenHelper.RotateScreensPrev);

			MaxConfigurableDesktops = CalcMaxConfigurableDesktops();

			ShowDesktopHotKeyControllers = new HotKeyController[MaxConfigurableDesktops];
			for (int desktop = 0; desktop < MaxConfigurableDesktops; desktop++)
			{
				int desktop1 = desktop + 1;	// 1 based value
				string name = "ShowDesktop" + desktop1.ToString();
				string description = string.Format(SwapScreenStrings.ShowDesktopNDescription, desktop1);
				string win7 = "";

				// must create a new var for the closure
				int closureVar = desktop;
				HotKey.HotKeyHandler handler = delegate() { ScreenHelper.ShowDesktop(closureVar); };
				ShowDesktopHotKeyControllers[desktop] = AddCommand(name, description, win7, handler);
			}

			ShowCursorDesktopHotKeyController = AddCommand("ShowCursorDesktop", SwapScreenStrings.ShowCursorDesktopDescription, SwapScreenStrings.ShowCursorDesktopWin7, ShowCursorDesktop);

			// Generic ShowDesktop for use by magic words with a parameter
			AddCommand("ShowDesktop", SwapScreenStrings.ShowDesktopDescription, string.Empty, nop, ShowDesktop);

			// User Defined Areas
			UdaControllers = new List<UdaController>();
			for (int idx = 0; idx < NumUdaControllers; idx++)
			{
				UdaControllers.Add(CreateUdaController(idx));
			}

			// UDAs are now completely user defined.
			//if (!_settingsService.SettingExists(ModuleName, UdaController.GetUdaMarkerSettingName()))
			//{
			//	// no existing UDA settings, so generate some as a starting point for the user
			//	ResetUdas();
			//}

			// System Defined Areas
			SdaControllers = new List<SdaController>();
			// These will be initialised after all other hot keys have been registered

			SdaHotKeyErrors = new List<string>();

			EnableSdasSetting = new BoolSetting(_settingsService, ModuleName, "EnableSdas", false);
			// if it looks like we have transparent borders, default to them being outside the SDA
			bool defaultForBordersOutsideSda = _localEnvironment.IsWin8OrLater();
			BorderOutsideSdaSetting = new BoolSetting(_settingsService, ModuleName, "BorderOutsideSda", defaultForBordersOutsideSda);
			SdaModifiersSetting = new StringSetting(_settingsService, ModuleName, "SdaModifiers", "");
		}

		/// <summary>
		/// Indicates all modules have started, and allows
		/// us to register the SDAs without fear of grabbing a hotkey
		/// that is used by another module
		/// </summary>
		public override void StartUpComplete()
		{
			GenerateSDAs();
		}

		/// <summary>
		/// Called when the display resolution changes
		/// </summary>
		public override void DisplayResolutionChanged()
		{
			// The number or resultion of the screens may have changed, 
			// so regenerate the SDAs
			// TODO: We could remember the old sizes so that we only
			// regenerate the SDAs if the sizes has changed
			GenerateSDAs();
		}

		//public void ResetUdas()
		//{
		//	//UdaHelper.GenerateDefaultUdas(UdaControllers, _localEnvironment.Monitors);
		//	// TODO: need factory
		//	bool forceHotKeyRegeneration = true;
		//	bool enableHotKeys = true;
		//	//IUdaGenerator udaGenerator = new UdaGeneratorOld();
		//	ISdaGenerator udaGenerator = new SdaGeneratorNumPad(forceHotKeyRegeneration, enableHotKeys);
		//	udaGenerator.GenerateDefaultUdas(UdaControllers, _localEnvironment.Monitors);

		//	// and make sure these new settings are saved
		//	_settingsService.SaveSettings();
		//}

		/// <summary>
		/// Gets the option nodes for this module
		/// </summary>
		/// <returns>The root node</returns>
		public override ModuleOptionNode GetOptionNodes()
		{
			Image image = new Bitmap(Properties.Resources.SwapScreen_16_16);
			ModuleOptionNodeBranch options = new ModuleOptionNodeBranch("Swap Screen", image, new SwapScreenRootOptionsPanel());
			options.Nodes.Add(new ModuleOptionNodeLeaf("Active Window", image, new SwapScreenActiveOptionsPanel(this)));
			_sdaOptionsPanel = new SwapScreenSdaOptionsPanel(this);
			options.Nodes.Add(new ModuleOptionNodeLeaf("System Defined Areas", image, _sdaOptionsPanel));
			options.Nodes.Add(new ModuleOptionNodeLeaf("User Defined Areas", image, new SwapScreenUdaOptionsPanel(this)));
			options.Nodes.Add(new ModuleOptionNodeLeaf("Other Windows", image, new SwapScreenOtherOptionsPanel(this)));

			return options;
		}

		UdaController CreateUdaController(int idx)
		{
			// idx is zero based.
			// Unlike DualLauncher, the UDA names are also zero based
			// this avoids confusion between UDA10 and UDA1 when used as magic words
			string name = string.Format("UDA{0}", idx);

			// set hotKey to false as UdaController is responsible for registering it
			Command command = new Command(name, string.Empty, string.Empty, null, false, true);
			base.AddCommand(command);
			UdaController udaController = new UdaController(ModuleName, command, _settingsService, _hotKeyService);

			return udaController;
		}

		int CalcMaxConfigurableDesktops()
		{
			// This is the maximum of
			// the current number of screens
			// and the most the user has configured hot keys for in the past
			//int ret = Screen.AllScreens.Length;
			int ret = Monitor.AllMonitors.Count;

			for (int desktop = ret; desktop < MaxNumScreens; desktop++)
			{
				string settingName = string.Format("ShowDesktop{0}HotKey", desktop + 1);

				if (_settingsService.SettingExists(ModuleName, settingName))
				{
					ret = desktop + 1;
				}
			}

			return ret;
		}

		void ShowDesktop(string parameters)
		{
			int desktopNum;
			if (int.TryParse(parameters, out desktopNum))
			{
				// want zero based desktops
				ScreenHelper.ShowDesktop(desktopNum - 1);
			}
		}

		void ShowCursorDesktop()
		{
			Point oldCursorPosition = System.Windows.Forms.Cursor.Position;
			//Screen screen = Screen.FromPoint(System.Windows.Forms.Cursor.Position);
			// zero based
			//int screenIndex = ScreenHelper.FindScreenIndex(screen);
			int screenIndex = Monitor.AllMonitors.MonitorIndexFromPoint(System.Windows.Forms.Cursor.Position);
			Monitor screen = Monitor.AllMonitors[screenIndex];
			ScreenHelper.ShowDesktop(screenIndex);
		}

		#region System Defined Areas
		public void GenerateSDAs()
		{
			SdaHotKeyErrors.Clear();

			// disable the existing SDAs first
			foreach (SdaController sdaController in SdaControllers)
			{
				sdaController.Disable();
			}

			if (EnableSdas)
			{
				uint[] modifierList = ToModifierList(SdaModifiersSetting.Value);
				ISdaGenerator sdaGenerator = new SdaGeneratorNumPad(this, modifierList);
				//sdaGenerator.GenerateSdas(SdaControllers, _localEnvironment.Monitors, SdaHotKeyErrors);
				sdaGenerator.GenerateSdas(SdaControllers, Monitor.AllMonitors, SdaHotKeyErrors);
			}

			if (_sdaOptionsPanel != null)
			{
				_sdaOptionsPanel.ShowErrors();
			}
		}

		public SdaController CreateSdaController(string commandName)
		{
			// set hotKey to false as the SdaGenerator will be responsible for registering it
			Command command = new Command(commandName, string.Empty, string.Empty, null, false, true);
			base.AddCommand(command);
			SdaController sdaController = new SdaController(this, command, _hotKeyService);

			return sdaController;
		}

		uint[] ToModifierList(string text)
		{
			string[] fields = text.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
			uint[] modifiers = new uint[fields.Count()];
			for (int n = 0; n < fields.Count(); n++)
			{
				modifiers[n] = StringUtils.GetFieldAsUInt(fields, n, 0);
			}

			return modifiers;
		}

		string FromModifierList(uint[] modifiers)
		{
			StringBuilder sb = new StringBuilder();
			for (int n = 0; n < modifiers.Count(); n++)
			{
				if (n > 0)
				{
					sb.Append(",");
				}
				sb.Append(modifiers[n].ToString());
			}

			return sb.ToString();
		}

		#endregion
	}
}
