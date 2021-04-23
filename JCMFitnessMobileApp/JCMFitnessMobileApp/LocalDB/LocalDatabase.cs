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

                await Database.CreateTablesAsync(CreateFlags.None, typeof(Models.Workout)).ConfigureAwait(false);
                await Database.CreateTablesAsync(CreateFlags.None, typeof(Exercise)).ConfigureAwait(false);

                initialized = true;
            }
        }

        public async Task<List<Workout>> GetWorkouts()
        {
            return await Database.GetAllWithChildrenAsync<Workout>();
        }

        public async Task AddWorkout(Workout workout)
        {
            
            if (ExistingWorkout(workout.WorkoutID))
            {
                await Database.InsertWithChildrenAsync(workout);
            }

        }

        public bool ExistingWorkout(string workoutID)
        {
            var b = Database.FindAsync<Workout>(m => m.WorkoutID == workoutID) == null;
            return b;
        }

        public async Task CreateWorkout(Workout localWorkout)
        {
            if (ExistingWorkout(localWorkout.WorkoutID))
            {
                await Database.InsertAsync(localWorkout);
            }
            
        }

        public async Task UpdateWorkout(Workout localWorkout)
        {
            await Database.UpdateAsync(localWorkout);
        }

        public async Task DeleteWorkout(Workout localWorkout)
        {
            await Database.DeleteAsync(localWorkout);
        }

        public async Task<Workout> GetWorkoutByID(string workoutID)
        {
            return await Database.GetAsync<Workout>(u => u.WorkoutID == workoutID);
        }

        //////////////////////////////////////////////////////////
        ///

        public async Task<List<Exercise>> GetWorkoutExercises(string workoutID)
        {
            return await Database.GetAllWithChildrenAsync<Exercise>(w => w.WorkoutID  == workoutID);
        }

        public async Task AddWorkoutExercises(IEnumerable<Exercise> exercise)
        {

            await Database.InsertAllWithChildrenAsync(exercise);
        }

        //**********************************************************************************


    }
}
