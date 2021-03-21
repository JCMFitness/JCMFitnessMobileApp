using System;
using System.Collections.Generic;

namespace JCMFitnessMobileApp.Models
{
    public class Workout
    {
        public long Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public List<Exercise> Exercises { get; set; }
    }
}