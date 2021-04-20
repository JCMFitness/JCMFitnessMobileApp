using SQLite;
using SQLiteNetExtensions.Attributes;
using System.Collections.Generic;


namespace JCMFitnessMobileApp.Models
{
    public class Exercise
    {
        [PrimaryKey]
        public string ExerciseID { get; set; }
        public string Name { get; set; }
        public int TimerValue { get; set; }
        public int Repititions { get; set; }
        public int Sets { get; set; }
        public bool IsPublic { get; set; }


        [ForeignKey(typeof(Workout))]     // Specify the foreign key
        public int WorkoutID { get; set; }


        [ManyToOne]      // Many to one relationship with Stock
        public Workout Workout { get; set; }
    }
}