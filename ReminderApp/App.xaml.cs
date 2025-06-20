using System;
using System.Configuration;
using System.Data;
using System.Windows;

namespace ReminderApp;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : System.Windows.Application
{
    // Flag to track if app is really exiting or just hiding the main window
    public static bool IsExiting { get; set; } = false;
    
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);
        
        // Set ShutdownMode to manually handle application closing
        ShutdownMode = ShutdownMode.OnExplicitShutdown;
    }
    
    // Explicit method to properly exit the application
    public static void ExitApplication()
    {
        IsExiting = true;
        Current.Shutdown();
    }
    
    protected override void OnExit(ExitEventArgs e)
    {
        base.OnExit(e);
    }
}

