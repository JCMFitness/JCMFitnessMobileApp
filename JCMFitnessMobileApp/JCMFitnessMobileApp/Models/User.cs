using System;
using System.Collections.Generic;
using System.Text;

namespace JCMFitnessMobileApp.Models
{
    public class User
    {

        public string Id { get; set; } 
        public DateTime JoinedDate { get; set; } 
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string WorkoutList { get; set; }
        public List<Workout> Workouts { get; set; }

    }
}
