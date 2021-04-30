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
    public class EditExerciseViewModel : BaseViewModel<Exercise>
    {
       Exercise _exercise;
        private readonly INavService navService;
        readonly IFitnessService _fitnessService;
        private readonly ILocalDatabase localDatabase;

        public EditExerciseViewModel(INavService navService, IFitnessService fitnessService, ILocalDatabase localDatabase)
           : base(navService)
        {
            this.navService = navService;
            _fitnessService = fitnessService;
            this.localDatabase = localDatabase;
        }

        public Exercise Exercise
        {
            get => _exercise;
            set
            {
                _exercise = value;
                OnPropertyChanged();
            }
        }

        public override void Init(Exercise exer)
        {
            Exercise = exer;
        }

        Command _saveExerciseEditCommand;
        public Command SaveExerciseCommand =>
            _saveExerciseEditCommand ?? (_saveExerciseEditCommand = new Command(async () => await EditExercise()));


        async Task EditExercise()
        {
            if (IsBusy)
                return;
            IsBusy = true;

            try
            {

                var current = Connectivity.NetworkAccess;

                if (current == NetworkAccess.Internet)
                {
                    var ApiExercise = new ApiExercise
                    {
                        ExerciseID = Exercise.ExerciseID,
                        Name = Exercise.Name,
                        TimerValue = Exercise.TimerValue,
                        Repititions = Exercise.Repititions,
                        Sets = Exercise.Sets,
                        IsPublic = Exercise.IsPublic,
                        LastUpdated = TimeZoneInfo.ConvertTimeToUtc(DateTime.Now, TimeZoneInfo.Local),
                        IsDeleted = Exercise.IsDeleted
                    };

                    await _fitnessService.EditExercise(ApiExercise);
                }
                else
                {
                    var LocalExercise = new Exercise
                    {
                        ExerciseID = Exercise.ExerciseID,
                        Name = Exercise.Name,
                        TimerValue = Exercise.TimerValue,
                        Repititions = Exercise.Repititions,
                        Sets = Exercise.Sets,
                        IsPublic = Exercise.IsPublic,
                        LastUpdated = TimeZoneInfo.ConvertTimeToUtc(DateTime.Now, TimeZoneInfo.Local),
                        IsDeleted = Exercise.IsDeleted,
                        Workout = Exercise.Workout,
                        WorkoutID = Exercise.WorkoutID
                    };

                    await localDatabase.UpdateExercise(LocalExercise);
                }



               

                NavService.RemoveLastTwoViews();
                await NavService.NavigateTo<ExerciseDetailViewModel, Exercise>(Exercise);
            }
            finally
            {
                IsBusy = false;
            }

        }
    }
}
