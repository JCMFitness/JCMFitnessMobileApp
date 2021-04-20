using JCMFitnessMobileApp.Models;
using JCMFitnessMobileApp.MyConstants;
using SQLite;
using SQLiteNetExtensionsAsync.Extensions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JCMFitnessMobileApp.LocalDB
{
    public class LocalDatabase : ILocalDatabase
    {
        static readonly Lazy<SQLiteAsyncConnection> lazyInitializer = new Lazy<SQLiteAsyncConnection>(() =>
        {
            return new SQLiteAsyncConnection(DBConstants.DatabasePath, DBConstants.Flags);
        });

        static SQLiteAsyncConnection Database => lazyInitializer.Value;
        static bool initialized = false;

        public LocalDatabase()
        {
            InitializeAsync().SafeFireAndForget(false);
        }

        async Task InitializeAsync()
        {
            if (!initialized)
            {

                await Database.CreateTablesAsync(CreateFlags.None, typeof(Models.User)).ConfigureAwait(false);
                await Database.CreateTablesAsync(CreateFlags.None, typeof(Models.Workout)).ConfigureAwait(false);
                await Database.CreateTablesAsync(CreateFlags.None, typeof(Exercise)).ConfigureAwait(false);

                initialized = true;
            }
        }

        public Task<List<Workout>> GetWorkouts()
        {
            return Database.Table<Workout>().ToListAsync();
        }

        public Task AddWorkouts(IEnumerable<Workout> workouts)
        {
            return Database.InsertAllAsync(workouts);
        }

        public Task CreateWorkout(Workout localWorkout)
        {

            return Database.InsertAsync(localWorkout);
        }

        public Task UpdateWorkout(Workout localWorkout)
        {
            return Database.UpdateAsync(localWorkout);
        }

        public Task DeleteWorkout(Workout localWorkout)
        {
            return Database.DeleteAsync(localWorkout);
        }

        //////////////////////////////////////////////////////////
        ///

        public Task<List<Workout>> GetWorkoutExercises(string workoutID)
        {
            return Database.GetAllWithChildrenAsync<Workout>(w => w.WorkoutID == workoutID);
        }

        //**********************************************************************************

        public Task AddUser(User localUser)
        {

            return Database.InsertAsync(localUser);
        }

        public Task<User> GetUser(string id)
        {
            return Database.GetAsync<User>(u => u.Id == id);
        }
        public Task UpdateUser(User user)
        {
            return Database.UpdateAsync(user);
        }


    }
}
