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

namespace DMT.Modules.Launcher
{
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

	using DMT.Library;
	using DMT.Library.GuiUtils;
	using DMT.Library.PInvoke;

	/// <summary>
	/// Handles the starting up of processes
	/// and waits for them to appear so they can be moved to desired location
	/// </summary>
	class StartupHandler
	{
		const int PollInterval = 100;

		object _pendingLock = new object();

		// These are the processes we are waiting on so we can position each of their main windows
		List<StartupProcess> pendingMoves = new List<StartupProcess>();

		ICommandRunner _commandRunner;

		System.Timers.Timer _timer;

		/// <summary>
		/// Initialises a new instance of the <see cref="StartupHandler" /> class.
		/// </summary>
		/// <param name="appForm">The main (hidden) window</param>
		/// <param name="commandRunner">Command runner</param>
		public StartupHandler(Form appForm, ICommandRunner commandRunner)
		{
			_commandRunner = commandRunner;

			_timer = new System.Timers.Timer(PollInterval);
			_timer.SynchronizingObject = appForm;
			_timer.Elapsed += new ElapsedEventHandler(Poll);
			_timer.AutoReset = true;
		}

		/// <summary>
		/// Starts a new process
		/// </summary>
		/// <param name="magicWord">The magic word being started</param>
		/// <param name="startPosition">The start up position to use</param>
		/// <param name="map">A ParameterMap to use for any dynamic input</param>
		/// <param name="startupTimeout">Max time to monitor the start up process</param>
		/// <returns>true if application started</returns>
		public bool Launch(MagicWord magicWord, StartupPosition startPosition, ParameterMap map, int startupTimeout)
		{
			bool ret = false;

			NativeMethods.STARTUPINFO si = new NativeMethods.STARTUPINFO();
			si.cb = (uint)Marshal.SizeOf(si);

			if (startPosition != null)
			{
				if (startPosition.EnablePosition)
				{
					si.dwX = (uint)startPosition.Position.Left;
					si.dwY = (uint)startPosition.Position.Top;
					si.dwXSize = (uint)startPosition.Position.Width;
					si.dwYSize = (uint)startPosition.Position.Height;
					si.dwFlags |= NativeMethods.STARTF_USEPOSITION | NativeMethods.STARTF_USESIZE;

					// TODO: this doesn't always work
					// si.wShowWindow = Win32.SW_HIDE;
					// and this can result in it being maximised on wrong screen
					// si.wShowWindow = (short)startPosition.ShowCmd;
					// si.dwFlags |= STARTF_USESHOWWINDOW;
				}
			}

			// want to start the thread suspended so we can get its pid as
			// soon as possible
			uint dwCreationFlags = NativeMethods.NORMAL_PRIORITY_CLASS | NativeMethods.CREATE_SUSPENDED;

			NativeMethods.PROCESS_INFORMATION pi = new NativeMethods.PROCESS_INFORMATION();

			// get the executable environment for the application
			MagicWordExecutable executable = new MagicWordExecutable(magicWord, _commandRunner, map);

			if (executable.InternalCommand)
			{
				_commandRunner.RunInternalCommand(executable.Executable, string.Empty);
				ret = true;
			}
			else
			{
				if (NativeMethods.CreateProcess(
					executable.Executable, 
					executable.CommandLine, 
					IntPtr.Zero, 
					IntPtr.Zero,
					false, 
					dwCreationFlags, 
					IntPtr.Zero,
					executable.WorkingDirectory,
					ref si, 
					out pi))
				{
					// process has been created (but suspended)
					AddPendingMove(pi.dwProcessId, magicWord, startPosition, startupTimeout);
					NativeMethods.ResumeThread(pi.hThread);

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
				}
			}

			return ret;
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
			NativeMethods.GetWindowThreadProcessId(hWnd, out pid);

			if (!NativeMethods.IsWindowVisible(hWnd))
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
			return pendingMoves.Count > 0;
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
		/// <param name="source">source of the poll event</param>
		/// <param name="e">poll event arguments</param>
		void Poll(object source, ElapsedEventArgs e)
		{
			lock (_pendingLock)
			{
				if (pendingMoves.Count > 0)
				{
					// need to check if this app has opened its top level window yet
					NativeMethods.EnumWindows(new NativeMethods.EnumWindowsProc(EnumWindowsCallback), 0);
				}

				// check if any we need to timeout while waiting for any of these windows to appear 
				foreach (StartupProcess pendingMove in pendingMoves)
				{
					if (pendingMove.ExpiryTime < DateTime.Now)
					{
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

		private bool MoveIfMatch(StartupProcess pendingMove, IntPtr hWnd)
		{
			NativeMethods.WINDOWPLACEMENT windowPlacement = new NativeMethods.WINDOWPLACEMENT();
			NativeMethods.GetWindowPlacement(hWnd, ref windowPlacement);

			Rectangle vitrualDesktopRect = ScreenHelper.GetVitrualDesktopRect();

			Rectangle windowRectangle = ScreenHelper.RectToRectangle(ref windowPlacement.rcNormalPosition);
			if (!windowRectangle.IntersectsWith(vitrualDesktopRect))
			{
				// window has been deliberately positioned offscreen, so leave alone
				return false;
			}

			if (!WindowMatches(hWnd, pendingMove))
			{
				// not the correct window
				return false;
			}

			// This should always be true
			if (pendingMove.StartupPosition.EnablePosition)
			{
				windowPlacement.rcNormalPosition.left = pendingMove.StartupPosition.Position.Left;
				windowPlacement.rcNormalPosition.top = pendingMove.StartupPosition.Position.Top;
				windowPlacement.rcNormalPosition.right = pendingMove.StartupPosition.Position.Right;
				windowPlacement.rcNormalPosition.bottom = pendingMove.StartupPosition.Position.Bottom;

				// If the window is to be shown minimised or maximised, then we 
				// show it normally first, then minimised/maximised
				windowPlacement.showCmd = NativeMethods.SW_SHOWNORMAL;
				NativeMethods.SetWindowPlacement(hWnd, ref windowPlacement);

				// now minimise/maximise if needed
				if ((uint)pendingMove.StartupPosition.ShowCmd == NativeMethods.SW_SHOWMAXIMIZED)
				{
					windowPlacement.showCmd = NativeMethods.SW_SHOWMAXIMIZED;
					NativeMethods.SetWindowPlacement(hWnd, ref windowPlacement);
				}
				else if ((uint)pendingMove.StartupPosition.ShowCmd == NativeMethods.SW_SHOWMINIMIZED)
				{
					windowPlacement.showCmd = NativeMethods.SW_SHOWMINIMIZED;
					NativeMethods.SetWindowPlacement(hWnd, ref windowPlacement);
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
				if (NativeMethods.GetClassName(hWnd, sb, sb.Capacity) > 0)
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
					string windowText = string.Empty;
					int textLen = NativeMethods.GetWindowTextLength(hWnd);
					if (textLen > 0)
					{
						StringBuilder sb = new StringBuilder(textLen + 1);
						NativeMethods.GetWindowText(hWnd, sb, sb.Capacity);
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
						// TODO
					}
				}
			}

			return ret;
		}
	}
}
