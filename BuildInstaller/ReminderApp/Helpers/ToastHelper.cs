using System;
using System.Windows;
using System.Windows.Forms;
using ReminderApp.Models;
using MessageBox = System.Windows.MessageBox;

namespace ReminderApp.Helpers
{    public static class ToastHelper
    {
        public static event EventHandler<ReminderActionEventArgs>? ReminderAction;
        
        public static void ShowToast(string title, string message, Reminder reminder)
        {
            // Show a notification dialog with several snooze options
            var form = new TaskDialogForm(title, message, reminder);
            form.ReminderAction += (s, e) => ReminderAction?.Invoke(s, e);
            form.TopMost = true;
            form.Show();
        }
        
        // This is our custom form for reminders with snooze options
        private class TaskDialogForm : Form
        {
            private readonly Reminder _reminder;
            public event EventHandler<ReminderActionEventArgs>? ReminderAction;
            
            public TaskDialogForm(string title, string message, Reminder reminder)
            {
                _reminder = reminder;
                
                // Setup form
                Text = title;
                Width = 400;
                Height = 200;
                FormBorderStyle = FormBorderStyle.FixedDialog;
                StartPosition = FormStartPosition.CenterScreen;
                MaximizeBox = false;
                MinimizeBox = false;
                TopMost = true;
                ShowInTaskbar = true;
                
                // Message label
                var messageLabel = new Label
                {
                    Text = message,
                    Left = 20,
                    Top = 20,
                    Width = 360,
                    Height = 50,
                    AutoSize = false
                };
                Controls.Add(messageLabel);
                
                // Snooze dropdown
                var snoozeLabel = new Label
                {
                    Text = "Snooze for:",
                    Left = 20,
                    Top = 80,
                    Width = 80,
                    Height = 20
                };
                Controls.Add(snoozeLabel);
                
                var snoozeCombo = new ComboBox
                {
                    Left = 100,
                    Top = 80,
                    Width = 120,
                    DropDownStyle = ComboBoxStyle.DropDownList
                };
                
                snoozeCombo.Items.Add("5 minutes");
                snoozeCombo.Items.Add("15 minutes");
                snoozeCombo.Items.Add("30 minutes");
                snoozeCombo.Items.Add("60 minutes");
                snoozeCombo.SelectedIndex = 0;
                
                Controls.Add(snoozeCombo);
                
                // Snooze button
                var snoozeButton = new Button
                {
                    Text = "Snooze",
                    Left = 100,
                    Top = 120,
                    Width = 100,
                    Height = 30
                };
                
                snoozeButton.Click += (s, e) => {
                    TimeSpan snoozeTime;
                    
                    switch (snoozeCombo.SelectedIndex)
                    {
                        case 1:
                            snoozeTime = TimeSpan.FromMinutes(15);
                            break;
                        case 2:
                            snoozeTime = TimeSpan.FromMinutes(30);
                            break;
                        case 3:
                            snoozeTime = TimeSpan.FromMinutes(60);
                            break;
                        default:
                            snoozeTime = TimeSpan.FromMinutes(5);
                            break;
                    }
                    
                    ReminderAction?.Invoke(this, new ReminderActionEventArgs(
                        new ReminderAction(_reminder, ActionType.Snooze, snoozeTime)));
                    
                    Close();
                };
                
                Controls.Add(snoozeButton);
                
                // Stop/Dismiss button
                var dismissButton = new Button
                {
                    Text = "Dismiss",
                    Left = 220,
                    Top = 120,
                    Width = 100,
                    Height = 30
                };
                
                dismissButton.Click += (s, e) => {
                    ReminderAction?.Invoke(this, new ReminderActionEventArgs(
                        new ReminderAction(_reminder, ActionType.Dismiss, TimeSpan.Zero)));
                    
                    Close();
                };
                
                Controls.Add(dismissButton);
            }
        }    }
}
