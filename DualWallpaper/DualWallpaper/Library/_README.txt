DMT has a library of useful routines that need to be available to other tools,
without the need of duplicating the source code.

Several ways to achieve this:

1) Use a DLL
	+ Normally the best approach for many reasons
	- Does complicate the portable install; especially as we would need to change the permissions on the DLL if it is downloaded over the internet
	  which would rule out the user from simply downloading, unzipping and running.
	- Also have to make sure versions match etc., especially if using tools taken from different releases.

2) Use ILMerge: http://research.microsoft.com/en-us/people/mbarnett/ilmerge.aspx
	- May result in a larger executable(?), especially if you only wanted a few classes from the library.
	- ILMerge could get withdrawn at some point

3) Embed the DLL as a resource in the EXE: http://blogs.msdn.com/b/microsoft_press/archive/2010/02/03/jeffrey-richter-excerpt-2-from-clr-via-c-third-edition.aspx?PageIndex=5#comments
	- May slow down starting up time?
	- Would result in larger executables.

4) In the project file, link directly to the source files in the library project
	- Messy and need to rebuild everything if a library file is updated.

Option 4) has been chosen, as it is the simplest, and seeing as most tools are now merged into 
a single executable, shouldn't be that much of an issue.