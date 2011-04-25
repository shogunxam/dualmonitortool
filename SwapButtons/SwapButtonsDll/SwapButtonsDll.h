// This file is part of Dual Monitor Tools which is a set of tools to assist
// users with multiple monitor setups.
// Copyright (C) 2011  Gerald Evans
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

// The following ifdef block is the standard way of creating macros which make exporting 
// from a DLL simpler. All files within this DLL are compiled with the SWAPBUTTONSDLL_EXPORTS
// symbol defined on the command line. This symbol should not be defined on any project
// that uses this DLL. This way any other project whose source files include this file see 
// SWAPBUTTONSDLL_API functions as being imported from a DLL, whereas this DLL sees symbols
// defined with this macro as being exported.
#ifdef SWAPBUTTONSDLL_EXPORTS
#define SWAPBUTTONSDLL_API __declspec(dllexport)
#else
#define SWAPBUTTONSDLL_API __declspec(dllimport)
#endif

// The following ifdef block allows exporting of the internal classes of the DLL
// so that they can be called directly by the calling app.
// This is purely for development so that instead of having the DLL injected
// into every running app which any bugs could crash, it is just loaded into the calling 
// application only.
// LOCALMODE_API needs to be defined as nothing when it is ready for a release
#ifdef SWAPBUTTONSDLL_EXPORTS
#define LOCALMODE_API __declspec(dllexport)
#else
#define LOCALMODE_API __declspec(dllimport)
#endif

extern "C"
{

SWAPBUTTONSDLL_API DWORD Hook(void);
SWAPBUTTONSDLL_API DWORD UnHook(void);

}