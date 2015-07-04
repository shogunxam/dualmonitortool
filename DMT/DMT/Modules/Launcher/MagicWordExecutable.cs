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
using System.Text;
using System.IO;
using System.Drawing;
using System.Web;
using System.Windows.Forms;
using DMT.Library;
using DMT.Library.PInvoke;
using System.Net;
using DMT.Library.Html;

namespace DMT.Modules.Launcher
{
	/// <summary>
	/// Represents the executble and its environment for a particular instance
	/// of a magic word.
	/// This handles finding the correct executable when the magic word specifies a
	/// document/directory/url and performs any parameter substitution.
	/// </summary>
	public class MagicWordExecutable
	{
		MagicWord _magicWord;
		ICommandRunner _commandRunner;
		ParameterMap _map;
		bool _internalCommand = false;
		string _executable = null;
		string _commandLine = null;
		string _escapedCommandLine = null;
		string _workingDirectory = null;

		static string _explorerPath = null;

		/// <summary>
		/// ctor takes MagicWord and a ParameterMap, so that parameters
		/// can be shared when multiple MagicWords all have the same alias
		/// and are to be started together.
		/// </summary>
		/// <param name="magicWord">The MagicWord thet we need executable details for</param>
		/// <param name="map">The parameter map used to remember named parameters</param>
		public MagicWordExecutable(MagicWord magicWord, ICommandRunner commandRunner, ParameterMap map)
		{
			_magicWord = magicWord;
			_commandRunner = commandRunner;
			_map = map;
		}

		public bool InternalCommand
		{ 
			get
			{
				if (_executable == null)
				{
					GetExecutable();
				}
				return _internalCommand;
			}
		}


		/// <summary>
		/// Readonly full pathname to the executable that is going to be run.
		/// This will be different to the filename of the MagicWord when the filename
		/// represents a document/directory/url.
		/// </summary>
		public string Executable
		{
			get
			{
				if (_executable == null)
				{
					GetExecutable();
				}
				return _executable;
			}
		}

		/// <summary>
		/// Readonly icon associated with the executable that would be run.
		/// </summary>
		public Icon Icon
		{
			get
			{
				if (_executable == null)
				{
					GetExecutable();
				}
				Icon fileIcon = null;
				try
				{
					if (InternalCommand)
					{
						fileIcon = _commandRunner.GetInternalCommandIcon(_executable);
					}
					else
					{
						fileIcon = Icon.ExtractAssociatedIcon(_executable);
					}
				}
				catch (Exception)
				{
				}
				return fileIcon;
			}
		}

		/// <summary>
		/// Readonly command line including executable and any parameters.
		/// This will also expand any escapes asking the user to enter dynamic input
		/// when required.
		/// </summary>
		public string CommandLine
		{
			get
			{
				if (_executable == null)
				{
					GetExecutable();
				}

				if (_escapedCommandLine == null)
				{
					_escapedCommandLine = ExpandEscapes(_commandLine);
				}
				return _escapedCommandLine;
			}
		}

		/// <summary>
		/// Readonly working(starting) directory for the executable.
		/// </summary>
		public string WorkingDirectory
		{
			get
			{
				if (_executable == null)
				{
					GetExecutable();
				}
				return _workingDirectory;
			}
		}

		/// <summary>
		/// Gets the application associated with the specified extension.
		/// </summary>
		/// <param name="extension">Extension including the leading '.'</param>
		/// <returns></returns>
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

		// The full path to Windows Explorer
		static string ExplorerPath
		{
			get 
			{
				if (_explorerPath == null)
				{
					// TODO: is explorer always in WINDIR?
					string winDir = Environment.GetEnvironmentVariable("WINDIR");
					_explorerPath = Path.Combine(winDir, "explorer.exe");
				}
				return _explorerPath;
			}
		}

		// Determines the type of MagicWord and finds corresponding executable,
		// command line and working directory. 
		void GetExecutable()
		{
			if (_commandRunner.IsInternalCommand(_magicWord.Filename))
			{
				_internalCommand = true;
				_executable = _magicWord.Filename;
			}
			else
			{
				string extension = Path.GetExtension(_magicWord.Filename);

				if (File.Exists(_magicWord.Filename))
				{
					// looks like a file on the local computer
					// explicit check for .exe as we know these should be run directly
					if (string.Compare(extension, ".exe", true) == 0)
					{
						// the filename can be executed directly
						_executable = _magicWord.Filename;
					}
					else
					{
						_executable = GetAssociatedApp(extension);
						// I can't see this documented anywhere, but if the extension belongs
						// to something that can be run directly (.exe, .bat etc.) then "%1"
						// seems to be returned
						if (_executable == "%1")
						{
							_executable = _magicWord.Filename;
						}
						else
						{
							_commandLine = string.Format("\"{0}\" \"{1}\"", _executable, _magicWord.Filename);
						}
					}
				}
				else if (Directory.Exists(_magicWord.Filename))
				{
					// looks like a directory - which we will open with Windows Explorer
					_executable = ExplorerPath;
					_commandLine = string.Format("\"{0}\" \"{1}\"", _executable, _magicWord.Filename);
				}
				else
				{
					// assume it is a url to be opened by the browser
					_executable = GetAssociatedApp(".htm");
					//_commandLine = string.Format("\"{0}\" {1}", _executable, _magicWord.Filename);
					_commandLine = string.Format("\"{0}\" \"{1}\"", _executable, _magicWord.Filename);
				}

				// make sure we specify a command line
				if (_commandLine == null)
				{
					_commandLine = string.Format("\"{0}\"", _executable);
				}

				// add on any parameters (but don't expand any escapes) to the command line
				if (_magicWord.Parameters != null && _magicWord.Parameters.Length > 0)
				{
					_commandLine += " " + _magicWord.Parameters;
				}

				// get the working directory for the application
				if (_magicWord.StartDirectory != null && _magicWord.StartDirectory.Length > 0)
				{
					_workingDirectory = _magicWord.StartDirectory;
				}
				else
				{
					// use directory that the application exists in
					_workingDirectory = Path.GetDirectoryName(_executable);
				}
			}
		}

		// This expands any escapes, requesting user input where required
		string ExpandEscapes(string input)
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

		string GetNextSequence(string input, out int inputLengthTaken)
		{
			// find start of first escape
			int dollarIndex = input.IndexOf('$');

			if (dollarIndex == 0)
			{
				// find end of escape
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

		// replaces the parameter with dynamic input.
		// Note: the named parameter is without the leading or trailing $'s.
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
				parameterName = _magicWord.Comment;
			}

			// check if we already know this parameter value
			string parameterValue = _map.GetValue(parameterName);

			if (parameterValue == null)
			{
				// no we don't, so ask user
				ParameterInputForm dlg = new ParameterInputForm();
				dlg.ParameterPrompt = parameterName;
				dlg.ShowDialog();
				// there is no cancel
				parameterValue = dlg.ParameterValue;

				// save the value in the map (unencoded) in case it is needed later
				_map.SetValue(parameterName, parameterValue);
			}

			if (parameterType == 'W')
			{
				//return HttpUtility.UrlEncode(parameterValue);
				return HttpHelper.UrlEncode(parameterValue);
			}
			else // if (parameterType == 'I')
			{
				return parameterValue;
			}
		}
	}
}
