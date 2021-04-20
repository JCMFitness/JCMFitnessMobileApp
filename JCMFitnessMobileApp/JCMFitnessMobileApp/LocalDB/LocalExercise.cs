using SQLite;
using SQLiteNetExtensions.Attributes;
using System.Collections.Generic;

namespace JCMFitnessMobileApp.LocalDB
{
    public class LocalExercise
    {
        [PrimaryKey, AutoIncrement]
        public string ExerciseID { get; set; }
        public string Name { get; set; }
        public int TimerValue { get; set; }
        public int Repititions { get; set; }
        public int Sets { get; set; }
        public bool IsPublic { get; set; }

        [ForeignKey(typeof(LocalWorkout))]     // Specify the foreign key
        public int WorkoutID { get; set; }


        [ManyToOne]      // Many to one relationship with Stock
        public LocalWorkout Workout { get; set; }
    }
}