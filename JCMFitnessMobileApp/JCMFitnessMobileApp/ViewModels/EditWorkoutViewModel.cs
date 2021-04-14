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
    public class EditWorkoutViewModel : BaseViewModel<Workout>
    {
        Workout _workout;
        readonly IFitnessService _fitnessService;
        public EditWorkoutViewModel(INavService navService, IFitnessService fitnessService)
           : base(navService)
        {
            _fitnessService = fitnessService;
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
                await _fitnessService.EditWorkout(Workout);

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
