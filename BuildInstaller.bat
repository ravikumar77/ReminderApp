@echo off
echo Building Reminder App for Release...

:: Go to the solution directory
cd "\\midp-oaf-116\FldrRedir_3$\N994992\Data\reminder app"

:: Build the project
echo Publishing the application...
dotnet publish ReminderApp.sln -c Release -r win-x64 --self-contained false

echo.
echo Build completed.
echo.
echo To create the installer:
echo 1. Open InnoSetup Compiler
echo 2. Open the ReminderAppInstaller.iss file
echo 3. Click on "Build" > "Compile"
echo.
echo The installer will be created in the "Output" folder.
echo.

pause
