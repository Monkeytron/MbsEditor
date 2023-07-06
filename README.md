# MbsEditor
View and edit .mbs data files from stencyl games.
***
Building on the code in [mbs-io](https://github.com/Monkeytron/mbs-io) this program has a simple interface for viewing and editing data from mbs files.

### Current features

- Can open the scene data files (eg scene-10.mbs) and view the data within them.
- The positions of all level terrain collision and any actors in the scene are displayed.
- All values in the file can be edited easily through this interface.
- Once editing is complete, you can save your changes to a new mbs file and run it in the game!

- For easier editing, you can also open a behaviors file (behaviors.mbs, using the same button as to open a scene file) to get the 'true' names of each snippet and attribute.
- Doing this will not close the current scene file (if one is loaded) or change the data stored in it, but it will make editing easier as the program will display "character logic - jump speed" instead of "snippet 34 - attribute 22" for example.
 
- The program can have multiple scene files loaded and ready to select for editing - eg, you can load an entire assets/data folder and then choose the right scene in the editor.

### Installation

To install: download the code from github, go to bin/publish and double click on setup.exe.
Once installed the program should auto update every time you open it.
Only windows compatible, unless you can run winforms on Linux...


### Possible future improvements

- The tile map of the level (what the background and floor looks like etc) is not stored in an mbs file. Instead, it is kept in a .scn file: currently I cannot open those files, instead use [hpr's program scnedit](https://hpr.github.io/scnedit) to view these.
- Potential complete redesign, using something more modern than winforms, to make it actually look nice.


To get in contact, DM me on discord: ben.thenerd
