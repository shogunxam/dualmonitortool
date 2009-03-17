Dual Monitor Tools
------------------

Dual Monitor Tools is a collection of tools for users running multiple
monitor setups on Windows.

Currently the only tools available are SwapScreen and DualSnap.

Dual Monitor Tools is free and has been released under the GPLv3 license.

Release 1.1
-----------

This release includes both SwapScreen and DualSnap.
There are no known problems with SwapScreen.  
However DualSnap needs a bit more work, but is still usable as it is.

The release consists of the following files:

SwapScreen.exe      The swap screen tool
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
It allows you to assign a hot key which when pressed will move the 
active window to the next screen.  This will also work if the active
window is a maximised window.

SwapScreen also has a context menu with options to minimise all windows 
on a particular screen and also to move all application windows to the
next screen.

If you want SwapScreen to start automatically when your computer boots,
then the easiest method is to paste a shortcut into the StartUp folder
on the Windows Start menu.

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

To uninstall the programs, you will need to remove any shortcuts you manually added, remove the files you unzipped and remove the configuration files.

The configuration file for SwapScreen is located at %LOCALAPPDATA%\GNE\SwapScreen.exe_Url_<sequence of chars>\1.1.0.0\user.config.
The configuration file for DualSnap is located at %LOCALAPPDATA%\GNE\DualSnap.exe_Url_<sequence of chars>\1.1.0.0\user.config.

If you have nothing else in %LOCALAPPDATA%\GNE that you want to keep, then it should be safe to delete this directory.


Known problems
--------------

The list of previous snaps in DualSnap doesn't scroll.

