using ReminderApp.Helpers;
using ReminderApp.Models;
using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Threading;

namespace ReminderApp;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private ReminderRepository _repo = new();
    private TrayHelper _tray = new();
    private DispatcherTimer _timer = new();

    public MainWindow()
    {
        InitializeComponent();
    }

    private void Window_Loaded(object sender, RoutedEventArgs e)
    {
        _repo.Load();
        RemindersGrid.ItemsSource = _repo.Reminders;
        
        // Setup reminder check timer
        _timer.Interval = TimeSpan.FromSeconds(30); // Check every 30 seconds
        _timer.Tick += Timer_Tick;
        _timer.Start();
          // Setup system tray
        _tray.Initialize("Reminder App");
        _tray.TrayDoubleClick += (s, e) => { 
            this.Show(); 
            this.WindowState = WindowState.Normal; 
        };
        
        // Setup reminder action handlers
        _tray.ReminderAction += HandleReminderAction;
        ToastHelper.ReminderAction += HandleReminderAction;
    }
    
    private void HandleReminderAction(object? sender, ReminderActionEventArgs e)
    {
        switch (e.Action.ActionType)
        {
            case ActionType.Snooze:
                SnoozeReminder(e.Action.Reminder, e.Action.SnoozeTime);
                break;
                
            case ActionType.Dismiss:
                DismissReminder(e.Action.Reminder);
                break;
        }
    }
    
    private void SnoozeReminder(Reminder reminder, TimeSpan snoozeTime)
    {
        // Update the reminder's next time
        reminder.DateTime = DateTime.Now.Add(snoozeTime);
        _repo.Save();
        
        // Refresh the UI
        Dispatcher.Invoke(() => RemindersGrid.Items.Refresh());
    }
    
    private void DismissReminder(Reminder reminder)
    {
        if (reminder.RepeatInterval.TotalSeconds > 0)
        {
            // For recurring reminders, calculate the next occurrence
            DateTime nextOccurrence = CalculateNextOccurrence(reminder, DateTime.Now);
            
            // Check if next occurrence is past end date
            if (reminder.EndDate.HasValue && nextOccurrence.Date > reminder.EndDate.Value.Date)
            {
                reminder.IsActive = false;
            }
            else
            {
                reminder.DateTime = nextOccurrence;
            }
        }
        else
        {
            // For one-time reminders, mark as inactive
            reminder.IsActive = false;
        }
        
        _repo.Save();
        
        // Refresh the UI
        Dispatcher.Invoke(() => RemindersGrid.Items.Refresh());
    }
    
    private void Timer_Tick(object? sender, EventArgs e)
    {
        var now = DateTime.Now;
        foreach (var reminder in _repo.Reminders.Where(r => r.IsActive))
        {
            // Check if reminder has expired according to end date
            if (reminder.EndDate.HasValue && now.Date > reminder.EndDate.Value.Date)
            {
                reminder.IsActive = false;
                continue;
            }
              if (reminder.DateTime <= now)
            {
                // Show notification
                ToastHelper.ShowToast("Reminder", reminder.Message, reminder);
                _tray.ShowBalloon("Reminder", reminder.Message, reminder);
                
                // Update if repeating reminder
                if (reminder.RepeatInterval.TotalSeconds > 0)
                {
                    // Calculate next occurrence
                    DateTime nextOccurrence = CalculateNextOccurrence(reminder, now);
                    
                    // Check if next occurrence is past end date
                    if (reminder.EndDate.HasValue && nextOccurrence.Date > reminder.EndDate.Value.Date)
                    {
                        reminder.IsActive = false;
                    }
                    else
                    {
                        reminder.DateTime = nextOccurrence;
                    }
                }
                else
                {
                    reminder.IsActive = false;
                }
            }
        }
        
        // Save updated reminders and refresh UI
        _repo.Save();
        RemindersGrid.Items.Refresh();
    }

    private void AddBtn_Click(object sender, RoutedEventArgs e)
    {
        var dialog = new ReminderDialog();
        if (dialog.ShowDialog() == true)
        {
            _repo.Reminders.Add(dialog.Reminder);
            _repo.Save();
        }
    }

    private void EditBtn_Click(object sender, RoutedEventArgs e)
    {
        if (RemindersGrid.SelectedItem is Reminder selected)
        {
            var dialog = new ReminderDialog(selected);
            if (dialog.ShowDialog() == true)
            {
                var idx = _repo.Reminders.IndexOf(selected);
                _repo.Reminders[idx] = dialog.Reminder;
                _repo.Save();
                RemindersGrid.Items.Refresh();
            }
        }
        else
        {
            System.Windows.MessageBox.Show("Please select a reminder to edit.", "No Selection", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }

    private void DeleteBtn_Click(object sender, RoutedEventArgs e)
    {
        if (RemindersGrid.SelectedItem is Reminder selected)
        {
            var result = System.Windows.MessageBox.Show("Are you sure you want to delete this reminder?", 
                "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Question);
                
            if (result == MessageBoxResult.Yes)
            {
                _repo.Reminders.Remove(selected);
                _repo.Save();
            }
        }
        else
        {
            System.Windows.MessageBox.Show("Please select a reminder to delete.", "No Selection", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }    private void Window_StateChanged(object sender, EventArgs e)
    {
        if (WindowState == WindowState.Minimized)
        {
            this.Hide(); // Hide window when minimized (app goes to system tray)
        }
    }

    private void Window_Closing(object sender, CancelEventArgs e)
    {
        // Check if this is a true app exit (from exit menu) or just hiding the window
        if (!App.IsExiting)
        {
            // Just hide the window instead of closing the app
            e.Cancel = true;
            this.Hide();
            
            // Show a notification that the app is still running
            _tray.ShowInfoBalloon("Reminder App", "App is still running in the background. Right-click the tray icon to exit.");
        }
        else
        {
            // Actually closing the app, clean up resources
            _timer.Stop();
            _tray.Dispose();
        }
    }

    private DateTime CalculateNextOccurrence(Reminder reminder, DateTime now)
    {
        // Start with simple interval addition
        DateTime nextOccurrence = now.Add(reminder.RepeatInterval);
        
        // Handle weekdays-only option
        if (reminder.WeekdaysOnly || reminder.RecurrenceType == RecurrenceType.Weekdays)
        {
            // Skip weekend days
            while (nextOccurrence.DayOfWeek == DayOfWeek.Saturday || nextOccurrence.DayOfWeek == DayOfWeek.Sunday)
            {
                nextOccurrence = nextOccurrence.AddDays(1);
            }
        }
        
        return nextOccurrence;
    }
}