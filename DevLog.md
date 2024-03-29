### V 1.6.0.0
- Included a feature that allows multiple scenes to be loaded together, and then swapped between based on their names in the editor, so you don't have to memorise the ID of each scene.
- Added a log window (and a copy of it written to LogFile.txt) that keeps a record of everything the program has done.
- Added a help button to show some basic info about the program and mbs files - not sure how helpful it is
- Significant redesign to accomodate the above changes.

### V 1.5.1.0
- Fixed an issue with null references preventing some files from saving.
- Made the behavior file loading *actually* work - now it instantly updates the names of all behaviors.
- Minor visual improvements.

### V 1.5.0.0
- Added behavior.mbs reading functionality: by opening a scene file as normal and then opening a behavior file using the same button, it loads the names of all behaviors and attributes to make everything easier.
- That's it for now I guess unless I broke something :)

### V 1.4.2.0

- Minor visual improvements
- Fixed bug where new objects added to lists were not visible in the main view
- Fixed bug where attempting to create new attributes for behaviors would throw an error
- Fixed other various bugs with lists and attributes
- Added button to automatically order actors by their z position and layer order, automatically assinging those as required
- Added button to refresh everything on the screen (should fix any weird bugs with lists not displaying properly)