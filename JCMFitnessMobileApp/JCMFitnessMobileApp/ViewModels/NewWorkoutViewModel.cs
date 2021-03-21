using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using JCMFitnessMobileApp.Models;
using JCMFitnessMobileApp.Services;
using Xamarin.Forms;

namespace JCMFitnessMobileApp.ViewModel
{
    public class NewWorkoutViewModel : BaseValidationViewModel
    {
       
        readonly IFitnessService _fitnessService;

        public NewWorkoutViewModel(INavService navService, IFitnessService fitness)
            : base(navService)
        {
            _fitnessService = fitness;
        }

        public override async void Init()
        {
           
        }

        string _title;
        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                Validate(() => !string.IsNullOrWhiteSpace(_title), "Title must be provided.");
                OnPropertyChanged();
                SaveCommand.ChangeCanExecute();
            }
        }
       
        
        string _notes;
        public string Description
        {
            get => _notes;
            set
            {
                _notes = value;
                OnPropertyChanged();
            }
        }

        Command _saveCommand;
        public Command SaveCommand =>
            _saveCommand ?? (_saveCommand = new Command(async () => await Save(), CanSave));
        async Task Save()
        {

            if (IsBusy)
                return;
            IsBusy = true;
            try
            {
                /*var newItem = new TripLogEntry
                {
                    Title = Title,
                    Latitude = Latitude,
                    Longitude = Longitude,
                    Date = Date,
                    Rating = Rating,
                    Notes = Notes
                };
                await _tripLogService.AddEntryAsync(newItem);*/
                await NavService.GoBack();
            }
            finally
            {
                IsBusy = false;
            }

        }
        bool CanSave() => !string.IsNullOrWhiteSpace(Title) && !HasErrors;



    }
}
