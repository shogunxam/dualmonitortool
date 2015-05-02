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

using DMT.Modules;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DMT
{
	partial class OptionsForm : Form
	{
		IModuleService _moduleService;
		List<ModuleOptionNode> _optionNodes;
		ContainerControl _currentPanel = null;

		public OptionsForm(IModuleService moduleService)
		{
			_moduleService = moduleService;
			InitializeComponent();
		}

		private void OptionsForm_Load(object sender, EventArgs e)
		{
			GetOptionNodes();
			FillTree();
			treeViewOptions.ExpandAll();
		}

		private void OptionsForm_SizeChanged(object sender, EventArgs e)
		{
			if (_currentPanel != null)
			{
				Point pt = panelPlaceholder.Location;
				Size sz = panelPlaceholder.Size;

				_currentPanel.Location = pt;
				_currentPanel.Size = sz;
			}
		}

		private void buttonClose_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.OK;
			Close();
		}

		void GetOptionNodes()
		{
			//_optionNodes = ModuleRepository.Instance.GetOptionNodes(this);
			_optionNodes = _moduleService.GetOptionNodes(this);
		}

		void FillTree()
		{
			treeViewOptions.Nodes.Clear();
			AddOptionNodes(_optionNodes, treeViewOptions.Nodes);
			//foreach (ModuleOptionNode optionNode in _optionNodes)
			//{
			//	AddOptionNode(optionNode);
			//}
		}

		void AddOptionNodes(List<ModuleOptionNode> optionNodes, TreeNodeCollection treeNodes)
		{
			foreach (ModuleOptionNode optionNode in optionNodes)
			{
				if (optionNode is ModuleOptionNodeLeaf)
				{
					ModuleOptionNodeLeaf optionLeaf = optionNode as ModuleOptionNodeLeaf;
					TreeNode newTreeNode = treeNodes.Add(optionLeaf.Name);
					newTreeNode.Tag = optionLeaf.OptionPanel;
				}
				else if (optionNode is ModuleOptionNodeBranch)
				{
					ModuleOptionNodeBranch optionBranch = optionNode as ModuleOptionNodeBranch;
					TreeNode newTreeNode = treeNodes.Add(optionBranch.Name);
					AddOptionNodes(optionBranch.Nodes, newTreeNode.Nodes);
				}
			}
		}

		private void treeViewOptions_AfterSelect(object sender, TreeViewEventArgs e)
		{
			ShowPanelForSelectedNode();
		}

		void ShowPanelForSelectedNode()
		{
			TreeNode selectedNode = treeViewOptions.SelectedNode;
			if (selectedNode != null)
			{
				ShowPanelForNode(selectedNode);
			}
		}

		void ShowPanelForNode(TreeNode treeNode)
		{
			if (treeNode != null)
			{
				// we copy what Visual Studio options does, in that if a non-leaf option is selected
				// it displays the first leaf that is a descendant of this
				treeNode = FindFirstLeaf(treeNode);
				ContainerControl panel = treeNode.Tag as ContainerControl;
				if (panel != null)
				{
					Point pt = panelPlaceholder.Location;
					Size sz = panelPlaceholder.Size;

					if (panel != _currentPanel)
					{
						panel.Parent = this;
						panel.Location = pt;
						panel.Size = sz;
						panel.Visible = true;
						if (_currentPanel != null)
						{
							_currentPanel.Visible = false;
						}
						_currentPanel = panel;
					}
				}
			}
		}

		TreeNode FindFirstLeaf(TreeNode treeNode)
		{
			TreeNode curNode = treeNode;

			if (curNode != null)
			{
				// only leaf nodes have a ContainerControl as a tag
				ContainerControl panel;
				while ((panel = curNode.Tag as ContainerControl) == null)
				{
					// look at first child
					if (curNode.Nodes.Count < 1)
					{
						// something has gone wrong
						return null;
					}
					curNode = curNode.Nodes[0];
				}
			}

			return curNode;
		}
	}
}
