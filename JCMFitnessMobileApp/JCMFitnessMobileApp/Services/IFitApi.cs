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
        [Get("/api/user/login?username={userid}&password={password}")]
        public Task<User> UserLoginAsync(string userid, string password);

        //User endpoints
        [Get("/api/user?userid={id}")]
        public Task<User> GetUserByIdAsync(string id);

        [Post("/api/user")]
        public Task AddNewUserAsync();

        [Put("/api/user?id={userid}")]
        public Task EditUserByIdAsync(string userid);

        [Delete("/api/user/{id}")]
        public Task DeleteUserByIdAsync(string id);




        //Workout endpoints
        [Get("/api/workouts/getall")]
        public Task<List<Workout>> GetWorkoutAsync();

        [Get("/api/workouts?workoutid={id}")]
        public Task<Workout> GetWorkoutsByIdAsync(string id);

        [Post("/api/workouts")]
        public Task AddNewWorkoutAsync([Body] Workout workout);

        [Put("/api/workouts")]
        public Task EditWorkoutsByIdAsync(string id);

        [Delete("/api/workouts?workouts={id}")]
        public Task DeleteWorkoutsByIdAsync(string id);


        //UserWorkout
        [Get("/api/userworkouts?userid={userid}")]
        public Task<List<Workout>> GetUserWorkoutsAsync(string userid);


        [Post("/api/userworkouts?userid={id}")]
        public Task AddNewUserWorkoutAsync(string id, [Body] Workout workout);

        [Put("/api/userworkouts")]
        public Task EditUserWorkoutByIdAsync(string id);

        [Delete("/api/userworkouts?userid={userid}&workoutid={workoutid}")]
        public Task DeleteUserWorkoutByIdAsync(string userid, string workoutid);



        //WorkoutExercise
        [Get("/api/workoutexercises?workoutid={id}")]
        public Task<List<Exercise>> GetWorkoutExercisesAsync(string id);
    }
}


/*-----------------------User------------------ -
GET / api / user / getall - get all users
GET     /api/user?userid={id}                                     -get user by id
POST    /api/user                                                 -create new user by posting new user object
PUT     /api/user                                                 -update the user by posting changed user object
DELETE  /api/user?userid={id}                                     -delete user
GET     /api/user/login?username={username}&password ={ password}
-verify user login, successful returns user object, if not returns 400 error message
-----------------------Workout-------------
GET     /api/workouts/getall                                      -get all workouts
GET     /api/workouts?workoutid={id}                              -get workout by id
POST    /api/workouts                                             -create new workout by posting new workout object
PUT     /api/workouts                                             -update the workout by posting changed user object
DELETE  /api/workouts?workoutid={id}                              -delete workout
---------------------- - UserWorkout------------ -
GET / api / userworkouts / getall - get all userworkouts
GET     /api/userworkouts?userid={userid}                         -get userworkout by passing user id (returns all the user workouts)
POST / api / userworkouts ? userid ={ userid}
-create new userworkout by passing user id and workout object
PUT     /api/userworkouts                                         -update the userworkout passing changed userworkout object
DELETE  /api/userworkouts?userid={userid}&workoutid ={ workoutid}
-delete one workout associated by one user by passing user and workout id
DELETE  /api/userworkouts/deleteall?userid={userid}               -delete all the workouts associated by the user*/