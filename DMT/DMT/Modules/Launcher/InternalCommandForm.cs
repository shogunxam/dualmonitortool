using DMT.Library.Command;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DMT.Modules.Launcher
{
	public partial class InternalCommandForm : Form
	{
		ICommandRunner _commandRunner;

		public string SelectedCommand { get; protected set; }

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

		void FillModuleList(string selectedModuleName)
		{
			comboBoxModule.BeginUpdate();
			//comboBoxModule.Items.Add(Resources.LauncherStrings.SelectAModule);
			IEnumerable<string> moduleNames = _commandRunner.GetModuleNames();
			foreach (string moduleName in moduleNames)
			{
				int idx = comboBoxModule.Items.Add(moduleName);
				if (moduleName == selectedModuleName)
				{
					//comboBoxModule.SelectedText = selectedModuleName;
					comboBoxModule.SelectedIndex = idx;
				}
			}
			comboBoxModule.EndUpdate();
		}

		void FillActionList(string selectedActionName)
		{
			comboBoxAction.BeginUpdate();
			comboBoxAction.Items.Clear();

//			string selectedModule = comboBoxModule.SelectedText;
//			string selectedModule = (string)comboBoxModule.SelectedValue;
			string selectedModule = GetSelectedName(comboBoxModule);

			IEnumerable<string> actionNames = _commandRunner.GetModuleCommandNames(selectedModule);
			if (actionNames != null && actionNames.Count() > 0)
			{
				//comboBoxAction.Items.Add(Resources.LauncherStrings.SelectAnAction);
				foreach (string actionName in actionNames)
				{
					int idx = comboBoxAction.Items.Add(actionName);
					if (actionName == selectedActionName)
					{
						//comboBoxAction.SelectedText = selectedActionName;
						comboBoxAction.SelectedIndex = idx;
					}
				}
			}

			comboBoxAction.EndUpdate();
		}

		private void comboBoxModule_SelectedIndexChanged(object sender, EventArgs e)
		{
			FillActionList("");
			ReflectSelection();
		}

		private void comboBoxCommand_SelectedIndexChanged(object sender, EventArgs e)
		{
			ReflectSelection();
		}

		void ReflectSelection()
		{
			// enable or disable the OK button depending on a valid module/action being selected
			//string selectedModule = (string)comboBoxModule.SelectedValue;
			string selectedModule = GetSelectedName(comboBoxModule);
			IEnumerable<string> actionNames = _commandRunner.GetModuleCommandNames(selectedModule);
			if (actionNames != null && actionNames.Count() > 0)
			{
				//string selectedAction = (string)comboBoxAction.SelectedValue;
				string selectedAction = GetSelectedName(comboBoxAction);
				if (actionNames.Contains(selectedAction))
				{
					SelectedCommand = MagicCommand.JoinMagicCommand(selectedModule, selectedAction);
					buttonOK.Enabled = true;
					string description = _commandRunner.GetModuleActionDescription(selectedModule, selectedAction);
					labelDescription.Text = description ?? "";
					return;
				}
			}

			SelectedCommand = null;
			buttonOK.Enabled = false;
			labelDescription.Text = "";
		}

		string GetSelectedName(ComboBox comboBox)
		{
			int idx = comboBox.SelectedIndex;
			if (idx >= 0)
			{
				return (string)comboBox.Items[idx];
			}

			return "";
		}
	}
}
