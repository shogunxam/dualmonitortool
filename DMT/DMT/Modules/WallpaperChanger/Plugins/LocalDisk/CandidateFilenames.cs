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

namespace DMT.Modules.WallpaperChanger.Plugins.LocalDisk
{
	using System;
	using System.Collections.Generic;
	using System.IO;
	using System.Linq;
	using System.Text;

	using DMT.Library.Utils;

	/// <summary>
	/// List of candidate image filenames
	/// </summary>
	public class CandidateFilenames
	{
		string _directory = null;
		bool _recursive = false;
		bool _cycle = false;
		List<string> _filenames = null;
		NumberCycler _filenameIndexCycler = null;

		/// <summary>
		/// Sets the directory to be searched
		/// </summary>
		/// <param name="directory">Directory to search</param>
		/// <param name="recursive">True if to search recursively through sub directories</param>
		public void SetDirectory(string directory, bool recursive, bool cycle)
		{
			// if Directory or recursive change, we must clear any cached filenames
			if (directory != _directory)
			{
				_directory = directory;
				_filenames = null;
			}

			if (recursive != _recursive)
			{
				_recursive = recursive;
				_filenames = null;
			}

			if (_cycle != cycle)
			{
				_cycle = cycle;
				// reset the cycling
				_filenameIndexCycler = null;
			}
		}

		/// <summary>
		/// Clears the cached search results
		/// </summary>
		public void ClearCache()
		{
			_filenames = null;
		}

		/// <summary>
		/// Gets a random file form the search results
		/// </summary>
		/// <returns></returns>
		public string GetRandomImage()
		{
			if (_filenames == null)
			{
				_filenames = GetCandidateFilenames(_directory, _recursive);
				_filenameIndexCycler = null;
			}

			if (_filenames.Count > 0)
			{
				if (_cycle)
				{
					// need to cycle through all, but in a random order
					if (_filenameIndexCycler == null || _filenameIndexCycler.Count == 0)
					{
						// need to start a new cycle
						_filenameIndexCycler = new NumberCycler(0, _filenames.Count - 1);
					}

					int index = _filenameIndexCycler.NextRandom();
					if (index >= 0 && index < _filenames.Count)
					{
						return _filenames[index];
					}
				}
				else
				{
					// choose one at random independent of previously returned images
					int index = RNG.Next(_filenames.Count);
					return _filenames[index];
				}
			}

			return null;
		}

		List<string> GetCandidateFilenames(string baseDirectory, bool recursive)
		{
			List<string> candidateFilenames = new List<string>();

			if (!string.IsNullOrEmpty(baseDirectory))
			{
				// check in case we are passed a single file rather than a directory
				if (File.Exists(baseDirectory))
				{
					FileInfo info = new FileInfo(baseDirectory);
					if (IsImageFile(info))
					{
						candidateFilenames.Add(info.FullName);
					}
				}
				else
				{
					AddCandidateFilenames(baseDirectory, recursive, candidateFilenames);
				}
			}

			return candidateFilenames;
		}

		void AddCandidateFilenames(string baseDirectory, bool recursive, List<string> candidateFilenames)
		{
			try
			{
				DirectoryInfo dir = new DirectoryInfo(baseDirectory);

				FileSystemInfo[] infos = dir.GetFileSystemInfos();
				foreach (FileSystemInfo info in infos)
				{
					if (info is FileInfo)
					{
						if (IsImageFile(info))
						{
							candidateFilenames.Add(info.FullName);
						}
					}
					else if (info is DirectoryInfo)
					{
						if (recursive)
						{
							AddCandidateFilenames(info.FullName, recursive, candidateFilenames);
						}
					}
				}
			}
			catch (Exception)
			{
				// ignore any i/o errors
			}
		}

		bool IsImageFile(FileSystemInfo info)
		{
			string extension = info.Extension.ToLower();
			switch (extension)
			{
				case ".jpg": // drop into ".jpeg"
				case ".jpeg":
					return true;
				case ".png":
					return true;
				case ".bmp":
					return true;
				case ".gif":
					return true;
			}

			return false;
		}
	}
}
