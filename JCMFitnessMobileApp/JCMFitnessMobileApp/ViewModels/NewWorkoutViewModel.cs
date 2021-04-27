using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using JCMFitnessMobileApp.LocalDB;
using JCMFitnessMobileApp.Models;
using JCMFitnessMobileApp.Services;
using MonkeyCache.FileStore;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace JCMFitnessMobileApp.ViewModel
{
    public class NewWorkoutViewModel : BaseValidationViewModel
    {

        readonly IFitnessService _fitnessService;

        readonly ILocalDatabase _localDatabase;

        public NewWorkoutViewModel(INavService navService, IFitnessService fitness, ILocalDatabase localDatabase)
            : base(navService)
        {
            _fitnessService = fitness;
            _localDatabase = localDatabase;
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


        string _description;
        public string Description
        {
            get => _description;
            set
            {
                _description = value;
                OnPropertyChanged();
            }
        }



        string _category;
        public string Category
        {
            get => _category;
            set
            {
                _category = value;
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
                var newWorkout = new Workout
                {
                    WorkoutID = Guid.NewGuid().ToString(),
                    Name = Title,
                    Description = Description,
                    Category = Category,
                    IsPublic = false

                };

                var response = Barrel.Current.Get<LoginResponse>(key: "user");

                var current = Connectivity.NetworkAccess;

                if (current == NetworkAccess.Internet)
                {
                    await _fitnessService.AddNewUserWorkout(response.User.Id, newWorkout);
                }
                else
                {
                    await _localDatabase.AddWorkout(newWorkout);
                }


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
