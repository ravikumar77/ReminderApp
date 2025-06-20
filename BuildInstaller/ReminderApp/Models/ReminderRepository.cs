using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Text.Json;

namespace ReminderApp.Models
{
    public class ReminderRepository
    {
        private const string FileName = "reminders.json";
        public ObservableCollection<Reminder> Reminders { get; set; } = new();

        public void Load()
        {
            try
            {
                if (File.Exists(FileName))
                {
                    var json = File.ReadAllText(FileName);
                    var loaded = JsonSerializer.Deserialize<ObservableCollection<Reminder>>(json);
                    if (loaded != null)
                    {
                        Reminders = loaded;
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle error - could log or show message
                System.Diagnostics.Debug.WriteLine($"Error loading reminders: {ex.Message}");
            }
        }

        public void Save()
        {
            try
            {
                var options = new JsonSerializerOptions { WriteIndented = true };
                var json = JsonSerializer.Serialize(Reminders, options);
                File.WriteAllText(FileName, json);
            }
            catch (Exception ex)
            {
                // Handle error - could log or show message
                System.Diagnostics.Debug.WriteLine($"Error saving reminders: {ex.Message}");
            }
        }
    }
}
