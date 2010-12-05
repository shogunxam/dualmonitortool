#region copyright
// This file is part of Dual Monitor Tools which is a set of tools to assist
// users with multiple monitor setups.
// Copyright (C) 2010  Gerald Evans
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
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;


namespace DualLauncher
{

	public class StartupController
	{

		public struct PROCESS_INFORMATION
		{
			public IntPtr hProcess;
			public IntPtr hThread;
			public uint dwProcessId;
			public uint dwThreadId;
		}

		public struct STARTUPINFO
		{
			public uint cb;
			public string lpReserved;
			public string lpDesktop;
			public string lpTitle;
			public uint dwX;
			public uint dwY;
			public uint dwXSize;
			public uint dwYSize;
			public uint dwXCountChars;
			public uint dwYCountChars;
			public uint dwFillAttribute;
			public uint dwFlags;
			public short wShowWindow;
			public short cbReserved2;
			public IntPtr lpReserved2;
			public IntPtr hStdInput;
			public IntPtr hStdOutput;
			public IntPtr hStdError;
		}


		public struct SECURITY_ATTRIBUTES
		{
			public int length;
			public IntPtr lpSecurityDescriptor;
			public bool bInheritHandle;
		}

		// flags for STARTUPINFO.dwFlags
		public const int STARTF_USESHOWWINDOW = 0x00000001;
		public const int STARTF_USESIZE = 0x00000002;
		public const int STARTF_USEPOSITION = 0x00000004;

		// flags for CreateProcess().dwCreationFlags
		public const uint CREATE_SUSPENDED = 0x00000004;
		public const uint NORMAL_PRIORITY_CLASS = 0x00000020;


