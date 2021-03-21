﻿using JCMFitnessMobileApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace JCMFitnessMobileApp.Services
{
    public interface IFitnessService
    {

        //Task<List<User>> GetUsers();


        Task<User> GetUserById(string id);

        Task<User> LoginUser(string userId, string password);

        Task<List<Workout>> GetUserWorkouts(string userId);
    }
}
