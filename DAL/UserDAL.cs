using System;
using System.Data;
using System.Data.SqlClient;
using UniversityManagementSystem.Models;

namespace UniversityManagementSystem.DAL
{
    public class UserDAL
    {
        // IMPORTANT: Replace with your actual server name
        private string connectionString = "Server=MALINDU-GUNATHI\\SQLEXPRESS;Database=UMS_DB;Trusted_Connection=True;";

        public User GetUserByUsername(string username)
        {
            User user = null;
            string query = "SELECT UserID, Username, Password, Role FROM Users WHERE Username = @Username";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Username", username);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                user = new User();
                                user.UserID = (int)reader["UserID"];
                                user.Username = reader["Username"].ToString();
                                user.Password = reader["Password"].ToString();
                                user.Role = reader["Role"].ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Database connection error: " + ex.Message);
            }
            return user;
        }

        public DataTable GetAllUsers()
        {
            DataTable dt = new DataTable();
            string query = "SELECT UserID, Username, Role, CreatedAt FROM Users ORDER BY Username";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            adapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving users: " + ex.Message);
            }
            return dt;
        }

        public bool AddUser(User user)
        {
            string query = "INSERT INTO Users (Username, Password, Role) VALUES (@Username, @Password, @Role)";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Username", user.Username);
                        cmd.Parameters.AddWithValue("@Password", user.Password);
                        cmd.Parameters.AddWithValue("@Role", user.Role);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627) // Unique constraint violation
                {
                    throw new Exception("Username already exists.");
                }
                throw new Exception("Database error: " + ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception("Error adding user: " + ex.Message);
            }
        }
    }
}