Dual Monitor Tools
------------------

Dual Monitor Tools is a collection of tools for users running multiple
monitor setups on Windows.

Currently the only tools available are SwapScreen, DualWallpaper and DualSnap.

Dual Monitor Tools is free and has been released under the GPLv3 license.

Release 1.2
-----------


The release consists of the following files:

SwapScreen.exe      The swap screen tool
DualWallpaper.exe	The wallpaper setting tool
DualSnap.exe		The screen capture tool
CHANGES.txt			List of changes
COPYING.txt         GPLv3 license
README.txt          This file

Requirements
------------

Dual Monitor Tools should run on any implementation of Windows that has
.NET 2.0 installed.
Obviously you should have two or more monitors attached to the computer.

Installation
------------

There is currently no installer, so you need to copy the files yourself
to a suitable directory. 

SwapScreen
----------

This is a small tool that when started resides in the System Tray.
It allows you to assign hotkeys for various actions.
The main hotkey allows you to move the active window  to the next screen.
This will also work if the active window is a maximised window.
There are also some hotkeys emulate functionality that is built into
Windows 7, such as minimising and maximising the active window, so
users of XP and Vista can also use these.
Further hotkeys allow all windows on a particular monitor to be minimised
and to rotate the contents of the monitors.

If you want SwapScreen to start automatically when your computer boots,
then the easiest method is to paste a shortcut into the StartUp folder
on the Windows Start menu.

DualWallpaper
-------------

Dual Wallpaper simplifies the process of using images as wallpaper on 
multiple monitor setups

You can have a single image which is spread across all of your monitors,
or you can have different images on each monitor, or if you have enough
monitors, you could say have an image spread across 2 of your monitors
with another image displayed on the third monitor.

It also correctly sets the wallpaper when your primary monitor is not
your leftmost or topmost monitor.

DualSnap
--------

This is a small tool that when started resides in the System Tray.
It allows you to assign a hot key which when pressed will capture 
the image on the primary screen and (optionally) display it on the
secondary screen.

It remembers previous screen captures (up to a configurable number)
and these may be reviewed later on and either copied to the clipboard
or saved as a PNG file.

If you want DualSnap to start automatically when your computer boots,
then the easiest method is to paste a shortcut into the StartUp folder
on the Windows Start menu.


Uninstall
---------

To uninstall the programs, you will need to remove any shortcuts you
manually added, remove the files you unzipped and remove the
configuration and data files.

The configuration and data files for the tools may be found under:
%LOCALDATA%\GNE.
Typically this directory is something like C:\Users\<UserName>\AppData\Local. 

If you have nothing else in %LOCALAPPDATA%\GNE that you want to keep,
then it should be safe to delete this directory.


Known problems
--------------

No known problems.

