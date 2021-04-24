﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using JCMFitnessMobileApp.Models;
using Refit;

namespace JCMFitnessMobileApp.Services
{
    public interface IFitApi
    {
        //Login
        [Post("/api/authentication/login")]
        public Task<LoginResponse> UserLoginAsync([Body] UserLogin userLogin);

        //SignUp
        [Post("/api/authentication/register")]
        public Task<SignUpResponse> UserSignUpAsync([Body] UserSignUp userSignUp);

        //User endpoints

        [Put("/api/user?id={userid}")]
        public Task EditUserAsync(string userid);

        [Delete("/api/user?userid={id}")]
        public Task DeleteUserAsync(string id);

        [Get("/api/user?userid={id}")]
        public Task<User> GetUserById(string id);

        [Post("/api/user/sync")]
        public Task SyncUser(User user);




        //Workout endpoints
        [Get("/api/workouts/getall")]
        public Task<List<Workout>> GetWorkoutAsync();

        [Get("/api/workouts?workoutid={id}")]
        public Task<Workout> GetWorkoutsAsync(string id);

        [Post("/api/workouts")]
        public Task AddNewWorkoutAsync([Body] Workout workout);

        [Put("/api/workouts")]
        public Task EditWorkoutAsync([Body] Workout workout);

        [Delete("/api/workouts?workouts={id}")]
        public Task DeleteWorkoutAsync(string id);

        [Post("/api/workouts/sync")]
        public Task SyncWorkouts(List<Workout> workouts);


        //UserWorkout
        [Get("/api/userworkouts?userid={userid}")]
        public Task<List<Workout>> GetUserWorkoutsAsync(string userid);

        [Post("/api/userworkouts?userid={id}")]
        public Task AddNewUserWorkoutAsync(string id, [Body] ApiWorkout workout);

        [Put("/api/userworkouts")]
        public Task EditUserWorkoutAsync(string id);

        [Delete("/api/userworkouts?userid={userid}&workoutid={workoutid}")]
        public Task DeleteUserWorkoutAsync(string userid, string workoutid);



        //WorkoutExercise
        [Get("/api/workoutexercises?workoutid={id}")]
        public Task<List<Exercise>> GetWorkoutExercisesAsync(string id);

        [Post("/api/workoutexercises?workoutid={id}")]
        public Task PostNewWorkoutExerciseAsync(string id, [Body] Exercise exercise);


        [Delete("/api/workoutexercises?exerciseid={exerciseid}&workoutid={workoutid}")]
        public Task DeleteWorkoutExerciseAsync(string workoutid, string exerciseid);

        //Exercise
        [Put("/api/excercise")]
        public Task EditExerciseAsync([Body] Exercise exercise);
        
        [Delete("/api/exercise?exerciseid={exerciseid}")]
        public Task DeleteExerciseAsync(string exerciseid);

        [Post("/api/exercise/sync")]
        public Task SyncExercises(List<Exercise> exercises);
    }
}


