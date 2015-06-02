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

using DMT.Library.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DMT.Library.HotKeys
{
	public class HotKeyRepository : IHotKeyService
	{
		Form _appForm;
		ISettingsService _settingsService;
		int _nextId;
		List<HotKeyController> _hotKeyControllers = new List<HotKeyController>();

		const int FIRST_HOTKEY_ID = 0x201;

		public HotKeyRepository(Form appForm, ISettingsService settingsService)
		{
			_appForm = appForm;
			_settingsService = settingsService;
			_nextId = FIRST_HOTKEY_ID;
		}

		public HotKeyController CreateHotKeyController(string moduleName, string settingName, string description, string win7Key, HotKey.HotKeyHandler handler)
		{
			HotKeyController hotKeyController = new HotKeyController(_appForm, _nextId, _settingsService, moduleName, settingName, description, win7Key, handler);
			_nextId++;
			_hotKeyControllers.Add(hotKeyController);
			return hotKeyController;

		}

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
