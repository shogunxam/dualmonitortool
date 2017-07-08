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
	/// Generates default SDAs using the numpad numbers to indicate position 
	/// </summary>
	class SdaGeneratorNumPad : ISdaGenerator
	{
		SwapScreenModule _swapScreenModule;

		uint[] ScreenBaseKeyCode;

		public SdaGeneratorNumPad(SwapScreenModule swapScreenModule, uint[] modifierList)
		{
			_swapScreenModule = swapScreenModule;

			if (modifierList.Length > 0)
			{
				ScreenBaseKeyCode = modifierList;
			}
		}

		/// <summary>
		/// Genertaes default system defined areas
		/// </summary>
		/// <param name="sdaControllers">Controllers for the system defined areas</param>
		/// <param name="allMonitors">All monitors</param>
		public void GenerateSdas(List<SdaController> sdaControllers, Monitors allMonitors, List<string> registrationErrors)
		{
			int screens = allMonitors.Count;
			Rectangle rect;
			string description;

			// for each screen
			for (int screenIndex = 0; screenIndex < allMonitors.Count; screenIndex++)
			{
				if (screenIndex == 0)
				{
					// we start with a supersized screen (covering all screens)
					SetDefaultSda(screenIndex, 0, allMonitors.WorkingArea, "Super size", sdaControllers, registrationErrors);
				}

				// need to add the rest so that the UDA indexes match the numeric key pad keys
				rect = allMonitors[screenIndex].WorkingArea;

				// 1 - bottom left quadrant
				description = string.Format("Screen {0} - bottom left quadrant", screenIndex + 1);
				SetDefaultSda(screenIndex, 1, rect.BottomLeftQuadrant(), description, sdaControllers, registrationErrors);

				// 2 - bottom half
				description = string.Format("Screen {0} - bottom half", screenIndex + 1);
				SetDefaultSda(screenIndex, 2, rect.BottomHalf(), description, sdaControllers, registrationErrors);

				// 3 - bottom right quadrant
				description = string.Format("Screen {0} - bottom right quadrant", screenIndex + 1);
				SetDefaultSda(screenIndex, 3, rect.BottomRightQuadrant(), description, sdaControllers, registrationErrors);

				// 4 - left half
				description = string.Format("Screen {0} - left half", screenIndex + 1);
				SetDefaultSda(screenIndex, 4, rect.LeftHalf(), description, sdaControllers, registrationErrors);

				// 5 - full screen
				description = string.Format("Screen {0}", screenIndex + 1);
				SetDefaultSda(screenIndex, 5, rect, description, sdaControllers, registrationErrors);

				// 6 - right half
				description = string.Format("Screen {0} - right half", screenIndex + 1);
				SetDefaultSda(screenIndex, 6, rect.RightHalf(), description, sdaControllers, registrationErrors);

				// 7 - top left quadrant
				description = string.Format("Screen {0} - top left quadrant", screenIndex + 1);
				SetDefaultSda(screenIndex, 7, rect.TopLeftQuadrant(), description, sdaControllers, registrationErrors);

				// 8 - top half
				description = string.Format("Screen {0} - top half", screenIndex + 1);
				SetDefaultSda(screenIndex, 8, rect.TopHalf(), description, sdaControllers, registrationErrors);

				// 9 - top right quadrant
				description = string.Format("Screen {0} - top right quadrant", screenIndex + 1);
				SetDefaultSda(screenIndex, 9, rect.TopRightQuadrant(), description, sdaControllers, registrationErrors);
			}
		}

		void SetDefaultSda(int screenIndex, int keyIndex, Rectangle rectangle, string description, List<SdaController> sdaControllers, List<string> registrationErrors)
		{
			// screenIndex is zero based
			// keyIndex is 0 for supersize on screen 0, otherwise it is 1-9
 
			int idx = 0;
			string commandName;
			if (keyIndex > 0)
			{
				idx = screenIndex * 9 + (keyIndex - 1) + 1;	// +1 as [0] is reserved for supersize
				commandName = string.Format("SDA{0}{1}", screenIndex + 1, keyIndex);
			}
			else
			{
				// supersize screen
				idx = 0;
				commandName  = "SDA0";
			}
			SdaController sdaController;
			if (idx < 0)
			{
				throw new ApplicationException(string.Format("SDA index: {0} should not be negative", idx));
			}
			else if (idx < sdaControllers.Count)
			{
				sdaController = sdaControllers[idx];
			}
			else if (idx == sdaControllers.Count)
			{
				sdaController = _swapScreenModule.CreateSdaController(commandName);
				sdaControllers.Add(sdaController);
			}
			else
			{
				throw new ApplicationException(string.Format("SDA index: {0} not consecutive to last: {1}", idx, sdaControllers.Count -1));
			}

			uint comboValue = KeyCombo.DisabledComboValue;
			// generate hotkey to use
			if (screenIndex < ScreenBaseKeyCode.Count())
			{
				// set modifier keys for this screen
				comboValue = ScreenBaseKeyCode[screenIndex];
				// and add the code for the required key on the numpad
				comboValue += (uint)Keys.NumPad0 + (uint)keyIndex;
				// and we want it automatically enabled
				comboValue |= KeyCombo.FlagEnabled;
			}

			// update the controller for the new values
			if (!sdaController.SetValues(comboValue, rectangle))
			{
				// and log the error
				KeyCombo keyCombo = new KeyCombo();
				keyCombo.ComboValue = comboValue;
				string msg = string.Format("{0} for {1}", keyCombo.ToString(), description);
				registrationErrors.Add(msg);
			}
		}
	}
}
