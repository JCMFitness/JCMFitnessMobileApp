using System.Collections.Generic;

namespace JCMFitnessMobileApp.Models
{
    public class Exercise
    {

        public string ExerciseID { get; set; }
        public string Name { get; set; }
        public int TimerValue { get; set; }
        public int Repititions { get; set; }
        public int Sets { get; set; }
        public bool IsPublic { get; set; }
        public List<WorkoutExercises> WorkoutExercises { get; set; }
    }
}