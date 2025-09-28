using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityManagementSystem.DAL;
using UniversityManagementSystem.Models;

namespace UniversityManagementSystem.BLL
{
    public class AuthenticationBLL
    {
        private readonly UserDAL _userDAL;

        public AuthenticationBLL()
        {
            _userDAL = new UserDAL();
        }

        public User AuthenticateUser(string username, string password)
        {
            User user = _userDAL.GetUserByUsername(username);

            if (user != null && password == user.Password)
            {
                return user;
            }

            return null;
        }
    }
}
