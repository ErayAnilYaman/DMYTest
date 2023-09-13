using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DMYTest.Web.Models
{
    public class User
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string RePassword { get; set; }
        public bool Status { get; set; }
        public string Role { get; set; }

    }
}