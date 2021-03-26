using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using JCMFitnessMobileApp.Models;
using Xamarin.Forms;
using System.Threading.Tasks;
using JCMFitnessMobileApp.Services;
using Akavache;

namespace JCMFitnessMobileApp.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        ObservableCollection<Workout> _userWorkouts;
        User _user;

        readonly IFitnessService _fitnessService;

        readonly IBlobCache _cache;


        public User User
        {
            get => _user;
            set
            {
                _user = value;
                OnPropertyChanged();
            }


        }
        public ObservableCollection<Workout> UserWorkouts
        {
            get => _userWorkouts;
            set
            {
                _userWorkouts = value;
                OnPropertyChanged();
            }
        }


        public Command<Workout> ViewCommand =>
            new Command<Workout>(async entry =>
                await NavService.NavigateTo<WorkoutDetailViewModel, Workout>(entry));

        public Command NewCommand =>
            new Command(async () =>
                await NavService.NavigateTo<NewWorkoutViewModel>());


        Command _refreshCommand;
        public Command RefreshCommand =>
            _refreshCommand ?? (_refreshCommand = new Command(LoadEntries));


        public MainViewModel(INavService navService, IFitnessService tripLogService, IBlobCache cache)
            : base(navService)
        {
            _fitnessService = tripLogService;
            _cache = cache;
            UserWorkouts = new ObservableCollection<Workout>();
        }
        public async override void Init()
        {
            await LoginUser();
            LoadEntries();
        }

        public async Task LoginUser()
        {
            if (IsBusy)
                return;
            IsBusy = true;

            try
            {


                // Username and password needs to be entered by the user here

                User = await _fitnessService.LoginUser("chancey", "1234");
                IsBusy = false;
            }
            catch
            {
                throw;
            }
            finally
            {
                IsBusy = false;
            }
        }

        public async void LoadEntries()
        {
            if (IsBusy)
                return;
            IsBusy = true;

            try
            {
                // Load from local cache and then immediately load from API
                _cache.GetAndFetchLatest("userworkouts", async () => await _fitnessService.GetUserWorkouts(User.UserID))
                    .Subscribe(workouts =>
                    {
                        ObservableCollection<Workout> newWorkouts = new ObservableCollection<Workout>(workouts);
                        UserWorkouts = new ObservableCollection<Workout>(newWorkouts);
                        IsBusy = false;
                    });
            }
            catch
            {
                throw;
            }
            finally
            {
                IsBusy = false;
            }
        }

    }
}
