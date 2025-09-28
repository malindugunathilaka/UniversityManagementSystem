using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


using System.Windows;
using UniversityManagementSystem.Core;

namespace UniversityManagementSystem.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            if (AppSession.CurrentUser == null)
            {
                LoginWindow loginWindow = new LoginWindow();
                loginWindow.Show();
                this.Close();
                return;
            }

            SetupDashboard();
        }

        private void SetupDashboard()
        {
            // Welcome message
            WelcomeMessage.Text = $"Welcome, {AppSession.CurrentUser.Username}!";
            WelcomeTextBlock.Text = $"Logged in as: {AppSession.CurrentUser.Username} ({AppSession.CurrentUser.Role})";
            StatusTextBlock.Text = "System Ready";

            // Role-based menu visibility
            switch (AppSession.CurrentUser.Role)
            {
                case "Admin":
                    ManageUsersButton.Visibility = Visibility.Visible;
                    ManageProgramsButton.Visibility = Visibility.Visible;
                    RegisterStudentButton.Visibility = Visibility.Visible;
                    ManageAttendanceButton.Visibility = Visibility.Visible;
                    EnterGradesButton.Visibility = Visibility.Visible;
                    ManageFeesButton.Visibility = Visibility.Visible;
                    break;

                case "Lecturer":
                    ManageAttendanceButton.Visibility = Visibility.Visible;
                    EnterGradesButton.Visibility = Visibility.Visible;
                    break;

                case "Finance Officer":
                    ManageFeesButton.Visibility = Visibility.Visible;
                    break;

                case "Student":
                    // Students would have limited access (view only)
                    break;
            }
        }

        // Event Handlers (placeholder implementations)
        private void ManageUsersButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                UserManagementWindow userManagementWindow = new UserManagementWindow();
                userManagementWindow.Show();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Error opening User Management: " + ex.Message, "Error",
                              MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ManageProgramsButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ReportWindow reportWindow = new ReportWindow();
                reportWindow.Show();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Error opening Reports: " + ex.Message, "Error",
                              MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void RegisterStudentButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Student Registration feature coming soon!", "Feature",
                          MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void ManageAttendanceButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Attendance Management feature coming soon!", "Feature",
                          MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void EnterGradesButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Grade Entry feature coming soon!", "Feature",
                          MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void ManageFeesButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Fee Management feature coming soon!", "Feature",
                          MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Are you sure you want to logout?", "Confirm Logout",
                                       MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                AppSession.Logout();
                LoginWindow loginWindow = new LoginWindow();
                loginWindow.Show();
                this.Close();
            }
        }
    }
}