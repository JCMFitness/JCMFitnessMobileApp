using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using JCMFitnessMobileApp.Models;
using JCMFitnessMobileApp.Services;
using Xamarin.Forms;

namespace JCMFitnessMobileApp.ViewModel
{
    public class WorkoutDetailViewModel : BaseViewModel<Workout>
    {
        Workout _workout;
        readonly IFitnessService _fitnessService;

        public Workout Workout
        {
            get => _workout;
            set
            {
                _workout = value;
                OnPropertyChanged();
            }
        }

        public WorkoutDetailViewModel(INavService navService, IFitnessService fitnessService)
            : base(navService)
        {
            _fitnessService = fitnessService;
        }

        public override void Init(Workout workout)
        {
            Workout = workout;
        }

        Command _deleteCommand;
        public Command DeleteCommand =>
            _deleteCommand ?? (_deleteCommand = new Command(async () => await Delete()));

        async Task Delete()
        {

            try
            {
                //await _fitnessService.DeleteEntryAsync(Entry);
                await NavService.GoBack();
            }
            finally
            {
                IsBusy = false;
            }

        }

        // ...
    }
}
