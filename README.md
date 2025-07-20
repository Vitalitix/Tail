# Tail text file utility for windows
## Usage:
- Commandline tail <filename>
- Explorrer right click > Tail
- While using:
	- Click ~ to load the entire file
 	- Type any valid REGEX to filter the log
## Install right click on windows
- Registry
  - Create .reg file with the following content:
	- Change C:\\Utils to the actual file location.
 	- CLick the file to add to registry.
```sh
Windows Registry Editor Version 5.00
[HKEY_CLASSES_ROOT\*\shell\Tail]
[HKEY_CLASSES_ROOT\*\shell\Tail\Command]
@="C:\\Utils\\Tail.exe ""%1"""
```
- Send to
	- Add shortcut at C:\Users\\\<User>\AppData\Roaming\Microsoft\Windows\SendTo
 	- The shortcut command: <Path to location>Tail.exe "%1"
