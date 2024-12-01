using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI
{
    public static class UserSession
    {
        public static User LoggedInUser { get; set; }

        public static bool IsLoggedIn()
        {
            return LoggedInUser != null;
        }
    }
}
