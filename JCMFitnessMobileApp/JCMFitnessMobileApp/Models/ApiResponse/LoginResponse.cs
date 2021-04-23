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
        public string Email { get; set; }
        public string UserId { get; set; }
    }
}
