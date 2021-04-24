﻿using System;
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

        public async Task EditExercise(Exercise exercise)
        {
            try
            {
                await fitApi.EditExerciseAsync(exercise);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                throw;
            }
        }
        public async Task EditWorkout(Workout workout)
        {
            try
            {
                await fitApi.EditWorkoutAsync(workout);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<LoginResponse> LoginUser(UserLogin userLogin)
        {
            try
            {
                return await fitApi.UserLoginAsync(userLogin);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                throw new Exception();
            }
        }


        public async Task<SignUpResponse> SignUpUser(UserSignUp userSignUp)
        {
            try
            {
                return await fitApi.UserSignUpAsync(userSignUp);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                throw;
            }

        }

        public async Task<User> GetUserById(string id)
        {
            try
            {
                return await fitApi.GetUserById(id);
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

        public async Task AddNewUserWorkout(string id, ApiWorkout workout)
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

        public async Task AddWorkoutExercise(string workoutID, Exercise exercise)
        {
            try
            {
                 await fitApi.PostNewWorkoutExerciseAsync(workoutID, exercise);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task DeleteWorkoutExercise(string workoutID, string exerciseID)
        {
            try
            {
                await fitApi.DeleteWorkoutExerciseAsync(workoutID, exerciseID);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task DeleteUser(string UserID)
        {
            try
            {
                await fitApi.DeleteUserAsync(UserID);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task PushSyncWorkout(List<Workout> workouts)
        {
            try
            {
                await fitApi.SyncWorkouts(workouts);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                throw;
            }
            
            
        }

        public async Task PushSyncExercises(List<Exercise> exercises)
        {
            try
            {
                await fitApi.SyncExercises(exercises);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task PushSyncUser(User user)
        {
            try
            {
                await fitApi.SyncUser(user);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                throw;
            }
        }


    }

}


