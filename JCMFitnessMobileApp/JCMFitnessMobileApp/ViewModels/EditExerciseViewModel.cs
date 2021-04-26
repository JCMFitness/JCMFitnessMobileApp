using JCMFitnessMobileApp.Models;
using JCMFitnessMobileApp.Services;
using JCMFitnessMobileApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace JCMFitnessMobileApp.ViewModels
{
    public class EditExerciseViewModel : BaseViewModel<Exercise>
    {
       Exercise _exercise;
        readonly IFitnessService _fitnessService;
        public EditExerciseViewModel(INavService navService, IFitnessService fitnessService)
           : base(navService)
        {
            _fitnessService = fitnessService;
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
