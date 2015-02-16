Dual Monitor Tools
------------------

Dual Monitor Tools is a collection of tools for users running multiple
monitor setups on Windows.

Currently the tools available are SwapScreen, DualLauncher, DualWallpaper, 
DualWallpaperChanger, DisMon and DualSnap.

Dual Monitor Tools is free and has been released under the GPLv3 license.

For further information on usage of the tools, visit http://dualmonitortool.sourceforge.net

Release 1.10
------------

The release consists of the following files:

SwapScreen.exe              The swap screen tool
DualLauncher.exe            Application launcher
DualWallpaper.exe           The wallpaper setting tool
DualWallpaperChanger.exe    Wallpaper changer tool
DWC_Library.dll             Wallpaper changer plugin library
DWC_LocalDisk.dll           Wallpaper changer plugin to show images from 
                            local disk
DWC_RandomShapes.dll        Wallpaper changer plugin to create images from
                            random shapes
DWC_Unsplash.dll            Wallpaper changer plugin to download images from 
                            www.unspash.com
DisMon.exe                  Disables secondary monitors
DualSnap.exe                The screen capture tool
CHANGES.txt                 List of changes
COPYING.txt                 GPLv3 license
README.txt                  This file
THANKS.txt                  Contributors to the project

Requirements
------------

Dual Monitor Tools should run on any implementation of Windows that has
.NET 2.0 installed,but DualWallpaperChanger requires .NET 4.5 or later.
Ideally you need two or more monitors attached to the computer, but tools
like DualLauncher and DualWallpaperChanger can be useful to users with a
single monitor.

Installation
------------

There is currently no installer, just follow the following steps to 
manually install:

1) Download DualMonitorTools-1.10.zip to a folder on your computer.
2) Before unzipping this file, it is recommended that you mark it as safe
by right-clicking on it, selecting "Properties", click the 'Unblock' button 
and then 'OK'. This step is not essential, but will save hassle later on
especially when trying to use DualWallpaperChanger.
3) Extract the contents of the zip to a suitable folder on your computer.

Then just run the tools that you are interested in.

SwapScreen, DualLauncher, DualWallpaperChanger and DualSnap will all put
themselves in the Notification Area (System Tray) when run, so if you run them
and it looks like nothing has happened, you need to look in the Notification
Area.  These four tools all have an option 'Start when Windows starts' which 
does what it says.

SwapScreen
----------

This allows you to assign hotkeys for various actions.
The main hotkey allows you to move the active window to the next screen.
This will also work if the active window is a maximised window.
There are also some hotkeys to emulate functionality that is built into
Windows 7, such as minimising and maximising the active window, so
users of XP and Vista can also use these.
Further hotkeys allow all windows on a particular monitor to be minimised
and to rotate the contents of the monitors.
This tool also allows you to restrict mouse movement between screens.

DualLauncher
------------

Dual Launcher allows you to launch your favourite applications with a 
few key strokes and to position them at pre-configured positions on any
of your monitors.

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

DualWallpaperChanger
--------------------

Uses plugins to find images to use as wallpaper and changes the wallpaper
on a periodic basis and/or at start up. 

You can select which plugins to use and configure them as required and
give them different weights.
3 plugins are available at the moment:
LocalDisk: takes images from specified directory on your local disk.
Unspalsh: takes images from www.unsplash.com.
RandomShapes: creates wallpaper from random shapes.

If you find that no plugins are available, then the chances are that 
the DWC_*.dll files are all being blocked by Windows.
To fix this, right click on each of these files in turn,
select "Properties", click the 'Unblock' button and then 'OK'.

DisMon (Beta software)
------

This allows you to change the enabled monitors and/or to change the
primary monitor while an application is run, with the monitor state
being restored when the application has finished running.
The application to run is specified as the first parameter to DisMon.
Any further parameters are passed onto the application.
Remember to enclose any paths or parameters within double quotes if
they contain spaces.

There is also a GUI to disable monitors and/or change the primary
monitor manually.

DualSnap
--------

This allows you to assign a hotkey which when pressed will capture 
the image on the primary screen and (optionally) display it on the
secondary screen.

It remembers previous screen captures (up to a configurable number)
and these may be reviewed later on and either copied to the clipboard
or saved as PNG files.


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

If you wish SwapScreen to fully work when you have windows from applications
that have been 'Run as Administartor', then you will also need to start
SwapScreen with 'Run as Administrator'.

DisMon has problems on Windows 7 when the (initial) primary monitor is
disabled.  Also on Windows XP, the task bar does not move with the primary
monitor.

The dll plugin files used by DualWallpaperChanger must be unblocked
(Properties->Unblock) before they can be used.
