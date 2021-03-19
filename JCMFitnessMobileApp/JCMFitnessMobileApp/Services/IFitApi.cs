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

      /*  //Workout endpoints
        [Get("/api/workout")]
        [Get("/api/workout/{id}")]
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
//-----------------------User------------------ -
//GET / api / user - get all users
//GET     /api/user/{id}         -get user by id
//POST    /api/user              -create new user by posting new user object
//PUT     /api/user              -update the user by id
//DELETE  /api/user/{id}         -delete user by id

//-----------------------Workout-------------------
//GET     /api/workout              -get all workouts
//GET     /api/workout/{id}         -get workout by id
//POST    /api/workout              -create new workout by posting new workout object
//PUT     /api/workout              -update the workout by id
//DELETE  /api/workout/{id}         -delete workout by id

//-----------------------UserWorkout-------------------
//GET     /api/userworkout              -get all userworkouts
//GET     /api/userworkout/{id}         -get userworkout by passing user id (returns all the user workouts)
//POST / api / userworkout - create new userworkout by passing user id and workout id
//PUT     /api/userworkout              -update the userworkout by id
//DELETE  /api/userworkout/{id}         -delete userworkout by id