using System;
using System.Collections.Generic;
using System.Text;

namespace JCMFitnessMobileApp.Models
{
    class User
    { 
        
            private int Id { get; }
            private string FirstName { get; set; }
            private string LastName { get; set; }
            private string Password { get; set; }
            public List<WorkOut> WorkOutList { get; set; }
            public List<Exercise> UserExercises { get; set; }
        
    }
}
