using System;

namespace ReminderApp.Models
{
    public enum RecurrenceType
    {
        None,
        Daily,
        Weekdays,
        Custom
    }

    public class Reminder
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Message { get; set; } = string.Empty;
        public DateTime DateTime { get; set; } = DateTime.Now;
        public TimeSpan RepeatInterval { get; set; } // Zero = no repeat
        public bool IsActive { get; set; } = true;
        
        // New properties for date range and recurrence
        public RecurrenceType RecurrenceType { get; set; } = RecurrenceType.None;
        public DateTime? EndDate { get; set; } = null; // null = no end date
        public bool WeekdaysOnly { get; set; } = false;
    }
}
