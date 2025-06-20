# Reminder App Installer

This folder contains an InnoSetup script for creating an installer for the Reminder App.

## Prerequisites

1. Download and install InnoSetup from https://jrsoftware.org/isdl.php

## Building the Application for Release

Before creating the installer, you need to publish the application in Release mode:

```powershell
cd "\\midp-oaf-116\FldrRedir_3$\N994992\Data\reminder app\ReminderApp"
dotnet publish -c Release -r win-x64 --self-contained false
```

## Creating the Installer

1. Open InnoSetup Compiler
2. Open the `ReminderAppInstaller.iss` file from this folder
3. Click on "Build" > "Compile"
4. The installer will be created in the "Output" folder

## Installation Notes

- The installer creates a start menu entry for the app
- Optionally adds desktop icon and quick launch shortcuts
- Configures the app to run at Windows startup
- Installs for the current user by default (doesn't require admin rights)

## Modifying the Installer

If you need to customize the installer:

1. Update the application information at the top of the script
2. Modify the file paths if your build output location changes
3. Add or remove registry entries as needed
