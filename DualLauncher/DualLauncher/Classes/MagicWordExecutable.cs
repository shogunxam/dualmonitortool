using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Drawing;

namespace DualLauncher
{
	public class MagicWordExecutable
	{
		private MagicWord magicWord;
		private string executable = null;
		private string commandLine = null;
		private string workingDirectory = null;

		public MagicWordExecutable(MagicWord magicWord)
		{
			this.magicWord = magicWord;
		}

		public string Executable
		{
			get
			{
				if (executable == null)
				{
					GetExecutable();
				}
				return executable;
			}
		}

		public Icon Icon
		{
			get
			{
				if (executable == null)
				{
					GetExecutable();
				}
				Icon fileIcon = null;
				try
				{
					fileIcon = Icon.ExtractAssociatedIcon(executable);
				}
				catch (Exception)
				{
				}
				return fileIcon;
			}
		}

		public string CommandLine
		{
			get
			{
				if (executable == null)
				{
					GetExecutable();
				}
				return commandLine;
			}
		}

		public string WorkingDirectory
		{
			get
			{
				if (executable == null)
				{
					GetExecutable();
				}
				return workingDirectory;
			}
		}

		private void GetExecutable()
		{
			string extension = Path.GetExtension(magicWord.Filename);

			if (File.Exists(magicWord.Filename))
			{
				// looks like a file on the local computer
				// explicit check for .exe as we know these should be run directly
				if (string.Compare(extension, ".exe", true) == 0)
				{
					// the filename can be executed directly
					executable = magicWord.Filename;
				}
				else
				{
					executable = GetAssociatedApp(extension);
					// I can't see this documented anywhere, but if the extension belongs
					// to something that can be run directly (.exe, .bat etc.) then "%1"
					// seems to be returned
					if (executable == "%1")
					{
						executable = magicWord.Filename;
					}
					else
					{
						commandLine = string.Format("\"{0}\" {1}", executable, magicWord.Filename);
					}
				}
			}
			else if (Directory.Exists(magicWord.Filename))
			{
				executable = "explorer.exe";
				commandLine = string.Format("explorer.exe {0}", magicWord.Filename);
			}
			else
			{
				// assume it is a url to be opened by the browser
				executable = GetAssociatedApp(".htm");
				commandLine = string.Format("\"{0}\" {1}", executable, magicWord.Filename);
			}

			if (magicWord.Parameters != null && magicWord.Parameters.Length > 0)
			{
				if (commandLine == null)
				{
					commandLine = string.Format("\"{0}\"", executable);
				}
				commandLine += " " + magicWord.Parameters;
			}

			if (magicWord.StartDirectory != null && magicWord.StartDirectory.Length > 0)
			{
				workingDirectory = magicWord.StartDirectory;
			}
			else
			{
				// use directory that the application exists in
				workingDirectory = Path.GetDirectoryName(executable);
			}

		}

		private string GetAssociatedApp(string extension)
		{

			// find length of buffer required to hold associated application
			uint pcchOut = 0;
			Win32.AssocQueryString(Win32.ASSOCF.ASSOCF_VERIFY, Win32.ASSOCSTR.ASSOCSTR_EXECUTABLE, extension, null, null, ref pcchOut);

			// allocate the buffer
			// pcchOut includes the '\0' terminator
			StringBuilder sb = new StringBuilder((int)pcchOut);

			// now get the app
			Win32.AssocQueryString(Win32.ASSOCF.ASSOCF_VERIFY, Win32.ASSOCSTR.ASSOCSTR_EXECUTABLE, extension, null, sb, ref pcchOut);

			return sb.ToString();

		}


	}
}
