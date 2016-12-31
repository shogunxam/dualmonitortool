Dual Monitor Tools v2.5
-----------------------

Dual Monitor Tools is a collection of tools for users running multiple
monitor setups on Windows.

Currently the tools available are DMT.exe, DualWallpaper.exe and
DmtWallpaper.scr.

DMT.exe is the main tool which will reside in the Windows Notification Area
and contains the following modules:

Cursor - Control movement of cursor between monitors.
        The cursor can be locked to a monitor or made sticky, so extra
        effort is required to move it between monitors.

Launcher - Allows you to launch your favourite applications with a 
        few key strokes and to position them at pre-configured
        positions on any of your monitors.  All other DMT commands can
        also be run this way.

Snap -  Take snapshot of primary monitor and show on secondary.
        Previous snapshots can be viewed, saved to a file or copied
        to the clipboard.

Swap Screen - Allows easy movement of windows between monitors,
        and allows Windows to be moved to pre-defined positions.

Wallpaper Changer - Changes wallpaper periodically using a variety of
        sources and allows you to control how images are shown
        across your monitors.


DualWallpaper.exe allows the manual creation of wallpaper for multiple
monitors.

DmtWallpaper.scr works with DMT.exe to show your wallpaper when the screen
saver is running.

Dual Monitor Tools is free and has been released under the GPLv3 license.

For further information on usage of the tools, visit http://dualmonitortool.sourceforge.net


Changes in version 2.5
----------------------

DMT:Wallpaper changer fixed flicker when using smooth changing
DMT:SwapScreen added a parametised "ShowDesktop" command
DMT:Wallpaper changer added a new URL image provider
DMT:Wallpaper changer Unsplash image provider has more configuration
DMT:Wallpaper changer new option to cycle though all local disk images before repeating
DMT:SwapScreen new hotkey to show desktop that cursor is on
DMT:SwapScreen option to reset the UDAs
DMT:SwapScreen "ShowDesktop" now supports up to 16 monitors
DmtWallpaper screen saver added


Requirements
------------

Dual Monitor Tools should run on any version of Windows that has
.NET 4.0 or later installed.
Ideally you need two or more monitors attached to the computer, but some
of the features can be useful to users with a single monitor.


Installation
------------

There are 2 choices for installation:
1) Use the msi installer - this is the simplest method and will make
installing any future versions even easier.
2) Portable install from a zip file - this gives you the most flexibility
and is the route to use if you are installing to a portable device.


Msi Installation for new users
------------------------------

Just download DualMonitorTools-2.5.msi and run it.


Msi Installation upgrades
-------------------------

If you have previously installed DualMonitorTools using the Msi installer
then you can just download DualMonitorTools-2.5.msi and run it.
Alternatively, you can do this via the options in Dual Monitor Tools,
by clicking the "Check for Updates" button in the "Dual Monitor
Tools"->General page.


Msi installation for existing install done from a zip file
----------------------------------------------------------

With a msi installation, the executable and configuration files will live
in different locations, so there are some extra steps that you need to go
through if you wish to keep your existing configuration:
1)  In your existing version of DMT, make sure "Start when Windows starts"
    (found in Options->Dual Monitor Tools->General) is not ticked.
2)  Make sure DMT is not currently running.
3)  Make sure you have a backup copy of the following configuration files:
    DmtMagicWords.xml
    DmtSettings.xml
    DmtWallpaperProviders.xml
    These would normally be in the same directory that you previously installed
    Dual Monitor Tools too.
4)  Download and run the new msi installer.
5)  Double click on DMT in the notification area.
6)  Select the 'General' page under 'Dual Monitor Tools'.
    This will show the location where the configuration files now live.
    The files will be in the '%appdata%\Dual Monitor Tools' folder, which should
    be something like:
    'C:\Users\<your login name>\AppData\Roaming\Dual Monitor Tools'
7)  Stop DMT.
8)  Copy the 3 xml files into the above location.
9)  Start DMT. There should be an option in the Start menu to do this.
10) Don't forget to tick the "Start when Windows starts" option if required. 

Note: future updates will be much simpler as they can be performed from within
DMT.


Portable installation
---------------------

1)  Download the zip file.
2)  If you are already running the files from a previous version, then stop
    them.
3)  Unzip the zip file to your desired location.
4)  The tools are now ready to run.  You may need to give Windows permission
    to run the files as they were downloaded from the internet.


Hotkeys
-------

Most of the actions are accessible via hotkeys.  You will need to define 
hotkeys for the actions that you want to use.  Not all key combinations
are available as Windows reserves quite a few for itself, and each new
version of Windows grabs more and more of them.

Probably the most important hotkey to define is the 'Activate Magic
Word input' under Launcher->Hotkeys, as this will then allow all other
actions to be actioned by the use of this hotkey followed by a magic
word.  On a UK keyboard Win+<OemPipe> is a good hotkey to use for this
as it is easy to enter.  For other keyboard layouts, you may need to
experiment to see what is available and easy to enter.

On the same page is the 'Add Magic Word for current application'
hotkey.  If you press this hotkey, while running another application,
it will offer to create a magic word which you can then use in the future
as a quick wat of starting that application. Win+A is a nice hotkey for
this, but be aware that Windows 10 now uses this key combination.


Magic Words
-----------

You can associate applications, websites and DMT commands with a Magic
Word.  When you hit the launcher hotkey, a dialog box will pop up 
allowing you to start entering the magic word. This uses auto-completion
so you won't normally have to type in the whole word.

You can also assign the same Magic Word to multiple applications.
When you enter the Magic Word, all of the applications will be started.


Portable Usage
--------------

The tools are designed to be easy to use in a portable environment, so
they can be copied to a USB drive and ran direct from there.
Make sure you install from the zip file and not from the msi installer.

When specifying filenames (for the launcher executables and wallpaper
locations), you may want to make sure that any paths you enter are
relative paths if they exist on the USB drive to ensure portability.


Data Files
----------

In a portable installation, the tools will write to data files in the
same directory that the executables are.
The files written to are:

DmtMagicWords.xml - Magic words used by the Launcher.
DmtWallpaperProviders.xml - Specifies wallpaper image source.
DmtSettings.xml - Any other settings go in here.
- There will also be .bak versions of these files which are just
backup copies of the previous versions of the files.

In a msi installation, there will be a DmtFileLocations.xml in this 
directory that will specify where the other xml files live which would be in
"%APPDATA%\Dual Monitor Tools".


Uninstall of msi installation
-----------------------------

You can uninstall from within Windows installed application list.
Any saved settings will need to be removed manually by deleting
the folder '%APPDATA%\Dual Monitor Tools'.


Uninstall of portable installation
----------------------------------

If you have set the 'Start when Windows starts' in the options, then you
will need to turn this off before removing anything.

Then make sure none of the tools are running, and you can just delete
the files you unzipped and the data files mentioned above.


Known problems
--------------

If you wish DMT to fully work when you have windows from applications
that have been 'Run as Administrator', then you will also need to start
DMT with 'Run as Administrator'.

