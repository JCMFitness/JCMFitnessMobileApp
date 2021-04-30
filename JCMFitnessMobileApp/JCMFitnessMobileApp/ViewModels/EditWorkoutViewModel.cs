using JCMFitnessMobileApp.LocalDB;
using JCMFitnessMobileApp.Models;
using JCMFitnessMobileApp.Services;
using JCMFitnessMobileApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace JCMFitnessMobileApp.ViewModels
{
    public class EditWorkoutViewModel : BaseViewModel<Workout>
    {
        Workout _workout;
        private readonly INavService navService;
        readonly IFitnessService _fitnessService;
        private readonly ILocalDatabase localDatabase;

        public EditWorkoutViewModel(INavService navService, IFitnessService fitnessService, ILocalDatabase localDatabase)
           : base(navService)
        {
            this.navService = navService;
            _fitnessService = fitnessService;
            this.localDatabase = localDatabase;
        }

        public Workout Workout
        {
            get => _workout;
            set
            {
                _workout = value;
                OnPropertyChanged();
            }
        }

        public override void Init(Workout work)
        {
            Workout = work;
        }

        Command _saveWorkoutEditCommand;
        public Command SaveWorkoutCommand =>
            _saveWorkoutEditCommand ?? (_saveWorkoutEditCommand = new Command(async () => await EditWorkout()));


        async Task EditWorkout()
        {
            if (IsBusy)
                return;
            IsBusy = true;

            try
            {
               

                var current = Connectivity.NetworkAccess;

                if (current == NetworkAccess.Internet)
                {
                    var ApiWorkout = new ApiWorkout
                    {
                        WorkoutID = Workout.WorkoutID,
                        Name = Workout.Name,
                        Description = Workout.Description,
                        Category = Workout.Category,
                        IsPublic = Workout.IsPublic,
                        LastUpdated = TimeZoneInfo.ConvertTimeToUtc(DateTime.Now, TimeZoneInfo.Local),
                        IsDeleted = Workout.IsDeleted

                    };

                    await _fitnessService.EditWorkout(ApiWorkout);
                }
                else
                {
                    var LocalWorkout = new Workout
                    {
                        WorkoutID = Workout.WorkoutID,
                        Name = Workout.Name,
                        Description = Workout.Description,
                        Category = Workout.Category,
                        IsPublic = Workout.IsPublic,
                        LastUpdated = TimeZoneInfo.ConvertTimeToUtc(DateTime.Now, TimeZoneInfo.Local),
                        IsDeleted = Workout.IsDeleted,
                        WorkoutExercises = Workout.WorkoutExercises

                    };

                    await localDatabase.UpdateWorkout(LocalWorkout);
                }


               

                NavService.RemoveLastTwoViews();
                await NavService.NavigateTo<WorkoutDetailViewModel,Workout>(Workout);
            }
            finally
            {
                IsBusy = false;
            }

        }
    }
}
