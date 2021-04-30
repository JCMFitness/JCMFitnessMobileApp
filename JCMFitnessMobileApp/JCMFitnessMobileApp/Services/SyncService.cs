using JCMFitnessMobileApp.LocalDB;
using JCMFitnessMobileApp.Models;
using MonkeyCache.FileStore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JCMFitnessMobileApp.Services
{
    public class SyncService : ISyncService
    {
        private readonly IFitnessService _fitnessService;
        private readonly ILocalDatabase _localDatabase;


        public SyncService(IFitnessService fitnessService, ILocalDatabase localDatabase)
        {
            _fitnessService = fitnessService;
            _localDatabase = localDatabase;
            Barrel.ApplicationId = "CachingDataSample";
        }



        public async Task PushSync()
        {
            var user = Barrel.Current.Get<LoginResponse>(key: "user").User;

            try
            {

                await _fitnessService.PushSyncUser(user);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }



            var workouts = await _localDatabase.GetWorkouts();

            try
            {
                if (workouts != null && workouts.Count != 0)
                {
                    await _fitnessService.PushSyncWorkout(user.Id, workouts);

                    var DeletedWorkouts = workouts.FindAll(w => w.IsDeleted == true);
                    
                    if(DeletedWorkouts != null && DeletedWorkouts.Count != 0)
                    {

                        foreach (var w in DeletedWorkouts)
                        {
                            await _localDatabase.DeleteWorkout(w);
                        }
                    }
                    
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

           

            foreach (var v in workouts)
            {
                try
                {
                    var workoutExercises = await _localDatabase.GetWorkoutExercises(v.WorkoutID);

                    if(workoutExercises != null && workoutExercises.Count != 0)
                    {
                        await _fitnessService.PushSyncExercises(v.WorkoutID, workoutExercises);

                        var DeletedExercises = workoutExercises.FindAll(w => w.IsDeleted == true);

                        if (DeletedExercises != null && DeletedExercises.Count != 0)
                        {

                            foreach (var e in DeletedExercises)
                            {
                                await _localDatabase.DeleteExercise(e);
                            }
                        }

                    }
                   
                    
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

        }

        public async Task PullSync()
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

            if (ApiUser.LastUpdated > LocalUser.LastUpdated)
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


            foreach (var w in ApiWorkouts)
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
