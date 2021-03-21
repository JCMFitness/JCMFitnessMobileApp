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
        [Get("/api/user/login?username=max&password=123")]
        public Task<User> UserLoginAsync(string userid, string pass);

        //User endpoints
        [Get("/api/user?userid={id}")]
        public Task<User> GetUserByIdAsync(string id);

        [Post("/api/user")]
        public Task<User> AddNewUserAsync();

        [Put("/api/user?id={userid}")]
        public Task<User> EditUserByIdAsync(string userid);

        [Delete("/api/user/{id}")]
        public Task<User> DeleteUserByIdAsync(string id);

       //Workout endpoints
        [Get("/api/userworkouts?userid={userid}")]
        public Task<List<Workout>> GetUserWorkoutsAsync(string userid);

        /*   [Get("/api/workout/{id}")]
          [Post("/api/workout")]
          [Put("/api/workout")]
          [Delete("/api/workout/{id}")]

          //UserWorkout
          [Get("/api/userworkout/{id}")]
          [Post("/api/userworkout")]
          [Put("/api/userworkout")]
          [Delete("/api/userworkout/{id}")]*/
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