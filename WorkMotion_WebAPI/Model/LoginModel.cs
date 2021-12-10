using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkMotion_WebAPI.Model
{
    public class LoginModel
    {
        public class InputLoginModel
        {
            public string Username { get; set; }
            public string Password { get; set; }
        }

        public class CustomerLoginModel
        {
            public string Username { get; set; }
            public string Password { get; set; }
        }
    }
}
