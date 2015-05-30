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

using DMT.Library;
using DMT.Library.GuiUtils;
using DMT.Library.PInvoke;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;

namespace DMT.Modules.Launcher
{
	class StartupHandler
	{
		object _pendingLock = new object();
		// These are the processes we are waiting on so we can position each of their main windows
		List<StartupProcess> pendingMoves = new List<StartupProcess>();

		const int _pollInterval = 100;

		ICommandRunner _commandRunner;

		System.Timers.Timer _timer;

		public StartupHandler(Form appForm, ICommandRunner commandRunner)
		{
			_commandRunner = commandRunner;

			_timer = new System.Timers.Timer(_pollInterval);
			_timer.SynchronizingObject = appForm;
			_timer.Elapsed += new ElapsedEventHandler(Poll);
			_timer.AutoReset = true;
		}

		/// <summary>
		/// Starts a new process
		/// </summary>
		/// <param name="magicWord">The magic word being started</param>
		/// <param name="startPosition">The StartupPosition to use</param>
		/// <param name="map">A ParameterMap to use for any dynamic input</param>
		/// <returns>true if application started</returns>
		public bool Launch(MagicWord magicWord, StartupPosition startPosition, ParameterMap map, int startupTimeout)
		{
			bool ret = false;

			Win32.STARTUPINFO si = new Win32.STARTUPINFO();
			si.cb = (uint)Marshal.SizeOf(si);

			if (startPosition != null)
			{
				if (startPosition.EnablePosition)
				{
					si.dwX = (uint)startPosition.Position.Left;
					si.dwY = (uint)startPosition.Position.Top;
					si.dwXSize = (uint)startPosition.Position.Width;
					si.dwYSize = (uint)startPosition.Position.Height;
					si.dwFlags |= Win32.STARTF_USEPOSITION | Win32.STARTF_USESIZE;

					// TODO: this doesn't always work
					//si.wShowWindow = Win32.SW_HIDE;
					// and this can result in it being maximised on wrong screen
					//si.wShowWindow = (short)startPosition.ShowCmd;
					//si.dwFlags |= STARTF_USESHOWWINDOW;
				}
			}

			// want to start the thread suspended so we can get its pid as
			// soon as possible
			uint dwCreationFlags = Win32.NORMAL_PRIORITY_CLASS | Win32.CREATE_SUSPENDED;

			Win32.PROCESS_INFORMATION pi = new Win32.PROCESS_INFORMATION();

			// get the executable environment for the application
			MagicWordExecutable executable = new MagicWordExecutable(magicWord, _commandRunner, map);

			if (executable.InternalCommand)
			{
				_commandRunner.RunInternalCommand(executable.Executable, "");
				ret = true;
			}
			else
			{
				if (Win32.CreateProcess(executable.Executable, executable.CommandLine, IntPtr.Zero, IntPtr.Zero,
					false, dwCreationFlags, IntPtr.Zero,
					executable.WorkingDirectory,
					ref si, out pi))
				{
					// process has been created (but suspended)
					AddPendingMove(pi.dwProcessId, magicWord, startPosition, startupTimeout);
					Win32.ResumeThread(pi.hThread);

					ret = true;
				}
				else
				{
					int err = Marshal.GetLastWin32Error();
					string cmd;
					if (executable.CommandLine != null)
					{
						cmd = executable.CommandLine;
					}
					else
					{
						cmd = executable.Executable;
					}
					//Trace.WriteLine(string.Format("CreateProcess({0}) failed with {1}", cmd, err));
				}
			}

			return ret;
		}

		private void AddPendingMove(uint pid, MagicWord magicWord, StartupPosition startPosition, int startupTimeout)
		{
			// no need to add if user doesn't care where it opens
			if (startPosition != null)
			{
				if (startPosition.EnablePosition)
				{
					lock (_pendingLock)
					{
						pendingMoves.Add(new StartupProcess(pid, magicWord, startPosition, startupTimeout));
						if (!_timer.Enabled)
						{
							_timer.Enabled = true;
						}
					}
				}
			}
		}

		/// <summary>
		/// Needs to be called periodically so that we can check the list of
		/// applications that we are waiting on to show their main windows.
		/// </summary>
		/// <returns>true if more polls are required</returns>
		void Poll(object source, ElapsedEventArgs e)
		{
			lock (_pendingLock)
			{
				if (pendingMoves.Count > 0)
				{
					//Trace.WriteLine("Starting Poll");
					// need to check if this app has opened its top level window yet
					Win32.EnumWindows(new Win32.EnumWindowsProc(EnumWindowsCallback), 0);
				}

				// check if any we need to timeout while waiting for any of these windows to appear 
				foreach (StartupProcess pendingMove in pendingMoves)
				{
					if (pendingMove.ExpiryTime < DateTime.Now)
					{
						//Trace.WriteLine("Startup timedout");
						// lets give up waiting for this application to start
						// to simplify logic, we only remove at most 1 pending move per poll
						pendingMoves.Remove(pendingMove);
						break;
					}
				}

				// if there are any pending moves left, indicate that the polling needs to continue
				if (pendingMoves.Count <= 0)
				{
					// no longer need timer running
					_timer.Enabled = false;
				}

			}
		}

