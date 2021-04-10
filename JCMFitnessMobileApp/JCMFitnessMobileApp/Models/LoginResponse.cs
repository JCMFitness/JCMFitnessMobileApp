using System;
using System.Collections.Generic;
using System.Text;

namespace JCMFitnessMobileApp.Models
{
    public class LoginResponse
    {
        public string Token { get; set; }
        public string ValidTo { get; set; }

        public User User { get; set; }
    }
}
