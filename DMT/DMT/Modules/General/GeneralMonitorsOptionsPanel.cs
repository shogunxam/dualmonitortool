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

		const int RowIdxIsActive = 0;
		const int RowIdxIsPrimary = 1;
		const int RowIdxSourceName = 2;
		const int RowIdxTargetName = 3;
		const int RowIdxDescription = 4;
		const int RowIdxSize = 5;
		const int RowIdxArea = 6;
		const int RowIdxWorkingArea = 7;
		const int RowIdxBpp = 8;
		const int RowIdxOutputTech = 9;
		const int RowIdxRotation = 10;
		const int RowIdxBrightness = 11;
		const int NumRows = 12;

		static Color EditableCellColour = Color.LightYellow;

		/// <summary>
		/// Initialises a new instance of the <see cref="GeneralOptionsPanel" /> class.
		/// </summary>
		/// <param name="generalModule">The general module</param>
		public GeneralMonitorsOptionsPanel(GeneralModule generalModule)
		{
			_generalModule = generalModule;

			InitializeComponent();

			InitGrid();

			//if (!generalModule.AllowShowVirtualMonitors)
			//{
			//	// hide the "Show virtual monitors" checkbox
			//	checkBoxShowAllMonitors.Visible = false;
			//}
		}

		void InitGrid()
		{
			dataGridView.ColumnCount = 0;
			dataGridView.RowCount = NumRows;

			dataGridView.Rows[RowIdxIsActive].HeaderCell.Value = "Is Active";
			dataGridView.Rows[RowIdxIsPrimary].HeaderCell.Value = "Is Primary";

			//dataGridView.Rows[n++].HeaderCell.Value = "Adapter name";
			dataGridView.Rows[RowIdxSourceName].HeaderCell.Value = "Source name";
			dataGridView.Rows[RowIdxTargetName].HeaderCell.Value = "Target name";
			//dataGridView.Rows[n++].HeaderCell.Value = "Device name";
			dataGridView.Rows[RowIdxDescription].HeaderCell.Value = "Description";

			dataGridView.Rows[RowIdxSize].HeaderCell.Value = "Size";
			dataGridView.Rows[RowIdxArea].HeaderCell.Value = "Area";
			dataGridView.Rows[RowIdxWorkingArea].HeaderCell.Value = "Woprking Area";
			dataGridView.Rows[RowIdxBpp].HeaderCell.Value = "Bits per Pixel";

			dataGridView.Rows[RowIdxOutputTech].HeaderCell.Value = "Output Tech";
			dataGridView.Rows[RowIdxRotation].HeaderCell.Value = "Rotation";
			dataGridView.Rows[RowIdxBrightness].HeaderCell.Value = "Brightness";

			ShowCurrentInfo();
		}

		public void ShowCurrentInfo()
		{
			labelErrorMsg.Text = string.Empty;
			try
			{
				//IList<DisplayDevice> allMonitorProperties = _generalModule.GetAllMonitorProperties();
				DisplayDevices displayDevices = _generalModule.GetDisplayDevices();
				IList<DisplayDevice> allMonitorProperties = displayDevices.Items;

				if (checkBoxShowAllMonitors.Checked)
				{
					dataGridView.ColumnCount = displayDevices.Count();
				}
				else
				{
					dataGridView.ColumnCount = displayDevices.ActiveCount();
				}

				DataGridViewCellStyle editableStyle = new DataGridViewCellStyle();
				editableStyle.BackColor = EditableCellColour;

				for (int col = 0; col < allMonitorProperties.Count; col++)
				{
					DisplayDevice monitorProperties = allMonitorProperties[col];

					if (monitorProperties.IsActive || checkBoxShowAllMonitors.Checked)
					{

						dataGridView.Columns[col].Width = 128;
						dataGridView.Columns[col].SortMode = DataGridViewColumnSortMode.NotSortable;

						string colHdr = (col + 1).ToString();

						dataGridView.Columns[col].HeaderCell.Value = colHdr;

						dataGridView.Rows[RowIdxIsActive].Cells[col].Value = YesNoText(monitorProperties.IsActive);
						dataGridView.Rows[RowIdxIsPrimary].Cells[col].Value = HideNonActive(monitorProperties, YesNoText(monitorProperties.IsPrimary));

						//dataGridView.Rows[n++].Cells[col].Value = monitorProperties.AdapterName;
						dataGridView.Rows[RowIdxSourceName].Cells[col].Value = monitorProperties.SourceName;
						dataGridView.Rows[RowIdxTargetName].Cells[col].Value = monitorProperties.FriendlyName;
						//dataGridView.Rows[n++].Cells[col].Value = monitorProperties.DeviceName;
						dataGridView.Rows[RowIdxDescription].Cells[col].Value = monitorProperties.Description;

						string size = string.Format("{0} * {1}", monitorProperties.Bounds.Width, monitorProperties.Bounds.Height);
						dataGridView.Rows[RowIdxSize].Cells[col].Value = HideNonActive(monitorProperties, size);

						string bounds = string.Format("({0}, {1}) - ({2}, {3})",
							monitorProperties.Bounds.Left, monitorProperties.Bounds.Top,
							monitorProperties.Bounds.Right - 1, monitorProperties.Bounds.Bottom - 1);
						dataGridView.Rows[RowIdxArea].Cells[col].Value = HideNonActive(monitorProperties, bounds);
						string workingArea = string.Format("({0}, {1}) - ({2}, {3})",
							monitorProperties.WorkingArea.Left, monitorProperties.WorkingArea.Top,
							monitorProperties.WorkingArea.Right - 1, monitorProperties.WorkingArea.Bottom - 1);
						dataGridView.Rows[RowIdxWorkingArea].Cells[col].Value = HideNonActive(monitorProperties, workingArea);

						//dataGridView.Rows[n++].Cells[col].Value = HideNonActive(monitorProperties, monitorProperties.BitsPerPixel == 0 ? "Unknown" : monitorProperties.BitsPerPixel.ToString());
						dataGridView.Rows[RowIdxBpp].Cells[col].Value = HideNonActive(monitorProperties, monitorProperties.BitsPerPixel.ToString());


						dataGridView.Rows[RowIdxOutputTech].Cells[col].Value = HideNonActive(monitorProperties, monitorProperties.OutputTechnology);
						dataGridView.Rows[RowIdxRotation].Cells[col].Value = HideNonActive(monitorProperties, monitorProperties.RotationDegrees.ToString());
						dataGridView.Rows[RowIdxBrightness].Cells[col].Value = HideNonActive(monitorProperties, monitorProperties.CurBrightness.ToString());

						// change the background colours of those cells that are currently editable
						if (monitorProperties.IsActive)
						{
							if (!monitorProperties.IsPrimary)
							{
								dataGridView.Rows[RowIdxIsPrimary].Cells[col].Style = editableStyle;
							}
							dataGridView.Rows[RowIdxBrightness].Cells[col].Style = editableStyle;
						}
					}
				}
			}
			catch (Exception ex)
			{
				// Shouldn't get here. but jic
				labelErrorMsg.Text = ex.Message;
			}
		}

		string YesNoText(bool flag)
		{
			return flag ? "Yes" : "No";
		}

		//string HideNonActive(MonitorProperties monitorProperties, string s)
		string HideNonActive(DisplayDevice monitorProperties, string s)
		{
			if (monitorProperties.IsActive)
			{
				return s;
			}

			return string.Empty;
		}

		private void checkBoxShowAll_CheckedChanged(object sender, EventArgs e)
		{
			ShowCurrentInfo();
		}

		private void dataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
		{
			int col = e.ColumnIndex;
			int row = e.RowIndex;

			if (row == RowIdxIsPrimary)
			{
				// primary 
				_generalModule.MakePrimary(col);
			}
			else if (row == RowIdxBrightness)
			{
				DisplayDevices displayDevices = _generalModule.GetDisplayDevices();
				if (col >= 0 && col < displayDevices.Items.Count)
				{
					DisplayDevice displayDevice = displayDevices.Items[col];

					BrightnessForm dlg = new BrightnessForm(displayDevices, col, displayDevice.CurBrightness, displayDevice.MinBrightness, displayDevice.MaxBrightness);
					if (dlg.ShowDialog(this) == DialogResult.OK)
					{
						// refresh grid in case brightness vale has been changed
						// as we currently don't get notifications when this changes
						ShowCurrentInfo();
					}
				}

			}
		}

		private void dataGridView_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
		{
			if (IsEditableCell(e))
			{
				dataGridView.Cursor = Cursors.Hand;
			}
		}

		private void dataGridView_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
		{
			if (IsEditableCell(e))
			{
				dataGridView.Cursor = Cursors.Default;
			}
		}

		bool IsEditableCell(DataGridViewCellEventArgs e)
		{
			if (e.RowIndex >= 0 && e.RowIndex < dataGridView.RowCount)
			{
				if (e.ColumnIndex >= 0 && e.ColumnIndex < dataGridView.ColumnCount)
				{
					// we could check if this cell should be editable, but would involve
					// determining the primary monitor and checking if monitor is enabled etc.,
					// but we cheat by checking the cell background colour, which we have already
					// set if the cell is editable
					DataGridViewCellStyle style = dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Style;
					if (style.BackColor == EditableCellColour)
					{
						return true;
					}
				}
			}

			return false;
		}
	}
}
