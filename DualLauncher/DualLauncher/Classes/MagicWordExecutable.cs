using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Drawing;
using System.Web;
using System.Windows.Forms;

namespace DualLauncher
{
	public class MagicWordExecutable
	{
		private MagicWord magicWord;
		private ParameterMap map;
		private string executable = null;
		private string commandLine = null;
		private string escapedCommandLine = null;
		private string workingDirectory = null;

		private static string explorerPath = null;

		public MagicWordExecutable(MagicWord magicWord, ParameterMap map)
		{
			this.magicWord = magicWord;
			this.map = map;
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
				//return commandLine;

				if (escapedCommandLine == null)
				{
					escapedCommandLine = ExpandEscapes(commandLine);
				}
				return escapedCommandLine;
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

		private static string ExplorerPath
		{
			get 
			{
				if (explorerPath == null)
				{
					// TODO: is explorer always in WINDIR?
					string winDir = Environment.GetEnvironmentVariable("WINDIR");
					explorerPath = Path.Combine(winDir, "explorer.exe");
				}
				return explorerPath;
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
				//executable = "explorer.exe";
				executable = ExplorerPath;
				commandLine = string.Format("\"{0}\" {1}", executable, magicWord.Filename);
			}
			else
			{
				// assume it is a url to be opened by the browser
				executable = GetAssociatedApp(".htm");
				commandLine = string.Format("\"{0}\" {1}", executable, magicWord.Filename);
			}

			// make sure we specify a command line
			if (commandLine == null)
			{
				commandLine = string.Format("\"{0}\"", executable);
			} 
			
			if (magicWord.Parameters != null && magicWord.Parameters.Length > 0)
			{
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

		public static string GetAssociatedApp(string extension)
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

		private string ExpandEscapes(string input)
		{
			string output = "";
			int inputLengthTaken;

			while (input.Length > 0)
			{
				output += GetNextSequence(input, out inputLengthTaken);
				input = input.Substring(inputLengthTaken);
			}

			return output;
		}

		private string GetNextSequence(string input, out int inputLengthTaken)
		{
			int dollarIndex = input.IndexOf('$');

			if (dollarIndex == 0)
			{
				int nextDollarIndex = input.IndexOf('$', 1);
				if (nextDollarIndex == 1)
				{
					// $$ is an escape for $ so return a single $
					inputLengthTaken = 2;
					return "$";
				}
				else if (nextDollarIndex > 1)
				{
					inputLengthTaken = nextDollarIndex + 1;
					return ReplaceParameter(input.Substring(1, nextDollarIndex - 1));
				}
				else
				{
					// no more $s so just return the sequence as is
					inputLengthTaken = input.Length;
					return input;
				}
			}
			else if (dollarIndex < 0)
			{
				// no $, so return all of string
				inputLengthTaken = input.Length;
				return input;
			}
			else
			{
				// return everything up to the $
				inputLengthTaken = dollarIndex;
				return input.Substring(0, dollarIndex);
			}
		}

		string ReplaceParameter(string parameter)
		{
			string parameterName;

			// first character of the parameter string is the parameter type
			char parameterType = Char.ToUpper(parameter[0]);
			
			if (parameter.Length > 1)
			{
				// anything after the parameter type is the prompt for the parameter
				parameterName = parameter.Substring(1);
			}
			else
			{
				// use the comment as the prompt (as SlickRun)
				parameterName = magicWord.Comment;
			}

			// check if we already know this parameter value
			string parameterValue = map.GetValue(parameterName);

			if (parameterValue == null)
			{
				// no we don't, so ask user
				ParameterInputForm dlg = new ParameterInputForm();
				dlg.ParameterPrompt = parameterName;
				dlg.ShowDialog();
				// there is no cancel
				parameterValue = dlg.ParameterValue;

				// save the value in the map (unencoded) in case it is needed later
				map.SetValue(parameterName, parameterValue);
			}

			if (parameterType == 'W')
			{
				return HttpUtility.UrlEncode(parameterValue);
			}
			else // if (parameterType == 'I')
			{
				return parameterValue;
			}
		}
	}
}
