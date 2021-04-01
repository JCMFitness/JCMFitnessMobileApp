using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using JCMFitnessMobileApp.Models;


namespace JCMFitnessMobileApp.Services
{

    public class FitnessService : IFitnessService
    {
        private readonly IFitApi fitApi;


        public FitnessService(IFitApi newsApi)
        {
            this.fitApi = newsApi;
        }


        public async Task<User> LoginUser(string id, string password)
        {
            try
            {
                return await fitApi.UserLoginAsync(id, password);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<List<Workout>> GetUserWorkouts(string id)
        {
            try
            {
                return await fitApi.GetUserWorkoutsAsync(id);
            }
            catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task AddNewUserWorkout(string id, Workout workout)
        {
            try
            {
                 await fitApi.AddNewUserWorkoutAsync(id, workout);
            }
            catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task DeleteUserWorkoutById(string userID, string workoutID)
        {
            try
            {
                await fitApi.DeleteUserWorkoutAsync(userID, workoutID);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<List<Exercise>> GetWorkoutExercises(string workoutID)
        {
            try
            {
                return await fitApi.GetWorkoutExercisesAsync(workoutID);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                throw;
            }
        }



    }

}


