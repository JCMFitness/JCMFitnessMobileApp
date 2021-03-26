using System;
using System.Collections.Generic;
using System.Text;

namespace JCMFitnessMobileApp.Models
{
    public class WorkoutExercises
    {
        public string Id { get; set; }
        public string ExerciseID { get; set; }
        public Exercise Exercise { get; set; }

        public string WorkoutID { get; set; }
        public Workout Workout { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public bool IsPublic { get; set; }
    }
}
