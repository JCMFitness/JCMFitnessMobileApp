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
            return await Database.Table<Workout>().ToListAsync();
        }

        public async Task<List<Workout>> GetWorkoutsWithExercises()
        {
            return await Database.GetAllWithChildrenAsync<Workout>();
        }

        public async Task AddWorkout(Workout workout)
        {
            var w = await Database.FindAsync<Workout>(m => m.WorkoutID == workout.WorkoutID);

            if (w == null)
            {
                await Database.InsertAsync(workout);
            }

        }

        public async Task<Workout> WorkoutExists(string workoutID)
        {
            var w = await Database.FindAsync<Workout>(m => m.WorkoutID == workoutID);

            return w;
        }

        public async Task CreateWorkout(Workout localWorkout)
        {
            var w = await WorkoutExists(localWorkout.WorkoutID);

            if (w == null)
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
            return await Database.GetWithChildrenAsync<Workout>(workoutID);
        }

        //////////////////////////////////////////////////////////
        ///

        public async Task<List<Exercise>> GetWorkoutExercises(string workoutID)
        {
            return await Database.Table<Exercise>().Where(w => w.WorkoutID  == workoutID).ToListAsync();
        }

        public async Task AddWorkoutExercises(string WorkoutID, List<Exercise> exercise)
        {
            var workout = await GetWorkoutByID(WorkoutID);

            foreach(var e in exercise)
            {
                await AddExercise(e);
            }

            workout.WorkoutExercises = exercise;


            await Database.UpdateWithChildrenAsync(workout);
        }

        public async Task AddWorkoutExercise(string WorkoutID, Exercise exercise)
        {
            var workout = await GetWorkoutByID(WorkoutID);

            await AddExercise(exercise);

            workout.WorkoutExercises.Add(exercise);


            await Database.UpdateWithChildrenAsync(workout);
        }

        //**********************************************************************************

        public async Task AddExercise(Exercise exercise)
        {
            var w = await Database.FindAsync<Exercise>(m => m.ExerciseID == exercise.ExerciseID);

            if (w == null)
            {
                await Database.InsertAsync(exercise);
            }

        }

        public async Task UpdateExercise(Exercise localExercise)
        {
            await Database.UpdateAsync(localExercise);
        }

        public async Task DeleteExercise(Exercise localExercise)
        {
            await Database.DeleteAsync(localExercise);
        }

        public async Task<Exercise> ExerciseExists(string exerciseID)
        {
            var w = await Database.FindAsync<Exercise>(m => m.ExerciseID == exerciseID);

            return w;
        }




        public async Task ClearDatabase()
        {
            await Database.DeleteAllAsync<Workout>();
            await Database.DeleteAllAsync<Exercise>();
        }
    }
}
