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

using DualMonitorTools.DualWallpaperChanger.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DualMonitorTools.DualWallpaperChanger
{
	public partial class AboutPluginForm : Form
	{
		Assembly _assembly;

		public AboutPluginForm(Assembly assembly, Image pluginImage)
		{
			_assembly = assembly;

			InitializeComponent();
			//  Initialize the AboutBox to display the product information from the assembly information.
			//  Change assembly information settings for your application through either:
			//  - Project->Properties->Application->Assembly Information
			//  - AssemblyInfo.cs
			this.Text = String.Format("About {0}", AssemblyTitle);
			//this.labelProductName.Text = String.Format("{0}  -  Version {1}", AssemblyProduct, AssemblyVersion);
			this.labelProductName.Text = String.Format("{0}  -  Version {1}", AssemblyTitle, AssemblyVersion);
			this.labelCopyright.Text = AssemblyCopyright;
			this.labelLicense.Text = Resources.License;
			this.labelDescription.Text = AssemblyDescription;
			if (pluginImage != null)
			{
				this.pictureBoxPlugin.Image = pluginImage;
			}
		}

		#region Assembly Attribute Accessors

		public string AssemblyTitle
		{
			get
			{
				// Get all Title attributes on this assembly
				object[] attributes = _assembly.GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
				// If there is at least one Title attribute
				if (attributes.Length > 0)
				{
					// Select the first one
					AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];
					// If it is not an empty string, return it
					if (!string.IsNullOrEmpty(titleAttribute.Title))
					{
						return titleAttribute.Title;
					}
				}
				// If there was no Title attribute, or if the Title attribute was the empty string, return the .exe name
				return System.IO.Path.GetFileNameWithoutExtension(_assembly.CodeBase);
			}
		}

		public string AssemblyVersion
		{
			get
			{
				return _assembly.GetName().Version.ToString();
			}
		}

		public string AssemblyDescription
		{
			get
			{
				// Get all Description attributes on this assembly
				object[] attributes = _assembly.GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
				// If there aren't any Description attributes, return an empty string
				if (attributes.Length == 0)
					return "";
				// If there is a Description attribute, return its value
				return ((AssemblyDescriptionAttribute)attributes[0]).Description;
			}
		}

		public string AssemblyCopyright
		{
			get
			{
				// Get all Copyright attributes on this assembly
				object[] attributes = _assembly.GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
				// If there aren't any Copyright attributes, return an empty string
				if (attributes.Length == 0)
					return "";
				// If there is a Copyright attribute, return its value
				return ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
			}
		}

		public string AssemblyCompany
		{
			get
			{
				// Get all Company attributes on this assembly
				object[] attributes = _assembly.GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
				// If there aren't any Company attributes, return an empty string
				if (attributes.Length == 0)
					return "";
				// If there is a Company attribute, return its value
				return ((AssemblyCompanyAttribute)attributes[0]).Company;
			}
		}
		#endregion
	}
}