		/// <summary>
		/// callback from Win32.EnumWindows()
		/// </summary>
		/// <param name="hWnd"></param>
		/// <param name="lParam"></param>
		/// <returns></returns>
		public bool EnumWindowsCallback(IntPtr hWnd, uint lParam)
		{
			// get the pid associated with this hWnd
			uint pid;
			Win32.GetWindowThreadProcessId(hWnd, out pid);

			if (!Win32.IsWindowVisible(hWnd))
			{
				// ignore invisible windows
				return true;
			}

			foreach (StartupProcess pendingMove in pendingMoves)
			{
				if (pendingMove.Pid == pid)
				{
					if (MoveIfMatch(pendingMove, hWnd))
					{
						pendingMoves.Remove(pendingMove);
						break;
					}
				}
			}

			// If there are still pending moves, return true so that enumeration continues
			return (pendingMoves.Count > 0);
		}

		private bool MoveIfMatch(StartupProcess pendingMove, IntPtr hWnd)
		{

			Win32.WINDOWPLACEMENT windowPlacement = new Win32.WINDOWPLACEMENT();
			Win32.GetWindowPlacement(hWnd, ref windowPlacement);

			Rectangle vitrualDesktopRect = ScreenHelper.GetVitrualDesktopRect();

			Rectangle windowRectangle = ScreenHelper.RectToRectangle(ref windowPlacement.rcNormalPosition);
			if (!windowRectangle.IntersectsWith(vitrualDesktopRect))
			{
				// window has been deliberately positioned offscreen, so leave alone
				return false;
			}

			//Trace.WriteLine("Found candidate");
			if (!WindowMatches(hWnd, pendingMove))
			{
				// not the correct window
				return false;
			}
			//Trace.WriteLine("Found correct window");

			// This should always be true
			if (pendingMove.StartupPosition.EnablePosition)
			{
				windowPlacement.rcNormalPosition.left = pendingMove.StartupPosition.Position.Left;
				windowPlacement.rcNormalPosition.top = pendingMove.StartupPosition.Position.Top;
				windowPlacement.rcNormalPosition.right = pendingMove.StartupPosition.Position.Right;
				windowPlacement.rcNormalPosition.bottom = pendingMove.StartupPosition.Position.Bottom;

				// If the window is to be shown minimised or maximised, then we 
				// show it normally first, then minimised/maximised
				windowPlacement.showCmd = Win32.SW_SHOWNORMAL;
				Win32.SetWindowPlacement(hWnd, ref windowPlacement);

				// now minimise/maximise if needed
				if ((uint)pendingMove.StartupPosition.ShowCmd == Win32.SW_SHOWMAXIMIZED)
				{
					windowPlacement.showCmd = Win32.SW_SHOWMAXIMIZED;
					Win32.SetWindowPlacement(hWnd, ref windowPlacement);
				}
				else if ((uint)pendingMove.StartupPosition.ShowCmd == Win32.SW_SHOWMINIMIZED)
				{
					windowPlacement.showCmd = Win32.SW_SHOWMINIMIZED;
					Win32.SetWindowPlacement(hWnd, ref windowPlacement);
				}
			}

			// indicate that the window matches the pending move 
			// and that we no longer need to monitor this pending move
			return true;
		}

		private bool WindowMatches(IntPtr hWnd, StartupProcess pendingMove)
		{
			bool ret = true;

			// if window class specified, then check it matches
			if (pendingMove.WindowClass != null && pendingMove.WindowClass.Length > 0)
			{
				ret = false;
				int maxClassNameLen = 128;
				StringBuilder sb = new StringBuilder(maxClassNameLen);
				if (Win32.GetClassName(hWnd, sb, sb.Capacity) > 0)
				{
					string className = sb.ToString();
					ret = (className == pendingMove.WindowClass);
				}
			}

			if (ret)
			{
				// if caption expression specified, then check it matches
				if (pendingMove.CaptionRegExpr != null && pendingMove.CaptionRegExpr.Length > 0)
				{
					ret = false;
					string windowText = "";
					int textLen = Win32.GetWindowTextLength(hWnd);
					if (textLen > 0)
					{
						StringBuilder sb = new StringBuilder(textLen + 1);
						Win32.GetWindowText(hWnd, sb, sb.Capacity);
						windowText = sb.ToString();
					}

					try
					{
						Regex regex = new Regex(pendingMove.CaptionRegExpr, RegexOptions.IgnoreCase | RegexOptions.Singleline);
						if (regex.IsMatch(windowText))
						{
							ret = true;
						}
					}
					catch (Exception)
					{
						//
					}
				}
			}

			return ret;
		}
	}
}
