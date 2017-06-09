#region copyright
// This file is part of Dual Monitor Tools which is a set of tools to assist
// users with multiple monitor setups.
// Copyright (C) 2015-2017  Gerald Evans
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
using DMT.Library.Extensions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DMT.Modules.SwapScreen
{
	class UdaGeneratorOld : IUdaGenerator
	{
		/// <summary>
		/// Genertaes default user defined areas
		/// </summary>
		/// <param name="udaControllers">Controllers for the user defined areas</param>
		/// <param name="allMonitors">All monitors</param>
		public void GenerateDefaultUdas(List<UdaController> udaControllers, Monitors allMonitors)
		{
			int screens = Screen.AllScreens.Length;
			int idx = 0;
			Rectangle rect;

			// start with a supersized screen for idx = 0
			// this ensures that for the full screens, idx will match the screen number
			SetDefaultUda(idx, allMonitors.WorkingArea, "Super size", udaControllers);
			idx++;

			// add full screens
			for (int screen = 0; screen < Screen.AllScreens.Length; screen++)
			{
				string description = string.Format("Screen {0}", screen + 1);
				SetDefaultUda(idx, allMonitors[screen].WorkingArea, description, udaControllers);
				idx++;
			}

			// add half screens
			for (int screen = 0; screen < Screen.AllScreens.Length; screen++)
			{
				rect = allMonitors[screen].WorkingArea;

				string description = string.Format("Screen {0} - left half", screen + 1);
				SetDefaultUda(idx, rect.LeftHalf(), description, udaControllers);
				idx++;

				description = string.Format("Screen {0} - right half", screen + 1);
				SetDefaultUda(idx, rect.RightHalf(), description, udaControllers);
				idx++;
			}

			// add quadrants
			for (int screen = 0; screen < Screen.AllScreens.Length; screen++)
			{
				rect = allMonitors[screen].WorkingArea;

				string description = string.Format("Screen {0} - top left quadrant", screen + 1);
				SetDefaultUda(idx, rect.TopLeftQuadrant(), description, udaControllers);
				idx++;

				description = string.Format("Screen {0} - top right quadrant", screen + 1);
				SetDefaultUda(idx, rect.TopRightQuadrant(), description, udaControllers);
				idx++;

				description = string.Format("Screen {0} - bottom left quadrant", screen + 1);
				SetDefaultUda(idx, rect.BottomLeftQuadrant(), description, udaControllers);
				idx++;

				description = string.Format("Screen {0} - bottom right quadrant", screen + 1);
				SetDefaultUda(idx, rect.BottomRightQuadrant(), description, udaControllers);
				idx++;
			}
		}

		void SetDefaultUda(int idx, Rectangle rectangle, string description, List<UdaController> udaControllers)
		{
			if (idx >= 0 && idx < udaControllers.Count)
			{
				UdaController udaController = udaControllers[idx];

				//uint keyCode = 0x0010030;	// Alt+0 - disabled
				//keyCode += (uint)idx;
				uint keyCode = 0x0010030;	// Alt+0 - disabled
				if (udaController.HotKey.HotKeyCombo.ToPropertyValue() == KeyCombo.DisabledComboValue)
				{
					// no hotkey has been assigned to this
					keyCode = 0x0010030;	// Alt+0 - disabled
					keyCode += (uint)idx;
				}
				else
				{
					// keep existing code
					keyCode = udaController.HotKey.HotKeyCombo.ComboValue;
				}

				// update the controller for the new values
				udaController.SetValues(keyCode, rectangle, description);
			}
		}
	}
}
