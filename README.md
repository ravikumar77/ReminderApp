# Reminder App

A Windows desktop application built with C# and WPF that allows users to create, configure, and manage multiple reminder notifications.

## Features

- Add, edit, and delete reminders
- Each reminder includes a message, date/time, and configurable repeat interval
- Precise minute selection (1-60) for setting reminder times
- Flexible recurrence options: daily, weekdays only, or custom intervals
- Snooze and dismiss options directly in notifications
- Reminders are saved locally for persistence between sessions
- Windows notification toast and system tray notifications
- Runs in the background (minimizes to system tray)
- Simple, modern UI for easy management
- Installer available for easy distribution

## Usage Instructions

1. **Add a Reminder**:
   - Click the "Add" button
   - Enter a message, select date and time, and set a repeat interval (in minutes)
   - Click "OK" to save

2. **Edit a Reminder**:
   - Select a reminder from the list
   - Click the "Edit" button
   - Modify the details as needed
   - Click "OK" to save changes

3. **Delete a Reminder**:
   - Select a reminder from the list
   - Click the "Delete" button
   - Confirm deletion

4. **Minimize to System Tray**:
   - Minimizing the window will hide it to the system tray
   - Double-click the system tray icon to restore the window

## Technical Details

- Built with C# and WPF
- Uses Windows Forms for system tray functionality
- Stores reminders in JSON format
- Checks for due reminders every 30 seconds

## Requirements

- .NET 9.0 or later
- Windows 10 or Windows 11

## Installation

You can install the Reminder App using the provided installer:

1. Run `ReminderAppSetup.exe`
2. Follow the installation wizard
3. The app will be available in your Start Menu

For developers:
- Run `Build.bat` to build and publish the application
- See `README-INSTALLER.md` for instructions on building the installer
- The solution file (`ReminderApp.sln`) can be opened in Visual Studio or built with `dotnet build ReminderApp.sln`

  ## Samples
- ![Screenshot 2025-06-20 195714](https://github.com/user-attachments/assets/ce4d7c95-971c-4e7e-92ab-371edeb9b2ef)
- ![Screenshot 2025-06-20 195714](https://github.com/user-attachments/assets/f1a0a570-0f3c-4cba-85c4-04ca5dd70c7c)  
![Screenshot 2025-06-20 195522](https://github.com/user-attachments/assets/e3a2ecfa-d254-4fc4-a11f-97205cd3f8d4)
![Screenshot 2025-06-20 195555](https://github.com/user-attachments/assets/830ed820-ab0a-4a77-8984-6e03386e030a)
![Screenshot 2025-06-20 195650](https://github.com/user-attachments/assets/03023caf-b54b-438d-a8ab-4f58f86bda9b)
![Screenshot 2025-06-20 195240](https://github.com/user-attachments/assets/960eb94d-5de9-42d2-a153-236be3000c72)
![Screenshot 2025-06-20 195304](https://github.com/user-attachments/assets/9b414977-ee36-441b-b414-61b539fb6888)
![Screenshot 2025-06-20 195349](https://github.com/user-attachments/assets/71fc0400-931b-4b91-a87d-76625ab6b5b2)

