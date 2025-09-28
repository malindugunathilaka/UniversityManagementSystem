
using System.Windows;
using System.Windows.Controls;

using UniversityManagementSystem.BLL;
using UniversityManagementSystem.Core;
using UniversityManagementSystem.Models;

namespace UniversityManagementSystem.Views
{
    public partial class LoginWindow : Window
    {
        private readonly AuthenticationBLL _authBLL = new AuthenticationBLL();

        public LoginWindow()
        {
            InitializeComponent();
            UsernameTextBox.Focus();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string username = UsernameTextBox.Text.Trim();
                string password = PasswordBox.Password;

                // Validation
                if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                {
                    MessageBox.Show("Please enter both username and password.", "Validation Error",
                                  MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                User user = _authBLL.AuthenticateUser(username, password);

                if (user != null)
                {
                    AppSession.Login(user);

                    MainWindow dashboard = new MainWindow();
                    dashboard.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Invalid username or password.", "Login Failed",
                                  MessageBoxButton.OK, MessageBoxImage.Error);
                    PasswordBox.Clear();
                    UsernameTextBox.Focus();
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error",
                              MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
