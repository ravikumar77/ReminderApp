using ReminderApp.Models;
using System;
using System.Windows;
using System.Windows.Controls;

namespace ReminderApp
{
    public partial class ReminderDialog : Window
    {
        public Reminder Reminder { get; private set; }        public ReminderDialog(Reminder? existingReminder = null)
        {
            InitializeComponent();
            
            // Set owner window for modal behavior
            if (System.Windows.Application.Current.MainWindow != this)
            {
                Owner = System.Windows.Application.Current.MainWindow;
            }

            // Populate minute picker with values 00-59
            PopulateMinutePicker();
            
            // Set default selected values for time pickers
            HourPicker.SelectedIndex = 0; // Default to 12
            MinutePicker.SelectedIndex = 0; // Default to 00
            AmPmPicker.SelectedIndex = 0; // Default to AM
            
            // Set default recurrence type
            RecurrenceTypeCombo.SelectedIndex = 0; // Default to None

            if (existingReminder != null)
            {
                // Edit existing reminder
                Reminder = new Reminder
                {
                    Id = existingReminder.Id,
                    Message = existingReminder.Message,
                    DateTime = existingReminder.DateTime,
                    RepeatInterval = existingReminder.RepeatInterval,
                    IsActive = existingReminder.IsActive,
                    RecurrenceType = existingReminder.RecurrenceType,
                    EndDate = existingReminder.EndDate,
                    WeekdaysOnly = existingReminder.WeekdaysOnly
                };
                
                // Populate UI with existing values
                MessageBox.Text = Reminder.Message;
                DatePicker.SelectedDate = Reminder.DateTime.Date;
                
                // Set time picker values
                SetTimePickerValues(Reminder.DateTime);
                
                // Set recurrence values
                RecurrenceTypeCombo.SelectedIndex = (int)Reminder.RecurrenceType;
                IntervalBox.Text = ((int)Reminder.RepeatInterval.TotalMinutes).ToString();
                WeekdaysOnlyCheckBox.IsChecked = Reminder.WeekdaysOnly;
                
                // Set end date if applicable
                if (Reminder.EndDate.HasValue)
                {
                    HasEndDateCheckBox.IsChecked = true;
                    EndDatePicker.IsEnabled = true;
                    EndDatePicker.SelectedDate = Reminder.EndDate.Value;
                }
                
                Title = "Edit Reminder";
            }
            else
            {
                // Create new reminder
                Reminder = new Reminder();
                
                // Default values
                DatePicker.SelectedDate = DateTime.Now.Date;
                
                // Set time picker to current time
                SetTimePickerValues(DateTime.Now);
                
                // Default recurrence values
                IntervalBox.Text = "0";
                WeekdaysOnlyCheckBox.IsChecked = false;
                
                Title = "New Reminder";
            }
        }        private void PopulateMinutePicker()
        {
            // Clear existing items
            MinutePicker.Items.Clear();
            
            // Add minutes from 0 to 59
            for (int i = 0; i < 60; i++)
            {
                MinutePicker.Items.Add(new ComboBoxItem { Content = i.ToString("D2") });
            }
        }
        
        private void SetTimePickerValues(DateTime dateTime)
        {
            int hour = dateTime.Hour % 12;
            if (hour == 0) hour = 12; // 12 AM or 12 PM
            
            // Set hour
            HourPicker.SelectedIndex = hour == 12 ? 0 : hour;
            
            // Set minute (now supports all minutes from 0-59)
            MinutePicker.SelectedIndex = dateTime.Minute;
            
            // Set AM/PM
            AmPmPicker.SelectedIndex = dateTime.Hour >= 12 ? 1 : 0;
        }        private void RecurrenceTypeCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (RecurrenceTypeCombo == null || IntervalBox == null) return;
            
            var selectedItem = RecurrenceTypeCombo.SelectedItem as ComboBoxItem;
            if (selectedItem == null) return;
            
            var recurrenceType = selectedItem.Tag.ToString();
            
