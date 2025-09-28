using UniversityManagementSystem.Models;

namespace UniversityManagementSystem.Core
{
    public static class AppSession
    {
        public static User CurrentUser { get; private set; }

        public static void Login(User user)
        {
            CurrentUser = user;
        }

        public static void Logout()
        {
            CurrentUser = null;
        }
    }
}