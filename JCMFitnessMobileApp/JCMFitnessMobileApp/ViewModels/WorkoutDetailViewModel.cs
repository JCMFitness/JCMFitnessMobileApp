using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TripLog.Models;
using TripLog.Services;
using Xamarin.Forms;

namespace TripLog.ViewModel
{
    public class WorkoutDetailViewModel : BaseViewModel<TripLogEntry>
    {
        TripLogEntry _entry;
        readonly ITripLogDataService _tripLogService;

        public TripLogEntry Entry
        {
            get => _entry;
            set
            {
                _entry = value;
                OnPropertyChanged();
            }
        }

        public WorkoutDetailViewModel(INavService navService, ITripLogDataService tripLogService)
            : base(navService)
        {
            _tripLogService = tripLogService;
        }

        public override void Init(TripLogEntry parameter)
        {
            Entry = parameter;
        }

        Command _deleteCommand;
        public Command DeleteCommand =>
            _deleteCommand ?? (_deleteCommand = new Command(async () => await Delete()));

        async Task Delete()
        {

            try
            {
                await _tripLogService.DeleteEntryAsync(Entry);
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
