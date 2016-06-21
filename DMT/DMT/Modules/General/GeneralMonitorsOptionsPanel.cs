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
	partial class GeneralMonitorsOptionsPanel : UserControl
	{
		GeneralModule _generalModule;

		/// <summary>
		/// Initialises a new instance of the <see cref="GeneralOptionsPanel" /> class.
		/// </summary>
		/// <param name="generalModule">The general module</param>
		public GeneralMonitorsOptionsPanel(GeneralModule generalModule)
		{
			_generalModule = generalModule;

			InitializeComponent();

			InitGrid();

		}

		void InitGrid()
		{
			dataGridView.ColumnCount = 0;
			dataGridView.RowCount = 12;

			int n = 0;
			dataGridView.Rows[n++].HeaderCell.Value = "Type";
			dataGridView.Rows[n++].HeaderCell.Value = "Number";
			dataGridView.Rows[n++].HeaderCell.Value = "Is Primary";
			dataGridView.Rows[n++].HeaderCell.Value = "Size";
			dataGridView.Rows[n++].HeaderCell.Value = "Bits per pixel";
			//dataGridView.Rows[n++].HeaderCell.Value = "Position";
			dataGridView.Rows[n++].HeaderCell.Value = "Area";
			dataGridView.Rows[n++].HeaderCell.Value = "Working area";
			dataGridView.Rows[n++].HeaderCell.Value = "Device name";
			dataGridView.Rows[n++].HeaderCell.Value = "Description";

			dataGridView.Rows[n++].HeaderCell.Value = "Brightness";
			dataGridView.Rows[n++].HeaderCell.Value = "Min brightness";
			dataGridView.Rows[n++].HeaderCell.Value = "Max brightness";

			//dataGridView.Rows[n++].HeaderCell.Value = "Handle";

			ShowCurrentInfo();
		}

		void ShowCurrentInfo()
		{
			bool showVirtualMonitors = checkBoxShowVirtual.Checked;
			List<MonitorProperties> allMonitorProperties = _generalModule.GetAllMonitorProperties(showVirtualMonitors);

			dataGridView.ColumnCount = allMonitorProperties.Count;

			for (int col = 0; col < allMonitorProperties.Count; col++)
			{
				MonitorProperties monitorProperties = allMonitorProperties[col];

				dataGridView.Columns[col].Width = 128;
				dataGridView.Columns[col].SortMode = DataGridViewColumnSortMode.NotSortable;

				string colHdr = "/";
				string type = "Unknown";
				int number = 0;
				if ((monitorProperties.MonitorType & MonitorProperties.EMonitorType.Physical) != 0)
				{
					type = "Physical";
					number = monitorProperties.PhysicalNumber;
					if (showVirtualMonitors)
					{
						colHdr = string.Format("{0}.{1}", monitorProperties.VirtualNumber, monitorProperties.ChildNumber);
					}
					else
					{
						colHdr = string.Format("{0}", monitorProperties.PhysicalNumber);
					}
				}
				else if ((monitorProperties.MonitorType & MonitorProperties.EMonitorType.Virtual) != 0)
				{
					type = "Virtual";
					number = monitorProperties.VirtualNumber;
					colHdr = string.Format("{0}", monitorProperties.VirtualNumber);
				}

				dataGridView.Columns[col].HeaderCell.Value = colHdr;

				int n = 0;
				dataGridView.Rows[n++].Cells[col].Value = type;
				dataGridView.Rows[n++].Cells[col].Value = number.ToString();

				dataGridView.Rows[n++].Cells[col].Value = monitorProperties.Primary ? "Yes" : "No";
				dataGridView.Rows[n++].Cells[col].Value = string.Format("{0} * {1}", monitorProperties.Bounds.Width, monitorProperties.Bounds.Height);
				dataGridView.Rows[n++].Cells[col].Value = monitorProperties.BitsPerPixel.ToString();
				//dataGridView.Rows[n++].Cells[col].Value = string.Format("({0}, {1})", monitorProperties.Bounds.Left, monitorProperties.Bounds.Top);
				dataGridView.Rows[n++].Cells[col].Value = string.Format("({0}, {1}) - ({2}, {3})", 
					monitorProperties.Bounds.Left, monitorProperties.Bounds.Top,
					monitorProperties.Bounds.Right - 1, monitorProperties.Bounds.Bottom - 1);
				dataGridView.Rows[n++].Cells[col].Value = string.Format("({0}, {1}) - ({2}, {3})",
					monitorProperties.WorkingArea.Left, monitorProperties.WorkingArea.Top,
					monitorProperties.WorkingArea.Right - 1, monitorProperties.WorkingArea.Bottom - 1);
				dataGridView.Rows[n++].Cells[col].Value = monitorProperties.DeviceName;
				dataGridView.Rows[n++].Cells[col].Value = monitorProperties.Description;

				dataGridView.Rows[n++].Cells[col].Value = monitorProperties.CurBrightness.ToString();
				dataGridView.Rows[n++].Cells[col].Value = monitorProperties.MinBrightness.ToString();
				dataGridView.Rows[n++].Cells[col].Value = monitorProperties.MaxBrightness.ToString();

				//dataGridView.Rows[n++].Cells[col].Value = monitorProperties.Handle.ToString("X");
			}
		}

		private void checkBoxShowVirtual_CheckedChanged(object sender, EventArgs e)
		{
			ShowCurrentInfo();
		}
	}
}
