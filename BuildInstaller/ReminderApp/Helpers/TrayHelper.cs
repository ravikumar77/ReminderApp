using System;
using System.Windows.Forms;
using ReminderApp.Models;
using ReminderApp;

namespace ReminderApp.Helpers
{
    public class TrayHelper
    {
        private NotifyIcon? _notifyIcon;
        private ContextMenuStrip? _reminderMenu;
        
        public event EventHandler? TrayDoubleClick;
        public event EventHandler<ReminderActionEventArgs>? ReminderAction;        public void Initialize(string tooltip)
        {
            _notifyIcon = new NotifyIcon
            {
                Icon = System.Drawing.SystemIcons.Information,
                Visible = true,
                Text = tooltip
            };
            
            _notifyIcon.DoubleClick += (s, e) => 
            {
                TrayDoubleClick?.Invoke(this, EventArgs.Empty);
            };
            
            // Create a context menu for the tray icon
            var contextMenu = new ContextMenuStrip();
            
            // Add open option
            var openItem = contextMenu.Items.Add("Open Reminder App");
            openItem.Click += (s, e) => TrayDoubleClick?.Invoke(this, EventArgs.Empty);
            
            contextMenu.Items.Add(new ToolStripSeparator());
            
            // Add exit option
            var exitItem = contextMenu.Items.Add("Exit");
            exitItem.Click += (s, e) => App.ExitApplication();
            
            _notifyIcon.ContextMenuStrip = contextMenu;
        }
        
        public void ShowInfoBalloon(string title, string message)
        {
            if (_notifyIcon != null)
            {
                _notifyIcon.BalloonTipTitle = title;
                _notifyIcon.BalloonTipText = message;
                _notifyIcon.BalloonTipIcon = ToolTipIcon.Info;
                _notifyIcon.ShowBalloonTip(5000);
            }
        }public void ShowBalloon(string title, string message, Reminder reminder)
        {
            if (_notifyIcon != null)
            {
                // Create context menu for the reminder
                _reminderMenu = new ContextMenuStrip();
                
                // Add snooze options
                var snooze5Min = _reminderMenu.Items.Add("Snooze 5 Minutes");
                snooze5Min.Tag = new ReminderAction(reminder, ActionType.Snooze, TimeSpan.FromMinutes(5));
                
                var snooze15Min = _reminderMenu.Items.Add("Snooze 15 Minutes");
                snooze15Min.Tag = new ReminderAction(reminder, ActionType.Snooze, TimeSpan.FromMinutes(15));
                
                var snooze30Min = _reminderMenu.Items.Add("Snooze 30 Minutes");
                snooze30Min.Tag = new ReminderAction(reminder, ActionType.Snooze, TimeSpan.FromMinutes(30));
                
                var snooze1Hour = _reminderMenu.Items.Add("Snooze 1 Hour");
                snooze1Hour.Tag = new ReminderAction(reminder, ActionType.Snooze, TimeSpan.FromHours(1));
                
                _reminderMenu.Items.Add(new ToolStripSeparator());
                
                // Add dismiss option
                var dismiss = _reminderMenu.Items.Add("Dismiss");
                dismiss.Tag = new ReminderAction(reminder, ActionType.Dismiss, TimeSpan.Zero);
                
                // Handle click events for menu items
                foreach (ToolStripItem item in _reminderMenu.Items)
                {
                    item.Click += ReminderMenuItem_Click;
                }
                
                // Set the context menu
                _notifyIcon.ContextMenuStrip = _reminderMenu;
                
                // Show balloon notification
                _notifyIcon.BalloonTipTitle = title;
                _notifyIcon.BalloonTipText = message + "\n(Right-click for options)";
                _notifyIcon.ShowBalloonTip(8000); // Show for longer to give time to interact
            }
        }
        
        private void ReminderMenuItem_Click(object? sender, EventArgs e)
        {
            if (sender is ToolStripItem menuItem && menuItem.Tag is ReminderAction action)
            {
                ReminderAction?.Invoke(this, new ReminderActionEventArgs(action));
            }
        }

        public void Dispose()
        {
            if (_notifyIcon != null)
            {
                _notifyIcon.Visible = false;
                _notifyIcon.Dispose();
                _notifyIcon = null;
            }
        }
    }
}
