Dual Monitor Tools
------------------

Dual Monitor Tools is a collection of tools for users running multiple
monitor setups on Windows.

Currently the tools available are DMT and DualWallpaper.

For users of the 1.* versions, DMT now provides all of the functionality
of SwapScreen, DualLauncher, DualWallpaperChanger and DualSnap.

Dual Monitor Tools is free and has been released under the GPLv3 license.

For further information on usage of the tools, visit http://dualmonitortool.sourceforge.net


Release 2.1
------------

The release consists of the following files:

DMT.exe                     The main tool
DmtFileLocations-sample.xml Sample of the optional DmtFileLocations.xml
DualWallpaper.exe           The wallpaper creation tool
CHANGES.txt                 List of changes
COPYING.txt                 GPLv3 license
README.txt                  This file
THANKS.txt                  Contributors to the project


Requirements
------------

Dual Monitor Tools should run on any version of Windows that has
.NET 4.0 or later installed.
Ideally you need two or more monitors attached to the computer, but some
of the features can be useful to users with a single monitor.


Installation
------------

There is currently no installer, just download the zip, and unzip the
files into a suitable directory, and you are then ready to run them.


TOOL - DMT
----------

This is the main tool, currently consisting of 5 modules offering the 
following;

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

When run, it will reside in the Windows Notification Area.
Double click this icon to see all of the options available.
'Dual Monitor Tools'->'General' contains an option so start up
DMT every time Windows starts.

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
it will offer to create a hotkey for it. Win+A is a nice hotkey for
this, but be aware that Windows 10 now uses this key combination.

Magic Words
-----------

You can associate applications, websites and DMT commands with a Magic
Word.  When you hit the launcher hotkey, a dialog box will pop up 
allowing you to start entering the magic word. This uses auto-completion
so you won't normally have to type in the whole word.

You can also assign the same Magic Word to multiple applications.
When you enter the Magic Word, all of the applications will be started.

Importing Magic Words
---------------------

If you are a user of Dual Launcher, then you can import your existing
Magic Words into DMT using the options on the 'Import / Export' page.
Dual Launcher saves it's Magic Words using a path of the form:
C:\Users\<Your login name>\AppData\Local\GNE\DualLauncher\DualLauncher.xml
Just press 'Import XML; and select this file to import it.


TOOL - DualWallpaper
--------------------

Dual Wallpaper simplifies the process of using images as wallpaper on 
multiple monitor setups

You can have a single image which is spread across all of your monitors,
or you can have different images on each monitor, or if you have enough
monitors, you could say have an image spread across 2 of your monitors
with another image displayed on the third monitor.

It also correctly sets the wallpaper when your primary monitor is not
your leftmost or topmost monitor.


Portable Usage
--------------

The tools are designed to be easy to use in a portable environment, so
they can be copied to a USB drive and ran direct from there.

When specifying filenames (for the launcher executables and wallpaper
locations), you may want to make sure that any paths you enter are
relative paths if they exist on the USB drive to ensure portability.


Data Files
----------

In a default installation, the tools will write to data files in the
same directory that the executables are.
The files written to are:

DmtMagicWords.xml - Magic words used by the Launcher.
DmtWallpaperProviders.xml - Specifies wallpaper image source.
DmtSettings.xml - Any other settings go in here.
- There will also be .bak versions of these files which are just
backup copies of the previous versions of the files.

If you want the data files to reside in a different directory, for example
if you like all of you executables to be under "C:\Program Files (x86)"
which is a read-only directory, then this can be achieved by using 
DmtFileLocations.xml.  See DmtFileLocations-sample.xml for more details.


Uninstall
---------

If you have set the 'Start when Windows starts' in the options, then you
will need to turn this off before removing anything.

Then make sure none of the tools are running, and you can just delete
the files you unzipped and the data files mentioned above.


Known problems
--------------

If you wish DMT to fully work when you have windows from applications
that have been 'Run as Administrator', then you will also need to start
DMT with 'Run as Administrator'.

