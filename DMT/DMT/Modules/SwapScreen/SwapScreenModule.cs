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
using DMT.Library.HotKeys;
using DMT.Resources;
using DMT.Library.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DMT.Library.GuiUtils;
using DMT.Library.Logging;

namespace DMT.Modules.SwapScreen
{
	class SwapScreenModule : Module
	{
		//const string _moduleName = "SwapScreen";

		ISettingsService _settingsService;
		//IHotKeyService _hotKeyService;
		ILogger _logger;

		// Active Window
		public HotKeyController NextScreenHotKeyController { get; protected set; }
		public HotKeyController PrevScreenHotKeyController { get; protected set; }
		public HotKeyController MinimiseHotKeyController { get; protected set; }
		public HotKeyController MaximiseHotKeyController { get; protected set; }
		public HotKeyController SupersizeHotKeyController { get; protected set; }
		public HotKeyController SwapTop2HotKeyController { get; protected set; }
		public HotKeyController SnapLeftHotKeyController { get; protected set; }
		public HotKeyController SnapRightHotKeyController { get; protected set; }
		public HotKeyController SnapUpHotKeyController { get; protected set; }
		public HotKeyController SnapDownHotKeyController { get; protected set; }

		// User Defined Areas
		const int _numUdaControllers = 10;
		public List<UdaController> UdaControllers { get; protected set; }

		// Other Windows
		public HotKeyController MinimiseAllButHotKeyController { get; protected set; }
		public HotKeyController RotateNextHotKeyController { get; protected set; }
		public HotKeyController RotatePrevHotKeyController { get; protected set; }
		public HotKeyController ShowDesktop1HotKeyController { get; protected set; }
		public HotKeyController ShowDesktop2HotKeyController { get; protected set; }
		public HotKeyController ShowDesktop3HotKeyController { get; protected set; }
		public HotKeyController ShowDesktop4HotKeyController { get; protected set; }


		public SwapScreenModule(ISettingsService settingsService, IHotKeyService hotKeyService, ILogger logger)
			: base(hotKeyService)
		{
			_settingsService = settingsService;
			//_hotKeyService = hotKeyService;
			_logger = logger;

			ModuleName = "SwapScreen";

			Start();
		}


		public override ModuleOptionNode GetOptionNodes()
		{
			ModuleOptionNodeBranch options = new ModuleOptionNodeBranch("Swap Screen", new SwapScreenRootOptionsPanel());
			options.Nodes.Add(new ModuleOptionNodeLeaf("Active Window", new SwapScreenActiveOptionsPanel(this)));
			options.Nodes.Add(new ModuleOptionNodeLeaf("User Defined Areas", new SwapScreenUdaOptionsPanel(this)));
			options.Nodes.Add(new ModuleOptionNodeLeaf("Other Windows", new SwapScreenOtherOptionsPanel(this)));

			return options;
		}


