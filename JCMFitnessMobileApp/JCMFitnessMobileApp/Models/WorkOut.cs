using System.Collections.Generic;

namespace JCMFitnessMobileApp.Models
{
    public class WorkOut
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<Exercise> ExercisesInWorkOut { get; set; }
    }
}