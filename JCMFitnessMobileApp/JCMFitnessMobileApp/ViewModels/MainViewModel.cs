using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using TripLog.Models;
using Xamarin.Forms;
using System.Threading.Tasks;
using TripLog.Services;
using Akavache;
using JCMFitnessMobileApp.Services;

namespace TripLog.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        ObservableCollection<TripLogEntry> _logEntries;
        readonly ITripLogDataService _tripLogService;
        readonly IBlobCache _cache;

        public ObservableCollection<TripLogEntry> LogEntries
        {
            get => _logEntries;
            set
            {
                _logEntries = value;
                OnPropertyChanged();
            }
        }


        public Command<TripLogEntry> ViewCommand =>
            new Command<TripLogEntry>(async entry =>
                await NavService.NavigateTo<WorkoutDetailViewModel, TripLogEntry>(entry));

        public Command NewCommand =>
            new Command(async () =>
                await NavService.NavigateTo<NewWorkoutViewModel>());


        Command _refreshCommand;
        public Command RefreshCommand =>
            _refreshCommand ?? (_refreshCommand = new Command(LoadEntries));

       
        public MainViewModel(INavService navService, ITripLogDataService tripLogService, IBlobCache cache)
            : base(navService)
        {
            _tripLogService = tripLogService;
            _cache = cache;
            LogEntries = new ObservableCollection<TripLogEntry>();
        }
        public override void Init()
        {
            LoadEntries();
        }



        async void LoadEntries()
        {
            if (IsBusy)
                return;
            IsBusy = true;
            try
            {
                // Load from local cache and then immediately load from API
                _cache.GetAndFetchLatest("entries", async () => await _tripLogService.GetEntriesAsync())
                    .Subscribe(entries =>
                    {
                        LogEntries = new ObservableCollection<TripLogEntry>(entries);
                        IsBusy = false;
                    });
            }
            finally
            {
                IsBusy = false;
            }
        }

    }
}
