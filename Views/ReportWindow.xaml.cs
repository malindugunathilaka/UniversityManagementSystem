using System;
using System.Data;
using System.IO;
using System.Text;
using System.Windows;
using UniversityManagementSystem.BLL;

namespace UniversityManagementSystem.Views
{
    public partial class ReportWindow : Window
    {
        private readonly UserBLL _userBLL = new UserBLL();

        public ReportWindow()
        {
            InitializeComponent();
        }

        private void GenerateReportButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Generate user statistics report
                DataTable userData = _userBLL.GetAllUsers();
                ReportDataGrid.ItemsSource = userData.DefaultView;

                // Generate summary
                int totalUsers = userData.Rows.Count;
                int adminCount = 0, lecturerCount = 0, studentCount = 0, financeCount = 0;

                foreach (DataRow row in userData.Rows)
                {
                    string role = row["Role"].ToString();
                    switch (role)
                    {
                        case "Admin": adminCount++; break;
                        case "Lecturer": lecturerCount++; break;
                        case "Student": studentCount++; break;
                        case "Finance Officer": financeCount++; break;
                    }
                }

                SummaryTextBlock.Text = $"Report Summary:\n" +
                                      $"Total Users: {totalUsers}\n" +
                                      $"Administrators: {adminCount}\n" +
                                      $"Lecturers: {lecturerCount}\n" +
                                      $"Students: {studentCount}\n" +
                                      $"Finance Officers: {financeCount}\n" +
                                      $"Generated on: {DateTime.Now:yyyy-MM-dd HH:mm:ss}";

                MessageBox.Show("Report generated successfully!", "Success",
                              MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error generating report: " + ex.Message, "Error",
                              MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ExportButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ReportDataGrid.ItemsSource == null)
                {
                    MessageBox.Show("Please generate a report first.", "No Data",
                                  MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Create a simple text export
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("UNIVERSITY MANAGEMENT SYSTEM - USER REPORT");
                sb.AppendLine("=" + new string('=', 50));
                sb.AppendLine($"Generated on: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
                sb.AppendLine();

                DataView dataView = (DataView)ReportDataGrid.ItemsSource;
                foreach (DataRowView row in dataView)
                {
                    sb.AppendLine($"User ID: {row["UserID"]}");
                    sb.AppendLine($"Username: {row["Username"]}");
                    sb.AppendLine($"Role: {row["Role"]}");
                    sb.AppendLine($"Created: {row["CreatedAt"]}");
                    sb.AppendLine(new string('-', 30));
                }

                sb.AppendLine();
                sb.AppendLine(SummaryTextBlock.Text);

                // Save to desktop
                string fileName = $"UMS_Report_{DateTime.Now:yyyyMMdd_HHmmss}.txt";
                string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                string fullPath = Path.Combine(desktopPath, fileName);

                File.WriteAllText(fullPath, sb.ToString());

                MessageBox.Show($"Report exported successfully to:\n{fullPath}", "Export Complete",
                              MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error exporting report: " + ex.Message, "Export Error",
                              MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}