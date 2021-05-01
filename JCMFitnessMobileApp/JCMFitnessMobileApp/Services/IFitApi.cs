using System;
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
        public Task EditWorkoutAsync([Body] ApiWorkout workout);

        [Delete("/api/workouts?workouts={id}")]
        public Task DeleteWorkoutAsync(string id);

       


        //UserWorkout
        [Get("/api/userworkouts?userid={userid}")]
        public Task<List<Workout>> GetUserWorkoutsAsync(string userid);

        [Post("/api/userworkouts?userid={id}")]
        public Task AddNewUserWorkoutAsync(string id, [Body] Workout workout);

        [Put("/api/userworkouts")]
        public Task EditUserWorkoutAsync(string id);

        [Delete("/api/userworkouts?userid={userid}&workoutid={workoutid}")]
        public Task DeleteUserWorkoutAsync(string userid, string workoutid);

        [Post("/api/userworkouts/sync?userid={id}")]
        public Task SyncWorkouts(string id, List<Workout> workouts);



        //WorkoutExercise
        [Get("/api/workoutexercises?workoutid={id}")]
        public Task<List<Exercise>> GetWorkoutExercisesAsync(string id);

        [Post("/api/workoutexercises?workoutid={id}")]
        public Task PostNewWorkoutExerciseAsync(string id, [Body] Exercise exercise);


        [Delete("/api/workoutexercises?exerciseid={exerciseid}&workoutid={workoutid}")]
        public Task DeleteWorkoutExerciseAsync(string workoutid, string exerciseid);

        [Post("/api/workoutexercises/sync?workoutid={id}")]
        public Task SyncExercises(string id, List<Exercise> exercises);

        //Exercise
        [Put("/api/exercise")]
        public Task EditExerciseAsync([Body] ApiExercise exercise);
        
        [Delete("/api/exercise?exerciseid={exerciseid}")]
        public Task DeleteExerciseAsync(string exerciseid);

       
    }
}


