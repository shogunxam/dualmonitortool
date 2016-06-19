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

namespace DMT.Modules.General
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel;
	using System.Data;
	using System.Drawing;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;
	using System.Windows.Forms;

	using DMT.Library.Settings;
	using DMT.Library.Utils;
	using DMT.Library.Environment;

	/// <summary>
	/// Options panel for the general options for DMT
	/// </summary>
	partial class GeneralPropertiesOptionsPanel : UserControl
	{
		GeneralModule _generalModule;

		/// <summary>
		/// Initialises a new instance of the <see cref="GeneralOptionsPanel" /> class.
		/// </summary>
		/// <param name="generalModule">The general module</param>
		public GeneralPropertiesOptionsPanel(GeneralModule generalModule)
		{
			_generalModule = generalModule;

			InitializeComponent();

			InitGrid();

		}

		void InitGrid()
		{
			dataGridView.ColumnCount = 0;
			dataGridView.RowCount = 12;

			dataGridView.Rows[0].HeaderCell.Value = "Type";
			dataGridView.Rows[1].HeaderCell.Value = "Is Primary";
			dataGridView.Rows[2].HeaderCell.Value = "Size";
			dataGridView.Rows[3].HeaderCell.Value = "Bits per pixel";
			dataGridView.Rows[4].HeaderCell.Value = "Position";
			dataGridView.Rows[5].HeaderCell.Value = "Area";
			dataGridView.Rows[6].HeaderCell.Value = "Working area";
			dataGridView.Rows[7].HeaderCell.Value = "Device name";

			dataGridView.Rows[8].HeaderCell.Value = "Brightness";
			dataGridView.Rows[9].HeaderCell.Value = "Min brightness";
			dataGridView.Rows[10].HeaderCell.Value = "Max brightness";

			dataGridView.Rows[11].HeaderCell.Value = "Handle";

			ShowCurrentInfo();
		}

		void ShowCurrentInfo()
		{
			List<MonitorProperties> allMonitorProperties = _generalModule.GetAllMonitorProperties();

			dataGridView.ColumnCount = allMonitorProperties.Count;

			for (int col = 0; col < allMonitorProperties.Count; col++)
			{
				MonitorProperties monitorProperties = allMonitorProperties[col];

				dataGridView.Columns[col].Width = 128;
				dataGridView.Columns[col].SortMode = DataGridViewColumnSortMode.NotSortable;

				dataGridView.Columns[col].HeaderCell.Value = (col + 1).ToString();

				string type = "Unknown";
				if ((monitorProperties.MonitorType & MonitorProperties.EMonitorType.Physical) != 0)
				{
					type = "Physical";
				}
				else if ((monitorProperties.MonitorType & MonitorProperties.EMonitorType.Virtual) != 0)
				{
					type = "Virtial";
				}
				dataGridView.Rows[0].Cells[col].Value = type;
				dataGridView.Rows[1].Cells[col].Value = monitorProperties.Primary ? "Yes" : "No";
				dataGridView.Rows[2].Cells[col].Value = string.Format("{0} * {1}", monitorProperties.Bounds.Width, monitorProperties.Bounds.Height);
				dataGridView.Rows[3].Cells[col].Value = monitorProperties.BitsPerPixel.ToString();
				dataGridView.Rows[4].Cells[col].Value = string.Format("({0}, {1})", monitorProperties.Bounds.Left, monitorProperties.Bounds.Top);
				dataGridView.Rows[5].Cells[col].Value = string.Format("({0}, {1}) - ({2}, {3})", 
					monitorProperties.Bounds.Left, monitorProperties.Bounds.Top,
					monitorProperties.Bounds.Right - 1, monitorProperties.Bounds.Bottom - 1);
				dataGridView.Rows[6].Cells[col].Value = string.Format("({0}, {1}) - ({2}, {3})",
					monitorProperties.WorkingArea.Left, monitorProperties.WorkingArea.Top,
					monitorProperties.WorkingArea.Right - 1, monitorProperties.WorkingArea.Bottom - 1);
				dataGridView.Rows[7].Cells[col].Value = monitorProperties.DeviceName;

				dataGridView.Rows[8].Cells[col].Value = monitorProperties.CurBrightness.ToString();
				dataGridView.Rows[9].Cells[col].Value = monitorProperties.MinBrightness.ToString();
				dataGridView.Rows[10].Cells[col].Value = monitorProperties.MaxBrightness.ToString();

				dataGridView.Rows[11].Cells[col].Value = monitorProperties.Handle.ToString("X");
			}
		}
	}
}
