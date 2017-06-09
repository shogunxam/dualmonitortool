#region copyright
// This file is part of Dual Monitor Tools which is a set of tools to assist
// users with multiple monitor setups.
// Copyright (C) 2017  Gerald Evans
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
using DMT.Library.Extensions;
using DMT.Library.HotKeys;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DMT.Modules.SwapScreen
{
	/// <summary>
	/// Generates default UDAs using the numpad numbers to indicate position 
	/// </summary>
	class UdaGeneratorNumPad : IUdaGenerator
	{
		bool _forceHotKeyRegeneration;

		uint[] ScreenBaseKeyCode =
		{
			KeyCombo.FlagControl,
			KeyCombo.FlagWin,
			KeyCombo.FlagAlt
		};

		public UdaGeneratorNumPad(bool forceHotKeyRegeneration)
		{
			_forceHotKeyRegeneration = forceHotKeyRegeneration;
		}

		/// <summary>
		/// Genertaes default user defined areas
		/// </summary>
		/// <param name="udaControllers">Controllers for the user defined areas</param>
		/// <param name="allMonitors">All monitors</param>
		public void GenerateDefaultUdas(List<UdaController> udaControllers, Monitors allMonitors)
		{
			int screens = allMonitors.Count;
			int idx = 0;
			Rectangle rect;
			string description;

			// for each screen
			for (int screen = 0; screen < allMonitors.Count; screen++)
			{
				// we start with a supersized screen (covering all screens)
				// we duplicate this for each screen, which seems a bit silly,
				// but it will mean each screen will take 10 UDAs and means
				// that the magic words for these will be easier to remember/use
				// as the first full screen would be UDA5, the next UDA15, then UDA25 etc.
				SetDefaultUda(idx, allMonitors.WorkingArea, "Super size", udaControllers);
				idx++;

				// need to add the rest so that the UDA indexes match the numeric key pad keys
				rect = allMonitors[screen].WorkingArea;

				// 1 - bottom left quadrant
				description = string.Format("Screen {0} - bottom left quadrant", screen + 1);
				SetDefaultUda(idx, rect.BottomLeftQuadrant(), description, udaControllers);
				idx++;

				// 2 - bottom half
				description = string.Format("Screen {0} - bottom half", screen + 1);
				SetDefaultUda(idx, rect.BottomHalf(), description, udaControllers);
				idx++;

				// 3 - bottom right quadrant
				description = string.Format("Screen {0} - bottom right quadrant", screen + 1);
				SetDefaultUda(idx, rect.BottomRightQuadrant(), description, udaControllers);
				idx++;

				// 4 - left half
				description = string.Format("Screen {0} - left half", screen + 1);
				SetDefaultUda(idx, rect.LeftHalf(), description, udaControllers);
				idx++;

				// 5 - full screen
				description = string.Format("Screen {0}", screen + 1);
				SetDefaultUda(idx, rect, description, udaControllers);
				idx++;

				// 6 - right half
				description = string.Format("Screen {0} - right half", screen + 1);
				SetDefaultUda(idx, rect.RightHalf(), description, udaControllers);
				idx++;

				// 7 - top left quadrant
				description = string.Format("Screen {0} - top left quadrant", screen + 1);
				SetDefaultUda(idx, rect.TopLeftQuadrant(), description, udaControllers);
				idx++;

				// 8 - top half
				description = string.Format("Screen {0} - top half", screen + 1);
				SetDefaultUda(idx, rect.TopHalf(), description, udaControllers);
				idx++;

				// 9 - top right quadrant
				description = string.Format("Screen {0} - top right quadrant", screen + 1);
				SetDefaultUda(idx, rect.TopRightQuadrant(), description, udaControllers);
				idx++;
			}
		}

		void SetDefaultUda(int idx, Rectangle rectangle, string description, List<UdaController> udaControllers)
		{
			if (idx >= 0 && idx < udaControllers.Count)
			{
				// it would be safer to pass these in, but we can work them out from idx
				// as long as we stick to 10 UDAs per screen
				int screenIndex = idx / 10;
				int keyIndex = idx % 10;

				UdaController udaController = udaControllers[idx];

				uint keyCode = KeyCombo.DisabledComboValue;
				if (udaController.HotKey.HotKeyCombo.ToPropertyValue() == KeyCombo.DisabledComboValue || _forceHotKeyRegeneration)
				{
					// no hotkey has been assigned to this
					if (screenIndex < ScreenBaseKeyCode.Count())
					{
						// set modifier keys for this screen
						keyCode = ScreenBaseKeyCode[screenIndex];
						// and add the code for the required key on the numpad
						keyCode += (uint)Keys.NumPad0 + (uint)keyIndex;
					}
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
