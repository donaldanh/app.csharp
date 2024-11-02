using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace appTest.Class
{
    public class User
    {
        public string Username { get; set; }
        public string Role { get; set; }
    }
    public class UserContext
    {
        public static User CurrentUser { get; set; }
    }

}
