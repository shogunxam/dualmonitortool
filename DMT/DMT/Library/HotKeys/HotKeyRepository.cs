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

namespace DMT.Library.HotKeys
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;
	using System.Windows.Forms;

	using DMT.Library.Settings;

	/// <summary>
	/// Repository for holding all of the hotkeys
	/// </summary>
	class HotKeyRepository : IHotKeyService
	{
		const int FirstHotkeyId = 0x201;

		Form _appForm;
		ISettingsService _settingsService;
		int _nextId;
		List<HotKeyController> _hotKeyControllers = new List<HotKeyController>();

		/// <summary>
		/// Initialises a new instance of the <see cref="HotKeyRepository" /> class.
		/// </summary>
		/// <param name="appForm">Main (hidden) window</param>
		/// <param name="settingsService">Settings repository</param>
		public HotKeyRepository(Form appForm, ISettingsService settingsService)
		{
			_appForm = appForm;
			_settingsService = settingsService;
			_nextId = FirstHotkeyId;
		}

		/// <summary>
		/// Creates a controller for a new hotkey
		/// </summary>
		/// <param name="moduleName">Module to own the hotkey</param>
		/// <param name="settingName">Setting name to save hotkey</param>
		/// <param name="description">Description of the hotkey</param>
		/// <param name="win7Key">Windows 7 description of the hotkey</param>
		/// <param name="handler">Hotkey handler</param>
		/// <returns>Controller for the hotkey</returns>
		public HotKeyController CreateHotKeyController(string moduleName, string settingName, string description, string win7Key, HotKey.HotKeyHandler handler)
		{
			HotKeyController hotKeyController = new HotKeyController(_appForm, _nextId, _settingsService, moduleName, settingName, description, win7Key, handler);
			_nextId++;
			_hotKeyControllers.Add(hotKeyController);
			return hotKeyController;
		}

		/// <summary>
		/// Stops and clears all hotkeys
		/// </summary>
		public void Stop()
		{
			foreach (HotKeyController hotKeyController in _hotKeyControllers)
			{
				hotKeyController.Dispose();
			}

			_hotKeyControllers.Clear();
		}
	}
}
