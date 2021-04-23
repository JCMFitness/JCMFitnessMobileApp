using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;

namespace JCMFitnessMobileApp.Models
{
    public class Workout
    {
        [PrimaryKey]
        public string WorkoutID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public bool IsPublic { get; set; }

        public DateTimeOffset LastUpdated { get; set; }

        public bool IsDeleted { get; set; }

        [OneToMany(CascadeOperations = CascadeOperation.All)]      // One to many relationship with Valuation
        public List<Exercise> WorkoutExercises { get; set; }
    }
}