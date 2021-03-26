using System;
using System.Collections.Generic;

namespace JCMFitnessMobileApp.Models
{
    public class Workout
    {

        public string WorkoutID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public bool IsPublic { get; set; }

        public List<UserWorkout> UserWorkouts { get; set; }
        public List<WorkoutExercises> WorkoutExercises { get; set; }
    }
}