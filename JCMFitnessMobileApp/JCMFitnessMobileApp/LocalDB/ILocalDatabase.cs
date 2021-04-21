using JCMFitnessMobileApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JCMFitnessMobileApp.LocalDB
{
    public interface ILocalDatabase
    {
        Task AddUser(User localUser);
        Task<User> GetUser(string id);


        Task CreateWorkout(Workout localWorkout);
        Task DeleteWorkout(Workout localWorkout);
        Task<List<Workout>> GetWorkouts();
        Task UpdateWorkout(Workout localWorkout);
        Task AddWorkouts(IEnumerable<Workout> workouts);
        Task<Workout> GetWorkoutByID(string workoutID);

        Task<List<Workout>> GetWorkoutExercises(string workoutID);
       
    }
}