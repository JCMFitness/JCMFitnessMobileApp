using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;

namespace JCMFitnessMobileApp.LocalDB
{
    public class LocalWorkout
    {
        [PrimaryKey, AutoIncrement]
        public string WorkoutID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public bool IsPublic { get; set; }

        [OneToMany(CascadeOperations = CascadeOperation.All)]      // One to many relationship with Valuation
        public List<LocalExercise> Exercises { get; set; }
    }
}