            // Set appropriate values based on recurrence type
            switch (recurrenceType)
            {
                case "None":
                    IntervalBox.Text = "0";
                    IntervalBox.IsEnabled = false;
                    WeekdaysOnlyCheckBox.IsChecked = false;
                    WeekdaysOnlyCheckBox.IsEnabled = false;
                    break;
                    
                case "Daily":
                    IntervalBox.Text = "1440"; // 24 hours in minutes
                    IntervalBox.IsEnabled = false;
                    WeekdaysOnlyCheckBox.IsEnabled = true;
                    break;
                    
                case "Weekdays":
                    IntervalBox.Text = "1440"; // 24 hours in minutes
                    IntervalBox.IsEnabled = false;
                    WeekdaysOnlyCheckBox.IsChecked = true;
                    WeekdaysOnlyCheckBox.IsEnabled = false;
                    break;
                    
                case "Custom":
                    IntervalBox.IsEnabled = true;
                    WeekdaysOnlyCheckBox.IsEnabled = true;
                    break;
            }
        }
        
        private void HasEndDateCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (EndDatePicker != null)
            {
                EndDatePicker.IsEnabled = true;
                if (!EndDatePicker.SelectedDate.HasValue)
                {
                    EndDatePicker.SelectedDate = DateTime.Now.Date.AddDays(7); // Default to 1 week later
                }
            }
        }
        
        private void HasEndDateCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            if (EndDatePicker != null)
            {
                EndDatePicker.IsEnabled = false;
            }
        }

        private void OkBtn_Click(object sender, RoutedEventArgs e)
        {
            // Validate inputs
            if (string.IsNullOrWhiteSpace(MessageBox.Text))
            {
                System.Windows.MessageBox.Show("Please enter a message.", "Missing Information", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            
            if (!DatePicker.SelectedDate.HasValue)
            {
                System.Windows.MessageBox.Show("Please select a date.", "Missing Information", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            
            if (!int.TryParse(IntervalBox.Text, out var minutes) || minutes < 0)
            {
                System.Windows.MessageBox.Show("Please enter a valid repeat interval (0 or more).", "Invalid Interval", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            
            // Validate end date if set
            if (HasEndDateCheckBox.IsChecked == true && (!EndDatePicker.SelectedDate.HasValue || EndDatePicker.SelectedDate < DatePicker.SelectedDate))
            {
                System.Windows.MessageBox.Show("End date must be after the start date.", "Invalid Date Range", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            
            // Get time from pickers
            TimeSpan time = GetTimeFromPickers();
            
            // Get recurrence type
            var selectedRecurrenceItem = RecurrenceTypeCombo.SelectedItem as ComboBoxItem;
            RecurrenceType recurrenceType = RecurrenceType.None;
            if (selectedRecurrenceItem != null)
            {
                Enum.TryParse(selectedRecurrenceItem.Tag.ToString(), out recurrenceType);
            }
            
            // Update reminder object
            var date = DatePicker.SelectedDate.Value.Date + time;
            Reminder.Message = MessageBox.Text;
            Reminder.DateTime = date;
            Reminder.RepeatInterval = TimeSpan.FromMinutes(minutes);
            Reminder.IsActive = true;
            Reminder.RecurrenceType = recurrenceType;
            Reminder.WeekdaysOnly = WeekdaysOnlyCheckBox.IsChecked == true;
            Reminder.EndDate = HasEndDateCheckBox.IsChecked == true ? EndDatePicker.SelectedDate : null;
            
            // Close dialog with success
            DialogResult = true;
        }

        private TimeSpan GetTimeFromPickers()
        {
            // Get selected hour (12-hour format)
            string hourString = ((ComboBoxItem)HourPicker.SelectedItem).Content.ToString() ?? "12";
            int hour = int.Parse(hourString);
            
            // Convert to 24-hour format based on AM/PM
            string amPmString = ((ComboBoxItem)AmPmPicker.SelectedItem).Content.ToString() ?? "AM";
            bool isPm = amPmString == "PM";
            if (isPm && hour < 12) hour += 12;
            if (!isPm && hour == 12) hour = 0;
            
            // Get selected minute
            string minuteString = ((ComboBoxItem)MinutePicker.SelectedItem).Content.ToString() ?? "00";
            int minute = int.Parse(minuteString);
            
            return new TimeSpan(hour, minute, 0);
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
