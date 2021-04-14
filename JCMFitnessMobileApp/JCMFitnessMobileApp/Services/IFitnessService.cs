using JCMFitnessMobileApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace JCMFitnessMobileApp.Services
{
    public interface IFitnessService
    {


        Task<User> LoginUser(string userId, string password);
        Task EditWorkout(Workout workout);
        Task EditExercise(Exercise exercise);
        Task<List<Workout>> GetUserWorkouts(string userId);

        Task AddNewUserWorkout(string id, Workout workout);

        Task DeleteUserWorkoutById(string userID, string workoutID);

        Task<List<Exercise>> GetWorkoutExercises(string workoutID);

        Task AddWorkoutExercise(string workoutID, Exercise exercise);

        Task DeleteWorkoutExercise(string workoutID, string exerciseID);
        Task DeleteExercise(string exercise);
    }
}
