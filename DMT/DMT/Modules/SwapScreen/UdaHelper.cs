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
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DMT.Modules.SwapScreen
{
	static class UdaHelper
	{
		public static void GenerateDefaultUdas(List<UdaController> udaControllers, Monitors allMonitors)
		{
			int screens = Screen.AllScreens.Length;
			int idx = 0;
			Rectangle rect;

			// start with a supersized screen for idx = 0
			// this ensures that for the full screens, idx will match the screen number
			rect = allMonitors.WorkingArea;
			SetDefaultUda(idx, rect.Left, rect.Top, rect.Width, rect.Height, "Super size", udaControllers);
			idx++;

			// add full screens
			for (int screen = 0; screen < Screen.AllScreens.Length; screen++)
			{
				rect = allMonitors[screen].WorkingArea;

				string description = string.Format("Screen {0}", screen + 1);
				SetDefaultUda(idx, rect.Left, rect.Top, rect.Width, rect.Height, description, udaControllers);
				idx++;
			}

			// add half screens
			for (int screen = 0; screen < Screen.AllScreens.Length; screen++)
			{
				rect = allMonitors[screen].WorkingArea;

				string description = string.Format("Screen {0} - left half", screen + 1);
				SetDefaultUda(idx, rect.Left, rect.Top, rect.Width / 2, rect.Height, description, udaControllers);
				idx++;

				description = string.Format("Screen {0} - right half", screen + 1);
				SetDefaultUda(idx, rect.Left + rect.Width / 2, rect.Top, rect.Width / 2, rect.Height, description, udaControllers);
				idx++;
			}

			// add quadrants
			for (int screen = 0; screen < Screen.AllScreens.Length; screen++)
			{
				rect = allMonitors[screen].WorkingArea;

				string description = string.Format("Screen {0} - top left quadrant", screen + 1);
				SetDefaultUda(idx, rect.Left, rect.Top, rect.Width / 2, rect.Height / 2, description, udaControllers);
				idx++;

				description = string.Format("Screen {0} - top right quadrant", screen + 1);
				SetDefaultUda(idx, rect.Left + rect.Width / 2, rect.Top, rect.Width / 2, rect.Height / 2, description, udaControllers);
				idx++;

				description = string.Format("Screen {0} - bottom left quadrant", screen + 1);
				SetDefaultUda(idx, rect.Left, rect.Top + rect.Height / 2, rect.Width / 2, rect.Height / 2, description, udaControllers);
				idx++;

				description = string.Format("Screen {0} - bottom right quadrant", screen + 1);
				SetDefaultUda(idx, rect.Left + rect.Width / 2, rect.Top + rect.Height / 2, rect.Width / 2, rect.Height / 2, description, udaControllers);
				idx++;
			}
		}

		static void SetDefaultUda(int idx, int left, int right, int width, int height, string description, List<UdaController> udaControllers)
		{
			if (idx >= 0 && idx < udaControllers.Count)
			{
				UdaController udaController = udaControllers[idx];

				uint keyCode = 0x0010030;	// Alt+0 - disabled
				keyCode += (uint)idx;

				Rectangle rectangle = new Rectangle(left, right, width, height);
				//udaController.InitFromProperty(UdaController.ToPropertyValue(keyCode, rectangle, description));

				// update the controller for the new values
				udaController.SetValues(keyCode, rectangle, description);
			}
		}
	}
}
