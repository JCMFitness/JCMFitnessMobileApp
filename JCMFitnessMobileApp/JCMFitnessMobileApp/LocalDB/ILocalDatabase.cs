using JCMFitnessMobileApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JCMFitnessMobileApp.LocalDB
{
    public interface ILocalDatabase
    {

        Task CreateWorkout(Workout localWorkout);
        Task DeleteWorkout(Workout localWorkout);
        Task<List<Workout>> GetWorkouts();
        Task UpdateWorkout(Workout localWorkout);
        Task AddWorkout(Workout workouts);
        Task<Workout> GetWorkoutByID(string workoutID);
        Task<List<Workout>> GetWorkoutsWithExercises();


        Task<List<Exercise>> GetWorkoutExercises(string workoutID);
        Task AddWorkoutExercises(string WorkoutID, List<Exercise> exercise);
        Task AddWorkoutExercise(string WorkoutID, Exercise exercise);


    }
}