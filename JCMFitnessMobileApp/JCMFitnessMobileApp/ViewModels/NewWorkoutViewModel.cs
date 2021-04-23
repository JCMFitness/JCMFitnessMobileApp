using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using JCMFitnessMobileApp.Models;
using JCMFitnessMobileApp.Services;
using MonkeyCache.FileStore;
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

                

                await _fitnessService.AddNewUserWorkout(response.User.Id, newWorkout);
                await NavService.NavigateTo<MainViewModel>();
            }
            finally
            {
                IsBusy = false;
            }

        }
        bool CanSave() => !string.IsNullOrWhiteSpace(Title) && !HasErrors;



    }
}
