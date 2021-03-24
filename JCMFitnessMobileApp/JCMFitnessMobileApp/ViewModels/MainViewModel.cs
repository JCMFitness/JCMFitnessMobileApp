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

        readonly IFitnessService _fitnessService;

        readonly IBlobCache _cache;

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
        public override void Init()
        {
            LoadEntries();
        }



        public async void LoadEntries()
        {
            if (IsBusy)
                return;
            IsBusy = true;

            try
            {
                // Load from local cache and then immediately load from API
                _cache.GetAndFetchLatest("entries", async () => await _fitnessService.GetUserWorkouts("2"))
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
