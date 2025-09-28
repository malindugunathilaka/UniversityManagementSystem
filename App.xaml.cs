using System;
using System.Windows;

namespace UniversityManagementSystem
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Global exception handler
            AppDomain.CurrentDomain.UnhandledException += (sender, args) =>
            {
                Exception ex = (Exception)args.ExceptionObject;
                MessageBox.Show($"An unexpected error occurred:\n{ex.Message}",
                              "System Error",
                              MessageBoxButton.OK,
                              MessageBoxImage.Error);
            };

            this.DispatcherUnhandledException += (sender, args) =>
            {
                MessageBox.Show($"An error occurred:\n{args.Exception.Message}",
                              "Application Error",
                              MessageBoxButton.OK,
                              MessageBoxImage.Error);
                args.Handled = true; // Prevent application crash
            };
        }
    }
}