		void Start()
		{
			// Active Window
			NextScreenHotKeyController = AddCommand("NextScreen", SwapScreenStrings.NextScreenDescription, SwapScreenStrings.NextScreenWin7, ScreenHelper.MoveActiveToNextScreen);
			PrevScreenHotKeyController = AddCommand("PrevScreen", SwapScreenStrings.PrevScreenDescription, SwapScreenStrings.PrevScreenWin7, ScreenHelper.MoveActiveToNextScreen);
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
			// TODO: need a better way of handling n screens
			ShowDesktop1HotKeyController = AddCommand("ShowDesktop1", SwapScreenStrings.ShowDesktop1Description, SwapScreenStrings.ShowDesktop1Win7, ScreenHelper.ShowDesktop1);
			ShowDesktop2HotKeyController = AddCommand("ShowDesktop2", SwapScreenStrings.ShowDesktop2Description, SwapScreenStrings.ShowDesktop2Win7, ScreenHelper.ShowDesktop2);
			ShowDesktop3HotKeyController = AddCommand("ShowDesktop3", SwapScreenStrings.ShowDesktop3Description, SwapScreenStrings.ShowDesktop3Win7, ScreenHelper.ShowDesktop3);
			ShowDesktop4HotKeyController = AddCommand("ShowDesktop4", SwapScreenStrings.ShowDesktop4Description, SwapScreenStrings.ShowDesktop4Win7, ScreenHelper.ShowDesktop4);

			// User Defined Areas
			UdaControllers = new List<UdaController>();
			for (int idx = 0; idx < _numUdaControllers; idx++)
			{
				UdaControllers.Add(CreateUdaController(idx));
			}
			if (!_settingsService.SettingExists(ModuleName, UdaController.GetUdaMarkerSettingName()))
			{
				// no existing UDA settings, so generate some as a starting point for the user
				UdaHelper.GenerateDefaultUdas(UdaControllers);
				// and make sure these new settings are saved
				_settingsService.SaveSettings();
			}

			//AddCommandsForUDAs();
			//// Active Window
			//NextScreenHotKeyController = CreateHotKeyController("NextScreenHotKey", SwapScreenStrings.NextScreenDescription, SwapScreenStrings.NextScreenWin7, ScreenHelper.MoveActiveToNextScreen);
			//PrevScreenHotKeyController = CreateHotKeyController("PrevScreenHotKey", SwapScreenStrings.PrevScreenDescription, SwapScreenStrings.PrevScreenWin7, ScreenHelper.MoveActiveToNextScreen);
			//MinimiseHotKeyController = CreateHotKeyController("MinimiseHotKey", SwapScreenStrings.MinimiseDescription, SwapScreenStrings.MinimiseWin7, ScreenHelper.MinimiseActive);
			//MaximiseHotKeyController = CreateHotKeyController("MaximiseHotKey", SwapScreenStrings.MaximiseDescription, SwapScreenStrings.MaximiseWin7, ScreenHelper.MaximiseActive);
			//SupersizeHotKeyController = CreateHotKeyController("SupersizeHotKey", SwapScreenStrings.SupersizeDescription, SwapScreenStrings.SupersizeWin7, ScreenHelper.SupersizeActive);
			//SwapTop2HotKeyController = CreateHotKeyController("SwapTop2HotKey", SwapScreenStrings.SwapTop2Description, SwapScreenStrings.SwapTop2Win7, ScreenHelper.SwapTop2Windows);
			//SnapLeftHotKeyController = CreateHotKeyController("SnapLeftHotKey", SwapScreenStrings.SnapLeftDescription, SwapScreenStrings.SnapLeftWin7, ScreenHelper.SnapActiveLeft);
			//SnapRightHotKeyController = CreateHotKeyController("SnapRightHotKey", SwapScreenStrings.SnapRightDescription, SwapScreenStrings.SnapRightWin7, ScreenHelper.SnapActiveRight);
			//SnapUpHotKeyController = CreateHotKeyController("SnapUpHotKey", SwapScreenStrings.SnapUpDescription, SwapScreenStrings.SnapUpWin7, ScreenHelper.SnapActiveUp);
			//SnapDownHotKeyController = CreateHotKeyController("SnapDownHotKey", SwapScreenStrings.SnapDownDescription, SwapScreenStrings.SnapDownWin7, ScreenHelper.SnapActiveDown);

			//// User Defined Areas
			//UdaControllers = new List<UdaController>();
			//for (int idx = 0; idx < _numUdaControllers; idx++)
			//{
			//	UdaControllers.Add(CreateUdaController(idx));
			//}
			//if (!_settingsService.SettingExists(ModuleName, UdaController.GetUdaMarkerSettingName()))
			//{
			//	// no existing UDA settings, so generate some as a starting point for the user
			//	UdaHelper.GenerateDefaultUdas(UdaControllers);
			//	// and make sure these new settings are saved
			//	_settingsService.SaveSettings();
			//}

			//// Other Windows
			//MinimiseAllButHotKeyController = CreateHotKeyController("MinimiseAllButHotKey", SwapScreenStrings.MinimiseAllButDescription, SwapScreenStrings.MinimiseAllButWin7, ScreenHelper.MinimiseAllButActive);
			//RotateNextHotKeyController = CreateHotKeyController("RotateNextHotKey", SwapScreenStrings.RotateNextDescription, SwapScreenStrings.RotateNextWin7, ScreenHelper.RotateScreensNext);
			//RotatePrevHotKeyController = CreateHotKeyController("RotatePrevHotKey", SwapScreenStrings.RotatePrevDescription, SwapScreenStrings.RotatePrevWin7, ScreenHelper.RotateScreensPrev);
			//// TODO: need a better way of handling n screens
			//ShowDesktop1HotKeyController = CreateHotKeyController("ShowDesktop1HotKey", SwapScreenStrings.ShowDesktop1Description, SwapScreenStrings.ShowDesktop1Win7, ScreenHelper.ShowDesktop1);
			//ShowDesktop2HotKeyController = CreateHotKeyController("ShowDesktop2HotKey", SwapScreenStrings.ShowDesktop2Description, SwapScreenStrings.ShowDesktop2Win7, ScreenHelper.ShowDesktop2);
			//ShowDesktop3HotKeyController = CreateHotKeyController("ShowDesktop3HotKey", SwapScreenStrings.ShowDesktop3Description, SwapScreenStrings.ShowDesktop3Win7, ScreenHelper.ShowDesktop3);
			//ShowDesktop4HotKeyController = CreateHotKeyController("ShowDesktop4HotKey", SwapScreenStrings.ShowDesktop4Description, SwapScreenStrings.ShowDesktop4Win7, ScreenHelper.ShowDesktop4);
		}

		//HotKeyController CreateHotKeyController(string settingName, string description, string win7Key, HotKey.HotKeyHandler handler)
		//{
		//	return _hotKeyService.CreateHotKeyController(ModuleName, settingName, description, win7Key, handler);
		//}

		UdaController CreateUdaController(int idx)
		{
			string name = string.Format("UDA_{0}_", idx);
			// set hotKey to false as UdaController is responsible for registering it
			Command command = new Command(name, "", "", null, false, true);
			base.AddCommand(command);
			UdaController udaController = new UdaController(ModuleName, command, _settingsService, _hotKeyService);

			return udaController;
		}


	}
}
