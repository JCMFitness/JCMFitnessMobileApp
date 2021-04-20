using JCMFitnessMobileApp.MyConstants;
using SQLite;
using SQLiteNetExtensionsAsync.Extensions;
using SQLiteNetExtensions;
using SQLitePCL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JCMFitnessMobileApp.LocalDB
{
    public class LocalDatabase
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
                
                await Database.CreateTablesAsync(CreateFlags.None, typeof(LocalUser)).ConfigureAwait(false);
                await Database.CreateTablesAsync(CreateFlags.None, typeof(LocalWorkout)).ConfigureAwait(false);
                await Database.CreateTablesAsync(CreateFlags.None, typeof(LocalExercise)).ConfigureAwait(false);

                initialized = true;
            }
        }

        public Task<List<LocalWorkout>> GetWorkouts()
        {
            return Database.Table<LocalWorkout>().ToListAsync();
        }

        public Task CreateWorkout(LocalWorkout localWorkout)
        {
            
           return Database.InsertAsync(localWorkout);
        }

        public Task UpdateWorkout(LocalWorkout localWorkout)
        {
            return Database.UpdateAsync(localWorkout);
        }

        public Task DeleteWorkout(LocalWorkout localWorkout)
        {
            return Database.DeleteAsync(localWorkout);
        }

        //////////////////////////////////////////////////////////
        ///

        public Task<List<LocalWorkout>> getWorkoutExercises(string workoutID)
        {
            return Database.GetAllWithChildrenAsync<LocalWorkout>(w => w.WorkoutID == workoutID);
        }

    }
}
