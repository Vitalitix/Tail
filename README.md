# Tail - Windows Log File Monitor

A lightweight utility for monitoring text files in real-time on Windows, similar to the Unix 'tail' command.

## Features

- Real-time file monitoring with automatic updates
- Regex-based filtering for log entries
- Multiple ways to launch:
  - Command line: `tail <filename>`
  - Windows Explorer context menu (right-click)
  - Windows "Send To" menu
- Load entire file contents on demand
- Window title shows time since last update and current filter
- Supports files opened by other processes (using ShareReadWrite)

## Usage

### Basic Usage
1. Launch the utility using one of the methods below:
   - Command line: `tail <filename>`
   - Right-click on a file in Explorer and select "Tail"
   - Right-click on a file > "Send To" > "Tail"

### While Monitoring
- Press `~` key to load the entire file from beginning
- Type any valid regex pattern to filter the log entries
- Use Backspace to modify the filter
- Monitor window title shows:
  - Time since last update
  - Current filter pattern
  - File name

## Installation

### Method 1: Windows Explorer Context Menu

1. Create a .reg file with the following content:
```
Windows Registry Editor Version 5.00
[HKEY_CLASSES_ROOT\*\shell\Tail]
[HKEY_CLASSES_ROOT\*\shell\Tail\Command]
@="C:\\Utils\\Tail.exe \"%1\""
```
2. Replace `C:\\Utils` with your actual installation path
3. Double-click the .reg file to add it to the registry

### Method 2: "Send To" Menu

1. Navigate to: `C:\Users\<Username>\AppData\Roaming\Microsoft\Windows\SendTo`
2. Create a shortcut with:
   - Target: `"<Path to Tail.exe>" "%1"`
   - Name: "Tail"

## Requirements

- Windows operating system
- .NET Framework 4.7.2 or higher

## Technical Details

- Uses FileStream with ShareReadWrite to monitor files that are being written to
- Implements real-time monitoring with minimal CPU usage
- Default buffer shows last ~1000 characters of the file
- Handles file updates without locking the source file

## License

MIT

## Version

Current version: 1.1
Copyright © 2015