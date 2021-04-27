using JCMFitnessMobileApp.LocalDB;
using JCMFitnessMobileApp.Models;
using MonkeyCache.FileStore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JCMFitnessMobileApp.Services
{
    public class SyncService
    {
        private readonly IFitnessService _fitnessService;
        private readonly ILocalDatabase _localDatabase;


        public SyncService(IFitnessService fitnessService, ILocalDatabase localDatabase)
        {
            _fitnessService = fitnessService;
            _localDatabase = localDatabase;
            Barrel.ApplicationId = "CachingDataSample";
        }



        public async void PushSync()
        {

            var workouts = await _localDatabase.GetWorkouts();

            try
            {
                await _fitnessService.PushSyncWorkout(workouts);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }



            foreach (var v in workouts)
            {
                try
                {
                    await _fitnessService.PushSyncExercises(v.WorkoutExercises);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            var user = Barrel.Current.Get<LoginResponse>(key: "user").User;

            try
            {
                await _fitnessService.PushSyncUser(user);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }



        }

        public async void PullSync()
        {
            var LocalUser = Barrel.Current.Get<LoginResponse>(key: "user").User;
            var ApiUser = new User();

            try
            {
                ApiUser = await _fitnessService.GetUserById(LocalUser.Id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            if(ApiUser.LastUpdated > LocalUser.LastUpdated)
            {
                var loginResponse = Barrel.Current.Get<LoginResponse>(key: "user");
                loginResponse.User = ApiUser;

                Barrel.Current.Add(key: "user", data: loginResponse, expireIn: TimeSpan.FromMinutes(10));
            }



            List<Workout> ApiWorkouts = new List<Workout>();


            try
            {
                ApiWorkouts = await _fitnessService.GetUserWorkouts(LocalUser.Id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


            foreach(var w in ApiWorkouts)
            {
                var localWorkout = await _localDatabase.GetWorkoutByID(w.WorkoutID);


                if (w.LastUpdated > localWorkout.LastUpdated)
                {
                    
                }
            }
           




        }
/*

            // Pull sync is just getting all records that have changed since that date.
            foreach (var row in _fitnessService.PullSync(_lastSync))
                if (!_rows.Any(x => x.Id == row.Id)) // Does not exist, hence insert 
                    InsertRow(new ClientTableSchema(row));
                else if (row.Deleted.HasValue)
                    DeleteRow(row.Id);
                else
                    UpdateRow(new ClientTableSchema(row));

            _lastSync = DateTimeOffset.Now;*/
        

    }
}
