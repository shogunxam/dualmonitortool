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

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMT.Library.Utils
{
	class SafeFileWriter
	{
		string _filename;

		public SafeFileWriter(string filename)
		{
			_filename = filename;
		}

		public Stream OpenForWriting()
		{
			return File.Open(GetTempFilename(), FileMode.Create);
		}

		public void CompleteWrite()
		{
			// assumes stream returned by OpenForWriting() has been closed

			if (File.Exists(_filename))
			{
				// file already exists, so take a backup first

				string backupFilename = GetBackupFilename();
				if (File.Exists(backupFilename))
				{
					// a old backup already exists, so remove this
					File.Delete(backupFilename);
				}

				// can rename existing file as the backup file
				File.Move(_filename, backupFilename);
			}

			string tempFilename = GetTempFilename();

			// can now rename the newly written temp file to become the new file
			File.Move(tempFilename, _filename);
		}

		string GetTempFilename()
		{
			return Path.ChangeExtension(_filename, "tmp");
		}

		string GetBackupFilename()
		{
			return Path.ChangeExtension(_filename, "bak");
		}
	}
}
