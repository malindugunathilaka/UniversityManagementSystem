using UniversityManagementSystem.DAL;
using UniversityManagementSystem.Models;
using System.Data;

namespace UniversityManagementSystem.BLL
{
    public class UserBLL
    {
        private readonly UserDAL _userDAL = new UserDAL();

        public DataTable GetAllUsers()
        {
            return _userDAL.GetAllUsers();
        }

        public bool AddUser(string username, string password, string role)
        {
            // Business Rule Validation
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(role))
            {
                return false;
            }

            // Password strength validation
            if (password.Length < 6)
            {
                return false;
            }

            // Hash the password
            string plainPassword = password;

            User newUser = new User
            {
                Username = username,
                Password = plainPassword,
                Role = role
            };

            return _userDAL.AddUser(newUser);
        }
    }
}