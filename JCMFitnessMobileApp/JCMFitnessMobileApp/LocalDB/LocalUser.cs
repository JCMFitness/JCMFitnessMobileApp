using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JCMFitnessMobileApp.LocalDB
{
    public class LocalUser
    {
        [PrimaryKey, AutoIncrement]
        public string Id { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string UserName { get; set; }
       
        public string Email { get; set; }

        public string PasswordHash { get; set; }

        public DateTime JoinedDate { get; set; } = DateTime.UtcNow;
        public bool IsAdmin { get; set; }
    }
}
