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
using System.Windows.Shapes;
using UniversityManagementSystem.BLL;
using UniversityManagementSystem.DAL;

namespace UniversityManagementSystem.Views
{
    public partial class UserManagementWindow : Window
    {
        private readonly UserBLL _userBLL = new UserBLL();

        public UserManagementWindow()
        {
            InitializeComponent();
            this.Loaded += UserManagementWindow_Loaded;
        }

        private void UserManagementWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadUsers();
        }

        private void LoadUsers()
        {
            try
            {
                UsersDataGrid.ItemsSource = _userBLL.GetAllUsers().DefaultView;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Error loading users: " + ex.Message, "Error",
                              MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void AddUserButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Validation
                if (string.IsNullOrWhiteSpace(UsernameTextBox.Text))
                {
                    MessageBox.Show("Username is required.", "Validation Error",
                                  MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (string.IsNullOrWhiteSpace(PasswordBox.Password))
                {
                    MessageBox.Show("Password is required.", "Validation Error",
                                  MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (RoleComboBox.SelectedItem == null)
                {
                    MessageBox.Show("Please select a role.", "Validation Error",
                                  MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                string username = UsernameTextBox.Text.Trim();
                string password = PasswordBox.Password;
                string role = (RoleComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();

                bool success = _userBLL.AddUser(username, password, role);

                if (success)
                {
                    MessageBox.Show("User added successfully!", "Success",
                                  MessageBoxButton.OK, MessageBoxImage.Information);
                    LoadUsers();
                    ClearForm();
                }
                else
                {
                    MessageBox.Show("Failed to add user. Username may already exist.", "Error",
                                  MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Error adding user: " + ex.Message, "Error",
                              MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            LoadUsers();
        }

        private void ClearForm()
        {
            UsernameTextBox.Clear();
            PasswordBox.Clear();
            RoleComboBox.SelectedIndex = -1;
        }
    }
}
