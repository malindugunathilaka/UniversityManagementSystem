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
        private readonly SecurityBLL _securityBLL;

        public AuthenticationBLL()
        {
            _userDAL = new UserDAL();
            _securityBLL = new SecurityBLL();
        }

        public User AuthenticateUser(string username, string password)
        {
            User user = _userDAL.GetUserByUsername(username);

            if (user != null && _securityBLL.VerifyPassword(password, user.PasswordHash))
            {
                return user;
            }

            return null;
        }
    }
}
