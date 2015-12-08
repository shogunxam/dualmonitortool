using DMT.Library.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace DMT.Modules.WallpaperChanger.Plugins.LocalDisk
{
	public class CandidateFilenames
	{
		string _directory = null;
		bool _recursive = false;
		List<string> _filenames = null;

		public CandidateFilenames()
		{
		}

		public void SetDirectory(string directory, bool recursive)
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
		}

		public void ClearCache()
		{
			_filenames = null;
		}

		public string GetRandomImage()
		{
			if (_filenames == null)
			{
				_filenames = GetCandidateFilenames(_directory, _recursive);
			}

			if (_filenames.Count > 0)
			{
				// choose one at random
				int index = RNG.Next(_filenames.Count);
				return _filenames[index];
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
