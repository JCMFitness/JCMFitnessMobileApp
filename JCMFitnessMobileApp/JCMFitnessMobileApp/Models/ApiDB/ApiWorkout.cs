
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JCMFitnessMobileApp.Models
{
    public class ApiWorkout
    {
      
        public string WorkoutID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public bool IsPublic { get; set; }
        public DateTimeOffset LastUpdated { get; set; }
        public bool IsDeleted { get; set; }

        public List<ApiUserWorkout> UserWorkouts { get; set; }
        public List<ApiWorkoutExercises> WorkoutExercises { get; set; }

    }
}