		[DllImport("kernel32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool CreateProcess(string lpApplicationName, string lpCommandLine, 
		IntPtr lpProcessAttributes, IntPtr lpThreadAttributes,
		bool bInheritHandles, uint dwCreationFlags, IntPtr lpEnvironment,
		string lpCurrentDirectory, ref STARTUPINFO lpStartupInfo,out PROCESS_INFORMATION lpProcessInformation);

		[DllImport("kernel32.dll")]
		public static extern uint ResumeThread(IntPtr hThread);

		//[DllImport("user32.dll")]
		//[return: MarshalAs(UnmanagedType.Bool)]
		//static extern bool EnumWindows(EnumWindowsProc lpEnumFunc, IntPtr lParam);

		[DllImport("user32.dll")]
		private static extern IntPtr GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

		[DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
		private static extern int GetClassName(IntPtr hWnd, StringBuilder lpClassName, int nMaxCount);


		//private delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);


		// currently only allow 1 pending move
		private StartupProcess pendingMoves = null;

		#region Singleton framework
		// the single instance of the controller object
		static readonly StartupController instance = new StartupController();

		// Explicit static constructor to tell C# compiler
		// not to mark type as beforefieldinit
		static StartupController()
		{
		}

		StartupController()
		{
		}

		public static StartupController Instance
		{
			get
			{
				return instance;
			}
		}
		#endregion

		public bool Launch(MagicWord magicWord, StartupPosition startPosition)
		{
			bool ret = false;

//			StartupPosition startPosition = magicWord.StartupPosition1; // .StartPosition(position);

			STARTUPINFO si = new STARTUPINFO();
			si.cb = (uint)Marshal.SizeOf(si);

			if (startPosition != null)
			{
				//if (startPosition.HasLocation)
				//{
				//    si.dwX = (uint)startPosition.Location.X;
				//    si.dwY = (uint)startPosition.Location.Y;
				//    si.dwFlags |= STARTF_USEPOSITION;
				//}
				//if (startPosition.HasSize)
				//{
				//    si.dwXSize = (uint)startPosition.Size.Width;
				//    si.dwYSize = (uint)startPosition.Size.Height;
				//    si.dwFlags |= STARTF_USESIZE;
				//}
				if (startPosition.EnablePosition)
				{
					si.dwX = (uint)startPosition.Position.Left;
					si.dwY = (uint)startPosition.Position.Top;
					si.dwXSize = (uint)startPosition.Position.Width;
					si.dwYSize = (uint)startPosition.Position.Height;
					si.dwFlags |= STARTF_USEPOSITION | STARTF_USESIZE;
				}
			}

			// want to start the thread suspended so we can get its pid as
			// soon as possible
			uint dwCreationFlags = NORMAL_PRIORITY_CLASS | CREATE_SUSPENDED;

			PROCESS_INFORMATION pi = new PROCESS_INFORMATION();

			if (CreateProcess(magicWord.Filename, null, IntPtr.Zero, IntPtr.Zero,
				false, dwCreationFlags, IntPtr.Zero,
				null,
				ref si, out pi))
			{
				//AddPendingMove(pi.dwProcessId, startPosition);
				AddPendingMove(pi.dwProcessId, magicWord, startPosition);
				ResumeThread(pi.hThread);

				ret = true;
			}

			return ret;
		}

		//private void AddPendingMove(uint pid, StartupPosition startPosition)
		//{
		//    // no need to add if user doesn't care where it opens
		//    if (startPosition != null)
		//    {
		//        if (startPosition.HasLocation || startPosition.HasSize)
		//        {
		//            pendingMoves = new StartupProcess(pid, startPosition.Clone());
		//        }
		//    }
		//}
		private void AddPendingMove(uint pid, MagicWord magicWord, StartupPosition startPosition)
		{
			// no need to add if user doesn't care where it opens
			if (startPosition != null)
			{
				if (startPosition.EnablePosition)
				{
					pendingMoves = new StartupProcess(pid, magicWord, startPosition);
				}
			}
		}

		public bool Poll()
		{
			bool bPollAgain = false;

			if (pendingMoves != null)
			{
				Trace.WriteLine("Starting Poll");
				// need to check if this app has opened its top level window yet
				Win32.EnumWindows(new Win32.EnumWindowsProc(EnumWindowsCallback), 0);
			}

			return bPollAgain;
		}

		public bool EnumWindowsCallback(IntPtr hWnd, uint lParam)
		{
			if (pendingMoves == null)
			{
				// not waiting for any windows
				// this shouldn't happen anyway
				return false;
			}

			// get the pid associated with this hWnd
			uint pid;
			GetWindowThreadProcessId(hWnd, out pid);
			//Trace.WriteLine(string.Format("pid: {0} hWnd: {1}", pid, hWnd));

			if (pendingMoves.Pid != pid)
			{
				// window does not belong to the process we are interested in
				return true;
			}

			if (!Win32.IsWindowVisible(hWnd))
			{
				// ignore invisible windows
				return true;
			}

			Win32.WINDOWPLACEMENT windowPlacement = new Win32.WINDOWPLACEMENT();
			Win32.GetWindowPlacement(hWnd, ref windowPlacement);

			Rectangle vitrualDesktopRect = ScreenHelper.GetVitrualDesktopRect();

			Rectangle windowRectangle = ScreenHelper.RectToRectangle(ref windowPlacement.rcNormalPosition);
			if (!windowRectangle.IntersectsWith(vitrualDesktopRect))
			{
				// window has been deliberately positioned offscreen, so leave alone
				return true;
			}

			Trace.WriteLine("Found candidate");
			if (!WindowMatches(hWnd, pendingMoves))
			{
				// not the correct window
				return true;
			}
			Trace.WriteLine("Found correct window");

			//if (pendingMoves.StartupPosition.HasLocation)
			//{
			//    if (!pendingMoves.StartupPosition.HasSize)
			//    {
			//        // must keep the same size
			//        windowPlacement.rcNormalPosition.right += pendingMoves.StartupPosition.Location.X - windowPlacement.rcNormalPosition.left;
			//        windowPlacement.rcNormalPosition.bottom += pendingMoves.StartupPosition.Location.Y - windowPlacement.rcNormalPosition.top;
			//    }
			//    windowPlacement.rcNormalPosition.left = pendingMoves.StartupPosition.Location.X;
			//    windowPlacement.rcNormalPosition.top = pendingMoves.StartupPosition.Location.Y;
			//}
			//if (pendingMoves.StartupPosition.HasSize)
			//{
			//    // TODO check the -1 
			//    windowPlacement.rcNormalPosition.right = windowPlacement.rcNormalPosition.left + pendingMoves.StartupPosition.Size.Width - 1;
			//    windowPlacement.rcNormalPosition.bottom = windowPlacement.rcNormalPosition.top + pendingMoves.StartupPosition.Size.Height - 1;
			//}
			if (pendingMoves.StartupPosition.EnablePosition)
			{
				windowPlacement.rcNormalPosition.left = pendingMoves.StartupPosition.Position.Left;
				windowPlacement.rcNormalPosition.top = pendingMoves.StartupPosition.Position.Top;
				windowPlacement.rcNormalPosition.right = pendingMoves.StartupPosition.Position.Right;
				windowPlacement.rcNormalPosition.bottom = pendingMoves.StartupPosition.Position.Bottom;
			}
			Win32.SetWindowPlacement(hWnd, ref windowPlacement);


			// indicate job done
			pendingMoves = null;
			// no need to carry on with enumeration
			return false;
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
				if (GetClassName(hWnd, sb, sb.Capacity) > 0)
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
