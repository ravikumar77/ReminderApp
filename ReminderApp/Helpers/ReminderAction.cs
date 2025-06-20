using System;
using ReminderApp.Models;

namespace ReminderApp.Helpers
{
    public enum ActionType
    {
        Snooze,
        Dismiss
    }
    
    public class ReminderAction
    {
        public Reminder Reminder { get; }
        public ActionType ActionType { get; }
        public TimeSpan SnoozeTime { get; }
        
        public ReminderAction(Reminder reminder, ActionType actionType, TimeSpan snoozeTime)
        {
            Reminder = reminder;
            ActionType = actionType;
            SnoozeTime = snoozeTime;
        }
    }
    
    public class ReminderActionEventArgs : EventArgs
    {
        public ReminderAction Action { get; }
        
        public ReminderActionEventArgs(ReminderAction action)
        {
            Action = action;
        }
    }
}
