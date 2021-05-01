using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using JCMFitnessMobileApp.Models;


namespace JCMFitnessMobileApp.Services
{

    public class FitnessService : IFitnessService
    {
        private readonly IFitApi fitApi;


        public FitnessService(IFitApi newsApi)
        {
            this.fitApi = newsApi;
        }

        public async Task EditExercise(ApiExercise exercise)
        {
            try
            {
                await fitApi.EditExerciseAsync(exercise);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                throw;
            }
        }
        public async Task EditWorkout(ApiWorkout workout)
        {
            try
            {
                await fitApi.EditWorkoutAsync(workout);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<LoginResponse> LoginUser(UserLogin userLogin)
        {
            try
            {
                return await fitApi.UserLoginAsync(userLogin);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                throw new Exception();
            }
        }


        public async Task<SignUpResponse> SignUpUser(UserSignUp userSignUp)
        {
            try
            {
                return await fitApi.UserSignUpAsync(userSignUp);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                throw;
            }

        }

        public async Task<User> GetUserById(string id)
        {
            try
            {
                return await fitApi.GetUserById(id);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<List<Workout>> GetUserWorkouts(string id)
        {
            try
            {
                List<Workout> workouts = new List<Workout>();

                var userWorkouts = await fitApi.GetUserWorkoutsAsync(id);

                foreach(var w in userWorkouts)
                {
                    var LocalWorkout = new Workout
                    {
                        WorkoutID = w.WorkoutID,
                        Name = w.Name,
                        Description = w.Description,
                        Category = w.Category,
                        IsPublic = w.IsPublic,
                        LastUpdated = TimeZoneInfo.ConvertTimeToUtc(DateTime.Now, TimeZoneInfo.Local),
                        IsDeleted = w.IsDeleted,
                        WorkoutExercises = null

                    };

                    workouts.Add(LocalWorkout);
                }

                return workouts;

            }
            catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task AddNewUserWorkout(string id, Workout workout)
        {
            try
            {
                 await fitApi.AddNewUserWorkoutAsync(id, workout);
            }
            catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task DeleteUserWorkoutById(string userID, string workoutID)
        {
            try
            {
                await fitApi.DeleteUserWorkoutAsync(userID, workoutID);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<List<Exercise>> GetWorkoutExercises(string workoutID)
        {
            try
            {
                return await fitApi.GetWorkoutExercisesAsync(workoutID);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task AddWorkoutExercise(string workoutID, Exercise exercise)
        {
            try
            {
                 await fitApi.PostNewWorkoutExerciseAsync(workoutID, exercise);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task DeleteWorkoutExercise(string workoutID, string exerciseID)
        {
            try
            {
                await fitApi.DeleteWorkoutExerciseAsync(workoutID, exerciseID);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task DeleteUser(string UserID)
        {
            try
            {
                await fitApi.DeleteUserAsync(UserID);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task PushSyncWorkout(string UserId, List<Workout> workouts)
        {
            try
            {
                await fitApi.SyncWorkouts(UserId, workouts);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                throw;
            }
            
            
        }

        public async Task PushSyncExercises(string WorkoutId, List<Exercise> exercises)
        {
            try
            {
                await fitApi.SyncExercises(WorkoutId, exercises);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task PushSyncUser(User user)
        {
            try
            {
                await fitApi.SyncUser(user);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                throw;
            }
        }


    }

}


