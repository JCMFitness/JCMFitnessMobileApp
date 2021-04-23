using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace JCMFitnessMobileApp.Models
{
    public class ApiExercise
    {
       
        public string ExerciseID { get; set; }
        public string Name { get; set; }
        public int TimerValue { get; set; }
        public int Repititions { get; set; }
        public int Sets { get; set; }
        public bool IsPublic { get; set; }
        public DateTimeOffset LastUpdated { get; set; }
        public bool IsDeleted { get; set; }

        public List<ApiWorkoutExercises> WorkoutExercises { get; set; }
    }
}
