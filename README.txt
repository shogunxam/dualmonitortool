Dual Monitor Tools
------------------

Dual Monitor Tools is a collection of tools for users running multiple
monitor setups on Windows.

Currently the only tool available is SwapScreen.

Dual Monitor Tools is free and has been released under the GPLv3 license.

Release 1.0
-----------

This is the first release and should be considered as an alpha release 
but there are currently no known major problems.

It consists of the following files:

SwapScreen.exe      The swap screen tool
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

Uninstall
---------

To uninstall the program, you will need to remove any shortcuts you manually added, remove the files you unzipped and remove File Locater's configuration and index file.

The configuration file is located at %LOCALAPPDATA%\GNE\SwapScreen.exe_Url_<sequence of chars>\1.0.0.0\user.config.

If you have nothing else in %LOCALAPPDATA%\GNE, then it should be safe to delete this directory.


Known problems
--------------

None.

