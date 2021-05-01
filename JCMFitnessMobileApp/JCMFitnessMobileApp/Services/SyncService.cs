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

            var LastSyncTime = Barrel.Current.Get<DateTime>(key: "sync");

            if (LastSyncTime != null)
            {
                var user = Barrel.Current.Get<LoginResponse>(key: "user").User;

                try
                {
                    if(user.LastUpdated > LastSyncTime)
                    {
                        await _fitnessService.PushSyncUser(user);
                    }
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
                        var ModifiedWorkouts = workouts.Where(m => m.LastUpdated > LastSyncTime).ToList();

                        await _fitnessService.PushSyncWorkout(user.Id, ModifiedWorkouts);

                        var DeletedWorkouts = ModifiedWorkouts.FindAll(w => w.IsDeleted == true);

                        if (DeletedWorkouts != null && DeletedWorkouts.Count != 0)
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

                        if (workoutExercises != null && workoutExercises.Count != 0)
                        {
                            var ModifiedExercises = workoutExercises.Where(m => m.LastUpdated > LastSyncTime).ToList();

                            await _fitnessService.PushSyncExercises(v.WorkoutID, ModifiedExercises);

                            var DeletedExercises = ModifiedExercises.FindAll(w => w.IsDeleted == true);

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

        }

        public async Task PullSync()
        {

            var LastSyncTime = Barrel.Current.Get<DateTime>(key: "sync");

            if (LastSyncTime != null)
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

                    Barrel.Current.Add(key: "user", data: loginResponse, expireIn: TimeSpan.FromHours(1));
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

                    if(localWorkout == null)
                    {
                        await _localDatabase.AddWorkout(w);
                    }
                    else if (w.LastUpdated > localWorkout.LastUpdated)
                    {
                        w.WorkoutExercises = localWorkout.WorkoutExercises;

                        await _localDatabase.UpdateWorkout(w);
                    }
                }


                //List<Exercise> ApiExercises = new List<Exercise>();

                foreach(var w in ApiWorkouts)
                {
                    var apiExercises = await _fitnessService.GetWorkoutExercises(w.WorkoutID);

                    //var localExercises = await _localDatabase.GetWorkoutExercises(w.WorkoutID);

                    foreach(var e in apiExercises)
                    {
                        if(_localDatabase.ExerciseExists(e.ExerciseID) == null)
                        {
                            await _fitnessService.AddWorkoutExercise(w.WorkoutID, e);
                        }
                        else
                        {
                            var localExercise = await _localDatabase.ExerciseExists(e.ExerciseID);

                            if(e.LastUpdated > localExercise.LastUpdated)
                            {
                                e.Workout = localExercise.Workout;
                                e.WorkoutID = localExercise.WorkoutID;

                                await _localDatabase.UpdateExercise(e);
                            }
                        }
                    }

                   
                }


                Barrel.Current.Add(key: "sync", data: TimeZoneInfo.ConvertTimeToUtc(DateTime.Now, TimeZoneInfo.Local), expireIn: TimeSpan.FromMinutes(5));
            }

        }



       public async Task PopulateLocalDBInitial()
       {
            List<Workout> ApiWorkouts = new List<Workout>();

            var LocalUser = Barrel.Current.Get<LoginResponse>(key: "user").User;

            try
            {
                ApiWorkouts = await _fitnessService.GetUserWorkouts(LocalUser.Id);

                foreach(var w in ApiWorkouts)
                {
                    await _localDatabase.AddWorkout(w);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }




            //List<Exercise> ApiExercises = new List<Exercise>();

            foreach (var w in ApiWorkouts)
            {
                var apiExercises = await _fitnessService.GetWorkoutExercises(w.WorkoutID);

                //var localExercises = await _localDatabase.GetWorkoutExercises(w.WorkoutID);

                foreach (var e in apiExercises)
                {
                  
                   await _fitnessService.AddWorkoutExercise(w.WorkoutID, e);
                    
                   
                }

            }
        }


    }
}
