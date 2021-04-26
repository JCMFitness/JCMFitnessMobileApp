﻿using JCMFitnessMobileApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace JCMFitnessMobileApp.Services
{
    public interface IFitnessService
    {

        Task DeleteUser(string UserID);
        Task<LoginResponse> LoginUser(UserLogin userLogin);
        Task<User> GetUserById(string id);

        Task EditWorkout(ApiWorkout workout);
        Task EditExercise(ApiExercise exercise);
        Task<SignUpResponse> SignUpUser(UserSignUp userSignUp);

        Task<List<ApiWorkout>> GetUserWorkouts(string userId);

        Task AddNewUserWorkout(string id, Workout workout);

        Task DeleteUserWorkoutById(string userID, string workoutID);

        Task<List<Exercise>> GetWorkoutExercises(string workoutID);

        Task AddWorkoutExercise(string workoutID, Exercise exercise);

        Task DeleteWorkoutExercise(string workoutID, string exerciseID);

        public Task PushSyncWorkout(List<Workout> workouts);
        public Task PushSyncExercises(List<Exercise> exercises);
        public Task PushSyncUser(User user);

    }
}
