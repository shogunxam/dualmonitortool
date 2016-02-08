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

namespace DMT
{
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

	using DMT.Resources;

	/// <summary>
	/// The 'About' dialog
	/// </summary>
	public partial class AboutForm : Form
	{
		/// <summary>
		/// Initialises a new instance of the <see cref="AboutForm" /> class.
		/// </summary>
		public AboutForm()
		{
			InitializeComponent();

			// Initialize the AboutBox to display the product information from the assembly information.
			// Change assembly information settings for your application through either:
			// - Project->Properties->Application->Assembly Information
			// - AssemblyInfo.cs
			this.Text = string.Format("About {0}", AssemblyTitle);
			this.labelProductName.Text = string.Format("{0}  -  Version {1}", AssemblyTitle, AssemblyVersion);
			this.labelCopyright.Text = AssemblyCopyright;
			this.labelLicense.Text = CommonStrings.License;
			this.labelDescription.Text = AssemblyDescription;
		}

		#region Assembly Attribute Accessors

		/// <summary>
		/// Gets our name ("Dual Monitor Tools")
		/// </summary>
		public string AssemblyTitle
		{
			get
			{
				return CommonStrings.MyTitle;
			}
		}

		/// <summary>
		/// Gets our current version as a a string
		/// </summary>
		public string AssemblyVersion
		{
			get
			{
				return Assembly.GetExecutingAssembly().GetName().Version.ToString();
			}
		}

		/// <summary>
		/// Gets a short description of DMT
		/// </summary>
		public string AssemblyDescription
		{
			get
			{
				// Get all Description attributes on this assembly
				object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);

				// If there aren't any Description attributes, return an empty string
				if (attributes.Length == 0)
				{
					return string.Empty;
				}

				// If there is a Description attribute, return its value
				return ((AssemblyDescriptionAttribute)attributes[0]).Description;
			}
		}

		/// <summary>
		/// Gets the copyright notice
		/// </summary>
		public string AssemblyCopyright
		{
			get
			{
				// Get all Copyright attributes on this assembly
				object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);

				// If there aren't any Copyright attributes, return an empty string
				if (attributes.Length == 0)
				{
					return string.Empty;
				}

				// If there is a Copyright attribute, return its value
				return ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
			}
		}

		/// <summary>
		/// Gets our company name
		/// </summary>
		public string AssemblyCompany
		{
			get
			{
				// Get all Company attributes on this assembly
				object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);

				// If there aren't any Company attributes, return an empty string
				if (attributes.Length == 0)
				{
					return string.Empty;
				}

				// If there is a Company attribute, return its value
				return ((AssemblyCompanyAttribute)attributes[0]).Company;
			}
		}
		#endregion
	}
}
