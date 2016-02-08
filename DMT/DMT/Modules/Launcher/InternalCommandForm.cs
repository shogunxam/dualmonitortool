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

namespace DMT.Modules.Launcher
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel;
	using System.Data;
	using System.Drawing;
	using System.Linq;
	using System.Text;
	using System.Windows.Forms;

	using DMT.Library.Command;

	/// <summary>
	/// Dialog to select an internal command
	/// </summary>
	public partial class InternalCommandForm : Form
	{
		ICommandRunner _commandRunner;

		/// <summary>
		/// Initialises a new instance of the <see cref="InternalCommandForm" /> class.
		/// </summary>
		/// <param name="commandRunner">Command runner</param>
		/// <param name="magicCommand">Internal command to pre-select</param>
		public InternalCommandForm(ICommandRunner commandRunner, string magicCommand)
		{
			_commandRunner = commandRunner;
			InitializeComponent();

			string moduleName;
			string actionName;
			MagicCommand.SplitMagicCommand(magicCommand, out moduleName, out actionName);

			FillModuleList(moduleName);
			FillActionList(actionName);
			ReflectSelection();
		}

		/// <summary>
		/// Gets the selected internal command
		/// </summary>
		public string SelectedCommand { get; private set; }

		void FillModuleList(string selectedModuleName)
		{
			comboBoxModule.BeginUpdate();
			IEnumerable<string> moduleNames = _commandRunner.GetModuleNames();
			foreach (string moduleName in moduleNames)
			{
				int idx = comboBoxModule.Items.Add(moduleName);
				if (moduleName == selectedModuleName)
				{
					comboBoxModule.SelectedIndex = idx;
				}
			}

			comboBoxModule.EndUpdate();
		}

		void FillActionList(string selectedActionName)
		{
			comboBoxAction.BeginUpdate();
			comboBoxAction.Items.Clear();

			string selectedModule = GetSelectedName(comboBoxModule);

			IEnumerable<string> actionNames = _commandRunner.GetModuleCommandNames(selectedModule);
			if (actionNames != null && actionNames.Count() > 0)
			{
				foreach (string actionName in actionNames)
				{
					int idx = comboBoxAction.Items.Add(actionName);
					if (actionName == selectedActionName)
					{
						comboBoxAction.SelectedIndex = idx;
					}
				}
			}

			comboBoxAction.EndUpdate();
		}

		private void comboBoxModule_SelectedIndexChanged(object sender, EventArgs e)
		{
			FillActionList(string.Empty);
			ReflectSelection();
		}

		private void comboBoxCommand_SelectedIndexChanged(object sender, EventArgs e)
		{
			ReflectSelection();
		}

		void ReflectSelection()
		{
			// enable or disable the OK button depending on a valid module/action being selected
			string selectedModule = GetSelectedName(comboBoxModule);
			IEnumerable<string> actionNames = _commandRunner.GetModuleCommandNames(selectedModule);
			if (actionNames != null && actionNames.Count() > 0)
			{
				string selectedAction = GetSelectedName(comboBoxAction);
				if (actionNames.Contains(selectedAction))
				{
					SelectedCommand = MagicCommand.JoinMagicCommand(selectedModule, selectedAction);
					buttonOK.Enabled = true;
					string description = _commandRunner.GetModuleActionDescription(selectedModule, selectedAction);
					labelDescription.Text = description ?? string.Empty;
					return;
				}
			}

			SelectedCommand = null;
			buttonOK.Enabled = false;
			labelDescription.Text = string.Empty;
		}

		string GetSelectedName(ComboBox comboBox)
		{
			int idx = comboBox.SelectedIndex;
			if (idx >= 0)
			{
				return (string)comboBox.Items[idx];
			}

			return string.Empty;
		}
	}
}
