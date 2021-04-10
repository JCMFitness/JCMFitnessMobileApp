using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using JCMFitnessMobileApp.Models;
using Xamarin.Forms;
using System.Threading.Tasks;
using JCMFitnessMobileApp.Services;
using Akavache;
using MonkeyCache.FileStore;

namespace JCMFitnessMobileApp.ViewModel
{
    public class MainViewModel : BaseViewModel<User>
    {
        ObservableCollection<Workout> _userWorkouts;
        User _user;
        private bool _isRefreshing;

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


        public bool IsRefreshing
        {
            get => _isRefreshing;
            set
            {
                _isRefreshing = value;
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
            Barrel.ApplicationId = "CachingDataSample";
        }
        public override void Init(User user)
        {
            User = user;

           /* if(User != null)
            {
                Barrel.Current.Add(key: "user", data: User, expireIn: TimeSpan.FromDays(1));
            }*/
            
            LoadEntries();
        }



        public async void LoadEntries()
        {
            var response = Barrel.Current.Get<LoginResponse>(key: "user");

            User = response.User;

            if (IsBusy || User == null)
            {
                IsRefreshing = false;
                return;
            }
              
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
                IsRefreshing = false;
            }
            catch(Exception ex)
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
