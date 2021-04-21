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
using JCMFitnessMobileApp.ViewModels;
using JCMFitnessMobileApp.LocalDB;

namespace JCMFitnessMobileApp.ViewModel
{
    public class MainViewModel : BaseViewModel<User>
    {
        ObservableCollection<Workout> _userWorkouts;
        User _user;
        private bool _isRefreshing;

        readonly IFitnessService _fitnessService;
        readonly ILocalDatabase _localDatabase;

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

        public Command UserDetailsCommand => new Command(async () =>
         await NavService.NavigateTo<UserDetailViewModel>());


      
        public Command RefreshCommand => new Command(async () => 
                await LoadEntriesAsync());

        public Command SignoutCommand => new Command(async () =>
                await SignoutAsync());


        public MainViewModel(INavService navService, IFitnessService tripLogService, IBlobCache cache, ILocalDatabase localDatabase)
            : base(navService)
        {
            _fitnessService = tripLogService;
            _localDatabase = localDatabase;
            _cache = cache;
            UserWorkouts = new ObservableCollection<Workout>();
            Barrel.ApplicationId = "CachingDataSample";
        }
        public override void Init()
        {

            LoadEntriesAsync();
        }



        public async Task LoadEntriesAsync()
        {
            var response = Barrel.Current.Get<LoginResponse>(key: "user");

            User = response.User;

            var DbUser = await _localDatabase.GetUser(User.Id);

            if (User == null)
            {
                var userId = response.UserId;
                User = await _fitnessService.GetUserById(userId);

                response.User = User;

                Barrel.Current.Add(key: "user", data: response, expireIn: TimeSpan.FromMinutes(10));
            }

            if (IsBusy || User == null)
            {
                IsRefreshing = false;
                return;
            }
              
            IsBusy = true;

            try
            {
                
                var newWorkouts = await _fitnessService.GetUserWorkouts(User.Id);

                UserWorkouts = new ObservableCollection<Workout>(newWorkouts);

                IsRefreshing = false;
                   

                /*foreach (var v in UserWorkouts)
                {
                    await _localDatabase.CreateWorkout(v);
                }*/

                var workouts = await _localDatabase.GetWorkouts();
               
            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                IsBusy = false;
            }
        }

        public async Task SignoutAsync()
        {
            Barrel.Current.EmptyAll();

            
            await NavService.NavigateTo<LandingViewModel>();

        }

    }
}
