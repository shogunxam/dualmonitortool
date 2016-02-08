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

namespace DMT.Modules
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;
	using System.Windows.Forms;

	/// <summary>
	/// Interface for a module repository
	/// </summary>
	interface IModuleService
	{
		/// <summary>
		/// Adds the module to the repository
		/// </summary>
		/// <param name="module">Module to add</param>
		void AddModule(Module module);

		/// <summary>
		/// Start all modules in the repository
		/// </summary>
		void StartAllModules();

		/// <summary>
		/// Second stage start up.
		/// This is to let the modules know that the other modules have been started
		/// so they can make use of them if required.
		/// </summary>
		void StartUpComplete();

		/// <summary>
		/// Informs modules that the display resolution has changed
		/// </summary>
		void DisplayResolutionChanged();

		/// <summary>
		/// Gets an enumerator to enumerate over all modules
		/// </summary>
		/// <returns>Module enumerator</returns>
		IEnumerable<Module> GetAllModules();

		/// <summary>
		/// Gets all of the option nodes for all of the modules
		/// </summary>
		/// <returns>All option nodes</returns>
		IEnumerable<ModuleOptionNode> GetOptionNodes();

		/// <summary>
		/// Let all modules flush any data out to disk
		/// </summary>
		void FlushAllModules();

		/// <summary>
		/// Terminates all modules
		/// </summary>
		void TerminateAllModules();
	}
}
